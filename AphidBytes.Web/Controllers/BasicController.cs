using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Web.Models;
using FileUploadMVC4.Models;
using iTextSharp.text.pdf.qrcode;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
//using Spire.Pdf;
//using Spire.Pdf.Graphics;
using AphidBytes.Web.Session_Helper;
using AphidBytes.Web.App_Code;
using Scribd.Net;
using AphidBytes.Web.Web;
using AphidBytes.Web.Utility;
using AphidBytes.Web.Extensions;
using AphidBytes.Core.Extensions;
using System.Transactions;

namespace AphidBytes.Web.Controllers
{
    [SessionHelper]
    public class BasicController : AphidController
    {
        //
        // GET: /Basic/
        static string nn = "";
        static string songname = "";
        static string songname2 = "";
        static string img = null;
        static string imagebyte1 = null;
        static string imagebyte2 = null;
        static byte[] songbyte = null;
        static string FilePhoto = null;
        static string imagebyte = null;
        static string pdfpath = null;
        static string pdffilepath = null;
        static string songpath = null;
        static string songpath2 = null;
        static string imagepath = null;
        static string ZipPath = null;
        static string intphoto = null;
        static byte[] videobyte = null;
        static string videopath = null;
        static byte[] gg = null;
        static string session = "";
        static string trackNo = "";
        static string artphototitle =null;
        static byte[] ZipArray = null;
        static string IntrepputedAudioPath;

        static List<BasicGenerateCloneModel> tvar1 = new List<BasicGenerateCloneModel>();
        static List<InterruptedFileModel> tvar2 = new List<InterruptedFileModel>();
        static List<CreateLinkPostModel> tvar3 = new List<CreateLinkPostModel>();
        static List<AllGenerateCloneModel> tvar4 = new List<AllGenerateCloneModel>();


        List<string> _songlist = new List<string>();

        private readonly IBasic _basic;
        private readonly ICommon _cmn;
        public class ImageNames
        {
            public string NameDefault { get; set; }
            public string Custom { get; set; }
        }

        public BasicController()
        {
            _basic = DependencyResolver.Current.GetService<IBasic>();
            _cmn = DependencyResolver.Current.GetService<ICommon>();
        }

        public ActionResult BasicAccountInfo()
        {
            ViewBag.Sucess = false;
            try
            {
                if (AphidSession.Current.AuthenticatedUser?.Identity?.Username != null)
                {
                    session = AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString();
                }


                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new MessageModel { NewCount = Convert.ToInt32(count) };
                BasicAccountViewModel basicTiseData = _basic.GetBasicAccountInfo(userID);
                IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                var loginProfile = accountsService.LoginWithSocialSite(userID.ToString());
                
                var result = basicTiseData.isActive;
                if ((basicTiseData.RecoveryEmail != null) && (basicTiseData.RecoveryEmail != ""))
                {
                    if (result == true)
                    {
                        ViewBag.Message = "verified";
                    }
                    else
                    {
                        ViewBag.Message = "not verified";
                    }
                }
              
                Session["EmailAddress"] = basicTiseData.EmailAddress;
                basicTiseData.NewCount = count;
                if (!string.IsNullOrEmpty(basicTiseData.Password) || !string.IsNullOrEmpty(basicTiseData.ConfirmPassword))
                {
                    string decryptpwd = CryptorEngine.Decrypt(basicTiseData.Password, true);
                    string decryptpwdd = CryptorEngine.Decrypt(basicTiseData.ConfirmPassword, true);
                    basicTiseData.Password = decryptpwd;
                    basicTiseData.ConfirmPassword = decryptpwdd;
 
                }
                if (!basicTiseData.isActive.HasValue || !basicTiseData.isActive.Value)
                {
                    basicTiseData.Validation = new ValidationModel();
                    basicTiseData.Validation.AddWarning("An email to verify your account was sent, check your Inbox or Spam folder");
                }

                return View(basicTiseData);
            }
            catch (Exception exc)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        [HttpPost]
        public ActionResult BasicAccountInfo(BasicAccountViewModel basicmodel)
        {
            try
            {
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.Password);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.ConfirmPassword);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.PostalCode);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.Region);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.City);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.AddressLine1);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.AddressLine2);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.NameOnCard);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.CSV);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.UserName);
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                BasicAccountViewModel basicData = _basic.GetBasicAccountInfo(userID);

                if (!ModelState.IsValid)
                {
                    basicmodel.UserName = basicData.UserName;
                    basicmodel.Validation.FillFromModelState(ModelState);
                    return View(basicmodel);
                }

                BasicAccountViewModel basicData1 = null;
                string guid = Guid.NewGuid().ToString();
                basicmodel.BasicUserID = userID;
                if (string.IsNullOrEmpty(basicData.SocialNetworkSource))
                {
                    string encryptPwd = CryptorEngine.Encrypt(basicmodel.Password, true);                 
                    basicmodel.Password = encryptPwd;
                }

                if (string.IsNullOrEmpty(basicData.ProfilePicturePath))
                {
                    ImageUploader.UploadProfilePictureAndSetLocation(basicmodel);
                }
                else
                {

                    if (ImageUploader.DeleteProfileImage(basicData.ProfilePicturePath))
                        ImageUploader.UploadProfilePictureAndSetLocation(basicmodel);
                }
                basicmodel.AddressID = basicData.AddressID;
                basicmodel.BankAccountID = basicData.BankAccountID;
                basicmodel.SecurityQuestionID = basicData.SecurityQuestionID;

                if (Session["BasicAudioFile"] != null)
                {
                    basicmodel.SelectedAudioPathForInt = Session["BasicAudioFile"].ToString();
                    var sp = (basicmodel.SelectedAudioPathForInt).Split('_');
                    basicmodel.SelectedAudioForInt = sp[1];
                }
                if (Session["SelectedAduio"] != null)
                {
                    basicmodel.SelectedAudio = Session["SelectedAduio"].ToString().Trim();

                    basicmodel.CustomAudioSelectedNew = Session["SelectedAduio"].ToString();
                }

                if (Session["BasicImage"] != null)
                {
                    basicmodel.SelectedImagePathInt = Session["BasicImage"].ToString();
                    var sp = (basicmodel.SelectedImagePathInt).Split('_');
                    basicmodel.SelectedImageNameInt = sp[1];
                }

                if (Session["SelectedImage"] != null)
                {
                    basicmodel.SelectedImage = Session["SelectedImage"].ToString().Trim();
                }

                string rec = basicmodel.RecoveryEmail;
                string Email = _basic.FetchEmailRecord(userID, rec);
                bool updateByter = _basic.UpdateBasicAccountInfo(basicmodel);


                //bool resultt = byter.updatbasicaccountt(userID, rec);

                basicData1 = _basic.GetBasicAccountInfo(userID);

                if (updateByter)
                {
                    if ((basicmodel.RecoveryEmail != null) && (basicmodel.RecoveryEmail != ""))
                    {
                        //if (basicData1.RecoveryEmail == null)

                        if ((basicData1.isActive == null) || (basicData1.isActive == false))
                        {
                            if (Email != "Already EmailId Present")
                            {
                                Guid token = Guid.NewGuid();
                                Email mail = new Email();
                                mail.sendMaill(basicmodel.BasicUserID, basicmodel.RecoveryEmail, "Basic", token, "", "VerifyEmail");
                            }
                        }

                    }
                    var result = basicData1.isActive;
                    if ((basicData1.RecoveryEmail != null) && (basicData1.RecoveryEmail != ""))
                    {
                        if (basicmodel.RecoveryEmail != null)
                        {
                            if (result == true)
                            {
                                ViewBag.Message = "verified";
                            }
                            else
                            {
                                ViewBag.Message = "not verified";
                            }
                        }
                    }
                    Guid userID1 = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());

                    basicData1 = _basic.GetBasicAccountInfo(userID);

                    

                    string count = _cmn.GetNewCount(userID1);
                    basicData1.NewCount = count;
                    if (string.IsNullOrEmpty(basicData.SocialNetworkSource))
                    {
                        string decryptpwd = CryptorEngine.Decrypt(basicData1.Password, true);
                        string decryptpwdd = CryptorEngine.Decrypt(basicData1.ConfirmPassword, true);
                        basicData1.Password = decryptpwd;
                        basicData1.ConfirmPassword = decryptpwdd;
                    }
                }
                ViewBag.Sucess = true;
                return View(basicData1);

            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult ChangePassword()
        {
            var model = new BasicAccountViewModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                model = _basic.GetBasicAccountInfo(userID);
                int count = Convert.ToInt32(_cmn.GetNewCount(userID));
                MessageModel mdl = new MessageModel();
                mdl.NewCount = count;
                ViewBag.MegCount = count;
            }
            catch (Exception ex)
            {
                model.Validation.AddError("Oops, something happened, try again later");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePassword(BasicAccountViewModel basicmodel)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                BasicAccountViewModel basicData = _basic.GetBasicAccountInfo(userID);
                basicmodel.BasicUserID = userID;
                var encryptPwd = CryptorEngine.Encrypt(basicmodel.Password, true);
                var success = _cmn.UpdatePassword(userID, encryptPwd);
                if (!success)
                {
                    basicData.Validation.AddError("Unable to change the current password");
                }
                else
                {
                    basicData.Validation.AddInformation("Successfully changed the password");
                }

                return View(basicData);
            }
            catch (Exception)
            {
                return RedirectToAction("BasicAccountInfo");
            }
        }
        public ActionResult CreditCardInfo()
        {
            var model = new BasicAccountViewModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                model = _basic.GetBasicAccountInfo(userID);
                string count = _cmn.GetNewCount(userID);
                MessageModel mdl = new MessageModel();
                mdl.NewCount = Convert.ToInt32(count);
                ViewBag.MegCount = count;
            }
            catch (Exception ex)
            {
                model.Validation.AddError("Oops, something happened, try again later");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult CreditCardInfo(BasicAccountViewModel basicmodel, string stripeToken)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                BasicAccountViewModel basicData = _basic.GetBasicAccountInfo(userID);
                var success = _cmn.UpdateStripeCard(userID, stripeToken);
                if (!success)
                {
                    basicData.Validation.AddError("Unable to change the credit card information on file");
                }
                else
                {
                    basicData.Validation.AddInformation("Successfully changed your credit card information");
                }

                return View(basicData);
            }
            catch (Exception)
            {
                return RedirectToAction("BasicAccountInfo");
            }
        }


        public string FacebookPost(string type)
        {
            try
            {
                return Posting(type, "Facebook");
            }
            catch
            {
                return "login error";
            }
        }

        public string YouTubePost(string type)
        {
            try
            {
                return Posting(type, "YouTube");                 
            }
            catch
            {
                return "login error";
            }
        }

        public string SoundCloudPost(string type)
        {
            try
            {
                return Posting(type, "SoundCloud");                 
            }
            catch
            {
                return "login error";
            }
        }
        
        public string LinkedLinPost(string type)
        {
            try
            {
                return Posting(type, "LinkedLin");                 
            }
            catch
            {
                return "login error";
            }
        }
        
        public string TwitterPost(string type)
        {
            try
            {
                return Posting(type, "Twitter");
            }
            catch
            {
                return "login error";
            }
        }

        public string FlickerPost(string type)
        {
            try
            {
                return Posting(type, "Flicker");                
            }
            catch
            {
                return "login error";
            }
        }


        public string DailyMotionPost(string type)
        {
            try
            {
                return Posting(type, "DailyMotion");   
            }           
            catch
            {
                return "login error";
            }
        }

        public string ScribdPost(string type)
        {
            try
            {
                return Posting(type, "Scribd");
            }
            catch
            {
                return "login error";
            }
        }
        public ActionResult Verification(Guid id)
        {

           
            BasicAccountViewModel basicData = _basic.GetBasicAccountInfo(id);
            if (basicData.isActive == true)
            {
                ViewBag.Message = "you are already verified";
            }
            else
            {
                bool result = _basic.updatbasicaccount(id);
                if (result)
                {
                    ViewBag.Message = "you account has been verified";
                }
                else
                {
                    ViewBag.Message = "there is a problem with your account, contact support@aphidbyte.com";
                }
            }
            return View();



        }
        public ActionResult Resend(string email)
        {

            if (email != null)
            {
              
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                BasicAccountViewModel basicData = _basic.GetBasicAccountInfo(userID);
                if ((basicData.isActive == null) || (basicData.isActive == false))
                {
                    Guid token = Guid.NewGuid();
                    Email mail = new Email();

                    mail.sendMaill(basicData.BasicUserID, email, "Basic", token, "", "VerifyEmail");
                }
            }
            return RedirectToAction("basicaccountinfo", "Basic");
        }



        public string Posting(string type, string postingType)
        {
            string result = string.Empty;
            Guid id = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            string tit = linktitle;
            string pat = linkpath;
            string filesize = Session["FileSize"].ToString();
            if ((tit == "") || (pat == ""))
            {
                throw (new Exception("link error"));
            }
            switch (postingType)
            {
                case "Facebook":
                {
                    var face = new FaceBookModel();
                    string[] dat = new string[] { linkpath, linktitle };
                    result = face.PostTowall(id, "Facebook", dat, type, Session["Trackid"].ToString(), filesize);
                    if (result == "deleted")
                    {
                        Session["fb_status"] = null;
                        return "Index";
                    }
                }
                    break;
                case "YouTube":
                {
                    var tube = new YouTubeModel();                
                    string[] dat = new string[] { linkpath.ToString(), linktitle.ToString(), linktag.ToString() };
                    result = tube.Youtube_post(id, "YouTube", dat, type, Session["Trackid"].ToString(), filesize);
                    if (result == "deleted")
                    {
                        Session["youtube_status"] = null;
                        return "Index";
                    }
                }
                    break;
                case "SoundCloud":
                {
                    var link = new SoundCloudModel();                
                    string[] dat = new string[] { linkpath, linktitle };
                    result = link.POST(id, "SoundCloud", dat, type, Session["Trackid"].ToString(), filesize);
                    if (result == "deleted")
                    {
                        Session["sound_status"] = null;
                        return "Index";
                    }
                }
                    break;
                case "LinkedLin":
                {
                    var link = new LinkedLinModel();
                    string[] dat = new string[] { linkpath, linktitle };
                    result = link.Post_to_link(id, "LinkedLin", dat, type, Session["Trackid"].ToString(), filesize);
                    if (result == "deleted")
                    {
                        Session["link_status"] = null;
                        return "Index";
                    }
                }
                    break;
                case "Twitter":
                {
                    var face = new TwitterModel();
                    string[] dat = new string[] { linkpath, linktitle };
                    result = face.Post_on_Twitter(id, "Twitter", dat, type, Session["Trackid"].ToString(), filesize);
                    if (result == "deleted")
                    {
                        Session["tw_status"] = null;
                        return "Index";
                    }
                }
                    break;
                case "Flicker":
                {
                    var flick = new FlickerModel();
                    string[] dat = new string[] { linkpath, linktitle, linktag };
                    result = flick.Post_on_Flicker(id, "Flicker", dat, type, Session["Trackid"].ToString(), filesize);
                    if (result == "deleted")
                    {
                        Session["flicker_status"] = null;
                        return "Index";
                    }
                }
                    break;
                case "DailyMotion":
                {
                    var daily = new Dailymotion();                
                    string[] dat = new string[] { linkpath, linktitle, linktag };
                    result = daily.post(id, "DailyMotion", dat, type, Session["Trackid"].ToString(), filesize);
                    if (result == "deleted")
                    {
                        Session["daily_status"] = null;
                        return "Index";
                    }                
                }
                    break;
                case "Scribd":
                {
                    var daily = new ScribdModel();
                    string[] dat = new string[] { linkpath, linktitle };
                    result = daily.Scribd_post(id, "Scribd", dat, type, Session["Trackid"].ToString(), filesize);
                    if (result == "deleted")
                    {
                        Session["scribd_status"] = null;
                        return "Index";
                    }
                }
                    break;
            }
            switch (result)
            {
                case "inserted":
                    return "Successfull";
                case "Setup":
                    return "Index";
                case "Timedout":
                    return "Error";
            }
            return result;
        }


        public ActionResult GetAudio(string key, string value)
        {
            try
            {
                if (value != null)
                {
                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    IEnumerable<AudioFileModel> audiofile = _cmn.GetAudioFiles(userID, 2).ToList();
                    var dadd = audiofile.ToList();
                    string mp = dadd[0].AudioFile;
                    using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + value + "")))
                    {
                        byte[] audio = System.IO.File.ReadAllBytes(mp);
                        fs.Write(audio, 0, audio.Length);
                    }
                    ViewBag.SoundPath = "/TempBasicImages/" + value + "";
                    return Json(new { success = true });
                }
                return View();

            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        public ActionResult GetImagePath(string ImgName)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string path = _basic.GetImagePath(userID, ImgName);
                return Json(path);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult GetAudioPath(string Name)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string path = _basic.GetAudioPath(userID, Name);
                return Json(path);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

      



        public ActionResult Success()
        {
            return View();
        }

              
        public ActionResult socialposting()
        {
            return View();
        }

        public ActionResult MusicPosting()
        {
            try
            {
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid aphidId = new Guid(session);
                string count = _cmn.GetNewCount(aphidId);
                var model = new SocialNetworkModel {NewCount = count};
                var list = new List<SocialNetworkModel>();
                list = _basic.SocialNetworkCat(aphidId);
                if (list.Count != 0)
                {
                    ViewBag.Social = list;
                    return View(model);
                }
                return RedirectToAction("Index", "SocialNetworks");
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult PhotoArtposting()
        {
            try
            {
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid aphidId = new Guid(session);
                string count = _cmn.GetNewCount(aphidId);
                var model = new SocialNetworkModel {NewCount = count};
                var list = new List<SocialNetworkModel>();
                list = _basic.SocialNetworkCat(aphidId);
                if (list.Count != 0)
                {
                    ViewBag.Social = list;
                    return View(model);
                }
                return RedirectToAction("Index", "SocialNetworks");
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }


        public ActionResult UpgardeToPremium()
        {
            try
            {
                Guid userId = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                bool result = _basic.UpgradeAccount(userId);
                if (result == true)
                {
                    return Json("Success");
                }
                return Json("Failed");
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public CaptchaModel ShowCaptchaImage()
        {
            return new CaptchaModel();
        }
        public ActionResult DataPlanLimit()
        {
            return View();
        }

        public ActionResult GenerateClones()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new MessageModel {NewCount = Convert.ToInt32(count)};
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult Byteyourmusic()
        {
            ViewBag.Sucess = false;
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new MessageModel {NewCount = Convert.ToInt32(count)};
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult Byteyourmusicalbum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Byteyourmusicalbum(BasicGenerateCloneModel model)
        {
            return View();
        }


        public ActionResult Byteyourmusicsingle()
        {
            ViewBag.Sucess = false;
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new BasicGenerateCloneModel {NewCount = count};
                DropBindAudio();
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        [HttpPost]
        public ActionResult Byteyourvideo(BasicGenerateCloneModel model)
        {
            ViewBag.Message = null;
            try
            {
                trackNo = RandomPassword.CreatePassword(7);
                if (model.Isvalid == true)
                {
                    var captchatext = HttpContext.Session["captchastring"].ToString();
                    if (model.Captcha == captchatext)
                    {
                        long len = 0, imgLength = 0;
                        Guid userId = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                        if (videobyte != null)
                        {
                            len = videobyte.Length;
                        }
                        if (imagepath != null)
                        {

                            imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                        }

                        long length = len + imgLength;
                        bool result = _cmn.CheckSpace(userId, length);
                        if (result == true)
                        {
                            model.UserID = userId;
                            model.CloneID = Guid.NewGuid();
                            model.TrackingNumber = trackNo;
                            if (videopath != "")
                            {
                                model.VideoFile = videopath;
                                model.VideoPath = model.VideoFile;
                                // videopath = null;
                            }
                            if (imagepath != null)
                            {
                                model.MatrixImageBytePath = imagepath;
                                imagepath = null;
                            }
                            var intModel = new InterruptedFileModel();
                            intModel.CloneId = Guid.NewGuid();
                            intModel.CreateDate = DateTime.Now;
                            //intModel.FileArray = songbyte;
                            if (intModel.FileName == null)
                            {
                                intModel.FileName = videopath.Substring(videopath.IndexOf("_") + 1);
                            }
                            //else
                            //{
                            //    intModel.FileName = null;
                            //}

                            intModel.IsActive = true;
                            intModel.ModifiedDate = System.DateTime.Now;
                            intModel.UserId = userId;
                            intModel.CatId = 2;
                            intModel.TrackNumber = trackNo;
                            if (videopath != null)
                            {
                                intModel.VideoPath = videopath;
                                intModel.InterruptedFilePath = videopath;
                                videopath = null;
                            }

                            var post = new CreateLinkPostModel();
                            if (videobyte != null)
                            {
                                post.FileSize = CalculateFileSize.Size(videobyte);
                            }
                            string no = RandomPassword.CreatePassword(8);
                            model.CatId = 2;
                            post.Category = "Video";
                            post.Channel = "Matrix";
                            post.Date = System.DateTime.Now;
                            post.Downloads = 0;
                            post.NoOfChannel = 0;
                            post.Title = model.Title;
                            post.TrackingNumber = trackNo;
                            post.Views = 0;
                            post.UserID = userId;
                            post.MatrixImagePath = model.MatrixImageBytePath;
                            var alldata = ConvertBasictoAllGenerateCloneModel(model, userId, intModel, model.CatId, model.Producer);
                            //  model.VideoPath = model.VideoFile;
                            model.TotalLength = length.ToString();
                            bool re = _basic.InsertBasicByteYourVideo(model, intModel, post, alldata);
                            if (re == true)
                            {
                                bool res = _cmn.UpdateDataMemory(userId, length);
                            }
                            DropBind();
                            string count = _cmn.GetNewCount(userId);

                            model.NewCount = count;
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Basic");

                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Captcha";
                        ViewBag.backpage = "/Basic/Byteyourvideo";
                        return View("GenralErrorPage");
                    }
                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            videopath = null;
            imagepath = null;
            videobyte = null;
            ViewBag.Sucess = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Byteyourmusicsingle(BasicGenerateCloneModel model)
        {
            try
            {
                //TODO: captcha model
                var savedCaptcha = Session["captchastring"].ToString();
                if (model.Captcha != savedCaptcha)
                {
                    ViewBag.Message = "Invalid Captcha";
                    ViewBag.backpage = "/Basic/Single";
                    return View("GenralErrorPage");
                }

                if (!ModelState.IsValid)
                {
                    model.Validation.FillFromModelState(ModelState);
                    return View(model);
                } 

                var trackNo = RandomPassword.CreatePassword(7);
                var userId = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var length = model.MatrixImage.ContentLength + model.Audio.ContentLength;
                var result = _cmn.CheckSpace(userId, length);
                if (!result)
                {
                    return RedirectToAction("DataPlanLimit", "Basic");
                }


                try
                {
                    var intModel = new InterruptedFileModel();
                    model.UserID = userId;
                    model.CloneID = Guid.NewGuid();
                    if (model.SongName == null)
                    {
                        songname = model.Title;
                        model.SongName = model.Title;// songname.Substring(songname.IndexOf("_", StringComparison.Ordinal) + 1);
                    }
                              
                    model.Type = "Single";
                    model.TrackingNumber = trackNo;
                    if (songpath != null)
                    {
                        model.UploadAudioPath = songpath;
                        intModel.VideoPath = songpath;
                    }
                    if (imagepath != null)
                    {
                        model.MatrixImageBytePath = imagepath;
                    }

                    intModel.CloneId = model.CloneID;
                    intModel.CreateDate = DateTime.Now;
                    if (gg != null)
                    {
                        intModel.InterruptedFilePath = IntrepputedAudioPath;
                    }
                    else
                    {
                        intModel.VideoPath = songname;
                    }
                    intModel.FileName = model.SongName;
                    intModel.IsActive = true;
                    intModel.ModifiedDate = System.DateTime.Now;
                    intModel.UserId = userId;
                    intModel.TrackNumber = trackNo;
                    intModel.CatId = 1;

                    var post = new CreateLinkPostModel();
                    if (gg != null)
                    {
                        post.FileSize = CalculateFileSize.Size(gg);

                    }
                    else
                    {
                        if (songname != null)
                        {
                            var ms=new MemoryStream();
                            model.Audio.InputStream.CopyTo(ms);// System.IO.File.ReadAllBytes(Server.MapPath(songname));
                            byte[] by = ms.ToArray();
                            post.FileSize = CalculateFileSize.Size(by);
                        }
                        else
                        {
                            post.FileSize = null;
                        }
                    }
                    post.Category = "Music";//byteType
                    post.Channel = "Matrix";
                    post.Date = DateTime.Now;
                    post.Downloads = 0;
                    post.NoOfChannel = 0;
                    post.Title = model.Title;
                    post.TrackingNumber = trackNo;
                    post.Views = 0;
                    post.UserID = userId;
                    post.MatrixImagePath = imagepath;
                    model.CatId = 1;
                    var alldata = ConvertBasictoAllGenerateCloneModel(model, userId, intModel, model.CatId, model.Producer);
                    model.TotalLength = length.ToString();
                    if (UploadSingleSong(model))
                    {

                        bool re = _basic.InsertSingleMusic(model, intModel, post, alldata);
                        if (re)
                        {
                            bool res = _cmn.UpdateDataMemory(userId, length);
                        }
                        DropBindAudio();
                        string count = _cmn.GetNewCount(userId);

                        model.NewCount = count;
                        TempData["FileName"] = post.Title;
                        return Redirect("UploadConfirmation");
                    }
                    else
                    {
                        //discount space
                    }
                }

                catch (Exception ex)
                {
                    return RedirectToAction("LoginUser", "Accounts");

                }

                DropBindAudio();
                songname = null;
                imagepath = null;
                ViewBag.Sucess = true;
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        private AllGenerateCloneModel ConvertBasictoAllGenerateCloneModel(BasicGenerateCloneModel model, Guid userId,
            InterruptedFileModel intModel, int catId, string rarFilePath)
        {
            var alldata = new AllGenerateCloneModel
            {
                UserID = userId,
                CloneId = model.CloneID,
                Title = model.Title,
                AlbumTitle = model.AlbumTitle,
                Tag = model.Tags,
                ArtistName = model.ArtistName,
                UploadFilePath = intModel.InterruptedFilePath,
                UploadImageFilePath = model.UploadImagePath,
                AudioFilePath = model.UploadAudioPath,
                MatrixFilePath = model.MatrixImageBytePath,
                ComposerName = model.Composer,
                Producer = model.Producer,
                Publisher = model.Publisher,
                InteruptionStyle = model.InterruptionStyle,
                AvailableForDownload = model.AvailableDownload,
                ExplicitContent = model.ExplicitContent,
                UploadPDFFilePath = model.PdfFilePath,
                PagePercentage = model.PagePercentage,
                Type = model.Type,
                FileNames = intModel.FileName,
                VideoFilePath = model.VideoFile,
                WaterMarkMatrixImagePath = model.WatermarkMatrixImagePath,
                WaterMarkMatrixImageText = model.WatermarkMatrixImageText,
                VideoCategory = model.VideoCategory,
                RARFilePath = model.Producer,
                MatrixImagePath = model.MatrixImageBytePath,
                CreatotName = model.CreatorName,
                TrackingNumber = model.TrackingNumber,
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                IsActive = true,
                CatID = catId,
                Audio2FilePath=model.UploadAudio2Path,
                
                GenCloneID = 1,
                SelectedInteruptionFile = model.SelectedIntFile
            };
            return alldata;
        }
        [HttpPost]
        public ActionResult Album(BasicGenerateCloneModel model)
        {
            try
            {
                ViewBag.Message = null;
                
                    trackNo = RandomPassword.CreatePassword(7);
                    string IntPath = AudioIntrepption(model.InterruptionStyle, model.SelectedIntFile, songpath, songpath2);
                   
                        long len = 0, imgLength = 0, intpath = 0;
                        Guid userId = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                        if (gg != null)
                        {
                            len = gg.Length;
                        }
                        if (imagepath != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                        }
                        if (!string.IsNullOrEmpty(IntPath))
                        {
                            intpath = System.IO.File.ReadAllBytes(Server.MapPath(IntPath)).Length;
                        }

                        long length = len + imgLength + intpath;

                        bool result = _cmn.CheckSpace(userId, length);
                        if (result)
                        {

                            try
                            {
                                model.TotalLength = length.ToString();
                                model.UserID = userId;
                                model.CloneID = Guid.NewGuid();
                                model.SongName = songname.Substring(songname.IndexOf("_", StringComparison.Ordinal) + 1);
                                model.SongName2 = songname.Substring(songname2.IndexOf("_", StringComparison.Ordinal) + 1);

                                model.Type = "Album";

                                model.TrackingNumber = trackNo;
                                if (songpath != null)
                                {
                                    model.UploadAudioPath = songpath;
                                }
                                 if (songpath2 != null)
                                {
                                    model.UploadAudio2Path = songpath2;
                                }
                                if (imagepath != null)
                                {
                                    model.MatrixImageBytePath = imagepath;
                                }
                                var intModel = new InterruptedFileModel
                                {
                                    CloneId = Guid.NewGuid(),
                                    CreateDate = DateTime.Now
                                };
                                if (gg != null)
                                {
                                    intModel.InterruptedFilePath = IntrepputedAudioPath;
                                   

                                }
                                else
                                {
                                    intModel.VideoPath = songname;
                                }
                                intModel.FileName = model.SongName;
                                intModel.FileName2 = model.SongName2;
                                
                                intModel.IsActive = true;
                                intModel.ModifiedDate = DateTime.Now;
                                intModel.UserId = userId;
                                intModel.TrackNumber = trackNo;
                                var post = new CreateLinkPostModel();
                                if (gg != null)
                                {
                                    post.FileSize = CalculateFileSize.Size(gg);

                                }
                                else
                                {
                                    byte[] by = System.IO.File.ReadAllBytes(Server.MapPath(songname));
                                    post.FileSize = CalculateFileSize.Size(by);
                                }
                                post.Category = "Music";
                                post.Channel = "Matrix";
                                post.Date = DateTime.Now;
                                post.Downloads = 0;
                                post.NoOfChannel = 0;
                                post.Title = model.Title;
                                post.TrackingNumber = trackNo;
                                post.Views = 0;
                                post.UserID = userId;
                                post.MatrixImagePath = imagepath;
                                var alldata = ConvertBasictoAllGenerateCloneModel(model, userId, intModel, 1, model.Producer);
                                tvar1.Add(model);
                                tvar2.Add(intModel);
                                tvar3.Add(post);
                                tvar4.Add(alldata);
                                model.Isvalid = false;
                                bool re = _basic.InsertSingleMusic(model, intModel, post, alldata);
                                if (re == true)
                                {
                                    bool res = _cmn.UpdateDataMemory(userId, length);
                                }
                                DropBindAudio();
                                string count = _cmn.GetNewCount(userId);
                                model.NewCount = count;
                            }
                            catch (Exception)
                            {
                                return RedirectToAction("LoginUser", "Accounts");
                            }
                        }

                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Basic");
                            //Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>");
                        }
                    }
                   
                
                //DropBindAudio();
               
            
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            return RedirectToAction("Byteyourmusicalbum", "basic");
        }

        [HttpPost]
        public ActionResult SingleUpdate(BasicGenerateCloneModel model)
        {
            try
            {
                ViewBag.Message = null;
                string IntPath = null;
                if (model.Isvalid == true)
                {
                    if (model.SelectedIntFile != "No Interruprtion")
                    {
                        if (songpath != null)
                        {
                            IntPath = AudioIntrepption(model.InterruptionStyle, model.SelectedIntFile, songpath, songpath2);
                        }
                        else
                        {
                            IntPath = AudioIntrepption(model.InterruptionStyle, model.SelectedIntFile, model.UploadAudioPath, model.UploadAudio2Path);
                        }
                    }
                    string cap = Session["captchastring"].ToString();
                    if (model.Captcha == cap)
                    {
                        long len = 0, imgLength = 0, intpath = 0;
                        Guid userId = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                        len = songpath != null ? System.IO.File.ReadAllBytes(Server.MapPath(songpath)).Length : System.IO.File.ReadAllBytes(Server.MapPath(model.UploadAudioPath)).Length;
                        imgLength = imagepath != null ? System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length : System.IO.File.ReadAllBytes(Server.MapPath(model.MatrixImageBytePath)).Length;
                        if (!string.IsNullOrEmpty(IntPath))
                        {
                            intpath = System.IO.File.ReadAllBytes(Server.MapPath(IntPath)).Length;
                        }

                        long length = len + imgLength + intpath;
                        try
                        {
                            model.UserID = userId;
                            model.SongName = songname != null ? songname.Substring(songname.IndexOf("_", StringComparison.Ordinal) + 1) : model.UploadAudioPath.Substring(model.UploadAudioPath.IndexOf("_", StringComparison.Ordinal) + 1);
                            model.Type = "Single";
                            model.CatId = 1;
                            model.UploadAudioPath = songpath != null ? songpath : model.UploadAudioPath;
                            model.MatrixImageBytePath = imagepath != null ? imagepath : model.MatrixImageBytePath = model.MatrixImageBytePath;
                            var intModel = new InterruptedFileModel
                            {
                                CreateDate = System.DateTime.Now,
                                FileName = model.SongName,
                                IsActive = true,
                                ModifiedDate = System.DateTime.Now,
                                UserId = userId,
                                TrackNumber = model.TrackingNumber,
                                VideoPath = songname != null ? songname : model.UploadAudioPath
                            };
                            intModel.InterruptedFilePath = IntPath != null ? IntPath : intModel.InterruptedFilePath = model.UploadAudioPath;
                            var alldata = ConvertBasictoAllGenerateCloneModel(model, userId, intModel, model.CatId, null);
                            if (songpath != null)
                            {
                                byte[] by = System.IO.File.ReadAllBytes(Server.MapPath(songpath));
                                alldata.filesize = CalculateFileSize.Size(by);

                            }
                            else
                            {
                                byte[] by = System.IO.File.ReadAllBytes(Server.MapPath(model.UploadAudioPath));
                                alldata.filesize = CalculateFileSize.Size(by);
                            }

                            model.Modified_Time = DateTime.Now;

                            bool re, result1;
                            result1 = _cmn.CheckEditStatus(intModel.UserId, length, model.TotalLength);
                            string count = _cmn.GetNewCount(userId);
                            model.NewCount = count;
                            if (result1)
                            {
                                model.TotalLength = length.ToString();
                                re = _basic.UpdateClone(model, intModel, alldata);
                            }
                            else
                            {
                                return RedirectToAction("DataPlanLimit", "Basic");
                            }
                            songpath = null;
                            imagepath = null;
                            return RedirectToAction("LinkPost", "Basic");
                        }

                        catch (Exception)
                        {
                            return RedirectToAction("LoginUser", "Accounts");

                        }
                    }
                    return RedirectToAction("DataPlanLimit", "Basic");

                    //Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>");
                }
                else
                {
                    ViewBag.Message = "Invalid Captcha";
                    ViewBag.backpage = "/Basic/Byteyourmusicsingle";
                    return View("GenralErrorPage");
                }
                return RedirectToAction("LoginUser", "Accounts");
            }
            catch
            {
                return RedirectToAction("LinkPost", "Basic");
            }
        }

        [HttpPost]
        public ActionResult Album2(BasicGenerateCloneModel model)
        {
            try
            {
                ViewBag.Message = null;
                if (model.Isvalid == true)
                {
                    trackNo = RandomPassword.CreatePassword(7);
                    string IntPath = AudioIntrepption(model.InterruptionStyle, model.SelectedIntFile, songpath, songpath2);
                    string cap = Session["captchastring"].ToString();
                    if (model.Captcha == cap)
                    {
                        long len = 0, imgLength = 0, intpath = 0;
                        Guid userId = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                        if (gg != null)
                        {
                            len = gg.Length;
                        }
                        if (imagepath != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                        }
                        if (!string.IsNullOrEmpty(IntPath))
                        {
                            intpath = System.IO.File.ReadAllBytes(Server.MapPath(IntPath)).Length;
                        }

                        long length = len + imgLength + intpath;

                        bool result = _cmn.CheckSpace(userId, length);
                        if (result)
                        {

                            try
                            {
                                model.TotalLength = length.ToString();
                                model.UserID = userId;
                                model.CloneID = Guid.NewGuid();
                                model.SongName = songname.Substring(songname.IndexOf("_", StringComparison.Ordinal) + 1);
                                model.Type = "Album";

                                model.TrackingNumber = trackNo;
                                if (songpath != null)
                                {
                                    model.UploadAudioPath = songpath;
                                }
                                if (imagepath != null)
                                {
                                    model.MatrixImageBytePath = imagepath;
                                }
                                var intModel = new InterruptedFileModel
                                {
                                    CloneId = Guid.NewGuid(),
                                    CreateDate = DateTime.Now
                                };
                                if (gg != null)
                                {
                                    intModel.InterruptedFilePath = IntrepputedAudioPath;
                                }
                                else
                                {
                                    intModel.VideoPath = songname;
                                }
                                intModel.FileName = model.SongName;
                                intModel.IsActive = true;
                                intModel.ModifiedDate = DateTime.Now;
                                intModel.UserId = userId;
                                intModel.TrackNumber = trackNo;
                                var post = new CreateLinkPostModel();
                                if (gg != null)
                                {
                                    post.FileSize = CalculateFileSize.Size(gg);

                                }
                                else
                                {
                                    byte[] by = System.IO.File.ReadAllBytes(Server.MapPath(songname));
                                    post.FileSize = CalculateFileSize.Size(by);
                                }
                                post.Category = "Music";
                                post.Channel = "Matrix";
                                post.Date = DateTime.Now;
                                post.Downloads = 0;
                                post.NoOfChannel = 0;
                                post.Title = model.Title;
                                post.TrackingNumber = trackNo;
                                post.Views = 0;
                                post.UserID = userId;
                                post.MatrixImagePath = imagepath;
                                var alldata = ConvertBasictoAllGenerateCloneModel(model, userId, intModel, 1, model.Producer);
                                tvar1.Add(model);
                                tvar2.Add(intModel);
                                tvar3.Add(post);
                                tvar4.Add(alldata);
                                //model.Isvalid = false;
                                //bool re = basic.InsertSingleMusic(model, intModel, post, Alldata);
                                //if (re == true)
                                //{
                                //    bool res = common.UpdateDataMemory(userID, length);
                                //}
                                //DropBindAudio();
                                string count = _cmn.GetNewCount(userId);
                                model.NewCount = count;
                            }
                            catch (Exception)
                            {
                                return RedirectToAction("LoginUser", "Accounts");
                            }
                        }

                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Basic");
                            //Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Captcha";
                        ViewBag.backpage = "/Basic/Byteyourmusicsingle";
                        return View("GenralErrorPage");
                    }
                }
                //DropBindAudio();
                ViewBag.albumcount = tvar1.Count();
           
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        [HttpPost]
        //public ActionResult SingleUpdate(BasicGenerateCloneModel model)
        //{
        //    try
        //    {
        //        ViewBag.Message = null;
        //        string IntPath = null;
        //        if (model.Isvalid == true)
        //        {
        //            if (model.SelectedIntFile != "No Interruprtion")
        //            {
        //                if (songpath != null)
        //                {
        //                    IntPath = AudioIntrepption(model.InterruptionStyle, model.SelectedIntFile, songpath, songpath2);
        //                }
        //                else
        //                {
        //                    IntPath = AudioIntrepption(model.InterruptionStyle, model.SelectedIntFile, model.UploadAudioPath,model.UploadAudio2Path);
        //                }
        //            }
        //            string cap = Session["captchastring"].ToString();
        //            if (model.Captcha == cap)
        //            {
        //                long len = 0, imgLength = 0, intpath = 0;
        //                Guid userId = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

        //                len = songpath != null ? System.IO.File.ReadAllBytes(Server.MapPath(songpath)).Length : System.IO.File.ReadAllBytes(Server.MapPath(model.UploadAudioPath)).Length;
        //                imgLength = imagepath != null ? System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length : System.IO.File.ReadAllBytes(Server.MapPath(model.MatrixImageBytePath)).Length;
        //                if (!string.IsNullOrEmpty(IntPath))
        //                {
        //                    intpath = System.IO.File.ReadAllBytes(Server.MapPath(IntPath)).Length;
        //                }

        //                long length = len + imgLength + intpath;
        //                try
        //                {
        //                    model.UserID = userId;
        //                    model.SongName = songname != null ? songname.Substring(songname.IndexOf("_", StringComparison.Ordinal) + 1) : model.UploadAudioPath.Substring(model.UploadAudioPath.IndexOf("_", StringComparison.Ordinal) + 1);
        //                    model.Type = "Single";
        //                    model.CatId = 1;
        //                    model.UploadAudioPath = songpath != null ?  songpath : model.UploadAudioPath;                            
        //                    model.MatrixImageBytePath = imagepath != null ? imagepath : model.MatrixImageBytePath = model.MatrixImageBytePath;
        //                    var intModel = new InterruptedFileModel
        //                    {
        //                        CreateDate = System.DateTime.Now,
        //                        FileName = model.SongName,
        //                        IsActive = true,
        //                        ModifiedDate = System.DateTime.Now,
        //                        UserId = userId,
        //                        TrackNumber = model.TrackingNumber,
        //                        VideoPath = songname != null ? songname : model.UploadAudioPath
        //                    };
        //                    intModel.InterruptedFilePath = IntPath != null ? IntPath : intModel.InterruptedFilePath = model.UploadAudioPath;
        //                    var alldata = ConvertBasictoAllGenerateCloneModel(model, userId, intModel, model.CatId, null);
        //                    if (songpath != null)
        //                    {
        //                        byte[] by = System.IO.File.ReadAllBytes(Server.MapPath(songpath));
        //                        alldata.filesize = CalculateFileSize.Size(by);

        //                    }
        //                    else
        //                    {
        //                        byte[] by = System.IO.File.ReadAllBytes(Server.MapPath(model.UploadAudioPath));
        //                        alldata.filesize = CalculateFileSize.Size(by);
        //                    }

        //                    model.Modified_Time = DateTime.Now;

        //                    bool re, result1;
        //                    result1 = _cmn.CheckEditStatus(intModel.UserId, length, model.TotalLength);
        //                    string count = _cmn.GetNewCount(userId);
        //                    model.NewCount = count;
        //                    if (result1)
        //                    {
        //                        model.TotalLength = length.ToString();
        //                        re = _basic.UpdateClone(model, intModel, alldata);
        //                    }
        //                    else
        //                    {
        //                        return RedirectToAction("DataPlanLimit", "Basic");
        //                    }
        //                    songpath = null;
        //                    imagepath = null;
        //                    return RedirectToAction("LinkPost", "Basic");
        //                }

        //                catch (Exception)
        //                {
        //                    return RedirectToAction("LoginUser", "Accounts");

        //                }
        //            }
        //            return RedirectToAction("DataPlanLimit", "Basic");

        //            //Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Invalid Captcha";
        //            ViewBag.backpage = "/Basic/Byteyourmusicsingle";
        //            return View("GenralErrorPage");
        //        }
        //        return RedirectToAction("LoginUser", "Accounts");
        //    }
        //    catch
        //    {
        //        return RedirectToAction("LinkPost", "Basic");
        //    }
        //}

        public ActionResult GetCaptcha()
        {
            try
            {
                var da = "";
                if (Session["captchastring"] != null)
                {
                    da = Session["captchastring"].ToString();
                }
                //return Json(new { name = da }, JsonRequestBehavior.AllowGet);
                return Json(da);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult Album()
        {
         
            if (tvar1.Count < 20)
            {
                ViewBag.albumcount = tvar1.Count();
                return View();
            }
            ViewBag.Message = "Max number songs reached ...";
            ViewBag.backpage = "/Basic/Album";
            return View("GenralErrorPage");
        }
        public ActionResult Byteyourvideo()
        {
            ViewBag.Sucess = false;
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new BasicGenerateCloneModel {NewCount = count};
                DropBind();
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }
        public ActionResult ByteyourEbook()
        {
            ViewBag.Sucess = false;
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new BasicGenerateCloneModel {NewCount = count};
                DropBind();
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult ByteyourArtPhotography()
        {
            ViewBag.sucess = false;
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new BasicGenerateCloneModel {NewCount = count};
                DropBind();
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public List<BindDropDown> DropBind()
        {
            var li = new List<BindDropDown>();
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            li = _basic.BindDropImage(userID);
            var list = li.Select(item => new SelectListItem
            {
                Text = item.Value, Value = item.id.ToString()
            }).ToList();

            ViewBag.CityList = list;
            return li;
        }

        [HttpPost]
        public ActionResult GetText()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
            }
            return View();
        }



        [HttpPost]
        public ContentResult UploadVideo()
        {
            HttpPostedFileBase hpf = null;
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null)
                {
                    var ConvertedPath = ConverttoMp4(hpf.FileName, hpf);
                    System.IO.File.Delete(Server.MapPath("/OriginalVideo/" + hpf.FileName));
                    string savedFileName = "/TempBasicImages/" + ConvertedPath.ToString();
                    var aa = System.IO.File.Exists(Server.MapPath(savedFileName));
                    if (aa == true)
                    {
                    }
                    videopath = savedFileName;
                    videobyte = System.IO.File.ReadAllBytes(Server.MapPath(savedFileName));
                }
            }
            return Content("{\"name\":\"" + hpf.FileName + "$" + videopath + "\"}");

        }

        public string ConverttoMp4(string fileName, HttpPostedFileBase hpf)
        {
            string html = string.Empty;
            //rename if file already exists
            string guid = Guid.NewGuid().ToString();
            int j = 0;
            string appPath = Request.PhysicalApplicationPath;
            //Get the application path
            string inputPath = appPath + "OriginalVideo";
            //Path of the original file
            string outputPath = appPath + "TempBasicImages";
            //Path of the converted file
            string imgpath = appPath + "Thumbs";
            //Path of the preview file
            string filepath = Server.MapPath("/OriginalVideo/" + fileName);
            while (System.IO.File.Exists(filepath))
            {
                j = j + 1;
                int dotPos = fileName.LastIndexOf(".", StringComparison.Ordinal);
                string namewithoutext = fileName.Substring(0, dotPos);
                string ext = fileName.Substring(dotPos + 1);
                fileName = namewithoutext + j + "." + ext;
                filepath = Server.MapPath("/OriginalVideo/" + fileName);
            }
            try
            {

                hpf.SaveAs(filepath);
            }
            catch
            {
                return "false";
            }
            string outPutFile;
            outPutFile = "~/OriginalVideo/" + fileName;
            int i = hpf.ContentLength;
            var a = new FileInfo(Server.MapPath(outPutFile));
            while (a.Exists == false)
            {

            }
            long b = a.Length;
            while (i != b)
            {

            }
            string cmd = " -i \"" + inputPath + "\\" + fileName + "\" \"" + outputPath + "\\" + guid + "_" + fileName.Remove(fileName.IndexOf(".", StringComparison.Ordinal)) + ".mp4" + "\"";
            ConvertNow(cmd);
            string imgargs = " -i \"" + outputPath + "\\" + fileName.Remove(fileName.IndexOf(".", StringComparison.Ordinal)) + ".flv" + "\" -f image2 -ss 1 -vframes 1 -s 280x200 -an \"" + imgpath + "\\" + fileName.Remove(fileName.IndexOf(".", StringComparison.Ordinal)) + ".jpg" + "\"";
            ConvertNow(imgargs);

            string filepathconverted = guid + "_" + fileName.Remove(fileName.IndexOf(".")) + ".mp4";
            return filepathconverted;
        }

        private void ConvertNow(string cmd)
        {            
            string AppPath = Request.PhysicalApplicationPath;
            //Get the application path
            string exepath = AppPath + "ffmpeg.exe";
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = exepath;
            //Path of exe that will be executed, only for "filebuffer" it will be "flvtool2.exe"
            proc.StartInfo.Arguments = cmd;
            //The command which will be executed
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.Start();
            while (proc.HasExited == false)
            {

            }
        }

        [HttpPost]
        public ContentResult VideoMatrixImage()
        {
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null)
                {
                    string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                    hpf.SaveAs(Server.MapPath(savedFileName));
                    imagepath = savedFileName;
                }
            }
            return Content("{\"name\":\"" + imagepath + "\"}");

        }

        public ActionResult InterruptVideo(string IntS, string IntF, string FileName, string Category, string Title)
        {
            try
            {
                Guid userId = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var ob = new VideoInterruptionModel();
                string name = ob.VideoInterruption(userId, IntS, IntF, FileName, Category, Title, session, trackNo);
                return Json(name + "&" + videopath, JsonRequestBehavior.AllowGet);
                //return Json(name, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        [HttpPost]
        public ActionResult Interruption(string IntS, string IntF, string FileName, string FileName2)
        {

            string intrepputedAudioPath = AudioIntrepption(IntS, IntF, FileName, FileName2);
            if (string.IsNullOrEmpty(intrepputedAudioPath))
            {
                return Json(FileName);
            }
            
             return Json(intrepputedAudioPath);
        }
        public string AudioIntrepption(string IntS, string IntF, string FileName, string FileName2)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                Guid guid = Guid.NewGuid();
                songname = FileName;
                songname2=FileName2;
                if (IntS == "Default Randomized Entry" && (IntF == "No Interruption" || IntF == "Default AphidByte" || IntF == "Custom Audio Interruption"))
                {
                    if (IntF == "Default AphidByte")
                    {
                        byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3"));//Upload by User
                        byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
                        var int1 = new List<byte>();
                        var int2 = new List<byte>();
                        var int3 = new List<byte>();

                        for (int i = 0; i < intreAudio.Length; i++)
                        {
                            if (i <= 100000)
                            {
                                int2.Add(intreAudio[i]);
                            }
                        }

                        int2.AddRange(mainAudio);
                        for (Int64 i = 0; i < intreAudio.Length - 1; i++)
                        {
                            if (i >= 100000 && i <= intreAudio.Length)
                            {
                                int3.Add(intreAudio[i - 1]);
                            }
                        }
                        int2.AddRange(int3);

                        gg = int2.ToArray();
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_myfile1.mp3")))
                        {
                            if (gg != null)
                            {
                                fs.Write(gg, 0, gg.Length);
                                IntrepputedAudioPath = @"/TempBasicImages/" + guid + "_myfile1.mp3";
                            }
                        }
                    }

                    if (IntF == "Custom Audio Interruption")
                    {
                        var dd = _basic.GetAudioForInterruption(userID);
                        if (dd.Count != 0)
                        { 
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_" + dd[0].Name)))
                        {
                            if (dd[0].AudioFileName != null)
                            {
                                byte[] audio = System.IO.File.ReadAllBytes(dd[0].AudioFileName);
                                fs.Write(audio, 0, dd[0].AudioFileName.Length);
                            }
                        }
                        byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/TempBasicImages/" + guid + "_" + dd[0].Name));//Upload by User
                        byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
                        var int1 = new List<byte>();
                        var int2 = new List<byte>();
                        var int3 = new List<byte>();
                        byte[] final2;

                        for (int i = 0; i < intreAudio.Length; i++)
                        {

                            if (i <= 100000)
                            {
                                int2.Add(intreAudio[i]);
                            }
                        }

                        int2.AddRange(mainAudio);
                        for (Int64 i = 0; i < intreAudio.Length - 1; i++)
                        {
                            if (i >= 100000 && i <= intreAudio.Length)
                            {
                                int3.Add(intreAudio[i - 1]);
                            }
                        }
                        int2.AddRange(int3);

                        gg = int2.ToArray();
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_myfile1.mp3")))
                        {
                            if (gg != null)
                            {
                                fs.Write(gg, 0, gg.Length);
                                IntrepputedAudioPath = @"/TempBasicImages/" + guid + "_myfile1.mp3";
                            }
                        }
                    }
                   }
                }
                if (IntS == "Producer Tag Sequence" && (IntF == "No Interruption" || IntF == "Default AphidByte" || IntF == "Custom Audio Interruption"))
                {

                    if (IntF == "Default AphidByte")
                    {
                        byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3"));//Upload by User
                        byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
                        var int1 = new List<byte>();
                        var int2 = new List<byte>();
                        var int3 = new List<byte>();
                        var int4 = new List<byte>();
                        for (int i = 0; i < mainAudio.Length; i++)
                        {
                            if (i <= 100000)
                            {
                                int1.Add(mainAudio[i]);
                            }
                        }
                        for (int i = 0; i < intreAudio.Length; i++)
                        {
                            if (i <= 100000)
                            {
                                int2.Add(intreAudio[i]);
                            }
                        }
                        for (int i = 0; i < mainAudio.Length; i++)
                        {
                            if (i >= 100000 && i <= mainAudio.Length)
                            {
                                int3.Add(mainAudio[i]);
                            }
                        }
                        for (int i = 0; i < intreAudio.Length; i++)
                        {
                            if (i >= 10000 && i <= intreAudio.Length)
                            {
                                int4.Add(intreAudio[i]);
                            }
                        }
                        int1.AddRange(int2);
                        int1.AddRange(int3);
                        int1.AddRange(int4);
                        gg = int1.ToArray();
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_myfile1.mp3")))
                        {
                            if (gg != null)
                            {
                                fs.Write(gg, 0, gg.Length);
                                IntrepputedAudioPath = @"/TempBasicImages/" + guid + "_myfile1.mp3";
                            }
                        }
                    }
                    if (IntF == "Custom Audio Interruption")
                    {
                        var dd = _basic.GetAudioForInterruption(userID);
                        if (dd.Count != 0)
                        { 
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_" + dd[0].Name)))
                        {
                            if (dd[0].AudioFileName != null)
                            {
                                byte[] audio = System.IO.File.ReadAllBytes(Server.MapPath(dd[0].AudioFileName));
                                fs.Write(audio, 0, dd[0].AudioFileName.Length);
                            }
                        }
                        byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/TempBasicImages/" + guid + "_" + dd[0].Name));//Upload by User
                        byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
                        var int1 = new List<byte>();
                        var int2 = new List<byte>();
                        var int3 = new List<byte>();
                        var int4 = new List<byte>();
                        for (int i = 0; i < mainAudio.Length; i++)
                        {
                            if (i <= 100000)
                            {
                                int1.Add(mainAudio[i]);
                            }
                        }
                        for (int i = 0; i < intreAudio.Length; i++)
                        {
                            if (i <= 100000)
                            {
                                int2.Add(intreAudio[i]);
                            }
                        }
                        for (int i = 0; i < mainAudio.Length; i++)
                        {
                            if (i >= 100000 && i <= mainAudio.Length)
                            {
                                int3.Add(mainAudio[i]);
                            }
                        }
                        for (int i = 0; i < intreAudio.Length; i++)
                        {
                            if (i >= 10000 && i <= intreAudio.Length)
                            {
                                int4.Add(intreAudio[i]);
                            }
                        }
                        int1.AddRange(int2);
                        int1.AddRange(int3);
                        int1.AddRange(int4);
                        gg = int1.ToArray();
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_myfile1.mp3")))
                        {
                            if (gg != null)
                            {
                                fs.Write(gg, 0, gg.Length);
                                IntrepputedAudioPath = @"/TempBasicImages/" + guid + "_myfile1.mp3";
                            }
                        }
                    }
                   }
                }
                if (IntS == "Beginning of File" && (IntF == "No Interruption" || IntF == "Default AphidByte" || IntF == "Custom Audio Interruption"))
                {
                    if (IntF == "Default AphidByte")
                    {
                        byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3"));//Upload by User
                        byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
                        var int1 = new List<byte>(mainAudio);
                        int1.AddRange(intreAudio);
                        gg = int1.ToArray();
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_myfile1.mp3")))
                        {
                            if (gg != null)
                            {
                                fs.Write(gg, 0, gg.Length);
                                IntrepputedAudioPath = @"/TempBasicImages/" + guid + "_myfile1.mp3";
                            }
                        }
                    }
                    if (IntF == "Custom Audio Interruption")
                    {
                        var dd = _basic.GetAudioForInterruption(userID);
                        if (dd.Count != 0)
                        { 
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_" + dd[0].Name)))
                        {
                            if (dd[0].AudioFileName != null)
                            {
                                byte[] audio = System.IO.File.ReadAllBytes(dd[0].AudioFileName);
                                fs.Write(audio, 0, dd[0].AudioFileName.Length);
                            }

                        }
                        byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/TempBasicImages/" + guid + "_" + dd[0].Name));//Upload by User
                        byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
                        var int1 = new List<byte>(mainAudio);
                        int1.AddRange(intreAudio);
                        gg = int1.ToArray();
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_myfile1.mp3")))
                        {
                            if (gg != null)
                            {
                                fs.Write(gg, 0, gg.Length);
                                IntrepputedAudioPath = @"/TempBasicImages/" + guid + "_myfile1.mp3";
                            }
                        }
                    }
                  }
                }
                if (IntS == "Ending of File" && (IntF == "No Interruption" || IntF == "Default AphidByte" || IntF == "Custom Audio Interruption"))
                {
                    if (IntF == "Default AphidByte")
                    {
                        byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3"));//Upload by User
                        byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
                        var int1 = new List<byte>(intreAudio);
                        int1.AddRange(mainAudio);
                        gg = int1.ToArray();
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_myfile1.mp3")))
                        {
                            if (gg != null)
                            {
                                fs.Write(gg, 0, gg.Length);
                                IntrepputedAudioPath = @"/TempBasicImages/" + guid + "_myfile1.mp3";
                            }
                        }
                    }
                    if (IntF == "Custom Audio Interruption")
                    {
                        var dd = _basic.GetAudioForInterruption(userID);
                        if (dd.Count != 0)
                        { 
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_" + dd[0].Name)))
                        {
                            if (dd[0].AudioFileName != null)
                            {
                                byte[] audio = System.IO.File.ReadAllBytes(dd[0].AudioFileName);
                                fs.Write(audio, 0, dd[0].AudioFileName.Length);
                            }
                        }
                        byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/TempBasicImages/" + guid + "_" + dd[0].Name));//Upload by User
                        byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
                        var int1 = new List<byte>(intreAudio);
                        int1.AddRange(mainAudio);
                        gg = int1.ToArray();
                        using (FileStream fs = System.IO.File.Create(Server.MapPath(@"/TempBasicImages/" + guid + "_myfile1.mp3")))
                        {
                            if (gg != null)
                            {
                                fs.Write(gg, 0, gg.Length);
                                IntrepputedAudioPath = @"/TempBasicImages/" + guid + "_myfile1.mp3";
                            }
                        }
                    }
                    }
                }
                return IntrepputedAudioPath;
            }
            catch
            {
                return "";
            }
        }

        public ActionResult PrievewMatrixImage()
        {
            var mm = "";
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null)
                {
                    mm = hpf.FileName;
                    var split = hpf.FileName.Split('.');
                    if (split[1] == "jpg" || split[1] == "JPG" || split[1] == "png" || split[1] == "PNG")
                    {
                        
                        string savedImageName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                        hpf.SaveAs(Server.MapPath(savedImageName));
                        imagebyte2 = savedImageName;
                        mm = savedImageName;
                    }
                    else
                    {
                        mm = "Invalid";
                    }
                }
            }
            return Content("{\"name\":\"" + mm + "\"}");

        }

        [HttpPost]
        public ContentResult PreviewAudio2()
        {
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null)
                {
                    string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                    //  string savedFileName2 = "/TempBasicImages/" + guid + "_" + hpf.FileName2;
                    hpf.SaveAs(Server.MapPath(savedFileName));
                    // hpf.SaveAs(Server.MapPath(savedFileName2));
                    songpath2 = savedFileName;
                    //  songpath2 = savedFileName2;
                }
            }
            return Content("{\"name\":\"" + songpath2 + "\"}");
        } 
        [HttpPost]
        public ContentResult PreviewAudio()
        {
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null)
                {
                    string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                  //  string savedFileName2 = "/TempBasicImages/" + guid + "_" + hpf.FileName2;
                    hpf.SaveAs(Server.MapPath(savedFileName));
                   // hpf.SaveAs(Server.MapPath(savedFileName2));
                    songpath = savedFileName;
                  //  songpath2 = savedFileName2;
                }
            }
            return Content("{\"name\":\"" + songpath + "\"}");
        }

        [HttpPost]
        public ContentResult pdffile()
        {
            var filename = "";
            Guid guid = Guid.NewGuid();
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null)
                {
                    filename = hpf.FileName;
                    artphototitle = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                    var split = hpf.FileName.Split('.');
                    if (split[1] == "pdf" || split[1] == "PDF")
                    {
                     
                        hpf.SaveAs(Server.MapPath(artphototitle));
                        pdffilepath = artphototitle;
                        
                    }
                    else
                    {

                        filename = "Invalid";
                    }
                }
            }
            return Content("{\"name\":\"" + filename + "$" + artphototitle + "\"}");
        }

        public ActionResult PdfInterruption(string IntF, string FileName, string Percentage, string Image, string Title, string ComposerName, string Default)
        {
            try
            {
                var obvpdf = new PdfIntrreputionModel();
                Guid userId = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                pdfpath = obvpdf.PdfIntreption(artphototitle, ComposerName, Percentage, Title, userId, IntF, imagepath);
                string[] PDF = pdfpath.Split('@');
                string pathpdf = PDF[0];
                string trackno = PDF[1];
                Session["TRACK"] = trackno;
                return Json(pathpdf);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        static string linkpath = "";
        static string linktitle = "";
        static string linktag = "";
        static string linkcategory = "";
        [HttpPost]

        public string selectchanneltoflood1(string trackid, string cat)
        {
            try
            {
                List<AllGenerateCloneModel> li = _basic.GetUploadfile(trackid);
                Session["FileSize"] = li[0].filesize;
                if (li.Count != 0)
                {
                    if ((li[0].CatID == 1) && (cat == "Music"))
                    {
                        linkpath = string.IsNullOrEmpty(li[0].UploadFilePath) ? li[0].AudioFilePath : li[0].UploadFilePath;
                        linktag = li[0].Tag;
                        linktitle = li[0].Title;
                        string displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + linkpath).Split('_')[1];
                        Session["LinkPath"] = displaypath;
                        Session["LinkTitle"] = linktitle;
                        Session["Trackid"] = trackid;
                        return "MusicPosting";
                    }
                    if ((li[0].CatID == 2) && (cat == "Video"))
                    {
                        linkpath = li[0].UploadFilePath == null ? li[0].VideoFilePath : li[0].UploadFilePath;
                        linktag = li[0].Tag;
                        linktitle = li[0].Title;
                        linkcategory = li[0].VideoCategory;
                        string displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + linkpath).Split('_')[1];
                        Session["LinkPath"] = displaypath;
                        Session["LinkTitle"] = linktitle;
                        Session["Trackid"] = trackid;
                        return "selectchanneltoflood";
                    }
                    else if ((li[0].CatID == 3) && (cat == "Photos"))
                    {
                        if (li[0].UploadFilePath == null)
                            linkpath = li[0].UploadImageFilePath;
                        else
                            linkpath = li[0].UploadFilePath;
                        linktag = li[0].Tag;
                        linktitle = li[0].Title;
                        string displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + linkpath).Split('_')[1];
                        Session["LinkPath"] = displaypath;
                        Session["LinkTitle"] = linktitle;
                        Session["Trackid"] = trackid;
                        return "PhotoArtposting";
                    }
                    else if ((li[0].CatID == 4) && (cat == "Pdf"))
                    {
                        if (li[0].UploadFilePath == null)
                            linkpath = li[0].PdfFilePath;
                        else
                            linkpath = li[0].UploadFilePath;
                        linktag = li[0].Tag;
                        linktitle = li[0].Title;
                        string displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + linkpath).Split('_')[1];
                        Session["LinkPath"] = displaypath;
                        Session["LinkTitle"] = linktitle;
                        Session["Trackid"] = trackid;
                        return "PdfEbookPosting";
                    }
                    else
                    {
                        if (li[0].UploadFilePath == null)
                            linkpath = li[0].RARFilePath;
                        else
                            linkpath = li[0].UploadFilePath;
                        linktag = li[0].Tag;
                        linktitle = li[0].Title;
                        string displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + linkpath).Split('_')[1];
                        Session["LinkPath"] = displaypath;
                        Session["LinkTitle"] = linktitle;
                        Session["Trackid"] = trackid;
                        return "FilesPosting";
                    }
                }
                return "LinkPost";
            }
            catch (Exception ex)
            {
                return "LinkPost";
            }
        }

        public ActionResult FilesPosting()
        {
            try
            {
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid Aphid_ID = new Guid(session);
                string count = _cmn.GetNewCount(Aphid_ID);
                var model = new SocialNetworkModel {NewCount = count};
                var list = new List<SocialNetworkModel>();
                list = _basic.SocialNetworkCat(Aphid_ID);
                if (list.Count != 0)
                {
                    ViewBag.Social = list;
                    return View(model);
                }
                return RedirectToAction("index", "SocialNetworks");
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }
        public ActionResult PdfEbookPosting()
        {
            try
            {
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid Aphid_ID = new Guid(session);
                string count = _cmn.GetNewCount(Aphid_ID);
                var model = new SocialNetworkModel {NewCount = count};
                var list = new List<SocialNetworkModel>();
                list = _basic.SocialNetworkCat(Aphid_ID);
                if (list.Count != 0)
                {
                    ViewBag.Social = list;
                    return View(model);
                }
                return RedirectToAction("index", "SocialNetworks");
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }
        public ActionResult selectchanneltoflood()
        {
            try
            {
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid Aphid_ID = new Guid(session);
                string count = _cmn.GetNewCount(Aphid_ID);
                var model = new SocialNetworkModel {NewCount = count};
                var list = new List<SocialNetworkModel>();
                list = _basic.SocialNetworkCat(Aphid_ID);
                if (list.Count != 0)
                {
                    ViewBag.Social = list;
                    return View(model);
                }
                return RedirectToAction("index", "SocialNetworks");
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }




        [HttpPost]
        public ContentResult MatrixImage()
        {
            var mm = "";
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null)
                {
                    var split = hpf.FileName.Split('.');
                    if (split[1] == "jpg" || split[1] == "JPG" || split[1] == "png" || split[1] == "PNG")
                    {
                        string savedImageName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                        string filename = Path.GetFileName(Request.Files[file].FileName);
                        Request.Files[file].SaveAs(Server.MapPath(savedImageName));
                        imagepath = savedImageName;
                        mm = savedImageName;
                        Session["imagepath"] = imagepath;

                    }
                    else
                    {

                        mm = "Invalid";
                    }
                }
            }
            return Content("{\"name\":\"" + mm + "\"}");
        }

        [HttpPost]
        public ContentResult PrievewImage()
        {
            var mm = "";
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null)
                {
                    mm = hpf.FileName;
                    var split = hpf.FileName.Split('.');
                    if (split[1] == "jpg" || split[1] == "JPG" || split[1] == "png" || split[1] == "PNG")
                    {
                        string savedImageName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                        hpf.SaveAs(Server.MapPath(savedImageName));
                        imagebyte1 = savedImageName;
                        mm = savedImageName;
                    }
                    else
                    {
                        mm = "Invalid";
                    }
                }
            }
            return Content("{\"name\":\"" + mm + "\"}");
        }


        public List<BindDropDown> DropBindAudio()
        {
            var li = new List<BindDropDown>();
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            li = _basic.BindDropVideo(userID);
            var list = li.Select(item => new SelectListItem
            {
                Text = item.Value, Value = item.id.ToString()
            }).ToList();
            ViewBag.AudioFiles = list;
            return li;
        }


        
        private string EbookInsertion(string p, HttpPostedFileBase httpPostedFileBase)
        {
            throw new NotImplementedException();
        }

       
        public ActionResult PhotoartInterruption(string IntF, string FileName, string IntS)
        {
            try
            {
                string FilePhoto = PhotoInsertion(IntF, FileName, IntS);

                return Json(FilePhoto, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public string PhotoInsertion(string IntF, string FileName, string IntS)
        {
            try
            {
                if (IntF == "Default")
                {

                    string first = Server.MapPath(FileName);
                    string second = Server.MapPath("/DefaultFiles/Logo_Tech_.png");
                    string savePath = Server.MapPath(FileName + "file.jpg");
                    var fs = new FileStream(first, FileMode.Open);
                    var b1 = new Bitmap(fs);
                    Image myBitmap = new Bitmap(second);
                    Graphics g1 = Graphics.FromImage(b1);
                    g1.DrawImage(myBitmap, 200, 200);
                    b1.Save(savePath);
                    FilePhoto = FileName + "file.jpg";
                    intphoto = FilePhoto;
                    FileName = null;
                    IntS = null;
                    fs.Close();

                }
                if (IntF == "myfile1")
                {
                    string first = Server.MapPath(FileName);
                    string second = Server.MapPath(IntS);
                    string savePath = Server.MapPath(FileName + "file.jpg");
                    var fs = new FileStream(first, FileMode.Open);
                    var b1 = new Bitmap(fs);
                    Image myBitmap = new Bitmap(second);
                    Graphics g1 = Graphics.FromImage(b1);
                    g1.DrawImage(myBitmap, 200, 200);
                    b1.Save(savePath);
                    FilePhoto = FileName + "file.jpg";
                    intphoto = FilePhoto;
                    FileName = null;
                    IntS = null;
                    fs.Close();

                }

            }
            catch
            {
                return "";
            }
            return FilePhoto;
        }


        public ActionResult Byteyourfiles()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new BasicGenerateCloneModel {NewCount = count};
                if (AphidSession.Current.AuthenticatedUser?.Identity?.Username != null)
                {
                    model.Composer = AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString();
                }
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        [HttpPost]
        public ActionResult ByteyourEbook(BasicGenerateCloneModel model)
        {
            //PdfIntrreputionModel pdfobj = new PdfIntrreputionModel();
            // pdfobj.PdfIntreption(string pdfpath, string composer, string title, Guid userid, string track, string IntFile);

            ViewBag.Message = null;

            try
            {
                string pathpdf = null;
                if (model.Isvalid == true)
                {
                    Guid userId = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    string cap = Session["captchastring"].ToString();

                    if (model.Captcha == cap)
                    {
                        string track;
                        if (model.SelectedIntFile == "No Interruption")
                        {
                            track = RandomPassword.CreatePassword(7);
                            model.UserID = userId;
                            model.TrackingNumber = track;
                            model.MatrixImageBytePath = imagepath;
                        }
                        else
                        {
                            var obvpdf = new PdfIntrreputionModel();
                            var image = Session["imagepath"].ToString();
                            model.imagepath = image;
                            pdfpath = obvpdf.PdfIntreption(artphototitle, model.Composer,model.PagePercentage, model.Title, userId, model.SelectedIntFile, model.imagepath);
                            string[] PDF = pdfpath.Split('@');
                            pathpdf = PDF[0];
                            string[] trackno = pdfpath.Split('@');
                            track = Session["TRACK"] != null ? Session["TRACK"].ToString() : trackno[1];
                        }
                        long len = 0, imgLength = 0, intpdf = 0;
                        if (artphototitle != null)
                        {
                            len = System.IO.File.ReadAllBytes(Server.MapPath(artphototitle)).Length;

                        }
                        if (imagepath != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                        }
                        if (pathpdf != null)
                        {
                            intpdf = System.IO.File.ReadAllBytes(Server.MapPath(pathpdf)).Length;
                        }

                        long length = len + imgLength + intpdf;
                        bool result = _cmn.CheckSpace(userId, length);
                        if (result == true)
                        {
                            model.UserID = userId;
                            model.CloneID = Guid.NewGuid();
                            model.TrackingNumber = track;
                            if (artphototitle != null)
                            {
                                model.PdfFilePath = artphototitle;

                            }
                            if (imagepath != null)
                            {
                                model.MatrixImageBytePath = imagepath;
                            }

                            var ob = new CreateLinkPostModel
                            {
                                Category = "Pdf",
                                Channel = "Matrix",
                                Date = System.DateTime.Now,
                                NoOfChannel = 0,
                                Title = model.Title,
                                TrackingNumber = track,
                                Views = 0,
                                Downloads = 0,
                                UserID = userId,
                                MatrixImagePath = imagepath
                            };
                            //string img = null;
                            //string mat = null;
                            byte[] size = null;
                            var ob1 = new InterruptedFileModel();
                            if (artphototitle != null)
                            {
                                size = System.IO.File.ReadAllBytes(Server.MapPath(artphototitle));
                                ob1.VideoPath = artphototitle;
                            }

                            if (pathpdf != null)
                            {
                                size = System.IO.File.ReadAllBytes(Server.MapPath(pathpdf));
                            }


                            if (size != null)
                            {
                                ob.FileSize = CalculateFileSize.Size(size);

                            }

                            ob1.CreateDate = DateTime.Now;
                            ob1.ModifiedDate = DateTime.Now;
                            ob1.TrackNumber = track;
                            //ob1.VideoPath = FilePhoto;
                            if (ob1.FileName == null)
                            {
                                ob1.FileName = artphototitle.Substring(artphototitle.IndexOf("_") + 1); ;
                            }
                            //else
                            //{
                            //    ob1.FileName = null;
                            //}
                            ob1.CloneId = model.CloneID;
                            ob1.UserId = userId;
                            ob1.IsActive = true;
                            if (pathpdf != null)
                            {
                                ob1.InterruptedFilePath = pathpdf;
                            }
                            var post = new CreateLinkPostModel();
                            if (pathpdf != null)
                            {
                                byte[] arr = System.IO.File.ReadAllBytes(Server.MapPath(pathpdf));
                                post.FileSize = CalculateFileSize.Size(arr);

                            }

                            var alldata = new AllGenerateCloneModel
                            {
                                UserID = userId,
                                CloneId = model.CloneID,
                                Title = model.Title,
                                AlbumTitle = model.AlbumTitle,
                                Tag = model.Tags,
                                ArtistName = model.ArtistName,
                                UploadFilePath = ob1.InterruptedFilePath,
                                UploadImageFilePath = model.UploadImagePath,
                                AudioFilePath = model.UploadAudioPath,
                                MatrixFilePath = model.MatrixImageBytePath,
                                ComposerName = model.Composer,
                                Producer = model.Producer,
                                Publisher = model.Publisher,
                                InteruptionStyle = model.InterruptionStyle,
                                AvailableForDownload = model.AvailableDownload,
                                ExplicitContent = model.ExplicitContent,
                                UploadPDFFilePath = model.PdfFilePath,
                                PagePercentage = model.PagePercentage,
                                Type = model.Type,
                                FileNames = ob1.FileName,
                                VideoFilePath = model.VideoFile,
                                SelectedInteruptionFile = model.SelectedIntFile,
                                WaterMarkMatrixImagePath = model.WatermarkMatrixImagePath,
                                WaterMarkMatrixImageText = model.WatermarkMatrixImageText,
                                VideoCategory = model.VideoCategory,
                                RARFilePath = model.Producer,
                                MatrixImagePath = model.MatrixImageBytePath,
                                CreatotName = model.CreatorName,
                                TrackingNumber = track,
                                CreatedDate = DateTime.Now,
                                ModifyDate = DateTime.Now,
                                IsActive = true,
                                CatID = 4,
                                GenCloneID = 1
                            };
                            model.CatId = 4;
                            ob1.CatId = 4;

                            model.TotalLength = length.ToString();
                            alldata.PdfFilePath = model.PdfFilePath;
                            bool re = _basic.InsertBasicByteYourEbook(model, ob1, ob, alldata);
                            if (re == true)
                            {
                                bool res = _cmn.UpdateDataMemory(userId, length);
                            }
                            string count = _cmn.GetNewCount(userId);

                            model.NewCount = count;
                            DropBind();
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Basic");
                            //Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>"); Response.Redirect("ds");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Captcha";
                        ViewBag.backpage = "/Basic/ByteyourEbook";
                        return View("GenralErrorPage");
                    }
                }
            }

            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");

            }
            // artphototitle = null;
            //imagepath = null;
            ViewBag.Sucess = true;
            return View(model);
        }


        [HttpPost]
        public ActionResult ByteyourArtPhotography(BasicGenerateCloneModel model)
        {
            ViewBag.Message = null;
            try
            {
                if (model.Isvalid == true)
                {
                    string IntPath = PhotoInsertion(model.SelectedIntFile, imagebyte1, imagebyte2);
                    trackNo = RandomPassword.CreatePassword(7);
                    string cap = Session["captchastring"].ToString();
                    if (model.Captcha == cap)
                    {
                        long len = 0, imgLength = 0, intpath = 0;
                        Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                        if (imagebyte1 != null)
                        {
                            len = System.IO.File.ReadAllBytes(Server.MapPath(imagebyte1)).Length;
                        }
                        if (imagebyte2 != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagebyte2)).Length;
                        }
                        if (!string.IsNullOrEmpty(IntPath))
                        {
                            intpath = System.IO.File.ReadAllBytes(Server.MapPath(IntPath)).Length;
                        }

                        long length = len + imgLength + intpath;
                        bool result = _cmn.CheckSpace(userID, length);
                        if (result == true)
                        {
                            model.UserID = userID;
                            model.CloneID = Guid.NewGuid();
                            artphototitle = model.Title;
                            //model.in = intphoto;
                            model.TrackingNumber = trackNo;
                            string mat = null;
                            if (imagebyte1 != null)
                            {
                                model.UploadImagePath = imagebyte1;
                            }
                            if (imagebyte2 != null)
                            {
                                model.MatrixImageBytePath = imagebyte2;
                            }

                            var intModel = new InterruptedFileModel
                            {
                                CloneId = Guid.NewGuid(),
                                CreateDate = System.DateTime.Now,
                                InterruptedFilePath = intphoto,
                                IsActive = true,
                                ModifiedDate = System.DateTime.Now,
                                UserId = userID,
                                TrackNumber = trackNo
                            };
                            if (intphoto != null)
                            {
                                intModel.InterruptedFilePath = intphoto;
                            }
                            if (intModel.FileName == null)
                            {
                                intModel.FileName = imagebyte1.Substring(imagebyte1.IndexOf("_") + 1);
                            }
                            //else
                            //{
                            //    intModel.FileName = null;
                            //}

                            CreateLinkPostModel post = new CreateLinkPostModel();
                            if (intphoto != null)
                            {
                                byte[] arr = System.IO.File.ReadAllBytes(Server.MapPath(intphoto));
                                post.FileSize = CalculateFileSize.Size(arr);

                            }
                            post.Category = "Photos";
                            post.Channel = "Matrix";
                            post.Date = System.DateTime.Now;
                            post.Downloads = 0;
                            post.NoOfChannel = 0;
                            post.Title = model.Title;
                            post.TrackingNumber = trackNo;
                            post.Views = 0;
                            post.UserID = userID;
                            post.MatrixImagePath = imagebyte2;
                            post.UploadImageFilePath = imagebyte1;
                            var alldata = new AllGenerateCloneModel
                            {
                                UserID = userID,
                                CloneId = model.CloneID,
                                Title = model.Title,
                                AlbumTitle = model.AlbumTitle,
                                Tag = model.Tags,
                                ArtistName = model.ArtistName,
                                UploadFilePath = intModel.InterruptedFilePath,
                                UploadImageFilePath = model.UploadImagePath,
                                MatrixFilePath = model.MatrixImageBytePath,
                                ComposerName = model.Composer,
                                Producer = model.Producer,
                                Publisher = model.Publisher,
                                InteruptionStyle = model.InterruptionStyle,
                                AvailableForDownload = model.AvailableDownload,
                                ExplicitContent = model.ExplicitContent,
                                UploadPDFFilePath = model.PdfFilePath,
                                PagePercentage = model.PagePercentage,
                                Type = model.Type,
                                FileNames = intModel.FileName,
                                VideoFilePath = model.VideoFile,
                                WaterMarkMatrixImagePath = model.WatermarkMatrixImagePath,
                                WaterMarkMatrixImageText = model.WatermarkMatrixImageText,
                                VideoCategory = model.VideoCategory,
                                RARFilePath = model.Producer,
                                MatrixImagePath = model.MatrixImageBytePath,
                                CreatotName = model.CreatorName,
                                TrackingNumber = trackNo,
                                CreatedDate = DateTime.Now,
                                ModifyDate = DateTime.Now,
                                IsActive = true,
                                CatID = 3,
                                SelectedInteruptionFile = model.SelectedIntFile
                            };
                            model.CatId = 3;
                            alldata.GenCloneID = 1;
                            model.TotalLength = length.ToString(); ;
                            bool re = _basic.InsertPhotoArt(model, intModel, post, alldata);
                            if (re == true)
                            {
                                bool res = _cmn.UpdateDataMemory(userID, length);
                            }
                            string count = _cmn.GetNewCount(userID);

                            model.NewCount = count;
                            DropBind();
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Basic");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Captcha";
                        ViewBag.backpage = "/Basic/ByteyourArtPhotography";
                        return View("GenralErrorPage");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            ViewBag.sucess = true;
            return View(model);
        }


        [HttpPost]
        public ActionResult Byteyourfiles(BasicGenerateCloneModel model)
        {
            try
            {
                long len = 0, imgLength = 0;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                if (ZipPath != null)
                {
                    len = System.IO.File.ReadAllBytes(Server.MapPath(ZipPath)).Length;
                }
                if (imagepath != null)
                {
                    imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                }
                if (imagepath != null)
                {
                    model.MatrixImageBytePath = imagepath;
                }

                long length = len + imgLength;

                bool result = _cmn.CheckSpace(userID, length);
                if (result == true)
                {
                    string track = RandomPassword.CreatePassword(7);
                    model.UserID = userID;
                    model.TotalLength = length.ToString();
                    model.CloneID = Guid.NewGuid();
                    model.RarFilePath = ZipPath;
                    model.RarPath = model.RarFilePath;
                    model.TrackingNumber = track;
                    model.CatId = 5;
                    var ob = new CreateLinkPostModel
                    {
                        Category = "Files",
                        Channel = "Matrix",
                        Date = DateTime.Now,
                        NoOfChannel = 0,
                        Title = model.Title,
                        TrackingNumber = track,
                        Views = 0,
                        Downloads = 0,
                        UserID = userID
                    };
                    byte[] size = null;
                    if (ZipPath != null)
                    {
                        size = System.IO.File.ReadAllBytes(Server.MapPath(ZipPath));
                    }
                    if (size != null)
                    {
                        ob.FileSize = CalculateFileSize.Size(size);
                    }
                    var ob1 = new InterruptedFileModel();
                    ob1.CreateDate = DateTime.Now;
                    ob1.ModifiedDate = DateTime.Now;
                    ob1.TrackNumber = track;
                    ob1.VideoPath = FilePhoto;
                    if (ZipPath == null)
                    {
                        ob1.FileName = ZipPath.Substring(ZipPath.IndexOf("_") + 1);
                    }
                    //else
                    //{
                    //    ob1.FileName = null;
                    //}
                    ob1.CloneId = model.CloneID;
                    ob1.VideoPath = ZipPath;
                    ob1.UserId = userID;
                    ob1.IsActive = true;
                    ob1.InterruptedFilePath = ZipPath;
                    ob1.CatId = 5;
                    var alldata = new AllGenerateCloneModel
                    {
                        UserID = userID,
                        CloneId = model.CloneID,
                        Title = model.Title,
                        AlbumTitle = model.AlbumTitle,
                        Tag = model.Tags,
                        ArtistName = model.ArtistName,
                        UploadFilePath = ob1.InterruptedFilePath,
                        UploadImageFilePath = model.UploadImagePath,
                        AudioFilePath = model.UploadAudioPath,
                        MatrixFilePath = model.MatrixImageBytePath,
                        ComposerName = model.Composer,
                        Producer = model.Producer,
                        Publisher = model.Publisher,
                        InteruptionStyle = model.InterruptionStyle,
                        AvailableForDownload = model.AvailableDownload,
                        ExplicitContent = model.ExplicitContent,
                        UploadPDFFilePath = model.PdfFilePath,
                        PagePercentage = model.PagePercentage,
                        Type = model.Type,
                        FileNames = ob1.FileName,
                        SelectedInteruptionFile = model.SelectedIntFile,
                        VideoFilePath = model.VideoFile,
                        WaterMarkMatrixImagePath = model.WatermarkMatrixImagePath,
                        WaterMarkMatrixImageText = model.WatermarkMatrixImageText,
                        VideoCategory = model.VideoCategory,
                        RARFilePath = model.RarFilePath,
                        MatrixImagePath = model.MatrixImageBytePath,
                        CreatotName = model.CreatorName,
                        TrackingNumber = track,
                        CreatedDate = DateTime.Now,
                        ModifyDate = DateTime.Now,
                        IsActive = true,
                        CatID = 5,
                        GenCloneID = 1
                    };
                    bool re = _basic.ByteYourFile(model, ob1, ob, alldata);
                    if (re == true)
                    {
                        bool res = _cmn.UpdateDataMemory(userID, length);
                    }
                }
                else
                {
                    return RedirectToAction("DataPlanLimit", "Basic");

                }
                ZipArray = null;
                ZipPath = null;
                imagepath = null;
                FilePhoto = null;
                string count = _cmn.GetNewCount(userID);
                model.NewCount = count;
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult channel()
        {
            return View();
        }

        public ActionResult ChangePostData(string Text)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var li = new List<CreateLinkPostModel>();
                var list = new List<CreateLinkPostModel>();
                li = _basic.GetPostData(userID);
                foreach (CreateLinkPostModel t in li)
                {
                    if (t.Category == Text)
                    {
                        string date = t.Date.ToString();
                        string[] dd = date.Split(' ');
                        t.DateShow = dd[0];
                        list.Add(new CreateLinkPostModel()
                        {
                            Title = t.Title,
                            Channel = t.Channel,
                            NoOfChannel = t.NoOfChannel,
                            Views = t.Views,
                            Downloads = t.Downloads,
                            FileSize = t.FileSize,
                            TrackingNumber = t.TrackingNumber,
                            DateShow = t.DateShow,
                            MatrixImagePath = t.MatrixImagePath

                        });
                    }
                }
                //  ViewBag.PostDataNew = list;

                return Json(list);
            }
            catch (Exception)
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult LinkPost()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new MessageModel {NewCount = Convert.ToInt32(count)};
                var li = new List<CreateLinkPostModel>();
                var list = new List<CreateLinkPostModel>();
                li = _basic.GetPostData(userID);
                foreach (CreateLinkPostModel t in li)
                {
                    if (t.Category == "Music")
                    {
                        string date = t.Date.ToString();
                        string[] dd = date.Split(' ');
                        t.DateShow = dd[0];
                        list.Add(new CreateLinkPostModel()
                        {
                            Title = t.Title,
                            Channel = t.Channel,
                            NoOfChannel = t.NoOfChannel,
                            Views = t.Views,
                            Downloads = t.Downloads,
                            FileSize = t.FileSize,
                            TrackingNumber = t.TrackingNumber,
                            DateShow = t.DateShow,
                            MatrixImagePath = t.MatrixImagePath
                        });
                    }
                }
                ViewBag.PostData = list;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult EditCreatePost(string trackno)
        {
            try
            {
                var name = _basic.GetCategory(trackno);
                return RedirectToAction(name, "Basic", new { trackno });
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public ActionResult Files(string trackno)
        {
            try
            {
                var data = _basic.EditClone(trackno);
                var ob = new BasicGenerateCloneModel
                {
                    AlbumTitle = data.AlbumTitle,
                    ArtistName = data.ArtistName,
                    AvailableDownload = data.AvailableDownload,
                    CloneID = data.CloneID,
                    Composer = data.Composer,
                    CreatorName = data.CreatorName,
                    ExplicitContent = data.ExplicitContent,
                    InterruptedAudioPath = data.InterruptedAudioPath,
                    InterruptionStyle = data.InterruptionStyle,
                    MatrixImageBytePath = data.MatrixImageBytePath,
                    RarFilePath = data.RarFilePath
                };
                if (ob.MatrixImageBytePath != null)
                {
                    ob.shortimagepath = data.MatrixImageBytePath.Substring(data.MatrixImageBytePath.IndexOf('_') + 1);
                }
                else
                {
                    ob.MatrixImageBytePath = data.MatrixImageBytePath;
                }

                ob.PagePercentage = data.PagePercentage;
                ob.PdfFilePath = data.PdfFilePath;
                ob.Producer = data.Producer;
                ob.Publisher = data.Publisher;
                ob.RarFile = data.RarFile;
                if (ob.RarFilePath != null)
                {
                    ob.shortcatpath = data.RarFilePath.Substring(data.RarFilePath.IndexOf('_') + 1);
                }
                else
                {
                    ob.RarFilePath = data.RarFilePath;
                }

                ob.SelectedIntFile = data.SelectedIntFile;
                ob.SongName = data.SongName;
                ob.Tags = data.Tags;
                ob.Title = data.Title;
                ob.TrackingNumber = data.TrackingNumber;
                ob.Type = data.Type;
                ob.UploadAudioPath = data.UploadAudioPath;
                ob.UploadImagePath = data.UploadImagePath;
                ob.VideoCategory = data.VideoCategory;
                ob.VideoFile = data.VideoFile;
                ob.WatermarkMatrixImagePath = data.WatermarkMatrixImagePath;
                ob.WatermarkMatrixImageText = data.WatermarkMatrixImageText;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                ob.NewCount = count;
                DropBind();
                return View(ob);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult UpdateFile(BasicGenerateCloneModel model)
        {

            try
            {
                long len = 0, imgLength = 0;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var alldata = new AllGenerateCloneModel();
                var ob1 = new InterruptedFileModel();
                byte[] size;
                if (ZipPath != null)
                {
                    len = ZipArray.Length;
                    model.RarPath = ZipPath;
                    alldata.RARFilePath = ZipPath;
                    ob1.InterruptedFilePath = ZipPath;
                    ob1.VideoPath = ZipPath;
                    model.RarPath = ZipPath;
                    size = System.IO.File.ReadAllBytes(Server.MapPath(ZipPath));
                    ob1.FileName = ZipPath.Substring(ZipPath.IndexOf("_") + 1);
                }
                else
                {
                    len = System.IO.File.ReadAllBytes(Server.MapPath(model.RarFilePath)).Length;
                    alldata.RARFilePath = model.RarFilePath;
                    ob1.InterruptedFilePath = model.RarFilePath;
                    ob1.VideoPath = model.RarFilePath;
                    model.RarPath = model.RarFilePath;
                    size = System.IO.File.ReadAllBytes(Server.MapPath(model.RarFilePath));
                    ob1.FileName = model.RarFilePath.Substring(model.RarFilePath.IndexOf("_") + 1);

                }
                if (imagepath != null)
                {
                    imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                    model.MatrixImageBytePath = imagepath;
                    alldata.MatrixFilePath = imagepath;
                    alldata.MatrixImagePath = imagepath;
                }
                else
                {
                    imgLength = System.IO.File.ReadAllBytes(Server.MapPath(model.MatrixImageBytePath)).Length;
                    alldata.MatrixFilePath = model.MatrixImageBytePath;
                    alldata.MatrixImagePath = model.MatrixImageBytePath;
                }

                long length = len + imgLength;
                model.UserID = userID;
                model.CatId = 5;
                model.Modified_Time = System.DateTime.Now;
                if (size != null)
                {
                    alldata.filesize = CalculateFileSize.Size(size);
                }
                ob1.ModifiedDate = System.DateTime.Now;
                ob1.TrackNumber = model.TrackingNumber;
                ob1.VideoPath = FilePhoto;
                ob1.CloneId = model.CloneID;
                ob1.VideoPath = ZipPath;
                ob1.UserId = userID;
                ob1.CatId = 5;
                ob1.IsActive = true;
                alldata.UserID = userID;
                alldata.CloneId = model.CloneID;
                alldata.Title = model.Title;
                alldata.AlbumTitle = model.AlbumTitle;
                alldata.Tag = model.Tags;
                alldata.ArtistName = model.ArtistName;
                alldata.UploadFilePath = ob1.InterruptedFilePath;
                alldata.UploadImageFilePath = model.UploadImagePath;
                alldata.AudioFilePath = model.UploadAudioPath;
                alldata.ComposerName = model.Composer;
                alldata.Producer = model.Producer;
                alldata.Publisher = model.Publisher;
                alldata.InteruptionStyle = model.InterruptionStyle;
                alldata.AvailableForDownload = model.AvailableDownload;
                alldata.ExplicitContent = model.ExplicitContent;
                alldata.UploadImageFilePath = model.UploadImagePath;
                alldata.UploadPDFFilePath = model.PdfFilePath;
                alldata.PagePercentage = model.PagePercentage;
                alldata.Type = model.Type;
                alldata.FileNames = ob1.FileName;
                alldata.SelectedInteruptionFile = model.SelectedIntFile;
                alldata.VideoFilePath = model.VideoFile;
                alldata.WaterMarkMatrixImagePath = model.WatermarkMatrixImagePath;
                alldata.WaterMarkMatrixImageText = model.WatermarkMatrixImageText;
                alldata.VideoCategory = model.VideoCategory;
                alldata.CreatotName = model.CreatorName;
                alldata.TrackingNumber = model.TrackingNumber;
                alldata.ModifyDate = System.DateTime.Now;
                alldata.IsActive = true;
                alldata.CatID = 5;
                alldata.GenCloneID = 1;
                model.RarPath = model.RarFilePath;
                bool re, result1;
                result1 = _cmn.CheckEditStatus(userID, length, model.TotalLength);
                string count = _cmn.GetNewCount(userID);
                model.NewCount = count;
                if (result1 == true)
                {
                    model.TotalLength = length.ToString();
                    re = _basic.UpdateClone(model, ob1, alldata);
                }
                else
                {
                    return RedirectToAction("DataPlanLimit", "Basic");
                }
                imagepath = null;
                ZipPath = null;
                ZipArray = null;
                FilePhoto = null;
                return RedirectToAction("LinkPost", "Basic");
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult Photo(string track)
        {
            try
            {
                var data = _basic.EditClone(trackNo);
                var ob = new BasicGenerateCloneModel();
                ob.AlbumTitle = data.AlbumTitle;
                ob.ArtistName = data.ArtistName;
                ob.AvailableDownload = data.AvailableDownload;
                ob.CloneID = data.CloneID;
                ob.Composer = data.Composer;
                ob.CreatorName = data.CreatorName;
                ob.ExplicitContent = data.ExplicitContent;
                ob.InterruptedAudioPath = data.InterruptedAudioPath;
                ob.InterruptionStyle = data.InterruptionStyle;
                ob.MatrixImageBytePath = data.MatrixImageBytePath;
                if (ob.MatrixImageBytePath != null)
                {

                }
                ob.PagePercentage = data.PagePercentage;
                ob.PdfFilePath = data.PdfFilePath;
                ob.Producer = data.Producer;
                ob.Publisher = data.Publisher;
                ob.RarFile = data.RarFile;
                ob.SelectedIntFile = data.SelectedIntFile;
                ob.SongName = data.SongName;
                ob.Tags = data.Tags;
                ob.Title = data.Title;
                ob.TrackingNumber = data.TrackingNumber;
                ob.Type = data.Type;
                ob.UploadAudioPath = data.UploadAudioPath;
                ob.UploadImagePath = data.UploadImagePath;
                ob.VideoCategory = data.VideoCategory;
                ob.VideoFile = data.VideoFile;
                ob.WatermarkMatrixImagePath = data.WatermarkMatrixImagePath;
                ob.WatermarkMatrixImageText = data.WatermarkMatrixImageText;
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userid);
                ob.NewCount = count;
                DropBind();
                return View(ob);
            }

            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        //public ActionResult Video(string trackno)
        //{
        //    try
        //    {
        //        IBasic basic = DependencyResolver.Current.GetService<IBasic>();
        //        BasicGenerateCloneModel model = new BasicGenerateCloneModel();
        //        Guid userID=new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
        //        ICommon common = DependencyResolver.Current.GetService<ICommon>();
        //        model.NewCount = common.GetNewCount(userID);
        //        var data = basic.EditClone(trackno);
        //        if (data != null)
        //        {
        //            model.AlbumTitle = data.AlbumTitle;
        //            model.ArtistName = data.ArtistName;
        //            model.AvailableDownload = data.AvailableDownload;
        //            model.CloneID = data.CloneID;
        //            model.Composer = data.Composer;
        //            model.CreatorName = data.CreatorName;
        //            model.ExplicitContent = data.ExplicitContent;
        //            model.InterruptionStyle = data.InterruptionStyle;
        //            if (data.MatrixImageBytePath != null)
        //            {
        //                model.shortimagepath = data.MatrixImageBytePath.Substring(data.MatrixImageBytePath.IndexOf('_') + 1);
        //            }
        //            model.PagePercentage = data.PagePercentage;
        //            model.PdfFilePath = data.PdfFilePath;
        //            model.Producer = data.Producer;
        //            model.Publisher = data.Publisher;
        //            model.RarFilePath = data.RarFilePath;
        //            model.SelectedIntFile = data.SelectedIntFile;
        //            model.Tags = data.Tags;
        //            model.Title = data.Title;
        //            model.TrackingNumber = data.TrackingNumber;
        //            model.Type = data.Type;
        //            model.VideoFile = data.VideoFile;
        //            model.MatrixImageBytePath = data.MatrixImageBytePath;
        //            if (data.VideoFile != null)
        //            {
        //                model.shortcatpath = data.VideoFile.Substring(data.VideoFile.IndexOf('_') + 1);
        //            }
        //            model.UploadImage = data.UploadImage;
        //            model.UserID = data.UserID;
        //            model.VideoFile = data.VideoFile;                    
        //            DropBind();
        //        }
        //        return View(model);
        //    }
        //    catch (Exception)
        //    {

        //        return RedirectToAction("LoginUser", "Accounts");
        //    }



        //}



        public ActionResult Photos(string trackno)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var model = new BasicGenerateCloneModel();
                model.NewCount = _cmn.GetNewCount(userID);
                var data = _basic.EditClone(trackno);
                if (data != null)
                {
                    model.AlbumTitle = data.AlbumTitle;
                    model.ArtistName = data.ArtistName;
                    model.AvailableDownload = data.AvailableDownload;
                    model.CloneID = data.CloneID;
                    model.Composer = data.Composer;
                    model.CreatorName = data.CreatorName;
                    model.ExplicitContent = data.ExplicitContent;
                    model.InterruptionStyle = data.InterruptionStyle;
                    if (data.MatrixImageBytePath != null)
                    {
                        model.shortimagepath = data.MatrixImageBytePath.Substring(data.MatrixImageBytePath.IndexOf('_') + 1);
                    }
                    model.PagePercentage = data.PagePercentage;
                    model.PdfFilePath = data.PdfFilePath;
                    model.Producer = data.Producer;
                    model.Publisher = data.Publisher;
                    model.RarFilePath = data.RarFilePath;
                    model.SelectedIntFile = data.SelectedIntFile;
                    model.Tags = data.Tags;
                    model.Title = data.Title;
                    model.TrackingNumber = data.TrackingNumber;
                    model.Type = data.Type;
                    model.VideoFile = data.VideoFile;
                    model.MatrixImageBytePath = data.MatrixImageBytePath;
                    if (data.UploadImagePath != null)
                    {
                        model.shortcatpath = data.UploadImagePath.Substring(data.UploadImagePath.IndexOf('_') + 1);
                    }
                    //model.UploadImage = data.UploadImage;
                    model.UploadImagePath = data.UploadImagePath;
                    DropBind();
                }
                return View(model);
            }

            catch (Exception)
            {

                throw;
            }

        }


        public ActionResult UpdatePhotos(BasicGenerateCloneModel model)
        {
            try
            {
                
                     string IntPath;
                    if((imagebyte1!=null)&&(imagebyte2!=null))
                    {
                    IntPath = PhotoInsertion(model.SelectedIntFile, imagebyte1, imagebyte2);
                    }
                    else
                    {
                   IntPath = PhotoInsertion(model.SelectedIntFile, model.UploadImagePath, model.MatrixImageBytePath);
                    }
                    InterruptedFileModel intModel = new InterruptedFileModel();
                    string cap = Session["captchastring"].ToString();
                    if (model.Captcha == cap)
                    {
                        long len = 0, imgLength = 0, intpath = 0;
                        Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                        len = imagebyte1 != null ? System.IO.File.ReadAllBytes(Server.MapPath(imagebyte1)).Length : System.IO.File.ReadAllBytes(Server.MapPath(model.UploadImagePath)).Length;
                        imgLength = imagebyte2 != null ? System.IO.File.ReadAllBytes(Server.MapPath(imagebyte2)).Length : System.IO.File.ReadAllBytes(Server.MapPath(model.MatrixImageBytePath)).Length;
                        intpath = IntPath != null ? System.IO.File.ReadAllBytes(Server.MapPath(IntPath)).Length : System.IO.File.ReadAllBytes(Server.MapPath(model.UploadImagePath)).Length;

                        long length = len + imgLength + intpath;                         
                            model.UserID = userID;
                            model.CatId = 3;                
                            if (imagebyte1 != null)
                            {
                                model.UploadImagePath = imagebyte1;
                                    intModel.FileName = imagebyte1.Substring(imagebyte1.IndexOf("_") + 1);
                                intModel.VideoPath=model.UploadImagePath;
                            }
                            else
                            {
                                model.UploadImagePath = model.UploadImagePath;
                                   intModel.FileName = model.UploadImagePath.Substring(model.UploadImagePath.IndexOf("_") + 1);
                                intModel.VideoPath=model.UploadImagePath;
                            }
                            if (imagebyte2 != null)
                            {
                                model.MatrixImageBytePath = imagebyte2;
                            }
                            else
                            {
                                model.MatrixImageBytePath = model.MatrixImageBytePath;
                            }
                           intModel.CloneId = model.CloneID;
                            intModel.ModifiedDate = System.DateTime.Now;
                            intModel.InterruptedFilePath = IntPath;
                            if (intphoto != null)
                            {
                                intModel.InterruptedFilePath = intphoto;
                            }
                            else
                            {
                                intModel.InterruptedFilePath = model.UploadImagePath;
                            }
                           
                            intModel.UserId = userID;
                            intModel.CatId = 3;
                            intModel.TrackNumber = model.TrackingNumber;
                            var alldata = new AllGenerateCloneModel();
                            if (intphoto != null)
                            {
                                byte[] arr = System.IO.File.ReadAllBytes(Server.MapPath(intphoto));
                                alldata.filesize = CalculateFileSize.Size(arr);
                            }                  
                            alldata.UserID = userID;
                            alldata.CloneId = model.CloneID;
                            alldata.Title = model.Title;
                            alldata.AlbumTitle = model.AlbumTitle;
                            alldata.Tag = model.Tags;
                            alldata.ArtistName = model.ArtistName;
                            alldata.UploadFilePath = intModel.InterruptedFilePath;
                            alldata.UploadImageFilePath = model.UploadImagePath;
                            alldata.MatrixFilePath = model.MatrixImageBytePath;
                            alldata.ComposerName = model.Composer;
                            alldata.Producer = model.Producer;
                            alldata.Publisher = model.Publisher;
                            alldata.InteruptionStyle = model.InterruptionStyle;
                            alldata.AvailableForDownload = model.AvailableDownload;
                            alldata.ExplicitContent = model.ExplicitContent;
                            alldata.UploadImageFilePath = model.UploadImagePath;
                            alldata.UploadPDFFilePath = model.PdfFilePath;
                            alldata.PagePercentage = model.PagePercentage;
                            alldata.Type = model.Type;
                            alldata.FileNames = intModel.FileName;
                            alldata.VideoFilePath = model.VideoFile;
                            alldata.WaterMarkMatrixImagePath = model.WatermarkMatrixImagePath;
                            alldata.WaterMarkMatrixImageText = model.WatermarkMatrixImageText;
                            alldata.VideoCategory = model.VideoCategory;
                            alldata.RARFilePath = model.Producer;
                            alldata.MatrixImagePath = model.UploadImagePath;
                            alldata.CreatotName = model.CreatorName;
                            alldata.TrackingNumber = trackNo;
                            alldata.CreatedDate = System.DateTime.Now;
                            alldata.ModifyDate = System.DateTime.Now;
                            alldata.IsActive = true;
                            alldata.CatID = 3;
                            alldata.GenCloneID = 1;
                            alldata.SelectedInteruptionFile = model.SelectedIntFile;
                          bool result1, re;
                          result1 = _cmn.CheckEditStatus(userID, length, model.TotalLength);
                        string count = _cmn.GetNewCount(userID);
                        model.NewCount = count;
                        if (result1 == true)
                        {
                            model.TotalLength = length.ToString();
                            re = _basic.UpdateClone(model, intModel, alldata);
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Basic");
                        }

                    }
                    else
                    {
                        ViewBag.Message = "Invalid Captcha";
                        ViewBag.backpage = "/Basic/Photos";
                        return View("GenralErrorPage");
                    }

                       imagebyte1 = null;
                imagebyte2 = null;
                intphoto = null;
                return RedirectToAction("LinkPost", "Basic");
            }
                
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }




        public ActionResult Pdf(string trackno)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                BasicGenerateCloneModel model = new BasicGenerateCloneModel();
                model.NewCount = _cmn.GetNewCount(userID);
                var data = _basic.EditClone(trackno);
                if (data != null)
                {
                    model.AlbumTitle = data.AlbumTitle;
                    model.ArtistName = data.ArtistName;
                    model.AvailableDownload = data.AvailableDownload;
                    model.CloneID = data.CloneID;
                    model.Composer = data.Composer;
                    model.CreatorName = data.CreatorName;
                    model.ExplicitContent = data.ExplicitContent;
                    model.InterruptionStyle = data.InterruptionStyle;
                    if (data.PdfFilePath != null)
                    {
                        model.shortcatpath = data.PdfFilePath.Substring(data.PdfFilePath.IndexOf('_') + 1);
                    }
                    model.PagePercentage = data.PagePercentage;
                    model.PdfFilePath = data.PdfFilePath;
                    model.Producer = data.Producer;
                    model.Publisher = data.Publisher;
                    model.RarFilePath = data.RarFilePath;
                    model.SelectedIntFile = data.SelectedIntFile;
                    model.Tags = data.Tags;
                    model.Title = data.Title;
                    model.TrackingNumber = data.TrackingNumber;
                    model.Type = data.Type;
                    model.VideoFile = data.VideoFile;
                    model.MatrixImageBytePath = data.MatrixImageBytePath;
                    if (data.MatrixImageBytePath != null)
                    {
                        model.shortimagepath = data.MatrixImageBytePath.Substring(data.MatrixImageBytePath.IndexOf('_') + 1);
                    }
                    //model.UploadImage = data.UploadImage;
                    model.UserID = data.UserID;
                    model.UploadImagePath = data.UploadImagePath;
                    DropBind();
                }
                return View(model);
            }


            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }


        }

        public ActionResult UpdatePDF(BasicGenerateCloneModel model)
        {
            try
            {
                string pathpdf = null;
                
                    string cap = Session["captchastring"].ToString();
                    if (model.Captcha == cap)
                    {
                        Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                        byte[] size = null;
                        long len = 0, imgLength = 0, intpdf = 0;
                        AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                        InterruptedFileModel ob1 = new InterruptedFileModel();
                        if (model.SelectedIntFile == "No Interruption")
                        {
                            model.UserID = userID;
                            if(imagepath!=null)
                            {
                            model.MatrixImageBytePath = imagepath;
                            }
                            else
                            {
                                model.MatrixImageBytePath = model.MatrixImageBytePath;
                            }
                        }
                        else
                        {
                            PdfIntrreputionModel obvpdf = new PdfIntrreputionModel();
                            if (artphototitle != null)
                            {
                                pdfpath = obvpdf.PdfIntreption(model.PdfFilePath, model.Composer,model.PagePercentage, model.Title, userID, model.Interruptedfile, model.imagepath);
                            }
                            else
                            {
                                pdfpath = obvpdf.PdfIntreption(model.PdfFilePath, model.Composer,model.PagePercentage, model.Title, userID, model.Interruptedfile, model.imagepath);
                            }
                            string[] PDF = pdfpath.Split('@');
                            pathpdf = PDF[0];
                            string[] trackno = pdfpath.Split('@');
                        }


                        if (!string.IsNullOrEmpty(artphototitle))
                        {
                            len = System.IO.File.ReadAllBytes(Server.MapPath(artphototitle)).Length;
                            size = System.IO.File.ReadAllBytes(Server.MapPath(artphototitle));
                            ob1.VideoPath = artphototitle;
                            ob1.FileName = artphototitle.Substring(artphototitle.IndexOf("_") + 1);
                            Alldata.PdfFilePath = artphototitle;
                            Alldata.UploadPDFFilePath = artphototitle;
                        }
                        else
                        {
                            len = System.IO.File.ReadAllBytes(Server.MapPath(model.PdfFilePath)).Length;
                            size = System.IO.File.ReadAllBytes(Server.MapPath(model.PdfFilePath));
                            ob1.VideoPath = model.PdfFilePath;
                            ob1.FileName = model.PdfFilePath.Substring(model.PdfFilePath.IndexOf("_") + 1);
                            Alldata.PdfFilePath = model.PdfFilePath;
                            Alldata.UploadPDFFilePath = model.PdfFilePath;
                        }
                        if (!string.IsNullOrEmpty(imagepath))
                        {
                            imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                            Alldata.MatrixImagePath = imagepath;
                            Alldata.MatrixFilePath = imagepath;
                        }
                        else
                        {
                            imgLength = System.IO.File.ReadAllBytes(Server.MapPath(model.MatrixImageBytePath)).Length;
                            Alldata.MatrixImagePath = model.MatrixImageBytePath;
                            Alldata.MatrixFilePath = model.MatrixImageBytePath;
                        }
                        if (pathpdf != null)
                        {
                            intpdf = System.IO.File.ReadAllBytes(Server.MapPath(pathpdf)).Length;
                            size = System.IO.File.ReadAllBytes(Server.MapPath(pathpdf));
                            ob1.InterruptedFilePath = pathpdf;
                            Alldata.UploadFilePath = pathpdf;
                            
                        }
                        long length = len + imgLength + intpdf;
                        model.UserID = userID;
                        if (size != null)
                        {
                            Alldata.filesize = CalculateFileSize.Size(size);
                        }
                        ob1.ModifiedDate = System.DateTime.Now;
                        ob1.TrackNumber = model.TrackingNumber;
                        ob1.CloneId = model.CloneID;
                        ob1.UserId = userID;
                        ob1.CatId = 4;
                        Alldata.UserID = userID;
                        Alldata.CloneId = model.CloneID;
                        Alldata.Title = model.Title;
                        Alldata.AlbumTitle = model.AlbumTitle;
                        Alldata.Tag = model.Tags;
                        Alldata.ArtistName = model.ArtistName;
                        model.Modified_Time = System.DateTime.Now;
                        Alldata.UploadImageFilePath = model.UploadImagePath;
                        Alldata.AudioFilePath = model.UploadAudioPath;
                        Alldata.MatrixFilePath = model.MatrixImageBytePath;
                        Alldata.ComposerName = model.Composer;
                        Alldata.Producer = model.Producer;
                        Alldata.Publisher = model.Publisher;
                        Alldata.InteruptionStyle = model.InterruptionStyle;
                        Alldata.AvailableForDownload = model.AvailableDownload;
                        Alldata.ExplicitContent = model.ExplicitContent;
                        Alldata.UploadImageFilePath = model.UploadImagePath;
                        Alldata.UploadPDFFilePath = model.PdfFilePath;
                        Alldata.PagePercentage = model.PagePercentage;
                        Alldata.Type = model.Type;
                        Alldata.FileNames = ob1.FileName;
                        Alldata.VideoFilePath = model.VideoFile;
                        Alldata.WaterMarkMatrixImagePath = model.WatermarkMatrixImagePath;
                        Alldata.WaterMarkMatrixImageText = model.WatermarkMatrixImageText;
                        Alldata.VideoCategory = model.VideoCategory;
                        Alldata.RARFilePath = model.Producer;
                        Alldata.MatrixImagePath = model.MatrixImageBytePath;
                        Alldata.CreatotName = model.CreatorName;
                        Alldata.TrackingNumber = model.TrackingNumber;
                        Alldata.ModifyDate = System.DateTime.Now;
                        Alldata.SelectedInteruptionFile = model.SelectedIntFile;
                        Alldata.IsActive = true;
                        Alldata.CatID = 4;
                        Alldata.GenCloneID = 1;
                        bool result1, re;
                        result1 = _cmn.CheckEditStatus(userID, length, model.TotalLength);
                        string count = _cmn.GetNewCount(userID);
                        model.NewCount = count;
                        if (result1 == true)
                        {
                            model.TotalLength = length.ToString();
                            re = _basic.UpdateClone(model, ob1, Alldata);
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Basic");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Captcha";
                        ViewBag.backpage = "/Basic/ByteyourEbook";
                        return View("GenralErrorPage");
                    }
                    artphototitle = null;
                    imagepath = null;
                    return RedirectToAction("LinkPost", "Basic");
                }            

            catch (Exception)
            {
                return RedirectToAction("LinkPost", "Basic");

            }
            
        }

        public ActionResult Video(string trackNo)
        {
            try
            {
                var data = _basic.EditClone(trackNo);
                var ob = new BasicGenerateCloneModel();
                ob.AlbumTitle = data.AlbumTitle;
                ob.ArtistName = data.ArtistName;
                ob.AvailableDownload = data.AvailableDownload;
                ob.CloneID = data.CloneID;
                ob.Composer = data.Composer;
                ob.CreatorName = data.CreatorName;
                ob.ExplicitContent = data.ExplicitContent;
                ob.InterruptedAudioPath = data.InterruptedAudioPath;
                ob.InterruptionStyle = data.InterruptionStyle;
                if (data.MatrixImageBytePath != null)
                {
                    ob.shortimagepath = data.MatrixImageBytePath.Substring(data.MatrixImageBytePath.IndexOf('_') + 1);
                }
                ob.PagePercentage = data.PagePercentage;
                ob.PdfFilePath = data.PdfFilePath;
                ob.Producer = data.Producer;
                ob.Publisher = data.Publisher;
                ob.RarFile = data.RarFile;
                ob.SelectedIntFile = data.SelectedIntFile;
                ob.SongName = data.SongName;
                ob.Tags = data.Tags;
                ob.Title = data.Title;
                ob.TrackingNumber = data.TrackingNumber;
                ob.Type = data.Type;
                ob.UploadAudioPath = data.UploadAudioPath;
                ob.UploadImagePath = data.UploadImagePath;
                ob.VideoCategory = data.VideoCategory;
                ob.VideoFile = data.VideoFile;
                ob.VideoPath = data.VideoPath;
                ob.MatrixImageBytePath = data.MatrixImageBytePath;
                if (data.VideoPath != null)
                {
                    ob.shortcatpath = data.VideoPath.Substring(data.VideoPath.IndexOf('_') + 1);
                }
                ob.WatermarkMatrixImagePath = data.WatermarkMatrixImagePath;
                ob.WatermarkMatrixImageText = data.WatermarkMatrixImageText;
                Guid id = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                ob.NewCount = _cmn.GetNewCount(id);
                DropBind();
                return View(ob);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult UpdateVideo(BasicGenerateCloneModel model)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var intModel = new InterruptedFileModel();
                var Alldata = new AllGenerateCloneModel();
                var captchatext = HttpContext.Session["captchastring"].ToString();
                if (model.Captcha == captchatext)
                {
                    long len = 0, imgLength = 0;

                    if (videobyte != null)
                    {
                        len = videobyte.Length;
                        model.VideoFile = videopath;
                        model.VideoPath = model.VideoFile;
                        intModel.VideoPath = videopath;
                        intModel.InterruptedFilePath = videopath;
                        intModel.FileName = videopath.Substring(videopath.IndexOf("_") + 1);
                        Alldata.filesize = CalculateFileSize.Size(videobyte);
                    }
                    else
                    {
                        len = System.IO.File.ReadAllBytes(Server.MapPath(model.VideoPath)).Length;
                        model.VideoFile = model.VideoPath;
                        intModel.VideoPath = model.VideoPath;
                        intModel.InterruptedFilePath = model.VideoPath;
                        intModel.FileName = model.VideoPath.Substring(model.VideoPath.IndexOf("_") + 1);

                        byte[] by = System.IO.File.ReadAllBytes(Server.MapPath(model.VideoPath));
                        Alldata.filesize = CalculateFileSize.Size(by);
                    }
                    if (imagepath != null)
                    {

                        imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                        model.MatrixImageBytePath = imagepath;
                    }
                    else
                    {
                        imgLength = System.IO.File.ReadAllBytes(Server.MapPath(model.MatrixImageBytePath)).Length;
                        model.MatrixImageBytePath = model.MatrixImageBytePath;
                    }
                    long length = len + imgLength;
                    model.UserID = userID;
                    //interruption table
                    intModel.ModifiedDate = DateTime.Now;
                    intModel.UserId = userID;
                    intModel.TrackNumber = model.TrackingNumber;
                    intModel.CatId = 2;
                    //all generate table
                    model.Modified_Time = DateTime.Now;
                    Alldata.ModifyDate = DateTime.Now;
                    Alldata.UserID = userID;
                    Alldata.CloneId = model.CloneID;
                    Alldata.Title = model.Title;
                    Alldata.AlbumTitle = model.AlbumTitle;
                    Alldata.Tag = model.Tags;
                    Alldata.ArtistName = model.ArtistName;
                    Alldata.UploadFilePath = intModel.InterruptedFilePath;
                    Alldata.UploadImageFilePath = model.UploadImagePath;
                    Alldata.AudioFilePath = model.UploadAudioPath;
                    Alldata.MatrixFilePath = model.UploadImagePath;
                    Alldata.ComposerName = model.Composer;
                    Alldata.Producer = model.Producer;
                    Alldata.Publisher = model.Publisher;
                    Alldata.InteruptionStyle = model.InterruptionStyle;
                    Alldata.AvailableForDownload = model.AvailableDownload;
                    Alldata.ExplicitContent = model.ExplicitContent;
                    Alldata.UploadImageFilePath = model.UploadImagePath;
                    Alldata.UploadPDFFilePath = model.PdfFilePath;
                    Alldata.PagePercentage = model.PagePercentage;
                    Alldata.Type = model.Type;
                    Alldata.FileNames = intModel.FileName;
                    Alldata.VideoFilePath = model.VideoFile;
                    Alldata.WaterMarkMatrixImagePath = model.WatermarkMatrixImagePath;
                    Alldata.WaterMarkMatrixImageText = model.WatermarkMatrixImageText;
                    Alldata.VideoCategory = model.VideoCategory;
                    Alldata.RARFilePath = model.Producer;
                    Alldata.MatrixImagePath = model.MatrixImageBytePath;
                    Alldata.CreatotName = model.CreatorName;
                    Alldata.TrackingNumber = model.TrackingNumber;
                    Alldata.ModifyDate = System.DateTime.Now;
                    Alldata.IsActive = true;
                    Alldata.CatID = 2;
                    Alldata.GenCloneID = 1;
                    Alldata.SelectedInteruptionFile = model.SelectedIntFile;
                    string count = _cmn.GetNewCount(userID);
                    model.NewCount = count;
                    bool res_ult, re;
                    res_ult = _cmn.CheckEditStatus(intModel.UserId, length, model.TotalLength);
                    if (res_ult == true)
                    {
                        model.TotalLength = length.ToString(); ;
                        re = _basic.UpdateClone(model, intModel, Alldata);
                    }
                    else
                    {
                        return RedirectToAction("DataPlanLimit", "Basic");
                    }
                    videopath = null;
                    imagepath = null;
                    videobyte = null;
                    return RedirectToAction("LinkPost", "Basic");
                }
                ViewBag.Message = "Invalid Captcha";
                ViewBag.backpage = "/Basic/Video";
                return View("GenralErrorPage");
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult Music(string trackno)
        {
            try
            {
                var data = _basic.EditClone(trackno);
                var ob = new BasicGenerateCloneModel();
                ob.AlbumTitle = data.AlbumTitle;
                ob.ArtistName = data.ArtistName;
                ob.AvailableDownload = data.AvailableDownload;
                ob.CloneID = data.CloneID;
                ob.Composer = data.Composer;
                ob.CreatorName = data.CreatorName;
                ob.ExplicitContent = data.ExplicitContent;
                ob.InterruptedAudioPath = data.InterruptedAudioPath;
                ob.InterruptionStyle = data.InterruptionStyle;
                ob.MatrixImageBytePath = data.MatrixImageBytePath;
                ob.UploadAudioPath = data.UploadAudioPath;
                if (ob.MatrixImageBytePath != null)
                {
                    ob.shortimagepath = data.MatrixImageBytePath.Substring(data.MatrixImageBytePath.IndexOf('_') + 1);

                }
                ob.MatrixImageBytePath = data.MatrixImageBytePath;
                ob.PagePercentage = data.PagePercentage;
                ob.PdfFilePath = data.PdfFilePath;
                ob.Producer = data.Producer;
                ob.Publisher = data.Publisher;
                ob.RarFile = data.RarFile;
                ob.SelectedIntFile = data.SelectedIntFile;
                ob.SongName = data.SongName;
                ob.Tags = data.Tags;
                ob.Title = data.Title;
                ob.TrackingNumber = data.TrackingNumber;
                ob.Type = data.Type;
                if (ob.UploadAudioPath != null)
                {
                    ob.shortcatpath = data.UploadAudioPath.Substring(data.UploadAudioPath.IndexOf('_') + 1);
                }
                ob.UploadAudioPath = data.UploadAudioPath;
                ob.UploadImagePath = data.UploadImagePath;
                ob.VideoCategory = data.VideoCategory;
                ob.VideoFile = data.VideoFile;
                ob.WatermarkMatrixImagePath = data.WatermarkMatrixImagePath;
                ob.WatermarkMatrixImageText = data.WatermarkMatrixImageText;
                DropBindAudio();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                ob.NewCount = count;
                return View(ob);

            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult SearchPostLink(string Text, string Category)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
                List<CreateLinkPostModel> list = new List<CreateLinkPostModel>();
                li = _basic.GetSearchRecord(userID, Text, Category);
                for (int i = 0; i < li.Count; i++)
                {
                    string date = li[i].Date.ToString();
                    string[] dd = date.Split(' ');
                    li[i].DateShow = dd[0];
                    list.Add(new CreateLinkPostModel()
                    {
                        Title = li[i].Title,
                        Channel = li[i].Channel,
                        NoOfChannel = li[i].NoOfChannel,
                        Views = li[i].Views,
                        Downloads = li[i].Downloads,
                        FileSize = li[i].FileSize,
                        TrackingNumber = li[i].TrackingNumber,
                        DateShow = li[i].DateShow,
                        MatrixImagePath = li[i].MatrixImagePath
                    });

                }
                return Json(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult EditPlaylist(string PlaylistName, string TrackingID)
        {
            bool result = false;
            try
            {
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                result = _basic.UpdatPlaylistBasic(PlaylistName, TrackingID, UserID);

            }
            catch (Exception)
            {

            }
            return Json(result);

        }
        public ActionResult Expand(string Trackingnumber)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<Bytetracker> li = new List<Bytetracker>();
                List<Bytetracker> list = new List<Bytetracker>();
                li = _basic.expand(userID, Trackingnumber);
                //list.Add(new Bytetracker());

                for (int i = 0; i < li.Count; i++)
                {
                    string date = li[i].Date.ToString();
                    string[] dd = date.Split(' ');
                    li[i].DateShow = dd[0];
                    list.Add(new Bytetracker()
                    {
                        Title = li[i].Title,
                        Channel = li[i].Channel,
                        NoOfclones = li[i].NoOfclones,
                        Views = li[i].Views,
                        Downloads = li[i].Downloads,
                        FileSize = li[i].FileSize,
                        TrackingNumber = li[i].TrackingNumber,
                        DateShow = li[i].DateShow,
                        MatrixImagePath = li[i].MatrixImagePath
                    });

                }

                return Json(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult AtoZ(string Text, string Order)
        {
            try
            {
                string oredr = Order.Trim();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
                List<CreateLinkPostModel> list = new List<CreateLinkPostModel>();
                li = _basic.AtoZ(userID, Text, oredr);
                for (int i = 0; i < li.Count; i++)
                {
                    string date = li[i].Date.ToString();
                    string[] dd = date.Split(' ');
                    li[i].DateShow = dd[0];
                    list.Add(new CreateLinkPostModel()
                    {
                        Title = li[i].Title,
                        Channel = li[i].Channel,
                        NoOfChannel = li[i].NoOfChannel,
                        Views = li[i].Views,
                        Downloads = li[i].Downloads,
                        FileSize = li[i].FileSize,
                        TrackingNumber = li[i].TrackingNumber,
                        DateShow = li[i].DateShow,
                        MatrixImagePath = li[i].MatrixImagePath
                    });

                }
                return Json(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        [HttpPost]
        public ContentResult MatrixImagePre()
        {
            var mm = "";
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file] as HttpPostedFileBase;
                mm = hpf.FileName;
                var split = hpf.FileName.Split('.');
                if (split[1] == "jpg" || split[1] == "JPG" || split[1] == "png" || split[1] == "PNG")
                {
                    string savedImageName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                    hpf.SaveAs(Server.MapPath(savedImageName));
                    imagepath = savedImageName;
                    mm = savedImageName;
                }
                else
                {
                    mm = "Invalid";
                }
            }
            return Content("{\"name\":\"" + mm + "\"}");

        }

        public ContentResult UploadRar()
        {
            var mm = "";
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file] as HttpPostedFileBase;
                mm = hpf.FileName;
                var split = hpf.FileName.Split('.');
                if (split[1] == "rar" || split[1] == "RAR" || split[1] == "zip" || split[1] == "ZIP")
                {
                    string savedZipPath = (@"/TempBasicImages/" + guid + "_" + hpf.FileName);
                    hpf.SaveAs(Server.MapPath(savedZipPath));
                    ZipArray = System.IO.File.ReadAllBytes(Server.MapPath(savedZipPath));
                    ZipPath = savedZipPath;
                }
                else
                {

                    mm = "Invalid";
                }
            }
            return Content("{\"name\":\"" + mm + "\"}");
        }

        public ActionResult Message()
        {
            ViewBag.sucess = false;
               var messagemodel = MessageDetails();
               return View(messagemodel);
        }
        public MessageModel MessageDetails()
        {
            var email = Session["EmailAddress"].ToString();
            var getusermessage = _cmn.GetMessageData(email);
            int Count = getusermessage.Count;
            var messagemodel = new MessageModel();
            foreach (var item in getusermessage)
            {
                messagemodel.listMessageModel.Add(new MessageModel()
                {
                    message_body = item.message_body,
                    message_subject = item.message_subject,
                    sender_username = item.sender_username,
                    receiver_username = item.receiver_username,
                    id=item.id
                });
            }
            ViewBag.Count = Count;
            return messagemodel;            
        }
     

        [HttpPost]
        public ActionResult Message(MessageModel messagemodel)
        {
          
            try
            {
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var email = Session["EmailAddress"].ToString();
                var username = AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString();
                messagemodel.sender_username = username;
                messagemodel.sender_Email = email;
                bool insert = _cmn.InsertMessageDetails(messagemodel);
                if (insert == true)
                {
                    messagemodel.Outboxlist.Add(new MessageModel(){
                        message_body=messagemodel.message_body,
                        receiver_username = messagemodel.receiver_username
                    });
                }
                ViewBag.sucess = true;
                return View(messagemodel);           
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        [HttpPost]
        
        public ActionResult MessageInbox()
        {
             var messagemodel=MessageDetails();
            return PartialView("_MessageInbox",messagemodel);
        }
        [HttpPost]
        public ActionResult ReadMessages(int msgId)
        {
            var readmsg = _cmn.GetReadMessage(msgId);
            return Json(readmsg);
        }



        public ActionResult bytetracker()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                MessageModel model = new MessageModel();
                DataPlanDetail datamodel = new DataPlanDetail();

                datamodel = _basic.DataPlanDetailMethod(userID);
                if (datamodel.PlanId != null)
                {
                    datamodel.FreeShow = CalculateFileSize.ConvertFromLength(datamodel.Free.ToString());
                    datamodel.UsedShow = CalculateFileSize.ConvertFromLength(datamodel.Used.ToString());
                }
                model.Free = datamodel.FreeShow;
                model.Plan = datamodel.UsedShow;
                model.NewCount = Convert.ToInt32(count);


                List<Bytetracker> li = new List<Bytetracker>();
                List<Bytetracker> list = new List<Bytetracker>();
                li = _basic.GetPostData1(userID);
                foreach (Bytetracker t in li)
                {
                    if (t.Category == "Music")
                    {
                        string date = t.Date.ToString();
                        string[] dd = date.Split(' ');
                        t.DateShow = dd[0];
                        list.Add(new Bytetracker()
                        {
                            Title = t.Title,
                            Channel = t.Channel,
                            NoOfclones = t.NoOfclones,
                            Views = t.Views,
                            Downloads = t.Downloads,
                            FileSize = t.FileSize,
                            TrackingNumber = t.TrackingNumber,
                            DateShow = t.DateShow,
                            MatrixImagePath = t.MatrixImagePath

                        });
                    }
                }
                ViewBag.PostData = list;
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult History()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var data = _cmn.GetDataForHistory(userID);

                List<LinkShareHistory> listPurchaseHistoryModel = new List<LinkShareHistory>();
                for (int i = 0; i < data.Count; i++)
                {
                    string date = data[i].DateShow.ToString();
                    string[] dd = date.Split(' ');
                    data[i].DateShow = dd[0];
                    listPurchaseHistoryModel.Add(new LinkShareHistory()
                    {
                        Title = data[i].Title,
                        DateShow = data[i].DateShow

                    });

                }


                //string count = cmn.GetNewCount(userID);
                //MessageModel model = new MessageModel();
                //model.NewCount = Convert.ToInt32(count);
                return View(listPurchaseHistoryModel);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult Dataplan()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());

                string count = _cmn.GetNewCount(userID);

                DataPlanDetail model = new DataPlanDetail();

                model = _basic.DataPlanDetailMethod(userID);
                model.NewCount = count;
                if (model.PlanId != null)
                {
                    model.FreeShow = model.Free.Gigabytes().ToString();
                    model.UsedShow = model.Used.Gigabytes().ToString();
                }
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult ChangeDataPlan(string planId)
        {
            try
            {
                var userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                var re = _cmn.ChangeDataPlan(planId, userID);
                return Json(re == true ? "Success" : "Failed");
            }
            catch (Exception)
            {
                return Json("Failed");
            }
        }

        public ActionResult BasicNextsong()
        {
            return View();

        }
        public void GetMusicPath()
        {
            string Trackingnumber = Session["Trackingnumber"].ToString();
            var li = new List<BasicGenerateCloneModel>();
            li = _basic.fileprivew(Trackingnumber);
            for (int i = 0; i < li.Count; i++)
            {
                string baseurl = Request.Url.GetLeftPart(UriPartial.Authority);
                //Session["MusicPath"] = baseurl+li[i].Interruptedfile;
                Session["MusicPath"] = baseurl + li[i].UploadAudioPath;
            }
        }

        public void GetVideoPath(string trackno)
        {

            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var li = new List<BasicGenerateCloneModel>();
                var list = new List<BasicGenerateCloneModel>();
                string Video_To_Preview = null;
                li = _basic.fileprivew(trackno);
                foreach (BasicGenerateCloneModel t in li)
                {
                    Video_To_Preview = t.VideoFile;
                    string baseurl = Request.Url.GetLeftPart(UriPartial.Authority);
                    Session["MusicPath"] = baseurl + t.Interruptedfile;
                }
            }
            catch
            {
                //return RedirectToAction("LoginUser", "Accounts");
            }
        }

        //public void GetVideoPath()
        //{
        //    IBasic basic = DependencyResolver.Current.GetService<IBasic>();

        public void GetPhotoPath()
        {
            string Trackingnumber = Session["Trackingnumber"].ToString();

            var li = new List<BasicGenerateCloneModel>();

            li = _basic.fileprivew(Trackingnumber);
            //string Video_To_Preview = null;
            for (int i = 0; i < li.Count; i++)
            {

                string baseurl = Request.Url.GetLeftPart(UriPartial.Authority);
                //Session["VideoPath"] = baseurl + li[i].UploadImagePath;
                Session["MusicPath"] = baseurl + li[i].UploadImagePath;

            }

        }

        public void GetPdfPath()
        {
            string Trackingnumber = Session["Trackingnumber"].ToString();

        }

        //    List<BasicGenerateCloneModel> li = new List<BasicGenerateCloneModel>();

        //    li = basic.fileprivew(Trackingnumber);
        //    string Video_To_Preview = null;
        //    for (int i = 0; i < li.Count; i++)
        //    {
        //        string baseurl = Request.Url.GetLeftPart(UriPartial.Authority);

        //        Session["VideoPath"] = baseurl + li[i].Interruptedfile;

        //    }

        //}

        public string FilePrivew(string Trackingnumber, string category)
        {
            try
            {
                //Session["MusicPath"] = null;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                Session["Trackingnumber"] = Trackingnumber;
                if (category == "Select File")
                {
                    GetMusicPath();
                    return ("MusicPrivew");
                }
                if (category == "Music")
                {
                    //List<BasicGenerateCloneModel> li = new List<BasicGenerateCloneModel>();
                    //List<BasicGenerateCloneModel> list = new List<BasicGenerateCloneModel>();
                    //li = basic.fileprivew(Trackingnumber,category);
                    //for (int i = 0; i < li.Count; i++)
                    //{

                    //    list.Add(new BasicGenerateCloneModel()
                    //    {
                    //        Title = li[i].Title,
                    //        AlbumTitle = li[i].AlbumTitle,
                    //        ExplicitContent=li[i].ExplicitContent,
                    //        ArtistName = li[i].ArtistName,
                    //        Composer = li[i].Composer,
                    //        AvailableDownload = li[i].AvailableDownload,
                    //        TrackingNumber = li[i].TrackingNumber,
                    //        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                    //        //Audio=li[i].InterruptedAudioPath,
                    //        //Image=li[i].UploadImagePath,
                    //        //Video=li[i].Video,
                    //    });


                    //}

                    //    ViewBag.PostData = list;
                    GetMusicPath();

                    return ("MusicPrivew");

                }

                if (category == "Video")
                {
                    GetVideoPath(Trackingnumber);
                    return ("VideoPreview");
                }
                if (category == "Photos")
                {
                    //GetImagePath();

                    return ("ArtAndPhotographyPrivew");
                }
                if (category == "Files")
                {
                    return ("FilesPrivew");
                }
                if (category == "Pdf")
                {
                    return ("EbookPrivew");
                }
                if (category == "EBook")
                {
                    return ("EbookPrivew");
                }
                return ("bytetracker");
            }
            catch
            {
                return "";
            }

        }

        public ActionResult DeleteRecord(string Track)
        {
            try
            {
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var res = _cmn.Deleteitem(userid, Track);
                return Json(res);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }



        public ActionResult DeleteImage()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                bool val = _basic.DeleteBasicImage(userID);
                return View();
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        //public ActionResult Deletepost()
        //{
        //    IBasic basic = DependencyResolver.Current.GetService<IBasic>();
        //    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());


        //}
        public ActionResult VideoPreview()
        {
            try
            {
                string trackno = Session["Trackingnumber"].ToString();
                var li = new List<BasicGenerateCloneModel>();
                var list = new List<BasicGenerateCloneModel>();
                string Video_To_Preview = null;
                li = _basic.fileprivew(trackno);
                foreach (BasicGenerateCloneModel t in li)
                {
                    list.Add(new BasicGenerateCloneModel()
                    {
                        Title = t.Title,
                        AlbumTitle = t.AlbumTitle,
                        ExplicitContent = t.ExplicitContent,
                        ArtistName = t.ArtistName,
                        Composer = t.Composer,
                        AvailableDownload = t.AvailableDownload,
                        TrackingNumber = t.TrackingNumber,
                        MatrixImageBytePath = t.MatrixImageBytePath,
                        UploadImagePath = t.UploadImagePath,
                        VideoFile = t.VideoFile,
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });
                    Video_To_Preview = t.VideoFile;
                }

                ViewBag.Video_To_Preview = Video_To_Preview;
                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult ArtAndPhotographyPrivew()
        {
            try
            {
                string Trackingnumber = Session["Trackingnumber"].ToString();
                var li = new List<BasicGenerateCloneModel>();
                li = _basic.fileprivew(Trackingnumber);
                var list = li.Select(t => new BasicGenerateCloneModel()
                {
                    Title = t.Title, AlbumTitle = t.AlbumTitle, ExplicitContent = t.ExplicitContent, ArtistName = t.ArtistName, Composer = t.Composer, AvailableDownload = t.AvailableDownload, TrackingNumber = t.TrackingNumber, MatrixImageBytePath = t.MatrixImageBytePath, UploadImagePath = t.UploadImagePath, Interruptedfile = t.Interruptedfile,
                    //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                    //Audio=li[i].InterruptedAudioPath,
                    //Image=li[i].UploadImagePath,
                    //Video=li[i].Video,
                }).ToList();
                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult ErrorPage()
        {
            return View();
        }
        public string Before_PlaylistPrivew(string TrackingNumber, string Playlist_Name)
        {
            Session["Trackingnumber"] = TrackingNumber;
            Session["Playlist_Name"] = Playlist_Name;
            return "PlaylistPrivew";
        }
        public ActionResult PlaylistPrivew()
        {
            try
            {
                string trackingnumber = Session["Trackingnumber"].ToString();
                string playlistName = Session["Playlist_Name"].ToString();
                var record = new AllGenerateCloneModel();
                try
                {
                    record = _basic.Get_A_Record_via_trackID(trackingnumber);
                    ViewBag.Record_To_Preview = record;
                    ViewBag.Playlist_Name = playlistName;
                }
                catch
                {
                    return RedirectToAction("LoginUser", "Accounts");
                }
                return View();
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult ModifyingNetworks()
        {
            return View();
        }
        public ActionResult MusicPrivew()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();

                var li = new List<BasicGenerateCloneModel>();
                var list = new List<BasicGenerateCloneModel>();
                li = _basic.fileprivew(Trackingnumber);
                string Song_To_Preview = null;
                foreach (BasicGenerateCloneModel t in li)
                {
                    list.Add(new BasicGenerateCloneModel()
                    {
                        Title = t.Title,
                        AlbumTitle = t.AlbumTitle,
                        ExplicitContent = t.ExplicitContent,
                        ArtistName = t.ArtistName,
                        Composer = t.Composer,
                        AvailableDownload = t.AvailableDownload,
                        TrackingNumber = t.TrackingNumber,
                        MatrixImageBytePath = t.MatrixImageBytePath,
                        UploadAudioPath = t.UploadAudioPath,
                        Interruptedfile=t.Interruptedfile,
                        UploadAudio2Path=t.UploadAudio2Path
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });
                    Song_To_Preview = t.UploadAudioPath;
                }

                ViewBag.Song_To_Preview = Song_To_Preview;
                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult UploadConfirmation()
        {
            var filename = TempData["FileName"];
            ViewBag.FileName = filename;
            return View();
        }
        public ActionResult AlbumFinish(BasicGenerateCloneModel model)
        {
            try
            {
                ViewBag.Message = null;
                if (model.Isvalid == true)
                {
                    trackNo = RandomPassword.CreatePassword(7);
                    string IntPath = AudioIntrepption(model.InterruptionStyle, model.SelectedIntFile, songpath, songpath2);
                    string cap = Session["captchastring"].ToString();
                    if (model.Captcha == cap)
                    {
                        long len = 0, imgLength = 0, intpath = 0;
                        Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                        if (gg != null)
                        {
                            len = gg.Length;
                        }
                        if (imagepath != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                        }
                        if (!string.IsNullOrEmpty(IntPath))
                        {
                            intpath = System.IO.File.ReadAllBytes(Server.MapPath(IntPath)).Length;
                        }

                        long length = len + imgLength + intpath;
                        bool result = _cmn.CheckSpace(userID, length);
                        if (result == true)
                        {

                            try
                            {
                                model.TotalLength = length.ToString();
                                model.UserID = userID;
                                model.CloneID = Guid.NewGuid();
                                model.SongName = songname.Substring(songname.IndexOf("_") + 1);
                                // model.InterruptedAudioPath = gg;
                                model.Type = "Album";

                                model.TrackingNumber = trackNo;
                                if (songpath != null)
                                {
                                    model.UploadAudioPath = songpath;
                                }
                                if (imagepath != null)
                                {
                                    model.MatrixImageBytePath = imagepath;
                                }
                                var intModel = new InterruptedFileModel();
                                intModel.CloneId = Guid.NewGuid();
                                intModel.CreateDate = System.DateTime.Now;
                                if (gg != null)
                                {
                                    intModel.InterruptedFilePath = IntrepputedAudioPath;
                                }
                                else
                                {
                                    intModel.VideoPath = songname;
                                }
                                intModel.FileName = model.SongName;
                                intModel.IsActive = true;
                                intModel.ModifiedDate = DateTime.Now;
                                intModel.UserId = userID;
                                intModel.TrackNumber = trackNo;
                                CreateLinkPostModel post = new CreateLinkPostModel();
                                if (gg != null)
                                {
                                    post.FileSize = CalculateFileSize.Size(gg);

                                }
                                else
                                {
                                    byte[] by = System.IO.File.ReadAllBytes(Server.MapPath(songname));
                                    post.FileSize = CalculateFileSize.Size(by);
                                }
                                post.Category = "Music";
                                post.Channel = "Matrix";
                                post.Date = System.DateTime.Now;
                                post.Downloads = 0;
                                post.NoOfChannel = 0;
                                post.Title = model.Title;
                                post.TrackingNumber = trackNo;
                                post.Views = 0;
                                post.UserID = userID;
                                var alldata = new AllGenerateCloneModel
                                {
                                    UserID = userID,
                                    CloneId = model.CloneID,
                                    Title = model.Title,
                                    AlbumTitle = model.AlbumTitle,
                                    Tag = model.Tags,
                                    ArtistName = model.ArtistName,
                                    UploadFilePath = intModel.InterruptedFilePath,
                                    UploadImageFilePath = model.UploadImagePath,
                                    AudioFilePath = model.UploadAudioPath,
                                    MatrixFilePath = model.MatrixImageBytePath,
                                    ComposerName = model.Composer,
                                    Producer = model.Producer,
                                    Publisher = model.Publisher,
                                    InteruptionStyle = model.InterruptionStyle,
                                    AvailableForDownload = model.AvailableDownload,
                                    ExplicitContent = model.ExplicitContent,
                                    UploadPDFFilePath = model.PdfFilePath,
                                    PagePercentage = model.PagePercentage,
                                    Type = model.Type,
                                    FileNames = intModel.FileName,
                                    VideoFilePath = model.VideoFile,
                                    WaterMarkMatrixImagePath = model.WatermarkMatrixImagePath,
                                    WaterMarkMatrixImageText = model.WatermarkMatrixImageText,
                                    VideoCategory = model.VideoCategory,
                                    RARFilePath = model.Producer,
                                    MatrixImagePath = model.MatrixImageBytePath,
                                    CreatotName = model.CreatorName,
                                    TrackingNumber = trackNo,
                                    CreatedDate = DateTime.Now,
                                    ModifyDate = DateTime.Now,
                                    IsActive = true,
                                    CatID = 1,
                                    GenCloneID = 1
                                };
                                tvar1.Add(model);
                                tvar2.Add(intModel);
                                tvar3.Add(post);
                                tvar4.Add(alldata);

                                string count = _cmn.GetNewCount(userID);
                                model.NewCount = count;

                                //db entries start
                                int i = 0;
                                int j = 0;
                                bool re = false;
                                for (; i < tvar1.Count(); i++)
                                {
                                    re = false;
                                    re = _basic.InsertSingleMusic(tvar1[i], tvar2[i], tvar3[i], tvar4[i]);
                                    if (re == true)
                                    {
                                        bool res = _cmn.UpdateDataMemory(userID, Int64.Parse(tvar1[i].TotalLength));
                                        j++;
                                    }
                                    else
                                    {
                                        //record uploaded on server but not entried on db..
                                    }
                                }
                                if (i == j)
                                {
                                    ViewBag.Messa = "Album successfully uploaded..";
                                }
                                //db entries end
                                tvar1.Clear();
                                tvar2.Clear();
                                tvar3.Clear();
                                tvar4.Clear();
                            }
                            catch (Exception)
                            {
                                return RedirectToAction("LoginUser", "Accounts");
                            }
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Basic");
                            //Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Captcha";
                        ViewBag.backpage = "/Basic/Byteyourmusicsingle";
                        return View("GenralErrorPage");
                    }
                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            ViewBag.Messa = "Album successfully uploaded..";
            return View("Album");
        }

        public ActionResult FilesPrivew()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();

                var li = new List<BasicGenerateCloneModel>();
                li = _basic.fileprivew(Trackingnumber);
                var list = li.Select(t => new BasicGenerateCloneModel()
                {
                    Title = t.Title, AlbumTitle = t.AlbumTitle, ExplicitContent = t.ExplicitContent, ArtistName = t.ArtistName, Composer = t.Composer, AvailableDownload = t.AvailableDownload, TrackingNumber = t.TrackingNumber, MatrixImageBytePath = t.MatrixImageBytePath, VideoFile = t.VideoFile, //this is for file path (rar)
                    //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                    //Audio=li[i].InterruptedAudioPath,
                    //Image=li[i].UploadImagePath,
                    //Video=li[i].Video,
                }).ToList();
                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult EbookPrivew()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();

                var li = new List<BasicGenerateCloneModel>();
                li = _basic.fileprivew(Trackingnumber);
                var list = li.Select(t => new BasicGenerateCloneModel()
                {
                    Title = t.Title, AlbumTitle = t.AlbumTitle, ExplicitContent = t.ExplicitContent, ArtistName = t.ArtistName, Composer = t.Composer, AvailableDownload = t.AvailableDownload, TrackingNumber = t.TrackingNumber, MatrixImageBytePath = t.MatrixImageBytePath, UploadFilePDFPath = t.UploadFilePDFPath, Interruptedfile = t.Interruptedfile
                    //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                    //Audio=li[i].InterruptedAudioPath,
                    //Image=li[i].UploadImagePath,
                    //Video=li[i].Video,
                }).ToList();
                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public ActionResult DeleteAudio()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                bool val = _basic.DeleteAudioBasic(userID);
                return View();
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult Overview()
        {
            try
            {
                return View();
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }



        [HttpPost]
        public ContentResult UploadImage()
        {
            double min = 0.0;
            var r = new List<UploadFilesResult>();

            foreach (string file in Request.Files)
            {
                string guid = Guid.NewGuid().ToString();
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentType.Contains("image/"))
                {
                    if (hpf.ContentLength == 0)
                        continue;
                    string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                    hpf.SaveAs(Server.MapPath(savedFileName));
                    byte[] byteImage = System.IO.File.ReadAllBytes(Server.MapPath(savedFileName));
                    Session["BasicImage"] = savedFileName;
                    // System.IO.File.Delete(Server.MapPath("~/TempBasicImages/" + hpf.FileName));
                    r.Add(new UploadFilesResult()
                    {
                        Name = hpf.FileName,
                        Length = hpf.ContentLength.ToString(),
                        Type = hpf.ContentType,
                        Duration = min.ToString()
                    });

                }
                else
                {
                    r.Add(new UploadFilesResult()
                    {
                        Name = "Invalid",
                        Length = "",
                        Type = "",
                        Duration = ""
                    });

                }
            }
            return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"duration\":\"" + r[0].Duration + "\",\"size\":\"" + string.Format(r[0].Length) + "\"}", "application/json");

        }

        public static double Convert100NanosecondsToMilliseconds(double nanoseconds)
        {
            // One million nanoseconds in 1 millisecond, but we are passing in 100ns units...
            return nanoseconds * 0.0001;
        }


        [HttpPost]
        public ContentResult UploadAudio()
        {
            double min = 0.0;
            var r = new List<UploadFilesResult>();

            foreach (string file in Request.Files)
            {
                string guid = Guid.NewGuid().ToString();
                var hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null && (hpf.FileName.Contains(".mp3") || hpf.FileName.Contains(".wma") || hpf.FileName.Contains(".MP3") || hpf.FileName.Contains(".WMA")))
                {
                    if (hpf.ContentLength == 0)
                        continue;
                    string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                    hpf.SaveAs(Server.MapPath(savedFileName));
                    string file1 = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                    ShellFile so = ShellFile.FromFilePath(Server.MapPath(file1));
                    double nanoseconds;
                    double.TryParse(so.Properties.System.Media.Duration.Value.ToString(), out nanoseconds);
                    Console.WriteLine("NanaoSeconds: {0}", nanoseconds);
                    if (nanoseconds > 0)
                    {

                        // double milliseconds = nanoseconds * 0.000001;
                        double seconds = Convert100NanosecondsToMilliseconds(nanoseconds) / 1000;
                        //min = seconds / 60;
                        //min = Math.Round(min, 2);
                        string aa = seconds.ToString();
                        string[] bb = aa.Split('.');
                        int cc = Convert.ToInt32(bb[0]);
                        if (cc >= 55 || cc <= 25)
                        {
                            System.IO.File.Delete(Server.MapPath("/TempBasicImages/" + guid + "_" + hpf.FileName));
                            r.Add(new UploadFilesResult()
                            {
                                Name = "Invalid",
                                Length = "",
                                Type = "",
                                Duration = "Invalid"
                            });
                        }
                        else
                        {

                            string savedFileName1 = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                            //  hpf.SaveAs(savedFileName1);
                            r.Add(new UploadFilesResult()
                            {
                                Name = hpf.FileName,
                                Length = hpf.ContentLength.ToString(),
                                Type = savedFileName1,
                                Duration = min.ToString()
                            });
                            //  byte[] audioByte = System.IO.File.ReadAllBytes(Server.MapPath(savedFileName));
                            //    System.IO.File.Delete(Server.MapPath("~/TempBasicAudio/" + hpf.FileName));
                            Session["BasicAudioFile"] = savedFileName1;
                        }
                    }
                }
                else
                {
                    r.Add(new UploadFilesResult()
                    {
                        Name = "Invalid",
                        Length = "",
                        Type = "",
                        Duration = ""
                    });
                }
            }
            return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"duration\":\"" + r[0].Duration + "\",\"size\":\"" + string.Format(r[0].Length) + "\"}", "application/json");
        }

        public ActionResult Playlist()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new MessageModel();
                model.NewCount = Convert.ToInt32(count);
                var li = new List<string>();
                li = _basic.GetPlaylistNames(userID, null);
                ViewBag.PostdataPlaylist = li.Count != 0 ? li : null;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Loginuser", "Accounts");
            }

        }

        public ActionResult GetPlayList(string TrackingID)
        {
            try
            {
                var li = new List<string>();
                try
                {
                    Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    li = _basic.GetPlaylistNames(UserID, TrackingID);
                }
                catch (Exception)
                {
                    //               return RedirectToAction("Loginuser", "Accounts");
                }
                return Json(li);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult totalplaylist()
        {
            try
            {
                int li = 0;
                try
                {
                    Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    li = _basic.totalplaylist(UserID);
                }
                catch (Exception)
                {
                    return RedirectToAction("Loginuser", "Accounts");
                }
                return Json(li);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public JsonResult GetSongList(string PlaylistName)
        {
            var li = new List<PlaylistModel>();
            try
            {
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                li = _basic.GetSongList(UserID, PlaylistName);
            }
            catch (Exception)
            {
                //               return RedirectToAction("Loginuser", "Accounts");
            }
            return Json(li);
        }

      
        public ActionResult DelSongFromPlay(string PlaylistName, string TrackingID)
        {
            bool result = false;
            try
            {
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                result = _basic.DelSongFromPlay(PlaylistName, TrackingID);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            return Json(result);
        }
        public JsonResult AddSongToPlaylist(string PlaylistName, string TrackingID)
        {
            bool result = false;
            try
            {
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                result = _basic.AddSongToPlaylist(PlaylistName, TrackingID, UserID);
            }
            catch (Exception)
            {

            }
            return Json(result);
        }


        //public JsonResult AddVideoToPlaylist(string PlaylistName, string TrackingID)
        //{
        //    bool result = false;
        //    try
        //    {
        //        IBasic basic = DependencyResolver.Current.GetService<IBasic>();
        //        Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
        //        result = basic.AddVideoToPlaylist(PlaylistName, TrackingID, UserID);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return Json(result);
        //}
        public ActionResult GenralErrorPage()
        {

            return View();
        }

        public ActionResult deleteAccount()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                bool result = _basic.deleteAccount(userID);
                if (result == true)
                {
                    return Json("Success");
                }
                return Json("Failed");
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public ActionResult Fetch_Ad_Video_Data(string ad_type_id)
        {
            try
            {
                var list = new AdvertisementModel();
                list = _basic.Fetch_Ad_Video_Data(ad_type_id);
                return Json(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public JsonResult AddSongtoFav(string TrackingID)
        {
            bool result = false;
            try
            {
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                result = _basic.AddSongtoFav(TrackingID, UserID);
            }
            catch (Exception)
            {

            }
            return Json(result);
        }


        public ActionResult Favourites()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new MessageModel {NewCount = Convert.ToInt32(count)};

                var li = new List<favourites>();
                li = _basic.GetFavourites(userID);
                ViewBag.PostdataFavourites = li.Count != 0 ? li : null;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Loginuser", "Accounts");
            }

        }

        public JsonResult DelfromFav(string TrackingID)
        {
            bool result = false;
            try
            {
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                result = _basic.DelfromFav(TrackingID, UserID);
            }
            catch (Exception)
            {

            }
            return Json(result);
        }

        public ActionResult fetchcat(string trackno)
        {
            try
            {
                //  List<BasicGenerateCloneModel> li = new List<BasicGenerateCloneModel>();
                var name = _basic.GetCategory(trackno);
                return RedirectToAction("FilePrivew", "Basic", new { Trackingnumber = trackno, category = name });
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }
        public ActionResult PostingDetail(string Trackingnumber)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var li = new List<Bytetracker>();
                var list = new List<Bytetracker>();
                li = _basic.Getpostingdata(userID, Trackingnumber);
                //list.Add(new Bytetracker());

                foreach (Bytetracker t in li)
                {
                    string date = t.Date.ToString();
                    string[] dd = date.Split(' ');
                    t.DateShow = dd[0];
                    if (t.poststatus == "True")
                    {
                        list.Add(new Bytetracker()
                        {

                            Title = t.Title,
                            Channel = t.Channel,
                            Category = t.Category,
                            NoOfclones = t.NoOfclones,
                            Views = t.Views,
                            Downloads = t.Downloads,
                            FileSize = t.FileSize,
                            TrackingNumber = t.TrackingNumber,
                            DateShow = t.DateShow
                        });
                    }
                }
                return Json(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public JsonResult GetSongListmusic(string PlaylistName)
        {
            var lists = new List<PlaylistModel>();
            try
            {
                var UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                lists.AddRange(_basic.GetSongListmusic(UserID, PlaylistName).Where(item => item.CatId == 1));
            }
            catch (Exception)
            {
                //               return RedirectToAction("Loginuser", "Accounts");
            }
            return Json(lists);
        }

        public ActionResult FacebookSignUp(FbModel fbmodel)
        {
            var basic =new BasicAccountViewModel
            {
                LastName = fbmodel.last_name,
                FirstName = fbmodel.first_name,
                EmailAddress = fbmodel.email,
                DOB = (fbmodel.birthday)
            };
            return View(fbmodel);
        }

        public ActionResult BasicAccountInfoGoogleLogin(BasicAccountViewModel model)
        {

            return View("BasicAccountInfo", model);
        }
        [HttpPost]
        public ActionResult MessageDelete(int MessageID)
        {
            bool result = false;
            try
            {
                result = _cmn.MessageDeleteCommon(MessageID);               
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            return Json(result);
        }

        public ActionResult Uploadmedia ()
        {

            return View();

        }

        public ActionResult SendVerificationMail()
        {
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
            BasicAccountViewModel basicData = _basic.GetBasicAccountInfo(userID);
            Email mail = new Email();//send mail                
            mail.sendMaill(basicData.BasicUserID, basicData.EmailAddress, "AphidLab", new Guid(), basicData.UserName, "VerifyEmail");
            return View();
        }

        bool UploadSingleSong(BasicGenerateCloneModel model)
        {

            try
            {
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.RequiresNew,new TimeSpan(1,0,0) ))
                {
                    AudioUploader.UploadAudio(model);
                    ImageUploader.UploadThumbnailAudioPictureAndSetLocation(model);
                    trans.Complete();
                    return true;
                }
            }catch(Exception ex)
            {
                return false;
            }
            



        }
    }
}
