using AphidBytes.Core.PaymentServices;
using AphidBytes.Web.Web;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using System.Net;
using System.Web.Mvc;

namespace AphidBytes.Web.Controllers
{
    [RequireHttps]
    [RouteArea("v1")]
    [RoutePrefix("registration")]
    public class RegistrationApiController : AphidController
    {
        [POST("byter")]
        public ActionResult RegisterByter(string stripeToken)
        {
            if (string.IsNullOrWhiteSpace(stripeToken))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Missing token");
            }

            var successful = StripeClient.CreateStripeCharge(StripePackages.Byter, stripeToken);

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        [POST("basic")]
        public ActionResult RegisterBasic(string stripeToken)
        {
            if (string.IsNullOrWhiteSpace(stripeToken))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Missing token");
            }

            var successful = StripeClient.CreateStripeCharge(StripePackages.Basic, stripeToken);

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        [POST("aphidlabs")]
        public ActionResult RegisterAphidLabs(string stripeToken)
        {
            if (string.IsNullOrWhiteSpace(stripeToken))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Missing token");
            }

            var successful = StripeClient.CreateStripeCharge(StripePackages.AphidLabs, stripeToken);

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        [POST("premium")]
        public ActionResult RegisterPremium(string stripeToken)
        {
            if (string.IsNullOrWhiteSpace(stripeToken))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Missing token");
            }

            var successful = StripeClient.CreateStripeCharge(StripePackages.Premium, stripeToken);

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

    }
}
