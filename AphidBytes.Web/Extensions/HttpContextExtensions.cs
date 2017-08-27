using System;
using System.Web;

namespace AphidBytes.Web.Extensions
{
    public static class HttpContextExtensions
    {
        public static void DeleteCookie(this HttpContext context, string cookieName)
        {
            DeleteCookie(new HttpContextWrapper(context), cookieName);
        }

        public static void DeleteCookie(this HttpContextBase context, string cookieName)
        {
            var reqCookie = context.Request.Cookies[cookieName];
            if (reqCookie == null) return;

            reqCookie.Value = String.Empty;
            reqCookie.Expires = DateTime.UtcNow.AddDays(-1);
            context.Response.Cookies.Set(reqCookie);
        }

        /// <summary>
        /// Add a new HttpCookie to the Response object
        /// </summary>
        /// <param name="cookieName">The key for the cookie</param>
        /// <param name="expireFromUtcNow">This will be added to DateTime.UtcNow to determine cookie expiration</param>
        /// <param name="cookieValue">The value for the cookie</param>
        /// <param name="httpOnly">Default true. Only set to false if you need to modify from javascript</param>
        public static void AddCookie(this HttpContextBase context, string cookieName, TimeSpan expireFromUtcNow, string cookieValue, bool httpOnly = true)
        {
            AddCookie(context, cookieName, DateTime.UtcNow.Add(expireFromUtcNow), cookieValue, httpOnly);
        }

        /// <summary>
        /// Add a new HttpCookie to the Response object
        /// </summary>
        /// <param name="cookieName">The key for the cookie</param>
        /// <param name="expiration">The DateTime that the cookie expires and becomes invalid</param>
        /// <param name="cookieValue">The value for the cookie</param>
        /// <param name="httpOnly">Default true. Only set to false if you need to modify from javascript</param>
        public static void AddCookie(this HttpContextBase context, string cookieName, DateTime expiration, string cookieValue, bool httpOnly = true)
        {
            var authCookie = new HttpCookie(cookieName)
            {
                HttpOnly = httpOnly,
                Value = cookieValue,
                Expires = expiration
            };
            context.Response.Cookies.Add(authCookie);
        }

        /// <summary>
        /// Get the Cookie with key cookieName from Request object
        /// </summary>
        /// <param name="cookieName">The key for the cookie to be returned</param>
        /// <returns>HttpCookie object for the given key</returns>
        public static HttpCookie GetCookie(this HttpContextBase context, string cookieName)
        {
            return context.Request.Cookies[cookieName];
        }
    }
}