using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Web.App_Code;
using AphidBytes.Web.Session_Helper;
using AphidBytes.Web.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Controllers
{
    
    public class HomeController : AphidController
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));
        static string fname = "";
        static int release = 1;
        public ActionResult Index()
        {
            IAccounts obj = DependencyResolver.Current.GetService<IAccounts>();
            string data = obj.TestMethod("Azad");
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            GetNewRelease();
            return View();
        }

        public List<AdminModel> GetNewRelease()
        {

            IHome home = DependencyResolver.Current.GetService<IHome>();
            List<AdminModel> li = new List<AdminModel>();
            List<PastReleaseModel> pastRelease = new List<PastReleaseModel>();
            List<NewReleaseModel> newRelease = new List<NewReleaseModel>();
            List<upcomingRelease> upcomingReleasee = new List<upcomingRelease>();

            li = home.GetNewRelease();

            for (int i = 0; i < li.Count; i++)
            {
                if (li[i].ReleaseID == 1)
                {
                    string path = li[i].ImagePath;

                    pastRelease.Add(new PastReleaseModel()
                    {
                        Message = li[i].Msg,
                        ImgPath = path,
                        PDBID = li[i].DBID
                    });


                }
                if (li[i].ReleaseID == 2)
                {
                    string path = li[i].ImagePath;

                    newRelease.Add(new NewReleaseModel()
                    {
                        Msg = li[i].Msg,
                        Path = path,
                        NDBID = li[i].DBID
                    });

                }
                if (li[i].ReleaseID == 3)
                {
                    string path2 = li[i].ImagePath;

                    upcomingReleasee.Add(new upcomingRelease()
                    {
                        Message = li[i].Msg,
                        Path = path2,
                        UDBID = li[i].DBID
                    });
                }

            }
            ViewBag.Message1 = pastRelease;
            ViewBag.Message2 = newRelease;
            ViewBag.Message3 = upcomingReleasee;
            return li;
        }



        public List<AdminModel> GetaaDMINRelease()
        {

            IHome home = DependencyResolver.Current.GetService<IHome>();
            List<AdminModel> li = new List<AdminModel>();
            List<PastReleaseModel> pastRelease = new List<PastReleaseModel>();
            List<NewReleaseModel> newRelease = new List<NewReleaseModel>();
            List<upcomingRelease> upcomingReleasee = new List<upcomingRelease>();

            li = home.GetaDMINRelease();

            for (int i = 0; i < li.Count; i++)
            {
                if (li[i].ReleaseID == 1)
                {
                    string path = li[i].ImagePath;

                    pastRelease.Add(new PastReleaseModel()
                    {
                        Message = li[i].Msg,
                        ImgPath = path,
                        PDBID = li[i].DBID
                    });


                }
                if (li[i].ReleaseID == 2)
                {
                    string path = li[i].ImagePath;

                    newRelease.Add(new NewReleaseModel()
                    {
                        Msg = li[i].Msg,
                        Path = path,
                        NDBID = li[i].DBID
                    });

                }
                if (li[i].ReleaseID == 3)
                {
                    string path2 = li[i].ImagePath;

                    upcomingReleasee.Add(new upcomingRelease()
                    {
                        Message = li[i].Msg,
                        Path = path2,
                        UDBID = li[i].DBID
                    });
                }

            }
            ViewBag.Message1 = pastRelease;
            ViewBag.Message2 = newRelease;
            ViewBag.Message3 = upcomingReleasee;
            return li;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }


        public ActionResult DeleteRecord(string ID)
        {
            IHome home = DependencyResolver.Current.GetService<IHome>();
            bool result = home.DeleteRecord(ID);
            return Json(result, JsonRequestBehavior.AllowGet);
            // return View();
        }

        public ActionResult PastAddData()
        {
            return View();
        }

        public ActionResult GetPastReleaseData(string name)
        {
            return View();
        }

        public string Searching(string Texttosearch, string category, string tracking)
        {
            IHome home = DependencyResolver.Current.GetService<IHome>();
            List<searchmodel> li = new List<searchmodel>();
            List<searchmodel> list = new List<searchmodel>();
            Session["Texttosearch"] = Texttosearch;
            if (category == "SEARCH")
            {
                Session["category"] = null;
            }
            else
            {
                Session["category"] = category;
            }

            Session["tracking"] = tracking;
            if (category != null || Texttosearch != null || tracking != null)
            {
                //searchpage(Texttosearch, category, tracking);
                return ("/home/SearchListView");
            }



            return ("Index");



        }
        public string searchpage(string Texttosearch, string category, string tracking)
        {
            return ("SearchListView");
        }
        public ActionResult GetId(string ID)
        {

            if (ID == "Past Release")
            {
                release = 1;
            }
            if (ID == "New Release")
            {
                release = 2;
            }
            if (ID == "Upcoming Release")
            {
                release = 3;
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        
        public ActionResult IndexSearch()
        {
            return View();
        }
        public ActionResult IndexSearchMobile()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult EditNewRelease()
        {
            GetaaDMINRelease();
            return View();
        }

        [HttpPost]
        public ActionResult EditNewRelease(string form)
        {
            IHome home = DependencyResolver.Current.GetService<IHome>();
            AdminModel model = new AdminModel();
            model.ImagePath = "/TempBasicImages/" + fname;
            model.Msg = form;
            model.ReleaseID = release;
            model.ImagePath = "/TempBasicImages/" + fname;
            bool result = home.ReleaseInsert(model);
            GetaaDMINRelease();
            return View();
        }

        public ActionResult EditResult()
        {
            return View();
        }


        public ActionResult Terms()
        {
            return View();
        }
        public ActionResult SearchListView()
        {
            try
            {
                string category;
                IHome home = DependencyResolver.Current.GetService<IHome>();
                List<searchmodel> li = new List<searchmodel>();
                List<searchmodel> list = new List<searchmodel>();
                ChannelModel model = new ChannelModel();
                string Texttosearch = Session["Texttosearch"].ToString();
                if (Session["category"] == null)
                {
                    category = null;
                }
                else
                {
                    category = Session["category"].ToString();
                }
                string tracking = Session["tracking"].ToString();
                li = home.outsearchmethod(Texttosearch, category, tracking);
                for (int i = 0; i < li.Count; i++)
                {
                    list.Add(new searchmodel()
                    {
                        Title = li[i].Title,
                        MatrixImagePath = li[i].MatrixImagePath,
                        TrackingNumber = li[i].TrackingNumber,
                        InterupptedFilepath = li[i].InterupptedFilepath,
                        category = li[i].category,
                        AccountTypeId = li[i].AccountTypeId,
                        PremiumUserId = li[i].PremiumUserId
                    });
                }
                ViewBag.PostData = list;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult UpdateRelease(string ID, string Text, string Path)
        {

            IHome home = DependencyResolver.Current.GetService<IHome>();
            bool update = home.UpdateRelease(ID, Path, Text);
            if (update == true)
            {
                return Json("Success");
            }
            else
            { return Json("Error"); }
        }

        public JsonResult AddSongtoFav(string TrackingID)
        {
            bool result = false;
            try
            {
                IHome home = DependencyResolver.Current.GetService<IHome>();
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                result = home.AddSongtoFav(TrackingID, UserID);
            }
            catch (Exception)
            {

            }
            return Json(result);

        }

        [HttpPost]
        public ContentResult UploadImage()
        {
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                string savedFileName = Path.Combine(Server.MapPath("~/TempBasicImages"), Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);
                fname = hpf.FileName;
            }
            return Content("{\"name\":\"" + hpf.FileName + "\"}");
        }
        public ActionResult searchsort(string Category)
        {
            try
            {
                IHome home = DependencyResolver.Current.GetService<IHome>();
                List<searchmodel> li = new List<searchmodel>();
                List<searchmodel> list = new List<searchmodel>();
                string Texttosearch = Session["Texttosearch"].ToString();
                string tracking = Session["tracking"].ToString();
                li = home.outsearchmethod(Texttosearch, Category, tracking);
                if (li.Count > 0)
                {
                    for (int i = 0; i < li.Count; i++)
                    {

                        list.Add(new searchmodel()
                        {
                            Title = li[i].Title,
                            MatrixImagePath = li[i].MatrixImagePath,
                            TrackingNumber = li[i].TrackingNumber,
                            InterupptedFilepath = li[i].InterupptedFilepath,
                            category = li[i].category
                        });
                    }
                    ViewBag.PostData = list;
                }

                return Json(list);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }

        public string FilePrivew(string trackingnumber, string category)
        {
            Session["Trackingnumber"] = trackingnumber;
            if (category == "MUSIC")
            {
                return ("MusicPrivew");
            }
            if (category == "Video")
            {
                return ("VideoPreview");
            }
            if (category == "Photos")
            {
                return ("ArtAndPhotographyPrivew");
            }
            if (category == "Files")
            {
                return ("FilesPrivew");
            }
            if (category == "Ebook")
            {
                return ("EbookPrivew");
            }
            else
            {
                return ("Index");
            }
        }

        public ActionResult SearchFilm()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult VideoPreview()
        {
            //  Session["ABC"] = "sparsh";
            try
            {
                IHome home = DependencyResolver.Current.GetService<IHome>();
                string trackno = Session["Trackingnumber"].ToString();
                List<BasicGenerateCloneModel> li = new List<BasicGenerateCloneModel>();
                BasicGenerateCloneModel list = new BasicGenerateCloneModel();
                string Video_To_Preview = null;
                list = home.fileprivew(trackno);
                //for (int i = 0; i < li.Count; i++)
                //{
                //    list.Title = li[i].Title;
                //    list.AlbumTitle = li[i].AlbumTitle;
                //    list.ExplicitContent = li[i].ExplicitContent;
                //    list.ArtistName = li[i].ArtistName;
                //    list.Composer = li[i].Composer;
                //    list.AvailableDownload = li[i].AvailableDownload;
                //    list.TrackingNumber = li[i].TrackingNumber;
                //    list.MatrixImageBytePath = li[i].MatrixImageBytePath;
                //    list.UploadImagePath = li[i].UploadImagePath;
                //    list.VideoFile = li[i].VideoFile;
                //    list.UserID = li[i].UserID;
                //    list.GenCloneType = li[i].GenCloneType;
                //    //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                //    //Audio=li[i].InterruptedAudioPath,
                //    //Image=li[i].UploadImagePath,
                //    //Video=li[i].Video,

                Video_To_Preview = list.VideoFile;

                // }

                ViewBag.Video_To_Preview = Video_To_Preview;
                ViewBag.PostData = list;
                return View(list);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }
        public string Email()
        {
            IHome home = DependencyResolver.Current.GetService<IHome>();
            Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            var data = home.FetchEmail(user);
            return data;

        }
        public ActionResult SendData(string Feedbacktxt, string TrackId, int Credits, string Path)
        {

            try
            {
                IHome home = DependencyResolver.Current.GetService<IHome>();
                Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                Guid feedback = Guid.NewGuid();
                var val = Email();
                var data = home.SubmitFeedback(user, Feedbacktxt, TrackId, Credits, Path, val);

                if (data == true || data == false)
                {
                    Email email = new Email();
                   // email.sendMail(val.Trim(), "FeedBack", feedback, "", "Thanks For FeedBack");

                    //return RedirectToAction("FeedBackRegister", "FeedBack", new { Email = val, Text = Feedbacktxt }); 
                }
                return Json(data);
            }
            catch
            {
                return View();
            }

        }


        public ActionResult CheckSession()
        {
            try
            {
                string result = "";
                if ((AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null) || (AphidSession.Current.AuthenticatedUser?.Identity?.Username != null))
                {
                    result = "Exists";
                }
                else { result = "NotExists"; }
                return Json(result);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult VideoPreview(BasicGenerateCloneModel obj)
        {
            //IHome home = DependencyResolver.Current.GetService<IHome>();
            //string encript = CryptorEngine.Encrypt(obj.Password, true);
            //string userid = obj.UserID.ToString();
            //obj.Password = encript;
            //var result = home.UserLogin(obj);
            //if (result!=null)
            //{
            //    AphidSession.Current.AuthenticatedUser?.Identity?.UserId  = result.UserID;
            //    Guid userID = new Guid(userid);
            //    string re = home.CheckAccountType(userID);
            //    if (re=="4")
            //    {
            //        return RedirectToAction("ChannelPage", "Premium", new { Userid = userid });
            //    }
            //}

            return View();
        }

        public ActionResult ShowChannel(string UserID)
        {
            try
            {
                IHome home = DependencyResolver.Current.GetService<IHome>();
                Guid userID = new Guid(UserID);
                string re = home.CheckAccountType(userID);
                if (re == "4")
                {
                    return RedirectToAction("ChannelPage", "Premium", new { Userid = userID, From = "Home" });
                }
                return Json("Success");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ArtAndPhotographyPrivew()
        {
            try
            {
                IHome home = DependencyResolver.Current.GetService<IHome>();

                string Trackingnumber = Session["Trackingnumber"].ToString();

                BasicGenerateCloneModel li = new BasicGenerateCloneModel();
                List<BasicGenerateCloneModel> list = new List<BasicGenerateCloneModel>();
                li = home.fileprivew(Trackingnumber);
                //for (int i = 0; i < li.Count; i++)
                //{

                //    list.Add(new BasicGenerateCloneModel()
                //    {
                //        Title = li[i].Title,
                //        AlbumTitle = li[i].AlbumTitle,
                //        ExplicitContent = li[i].ExplicitContent,
                //        ArtistName = li[i].ArtistName,
                //        Composer = li[i].Composer,
                //        AvailableDownload = li[i].AvailableDownload,
                //        TrackingNumber = li[i].TrackingNumber,
                //        MatrixImageBytePath = li[i].MatrixImageBytePath,
                //        UploadImagePath = li[i].UploadImagePath,
                //        Interruptedfile = li[i].Interruptedfile,
                //        UserID = li[i].UserID,
                //        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                //        //Audio=li[i].InterruptedAudioPath,
                //        //Image=li[i].UploadImagePath,
                //        //Video=li[i].Video,
                //    });


                //}
                //ViewBag.PostData = list;
                return View(li);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult FilesPrivew()
        {
            try
            {
                IHome basic = DependencyResolver.Current.GetService<IHome>();

                string Trackingnumber = Session["Trackingnumber"].ToString();

                BasicGenerateCloneModel li = new BasicGenerateCloneModel();
                li = basic.fileprivew(Trackingnumber);
                //  ViewBag.PostData = list;
                return View(li);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult EbookPrivew()
        {
            try
            {
                IHome basic = DependencyResolver.Current.GetService<IHome>();
                string Trackingnumber = Session["Trackingnumber"].ToString();
                BasicGenerateCloneModel li = new BasicGenerateCloneModel();
                li = basic.fileprivew(Trackingnumber);
                //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                //Audio=li[i].InterruptedAudioPath,
                //Image=li[i].UploadImagePath,
                //Video=li[i].Video,
                return View(li);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Fetch_Ad_Video_Data(string ad_type_id)
        {
            try
            {
                AdvertisementModel list = new AdvertisementModel();
                IHome home = DependencyResolver.Current.GetService<IHome>();
                list = home.Fetch_Ad_Video_Data(ad_type_id);
                if (list != null)
                {
                    list.AdVideo = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + list.AdVideo;
                }

                return Json(list);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }


        public JsonResult GetTrack(string trackingnumber)
        {
            ViewBag.Toplay = null;
            IHome home = DependencyResolver.Current.GetService<IHome>();
            AllGenerateCloneModel re = new AllGenerateCloneModel();
            re = home.GetTrack(trackingnumber);
            ViewBag.trackpath = re.AudioFilePath;
            return Json(re);
        }

        public ActionResult logmein(string uname, string pass, string trackid)
        {
            var res = false;
            LoginUser obj = new LoginUser();
            obj.UserName = uname; ;
            obj.Password = pass;
            string encript = CryptorEngine.Encrypt(obj.Password, true);
            obj.Password = encript;
            IAccounts userLogin = DependencyResolver.Current.GetService<IAccounts>();
            var newobj = userLogin.LoginWithUser(obj);
            if (newobj.Status == true && newobj.Username == obj.UserName)
            {
                if (!string.IsNullOrEmpty(newobj.Username))
                {
                    AphidSession.Current.SetIdentity(newobj, newobj.ExpirationDate.AddHours(-1)); 
                    Session["Trackingnumber"] = trackid;
                    res = true;
                }
            }
            return Json(res);
        }
        public ActionResult Survey(Guid suyveyid)
        {
            try
            {
                IHome home = DependencyResolver.Current.GetService<IHome>();
                AdvertisementModel li = new AdvertisementModel();
                li = home.survey(suyveyid);
                return Json(li);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult logout()
        {
            string result = "";
            try
            {
                if ((AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null) || (AphidSession.Current.AuthenticatedUser?.Identity?.Username != null))
                {
                    Session.Abandon();
                    result = "logedout";
                }

                else { result = "notlogedout"; }
                return Json(result);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LearnMore()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HomePage(string password)
        {
            if (!string.IsNullOrEmpty(password) && password.ToLower() == "wearegeniuses")
                return RedirectToAction("Index", "Home");
            else
                return View();
        }
    }


}

