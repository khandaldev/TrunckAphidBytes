using AphidBytes.Web.Session_Helper;
using System.Web.Mvc;
using System.Web.Routing;

namespace AphidBytes.Web.Web
{
    public abstract class AphidController : Controller
    {
        protected AphidController()
        {
            AphidByteSession = AphidSession.Current;
        }

        public new AphidSession AphidByteSession { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //check Session here
           /* if (Session["Pass"] == null || Session["Pass"].ToString() != "wearegeniuses")
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                    {{"controller", "Private"}, {"action", "Index"}});
                //return RedirectToAction("Index", "Private");
            }*/
        }
    }
}
