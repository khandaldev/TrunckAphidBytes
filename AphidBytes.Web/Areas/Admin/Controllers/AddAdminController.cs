using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Web.App_Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Areas.Admin.Controllers
{
    public class AddAdminController : Controller
    {
        //
        // GET: /Admin/AddAdmin/

        public ActionResult AddNewAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewAdmin(RegisterAdmin obvRegister)
        {
            try
            {
                IAccounts admin = DependencyResolver.Current.GetService<IAccounts>();
                string encryptPwd = CryptorEngine.Encrypt(obvRegister.ConfirmPassword, true);
                obvRegister.ConfirmPassword = encryptPwd;
                bool re = admin.RegisterAdmin(obvRegister);
                if (re == true)
                {
                    return RedirectToAction("LoginUser", "Accounts", new  {Area="" });
                }
                else { 
                }
            }
            catch (Exception)
            {
                
                throw;
            }
           
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                string savedImageName = "/TempBasicImages/" + file.FileName;
                file.SaveAs(Server.MapPath(savedImageName));
            }
            return View();
        }
    }
}
