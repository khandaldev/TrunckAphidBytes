using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.ContentServers;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Accounts.Contracts.Model.BaseTypes;
using AphidBytes.BLL;
using AphidBytes.Core.Images;
using AphidBytes.Core.PaymentServices;
using AphidBytes.Web.App_Code;
using AphidBytes.Web.Extensions;
using AphidBytes.Web.Models;
using AphidBytes.Web.Session_Helper;
using AphidBytes.Web.Utility;
using AphidBytes.Web.Web;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Controllers
{
    
    public class RegisterController : AphidController
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(RegisterController));
        public ActionResult Index()
        {
            return View();
        }

        private void PerformLogin(Guid userId)
        {
            logger.Error("PerformLogin");
            var accountsService = DependencyResolver.Current.GetService<IAccounts>();
            var loginProfile = accountsService.LoginWithSocialSite(userId.ToString());

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
        }

        #region Basic
        [HttpGet]
        public ActionResult BasicRegister(BasicAccountViewModel basicModel)
        {
            return View(basicModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BasicRegister(BasicAccountViewModel basicModel, HttpPostedFileBase file, string stripeToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    basicModel.Validation.FillFromModelState(ModelState);
                    return View(basicModel);
                }

                if (string.IsNullOrWhiteSpace(basicModel.UserName))
                {
                    basicModel.Validation.AddError("Invalid username.");
                    return View(basicModel);
                }

                if (!string.IsNullOrWhiteSpace(basicModel.PromoCode))
                {
                    var coupon = StripeClient.ValidatePromotion(basicModel.PromoCode); 
                    if (coupon == null || (coupon.RedeemBy.HasValue && coupon.RedeemBy < DateTime.UtcNow))
                    {
                        basicModel.Validation.AddError(ErrorConstants.InvalidPromotion);
                        return View(basicModel);
                    }
                }

                ImageUploader.UploadProfilePictureAndSetLocation(basicModel);
                string encryptPwd = CryptorEngine.Encrypt(basicModel.Password, true);
                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                basicModel.UserName = basicModel.UserName.ToLower();
                basicModel.Password = encryptPwd;
                basicModel.AccountIdBasic = Guid.NewGuid();
                basicModel.StripeToken = stripeToken;
                var successful = userRegister.RegisterBasicAccount(basicModel);
                if (!successful)
                {
                    basicModel.Password = string.Empty;
                    basicModel.ConfirmPassword = string.Empty;
                    basicModel.Validation.AddError(ErrorConstants.GenericError);
                    return View(basicModel);
                } 

                Guid token = Guid.NewGuid();
                bool activationresult = userRegister.activationInsert(basicModel.AccountIdBasic, token, basicModel.UserName);

                PerformLogin(basicModel.AccountIdBasic);
                Email mail = new Email();//send mail                
                mail.sendMaill(basicModel.AccountIdBasic, basicModel.EmailAddress, "Basic", token,basicModel.UserName, "VerifyEmail");
                return RedirectToAction("BasicAccountInfo", "Basic");
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message + Environment.NewLine + ex.StackTrace);
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        #endregion

        #region Byter Area
        [HttpGet]
        public ActionResult ByterRegister(ByterAccountViewModel byterModel)
        {
            return View(byterModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ByterRegister(ByterAccountViewModel byterModel, HttpPostedFileBase file, string stripeToken)
        {
            logger.Error("ByterRegister");
            try
            {
                if (!ModelState.IsValid)
                {
                    byterModel.Validation.FillFromModelState(ModelState);
                    return View(byterModel);
                }
                if (string.IsNullOrWhiteSpace(byterModel.UserName))
                {
                    byterModel.Validation.AddError("Invalid username.");
                    return View(byterModel);
                }

                var newUserId = Guid.NewGuid();
                ImageUploader.UploadProfilePictureAndSetLocation(byterModel);
                string encryptPwd = CryptorEngine.Encrypt(byterModel.Password, true);
                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                byterModel.Password = encryptPwd;
                byterModel.UserName = byterModel.UserName.ToLower();
                byterModel.AccountID = newUserId;
                byterModel.StripeToken = stripeToken;
                var successful = userRegister.RegisterByterAccount(byterModel);
                if (!successful)
                {
                    byterModel.Password = string.Empty;
                    byterModel.ConfirmPassword = string.Empty;
                    byterModel.Validation.AddError(ErrorConstants.GenericError);
                    return View(byterModel);
                }

                Guid token = Guid.NewGuid();
                bool activationresult = userRegister.activationInsert(newUserId, token, byterModel.UserName);

                PerformLogin(byterModel.AccountID);
                //send mail
                /*IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                var loginProfile = accountsService.LoginWithSocialSite(byterModel.AccountID.ToString());
                AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));*/
                //return ("ByterAccountInfo+" + "Byter");
                Email mail = new Email();//send mail                
                mail.sendMaill(byterModel.AccountID, byterModel.EmailAddress, "Byter", token, byterModel.UserName, "VerifyEmail");
                return RedirectToAction("ByterAccountInfo", "Byter");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
                //return RedirectToAction("LoginUser", "Accounts");
            }
        }
        #endregion
        
        #region AphidTise Area
        [HttpGet]
        public ActionResult AphidTiseRegister(AphidTiseAccountViewModel aphidtiseModel)
        {
            return View(aphidtiseModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AphidTiseRegister(AphidTiseAccountViewModel aphidtiseModel, HttpPostedFileBase file, string stripeToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    aphidtiseModel.Validation.FillFromModelState(ModelState);
                    return View(aphidtiseModel);
                }

                ImageUploader.UploadProfilePictureAndSetLocation(aphidtiseModel);
                string encryptPwd = CryptorEngine.Encrypt(aphidtiseModel.Password, true);
                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                aphidtiseModel.Password = encryptPwd;
                aphidtiseModel.UserName = aphidtiseModel.UserName.ToLower();
                aphidtiseModel.AccountIdAphid = Guid.NewGuid();
                aphidtiseModel.StripeToken = stripeToken;
                var successful = userRegister.RegisterAphidTiseAccount(aphidtiseModel);
                if (!successful)
                {
                    aphidtiseModel.Password = string.Empty;
                    aphidtiseModel.ConfirmPassword = string.Empty;
                    aphidtiseModel.Validation.AddError(ErrorConstants.GenericError);
                    return View(aphidtiseModel);
                }

                Guid token = Guid.NewGuid();
                bool activationresult = userRegister.activationInsert(aphidtiseModel.AccountIdAphid, token, aphidtiseModel.UserName);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        #endregion

        #region Premium Area
        public ActionResult Premium()
        {
            var premiumCodesModel = new PremiumCodesModel();
            return View(premiumCodesModel);
        }
        [HttpPost]
        public ActionResult Premium(PremiumCodesModel premiumCodesModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(premiumCodesModel.PremiumEntryCode))
                {
                    IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                    var successful = accountsService.VerifyPremiumAccountCode(premiumCodesModel.PremiumEntryCode.Trim());
                    if (successful)
                    {
                        return View("PremiumRegister", new PremiumAccountViewModel());
                    }
                }

                premiumCodesModel.Validation.AddError("Invalid. Please re-enter the code provided to you by AphidByte via email to proceed");
                return View(premiumCodesModel);

            }
            catch (Exception ex)
            {
                var freshModel = new PremiumCodesModel();
                freshModel.Validation.AddError("Invalid. Please re-enter the code provided to you by AphidByte via email to proceed");
                return View(freshModel);
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PremiumRegister(PremiumAccountViewModel premiumModel, HttpPostedFileBase file, string stripeToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    premiumModel.Validation.FillFromModelState(ModelState);
                    return View(premiumModel);
                }

                if (string.IsNullOrWhiteSpace(premiumModel.UserName))
                {
                    premiumModel.Validation.AddError("Invalid username.");
                    return View(premiumModel);
                }

                if (!string.IsNullOrWhiteSpace(premiumModel.PromoCode))
                {
                    var coupon = StripeClient.ValidatePromotion(premiumModel.PromoCode);
                    if (coupon == null || (coupon.RedeemBy.HasValue && coupon.RedeemBy < DateTime.UtcNow))
                    {
                        premiumModel.Validation.AddError(ErrorConstants.InvalidPromotion);
                        return View(premiumModel);
                    }
                }
                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                ImageUploader.UploadProfilePictureAndSetLocation(premiumModel);
                string encryptPwd = CryptorEngine.Encrypt(premiumModel.Password, true);
                premiumModel.Password = encryptPwd;
                premiumModel.UserName = premiumModel.UserName.ToLower();
                premiumModel.AccountIDPre = Guid.NewGuid();
                premiumModel.StripeToken = stripeToken;
                var successful = userRegister.RegisterPremiumAccount(premiumModel);
                if (!successful)
                {
                    premiumModel.Password = string.Empty;
                    premiumModel.ConfirmPassword = string.Empty;
                    premiumModel.Validation.AddError(ErrorConstants.GenericError);
                    return View(premiumModel);
                }

                Guid token = Guid.NewGuid();
                bool activationresult = userRegister.activationInsert(premiumModel.AccountIDPre, token, premiumModel.UserName);

                PerformLogin(premiumModel.AccountIDPre);
                Email mail = new Email();//send mail                
                mail.sendMaill(premiumModel.AccountIDPre, premiumModel.EmailAddress, "Premium", token, premiumModel.UserName, "VerifyEmail");
                return RedirectToAction("PremiumAccountInfo", "Premium");
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        #endregion

        #region AphidLab Area
        [HttpGet]
        public ActionResult AphidLabRegister(AphidLabAccountModel aphidlabModel)
        {
            return View(aphidlabModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AphidLabRegister(AphidLabAccountModel aphidlabModel, HttpPostedFileBase file, string stripeToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    aphidlabModel.Validation.FillFromModelState(ModelState);
                    return View(aphidlabModel);
                }


                if (string.IsNullOrWhiteSpace(aphidlabModel.UserName))
                {
                    aphidlabModel.Validation.AddError("Invalid username.");
                    return View(aphidlabModel);
                }


                if (!string.IsNullOrWhiteSpace(aphidlabModel.PromoCode))
                {
                    var coupon = StripeClient.ValidatePromotion(aphidlabModel.PromoCode);
                    if (coupon == null || (coupon.RedeemBy.HasValue && coupon.RedeemBy < DateTime.UtcNow))
                    {
                        aphidlabModel.Validation.AddError(ErrorConstants.InvalidPromotion);
                        return View(aphidlabModel);
                    }
                }

                ImageUploader.UploadProfilePictureAndSetLocation(aphidlabModel);
                string encryptPwd = CryptorEngine.Encrypt(aphidlabModel.Password, true);
                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                Guid accontid = Guid.NewGuid();
                aphidlabModel.Password = encryptPwd;
                aphidlabModel.UserName = aphidlabModel.UserName.ToLower();
                aphidlabModel.StripeToken = stripeToken;
                aphidlabModel.AphidlabUserID = accontid;
                var successful = userRegister.RegisterAphidlabAccount(aphidlabModel);
                if (!successful)
                {
                    aphidlabModel.Password = string.Empty;
                    aphidlabModel.ConfirmPassword = string.Empty;
                    aphidlabModel.Validation.AddError(ErrorConstants.GenericError);
                    return View(aphidlabModel);
                }
                var token = Guid.NewGuid();
                bool activationresult = userRegister.activationInsert(accontid, token, aphidlabModel.UserName);

                PerformLogin(accontid);
                Email mail = new Email();//send mail                
                mail.sendMaill(aphidlabModel.AphidlabUserID.Value, aphidlabModel.EmailAddress, "AphidLab", token, aphidlabModel.UserName, "VerifyEmail");
                return RedirectToAction("AphidLabsAccountInfo", "AphidLabs");
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        #endregion

        public void FillDataFromFacebookIAccountInfo(IAccountInfo baseModel, FacebookAccountViewModel fbmodel)
        {
            baseModel.UserName = fbmodel.UserName;
            baseModel.Password = fbmodel.Password;
            baseModel.ConfirmPassword = fbmodel.ConfirmPassword;
            baseModel.FirstName = fbmodel.FirstName;
            baseModel.LastName = fbmodel.LastName;
            baseModel.EmailAddress = fbmodel.EmailAddress;
            baseModel.DOB = fbmodel.DOB;
            baseModel.Phone = fbmodel.Phone;
            baseModel.AccountTypeID = int.Parse(fbmodel.AccountType); 




        }

        public void FillDataFromGoogleIAccountInfo(IAccountInfo baseModel, GooglePlusAccountViewModel gpmodel)
        {
            baseModel.UserName = gpmodel.UserName;
            baseModel.Password = gpmodel.Password;
            baseModel.ConfirmPassword = gpmodel.ConfirmPassword;
            baseModel.FirstName = gpmodel.FirstName;
            baseModel.LastName = gpmodel.LastName;
            baseModel.EmailAddress = gpmodel.EmailAddress;
            baseModel.DOB = gpmodel.DOB;
            baseModel.Phone = gpmodel.Phone;
            baseModel.AccountTypeID = int.Parse(gpmodel.AccountType);




        }

        public void FillDataFromFacebookIAccountInfo(ByterAccountViewModel baseModel, FacebookAccountViewModel fbmodel)
        {
            baseModel.UserName = fbmodel.UserName;
            baseModel.Password = fbmodel.Password;
            baseModel.ConfirmPassword = fbmodel.ConfirmPassword;
            baseModel.FirstName = fbmodel.FirstName;
            baseModel.LastName = fbmodel.LastName;
            baseModel.EmailAddress = fbmodel.EmailAddress;
            baseModel.DOB = fbmodel.DOB;
            baseModel.Phone = fbmodel.Phone;
            baseModel.AccountTypeID = int.Parse(fbmodel.AccountType);
            baseModel.AddressLine1 = fbmodel.AddressLine1;
            baseModel.AddressLine2 = fbmodel.AddressLine2;
            baseModel.City = fbmodel.City;
            baseModel.PostalCode = fbmodel.PostalCode;
            baseModel.Region = fbmodel.Region;
            baseModel.SecurityQuestion1 = string.Empty;
            baseModel.SecurityQuestion2 = string.Empty;
            baseModel.Answer1 = string.Empty;
            baseModel.Answer2 = string.Empty;
            baseModel.SocialNetworkSource = "Facebook";
            baseModel.StripeToken = fbmodel.StripeToken;
            baseModel.ProfilePicture = fbmodel.ProfilePicture;
        }

        public void FillDataFromGoogleIAccountInfo(ByterAccountViewModel baseModel, GooglePlusAccountViewModel gpmodel)
        {
            baseModel.UserName = gpmodel.UserName;
            baseModel.Password = gpmodel.Password;
            baseModel.ConfirmPassword = gpmodel.ConfirmPassword;
            baseModel.FirstName = gpmodel.FirstName;
            baseModel.LastName = gpmodel.LastName;
            baseModel.EmailAddress = gpmodel.EmailAddress;
            baseModel.DOB = gpmodel.DOB;
            baseModel.Phone = gpmodel.Phone;
            baseModel.AccountTypeID = int.Parse(gpmodel.AccountType);
            baseModel.AddressLine1 = gpmodel.AddressLine1;
            baseModel.AddressLine2 = gpmodel.AddressLine2;
            baseModel.City = gpmodel.City;
            baseModel.PostalCode = gpmodel.PostalCode;
            baseModel.Region = gpmodel.Region;
            baseModel.SecurityQuestion1 = string.Empty;
            baseModel.SecurityQuestion2 = string.Empty;
            baseModel.Answer1 = string.Empty;
            baseModel.Answer2 = string.Empty;
            baseModel.SocialNetworkSource = "Google";
            baseModel.StripeToken = gpmodel.StripeToken;
            baseModel.ProfilePicture = gpmodel.ProfilePicture;
        }



        public void FillDataFromFacebookIAccountInfo(PremiumAccountViewModel baseModel, FacebookAccountViewModel fbmodel)
        {
            baseModel.UserName = fbmodel.UserName;
            baseModel.Password = fbmodel.Password;
            baseModel.ConfirmPassword = fbmodel.ConfirmPassword;
            baseModel.FirstName = fbmodel.FirstName;
            baseModel.LastName = fbmodel.LastName;
            baseModel.EmailAddress = fbmodel.EmailAddress;
            baseModel.DOB = fbmodel.DOB;
            baseModel.Phone = fbmodel.Phone;
            baseModel.AccountTypeID = int.Parse(fbmodel.AccountType);
            baseModel.AddressLine1 = fbmodel.AddressLine1;
            baseModel.AddressLine2 = fbmodel.AddressLine2;
            baseModel.City = fbmodel.City;
            baseModel.PostalCode = fbmodel.PostalCode;
            baseModel.Region = fbmodel.Region;
            baseModel.SecurityQuestion1 = string.Empty;
            baseModel.SecurityQuestion2 = string.Empty;
            baseModel.Answer1 = string.Empty;
            baseModel.Answer2 = string.Empty;
            baseModel.SocialNetworkSource = "Facebook";
            baseModel.StripeToken = fbmodel.StripeToken;
            baseModel.ProfilePicture = fbmodel.ProfilePicture;





        }

        public void FillDataFromGoogleIAccountInfo(PremiumAccountViewModel baseModel, GooglePlusAccountViewModel gpmodel)
        {
            baseModel.UserName = gpmodel.UserName;
            baseModel.Password = gpmodel.Password;
            baseModel.ConfirmPassword = gpmodel.ConfirmPassword;
            baseModel.FirstName = gpmodel.FirstName;
            baseModel.LastName = gpmodel.LastName;
            baseModel.EmailAddress = gpmodel.EmailAddress;
            baseModel.DOB = gpmodel.DOB;
            baseModel.Phone = gpmodel.Phone;
            baseModel.AccountTypeID = int.Parse(gpmodel.AccountType);
            baseModel.AddressLine1 = gpmodel.AddressLine1;
            baseModel.AddressLine2 = gpmodel.AddressLine2;
            baseModel.City = gpmodel.City;
            baseModel.PostalCode = gpmodel.PostalCode;
            baseModel.Region = gpmodel.Region;
            baseModel.SecurityQuestion1 = string.Empty;
            baseModel.SecurityQuestion2 = string.Empty;
            baseModel.Answer1 = string.Empty;
            baseModel.Answer2 = string.Empty;
            baseModel.SocialNetworkSource = "Google";
            baseModel.StripeToken = gpmodel.StripeToken;
            baseModel.ProfilePicture = gpmodel.ProfilePicture;
        }

        public void FillDataFromFacebookIAccountInfo(AphidLabAccountModel baseModel, FacebookAccountViewModel fbmodel)
        {
            baseModel.UserName = fbmodel.UserName;
            baseModel.Password = fbmodel.Password;
            baseModel.ConfirmPassword = fbmodel.ConfirmPassword;
            baseModel.FirstName = fbmodel.FirstName;
            baseModel.LastName = fbmodel.LastName;
            baseModel.EmailAddress = fbmodel.EmailAddress;
            baseModel.DOB = fbmodel.DOB;
            baseModel.Phone = fbmodel.Phone;
            baseModel.AccountTypeID = int.Parse(fbmodel.AccountType);
            baseModel.AddressLine1 = fbmodel.AddressLine1;
            baseModel.AddressLine2 = fbmodel.AddressLine2;
            baseModel.City = fbmodel.City;
            baseModel.PostalCode = fbmodel.PostalCode;
            baseModel.Region = fbmodel.Region;
            baseModel.SecurityQuestion1 = string.Empty;
            baseModel.SecurityQuestion2 = string.Empty;
            baseModel.Answer1 = string.Empty;
            baseModel.Answer2 = string.Empty;
            baseModel.SocialNetworkSource = "Facebook";
            baseModel.StripeToken = fbmodel.StripeToken;
            baseModel.ProfilePicture = fbmodel.ProfilePicture;





        }

        public void FillDataFromGoogleIAccountInfo(AphidLabAccountModel baseModel, GooglePlusAccountViewModel gpmodel)
        {
            baseModel.UserName = gpmodel.UserName;
            baseModel.Password = gpmodel.Password;
            baseModel.ConfirmPassword = gpmodel.ConfirmPassword;
            baseModel.FirstName = gpmodel.FirstName;
            baseModel.LastName = gpmodel.LastName;
            baseModel.EmailAddress = gpmodel.EmailAddress;
            baseModel.DOB = gpmodel.DOB;
            baseModel.Phone = gpmodel.Phone;
            baseModel.AccountTypeID = int.Parse(gpmodel.AccountType);
            baseModel.AddressLine1 = gpmodel.AddressLine1;
            baseModel.AddressLine2 = gpmodel.AddressLine2;
            baseModel.City = gpmodel.City;
            baseModel.PostalCode = gpmodel.PostalCode;
            baseModel.Region = gpmodel.Region;
            baseModel.SecurityQuestion1 = string.Empty;
            baseModel.SecurityQuestion2 = string.Empty;
            baseModel.Answer1 = string.Empty;
            baseModel.Answer2 = string.Empty;
            baseModel.SocialNetworkSource = "Google";
            baseModel.StripeToken = gpmodel.StripeToken;
            baseModel.ProfilePicture = gpmodel.ProfilePicture;
        }


        //

        public void FillDataFromFacebookIAccountInfo(BasicAccountViewModel baseModel, FacebookAccountViewModel fbmodel)
        {
            
            baseModel.UserName = fbmodel.UserName;
            baseModel.Password = fbmodel.Password;
            baseModel.ConfirmPassword = fbmodel.ConfirmPassword;
            baseModel.FirstName = fbmodel.FirstName;
            baseModel.LastName = fbmodel.LastName;
            baseModel.EmailAddress = fbmodel.EmailAddress;
            baseModel.DOB = fbmodel.DOB;
            baseModel.Phone = fbmodel.Phone;
            baseModel.AccountTypeID = int.Parse(fbmodel.AccountType);
            baseModel.AddressLine1 = fbmodel.AddressLine1;
            baseModel.AddressLine2 = fbmodel.AddressLine2;
            baseModel.City = fbmodel.City;
            baseModel.PostalCode = fbmodel.PostalCode;
            baseModel.Region = fbmodel.Region;
            baseModel.SecurityQuestion1 = string.Empty;
            baseModel.SecurityQuestion2 = string.Empty;
            baseModel.Answer1 = string.Empty;
            baseModel.Answer2 = string.Empty;
            baseModel.SocialNetworkSource = "Facebook";
            baseModel.StripeToken = fbmodel.StripeToken;
            baseModel.ProfilePicture = fbmodel.ProfilePicture;
        }

        public void FillDataFromFacebookIAccountInfo(BasicAccountViewModel baseModel, GooglePlusAccountViewModel gpmodel)
        {

            baseModel.UserName = gpmodel.UserName;
            baseModel.Password = gpmodel.Password;
            baseModel.ConfirmPassword = gpmodel.ConfirmPassword;
            baseModel.FirstName = gpmodel.FirstName;
            baseModel.LastName = gpmodel.LastName;
            baseModel.EmailAddress = gpmodel.EmailAddress;
            baseModel.DOB = gpmodel.DOB;
            baseModel.Phone = gpmodel.Phone;
            baseModel.AccountTypeID = int.Parse(gpmodel.AccountType);
            baseModel.AddressLine1 = gpmodel.AddressLine1;
            baseModel.AddressLine2 = gpmodel.AddressLine2;
            baseModel.City = gpmodel.City;
            baseModel.PostalCode = gpmodel.PostalCode;
            baseModel.Region = gpmodel.Region;
            baseModel.SecurityQuestion1 = string.Empty;
            baseModel.SecurityQuestion2 = string.Empty;
            baseModel.Answer1 = string.Empty;
            baseModel.Answer2 = string.Empty;
            baseModel.SocialNetworkSource = "Google";
            baseModel.StripeToken = gpmodel.StripeToken;
            baseModel.ProfilePicture = gpmodel.ProfilePicture;
        }

        public void FillDataFromFacebookISecurityQuestions(ISecurityQuestions baseModel, FacebookAccountViewModel fbmodel)
        {
            baseModel.SecurityQuestion1 = string.Empty;
            baseModel.SecurityQuestion2 = fbmodel.SecurityQuestion2;
            baseModel.Answer1 = fbmodel.Answer1;
            baseModel.Answer2 = fbmodel.Answer2;
        }

        public void FillDataFromFacebookISecurityQuestions(ISecurityQuestions baseModel, GooglePlusAccountViewModel gpmodel)
        {
            baseModel.SecurityQuestion1 = string.Empty;
            baseModel.SecurityQuestion2 = gpmodel.SecurityQuestion2;
            baseModel.Answer1 = gpmodel.Answer1;
            baseModel.Answer2 = gpmodel.Answer2;
        }

        public string FacebookByterRegister(FacebookAccountViewModel fbmodel)
        {
            ByterAccountViewModel bytermodel = new ByterAccountViewModel();
            //bytermodel = fbmodel;
            FillDataFromFacebookIAccountInfo(bytermodel, fbmodel);
            //FillDataFromFacebookISecurityQuestions(bytermodel, fbmodel);--facebook account model does not need questions
            bytermodel.RecoveryEmail = fbmodel.RecoveryEmail;
            try
            {
                if (ModelState.IsValid)
                {
                    ImageUploader.UploadProfilePictureAndSetLocation(bytermodel);                    
                   // string encryptPwd = CryptorEngine.Encrypt(bytermodel.Password, true);
                    IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                    //bytermodel.Password = encryptPwd;
                    //bytermodel.UserName = fbmodel.UserName.ToLower();
                    Guid accontid = Guid.NewGuid();
                    bytermodel.AccountID = accontid;
                    bool _records = userRegister.RegisterByterAccount(bytermodel);
                    if (_records)
                    {
                        Guid token = Guid.NewGuid();
                        bool activationresult = userRegister.activationInsert(accontid, token, bytermodel.UserName);
                        Email mail = new Email();//send mail                
                        mail.sendMaill(bytermodel.AccountID, bytermodel.EmailAddress, "Byter", token, bytermodel.UserName, "VerifyEmail");
                        IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                        var loginProfile = accountsService.LoginWithSocialSite(bytermodel.AccountID.ToString());
                        AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
                        return ("ByterAccountInfo+" + "Byter");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can Not Insert");
                    }
                }
                return ("LoginUser+" + "Accounts");
            }
            catch(Exception ex)
            {
                return ("LoginUser+" + "Accounts");
            }
        }
        public string FacebookBasicRegister(FacebookAccountViewModel fbmodel)
        {
            BasicAccountViewModel basicmodel = new BasicAccountViewModel();
            FillDataFromFacebookIAccountInfo(basicmodel, fbmodel);
            //FillDataFromFacebookISecurityQuestions(basicmodel, fbmodel);            
            basicmodel.RecoveryEmail = fbmodel.RecoveryEmail;

            try
            {
                if (fbmodel.AccountType == "2" || fbmodel.AccountType == "4")
                {
                    if (!string.IsNullOrWhiteSpace(fbmodel.PromoCode))
                    {
                        var coupon = StripeClient.ValidatePromotion(fbmodel.PromoCode);
                        if (coupon == null || (coupon.RedeemBy.HasValue && coupon.RedeemBy < DateTime.UtcNow))
                        {
                            return ErrorConstants.InvalidPromotion;

                        }
                    }
                }
                ImageUploader.UploadProfilePictureAndSetLocation(basicmodel);
                //string encryptPwd = CryptorEngine.Encrypt(basicmodel.Password, true);
                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                basicmodel.UserName = fbmodel.UserName.ToLower();
                //basicmodel.Password = string.Empty;
                basicmodel.AccountIdBasic = Guid.NewGuid();
                bool _records = userRegister.RegisterBasicAccount(basicmodel);

                if (_records)
                {
                    Guid token = Guid.NewGuid();
                    bool activationresult = userRegister.activationInsert(basicmodel.AccountIdBasic, token, basicmodel.UserName);
                    Email mail = new Email();//send mail                
                    mail.sendMaill(basicmodel.AccountIdBasic, basicmodel.EmailAddress, "Basic", token, basicmodel.UserName, "VerifyEmail");
                    IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                    var loginProfile = accountsService.LoginWithSocialSite(basicmodel.AccountIdBasic.ToString());
                    AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
                    return ("BasicAccountInfo+" + "Basic");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
                // }
                return ("LoginUser+" + "Accounts");
            }
            catch
            {
                return ("LoginUser+" + "Accounts");
            }
        }
        public string FacebookAphidPremiumRegister(FacebookAccountViewModel fbmodel)
        {
            PremiumAccountViewModel premmodel = new PremiumAccountViewModel();
            FillDataFromFacebookIAccountInfo(premmodel, fbmodel);
            //FillDataFromFacebookISecurityQuestions(tisecmodel, fbmodel);

            try
            {
                ImageUploader.UploadProfilePictureAndSetLocation(premmodel);

                //string encryptPwd = CryptorEngine.Encrypt(tisecmodel.Password, true);

                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                //tisecmodel.Password = encryptPwd;
                premmodel.UserName = premmodel.UserName.ToLower();
                premmodel.AccountIDPre = Guid.NewGuid();
                bool _records = userRegister.RegisterPremiumAccount(premmodel);
                if (_records)
                {
                    Guid token = Guid.NewGuid();
                    bool activationresult = userRegister.activationInsert(premmodel.AccountIDPre, token, premmodel.UserName);
                    Email mail = new Email();//send mail                
                    mail.sendMaill(premmodel.AccountIDPre, premmodel.EmailAddress, "Premium", token, premmodel.UserName, "VerifyEmail");
                    IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                    var loginProfile = accountsService.LoginWithSocialSite(premmodel.AccountIDPre.ToString());
                    AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
                    return ("BasicAccountInfo+" + "Basic");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }

                return ("LoginUser+" + "Accounts");
            }
            catch
            {
                return ("LoginUser+" + "Accounts");
            }
        }

        public string GoogleAphidPremiumRegister(FacebookAccountViewModel gpviewmodel)
        {
            PremiumAccountViewModel premmodel = new PremiumAccountViewModel();
            FillDataFromFacebookIAccountInfo(premmodel, gpviewmodel);
            //FillDataFromFacebookISecurityQuestions(tisecmodel, fbmodel);

            try
            {
                ImageUploader.UploadProfilePictureAndSetLocation(premmodel);

                //string encryptPwd = CryptorEngine.Encrypt(tisecmodel.Password, true);

                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                //tisecmodel.Password = encryptPwd;
                premmodel.UserName = premmodel.UserName.ToLower();
                premmodel.AccountIDPre = Guid.NewGuid();
                bool _records = userRegister.RegisterPremiumAccount(premmodel);
                if (_records)
                {
                    Guid token = Guid.NewGuid();
                    bool activationresult = userRegister.activationInsert(premmodel.AccountIDPre, token, premmodel.UserName);
                    Email mail = new Email();//send mail                
                    mail.sendMaill(premmodel.PremiumUserID, premmodel.EmailAddress, "Premium", token, premmodel.UserName, "VerifyEmail");
                    IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                    var loginProfile = accountsService.LoginWithSocialSite(premmodel.AccountIDPre.ToString());
                    AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
                    return ("BasicAccountInfo+" + "Basic");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }

                return ("LoginUser+" + "Accounts");
            }
            catch
            {
                return ("LoginUser+" + "Accounts");
            }
        }
        public string FacebookAphidLabRegister(FacebookAccountViewModel fbmodel)
        {
            AphidLabAccountModel labmodel = new AphidLabAccountModel();
            FillDataFromFacebookIAccountInfo(labmodel, fbmodel);
            //FillDataFromFacebookISecurityQuestions(labmodel, fbmodel);

            labmodel.RecoveryEmail = fbmodel.RecoveryEmail;
            try
            {
                if (ModelState.IsValid)
                {
                    if (fbmodel.AccountType == "2" || fbmodel.AccountType == "4")
                    {
                        if (!string.IsNullOrWhiteSpace(fbmodel.PromoCode))
                        {
                            var coupon = StripeClient.ValidatePromotion(fbmodel.PromoCode);
                            if (coupon == null || (coupon.RedeemBy.HasValue && coupon.RedeemBy < DateTime.UtcNow))
                            {
                                return ErrorConstants.InvalidPromotion;

                            }
                        }
                    }
                    ImageUploader.UploadProfilePictureAndSetLocation(labmodel);

                    //Incrypt password
                  //  string encryptPwd = CryptorEngine.Encrypt(labmodel.Password, true);

                    IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                   // labmodel.Password = encryptPwd;
                    labmodel.UserName = labmodel.UserName.ToLower();
                    Guid accontid = Guid.NewGuid();
                    labmodel.AphidlabUserID = accontid;
                    bool _records = userRegister.RegisterAphidlabAccount(labmodel);
                    if (_records)
                    {
                        Guid token = Guid.NewGuid();
                        bool activationresult = userRegister.activationInsert(accontid, token, labmodel.UserName);
                        Email mail = new Email();//send mail                
                        mail.sendMaill(labmodel.AphidlabUserID.Value, labmodel.EmailAddress, "AphidLab", token, labmodel.UserName, "VerifyEmail");
                        IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                        var loginProfile = accountsService.LoginWithSocialSite(labmodel.AphidlabUserID.ToString());
                        AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
                        return ("AphidLabsAccountInfo+" + "AphidLabs");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can Not Insert");
                    }
                }
                return ("LoginUser+" + "Accounts");
            }
            catch
            {
                return ("LoginUser+" + "Accounts");
            }
        }


      


        [HttpGet]
        public ActionResult SignUpWithFacebook(FacebookAccountViewModel fbviewmodel)
        {
            if(fbviewmodel.Validation==null)
                fbviewmodel.Validation=new ValidationModel();
            if(string.IsNullOrEmpty(fbviewmodel.AccountType))
            {
                return RedirectToAction("LoginorSignUp", "Accounts");
            }
            //ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");
            //FacebookByterRegister(fbviewmodel);    
            bool mailExixt = false;            
            AphidBytes.BLL.AccountBLL bll = new BLL.AccountBLL();
            if (!string.IsNullOrEmpty(fbviewmodel.EmailAddress))
            {
                mailExixt = bll.IsEmailAlreadyRegistered(fbviewmodel.EmailAddress);
               
            }
            
            ViewBag.MailExist = mailExixt;
            fbviewmodel.LoadStripeAccountInfo();
            return View(fbviewmodel);
        }
        [HttpPost]
        public ActionResult SignUpWithFacebook(FacebookAccountViewModel fbviewmodel, string id)
        {

            bool mailExixt = false;
            AphidBytes.BLL.AccountBLL bll = new BLL.AccountBLL();
            if (!string.IsNullOrEmpty(fbviewmodel.EmailAddress))
            {
                mailExixt = bll.IsEmailAlreadyRegistered(fbviewmodel.EmailAddress);

            }

            ViewBag.MailExist = mailExixt;
            
            if (ModelState.IsValid)
            {
                if (fbviewmodel.AccountType == "1")
                {
                    //byter
                    string str = FacebookByterRegister(fbviewmodel);
                    string[] action = str.Split('+');
                    return RedirectToAction(action[0], action[1]);
                }
                else if (fbviewmodel.AccountType == "2")
                {
                    //baic
                    string str = FacebookBasicRegister(fbviewmodel);
                    string[] action = str.Split('+');
                    if (action.Length > 1)
                    {
                        return RedirectToAction(action[0], action[1]);
                    }else
                    {
                        fbviewmodel.Validation.AddError(str);
                        return View(fbviewmodel);
                    }
                }
                else if (fbviewmodel.AccountType == "3")
                {
                    //tise
                    string str = FacebookAphidPremiumRegister(fbviewmodel);
                    string[] action = str.Split('+');
                    return RedirectToAction(action[0], action[1]);

                }
                else if (fbviewmodel.AccountType == "4")
                {
                    //lab
                    string str = FacebookAphidLabRegister(fbviewmodel);
                    string[] action = str.Split('+');
                    if (action.Length > 1)
                    {
                        return RedirectToAction(action[0], action[1]);
                    }else
                    {
                        fbviewmodel.Validation.AddError(str);
                        return View(fbviewmodel);
                    }
                    //return RedirectToAction("FacebookAphidLabRegister", fbviewmodel);
                }
                fbviewmodel.LoadStripeAccountInfo();
                return View(fbviewmodel);
            }
            else
            {
                fbviewmodel.LoadStripeAccountInfo();
                return View(fbviewmodel);
            }
        }


        public string GoogleplusByterRegister(GooglePlusAccountViewModel fbmodel)
        {
            
            ByterAccountViewModel bytermodel = new ByterAccountViewModel();
            //bytermodel = fbmodel;
            FillDataFromGoogleIAccountInfo(bytermodel, fbmodel);
            //FillDataFromFacebookISecurityQuestions(bytermodel, fbmodel);--facebook account model does not need questions
            bytermodel.RecoveryEmail = fbmodel.RecoveryEmail;
            try
            {
                if (ModelState.IsValid)
                {
                    ImageUploader.UploadProfilePictureAndSetLocation(bytermodel);
                    // string encryptPwd = CryptorEngine.Encrypt(bytermodel.Password, true);
                    IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                    //bytermodel.Password = encryptPwd;
                    //bytermodel.UserName = fbmodel.UserName.ToLower();
                    Guid accontid = Guid.NewGuid();
                    bytermodel.AccountID = accontid;
                    bool _records = userRegister.RegisterByterAccount(bytermodel);
                    if (_records)
                    {
                        Guid token = Guid.NewGuid();
                        bool activationresult = userRegister.activationInsert(accontid, token, bytermodel.UserName);
                        Email mail = new Email();//send mail                
                        mail.sendMaill(bytermodel.AccountID, bytermodel.EmailAddress, "Byter", token, bytermodel.UserName, "VerifyEmail");
                        IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                        var loginProfile = accountsService.LoginWithSocialSite(bytermodel.AccountID.ToString());
                        AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
                        return ("ByterAccountInfo+" + "Byter");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can Not Insert");
                    }
                }
                return ("LoginUser+" + "Accounts");
            }
            catch (Exception ex)
            {
                return (ex.Message + "LoginUser+" + "Accounts");
            }
            /* ByterAccountViewModel bytermodel = new ByterAccountViewModel();
             FillDataFromFacebookIAccountInfo(bytermodel, fbmodel);
             FillDataFromFacebookISecurityQuestions(bytermodel, fbmodel);

             bytermodel.AddressLine1 = fbmodel.AddressLine1;
             bytermodel.AddressLine2 = fbmodel.AddressLine2;
             bytermodel.City = fbmodel.City;
             bytermodel.Region = fbmodel.Region;
             bytermodel.PostalCode = fbmodel.PostalCode;

             try
             {
                 if (ModelState.IsValid)
                 {
                     ImageUploader.UploadProfilePictureAndSetLocation(bytermodel);

                     //Incrypt password
                     string encryptPwd = CryptorEngine.Encrypt(bytermodel.Password, true);

                     IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                     bytermodel.Password = encryptPwd;
                     bytermodel.UserName = fbmodel.UserName.ToLower();
                     Guid accontid = Guid.NewGuid();
                     bytermodel.AccountID = accontid;
                     bool _records = userRegister.RegisterByterAccount(bytermodel);
                     if (_records)
                     {
                         Guid token = Guid.NewGuid();
                         bool activationresult = userRegister.activationInsert(accontid, token, bytermodel.UserName);
                         Email mail = new Email();
                         return ("RegisterPosting+" + "Accounts");
                     }
                     else
                     {
                         ModelState.AddModelError("", "Can Not Insert");
                     }
                 }
                 return ("LoginUser+" + "Accounts");
             }
             catch
             {
                 return ("LoginUser+" + "Accounts");
             }*/
        }
        public string GoogleplusBasicRegister(GooglePlusAccountViewModel gpviewmodel)
        {/*
            BasicAccountViewModel basicmodel = new BasicAccountViewModel();
            FillDataFromFacebookIAccountInfo(basicmodel, gpviewmodel);
            FillDataFromFacebookISecurityQuestions(basicmodel, gpviewmodel);

            basicmodel.RecoveryEmail = gpviewmodel.RecoveryEmail;
            basicmodel.AddressLine1 = gpviewmodel.AddressLine1;
            basicmodel.AddressLine2 = gpviewmodel.AddressLine2;
            basicmodel.City = gpviewmodel.City;
            basicmodel.Region = gpviewmodel.Region;
            basicmodel.PostalCode = gpviewmodel.PostalCode;
            basicmodel.WebSiteUrl = gpviewmodel.WebSiteUrl;*/
            BasicAccountViewModel basicmodel = new BasicAccountViewModel();
            FillDataFromFacebookIAccountInfo(basicmodel, gpviewmodel);
            //FillDataFromFacebookISecurityQuestions(basicmodel, fbmodel);            
            basicmodel.RecoveryEmail = gpviewmodel.RecoveryEmail;

            try
            {
                if (gpviewmodel.AccountType == "2" || gpviewmodel.AccountType == "4")
                {
                    if (!string.IsNullOrWhiteSpace(gpviewmodel.PromoCode))
                    {
                        var coupon = StripeClient.ValidatePromotion(gpviewmodel.PromoCode);
                        if (coupon == null || (coupon.RedeemBy.HasValue && coupon.RedeemBy < DateTime.UtcNow))
                        {
                            return ErrorConstants.InvalidPromotion;

                        }
                    }
                }
                ImageUploader.UploadProfilePictureAndSetLocation(basicmodel);
                //string encryptPwd = CryptorEngine.Encrypt(basicmodel.Password, true);
                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                basicmodel.UserName = gpviewmodel.UserName.ToLower();
                //basicmodel.Password = string.Empty;
                basicmodel.AccountIdBasic = Guid.NewGuid();
                bool _records = userRegister.RegisterBasicAccount(basicmodel);

                if (_records)
                {
                    Guid token = Guid.NewGuid();
                    bool activationresult = userRegister.activationInsert(basicmodel.AccountIdBasic, token, basicmodel.UserName);
                    Email mail = new Email();//send mail                
                    mail.sendMaill(basicmodel.AccountIdBasic, basicmodel.EmailAddress, "Basic", token, basicmodel.UserName, "VerifyEmail");
                    IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                    var loginProfile = accountsService.LoginWithSocialSite(basicmodel.AccountIdBasic.ToString());
                    AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
                    return ("BasicAccountInfo+" + "Basic");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
                // }
                return ("LoginUser+" + "Accounts");
            }
            catch
            {
                return ("LoginUser+" + "Accounts");
            }


            try
            {
                ImageUploader.UploadProfilePictureAndSetLocation(basicmodel);

                string encryptPwd = CryptorEngine.Encrypt(basicmodel.Password, true);

                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                basicmodel.UserName = gpviewmodel.UserName.ToLower();
                basicmodel.Password = encryptPwd;
                basicmodel.AccountIdBasic = Guid.NewGuid();
                bool _records = userRegister.RegisterBasicAccount(basicmodel);

                if (_records)
                {
                    Guid token = Guid.NewGuid();
                    bool activationresult = userRegister.activationInsert(basicmodel.AccountIdBasic, token, basicmodel.UserName);
                    Email mail = new Email();
                    return ("RegisterPosting+" + "Accounts");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
                // }
                return ("LoginUser+" + "Accounts");
            }
            catch
            {
                return ("LoginUser+" + "Accounts");
            }
        }
        public string GoogleplusAphidTiseRegister(GooglePlusAccountViewModel gpviewmodel)
        {
            AphidTiseAccountViewModel tisecmodel = new AphidTiseAccountViewModel();
            FillDataFromGoogleIAccountInfo(tisecmodel, gpviewmodel);
            FillDataFromFacebookISecurityQuestions(tisecmodel, gpviewmodel);

            tisecmodel.AddressLine1 = gpviewmodel.AddressLine1;
            tisecmodel.AddressLine2 = gpviewmodel.AddressLine2;
            tisecmodel.City = gpviewmodel.City;
            tisecmodel.Region = gpviewmodel.Region;
            tisecmodel.PostalCode = gpviewmodel.PostalCode;
            tisecmodel.Website = gpviewmodel.Website;


            try
            {
                ImageUploader.UploadProfilePictureAndSetLocation(tisecmodel);

                string encryptPwd = CryptorEngine.Encrypt(tisecmodel.Password, true);

                IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                tisecmodel.Password = encryptPwd;
                tisecmodel.UserName = tisecmodel.UserName.ToLower();
                tisecmodel.AccountIdAphid = Guid.NewGuid();
                bool _records = userRegister.RegisterAphidTiseAccount(tisecmodel);
                if (_records)
                {
                    Guid token = Guid.NewGuid();
                    bool activationresult = userRegister.activationInsert(tisecmodel.AccountIdAphid, token, tisecmodel.UserName);
                    Email mail = new Email();
                    // mail.sendMail(tisecmodel.EmailAddress, "AphidTise", token, "", "Welcome");
                    //return ("Index+" + "Home");
                    return ("RegisterPosting+" + "Accounts");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }

                return ("LoginUser+" + "Accounts");
            }
            catch
            {
                return ("LoginUser+" + "Accounts");
            }
        }
        public string GoogleplusAphidLabRegister(GooglePlusAccountViewModel gpviewmodel)
        {/*
            AphidLabAccountModel labmodel = new AphidLabAccountModel();
            FillDataFromGoogleIAccountInfo(labmodel, gpviewmodel);
            FillDataFromFacebookISecurityQuestions(labmodel, gpviewmodel);

            labmodel.RecoveryEmail = gpviewmodel.RecoveryEmail;
            labmodel.AddressLine1 = gpviewmodel.AddressLine1;
            labmodel.AddressLine2 = gpviewmodel.AddressLine2;
            labmodel.City = gpviewmodel.City;
            labmodel.Region = gpviewmodel.Region;
            labmodel.PostalCode = gpviewmodel.PostalCode;
            labmodel.Website = gpviewmodel.Website;


            try
            {
                if (ModelState.IsValid)
                {
                    ImageUploader.UploadProfilePictureAndSetLocation(labmodel);

                    string encryptPwd = CryptorEngine.Encrypt(labmodel.Password, true);

                    IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                    labmodel.Password = encryptPwd;
                    labmodel.UserName = labmodel.UserName.ToLower();
                    Guid accontid = Guid.NewGuid();
                    labmodel.AphidlabUserID = accontid;
                    bool _records = userRegister.RegisterAphidlabAccount(labmodel);
                    if (_records)
                    {
                        Guid token = Guid.NewGuid();
                        bool activationresult = userRegister.activationInsert(accontid, token, labmodel.UserName);
                        Email mail = new Email();
                        return ("RegisterPosting+" + "Accounts");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can Not Insert");
                    }
                }
                return ("LoginUser+" + "Accounts");
            }
            catch
            {
                return ("LoginUser+" + "Accounts");
            }*/
            AphidLabAccountModel labmodel = new AphidLabAccountModel();
            FillDataFromGoogleIAccountInfo(labmodel, gpviewmodel);
            //FillDataFromFacebookISecurityQuestions(labmodel, fbmodel);

            labmodel.RecoveryEmail = gpviewmodel.RecoveryEmail;
            try
            {
                if (ModelState.IsValid)
                {
                    if (gpviewmodel.AccountType == "2" || gpviewmodel.AccountType == "4")
                    {
                        if (!string.IsNullOrWhiteSpace(gpviewmodel.PromoCode))
                        {
                            var coupon = StripeClient.ValidatePromotion(gpviewmodel.PromoCode);
                            if (coupon == null || (coupon.RedeemBy.HasValue && coupon.RedeemBy < DateTime.UtcNow))
                            {
                                return ErrorConstants.InvalidPromotion;

                            }
                        }
                    }
                    ImageUploader.UploadProfilePictureAndSetLocation(labmodel);

                    //Incrypt password
                    //  string encryptPwd = CryptorEngine.Encrypt(labmodel.Password, true);

                    IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
                    // labmodel.Password = encryptPwd;
                    labmodel.UserName = labmodel.UserName.ToLower();
                    Guid accontid = Guid.NewGuid();
                    labmodel.AphidlabUserID = accontid;
                    bool _records = userRegister.RegisterAphidlabAccount(labmodel);
                    if (_records)
                    {
                        Guid token = Guid.NewGuid();
                        bool activationresult = userRegister.activationInsert(accontid, token, labmodel.UserName);
                        Email mail = new Email();//send mail                
                        mail.sendMaill(labmodel.AphidlabUserID.Value, labmodel.EmailAddress, "AphidLab", token, labmodel.UserName, "VerifyEmail");
                        IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                        var loginProfile = accountsService.LoginWithSocialSite(labmodel.AphidlabUserID.ToString());
                        AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
                        return ("AphidLabsAccountInfo+" + "AphidLabs");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can Not Insert");
                    }
                }
                return ("LoginUser+" + "Accounts");
            }
            catch
            {
                return ("LoginUser+" + "Accounts");
            }
        }
        [HttpGet]
        public ActionResult SignUpWithGoogle(GooglePlusAccountViewModel fbviewmodel)
        {
            if (fbviewmodel.Validation == null)
                fbviewmodel.Validation = new ValidationModel();

            if (string.IsNullOrEmpty(fbviewmodel.AccountType))
            {
                return RedirectToAction("LoginorSignUp", "Accounts");
            }
            //ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");
            //FacebookByterRegister(fbviewmodel);    
            bool mailExixt = false;            
            AphidBytes.BLL.AccountBLL bll = new BLL.AccountBLL();
            if (!string.IsNullOrEmpty(fbviewmodel.EmailAddress))
            {
                mailExixt = bll.IsEmailAlreadyRegistered(fbviewmodel.EmailAddress);
               
            }
            
            ViewBag.MailExist = mailExixt;
            fbviewmodel.LoadStripeAccountInfo();
            return View(fbviewmodel);
            
            //return View(fbviewmodel);
        }
        [HttpPost]
        public ActionResult SignUpWithGoogle(GooglePlusAccountViewModel gpviewmodel, string id)
        {
            bool mailExixt = false;
            AphidBytes.BLL.AccountBLL bll = new BLL.AccountBLL();
            if (!string.IsNullOrEmpty(gpviewmodel.EmailAddress))
            {
                mailExixt = bll.IsEmailAlreadyRegistered(gpviewmodel.EmailAddress);

            }

            ViewBag.MailExist = mailExixt;
            if (gpviewmodel.AccountType == "1")
            {
                //byter
                string str = GoogleplusByterRegister(gpviewmodel);
                string[] action = str.Split('+');
                return RedirectToAction(action[0], action[1]);
            }
            else if (gpviewmodel.AccountType == "2")
            {
                //baic

                string str = GoogleplusBasicRegister(gpviewmodel);
                string[] action = str.Split('+');
                if (action.Length > 1)
                    return RedirectToAction(action[0], action[1]);
                else
                {
                    gpviewmodel.Validation.AddError(str);
                    return View(gpviewmodel);
                }
            }
            else if (gpviewmodel.AccountType == "3")
            {
                //tise
                string str = GoogleplusAphidTiseRegister(gpviewmodel);
                string[] action = str.Split('+');
                return RedirectToAction(action[0], action[1]);

            }
            else if (gpviewmodel.AccountType == "4")
            {
                //lab
                string str = GoogleplusAphidLabRegister(gpviewmodel);
                string[] action = str.Split('+');
                if (action.Length > 1)
                    return RedirectToAction(action[0], action[1]);
                else
                {
                    gpviewmodel.Validation.AddError(str);
                    return View(gpviewmodel);
                };
            }

            return View(gpviewmodel);
        }

        public ActionResult UserSubscription(int? id)
        {
            AphidBytes.BLL.AccountBLL bll = new BLL.AccountBLL();
            IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
            UserSubscribeModel objModel = new UserSubscribeModel();
            string userstr = Convert.ToString(Request.QueryString["Name"]);
            IAphidIdentity aphidIdentity = null;
            if (AphidBytes.Web.Session_Helper.AphidSession.Current.AuthenticatedUser != null)
            {
                aphidIdentity = AphidBytes.Web.Session_Helper.AphidSession.Current.AuthenticatedUser.Identity;
            }
            if (userstr != null)
            {
                objModel = bll.GetUserInfoByUserName(userstr);
                if (objModel != null)
                {                   
                    objModel.SubscribeUserId = objModel.UserId;
                    if (aphidIdentity != null)
                    {
                        objModel.UserId = new Guid(aphidIdentity.UserId);
                    }
                    else
                    {
                        objModel.isOwn = true;
                        objModel.UserId = Guid.Empty;
                    }
                    objModel.ProfilePic = objModel.ProfilePic;
                    UserSubscribeModel objTmp = userRegister.GetUserSubscribe(objModel.SubscribeUserId, objModel.UserId);
                    if (objTmp != null && objTmp.SubscribeUserId != Guid.Empty)
                    {
                        objModel.isSubscribed = true;
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                var loginProfile = userRegister.LoginWithSocialSite(aphidIdentity.UserId.ToString());
                objModel.ProfilePic = aphidIdentity.ProfilePicture;
                objModel.UserName = aphidIdentity.Username;
                objModel.UserId = new Guid(aphidIdentity.UserId);
                objModel.Email = loginProfile.EmailAddress;
                objModel.isOwn = true;
            }
            
            return View(objModel);
        }
        [HttpPost]
        public ActionResult UserSubscription(UserSubscribeModel objModel)
        {
            IAccounts userRegister = DependencyResolver.Current.GetService<IAccounts>();
            IAphidIdentity aphidIdentity = AphidBytes.Web.Session_Helper.AphidSession.Current.AuthenticatedUser.Identity;
            if (objModel != null)
            {
                objModel.SubscribeUserId = objModel.SubscribeUserId;
                objModel.UserId = new Guid(aphidIdentity.UserId);
                objModel.ProfilePic = objModel.ProfilePic;
                objModel.UserName = objModel.UserName;
                userRegister.InsertUserSubscribe(objModel);
                objModel.isSubscribed = true;
            }
           
            return View(objModel);
        }
    }
}
