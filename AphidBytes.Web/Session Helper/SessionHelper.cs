using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AphidBytes.Web.Session_Helper
{
    public class SessionHelper : ActionFilterAttribute,IActionFilter
    {
         void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (HttpContext.Current.AphidSession.Current.AuthenticatedUser?.Identity?.UserId  == null )
            //{
            //    var url = new UrlHelper(filterContext.RequestContext);
            //    HttpContext.Current.Session.RemoveAll();
            //    HttpContext.Current.Session.Clear();
            //    HttpContext.Current.Session.Abandon();
            //    //filterContext.HttpContext.Response.RedirectToRoute("Default");
            //    filterContext.HttpContext.Response.Redirect(System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)+"/Accounts/SessionExpire", false);
            //}
            //this.OnActionExecuting(filterContext);
            if (!AphidSession.Current.IsAuthenticated)
            {
            //if (filterContext.HttpContext.AphidSession.Current.AuthenticatedUser?.Identity?.UserId  == null)
            //{
                var values = new
                {
                    controller = "Accounts",
                    action = "SessionExpire",
                    returnUrl = filterContext.HttpContext.Request.Url.AbsoluteUri
                };
                var result = new RedirectToRouteResult("Default", new RouteValueDictionary(values));

                filterContext.Result = result;
            //}
            }
        }
    }
}