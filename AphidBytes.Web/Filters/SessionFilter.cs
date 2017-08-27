using AphidBytes.Web.Session_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Filters
{
    public class SessionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrWhiteSpace(AphidSession.Current.AuthenticatedUser?.Identity?.Username)
             || string.IsNullOrWhiteSpace(AphidSession.Current.AuthenticatedUser?.Identity?.UserId))
            {
                var url = new UrlHelper(filterContext.RequestContext);
                var loginUrl = url.Content("http://localhost:55517/Accounts/LoginUser");
                AphidSession.Current.ClearIdentity();
                HttpContext.Current.Session.RemoveAll();
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                filterContext.HttpContext.Response.Redirect(loginUrl, false);
            }
            

            this.OnActionExecuting(filterContext);
        }
    }
}