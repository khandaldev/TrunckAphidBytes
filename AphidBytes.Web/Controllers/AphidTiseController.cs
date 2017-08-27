using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Web.App_Code;
using AphidBytes.Web.Filters;
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
    [SessionFilter]
    public class AphidTiseController : AphidController
    {
        //
        // GET: /AphidTise/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        
        AphidTiseAccountViewModel aphidTiseData1;
        static byte[] ImgByte;
        static int val;
        static string songpath="";
        static string imgpath = "";
        static string logopath = "";
        public ActionResult AphidTiseAccountInfo()
        {
            try
            {

                IAphidTise aphidTise = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                AphidTiseAccountViewModel aphidTiseData = aphidTise.GetAphidTiseInfo(userID);

                ImgByte = aphidTiseData.CompanyLogoByter;
                // Session["AccountInformation"] = aphidTiseData;
                return View(aphidTiseData);
            }
            catch (Exception error)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public ActionResult AphidTiseAdID(string key, string value)
        {
            try
            {
                if (value != null)
                {
                    IAphidTise aphid = DependencyResolver.Current.GetService<IAphidTise>();
                    val = aphid.GetAdId(value);
                    AphidTiseGenerateAds ads = new AphidTiseGenerateAds();
                    ads.AdTypeID = val;
                }
                return View();
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        [HttpPost]
        public ActionResult AphidTiseAccountInfo(AphidTiseAccountViewModel aphidtisemodel)
        {
            try
            {
                ModelState.RemoveFor<AphidTiseAccountViewModel>(x => x.Password);
                ModelState.RemoveFor<AphidTiseAccountViewModel>(x => x.ConfirmPassword);
                if (ModelState.IsValid)
                {
                    //Get AphidTise User Info
                    IAphidTise aphidTise = DependencyResolver.Current.GetService<IAphidTise>();
                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    AphidTiseAccountViewModel aphidTiseData = aphidTise.GetAphidTiseInfo(userID);

                    aphidtisemodel.AphidTiseUserID = userID;// new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString());
                    //Convert profile image in byte array
                    if (aphidtisemodel.CompanyLogo != null)
                    {
                        byte[] data;
                        using (Stream inputStream = aphidtisemodel.CompanyLogo.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            data = memoryStream.ToArray();
                            aphidtisemodel.CompanyLogoByter = data;
                        }
                    }
                    aphidtisemodel.AddressID = aphidTiseData.AddressID;
                    aphidtisemodel.BankAccountID = aphidTiseData.BankAccountID;
                    aphidtisemodel.SecurityQuestionID = aphidTiseData.SecurityQuestionID;

                    bool updateAphidTise = aphidTise.UpdateAphidTiseAccountInfo(aphidtisemodel);
                    if (updateAphidTise == true)
                    {
                        IAphidTise aphidTise1 = DependencyResolver.Current.GetService<IAphidTise>();
                        Guid userID1 = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                        aphidTiseData1 = aphidTise.GetAphidTiseInfo(userID);
                        ImgByte = aphidTiseData1.CompanyLogoByter;
                    }

                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }

            return View(aphidTiseData1);
        }

        public List<BindDropDown> DropBind()
        {
            IAphidTise aphid = DependencyResolver.Current.GetService<IAphidTise>();
            List<BindDropDown> li = new List<BindDropDown>();
            li = aphid.BindDrop();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in li)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.id.ToString()
                });
            }

            ViewBag.CityList = aphid.BindDrop();
            return li;
        }

        public ActionResult AphidTiseGenrateAds()
        {
            DropBind();
            return View();
        }
        public ActionResult AdvGenAds()
        {
            DropBind();
            return View();
        }

        public ActionResult Fetchbookdate(string ad_type_no)
        {
            if (ad_type_no != null)
            {
                IAphidTise AphidAds = DependencyResolver.Current.GetService<IAphidTise>();
                List<string> listDate = new List<string>();
                string value = "", val = "";
                var data = AphidAds.fetch_booked_dates(ad_type_no);
                foreach (var dateTime in data)
                {
                    listDate.Add(dateTime.ToString("d"));
                }

                return Json(listDate);
            }
            else
            {
              return  RedirectToAction("LoginUser","Accounts");
            }
        }
        [HttpPost]
        public ActionResult AphidTiseGenrateAds(AphidTiseGenerateAds AphidAdds)
        {
           
            string trackforpayment = "";
            int ad_ty = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    IAphidTise AphidAds = DependencyResolver.Current.GetService<IAphidTise>();
                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    AphidAdds.TrackingNumber = RandomPassword.CreatePassword(7);
                    AphidAdds.UserID = userID;
                    AphidAdds.CreateDate = System.DateTime.Now;
                    AphidAdds.ModifyDate = System.DateTime.Now;
                    AphidAdds.IsDelete = false;
                    AphidAdds.AdTypeID = val;
               
                    ad_ty = val;
                    Guid guid = Guid.NewGuid();

                    string Clogo = null;
                    string video = null;
                    string pic = null;
                    if (logopath != null)
                    {
                        AphidAdds.CompanyLogoByte = logopath;
                    }
                    if (imgpath != null)
                    {
                        AphidAdds.AdPictureByte = imgpath;
                    }

                    if (songpath != null)
                    {
                        AphidAdds.AdVideoByte = songpath;
                    }
                    
                    bool result = AphidAds.InsertAphidAds(AphidAdds);
                    if (result == true)
                    {
                        trackforpayment = AphidAdds.TrackingNumber;
                    }
                }
                DropBind();
                if (trackforpayment != "")
                {
                    if (ad_ty == 1) 
                    {
                        return RedirectToAction("BeforePlayAdd", "AphidTise", new { TrackNo = trackforpayment }); 
                    }
                    if (ad_ty == 2)
                    {
                        return RedirectToAction("InsideMatrixSearchAdd", "AphidTise", new { TrackNo = trackforpayment }); 
                    }
                    if (ad_ty == 3)
                    {
                        return RedirectToAction("EnteringMatrixAdd", "AphidTise", new { TrackNo = trackforpayment }); 
                    }
                    if (ad_ty == 4)
                    {
                        return RedirectToAction("SearchBarAdd", "AphidTise", new { TrackNo = trackforpayment }); 
                    }
                }
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult AphidtiseMessage()
        {
            return View();
        
        }
        
        public ActionResult AphidtiseHistory()
        {
            return View();
        
        }
        public ActionResult ThanksPage()
        {
            return View();
        }

        public ActionResult AphidtiseByteTracker()
        {
            try
            {
                IAphidTise AphidAds = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<AdvertisementModel> li = new List<AdvertisementModel>();
                List<AdvertisementModel> list = new List<AdvertisementModel>();

                var data = AphidAds.GetPostedDataResult(userID);
                if (data != null)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (Convert.ToDateTime(data[i].AdCycleFromDate) <= System.DateTime.Now && Convert.ToDateTime(data[i].AdCycleToDate) >= System.DateTime.Now)
                        {
                            li.Add(data[i]);
                        }
                        else
                        {
                            list.Add(data[i]);
                        }


                    }


                }
                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult SearchAd(string Text, string clsName)
        {
            try
            {
                IAphidTise aphid = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<AdvertisementModel> li = new List<AdvertisementModel>();
                List<AdvertisementModel> list = new List<AdvertisementModel>();

                var model = aphid.SearchAdv(userID, Text);
                if (model != null)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        if (Convert.ToDateTime(model[i].AdCycleFromDate) <= System.DateTime.Now && Convert.ToDateTime(model[i].AdCycleToDate) >= System.DateTime.Now)
                        {
                            li.Add(model[i]);
                        }
                        else
                        {
                            list.Add(model[i]);
                        }


                    }

                }
                if (clsName == "one")
                {
                    return Json(list);
                }
                else { return Json(li); }

            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult Order(string Sort, string Classname)
        {
            try
            {
                IAphidTise aphids = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<AdvertisementModel> li = new List<AdvertisementModel>();
                List<AdvertisementModel> list = new List<AdvertisementModel>();

                var model = aphids.SortingOrd(userID, Sort);
                if (model != null)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        if (Convert.ToDateTime(model[i].AdCycleFromDate) <= System.DateTime.Now && Convert.ToDateTime(model[i].AdCycleToDate) >= System.DateTime.Now)
                        {
                            li.Add(model[i]);
                        }
                        else
                        {
                            list.Add(model[i]);
                        }


                    }
                }
                if (Classname == "one")
                {
                    return Json(list);
                }
                else { return Json(li); }
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
                if (ImgByte != null)
                {
                    byte[] objByteArray = ImgByte;
                    return File(objByteArray, "image/jpeg");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult BeforePlayAdd(string TrackNo)
        {
            AdvertisementModel reec = new AdvertisementModel();
            try
            {
                IAphidTise aphidTise = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var data = aphidTise.fetch_record(TrackNo);
               
                if (data != null)
                {
                    reec.TrackingNumber = data.TrackingNumber;
                    reec.Title = data.Title;
                    reec.AdHyperLinkUrl = data.AdHyperLinkUrl;
                    reec.AdVideo = data.AdVideo;
                    reec.PriceToDisplay = data.PriceToDisplay;
                    reec.CreditsID = data.CreditsID;
                    reec.AdInformation = data.AdInformation;
                }
                else 
                {
                    
                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            return View(reec);
        }

        public ActionResult EnteringMatrixAdd(string TrackNo)
        {
            AdvertisementModel reec = new AdvertisementModel();
            try
            {
                IAphidTise aphidTise = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var data = aphidTise.fetch_record(TrackNo);
                if (data != null)
                {
                    reec.TrackingNumber = data.TrackingNumber;
                    reec.Title = data.Title;
                    reec.AdHyperLinkUrl = data.AdHyperLinkUrl;
                    reec.AdVideo = data.AdVideo;
                    reec.PriceToDisplay = data.PriceToDisplay;
                    reec.CreditsID = data.CreditsID;
                    reec.AdInformation = data.AdInformation;
                }
                else 
                {
                    
                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            return View(reec);
        }

        public ActionResult InsideMatrixSearchAdd(string TrackNo)
        {
            AdvertisementModel reec = new AdvertisementModel();
            try
            {
                IAphidTise aphidTise = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var data = aphidTise.fetch_record(TrackNo);
                if (data != null)
                {
                    reec.Title = data.Title;
                    reec.AdHyperLinkUrl = data.AdHyperLinkUrl;
                    reec.AdVideo = data.AdVideo;
                    reec.PriceToDisplay = decimal.Round(decimal.Parse(data.PriceToDisplay), 2).ToString();
                    reec.CreditsID = data.CreditsID;
                    reec.AdInformation = data.AdInformation;
                    reec.TrackingNumber = data.TrackingNumber;
                }
                else 
                {
                    
                }
            }
            catch 
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            return View(reec);
        }

        public ActionResult SearchBarAdd(string TrackNo)
        {
            AdvertisementModel reec = new AdvertisementModel();
            try
            {
                IAphidTise aphidTise = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var data = aphidTise.fetch_record(TrackNo);
                if (data != null)
                {
                    reec.TrackingNumber = data.TrackingNumber;
                    reec.Title = data.Title;
                    reec.AdHyperLinkUrl = data.AdHyperLinkUrl;
                    reec.AdPicture = data.AdPicture;
                    reec.PriceToDisplay = decimal.Round(decimal.Parse(data.PriceToDisplay),2).ToString();
                    reec.CreditsID = data.CreditsID;
                    reec.AdInformation = data.AdInformation;

                }
                else 
                {
                    
                }
            }
            catch 
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            return View(reec);
        }

        [HttpPost]
        public ContentResult vidtodb()
        {
            HttpPostedFileBase hpf = null;
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                hpf.SaveAs(Server.MapPath(savedFileName));
                songpath = savedFileName;
            }

            //return Json(songpath);
            return Content("{\"name\":\"" + songpath + "\"}");
        }

        [HttpPost]
        public ContentResult logotodb()
        {
            HttpPostedFileBase hpf = null;
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                hpf.SaveAs(Server.MapPath(savedFileName));
                logopath = savedFileName;
            }

            //return Json(songpath);
            return Content("{\"name\":\"" + logopath + "\"}");
        }

        [HttpPost]
        public ContentResult imgtodb()
        {
            HttpPostedFileBase hpf = null;
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                hpf.SaveAs(Server.MapPath(savedFileName));
                imgpath = savedFileName;
            }

            //return Json(songpath);
            return Content("{\"name\":\"" + imgpath + "\"}");
        }


        public ActionResult PaymentPage()
        {
            return View();
        
        }

        [HttpPost]
        public ActionResult InsideMatrixSearchAdd(AdvertisementModel objet)
        {
            bool result = false;
            AphidTiseGenerateAds record = new AphidTiseGenerateAds();
            try
            {
                IAphidTise aphidTise = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                if (objet.TrackingNumber != null)
                {
                    if (songpath != "")
                    {
                        objet.AdVideo = songpath;
                    }

                    //if (imgpath != "")
                    //{
                    //    objet.AdPicture = imgpath;
                    //}
                    result = aphidTise.insideSearch(objet);
                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            if (result == true)
            {
                return View("PaymentPage");
            }
            else
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        [HttpPost]
        public ActionResult SearchBarAdd(AdvertisementModel objet)
        {
            bool result = false;
            AphidTiseGenerateAds record = new AphidTiseGenerateAds();
            try
            {
                IAphidTise aphidTise = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                if (objet.TrackingNumber != null)
                {
                    if(imgpath!="")
                    {
                        objet.AdPicture = imgpath;
                    }
                    result = aphidTise.modify_searchbaradd(objet);
                }

            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            if (result == true)
            {
                return View("PaymentPage");
            }
            else 
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        [HttpPost]
        public ActionResult EnteringMatrixAdd(AdvertisementModel objet)
        {
            bool result = false;
            AphidTiseGenerateAds record = new AphidTiseGenerateAds();
            try
            {
                IAphidTise aphidTise = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                if (objet.TrackingNumber != null)
                {
                    if (songpath != "")
                    {
                        objet.AdVideo = songpath;
                    }

                    //if (imgpath != "")
                    //{
                    //    objet.AdPicture = imgpath;
                    //}
                    result = aphidTise.entermatrixadd(objet);
                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            if (result == true)
            {
                return View("PaymentPage");
            }
            else
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        [HttpPost]
        public ActionResult BeforePlayAdd(AdvertisementModel objet)
        {
            bool result = false;
            AphidTiseGenerateAds record = new AphidTiseGenerateAds();
            try
            {
                IAphidTise aphidTise = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                if (objet.TrackingNumber != null)
                {
                    if (songpath != "")
                    {
                        objet.AdVideo = songpath;
                    }

                    //if (imgpath != "")
                    //{
                    //    objet.AdPicture = imgpath;
                    //}
                    result = aphidTise.beforeplayadd(objet);
                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            if (result == true)
            {
                return View("PaymentPage");
            }
            else
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult GetAdHistory()
        {
            try
            {
                IAphidTise AphidAds = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<AdvertisementModel> li = new List<AdvertisementModel>();
                List<AdvertisementModel> list = new List<AdvertisementModel>();

                var data = AphidAds.GetPostedDataResult(userID);
                if (data != null)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (Convert.ToDateTime(data[i].AdCycleFromDate) <= System.DateTime.Now && Convert.ToDateTime(data[i].AdCycleToDate) >= System.DateTime.Now)
                        {
                            li.Add(data[i]);
                        }
                        else
                        {
                            list.Add(data[i]);
                        }


                    }


                }
                return Json(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult GetActiveAds()
        {
            try
            {
                IAphidTise AphidAds = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<AdvertisementModel> li = new List<AdvertisementModel>();


                var data = AphidAds.GetPostedDataResult(userID);
                if (data != null)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (Convert.ToDateTime(data[i].AdCycleFromDate) <= System.DateTime.Now && Convert.ToDateTime(data[i].AdCycleToDate) >= System.DateTime.Now)
                        {
                            li.Add(data[i]);
                        }


                    }


                }
                return Json(li);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
 
        }

        public ActionResult EditActiveAd()
        {
            try
            {
                IAphidTise AphidAds = DependencyResolver.Current.GetService<IAphidTise>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<AdvertisementModel> li = new List<AdvertisementModel>();

                var data = AphidAds.GetPostedDataResult(userID);
                if (data != null)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (Convert.ToDateTime(data[i].AdCycleFromDate) <= System.DateTime.Now && Convert.ToDateTime(data[i].AdCycleToDate) >= System.DateTime.Now)
                        {
                            li.Add(data[i]);
                        }


                    }


                }
                return Json(li);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
 
        }

    }
}
