using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Web.App_Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AphidBytes.Web.Models;
using System.Text.RegularExpressions;
using AphidBytes.Web.Session_Helper;
using System.Net.Mail;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using AphidBytes.Web.Web;

namespace AphidBytes.Web.Controllers
{

    public class AccountsController : AphidController
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AccountsController));
        #region Login & Logout

        public ActionResult DataPlan()
        {
            return View();
        }
        public ActionResult Guides()
        {
            return View();

        }
        public ActionResult LoginUser()
        {
            if (AphidSession.Current.IsAuthenticated) 
            {
                switch (AphidSession.Current.AuthenticatedUser.Identity.AccountTypeId)
                {
                    case /*1*/2:
                        return RedirectToAction("BasicAccountInfo", "Basic");
                    case 9:
                        return RedirectToAction("AphidTiseAccountInfo", "AphidTise");
                    case /*3*/1:
                        return RedirectToAction("ByterAccountInfo", "Byter");
                    case /*4*/3:
                        return RedirectToAction("PremiumAccountInfo", "Premium");
                    case 5:
                        return RedirectToAction("EditNewRelease", "Home");
                    case /*6*/4:
                        return RedirectToAction("AphidLabsAccountInfo", "AphidLabs");
                    case 7:
                        return RedirectToAction("EditNewRelease", "Home");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View(new LoginUser());
            }
        }
        [HttpPost]
        public ActionResult LoginUser(LoginUser Login)
        {
            try
            {
                IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                Login.Password = CryptorEngine.Encrypt(Login.Password, true);
                var loginProfile = accountsService.LoginWithUser(Login);
                if (loginProfile == null)
                {
                    var model = new LoginUser { UserName = Login.UserName };
                    model.Validation.AddError("Invalid user name or password combination");
                    return View(model); 
                }

                var guidFromUserId = new Guid(loginProfile.UserId);
                var fb_li = accountsService.SocialStatus(guidFromUserId);
                for (int i = 0; i < fb_li.Count; i++)
                {
                    loginProfile.SocialStatus["fb_status"] = (fb_li[i].category == "Facebook");
                    loginProfile.SocialStatus["link_status"] = (fb_li[i].category == "LinkedLin");
                    loginProfile.SocialStatus["tw_status"] = (fb_li[i].category == "Twitter");
                    loginProfile.SocialStatus["flick_status"] = (fb_li[i].category == "Flicker");
                    loginProfile.SocialStatus["scribd_status"] = (fb_li[i].category == "Scribd");
                    loginProfile.SocialStatus["youtube_status"] = (fb_li[i].category == "YouTube");
                    loginProfile.SocialStatus["daily_status"] = (fb_li[i].category == "DailyMotion");

                    if (loginProfile.SocialStatus["fb_status"] && DateTime.Now >= fb_li[i].Expires)
                    {
                        FaceBookModel fb = new FaceBookModel();
                        DateTime value = fb.Get_Extended_Token(fb_li[i].RefereshToken);
                        accountsService.UpdateExpires(guidFromUserId, fb_li[i].category, value, fb_li[i].RefereshToken);
                    }

                    if (loginProfile.SocialStatus["daily_status"] && DateTime.Now.Date >= fb_li[i].Expires.Date)
                    {
                        Dailymotion daily = new Dailymotion();
                        string value = daily.GetExte_time(fb_li[i].RefereshToken);
                        string[] words = value.Split('_');
                        DateTime val = DateTime.Parse(words[1]);
                        accountsService.UpdateExpires(guidFromUserId, fb_li[i].category, val, words[2], words[0]);
                    }

                    if (loginProfile.SocialStatus["link_status"] && DateTime.Now >= fb_li[i].Expires)
                    {
                        LinkedLinModel link = new LinkedLinModel();
                        DateTime value = link.GetNewExpires(fb_li[i].RefereshToken);
                        accountsService.UpdateExpires(guidFromUserId, fb_li[i].category, value, fb_li[i].RefereshToken);
                    }
                }

                AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));

                switch (loginProfile.AccountTypeId)
                {
                    case 2/*1*/:
                        return RedirectToAction("BasicAccountInfo", "Basic");
                    case 9/*2*/:
                        return RedirectToAction("AphidTiseAccountInfo", "AphidTise");
                    case /*3*/1:
                        return RedirectToAction("ByterAccountInfo", "Byter");
                    case /*4*/3:
                        return RedirectToAction("PremiumAccountInfo", "Premium");
                    case 5:
                        return RedirectToAction("EditNewRelease", "Home");
                    case /*6*/4:
                        return RedirectToAction("AphidLabsAccountInfo", "AphidLabs");
                    case 7:
                        return RedirectToAction("EditNewRelease", "Home");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + Environment.NewLine + ex.StackTrace + ex.InnerException);
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult FacebookLoginBasic(BasicAccountViewModel model)
        {
            IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
            var loginProfile = accountsService.LoginWithSocialSite(model.BasicUserID.ToString()); 
            AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));

            return RedirectToAction("BasicAccountInfo", "Basic", model);
        }


        public ActionResult GooglePlusLoginBasic(BasicAccountViewModel model)
        {
            IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
            var loginProfile = accountsService.LoginWithSocialSite(model.BasicUserID.ToString());
            AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));

            return RedirectToAction("BasicAccountInfo", "Basic", model);
        }

        public ActionResult NotAuthorised()
        {
            return View();
        }

        /// <summary>
        /// Error Message
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginFailed()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            AphidSession.Current.ClearIdentity();
            return RedirectToAction("LoginUser", "Accounts");
        }
        #endregion

        public ActionResult SignUpWithFacebook()
        {
            return View();
        }
        //public ActionResult SignUpWithGoogle()
        //{
        //    return View();
        //}
        //code new fb and google
        #region Register with facebook
        // Register with facebok pravin

    


        //public ActionResult SignUpWithFacebook()
        //{
        //    return View();
        //}
        

        #endregion

        //public ActionResult LoginWtihSocialSite(int id)
        //{

        //    var logintype = new LoginUser();
        //    logintype.AccountTypeID = id;
        //    return View(logintype);
        //}

        public ActionResult RegisterPosting()
        {
            return View();
        }
        public ActionResult Skip()
        {
            return RedirectToAction("Index", "Home");
        }
        //[HttpGet]
        //public ActionResult Continuebtn(string id)
        //{
        //    if (id == "1")
        //    {


        //    }
        //    else if (id == "2")
        //    {

        //    }
        //    else if (id == "3")
        //    {

        //    }
        //    return View();
        //}

        #region Register with Google plus
        // Register with googleplus 

    

        #endregion
        //code new fb and google
        public ActionResult UserConfirmAccount(string token)
        {
            try
            {
                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                string sta = userRegister.activateUser(token);
                if (sta == "User Already Activated")
                {
                    ViewBag.Message = "Already Activated";
                }
                if (sta == "User Activated")
                {
                    return RedirectToAction("LoginUser", "Accounts");
                    // ViewBag.Message = "Your account has been Successfull Activated";
                }
                if (sta == "Invalid User")
                {
                    ViewBag.Message = "Invalid User";
                }
                return View();
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult ByterSocialSignUp()
        {
            return View();
        }


        public ActionResult LoginorSignUp()
        {
            return View();
        }

        public ActionResult SelectsSignup()
        {

            return View();
        }

        static string tokenId;
        public ActionResult UserConfirmationForgrtPassword(string token)
        {
            try
            {
                tokenId = token;
                IAccounts acc = DependencyResolver.Current.GetService<IAccounts>();
                bool re = acc.UserConfirmationForgrtPassword(token);
                return View();
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult LoginWtihSocialSite()
        {
            return View();

        }
        public ActionResult UserConfirmationForgrtPasswordChange(string password)
        {
            try
            {
                string token = tokenId;
                string pass = password;
                string encryptPwd = CryptorEngine.Encrypt(pass, true);
                IAccounts acc = DependencyResolver.Current.GetService<IAccounts>();
                bool result = acc.UpdatePassword(token, encryptPwd);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

            return View();
        }
        public ActionResult Terms()
        {
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }
        public ActionResult AboutUsPage()
        {
            return View();
        }
        public ActionResult CompanyInfo()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Legal()
        {
            return View();

        }
        public ActionResult SystemRequirment()
        {
            return View();
        }
        public ActionResult CorparateTeam()
        {
            return View();
        }
        public ActionResult Support()
        {
            return View();

        }
        public ActionResult ContactUs()
        {
            return View();

        }
        public ActionResult VideoTutorials(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                s = "videos"; 
            }

            var model = new VideoTutorialsDisplay();
            model.IsVideoTutorials = s.Equals("videos", StringComparison.OrdinalIgnoreCase); 
            model.IsAccounts = s.Equals("accounts", StringComparison.OrdinalIgnoreCase); 
            model.IsAdvertising = s.Equals("advertising", StringComparison.OrdinalIgnoreCase); 
            model.IsClones = s.Equals("clones", StringComparison.OrdinalIgnoreCase);
            if (!model.IsVideoTutorials && !model.IsAdvertising && !model.IsAccounts && !model.IsClones)
            {
                model.IsVideoTutorials = true; 
            }

            return View(model);
        }
        public ActionResult ChatSupport()
        {



            return View();
        }
        public ActionResult IhaveAccountConfrm()
        {



            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword textarea)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    IAccounts userLogin = DependencyResolver.Current.GetService<IAccounts>();
                    string encript = CryptorEngine.Encrypt(textarea.ConfirmPassword, true);
                    var userid = AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString();
                    bool re = userLogin.ChangePassword(encript, userid);
                }
                return View();
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult ForgetPassword()
        {
            ForgetPassword model = new ForgetPassword();
            return View(model);
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPassword model)
        {
            try
            {
                IAccounts acc = DependencyResolver.Current.GetService<IAccounts>();
                Guid guid = Guid.NewGuid();
                model.Token = guid;
                var result = acc.ForgetPasword(model);
                if (result != null&&result.UserId!=null&&!string.IsNullOrEmpty(result.AccountType)&&!string.IsNullOrEmpty(result.UserName))
                {
                    model.VerifiedEmail = result.EmailAddress;

                    bool re = acc.InsertForgetPasswordDetail(model);
                    if (re == true)
                    {
                        Email email = new Email();
                        email.sendMaill(model.UserId, model.EmailAddress, "ForgetPassword",model.Token, model.UserName, "ForgetPassword");
                        //  email.sendMail(model.VerifiedEmail, "ForgetPassword", model.Token, model.UserName, "Password Reset");
                    }
                }
                return RedirectToAction("ThanksForSubmitting");
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult ThanksForSubmitting()
        {
            return View();
        }


        public ActionResult SecurityQuestion1(ForgetPassword model)
        {
            try
            {
                List<ForgetPassword> list = new List<ForgetPassword>();
                if (model.UserName != null)
                {
                    IAccounts acc = DependencyResolver.Current.GetService<IAccounts>();
                    list = acc.Secure_Getdata(model.UserName);

                }
                else if (model.EmailAddress != null)
                {
                    IAccounts acc = DependencyResolver.Current.GetService<IAccounts>();
                    list = acc.Secure_Getdata(model.EmailAddress);
                }
                if (list.Count != 0)
                {
                    model.UserName = list[0].UserName;
                    for (int i = 0; i < list.Count; i++)
                    {
                        model.SecurityAnswer1 = list[0].SecurityAnswer1;
                        model.SecurityAnswer2 = list[0].SecurityAnswer2;
                        model.SecurityQuestion1 = list[0].SecurityQuestion1;
                        model.SecurityQuestion2 = list[0].SecurityQuestion2;
                    }
                    return RedirectToAction("SecurityQuestion", model);
                }
                else
                    return RedirectToAction("ForgetPassword");

            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        #region security question password change
        public ActionResult SecurityQuestion()
        {
            var model = new ForgetPassword();
            return View(model);
        }
        [HttpPost]
        public ActionResult SecurityQuestion(ForgetPassword forgetPasswordModel)
        {
            var model = new ForgetPassword(); 
            try
            {
                var accountsService = DependencyResolver.Current.GetService<IAccounts>();
                var accountModel = accountsService.Secure_Getdata(forgetPasswordModel.UserName);
                if (accountModel == null || !accountModel.Any())
                {
                    model.Validation.AddError("Account does not exist, or was not found");
                    return View("SecurityQuestion", model);
                }

                var userModel = accountModel.FirstOrDefault();
                model.UserName = userModel.UserName;
                model.SecurityQuestion1 = userModel.SecurityQuestion1;
                model.SecurityQuestion2 = userModel.SecurityQuestion2;
                return View("SecurityQuestionsAnswer", model);
            }
            catch (Exception e)
            {
                model.Validation.AddError("Oops, something happened, try again later");
                return View("SecurityQuestion", model);
            }
        }
        [HttpPost]
        public ActionResult SecurityQuestionsAnswer(ForgetPassword forgetPasswordModel)
        {
            try
            {
                var accountsService = DependencyResolver.Current.GetService<IAccounts>();
                var accountModel = accountsService.Secure_Getdata(forgetPasswordModel.UserName);
                if (accountModel == null || !accountModel.Any())
                {
                    forgetPasswordModel.Validation.AddError("Account does not exist, or was not found");
                    return View("SecurityQuestionsAnswer", forgetPasswordModel);
                }

                var userModel = accountModel.FirstOrDefault();
                if (!userModel.SecurityAnswer1.Equals(forgetPasswordModel.SecurityAnswer1, StringComparison.OrdinalIgnoreCase) 
                 || !userModel.SecurityAnswer2.Equals(forgetPasswordModel.SecurityAnswer2, StringComparison.OrdinalIgnoreCase))
                {
                    forgetPasswordModel.Validation.AddError("Security questions do not match, try again");
                    return View("SecurityQuestionsAnswer", forgetPasswordModel);
                }

                forgetPasswordModel.UserName = userModel.UserName; 

                return View("SecurityQuestionsPassword", forgetPasswordModel);
            }
            catch  (Exception e)
            {
                var model = new ForgetPassword();
                model.Validation.AddError("Oops, something happened, try again later");
                return View("ForgetPassword", model);
            }
        }
        [HttpPost]
        public ActionResult SecurityQuestionsPassword(ForgetPassword forgetPasswordModel)
        {
            try
            {
                var accountsService = DependencyResolver.Current.GetService<IAccounts>();
                var accountModel = accountsService.Secure_Getdata(forgetPasswordModel.UserName);
                if (accountModel == null || !accountModel.Any())
                {
                    forgetPasswordModel.Validation.AddError("Account does not exist, or was not found");
                    return View("SecurityQuestionsPassword", forgetPasswordModel);
                }

                var userModel = accountModel.FirstOrDefault();
                if (!forgetPasswordModel.Password.Equals(forgetPasswordModel.ConfirmPassword, StringComparison.Ordinal))
                {
                    forgetPasswordModel.Validation.AddError("Passwords do not match, try again");
                    return View("SecurityQuestionsPassword", forgetPasswordModel);
                }

                var commonService = DependencyResolver.Current.GetService<ICommon>();
                var encryptPwd = CryptorEngine.Encrypt(forgetPasswordModel.Password, true);
                var success = commonService.UpdatePassword(userModel.UserId, encryptPwd);
                if (!success)
                {
                    forgetPasswordModel.Validation.AddError("Unable to change the current password");
                    return View("SecurityQuestionsPassword", forgetPasswordModel);
                }

                return RedirectToAction("LoginUser", "Accounts");
            }
            catch (Exception e)
            {
                var model = new ForgetPassword();
                model.Validation.AddError("Oops, something happened, try again later");
                return View("ForgetPassword", model);
            }
        }
        #endregion

        public string SecurityQuestions(string Insertanswer1, string Insertanswer2, string Username)
        {
            try
            {
                List<ForgetPassword> list = new List<ForgetPassword>();
                IAccounts acc = DependencyResolver.Current.GetService<IAccounts>();
                list = acc.Secure_Getdata(Username);
                if ((Insertanswer1 == list[0].SecurityAnswer1) && (Insertanswer2 == list[0].SecurityAnswer2))
                {
                    ForgetPassword model = new ForgetPassword();
                    Guid guid = Guid.NewGuid();
                    model.Token = guid;
                    model.UserName = Username;
                    model.EmailAddress = list[0].EmailAddress;
                    var result = acc.ForgetPasword(model);
                    if (result != null && result.UserId != null && !string.IsNullOrEmpty(result.AccountType) && !string.IsNullOrEmpty(result.UserName))
                    {
                        model.VerifiedEmail = result.EmailAddress;

                        bool re = acc.InsertForgetPasswordDetail(model);
                        if (re == true)
                        {
                            Email email = new Email();
                            //  email.sendMail(model.VerifiedEmail, "ForgetPassword", model.Token, Username,"Password Reset");
                        }
                    }
                    return "ThanksForSubmitting";
                }
                else
                {
                    if (Insertanswer1 == list[0].SecurityAnswer1)
                    {
                        if (Insertanswer2 != list[0].SecurityAnswer2)
                        {
                            return "Failure2";
                        }
                    }
                    else if (Insertanswer2 == list[0].SecurityAnswer2)
                    {
                        if (Insertanswer1 != list[0].SecurityAnswer1)
                        {
                            return "Failure1";
                        }
                    }

                }
                return "Failure";
            }

            catch (Exception ex)
            {
                return "LoginUser";
            }
        }
        public JsonResult Chat_login()
        {

            IAccounts acc = DependencyResolver.Current.GetService<IAccounts>();
            myAdminModel abb = new myAdminModel();
            if ((!string.IsNullOrWhiteSpace(AphidSession.Current.AuthenticatedUser?.Identity?.Username)) 
             && (!string.IsNullOrWhiteSpace(AphidSession.Current.AuthenticatedUser?.Identity?.UserId)))
            {
                //      abb=acc.fet_data_chat(AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString(),AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            }

            return Json(abb);
        }

        

        public ActionResult SessionExpire()
        {
            return View();
        }
        public ActionResult Culture()
        {
            return View();
        }
        public ActionResult Wallpapers()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Pictures()
        {
            return View();
        }

        public ActionResult submityourimage()
        {
            return View();
        }
       
        public ActionResult SendMail()
        {
            string result = Boolean.FalseString;

            var Name = ValueProvider.GetValue("Name").AttemptedValue;
            var caption = ValueProvider.GetValue("Caption").AttemptedValue;
            var channel = ValueProvider.GetValue("Channel").AttemptedValue;
            var Image = ValueProvider.GetValue("Image").AttemptedValue;
            Image = Image.Replace("data:image/jpeg;base64,", "");
            byte[] data = System.Convert.FromBase64String(Image);
            Stream ms = new MemoryStream(data);
            
           
            if (ModelState.IsValid)
            {
                string to = "rishajava@gmail.com";
                using (MailMessage mail = new MailMessage())
                {

                    mail.Subject = caption;
                    mail.Body = Name + " " + channel;
                    mail.From = new MailAddress("ris7@gmail.com");
                    mail.To.Add(to);
                    if (Image != null)
                    {
                        string fileName = "hello.jpg";
                        mail.Attachments.Add(new Attachment(ms, fileName));
                    }
                    mail.IsBodyHtml = false;
                    try
                    {
                        SmtpClient smtp = new SmtpClient("Smtp.gmail.com");
                        smtp.EnableSsl = true;
                        NetworkCredential networkCredential = new NetworkCredential("ris7@gmail.com", "1111111111");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCredential;
                        smtp.Port = 587;
                        smtp.Send(mail);
                        result = Boolean.TrueString;
                    }
                    catch (Exception ex)
                    {
                        logger.Error("SendMail-" + ex.Message + Environment.NewLine + ex.StackTrace);
                        result = Boolean.FalseString;
                    }
                }
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        private Stream byteArrayToImage(byte[] byteArrayIn)
        {
            Stream stream;
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                stream = ms;
            }
            
            return stream;
        }

    }
}
