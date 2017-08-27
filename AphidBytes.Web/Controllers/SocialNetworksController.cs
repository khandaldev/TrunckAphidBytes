using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Web.App_Code;
using AphidBytes.Web.Models;
using Facebook;
using Google;
using ImpactWorks.FBGraph.Interfaces;
using ImpactWorks.FBGraph.Connector;
using ImpactWorks.FBGraph.Core;
using System.Text;
using AphidBytes.Web.Session_Helper;
using DotNetOpenAuth.AspNet.Clients;
using System.IO.Compression;
using AphidBytes.Web.Web;
using AphidBytes.BLL;

namespace AphidBytes.Web.Controllers
{
    //[SessionHelper]
    public class SocialNetworksController : AphidController
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SocialNetworksController));
        //
        // GET: /SocialNetworks/

        public ActionResult Index()
        {
            try
            {
                ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                List<SocialNetworkModel> list = social.AddChannel(Aphid_ID);
                string result = social.Fetch_AccountType(Aphid_ID);
                if (result == "Byter Account") { ViewBag.ResultIndex = result; }
                else { ViewBag.ResultIndex = null; }
                ViewBag.Status = list;

                Guid id = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                return View();
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult MusicPrivew(string type)
        {
            try
            {
                IBasic basic = DependencyResolver.Current.GetService<IBasic>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();

                List<BasicGenerateCloneModel> li = new List<BasicGenerateCloneModel>();
                List<BasicGenerateCloneModel> list = new List<BasicGenerateCloneModel>();
                li = basic.fileprivew(Trackingnumber);
                string Song_To_Preview = null;
                for (int i = 0; i < li.Count; i++)
                {
                    if (type == "Twitter")
                    {
                        TwitterModel tr = new TwitterModel();
                        tr.Post("title", "accesstoken", li[i].Interruptedfile);
                    }
                    if (type == "GooglePlus")
                    {


                    }


                    list.Add(new BasicGenerateCloneModel()
                    {
                        Title = li[i].Title,
                        AlbumTitle = li[i].AlbumTitle,
                        ExplicitContent = li[i].ExplicitContent,
                        ArtistName = li[i].ArtistName,
                        Composer = li[i].Composer,
                        AvailableDownload = li[i].AvailableDownload,
                        TrackingNumber = li[i].TrackingNumber,
                        MatrixImageBytePath = li[i].MatrixImageBytePath,
                        UploadAudioPath = li[i].UploadAudioPath,
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });
                    Song_To_Preview = li[i].UploadAudioPath;

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
        //public ActionResult Facebook()
        //{
        //    try
        //    {
        //        FaceBookModel fb = new FaceBookModel();
        //        string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
        //        Guid Aphid_ID = new Guid(session);
        //        fb.init1(Aphid_ID);
        //        if (Session["status"] != null)
        //        {
        //            return View();
        //        }
        //        else
        //            return RedirectToAction("Index", "SocialNetworks");
        //    }
        //    catch
        //    {
        //        return
        //            RedirectToAction("Loginuser", "Accounts");
        //    }


        //}

        //fb posting
        [HttpGet]
        public ActionResult Fbposting(string id)
        {

            FaceBookModel fbp = new FaceBookModel();
            //  fbp.posting("title","accesstoken");
            return View();
        }

        [HttpGet]
        public ActionResult Twitterposting(string id)
        {

            //FaceBookModel fbp = new FaceBookModel();
            //fbp.posting("title", "accesstoken");
            return View();
        }

        [HttpGet]
        public ActionResult GooglePlusposting(string id)
        {

            //FaceBookModel fbp = new FaceBookModel();
            //fbp.posting("title", "accesstoken");
            return View();
        }

        [HttpGet]
        public ActionResult Continuebtn(string id, string tweeterText)
        {

            if (id == "1" && tweeterText != null)
            {
                TwitterModel tr = new TwitterModel();
                tr.Post("title", "accesstoken", tweeterText);

            }
            else if (id == "2")
            {
                FaceBookModel fbp = new FaceBookModel();
                // fbp.posting("title", "accesstoken");
                //return View();
            }
            else if (id == "3")
            {


            }

            //return View(Continuebtn);
            //Byter/ByterAccountInfo
            return RedirectToAction("Index", "Home");
        }

        //primium
        [HttpGet]
        public ActionResult Continuebtnfileposting(string id, string tweeterText)
        {

            if (id == "1" && tweeterText != null)
            {
                TwitterModel tr = new TwitterModel();
                tr.Post("title", "accesstoken", tweeterText);

            }
            else if (id == "2")
            {
                FaceBookModel fbp = new FaceBookModel();
                //fbp.posting("title", "accesstoken");
                //return View();
            }
            else if (id == "3")
            {


            }

            //return View(Continuebtn);
            return RedirectToAction("socialposting", "Premium");
        }
        //basic
        [HttpGet]
        public ActionResult Continuebtnpostingbasic(string id, string tweeterText)
        {

            if (id == "1" && tweeterText != null)
            {
                TwitterModel tr = new TwitterModel();
                tr.Post("title", "accesstoken", tweeterText);

            }
            else if (id == "2")
            {
                FaceBookModel fbp = new FaceBookModel();
                // fbp.posting("title", "accesstoken");
                //return View();
            }
            else if (id == "3")
            {


            }

            //return View(Continuebtn);
            return RedirectToAction("socialposting", "Basic");
        }



        public ActionResult LoginWtihGoogleplus()
        {
            GoogleOAuth2Client model = new GoogleOAuth2Client();
            FbModel fbmodel = model.init();
            //return RedirectToAction("LoginWtihSocialSite", "Accounts");
            //return RedirectToAction("BasicAccountInfo", "Basic");
            return View();

        }
        //pravin 
        //[HttpGet]
        public ActionResult LoginWtihfb()
        {
            FaceBookModel fb = new FaceBookModel();
            // fb.FbLogin();
            //return RedirectToAction("LoginWtihSocialSite", "Accounts");
            // return RedirectToAction("BasicAccountInfo", "Basic");
            return View();

        }
        [HttpGet]
        public ActionResult RegisterFacevook(string id)
        {

            FaceBookModel fb = new FaceBookModel();
            FbModel fbmodel = fb.RegisterFb();
            AccountBLL bll = new AccountBLL();
            FacebookAccountViewModel fbviewmodel = new FacebookAccountViewModel();
            if (fbmodel != null)
            {
                var accountType = bll.GetTypeofAccountBySocialNetworkLogin(fbmodel.email, "Facebook");
                if (accountType != 0)
                {
                    return RedirectToAction("LoginFacebook");


                }
                else
                {
                    if (id == "1")
                    {
                        // return RedirectToAction("BasicAccountInfo", "Basic", new FacebookAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "1" });
                        return RedirectToAction("SignUpWithFacebook", "Register", new FacebookAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "1" });
                    }
                    else if (id == "2")
                    {
                        //return RedirectToAction("BasicRegister", "Accounts", new BasicAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture });
                        return RedirectToAction("SignUpWithFacebook", "Register", new FacebookAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "2" });

                    }
                    else if (id == "3")
                    {
                        //return RedirectToAction("premium", "Accounts", new AphidTiseAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture });
                        return RedirectToAction("SignUpWithFacebook", "Register", new FacebookAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "3" });

                    }
                    else if (id == "4")
                    {
                        return RedirectToAction("SignUpWithFacebook", "Register", new FacebookAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "4" });
                        //return RedirectToAction("SignUpWithFacebook", "Accounts", new AphidLabAccountModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, UserEmail = fbmodel.email, Ppicture = fbmodel.picture });
                    }
                }

            }
            return View();
        }

        [HttpGet]
        public ActionResult RegisterGooglePlus(string id)
        {
            logger.Error("Start - RegisterGooglePlus(" + id + ")");
            GoogleOAuth2Client model = new GoogleOAuth2Client();
            FbModel fbmodel = model.init();
            GooglePlusAccountViewModel gpviewmodel = new GooglePlusAccountViewModel();
            //ByterAccountViewModel byter = new ByterAccountViewModel();
            AccountBLL bll = new AccountBLL();
            if (fbmodel != null)
            {
                var accountType = bll.GetTypeofAccountBySocialNetworkLogin(fbmodel.email, "Google");
                if (accountType != 0)
                {
                    return RedirectToAction("LoginGooglePlus", new { id = accountType });
                }
                else
                {
                    if (id == "1")
                    {
                        // return RedirectToAction("BasicAccountInfo", "Basic", new FacebookAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "1" });
                        return RedirectToAction("SignUpWithGoogle", "Register", new GooglePlusAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "1" });
                    }
                    else if (id == "2")
                    {
                        //return RedirectToAction("BasicRegister", "Accounts", new BasicAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture });
                        return RedirectToAction("SignUpWithGoogle", "Register", new GooglePlusAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "2" });

                    }
                    else if (id == "3")
                    {
                        //return RedirectToAction("premium", "Accounts", new AphidTiseAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture });
                        return RedirectToAction("SignUpWithGoogle", "Register", new GooglePlusAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "3" });

                    }
                    else if (id == "4")
                    {
                        return RedirectToAction("SignUpWithGoogle", "Register", new GooglePlusAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "4" });
                        //return RedirectToAction("SignUpWithFacebook", "Accounts", new AphidLabAccountModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, UserEmail = fbmodel.email, Ppicture = fbmodel.picture });
                    }
                }

            }

            return View();
        }
        [HttpGet]
        public ActionResult LoginGooglePlus(string id)
        {
            logger.Info("Call LoginGooglePlus for account Type - " + id);
            GoogleOAuth2ClientLoginBasic model = new GoogleOAuth2ClientLoginBasic();
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            //Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            FbModel fbmodel = model.init();
            /*
            if (fbmodel != null)
            {
                var getuserinfo = social.GetUserInfo(fbmodel.username);
            }

            BasicAccountViewModel bsviewmodel = new BasicAccountViewModel();
            // bsviewmodel.BasicUserID = user;
            if (fbmodel != null)
            {
                var getuserinfo = social.GetUserInfo(fbmodel.username);
                return RedirectToAction("GooglePlusLoginBasic", "Accounts", new BasicAccountViewModel { UserName = fbmodel.username, BasicUserID = getuserinfo.BasicUserID });
            }
            */
            
            if (fbmodel != null)
            {
                AccountBLL bll = new AccountBLL();
                Guid guiidbll = bll.GetUserIdBySocialNetworkLogin(fbmodel.email, "Google");
                var accountType = bll.GetTypeofAccountBySocialNetworkLogin(fbmodel.email, "Google");
                var getuseraccountinfo = social.GetUserInfo(fbmodel.email);
                //Guid guiid = getuseraccountinfo.BasicUserID;
                //sessiomn
                IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                var loginProfile = accountsService.LoginWithSocialSite(guiidbll.ToString());
                if (loginProfile == null)
                {
                    logger.Info("Found google plus login profile empty for Email - " + fbmodel.email.ToString());
                    return RedirectToAction("Index", "Register");
                }
                AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
                switch (accountType)
                {
                    case 1:
                        return RedirectToAction("ByterAccountInfo", "Byter");
                        break;
                    case 2:
                        return RedirectToAction("BasicAccountInfo", "Basic");
                        break;
                    case 3:
                        return RedirectToAction("PremiumAccountInfo", "Premium");
                        break;
                    case 4:
                        return RedirectToAction("AphidLabsAccountInfo", "AphidLabs");
                        break;
                }
                //  return RedirectToAction("FacebookLoginBasic", "Accounts", new BasicAccountViewModel { UserName = fbmodel.email, BasicUserID = guiid });
            }
            return View();
        }

        public ActionResult LoginFacebook(string id)
        {
            FaceBookModel model = new FaceBookModel();
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            FbModel fbmodel = model.FbLogin();
            //get the account type
            if (fbmodel != null)
            {
                AccountBLL bll = new AccountBLL();
                Guid guiidbll = bll.GetUserIdBySocialNetworkLogin(fbmodel.email, "Facebook");
                var accountType = bll.GetTypeofAccountBySocialNetworkLogin(fbmodel.email, "Facebook");
                var getuseraccountinfo = social.GetUserInfo(fbmodel.email);
                //Guid guiid = getuseraccountinfo.BasicUserID;
                //sessiomn
                IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                var loginProfile = accountsService.LoginWithSocialSite(guiidbll.ToString());
                if (loginProfile == null)
                {
                    logger.Info("Found facebook login profile empty for Email - " + fbmodel.email.ToString());
                    return RedirectToAction("Index", "Register");
                }
                AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
                switch (accountType)
                {
                    case 1:
                        return RedirectToAction("ByterAccountInfo", "Byter");
                        break;
                    case 2:
                        return RedirectToAction("BasicAccountInfo", "Basic");
                        break;
                    case 3:
                        return RedirectToAction("PremiumAccountInfo", "Premium");
                        break;
                    case 4:
                        return RedirectToAction("AphidLabsAccountInfo", "AphidLabs");
                        break;
                }
                //  return RedirectToAction("FacebookLoginBasic", "Accounts", new BasicAccountViewModel { UserName = fbmodel.email, BasicUserID = guiid });
            }


            return View();
        }







        [HttpGet]
        public ActionResult RegisterGooglePlusbasic(string id)
        {
            GoogleOAuth2Client model = new GoogleOAuth2Client();
            FbModel fbmodel = model.init();

            //BasicAccountViewModel basic = new BasicAccountViewModel();
            FacebookAccountViewModel fbviewmodel = new FacebookAccountViewModel();

            if (fbmodel != null)
            {

                //return RedirectToAction("BasicRegister", "Accounts", new BasicAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, UserName = fbmodel.username, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture });
                return RedirectToAction("SignUpWithGoogle", "Register", new FacebookAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, UserName = fbmodel.username, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "2" });

            }

            return View();
        }

        [HttpGet]
        public ActionResult RegisterGooglePlustise(string id)
        {
            GoogleOAuth2Client model = new GoogleOAuth2Client();
            FbModel fbmodel = model.init();


            //AphidTiseAccountViewModel tise = new AphidTiseAccountViewModel();

            if (fbmodel != null)
            {

                //return RedirectToAction("AphidTiseRegister", "Accounts", new AphidTiseAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture });
                return RedirectToAction("SignUpWithGoogle", "Register", new FacebookAccountViewModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "3" });

            }

            return View();
        }

        [HttpGet]
        public ActionResult RegisterGooglePlusbasiclab(string id)
        {
            GoogleOAuth2Client model = new GoogleOAuth2Client();
            FbModel fbmodel = model.init();

            FacebookAccountViewModel fbviewmodel = new FacebookAccountViewModel();
            //AphidLabAccountModel lab = new AphidLabAccountModel();
            if (fbmodel != null)
            {

                //return RedirectToAction("AphidLabRegister", "Accounts", new AphidLabAccountModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, UserEmail = fbmodel.email, Ppicture = fbmodel.picture });
                return RedirectToAction("SignUpWithGoogle", "Register", new AphidLabAccountModel { LastName = fbmodel.last_name, FirstName = fbmodel.first_name, EmailAddress = fbmodel.email, Ppicture = fbmodel.picture, AccountType = "4" });

            }

            return View();
        }
        //pravin end


        public ActionResult Fb_Unlink(string id)
        {
            try
            {
                //FaceBookModel fb = new FaceBookModel();
                //string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                //Guid Aphid_ID = new Guid(session);
                //fb.fbUnlink(Aphid_ID);
                //Session["fb_status"] = null;
                return RedirectToAction("Index", "SocialNetworks");
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }

        }


        public ActionResult LinkedLin()
        {
            try
            {
                LinkedLinModel lm = new LinkedLinModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                lm.init(Aphid_ID);
                if (Session["linkstatus"] != null)
                {
                    return View("Facebook");
                }
                else
                {
                    return RedirectToAction("Index", "SocialNetworks");
                }
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }

        }

        public ActionResult Link_Unlink()
        {
            try
            {
                LinkedLinModel unlink = new LinkedLinModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                unlink.Linkedin_Unlink(Aphid_ID);
                Session["link_status"] = null;
                return RedirectToAction("Index", "SocialNetworks");
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }
        public ActionResult Twitter_link()
        {
            try
            {
                TwitterModel tweet = new TwitterModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                tweet.init(Aphid_ID);
                if (Session["twstatus"] != null)
                {
                    return View("Facebook");
                }
                else
                {
                    return RedirectToAction("Index", "SocialNetworks");
                }
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult Twitter_Unlink()
        {
            try
            {
                TwitterModel unlink = new TwitterModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                unlink.twUnlink(Aphid_ID);
                Session["tw_status"] = null;
                return RedirectToAction("Index", "SocialNetworks");
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult Flicker_link()
        {
            try
            {
                FlickerModel tweet = new FlickerModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                tweet.init(Aphid_ID);
                if (Session["flickstatus"] != null)
                {
                    return View("Facebook");
                }
                else
                {
                    return RedirectToAction("Index", "SocialNetworks");
                }
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult Flicker_Unlink()
        {
            try
            {
                FlickerModel tweet = new FlickerModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                tweet.Flick_Unlink(Aphid_ID);
                Session["flick_status"] = null;
                return RedirectToAction("Index", "SocialNetworks");
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult DailyMotion_link()
        {
            try
            {
                Dailymotion tweet = new Dailymotion();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                tweet.init(Aphid_ID);
                if (Session["daily_status"] != null)
                {
                    return View("Facebook");
                }
                else
                {
                    return RedirectToAction("Index", "SocialNetworks");
                }
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult Dailymotion_Unlink()
        {
            try
            {
                Dailymotion tweet = new Dailymotion();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                tweet.Dailymotion_Unlink(Aphid_ID);
                Session["daily_status"] = null;
                return RedirectToAction("Index", "SocialNetworks");
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }
        //pranav code-login with google plus
        public ActionResult loginwithfbgp(string id)
        {
            GoogleOAuth2Client gplus = new GoogleOAuth2Client();
            // gplus.login();
            //try
            //{
            if (id == "1")
            {
                //returnToA
            }
            if (id == "2")
            {
                return RedirectToAction("BasicAccountInfo", "Basic");
            }
            if (id == "3")
            {

            }

            //}
            //catch
            //{
            //    return RedirectToAction("Loginuser", "Accounts");
            //}
            return View();

        }
        public ActionResult Scribd_link(SocialNetworkModel model)
        {
            try
            {
                string data = "";
                ScribdModel scribd = new ScribdModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                model.ID = Guid.NewGuid();
                model.Aphid_id = Aphid_ID;
                model.category = "Scribd";
                model.IsDeleted = true;
                model.Password = CryptorEngine.Encrypt(model.Password, true);
                data = scribd.Scribd_init(model);
                if (data == null)
                    return View("Index");
                else
                    return View("Facebook");
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }


        public ActionResult Scribd_Unlink()
        {
            try
            {
                ScribdModel tweet = new ScribdModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                tweet.Scribd_unlink(Aphid_ID);
                Session["scribd_status"] = null;
                return RedirectToAction("Index", "SocialNetworks");
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        //public ActionResult YouTube_link()
        //{
        //    try
        //    {
        //        YouTubeModel fb = new YouTubeModel();
        //        string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
        //        Guid Aphid_ID = new Guid(session);
        //        //fb.ID(Aphid_ID);
        //        if (Session["status"] != null)
        //        {
        //            return View();
        //        }
        //        else
        //            return RedirectToAction("Index", "SocialNetworks");
        //    }
        //    catch
        //    {
        //        return
        //            RedirectToAction("Loginuser", "Accounts");
        //    }
        //}

        public ActionResult YouTube_link(SocialNetworkModel model)
        {
            try
            {
                string data = "";
                YouTubeModel scribd = new YouTubeModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                model.ID = Guid.NewGuid();
                model.Aphid_id = Aphid_ID;
                model.category = "YouTube";
                model.IsDeleted = true;
                model.Expires = DateTime.Now;
                model.Password = CryptorEngine.Encrypt(model.Password, true);
                data = scribd.init(model);
                if (data == null)
                    return View("Index");
                else
                    return View("Facebook");
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult YouTube_Unlink()
        {
            try
            {
                YouTubeModel tweet = new YouTubeModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                tweet.Youtube_unlink(Aphid_ID);
                Session["youtube_status"] = null;
                return RedirectToAction("Index", "SocialNetworks");
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult SoundCloud()
        {
            try
            {
                SoundCloudModel sound = new SoundCloudModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                sound.init(Aphid_ID);
                if (Session["soundstatus"] != null)
                {
                    return View("Facebook");
                }
                else
                    return RedirectToAction("Index", "SocialNetworks");
            }
            catch
            {
                return
                    RedirectToAction("Loginuser", "Accounts");
            }
        }
        public ActionResult SoundCloud_Unlink()
        {
            try
            {
                SoundCloudModel tweet = new SoundCloudModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                tweet.SoundCloud_unlink(Aphid_ID);
                Session["sound_status"] = null;
                return RedirectToAction("Index", "SocialNetworks");
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }

        }

        public ActionResult GallVideo()
        {
            return View();
        }

        public ActionResult GallMusic()
        {
            return View();
        }
        public ActionResult GallEbook()
        {
            return View();
        }
        public ActionResult Gallphotography()
        {
            return View();


        }
        public ActionResult GenCLone()
        {
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
            var result = social.Fetch_AccountType(user);
            return Json(result);
        }




        public ActionResult ModifyExistingNetworks()
        {
            try
            {
                ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                List<SocialNetworkModel> list = social.AddChannel(Aphid_ID);
                ViewBag.Statuslist = list;
                return View();
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public string Deletedata(string value)
        {
            try
            {
                ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                string value1 = social.Modifysocialdata(Aphid_ID, value);
                return "Deleted";
            }
            catch
            {
                return "LoginUser";
            }
        }


        public ActionResult AddChannel()
        {
            try
            {
                ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
                Guid Aphid_ID = new Guid(session);
                List<SocialNetworkModel> list = social.AddChannel(Aphid_ID);
                ViewBag.SocialChannel = list;
                return View();
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }

        }
        public string FindChannel(string socialname)
        {
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString();
            Guid Aphid_ID = new Guid(session);
            string list = social.FindChannel(Aphid_ID, socialname);
            if (list == "true")
            {
                return (socialname + "is inserted");
            }
            else
            {
                return ("Try Again");
            }
        }


        public ActionResult AddNetworkToDb()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNetworkToDb(UrlLinkModel model)
        {
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
            model.UserID = user;
            model.CreatedDate = System.DateTime.Now;
            model.ModifiedDate = System.DateTime.Now;
            model.Status = false;
            var result = social.InsertUrlLinks(model);
            return View();
        }

        public ActionResult FacebookPost()
        {
            return View();
        }
        Authentication auth = new Authentication();

        public JsonResult PostStatus(string msg)
        {
            Session["postStatus"] = msg;


            ImpactWorks.FBGraph.Connector.Facebook facebook = auth.FacebookAuth();
            if (Session["facebookQueryStringValue"] == null)
            {
                string authLink = facebook.GetAuthorizationLink();
                return Json(authLink);
            }

            if (Session["facebookQueryStringValue"] != null)
            {
                facebook.GetAccessToken(Session["facebookQueryStringValue"].ToString());
                FBUser currentUser = facebook.GetLoggedInUserInfo();
                IFeedPost FBpost = new FeedPost();
                if (Session["postStatus"].ToString() != "")
                {
                    FBpost.Message = Session["postStatus"].ToString();
                    facebook.PostToWall(currentUser.id.GetValueOrDefault(), FBpost);
                    Session["facebookQueryStringValue"] = "";
                }
            }
            return Json("No");
        }
        public ActionResult Success()
        {

            if (Request.QueryString["code"] != null)
            {
                string Code = Request.QueryString["code"];
                Session["facebookQueryStringValue"] = Code;
            }
            if (Session["facebookQueryStringValue"] != null)
            {

                ImpactWorks.FBGraph.Connector.Facebook facebook = auth.FacebookAuth();
                facebook.GetAccessToken(Session["facebookQueryStringValue"].ToString());
                FBUser currentUser = facebook.GetLoggedInUserInfo();
                //IFeedPost FBpost = new FeedPost();
                //if (Session["postStatus"].ToString() != "")
                //{
                //    FBpost.Message = Session["postStatus"].ToString();
                //    var postID = facebook.PostToWall(currentUser.id.GetValueOrDefault(), FBpost);
                //    return RedirectToAction("Index");
                //}
                string response = PostOnFacebookWall(facebook.Token, Session["postStatus"].ToString());
            }

            return View();
        }
        private string PostOnFacebookWall(string accesstoken, string postContent)
        {
            string postResponse = string.Empty;
            HttpWebResponse response = null;
            try
            {
                string url = "https://graph.facebook.com/v2.6/me/feed?access_token=" + accesstoken;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.ContentType = "application/x-www-form-urlencoded";

                request.Method = "POST";
                // request.ServicePoint.Expect100Continue = false;

                string body = @"message=" + postContent;
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();

                response = (HttpWebResponse)request.GetResponse();
                postResponse = ReadResponse(response);
                return postResponse;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)e.Response;
                    postResponse = ReadResponse(response);
                    return postResponse;
                }
                else
                {
                    postResponse = ReadResponse(response);
                    return postResponse;
                }
            }
            catch (Exception)
            {


            }

            postResponse = ReadResponse(response);
            return postResponse;
        }

        private static string ReadResponse(HttpWebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                Stream streamToRead = responseStream;
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    streamToRead = new GZipStream(streamToRead, CompressionMode.Decompress);
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    streamToRead = new DeflateStream(streamToRead, CompressionMode.Decompress);
                }

                using (StreamReader streamReader = new StreamReader(streamToRead, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }



    }
}
