using AphidBytes.Web.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace AphidBytes.Web.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class ValidationsController : AphidController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IsUserNameAvailable(string UserName)
        {
            try
            {
                AphidBytes.BLL.AccountBLL bll = new BLL.AccountBLL();
                bool isValid = true;
                if (bll.IsUsernameAvailable(UserName))
                {
                    Response.AddHeader("X-UserName", UserName);
                    return Json("User name already exist.", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Response.AddHeader("X-UserName", "Not Available");
                    isValid = true;
                }

                return Json(isValid, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //TODO LOG ERROR
                Response.AddHeader("X-UserName", "false");
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult IsEmailAlreadyRegister(string EmailAddress)
        {
            try
            {
                AphidBytes.BLL.AccountBLL bll = new BLL.AccountBLL();
                bool isValid = true;
                if (bll.IsEmailAlreadyRegistered(EmailAddress))
                {
                    Response.AddHeader("X-Email", "Already Registered");
                    return Json("This email is already registered.", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    isValid = true;
                    Response.AddHeader("X-Email", EmailAddress);
                }
                return Json(isValid, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //TODO LOG ERROR
                Response.AddHeader("X-Email", "false");
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}