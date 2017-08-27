using System;
using System.Web;
using log4net;
using System.Threading;
using System.Security.Principal;
using AphidBytes.Web.Extensions;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Accounts.Contracts;
using System.Web.Mvc;

namespace AphidBytes.Web.Session_Helper
{
    public class AphidSession
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AphidSession));
        private readonly HttpContext _httpContext;

        #region Constants

        private const string AphidSessionContextItemsKey = "AphidSession.Current";
        private const string AnonymousUserContext = "s$l@xc0sk3ZSn4<9xl1nyi6";

        internal static class CookieNames
        {
            public const string Identity = "ident";
        }

        private static class IdentityCookieItemNames
        {
            public const string AuthToken = "a";
            public const string UserId = "u";
        }

        #endregion

        #region Initialization

        private AphidSession(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public static AphidSession Current
        {
            get { return GetSessionForContext(HttpContext.Current); }
        }

        private static AphidSession GetSessionForContext(HttpContext context)
        {
            var thorSession = context.Items[AphidSessionContextItemsKey] as AphidSession;
            if (thorSession == null)
            {
                context.Items[AphidSessionContextItemsKey] = thorSession = new AphidSession(context);
            }
            return thorSession;
        }

        #endregion

        #region User

        private IAphidPrincipal _authenticatedUser;

        //[CanBeNull]
        public IAphidPrincipal AuthenticatedUser
        {
            get { return _getOrInitAuthenticatedUser(true); }
            private set { _authenticatedUser = value; }
        }

        private IAphidPrincipal _getOrInitAuthenticatedUser(bool warnIfNotYetAuthenticated)
        {
            // if user is null, ensure authentication has been performed
            if (_authenticatedUser == null && TryAuthenticate() && warnIfNotYetAuthenticated)
            {
                Log.Warn($"Attempt to access AuthenticatedUser in an unauthenticated session. Session should have been authenticated in AuthenticationModule. StackTrace={Environment.StackTrace}");
            }

            return _authenticatedUser;
        }

        public bool IsAuthenticated
        {
            get { return _getOrInitAuthenticatedUser(false) != null; }
        }

        #endregion

        #region Identity

        public void SetIdentity(LoginProfileDto profile, DateTime expires)
        {
            if (profile == null)
            {
                Log.Warn($"SetIdentity shouldn't be called with a null profile. Falling back to ClearIdentity(). StackTrace={Environment.StackTrace}"); 
                ClearIdentity();
                return;
            }

            // ensure profile is complete
            if (string.IsNullOrWhiteSpace(profile.Token)) throw new ArgumentException("Token property of profile cannot be empty.", "profile");
            if (string.IsNullOrWhiteSpace(profile.UserId)) throw new ArgumentException("User ID property of profile cannot be empty.", "profile");

            // always create a fresh cookie to simplify logic and ensure consistency; we don't need to keep any remains from the old cookie, should it exist
            var identityCookie = new HttpCookie(CookieNames.Identity)
            {
                Expires = expires
            };
            identityCookie.Values[IdentityCookieItemNames.AuthToken] = _httpContext.Server.UrlEncode(profile.Token);
            identityCookie.Values[IdentityCookieItemNames.UserId] = _httpContext.Server.UrlEncode(profile.UserId);

            _httpContext.Response.Cookies.Add(identityCookie);

            LoadIdentity(profile);
        }

        public void ClearIdentity()
        {
            _httpContext.DeleteCookie(CookieNames.Identity);
            LoadIdentity(null);
        }

        private void LoadIdentity(LoginProfileDto profile)
        {
            var principal = profile == null ? null : new AphidPrincipal(new AphidIdentity(profile));
            _httpContext.User = principal;
            Thread.CurrentPrincipal = principal;
            AuthenticatedUser = principal;
        }

        #endregion

        #region Authentication

        private bool _hasAttemptedAuthentication;

        internal static void AuthenticateContext(HttpContext context)
        {
            var thorSession = GetSessionForContext(context);
            thorSession.TryAuthenticate();
        }

        private bool TryAuthenticate()
        {
            if (_hasAttemptedAuthentication) return false;
            _hasAttemptedAuthentication = true;

            var authToken = GetAuthToken(_httpContext);

            var profile = authToken == null ? null : AuthenticateWithToken(authToken);

            LoadIdentity((profile == null || String.IsNullOrWhiteSpace(profile.UserId)) ? null : profile);

            return true;
        }

        private static LoginProfileDto AuthenticateWithToken(string token)
        {
            IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
            return accountsService.LoginWithToken(token);
        }

        private static string GetAuthToken(HttpContext context)
        {
            var authToken = GetCookieAuthToken(context); 
            if (string.IsNullOrWhiteSpace(authToken)) return null;

            // TODO this seems unnecessary. Why do we need a token to signify anonymous?
            // this appears to be only for service contexts; logging to do some follow-up to ensure this is actually necessary here.
            if (authToken.Equals(AnonymousUserContext, StringComparison.Ordinal))
            {
                Log.Info("Anonymous user context ignored.");
                return null;
            }
            return authToken;
        }

        private static string GetCookieAuthToken(HttpContext context)
        {
            var cookie = context.Request.Cookies[CookieNames.Identity];
            if (cookie == null) return null;
            var cookieValue = cookie.Values[IdentityCookieItemNames.AuthToken];
            return String.IsNullOrWhiteSpace(cookieValue) ? null : context.Server.UrlDecode(cookieValue);
        }

        #endregion
    }

    public interface IAphidPrincipal
    {
        /// <summary>
        ///     The identity of the authenticated user.
        /// </summary>
        IAphidIdentity Identity { get; }

        /// <summary>
        ///     A conversion to <see cref="IPrincipal" /> that is guaranteed to be not null.
        /// </summary>
        IPrincipal AsPrincipal { get; }
    }

    
    public class AphidPrincipal : IAphidPrincipal, IPrincipal
    {
        public AphidPrincipal(IAphidIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException("identity");
            AphidIdentity = identity;
        }

        #region IPrincipal Members

        private IAphidIdentity AphidIdentity { get; set; }

        IIdentity IPrincipal.Identity
        {
            get { return AphidIdentity.AsIdentity; }
        }

        public bool IsInRole(string role)
        {
            return false;
        }

        #endregion

        #region IAphidPrincipal Members

        public long UserId
        {
            get { return long.Parse(AphidIdentity.UserId); }
        }

        public IAphidIdentity Identity
        {
            get { return AphidIdentity; }
        }

        public IPrincipal AsPrincipal
        {
            get { return this; }
        }


        #endregion
    }


    public interface IAphidIdentity
    {
        string UserId { get; }

        string Username { get; }

        int AccountTypeId { get; }

        string ProfilePicture { get; }

        string AuthenticationToken { get; }

        IIdentity AsIdentity { get; }
    }

    public class AphidIdentity : IAphidIdentity, IIdentity
    {
        private readonly LoginProfileDto _profile;

        public AphidIdentity(LoginProfileDto profile)
        {
            if (profile == null) throw new ArgumentNullException("profile");
            _profile = profile;
        }

        #region IAphidIdentity Members

        public string UserId
        {
            get { return _profile.UserId; }
        }

        public string AuthenticationToken
        {
            get { return _profile.Token; }
        }

        public string Username
        {
            get { return _profile.Username;  }
        }

        public int AccountTypeId
        {
            get { return _profile.AccountTypeId ?? 0; }
        }

        public string ProfilePicture
        {
            get { return _profile.ProfilePicture; }
        }

        public IIdentity AsIdentity
        {
            get { return this; }
        }

        #endregion

        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "AphidBytes"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return _profile.Username ?? string.Empty; }
        }

        #endregion
    }

}