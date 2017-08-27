using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Accounts.Processor;
using AphidBytes.Web.Session_Helper;
using AphidBytes.Web.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Controllers
{
    public class ChatController : AphidController
    {
        //
        // GET: /Chat/

        public ActionResult ChatSupport()
        {
            IChat acc = DependencyResolver.Current.GetService<IChat>();
            Chat abc = new Chat();
            myAdminModel obj = new myAdminModel();
            var username = "";
            ViewBag.msg = null;
            ViewBag.emsg = null;
            int acc_type = 0;
            string uid = "";
            if (AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null)
            {
                username = AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString();
                uid = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                acc_type = acc.fetch_acType(uid);
                if (acc_type == 9)
                {
                    ViewBag.emsg = "There is no representative available at this time. Please try again later. Thank You.";
                }
                if (acc_type != 0)
                {
                    ViewBag.msg = "Welcome " + username + "... ";
                }
                else
                {
                    ViewBag.msg = "Welcome Guest... ";
                }
            }
            else
            {
                //username = "Guest";
                //AphidSession.Current.AuthenticatedUser?.Identity?.Username = username;
                //string uidd = System.Guid.NewGuid().ToString();
                //AphidSession.Current.AuthenticatedUser?.Identity?.UserId  = uidd;
                //ViewBag.msg = "Welcome Guest... ";
                //acc_type = acc.fetch_guestacType(uidd);
                //if (acc_type == 9)
                //{
                //    ViewBag.emsg = "There is no representative available at this time. Please try again later. Thank You.";
                //}

            }
            return View();
        }

    }
}
