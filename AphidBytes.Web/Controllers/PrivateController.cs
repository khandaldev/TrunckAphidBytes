using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Controllers
{
    public class PrivateController: Controller
    {

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {               
                
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}