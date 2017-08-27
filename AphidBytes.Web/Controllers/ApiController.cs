using AphidBytes.Core.PaymentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /Api/

        public ActionResult ValidatePromotion(string promoCode)
        {
            try
            {
                var promo = StripeClient.ValidatePromotion(promoCode);

                return Json(promo != null ? "valid" : "invalid");
            }
            catch (Exception)
            {
                return Json("invalid");
            }
        }

    }
}
