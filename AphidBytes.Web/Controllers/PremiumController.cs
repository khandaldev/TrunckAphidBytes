using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using FileUploadMVC4.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AphidBytes.Web.Models;
using System.Text;
//using Spire.Pdf;
//using Spire.Pdf.Graphics;
using Microsoft.WindowsAPICodePack.Shell;

using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing.Imaging;
using AphidBytes.Web.App_Code;
using AphidBytes.Web.Session_Helper;
using AphidBytes.Web.Web;
using AphidBytes.Core.Utility;
using AphidBytes.Core.Extensions;

namespace AphidBytes.Web.Controllers
{
    [SessionHelper]
    public class PremiumController : AphidController
    {
        UserTool obvUserTools;
        ToolsModel obvToolsModel;
        AllTools obvAllTools;

        public static byte[] ImgByte;
        static int val1 = 0;
        static string val2 = "";
        static string nn = "";
        static string songname;
        string IntrepputedAudioPath = null;
        static string img = null;
        static string imagebyte1 = null;
        static string imagebyte2 = null;
        static string imagebyte = null;
        static string imagename;
        static string trackNo = "";
        static string interrupteaudioname;
        static string songpath = null;
        static byte[] videobyte = null;
        static string videopath = null;
        static string session;
        static string intphoto = null;
        static string artphototitle = "";
        static string imagepath = null;
        static string ZipPath = null;
        static string FilePhoto = null;
        static string imagename1;
        string pdfpath;
        static byte[] ZipArray;

        private readonly IPremium _premium;
        private readonly ICommon _cmn;
        public class ImageNames
        {
            public string One { get; set; }
            public string Two { get; set; }
            public string Three { get; set; }
            public bool? SelectedImage1 { get; set; }
            public bool? SelectedImage2 { get; set; }
            public bool? SelectedImage3 { get; set; }
            public bool DefaultImage { get; set; }
            public string ImagePAth1 { get; set; }
            public string ImagePAth2 { get; set; }
            public string ImagePAth3 { get; set; }
        }
        public PremiumController()
        {
            _premium = DependencyResolver.Current.GetService<IPremium>();
            _cmn = DependencyResolver.Current.GetService<ICommon>();
        }

        public class AudioNames
        {
            public string Audio1 { get; set; }
            public string Audio2 { get; set; }
            public string Audio3 { get; set; }
            public bool? SelectedAudio1 { get; set; }
            public bool? SelectedAudio2 { get; set; }
            public bool? SelectedAudio3 { get; set; }
            public bool DefaultAudio { get; set; }
            public string AudioPath1 { get; set; }
            public string AudioPath2 { get; set; }
            public string AudioPath3 { get; set; }
        }
        Guid userID;
        PremiumAccountViewModel PremiumData = null;
        PremiumAccountViewModel PremiumData1 = null;
        ImageNames imgobj;
        AudioNames audobj;

        static List<PremiumGenerateCloneModel> ptvar1 = new List<PremiumGenerateCloneModel>(); 
        static List <InterruptedFileModel> ptvar2 = new List<InterruptedFileModel>();
        static List <CreateLinkPostModel> ptvar3 = new List<CreateLinkPostModel>();
        static List <AllGenerateCloneModel> ptvar4 = new List<AllGenerateCloneModel>();

        public ActionResult PremiumAccountInfo()
        {
            try
            {
                if (AphidSession.Current.AuthenticatedUser?.Identity?.Username != null)
                {
                    session = AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString();
                }
                if (Session["ImageSession"] != null)
                {
                    imgobj = Session["ImageSession"] as ImageNames;
                }
                if (Session["AudioSession"] != null)
                {
                    audobj = Session["AudioSession"] as AudioNames;
                }

               
                if (AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null)
                {
                    userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                }

                PremiumData = _premium.GetPremiumAccountInfo(userID);
                var result = PremiumData.IsActive;
                if ((PremiumData.RecoveryEmail != null) && (PremiumData.RecoveryEmail != ""))
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
                Session["EmailAddress"] = PremiumData.EmailAddress;
                ImgByte = PremiumData.ProfilePictureInBytes;
                if (PremiumData.DefaultSelectedAud == true)
                {
                    PremiumData.DefaultSelectedAudio = "Default";
                }
                else
                {
                    if (PremiumData.SelectedAudio1 == true)
                    {
                        PremiumData.DefaultSelectedAudio = PremiumData.Audio1Name;
                    }
                    if (PremiumData.SelectedAudio2 == true)
                    {
                        PremiumData.DefaultSelectedAudio = PremiumData.Audio2Name;
                    }
                    if (PremiumData.SelectedAudio3 == true)
                    {
                        PremiumData.DefaultSelectedAudio = PremiumData.Audio3Name;
                    }
                }
                if (PremiumData.DefaultSelectedImg == true)
                {
                    PremiumData.DefaultSelectedImage = "Default";
                }
                else
                {
                    if (PremiumData.SelectedImage1 == true)
                    {
                        PremiumData.DefaultSelectedImage = PremiumData.Image1Name;
                    }
                    if (PremiumData.SelectedImage2 == true)
                    {
                        PremiumData.DefaultSelectedImage = PremiumData.Image2Name;
                    }
                    if (PremiumData.SelectedImage3 == true)
                    {
                        PremiumData.DefaultSelectedImage = PremiumData.Image3Name;
                    }
                }


                if (audobj == null)
                {
                    audobj = new AudioNames();
                    if (PremiumData.Audio1Name != null)
                    {
                        audobj.Audio1 = PremiumData.Audio1Name;
                        audobj.SelectedAudio1 = PremiumData.SelectedAudio1;
                        Session["AudioSession"] = audobj;
                    }
                    if (PremiumData.Audio2Name != null)
                    {
                        audobj.Audio2 = PremiumData.Audio2Name;
                        audobj.SelectedAudio2 = PremiumData.SelectedAudio2;
                        Session["AudioSession"] = audobj;
                    }
                    if (PremiumData.Audio3Name != null)
                    {
                        audobj.Audio3 = PremiumData.Audio3Name;
                        audobj.SelectedAudio3 = PremiumData.SelectedAudio3;
                        Session["AudioSession"] = audobj;
                    }
                }
                if (imgobj == null)
                {
                    imgobj = new ImageNames();
                    if (PremiumData.Image1Name != null)
                    {
                        imgobj.One = PremiumData.Image1Name;
                        imgobj.SelectedImage1 = PremiumData.SelectedImage1;
                        Session["ImageSession"] = imgobj;
                    }
                    if (PremiumData.Image2Name != null)
                    {
                        imgobj.Two = PremiumData.Image2Name;
                        imgobj.SelectedImage2 = PremiumData.SelectedImage2;
                        Session["ImageSession"] = imgobj;
                    }
                    if (PremiumData.Image3Name != null)
                    {
                        imgobj.Three = PremiumData.Image3Name;
                        imgobj.SelectedImage3 = PremiumData.SelectedImage3;
                        Session["ImageSession"] = imgobj;
                    }
                }
                
                string count = _cmn.GetNewCount(userID);
                PremiumData.NewCount = count;
                string decryptpwd = CryptorEngine.Decrypt(PremiumData.Password, true);
                string decryptpwdd = CryptorEngine.Decrypt(PremiumData.ConfirmPassword, true);
                PremiumData.Password = decryptpwd;
                PremiumData.ConfirmPassword = decryptpwdd;
                if (!PremiumData.IsActive.HasValue || !PremiumData.IsActive.Value)
                {
                    PremiumData.Validation = new ValidationModel();
                    PremiumData.Validation.AddWarning("An email to verify your account was sent, check your Inbox or Spam folder");
                }
                return View(PremiumData);
            }
            catch (Exception exx)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PremiumAccountInfo(PremiumAccountViewModel premiunModel)
        {
            try
            {
                ImageNames imgobj = Session["ImageSession"] as ImageNames;
                AudioNames audobj = Session["AudioSession"] as AudioNames;
                string SelectedAudio = null;
                string SelectedImage = null;
                if (Session["SelectedAduio"] != null)
                {
                    SelectedAudio = Session["SelectedAduio"].ToString();
                }
                if (Session["SelectedImage"] != null)
                {
                    SelectedImage = Session["SelectedImage"].ToString();
                }
                ModelState.RemoveFor<PremiumAccountViewModel>(x => x.Password);
                ModelState.RemoveFor<PremiumAccountViewModel>(x => x.ConfirmPassword);
                ModelState.RemoveFor<PremiumAccountViewModel>(x => x.PostalCode);
                ModelState.RemoveFor<PremiumAccountViewModel>(x => x.Region);
                ModelState.RemoveFor<PremiumAccountViewModel>(x => x.City);
                ModelState.RemoveFor<PremiumAccountViewModel>(x => x.AddressLine1);
                ModelState.RemoveFor<PremiumAccountViewModel>(x => x.AddressLine2);
                ModelState.RemoveFor<PremiumAccountViewModel>(x => x.NameOnCard);
                ModelState.RemoveFor<PremiumAccountViewModel>(x => x.CSV);
                ModelState.RemoveFor<PremiumAccountViewModel>(x => x.ExpiryMonth);
                if (ModelState.IsValid)
                {

                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                    PremiumAccountViewModel PremiumData = _premium.GetPremiumAccountInfo(userID);

                    premiunModel.PremiumUserID = userID;
                    string encryptPwd = CryptorEngine.Encrypt(premiunModel.Password, true);

                    //Convert profile image in byte array
                    premiunModel.Password = encryptPwd;
                    if (Session["AudioSession"] != null)
                    {
                        if (Session["SelectedAduio"] != null)
                        {


                            if (audobj.Audio1 != null)
                            {
                                premiunModel.Audio1Name = audobj.Audio1;
                                // premiunModel.Audio1 = audobj.Audio1Byte;
                                if (SelectedAudio == audobj.Audio1)
                                {
                                    premiunModel.SelectedAudio1 = true;
                                }
                                else { premiunModel.SelectedAudio1 = false; }
                            }
                            if (audobj.Audio2 != null)
                            {
                                premiunModel.Audio2Name = audobj.Audio2;
                                //premiunModel.Audio2 = audobj.Audio2Byte;
                                if (SelectedAudio == audobj.Audio2)
                                {
                                    premiunModel.SelectedAudio2 = true;
                                }
                                else { premiunModel.SelectedAudio2 = false; }
                            }
                            if (audobj.Audio3 != null)
                            {
                                premiunModel.Audio3Name = audobj.Audio3;
                                // premiunModel.Audio3 = audobj.Audio3Byte;
                                if (SelectedAudio == audobj.Audio3)
                                {
                                    premiunModel.SelectedAudio3 = true;
                                }
                                else { premiunModel.SelectedAudio3 = false; }
                            }

                        }
                    }
                    if (Session["ImageSession"] != null)
                    {
                        if (Session["SelectedImage"] != null)
                        {


                            if (imgobj.One != null)
                            {
                                premiunModel.Image1Name = imgobj.One;
                                // premiunModel.Image1 = imgobj.OneByte;
                                if (SelectedImage == imgobj.One)
                                {
                                    premiunModel.SelectedImage1 = true;
                                }
                                else { premiunModel.SelectedImage1 = false; }

                            }
                            if (imgobj.Two != null)
                            {
                                premiunModel.Image2Name = imgobj.Two;
                                //premiunModel.Image2 = imgobj.TwoByte;
                                if (SelectedImage == imgobj.Two)
                                {
                                    premiunModel.SelectedImage2 = true;
                                }
                                else { premiunModel.SelectedImage2 = false; }
                            }
                            if (imgobj.Three != null)
                            {
                                premiunModel.Image3Name = imgobj.Three;
                                // premiunModel.Image3 = imgobj.ThreeByte;
                                if (SelectedImage == imgobj.Three)
                                {
                                    premiunModel.SelectedImage3 = true;
                                }
                                else { premiunModel.SelectedImage3 = false; }
                            }
                        }
                    }
                    if (premiunModel.ProfilePicture != null)
                    {
                        byte[] img;
                        using (Stream inputStream = premiunModel.ProfilePicture.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            img = memoryStream.ToArray();
                            premiunModel.ProfilePictureInBytes = img;

                        }
                    }
                    premiunModel.AddressID = PremiumData.AddressID;
                    premiunModel.BankAccountID = PremiumData.BankAccountID;
                    premiunModel.SecurityQuestionID = PremiumData.SecurityQuestionID;
                    if (Session["AudioSession"] != null)
                    {
                        premiunModel.Audio1Path = audobj.AudioPath1;
                        premiunModel.Audio2Path = audobj.AudioPath2;
                        premiunModel.Audio3Path = audobj.AudioPath3;
                    }
                    if (Session["ImageSession"] != null)
                    {
                        premiunModel.Image1Path = imgobj.ImagePAth1;
                        premiunModel.Image2Path = imgobj.ImagePAth2;
                        premiunModel.Image3Path = imgobj.ImagePAth3;
                    }
                    byte[] audio, imageByte;

                    if (Session["AudioFile"] == "Default")
                    {
                        string savedFileName = Path.Combine(Server.MapPath("/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3"));
                        audio = System.IO.File.ReadAllBytes(savedFileName);
                        audobj.DefaultAudio = true;
                    }
                    else
                    {

                    }

                    if (Session["ImageFile"] == "Default")
                    {
                        string savedFileName = Path.Combine(Server.MapPath("/DefaultFiles/Logo_Tech_.png"));
                        imgobj.DefaultImage = true;
                    }
                    else
                    {

                    }
                    string rec = premiunModel.RecoveryEmail;
                    string Email = _premium.FetchEmailRecord(userID, rec);
                    bool updatePremium = _premium.UpdatePremiumAccountInfo(premiunModel);
                    if (updatePremium)
                    {
                        if ((premiunModel.RecoveryEmail != null) && (premiunModel.RecoveryEmail != ""))
                        {
                            if ((PremiumData.IsActive == null) || (PremiumData.IsActive == false))
                            {
                                if (Email != "Already EmailId Present")
                                {
                                    Guid token = Guid.NewGuid();
                                    Email mail = new Email();
                                    mail.sendMaill(premiunModel.PremiumUserID, premiunModel.RecoveryEmail, "Basic", token, "", "VerifyEmail");
                                }
                            }
                        }


                        userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                        PremiumData1 = _premium.GetPremiumAccountInfo(userID);
                        ImgByte = PremiumData1.ProfilePictureInBytes;
                        Session["ImageSession"] = null;
                        Session["AudioSession"] = null;
                        Session["AudioFile"] = null;
                        Session["ImageFile"] = null;

                        string count = _cmn.GetNewCount(userID);
                        PremiumData1.NewCount = count;
                        string decryptpwd = CryptorEngine.Decrypt(PremiumData1.Password, true);
                        string decryptpwdd = CryptorEngine.Decrypt(PremiumData1.ConfirmPassword, true);
                        PremiumData1.Password = decryptpwd;
                        PremiumData1.ConfirmPassword = decryptpwdd;
                        var result = PremiumData1.IsActive;
                        if ((PremiumData1.RecoveryEmail != null) && (PremiumData1.RecoveryEmail != ""))
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

                }
                return View(PremiumData1);
            }
            catch (Exception ex)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }

        }
        public ActionResult ChangePassword()
        {
            var model = new PremiumAccountViewModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                model = _premium.GetPremiumAccountInfo(userID);
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
        public ActionResult ChangePassword(PremiumAccountViewModel premiumModel)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                var premiumData = _premium.GetPremiumAccountInfo(userID);
                premiumModel.PremiumUserID = userID;
                var encryptPwd = CryptorEngine.Encrypt(premiumModel.Password, true);
                var success = _cmn.UpdatePassword(userID, encryptPwd);
                if (!success)
                {
                    premiumData.Validation.AddError("Unable to change the current password");
                }
                else
                {
                    premiumData.Validation.AddInformation("Successfully changed the password");
                }

                return View(premiumData);
            }
            catch (Exception)
            {
                return RedirectToAction("BasicAccountInfo");
            }
        }
        public ActionResult CreditCardInfo()
        {
            var model = new PremiumAccountViewModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                model = _premium.GetPremiumAccountInfo(userID);
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
        public ActionResult CreditCardInfo(PremiumAccountViewModel basicmodel, string stripeToken)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                var premiumData = _premium.GetPremiumAccountInfo(userID);
                var success = _cmn.UpdateStripeCard(userID, stripeToken);
                if (!success)
                {
                    premiumData.Validation.AddError("Unable to change the credit card information on file");
                }
                else
                {
                    premiumData.Validation.AddInformation("Successfully changed your credit card information");
                }

                return View(premiumData);
            }
            catch (Exception)
            {
                return RedirectToAction("BasicAccountInfo");
            }
        }

        public CaptchaModel ShowCaptchaImage()
        {
            return new CaptchaModel();
        }

        public List<BindDropDown> DropBindAudio()
        {
            List<BindDropDown> li = new List<BindDropDown>();
            try
            {
             
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                li = _premium.BindDropAudio(userID);
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in li)
                {
                    list.Add(new SelectListItem
                    {
                        Text = item.Value,
                        Value = item.id.ToString()

                    });
                }
                ViewBag.AudioFiles = list;

            }
            catch (Exception)
            {


            }
            return li;
        }

        public ActionResult GetAudio()
        {
            if (AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null)
            {
                try
                {
                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                   System.Collections.Generic.IEnumerable<AudioFileModel> audiofile = _cmn.GetAudioFiles(userID, 2).ToList();
                    //dynamic data = _db.tblAudioInterruptions.ToList();
                    var dadd = audiofile.ToList();
                    string mp = null;
                    string nae = "";
                    List<AudioFileName> li = new List<AudioFileName>();
                    for (int i = 0; i < dadd.Count(); i++)
                    {
                        mp = dadd[i].AudioFile;
                        switch (i)
                        {
                            case 0:
                                ViewBag.SoundPath1 = "/TempBasicImages/" + dadd[i].AudioFileName + "";
                                li.Add(new AudioFileName { name = dadd[i].AudioFileName });
                                break;
                            case 1:
                                ViewBag.SoundPath2 = "/TempBasicImages/" + dadd[i].AudioFileName + "";
                                li.Add(new AudioFileName { name = dadd[i].AudioFileName });
                                break;
                            case 2:
                                ViewBag.SoundPath3 = "/TempBasicImages/" + dadd[i].AudioFileName + "";
                                li.Add(new AudioFileName { name = dadd[i].AudioFileName });
                                break;
                            case 3:
                                ViewBag.SoundPath4 = "/TempBasicImages/" + dadd[i].AudioFileName + "";
                                li.Add(new AudioFileName { name = dadd[i].AudioFileName });
                                break;
                               default:
                                break;
                        }

                        using (FileStream fs = System.IO.File.Create(Path.Combine(Server.MapPath(@"\TempBasicAudio\" + dadd[i].AudioFileName + ""))))
                        {
                            if (mp != null)
                            {
                                byte[] audio = System.IO.File.ReadAllBytes(mp);
                                fs.Write(audio, 0, audio.Length);
                            }
                        }
                    }
                    var data = li;

                    return Json(data, "success", JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return this.Json(new { success = false });
                }

            }
            else
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult ShowUserTools()
        {
            try
            {
                obvToolsModel = new ToolsModel();
                obvToolsModel.AllToolsInfo = new List<AllTools>();
                obvToolsModel.UserTools = new List<UserTool>();

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                // var res = pre.RetPremiumToolsInfo();
                var res1 = _premium.RetUserTools(userID);
                foreach (var item in res1)
                {
                    obvAllTools = new AllTools();
                    obvAllTools.ImagePath = item.ImagePath;
                    obvAllTools.IsActive = item.IsActive;
                    obvAllTools.ToolId = item.ToolId;
                    obvAllTools.ToolINfo = item.ToolINfo;
                    obvAllTools.ToolName = item.ToolName;

                    obvToolsModel.AllToolsInfo.Add(obvAllTools);
                }



            }
            catch
            {

            }
            return View(obvToolsModel);
        }
        public ActionResult PostingDetail(string Trackingnumber)
        {
            try
            {
              
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<Bytetracker> li = new List<Bytetracker>();
                List<Bytetracker> list = new List<Bytetracker>();
                li = _premium.Getpostingdata(userID, Trackingnumber);
                //list.Add(new Bytetracker());

                for (int i = 0; i < li.Count; i++)
                {
                    string date = li[i].Date.ToString();
                    string[] dd = date.Split(' ');
                    li[i].DateShow = dd[0];
                    if (li[i].poststatus == "True")
                    {
                        list.Add(new Bytetracker()
                        {
                            Title = li[i].Title,
                            Channel = li[i].Channel,
                            Category = li[i].Category,
                            NoOfclones = li[i].NoOfclones,
                            Views = li[i].Views,
                            Downloads = li[i].Downloads,
                            FileSize = li[i].FileSize,
                            TrackingNumber = li[i].TrackingNumber,
                            DateShow = li[i].DateShow

                        });
                    }


                }


                return Json(list);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult PremiumTools()
        {
            try
            {
                obvToolsModel = new ToolsModel();
                obvToolsModel.AllToolsInfo = new List<AllTools>();
                obvToolsModel.UserTools = new List<UserTool>();
               
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var res = _premium.RetPremiumToolsInfo();
                var res1 = _premium.RetUserTools(userid);

                foreach (var item in res)
                {
                    obvAllTools = new AllTools();
                    obvAllTools.ImagePath = item.ImagePath;
                    obvAllTools.IsActive = item.IsActive;
                    obvAllTools.ToolId = item.ToolId;
                    obvAllTools.ToolINfo = item.ToolINfo;
                    obvAllTools.ToolName = item.ToolName;

                    obvToolsModel.AllToolsInfo.Add(obvAllTools);
                }
                foreach (var item in res1)
                {
                    obvUserTools = new UserTool();
                    obvUserTools.ToolId = item.ToolId;
                    obvToolsModel.UserTools.Add(obvUserTools);
                }

                return View(obvToolsModel);

            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }


        }

        [HttpPost]
        public ActionResult PremiumTools(int ID, string text)
        {
            try
            {
                AllTools model = new AllTools();
                UserTool usermodel = new UserTool();
                bool result = false;
                Filecontent filemodel = new Filecontent();
                try
                {


                   
                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    model.ToolId = ID;
                    model.ToolName = text;
                    usermodel.userid = userID;

                    result = _premium.InsertPremiumTools(model, usermodel);

                    //foreach (var item in result)
                    //{

                    //}

                    ViewBag.Tool = result;


                    return Json(result);
                    // return View(result);
                }
                catch
                {
                    //return View();
                }
                Session["Tools"] = result;
                return Json(result);
                //return View(result);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult WriterContent(string content)
        {
            try
            {
                Session["Content"] = content;
               
                List<string> list = new List<string>();
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var result = _premium.RetPremiumToolFileContent(userid, content);

                //ViewBag.Text = result;
                return Json(result);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult WritersPad(string text)
        {
            try
            {
              
                List<string> li = new List<string>();
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var res = _premium.RetPremiumToolFile(userid);
                for (int i = 0; i < res.Count; i++)
                {
                    li.Add(res[i]);
                }

                ViewBag.FileNames = li;


                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        [HttpPost]
        public ActionResult WriterPad(string content, int id1, string file)
        {

            try
            {
                AllTools model = new AllTools();
                UserTool usermodel = new UserTool();
                Filecontent filemodel = new Filecontent();
                try
                {
                   
                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                  
                    filemodel.FileName = file;

                    filemodel.ToolContent = content;
                    model.ToolId = id1;
                    usermodel.ToolId = id1;
                    filemodel.ToolId = id1;
                    usermodel.CreatedOn = DateTime.Now;
                    usermodel.userid = userID;
                    var res = _premium.InsertPremiumToolFile(model, usermodel, filemodel);
                    return Json(res == false ? "File Already Exists" : "Success");
                    
                }
                catch
                {
                    return Json("Failed");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult Deletefile(string content, int id1)
        {
            AllTools model = new AllTools();
            Filecontent filemodel = new Filecontent();
            UserTool usermodel = new UserTool();

            try
            {
                
                Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                filemodel.ToolId = id1;
                filemodel.ToolContent = content;
                filemodel.Userid = user;
                filemodel.FileName = Session["Content"].ToString();
                _premium.Deletefilecontent(filemodel, usermodel);
                return Json("Success");
            }
            catch
            {
                return Json("failed");
            }

        }

        public ActionResult DashBoard()
        {
            return View();
        }

        public static double Convert100NanosecondsToMilliseconds(double nanoseconds)
        {
            // One million nanoseconds in 1 millisecond, but we are passing in 100ns units...
            return nanoseconds * 0.0001;
        }

        static string linkpath = "";
        static string linktitle = "";
        static string linktag = "";
        static string linkcategory = "";
        public string selectchanneltoflood1(string trackid, string cat)
        {
            try
            {
              
                List<AllGenerateCloneModel> li = _premium.GetUploadfile(trackid);
                Session["FileSize"] = li[0].filesize;
                if (li.Count != 0)
                {
                    if ((li[0].CatID == 1) && (cat == "Music"))
                    {
                        if (li[0].UploadFilePath == null || li[0].UploadFilePath=="")
                        {
                            linkpath = li[0].AudioFilePath;
                        }
                        else
                        {
                            linkpath = li[0].UploadFilePath;
                        }
                        linktag = li[0].Tag;
                        linktitle = li[0].Title;
                        string displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + linkpath).Split('_')[1];
                        Session["LinkPath"] = displaypath;
                        Session["LinkTitle"] = linktitle;
                        Session["Trackid"] = trackid;
                        return "MusicPosting";
                    }
                    else if ((li[0].CatID == 2) && (cat == "Video"))
                    {
                        if (li[0].UploadFilePath == null)
                            linkpath = li[0].VideoFilePath;
                        else
                        linkpath = li[0].UploadFilePath;
                        linktag = li[0].Tag;
                        linktitle = li[0].Title;
                        linkcategory = li[0].VideoCategory;
                        string displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + linkpath).Split('_')[1];
                        Session["LinkPath"] = displaypath;
                        Session["LinkTitle"] = linktitle;
                        Session["Trackid"] = trackid;
                        return "VideoPosting";
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
                        return "photoArtPosting";
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
                        return "EbookPosting";
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
                else
                    return "PremiumLinkPost";
            }
            catch (Exception ex)
            {
                return "PremiumLinkPost";
            }
        }
        public string TwitterPost(string type)
        {
            try
            {
                TwitterModel face = new TwitterModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid id = new Guid(session);
                try
                {
                    string tit = linktitle.ToString();
                    string pat = linkpath.ToString();
                    string filesize = Session["FileSize"].ToString();
                    if ((tit == "") || (pat == ""))
                    {
                        throw (new Exception("link error"));
                    }
                    else
                    {
                        string[] dat = new string[] { linkpath.ToString(), linktitle.ToString() };
                        string value = face.Post_on_Twitter(id, "Twitter", dat, type, Session["Trackid"].ToString(), filesize);
                        if (value == "inserted")
                            return "Successfull";
                        else if (value == "deleted")
                        {
                            Session["tw_status"] = null;
                            return "Index";
                        }
                        else if (value == "Timedout")
                        {
                            return "Error";
                        }
                        else
                        {
                            return value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "PremiumLinkPost";
                }
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
                FlickerModel flick = new FlickerModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid id = new Guid(session);
                try
                {
                    string tit = linktitle.ToString();
                    string pat = linkpath.ToString();
                    string filesize = Session["FileSize"].ToString();
                    if ((tit == "") || (pat == ""))
                    {

                        throw (new Exception("link error"));
                    }
                    else
                    {
                        string[] dat = new string[] { linkpath.ToString(), linktitle.ToString(), linktag.ToString() };
                        string value = flick.Post_on_Flicker(id, "Flicker", dat, type, Session["Trackid"].ToString(), filesize);
                        if (value == "inserted")
                            return "Successfull";
                        else if (value == "deleted")
                        {
                            Session["flicker_status"] = null;
                            return "Index";
                        }
                        else
                        {
                            return value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "PremiumLinkPost";
                }
            }
            catch
            {
                return "login error";
            }
        }
        public string FacebookPost(string type)
        {
            try
            {
                FaceBookModel face = new FaceBookModel();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid id = new Guid(session);
                try
                {
                    string tit = linktitle.ToString();
                    string pat = linkpath.ToString();
                    string filesize = Session["FileSize"].ToString();
                    if ((tit == "") || (pat == ""))
                    {
                        throw (new Exception("link error"));
                    }
                    else
                    {
                        string[] dat = new string[] { linkpath.ToString(), linktitle.ToString() };
                        string value = face.PostTowall(id, "Facebook", dat, type, Session["Trackid"].ToString(), filesize);
                        if (value == "inserted")
                        {
                           
                            return "Successfull";
                        }
                        else if (value == "deleted")
                        {
                            Session["fb_status"] = null;
                            return "Index";
                        }
                        else if (value == "Timedout")
                        {
                            return "Error";
                        }
                        else
                        {
                           
                            return "Facebook";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "PremiumLinkPost";
                }
            }
            catch
            {
                return "login error";
            }
        }

        public string DailyMotionPost(string type)
        {
            Dailymotion daily = new Dailymotion();
            Guid id = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            try
            {
                string tit = linktitle.ToString();
                string pat = linkpath.ToString();
                string filesize = Session["FileSize"].ToString();
                if ((tit == "") || (pat == ""))
                {
                    throw (new Exception("link error"));

                }
                else
                {
                    string[] dat = new string[] { linkpath.ToString(), linktitle.ToString(), linktag.ToString() };
                    string result = daily.post(id, "DailyMotion", dat, type, Session["Trackid"].ToString(), filesize);
                    if (result == "inserted")
                        return "Successfull";
                    else if (result == "deleted")
                    {
                        Session["daily_status"] = null;
                        return "Index";
                    }
                    else if (result == "Timedout")
                    {
                        return "Error";
                    }
                    else
                    {
                        return result;
                    }
                }
            }

            catch (Exception ex)
            {
                return "PremiumLinkPost";
            }


            catch
            {
                return "login error";
            }

        }

        public string ScribdPost(string type)
        {
            ScribdModel daily = new ScribdModel();
            try
            {
                Guid id = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                try
                {
                    string tit = linktitle.ToString();
                    string pat = linkpath.ToString();
                    string filesize = Session["FileSize"].ToString();
                    if ((tit == "") || (pat == ""))
                    {
                        throw (new Exception("link error"));

                    }
                    else
                    {
                        string[] dat = new string[] { linkpath.ToString(), linktitle.ToString() };
                        string result = daily.Scribd_post(id, "Scribd", dat, type, Session["Trackid"].ToString(), filesize);
                        if (result == "inserted")
                            return "Successfull";
                        else if (result == "deleted")
                        {
                            Session["scribd_status"] = null;
                            return "Index";
                        }
                        else if (result == "Timedout")
                        {

                            return "Error";
                        }
                        else
                        {
                            return result;
                        }
                    }
                }

                catch (Exception ex)
                {
                    return "PremiumLinkPost";
                }

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
                YouTubeModel tube = new YouTubeModel();
                Guid id = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                try
                {
                    string tit = linktitle.ToString();
                    string pat = linkpath.ToString();
                    string filesize = Session["FileSize"].ToString();
                    if ((tit == "") || (pat == ""))
                    {
                        throw (new Exception("link error"));

                    }
                    else
                    {
                        string[] dat = new string[] { linkpath.ToString(), linktitle.ToString(), linktag.ToString(), linkcategory.ToString() };
                        string result = tube.Youtube_post(id, "YouTube", dat, type, Session["Trackid"].ToString(), filesize);
                        if (result == "inserted")
                            return "Successfull";
                        else if (result == "deleted")
                        {
                            Session["youtube_status"] = null;
                            return "Index";
                        }
                        else if (result == "Timedout")
                        {
                            return "Error";
                        }
                        else
                        {
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "PremiumLinkPost";
                }
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
                SoundCloudModel link = new SoundCloudModel();
                Guid id = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                try
                {
                    string tit = linktitle.ToString();
                    string pat = linkpath.ToString();
                    string filesize = Session["FileSize"].ToString();
                    if ((tit == "") || (pat == ""))
                    {
                        throw (new Exception("link error"));

                    }
                    else
                    {
                        string[] dat = new string[] { linkpath.ToString(), linktitle.ToString() };
                        string result = link.POST(id, "SoundCloud", dat, type, Session["Trackid"].ToString(), filesize);
                        if (result == "inserted")
                            return "Successfull";
                        else if (result == "deleted")
                        {
                            Session["sound_status"] = null;
                            return "Index";
                        }
                        else if (result == "Timedout")
                        {
                            return "Error";
                        }
                        else
                        {
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "PremiumLinkPost";
                }
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
                LinkedLinModel link = new LinkedLinModel();
                Guid id = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                try
                {
                    string tit = linktitle.ToString();
                    string pat = linkpath.ToString();
                    string filesize = Session["FileSize"].ToString();
                    if ((tit == "") || (pat == ""))
                    {
                        throw (new Exception("link error"));

                    }
                    else
                    {
                        string[] dat = new string[] { linkpath.ToString(), linktitle.ToString() };
                        string result = link.Post_to_link(id, "LinkedLin", dat, type, Session["Trackid"].ToString(), filesize);
                        if (result == "inserted")
                            return "Successfull";
                        else if (result == "deleted")
                        {
                            Session["link_status"] = null;
                            return "Index";
                        }
                        else if (result == "Timedout")
                        {
                            return "Error";
                        }
                        else
                        {
                            return result;
                        }
                    }
                }

                catch (Exception ex)
                {
                    return "PremiumLinkPost";
                }

            }
            catch
            {
                return "login error";
            }

        }

        public ActionResult Verification(Guid id)
        {

            PremiumAccountViewModel PremiumData = _premium.GetPremiumAccountInfo(id);
            if (PremiumData.IsActive == true)
            {
                ViewBag.Message = "you are already verified";
            }
            else
            {
                bool result = _premium.VerifyEmailAccount(id);
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
                PremiumAccountViewModel PremiumData = _premium.GetPremiumAccountInfo(userID);
                if ((PremiumData.IsActive == null) || (PremiumData.IsActive == false))
                {
                    Guid token = Guid.NewGuid();
                    Email mail = new Email();

                    mail.sendMaill(PremiumData.PremiumUserID, email, "Basic", token, "", "VerifyEmail");
                }
            }
            return RedirectToAction("basicaccountinfo", "Basic");
        }
 
        

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult GenerateClones()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new MessageModel { NewCount = Convert.ToInt32(count) };
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }


        }
        public ActionResult PremiumByteyourmusic()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new MessageModel{NewCount = Convert.ToInt32(count)};
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult InterruptVideo(string IntS, string IntF, string FileName, string Category, string Title)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                PremiumVideoInterruption ob = new PremiumVideoInterruption();
                string name = ob.VideoInterruption(userID, IntS, IntF, FileName, Category, Title, session);
                return Json(name + "&" + videopath, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public ActionResult Interruption(string IntS, string IntF, string FileName)
        {
            string intPath = AudioIntrepption(IntS, IntF, FileName);
            if (intPath == "" || intPath == null)
            {
                return Json(FileName);
            }
            return Json(intPath);

        }

        public string AudioIntrepption(string IntS, string IntF, string FileName)
        {
           
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            string guid = Guid.NewGuid().ToString();
            string intPath = "";
            if (IntS == "Default Randomized Entry" && (IntF == "No Interruption" || IntF == "Default AphidByte" || IntF == "Custom Audio Interruption"))
            {
                if (IntF == "Default AphidByte")
                {
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3")));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(FileName)));//File Selected For Interruption
                    List<byte> int1 = new List<byte>();
                    List<byte> int2 = new List<byte>();
                    List<byte> int3 = new List<byte>();

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

                    byte[] gg = int2.ToArray();
                    using (FileStream fs = System.IO.File.Create(Path.Combine(Server.MapPath(@"/TempBasicImages/" + guid + "_file.mp3"))))
                    {
                        intPath = @"/TempBasicImages/" + guid + "_file.mp3";
                        songpath = intPath;
                        if (gg != null)
                        {
                            fs.Write(gg, 0, gg.Length);
                        }
                    }
                }

                if (IntF == "Custom Audio Interruption")
                {
                    var dd = _premium.BindDropAudio(userID);
                    if (dd.Count != 0)
                    { 
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(dd[0].Path)));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songname)));//File Selected For Interruption
                    List<byte> int1 = new List<byte>();
                    List<byte> int2 = new List<byte>();
                    List<byte> int3 = new List<byte>();
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

                    byte[] gg = int2.ToArray();
                    using (FileStream fs = System.IO.File.Create(Path.Combine(Server.MapPath("/TempBasicImages/" + guid + "_file.mp3"))))
                    {
                        intPath = @"/TempBasicImages/" + guid + "_file.mp3";
                        songpath = intPath;
                        if (gg != null)
                        {
                            fs.Write(gg, 0, gg.Length);
                        }
                    }
                }
                }
            }
            if (IntS == "Producer Tag Sequence" && (IntF == "No Interruption" || IntF == "Default AphidByte" || IntF == "Custom Audio Interruption"))
            {
                if (IntF == "Default AphidByte")
                {
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3")));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(FileName)));//File Selected For Interruption
                    List<byte> int1 = new List<byte>();
                    List<byte> int2 = new List<byte>();
                    List<byte> int3 = new List<byte>();
                    List<byte> int4 = new List<byte>();
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
                    byte[] gg = int1.ToArray();
                    using (FileStream fs = System.IO.File.Create(Path.Combine(Server.MapPath("/TempBasicImages/" + guid + "_file.mp3"))))
                    {
                        intPath = @"/TempBasicImages/" + guid + "_file.mp3";
                        songpath = intPath;
                        if (gg != null)
                        {
                            fs.Write(gg, 0, gg.Length);
                        }
                    }
                }
                if (IntF == "Custom Audio Interruption")
                {
                    var dd = _premium.BindDropAudio(userID);
                    if (dd.Count != 0)
                    { 
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(dd[0].Path)));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(FileName)));//File Selected For Interruption
                    List<byte> int1 = new List<byte>();
                    List<byte> int2 = new List<byte>();
                    List<byte> int3 = new List<byte>();
                    List<byte> int4 = new List<byte>();
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
                    byte[] gg = int1.ToArray();
                    using (FileStream fs = System.IO.File.Create(Path.Combine(Server.MapPath("/TempBasicImages/" + guid + "_file.mp3"))))
                    {
                        intPath = @"/TempBasicImages/" + guid + "_file.mp3";
                        songpath = intPath;
                        if (gg != null)
                        {
                            fs.Write(gg, 0, gg.Length);
                        }
                    }
                }

            }
            }
            if (IntS == "Beginning of File" && (IntF == "No Interruption" || IntF == "Default AphidByte" || IntF == "Custom Audio Interruption"))
            {
                if (IntF == "Default AphidByte")
                {
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3")));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(FileName)));//File Selected For Interruption
                    List<byte> int1 = new List<byte>(mainAudio);
                    int1.AddRange(intreAudio);
                    byte[] gg = int1.ToArray();
                    using (FileStream fs = System.IO.File.Create(Path.Combine(Server.MapPath("/TempBasicImages/" + guid + "_file.mp3"))))
                    {
                        intPath = "/TempBasicImages/" + guid + "_file.mp3";
                        if (gg != null)
                        {
                            fs.Write(gg, 0, gg.Length);
                        }
                    }
                }
                if (IntF == "Custom Audio Interruption")
                {
                    var dd = _premium.BindDropAudio(userID);
                    if (dd.Count != 0)
                    { 
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(dd[0].Path)));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(FileName)));//File Selected For Interruption
                    List<byte> int1 = new List<byte>(mainAudio);
                    int1.AddRange(intreAudio);
                    byte[] gg = int1.ToArray();
                    using (FileStream fs = System.IO.File.Create(Path.Combine(Server.MapPath(@"/TempBasicImages/" + guid + "_file.mp3"))))
                    {
                        intPath = @"/TempBasicImages/" + guid + "_file.mp3";
                        songpath = intPath;
                        if (gg != null)
                        {
                            fs.Write(gg, 0, gg.Length);
                        }
                    }
                }
               }
            }
            if (IntS == "Ending of File" && (IntF == "No Interruption" || IntF == "Default AphidByte" || IntF == "Custom Audio Interruption"))
            {
                if (IntF == "Default AphidByte")
                {
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3")));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(FileName)));//File Selected For Interruption
                    List<byte> int1 = new List<byte>(intreAudio);
                    int1.AddRange(mainAudio);
                    byte[] gg = int1.ToArray();
                    using (FileStream fs = System.IO.File.Create(Path.Combine(Server.MapPath(@"/TempBasicImages/" + guid + "_file.mp3"))))
                    {
                        intPath = @"/TempBasicImages/" + guid + "_file.mp3";
                        songpath = intPath;
                        if (gg != null)
                        {
                            fs.Write(gg, 0, gg.Length);
                        }
                    }
                }
                if (IntF == "Custom Audio Interruption")
                {
                    var dd = _premium.BindDropAudio(userID);
                    if (dd.Count != 0)
                    { 
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(dd[0].Path)));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(FileName)));//File Selected For Interruption
                    List<byte> int1 = new List<byte>(intreAudio);
                    int1.AddRange(mainAudio);
                    byte[] gg = int1.ToArray();
                    using (FileStream fs = System.IO.File.Create(Path.Combine(Server.MapPath(@"/TempBasicImages/" + guid + "_file.mp3"))))
                    {
                        intPath = @"/TempBasicImages/" + guid + "_file.mp3";
                        songpath = intPath;
                        if (gg != null)
                        {
                            fs.Write(gg, 0, gg.Length);
                        }
                    }

                }
                }
            }


            return intPath;
        }



        public List<BindDropDown> DropBind()
        {

          
            List<BindDropDown> li = new List<BindDropDown>();
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            li = _premium.BindDropImage(userID);
            //li = pre.BindDropAudio(userID);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in li)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.id.ToString()
                });
            }

            ViewBag.CityList = list;
            ViewBag.AudioFiles = list;
            return li;
        }

        public List<BindDropDown> DropBindIMage()
        {
            try
            {


                List<BindDropDown> li = new List<BindDropDown>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                li = _premium.BindDropImage(userID);
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in li)
                {
                    list.Add(new SelectListItem
                    {
                        Text = item.Name

                    });
                }
                ViewBag.CityList = list;
                return li;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ContentResult PreviewAudio()
        {
            string path = "";
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                string name = Guid.NewGuid().ToString();
                hpf = Request.Files[file] as HttpPostedFileBase;
                string savedFileName = ("/TempBasicImages/" + name + "_" + hpf.FileName);
                hpf.SaveAs(Path.Combine(Server.MapPath(savedFileName)));
                songname = savedFileName;
                path = "/TempBasicImages/" + name + "_" + hpf.FileName;
            }
            return Content("{\"name\":\"" + path + "\"}");
        }
        [HttpPost]
        public ContentResult pdffile()
        {
            var filename = "";
            Guid guid = Guid.NewGuid();
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                filename = hpf.FileName;
                artphototitle = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                var split = hpf.FileName.Split('.');
                if (split[1] == "pdf" || split[1] == "PDF")
                {
                    string savedFileName = Path.Combine(Server.MapPath("/pdffile/"), Path.GetFileName(hpf.FileName));
                    hpf.SaveAs(Path.Combine(Server.MapPath(artphototitle)));
                   // hpf.SaveAs(Server.MapPath(artphototitle));
                    //  string fileinp = hpf.FileName;
                }
                else
                {

                    filename = "Invalid";
                }
            }
            return Content("{\"name\":\"" + filename + "$" + artphototitle + "\"}");
        }

        public ActionResult DataPlanLimit()
        {
            return View();
        }





        [HttpPost]
        public ActionResult PdfInterruption(string IntF, string FileName, string Percentage, string Image, string Title, string ComposerName, string Default)
        {
            try
            {
                //string track = RandomPassword.CreatePassword(7);
                PdfIntrreputionModel obvpdf = new PdfIntrreputionModel();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                pdfpath = obvpdf.PdfIntreption(artphototitle, ComposerName,Percentage, Title, userID, IntF, imagepath);
                string[] PDF = pdfpath.Split('@');
                string pathpdf = PDF[0];
                string trackid = PDF[1];
               
                Session["TRACK"] = trackid;
                return Json(pathpdf);
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
            Guid guid = Guid.NewGuid();
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                mm = hpf.FileName;
                var split = hpf.FileName.Split('.');
                if (split[1] == "jpg" || split[1] == "JPG" || split[1] == "png" || split[1] == "PNG")
                {
                    string savedImageName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                    //string savedImageName = Path.Combine(Server.MapPath("TempBasicImages/"), Path.GetFileName(hpf.FileName));
                    hpf.SaveAs(Path.Combine(Server.MapPath(savedImageName)));
                    imagepath = savedImageName;
                    mm = savedImageName;
                    Session["imagepath"] = imagepath;

                }


                else
                {

                    mm = "Invalid";
                }
            }

            string loc = (Path.Combine(Server.MapPath("/pdffile/" + nn)));



            return Content("{\"name\":\"" + mm + "\"}");
        }

        [HttpPost]
        public ContentResult PreviewImage()
        {
            var mm = "";
            string guid = Guid.NewGuid().ToString();
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                mm = hpf.FileName;
                var split = hpf.FileName.Split('.');
                if (split[1] == "jpg" || split[1] == "JPG" || split[1] == "png" || split[1] == "PNG")
                {
                    string savedImageName = @"/TempBasicImages/" + guid + "_" + hpf.FileName;
                    hpf.SaveAs(Path.Combine(Server.MapPath(savedImageName)));
                    imagename = savedImageName;
                }
                else
                {

                    mm = "Invalid";
                }
            }

            return Content("{\"name\":\"" + imagename + "\"}");
        }
        public ActionResult PrievewMatrixImage()
        {
            var mm = "";
            string guid = Guid.NewGuid().ToString();
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                mm = hpf.FileName;
                var split = hpf.FileName.Split('.');
                if (split[1] == "jpg" || split[1] == "JPG" || split[1] == "png" || split[1] == "PNG")
                {
                    string savedImageName = @"/TempBasicImages/" + guid + "_" + hpf.FileName;
                    hpf.SaveAs(Path.Combine(Server.MapPath(savedImageName)));
                    imagename1 = savedImageName;
                }
                else
                {

                    mm = "Invalid";
                }
            }

            return Content("{\"name\":\"" + imagename1 + "\"}");

        }

        public ActionResult VideoPosting()
        {
            try
            {
                SocialNetworkModel model = new SocialNetworkModel();
                IBasic basic = DependencyResolver.Current.GetService<IBasic>();
               
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid Aphid_ID = new Guid(session);
                List<SocialNetworkModel> list = new List<SocialNetworkModel>();
                list = basic.SocialNetworkCat(Aphid_ID);
                model.NewCount = _cmn.GetNewCount(Aphid_ID);
                if (list.Count != 0)
                {
                    ViewBag.Social = list;
                    return View(model);
                }
                else
                    return RedirectToAction("index", "SocialNetworks");
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult photoArtPosting()
        {
            try
            {
                IBasic basic = DependencyResolver.Current.GetService<IBasic>();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid Aphid_ID = new Guid(session);
                List<SocialNetworkModel> list = new List<SocialNetworkModel>();
                 string count = _cmn.GetNewCount(Aphid_ID);
                list = basic.SocialNetworkCat(Aphid_ID);
                SocialNetworkModel model = new SocialNetworkModel();
                if (list.Count != 0)
                {
                    ViewBag.Social = list;
                    return View(model);
                }
                else
                    return RedirectToAction("Index", "SocialNetworks");
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }
        public ActionResult FilesPosting()
        {
            try
            {
                IBasic basic = DependencyResolver.Current.GetService<IBasic>();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid Aphid_ID = new Guid(session);
                SocialNetworkModel model = new SocialNetworkModel();
                string count = _cmn.GetNewCount(userID);
                model.NewCount = count;
                List<SocialNetworkModel> list = new List<SocialNetworkModel>();
                list = basic.SocialNetworkCat(Aphid_ID);
                if (list.Count != 0)
                {
                    ViewBag.Social = list;
                    return View(model);
                }
                else
                    return View("index", "SocialNetworks");
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult MusicPosting()
        {
            try
            {
                SocialNetworkModel model = new SocialNetworkModel();
                IBasic basic = DependencyResolver.Current.GetService<IBasic>();
                string session = AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString();
                Guid Aphid_ID = new Guid(session);
                model.NewCount = _cmn.GetNewCount(Aphid_ID);
                List<SocialNetworkModel> list = new List<SocialNetworkModel>();
                list = basic.SocialNetworkCat(Aphid_ID);
                if (list.Count != 0)
                {
                    ViewBag.Social = list;
                    return View(model);
                }
                else
                    return RedirectToAction("Index", "SocialNetworks");
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }
        public ActionResult EbookPosting()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                SocialNetworkModel social = new SocialNetworkModel();
                social.NewCount = count;
                
                List<SocialNetworkModel> list = new List<SocialNetworkModel>();
                list = _premium.SocialNetworkCat(userID);
                if (list.Count != 0)
                {
                    ViewBag.Social = list;
                    return View(social);
                }
                return View("index", "SocialNetworks");
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }
        public ActionResult Premiumsingle()
        {
            ViewBag.Sucess = false;
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
               string count = _cmn.GetNewCount(userID);
               var model = new PremiumGenerateCloneModel { NewCount=count};
               DropBindAudio();
               return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        [HttpPost]
        public ActionResult Premiumsingle(PremiumGenerateCloneModel CloneModel)
        {
            try
            {
                bool result = true;
                byte[] audio = null;
                byte[] image = null;
                string guid = Guid.NewGuid().ToString();
                string audiopath = null;

                if (CloneModel.Isvalid == true)
                {
                    string intPath = AudioIntrepption(CloneModel.InterruptionStyle, CloneModel.SelectedIntFile, songname);
                    string cap = Session["captchastring"].ToString();
                    var captchatext = HttpContext.Session["captchastring"].ToString();
                    if (CloneModel.Captcha == captchatext)
                    {
                        long len = 0, imgLength = 0, intLength = 0;
                        Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                        if (songname != null)
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songname))).Length;
                        }
                        if (imagename != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagename))).Length;
                        }
                        if (intPath != null && intPath != "")
                        {
                            intLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(intPath))).Length;
                        }
                        long length = len + imgLength + intLength;
                        result = _cmn.CheckSpace(userID, length);
                        if (result == true)
                        {
                         
                            CloneModel.UserID = userID;
                            CloneModel.CloneID = Guid.NewGuid();
                            CloneModel.MatrixImageBytePath = imagename;

                            if (imagename != null)
                            {
                                img = imagename;
                            }

                            InterruptedFileModel intModel = new InterruptedFileModel();
                            intModel.CloneId = Guid.NewGuid();
                            intModel.InterruptedFilePath = intPath;
                            intModel.VideoPath = songpath;
                            intModel.CreateDate = System.DateTime.Now;
                            intModel.CatId = 1;
                            if (songpath != null)
                            {
                                intModel.InterruptedFilePath = intPath;

                            }
                            if (songname != null)
                            {
                                intModel.VideoPath = songname;
                            }
                            if (intModel.FileName == null)
                            {
                                intModel.FileName = songname.Substring(songname.IndexOf("_") + 1);
                            }
                          
                           
                            intModel.IsActive = true;
                            intModel.ModifiedDate = System.DateTime.Now;
                            intModel.UserId = userID;
                            CreateLinkPostModel post = new CreateLinkPostModel();
                            if (songpath != null)
                            {
                                byte[] by = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songpath)));
                                post.FileSize = CalculateFileSize.Size(by);


                            }
                            else
                            {
                                if (songname != null)
                                {
                                    byte[] by = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songname)));
                                    post.FileSize = CalculateFileSize.Size(by);

                                }
                                else
                                {
                                    post.FileSize = null;
                                }
                                
                                
                            }
                            string no = RandomPassword.CreatePassword(7);
                          
                            string count = _cmn.GetNewCount(userID);
                            CloneModel.NewCount = count;
                            post.Category = "Music";
                            post.Channel = "Matrix";
                            post.Date = System.DateTime.Now;
                            post.Downloads = 0;
                            post.NoOfChannel = 0;
                            CloneModel.TrackingNumber = no;
                            intModel.TrackNumber = no;
                            post.Title = CloneModel.Title;
                            post.TrackingNumber = no.ToString();
                            post.Views = 0;
                            post.UserID = userID;
                            post.MatrixImagePath = imagename;
                            CloneModel.UploadAudioPath = songname;
                            CloneModel.UploadImage = image;
                            CloneModel.FileLength = length.ToString();
                            CloneModel.Type = "SingleMusic";
                            AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                            Alldata.UserID = userID;
                            Alldata.CloneId = CloneModel.CloneID;
                            Alldata.Title = CloneModel.Title;
                            Alldata.AlbumTitle = CloneModel.AlbumTitle;
                            Alldata.Tag = CloneModel.Tags;
                            Alldata.ArtistName = CloneModel.ArtistName;
                            Alldata.UploadFilePath = intModel.InterruptedFilePath;
                            Alldata.UploadImageFilePath = CloneModel.UploadImagePath;
                            Alldata.AudioFilePath = CloneModel.UploadAudioPath;
                            Alldata.MatrixFilePath = CloneModel.MatrixImageBytePath;
                            Alldata.ComposerName = CloneModel.Composer;
                            Alldata.Producer = CloneModel.Producer;
                            Alldata.Publisher = CloneModel.Publisher;
                            Alldata.InteruptionStyle = CloneModel.InterruptionStyle;
                            Alldata.AvailableForDownload = CloneModel.AvailableDownload;
                            Alldata.ExplicitContent = CloneModel.ExplicitContent;
                            Alldata.UploadImageFilePath = CloneModel.UploadImagePath;
                            Alldata.UploadPDFFilePath = CloneModel.PdfFilePath;
                            Alldata.PagePercentage = CloneModel.PagePercentage;
                            Alldata.Type = CloneModel.Type;
                            Alldata.FileNames = intModel.FileName;
                            Alldata.VideoFilePath = CloneModel.VideoFile;
                            Alldata.WaterMarkMatrixImagePath = "NUll";
                            Alldata.WaterMarkMatrixImageText = "NULL";
                            Alldata.VideoCategory = "NULL";
                            Alldata.RARFilePath = CloneModel.Producer;
                            Alldata.MatrixImagePath = img;
                            Alldata.CreatotName = CloneModel.CreatorName;
                            Alldata.TrackingNumber = no;
                            Alldata.CreatedDate = System.DateTime.Now;
                            Alldata.ModifyDate = System.DateTime.Now;
                            Alldata.IsActive = true;
                            Alldata.CatID = 1;
                            CloneModel.CatID = 1;
                            Alldata.GenCloneID = 2;
                            bool re = _premium.InsertPremiumBiteMusicSingle(CloneModel, intModel, post, Alldata);
                            songname = null;
                            imagename = null;
                            if (re == true)
                            {
                                bool res = _cmn.UpdateDataMemory(userID, length);
                            }
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Premium");


                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Captcha";
                        // Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>");
                    }
                }
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
            DropBind();
            ViewBag.Sucess = true;
            return View(CloneModel);
        }


        public ActionResult GetCaptcha()
        {
            var da = "";

            if (Session["captchastring"] != null)
            {
                da = Session["captchastring"].ToString();
            }

            //return Json(new { name = da }, JsonRequestBehavior.AllowGet);
            return Json(da);
        }

        public ActionResult PremiumAlbum()
        {
            ViewBag.Sucess = false;
            try
            {
               
                PremiumGenerateCloneModel model = new PremiumGenerateCloneModel();
                userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                model.NewCount = _cmn.GetNewCount(userID);
                ViewBag.albumcount = ptvar1.Count();
                if (ptvar1.Count() < 20)
                {
                    return View(model);
                }
                else 
                {
                    ViewBag.Message = "Max number songs reached ...";
                    ViewBag.backpage = "/Premium/PremiumAlbum";
                    return View("GenralErrorPage");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }

            

        }
        [HttpPost]
        public ActionResult PremiumAlbum(PremiumGenerateCloneModel CloneModel)
        {
            
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            try
            {
                bool result = true;
                byte[] audio = null;
                byte[] image = null;
                string guid = Guid.NewGuid().ToString();
                string audiopath = null;

                if (CloneModel.Isvalid == true)
                {
                    string intPath = AudioIntrepption(CloneModel.InterruptionStyle, CloneModel.SelectedIntFile, songname);
                    string cap = Session["captchastring"].ToString();
                    var captchatext = HttpContext.Session["captchastring"].ToString();
                    if (CloneModel.Captcha == captchatext)
                    {
                        long len = 0, imgLength = 0, intLength = 0;
                        if (songname != null)
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songname))).Length;
                        }
                        if (imagename != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagename))).Length;
                        }
                        if (intPath != null && intPath != "")
                        {
                            intLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(intPath))).Length;
                        }
                        long length = len + imgLength + intLength;
                        result = _cmn.CheckSpace(userID, length);
                        if (result == true)
                        {
                           
                            CloneModel.UserID = userID;
                            CloneModel.CloneID = Guid.NewGuid();
                            CloneModel.MatrixImageBytePath = imagename;

                            if (imagename != null)
                            {
                                img = imagename;
                            }



                            InterruptedFileModel intModel = new InterruptedFileModel();
                            intModel.CloneId = Guid.NewGuid();
                            intModel.InterruptedFilePath = intPath;
                            intModel.VideoPath = songpath;
                            intModel.CreateDate = System.DateTime.Now;
                            if (songpath != null)
                            {
                                intModel.InterruptedFilePath = intPath;

                            }
                            if (songname != null)
                            {
                                intModel.VideoPath = songname;
                            }
                            if (intModel.FileName != null)
                            {

                                intModel.FileName = songname.Substring(songname.IndexOf("_") + 1);
                            }
                            else
                            {
                                intModel.FileName =  null;
                            }

                            intModel.IsActive = true;

                            intModel.ModifiedDate = System.DateTime.Now;
                            intModel.UserId = userID;
                            CreateLinkPostModel post = new CreateLinkPostModel();
                            if (songpath != null)
                            {
                                byte[] by = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songpath)));
                                post.FileSize = CalculateFileSize.Size(by);


                            }
                            else
                            {
                                byte[] by = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songname)));
                                post.FileSize = CalculateFileSize.Size(by);
                            }
                            string no = RandomPassword.CreatePassword(7);
                            
                            string count = _cmn.GetNewCount(userID);
                            CloneModel.NewCount = count;
                            post.Category = "Music";
                            post.Channel = "Matrix";
                            post.Date = System.DateTime.Now;
                            post.Downloads = 0;
                            post.NoOfChannel = 0;
                            CloneModel.TrackingNumber = no;
                            intModel.TrackNumber = no;
                            post.Title = CloneModel.Title;
                            post.TrackingNumber = no.ToString();
                            post.Views = 0;
                            post.UserID = userID;
                            post.MatrixImagePath = imagename;
                            CloneModel.UploadAudioPath = audiopath;
                            CloneModel.UploadImage = image;
                            CloneModel.Type = "AlbumMusic";
                            AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                            Alldata.UserID = userID;
                            Alldata.CloneId = CloneModel.CloneID;
                            Alldata.Title = CloneModel.Title;
                            Alldata.AlbumTitle = CloneModel.AlbumTitle;
                            Alldata.Tag = CloneModel.Tags;
                            Alldata.ArtistName = CloneModel.ArtistName;
                            Alldata.UploadFilePath = intModel.InterruptedFilePath;
                            Alldata.UploadImageFilePath = CloneModel.UploadImagePath;
                            Alldata.AudioFilePath = CloneModel.UploadAudioPath;
                            Alldata.MatrixFilePath = CloneModel.MatrixImageBytePath;
                            Alldata.ComposerName = CloneModel.Composer;
                            Alldata.Producer = CloneModel.Producer;
                            Alldata.Publisher = CloneModel.Publisher;
                            Alldata.InteruptionStyle = CloneModel.InterruptionStyle;
                            Alldata.AvailableForDownload = CloneModel.AvailableDownload;
                            Alldata.ExplicitContent = CloneModel.ExplicitContent;
                            Alldata.UploadImageFilePath = CloneModel.UploadImagePath;
                            Alldata.UploadPDFFilePath = CloneModel.PdfFilePath;
                            Alldata.PagePercentage = CloneModel.PagePercentage;
                            Alldata.Type = CloneModel.Type;
                            Alldata.FileNames = intModel.FileName;

                            Alldata.VideoFilePath = CloneModel.VideoFile;
                            Alldata.WaterMarkMatrixImagePath = "NUll";
                            Alldata.WaterMarkMatrixImageText = "NULL";
                            Alldata.VideoCategory = "NULL";
                            Alldata.RARFilePath = CloneModel.Producer;
                            Alldata.MatrixImagePath = img;
                            Alldata.CreatotName = CloneModel.CreatorName;
                            Alldata.TrackingNumber = no;
                            Alldata.CreatedDate = System.DateTime.Now;
                            Alldata.ModifyDate = System.DateTime.Now;
                            Alldata.IsActive = true;
                            Alldata.CatID = 1;
                            Alldata.GenCloneID = 2;
                            CloneModel.FileLength = length.ToString();
                            ptvar1.Add(CloneModel);
                            ptvar2.Add(intModel);
                            ptvar3.Add(post);
                            ptvar4.Add(Alldata);
                            bool re = _premium.InsertPremiumBiteMusicSingle(CloneModel, intModel, post, Alldata);
                            if (re == true)
                            {
                                bool res = _cmn.UpdateDataMemory(userID, length);
                            }
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Premium");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Captcha";
                        // Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>");
                    }
                }
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
            //DropBind();
            ViewBag.albumcount = ptvar1.Count();
            ViewBag.Sucess = true;
            return View(CloneModel);
        }
        public ActionResult PremiumByteyourvideo()
        {
            ViewBag.Sucess = false;
            PremiumGenerateCloneModel model = new PremiumGenerateCloneModel();
            if (AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null)
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                model.NewCount = count;
                DropBindIMage();
            }

            else { return RedirectToAction("LoginUser", "Accounts"); }
            return View(model);
        }

        [HttpPost]
        public ContentResult UploadVideo()
        {
            HttpPostedFileBase hpf = null;
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                var ConvertedPath = ConverttoMp4(hpf.FileName,hpf);
                System.IO.File.Delete(Path.Combine(Server.MapPath("/OriginalVideo/"+hpf.FileName)));
                //System.IO.File.Delete(Server.MapPath("/TempBasicImages/"+hpf.FileName));
                //string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;   //string savedFileName = (@"\TempBasicImages\" + guid + "_" + hpf.FileName);
                //hpf.SaveAs(Server.MapPath(savedFileName));
                string savedFileName = "/TempBasicImages/" + ConvertedPath.ToString();
                var aa = System.IO.File.Exists(Path.Combine(Server.MapPath(savedFileName)));
                if (aa == true) 
                {
                }
                videopath = savedFileName;
                videobyte = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(savedFileName)));
            }
            return Content("{\"name\":\"" + hpf.FileName + "$" + videopath + "\"}");

        }


        public string ConverttoMp4(string fileName,HttpPostedFileBase hpf)
        {
            string html = string.Empty;
            //rename if file already exists
            string guid = Guid.NewGuid().ToString();
            int j = 0;
            string AppPath;
            string inputPath;
            string outputPath;
            string imgpath;
            AppPath = Request.PhysicalApplicationPath;
            //Get the application path
            inputPath = AppPath + "OriginalVideo";
            //Path of the original file
            outputPath = AppPath + "TempBasicImages";
            //Path of the converted file
            imgpath = AppPath + "Thumbs";
            //Path of the preview file
            string filepath = Path.Combine(Server.MapPath("/OriginalVideo/" + fileName));
            while (System.IO.File.Exists(filepath))
            {
                j = j + 1;
                int dotPos = fileName.LastIndexOf(".");
                string namewithoutext = fileName.Substring(0, dotPos);
                string ext = fileName.Substring(dotPos + 1);
                fileName = namewithoutext + j + "." + ext;
                //filepath = Server.MapPath("~/OriginalVideo/" + fileName);
                 filepath=Path.Combine(Server.MapPath("/OriginalVideo/" + fileName));
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
            outPutFile = "/OriginalVideo/" + fileName;
            int i = hpf.ContentLength;
            System.IO.FileInfo a = new System.IO.FileInfo(Path.Combine(Server.MapPath(outPutFile)));
            while (a.Exists == false)
            {

            }
            long b = a.Length;
            while (i != b)
            {

            }
            string cmd = " -i \"" + inputPath + "\\" + fileName + "\" \"" + outputPath + "\\"+guid+"_"+ fileName.Remove(fileName.IndexOf(".")) + ".mp4" + "\"";
            ConvertNow(cmd);
            string imgargs = " -i \"" + outputPath + "\\" + fileName.Remove(fileName.IndexOf(".")) + ".flv" + "\" -f image2 -ss 1 -vframes 1 -s 280x200 -an \"" + imgpath + "\\" + fileName.Remove(fileName.IndexOf(".")) + ".jpg" + "\"";
            ConvertNow(imgargs);

            string filepathconverted = guid + "_" + fileName.Remove(fileName.IndexOf(".")) + ".mp4";
            return filepathconverted;
        }

        private void ConvertNow(string cmd)
        {
            string exepath;
            string AppPath = Request.PhysicalApplicationPath;
            //Get the application path
            exepath = Server.MapPath("~/ffmpeg.exe");
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
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
        public ContentResult MatrixImageVideo()
        {
            string guid = Guid.NewGuid().ToString();
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                string savedFileName = @"/TempBasicImages/" + guid + "_" + hpf.FileName;
                hpf.SaveAs(Path.Combine(Server.MapPath(savedFileName)));
                imagename = savedFileName;
            }
            return Content("{\"name\":\"" + hpf.FileName + "\"}");

        }



        [HttpPost]
        public ActionResult PremiumByteyourvideo(PremiumGenerateCloneModel model)
        {
            try
            {
                bool result = true;
                long len = 0, imgLength = 0, intLength = 0;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                model.NewCount = _cmn.GetNewCount(userID);
                if (videobyte != null)
                {
                    len = videobyte.Length;
                }
                if (imagename != null)
                {
                    imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagename))).Length;
                }
                //Messages

                long length = len + imgLength + intLength;
                model.FileLength = length.ToString();
                result = _cmn.CheckSpace(userID, length);
                if (result == true)
                {
                  
                    string no = RandomPassword.CreatePassword(7);
                    model.TrackingNumber = no.ToString();
                    model.UserID = userID;
                    model.CloneID = Guid.NewGuid();
                    model.VideoFile = videopath;
                    model.MatrixImageBytePath = imagename;
                    model.CatID = 2;
                    // model.UploadImage = videoimage;
                    if (model.SelectedIntFile != "No Interruption")
                    {
                        PremiumVideoInterruption ob = new PremiumVideoInterruption();
                        string name = ob.VideoInterruption(userID, model.SelectedIntFile, model.Interruptedfile, videopath, null, model.Title, session);
                    }
                    InterruptedFileModel intModel = new InterruptedFileModel();
                    intModel.TrackNumber = no.ToString();
                    intModel.CloneId = Guid.NewGuid();
                    intModel.CreateDate = System.DateTime.Now;
                    if (model.SelectedIntFile == "No Interruption")
                    {
                        intModel.VideoPath = videopath;
                        intModel.InterruptedFilePath = videopath;
                    }
                    else
                    {
                        intModel.VideoPath = videopath;
                        intModel.InterruptedFilePath = videopath;
                    }
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
                    intModel.UserId = userID;
                    intModel.CatId = 2;
                  
                    CreateLinkPostModel post = new CreateLinkPostModel();
                    if (videobyte != null)
                    {
                        post.FileSize = CalculateFileSize.Size(videobyte);

                    }
                    post.Category = "Video";
                    post.Channel = "Matrix";
                    post.Date = System.DateTime.Now;
                    post.Downloads = 0;
                    post.NoOfChannel = 0;
                    post.Title = model.AlbumTitle;
                    post.TrackingNumber = no.ToString();
                    post.Views = 0;
                    post.UserID = userID;
                    post.MatrixImagePath = imagename;
                    AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                    Alldata.UserID = userID;
                    Alldata.CloneId = model.CloneID;
                    Alldata.Title = model.Title;
                    Alldata.AlbumTitle = model.AlbumTitle;
                    Alldata.Tag = model.Tags;
                    Alldata.ArtistName = model.ArtistName;
                    Alldata.UploadFilePath = intModel.VideoPath;
                    Alldata.UploadImageFilePath = model.UploadImagePath;
                    Alldata.AudioFilePath = model.UploadAudioPath;
                    Alldata.MatrixFilePath = "NULL";
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
                    Alldata.WaterMarkMatrixImagePath = "NUll";
                    Alldata.WaterMarkMatrixImageText = "NULL";
                    Alldata.VideoCategory = "Null";
                    Alldata.RARFilePath = model.Producer;
                    Alldata.MatrixImagePath = model.MatrixImageBytePath;
                    Alldata.CreatotName = model.CreatorName;
                    Alldata.TrackingNumber = model.TrackingNumber;
                    Alldata.CreatedDate = System.DateTime.Now;
                    Alldata.ModifyDate = System.DateTime.Now;
                    Alldata.IsActive = true;
                    Alldata.CatID = 2;
                    Alldata.GenCloneID = 2;
                    bool re = _premium.InsertByteYourVideo(model, intModel, post, Alldata);
                    if (re == true)
                    {
                        bool res = _cmn.UpdateDataMemory(userID, length);
                    }
                    imagename = null;
                    videopath = null;
                    videobyte = null;
                    DropBindIMage();
                }
                else
                {
                    return RedirectToAction("DataPlanLimit", "Premium");


                }
            }

            catch (Exception)
            {
                RedirectToAction("LoginUser", "Accounts");
            }
            ViewBag.Sucess = true;
            return View(model);

        }


        [HttpPost]
        public ActionResult PremiumByteyourEbook(PremiumGenerateCloneModel model)
        {

            try
            {
                bool result = true;
                string track;
                string pathpdf = null;
                if (model.Isvalid == true)
                {
                    string cap = Session["captchastring"].ToString();
                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    if (model.Captcha == cap)
                    {
                        if (model.SelectedIntFile == "No Interruption")
                        {
                            track = RandomPassword.CreatePassword(7);


                            model.UserID = userID;

                            model.TrackingNumber = track;
                            model.MatrixImageBytePath = artphototitle;

                        }
                        else
                        {

                            PdfIntrreputionModel obvpdf = new PdfIntrreputionModel();
                            var image = Session["imagepath"].ToString();
                            model.imagepath = image;
                            pdfpath = obvpdf.PdfIntreption(artphototitle, model.Composer,model.PagePercentage, model.Title, userID, model.SelectedIntFile,model.imagepath );
                            string[] PDF = pdfpath.Split('@');
                            pathpdf = PDF[0];
                            string[] trackno = pdfpath.Split('@');

                            if (Session["TRACK"] != null)
                            {
                                track = Session["TRACK"].ToString();

                            }
                            else
                            {
                                track = trackno[1];

                            }

                            model.UserID = userID;
                            model.MatrixImageBytePath = pathpdf;
                        }
                        long len = 0, imgLength = 0, intpdf = 0;

                        model.NewCount = _cmn.GetNewCount(userID);
                        if (artphototitle != null && artphototitle!="")
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(artphototitle))).Length;
                        }
                        if (imagepath != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagepath))).Length;
                        }
                        if (pathpdf != null)
                        {
                            intpdf = System.IO.File.ReadAllBytes(Server.MapPath(pathpdf)).Length;
                        }
                        long length = len + imgLength + intpdf;
                        result = _cmn.CheckSpace(userID, length);
                        if (result == true)
                        {
                           
                            model.CloneID = Guid.NewGuid();
                            model.TrackingNumber = track;
                            model.MatrixImageBytePath = imagepath;
                            model.PdfFilePath = artphototitle;
                            CreateLinkPostModel ob = new CreateLinkPostModel();
                            ob.Category = "Pdf";
                            ob.Channel = "Matrix";
                            ob.Date = System.DateTime.Now;
                            ob.NoOfChannel = 0;
                            ob.Title = model.Title;
                            ob.TrackingNumber = track;
                            ob.Views = 0;
                            ob.Downloads = 0;
                            ob.UserID = userID;
                            ob.MatrixImagePath = imagepath;
                            string img = null;
                            string mat = null;
                            byte[] size = null;
                            if (artphototitle != null && artphototitle!="")
                            {
                                size = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(artphototitle)));
                            }
                            if (pathpdf != null)
                            {
                                size = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(pathpdf)));
                            }


                            if (size != null)
                            {
                                ob.FileSize = CalculateFileSize.Size(size);

                            }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                            if (imagepath != null)
                            {
                                img = imagepath;
                            }

                            InterruptedFileModel ob1 = new InterruptedFileModel();
                            ob1.CreateDate = System.DateTime.Now;
                            ob1.ModifiedDate = System.DateTime.Now;
                            ob1.TrackNumber = track;
                            ob1.VideoPath = FilePhoto;
                            ob1.FileName = FilePhoto;
                            ob1.CloneId = model.CloneID;
                            ob1.VideoPath = ZipPath;
                            ob1.UserId = userID;
                            ob1.InterruptedFilePath = pathpdf;
                            ob1.VideoPath = artphototitle;
                            ob1.IsActive = true;

                            //ZipPath = null;
                            FilePhoto = null;
                            AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                            Alldata.UserID = userID;
                            Alldata.CloneId = model.CloneID;
                            Alldata.Title = model.Title;
                            Alldata.AlbumTitle = model.AlbumTitle;
                            if (artphototitle != null)
                            {
                                Alldata.FileNames = artphototitle.Substring(artphototitle.IndexOf("_") + 1);
                            }
                            else
                            {
                                Alldata.FileNames = null;
                            }
                            Alldata.Tag = model.Tags;
                            Alldata.ArtistName = model.ArtistName;
                            Alldata.UploadFilePath = artphototitle;
                            Alldata.UploadImageFilePath = model.UploadImagePath;
                            Alldata.AudioFilePath = model.UploadAudioPath;
                            Alldata.MatrixFilePath = "NULL";
                            Alldata.ComposerName = model.Composer;
                            Alldata.Producer = model.Producer;
                            Alldata.Publisher = model.Publisher;
                            Alldata.InteruptionStyle = model.InterruptionStyle;
                            Alldata.AvailableForDownload = model.AvailableDownload;
                            Alldata.ExplicitContent = model.ExplicitContent;
                            Alldata.UploadImageFilePath = model.UploadImagePath;
                            if (pathpdf != null)
                            {
                                Alldata.UploadPDFFilePath = pathpdf;
                                Alldata.PdfFilePath = pathpdf;
                            }
                            else
                            {
                                Alldata.PdfFilePath = artphototitle;
                            }
                            Alldata.PagePercentage = model.PagePercentage;
                            Alldata.Type = model.Type;
                            Alldata.VideoFilePath = model.VideoFile;
                            Alldata.WaterMarkMatrixImagePath = "NUll";
                            Alldata.WaterMarkMatrixImageText = "NULL";
                            Alldata.VideoCategory = model.VideoFile;
                            Alldata.RARFilePath = model.Producer;
                            Alldata.MatrixImagePath = img;
                            Alldata.CreatotName = model.CreatorName;
                            Alldata.TrackingNumber = track;
                            Alldata.CreatedDate = System.DateTime.Now;
                            Alldata.ModifyDate = System.DateTime.Now;
                            Alldata.IsActive = true;
                            Alldata.CatID = 4;
                            Alldata.GenCloneID = 2;
                            bool re = _premium.InsertPremiumByteyourEbook(model, ob1, ob, Alldata);
                            if (re == true)
                            {
                                bool res = _cmn.UpdateDataMemory(userID, length);
                            }
                            DropBind();
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Premium");

                            // Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>");
                        }
                    }
                }
            }

            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");

            }
            ViewBag.Sucess = true;
            return View(model);
        }


        public ActionResult PremiumByteyourEbook()
        {
            ViewBag.Sucess = false;
            PremiumGenerateCloneModel model = new PremiumGenerateCloneModel();
            if (AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null)
            {
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                model.NewCount = _cmn.GetNewCount(userid);
                DropBind();
            }
            else
            {
                RedirectToAction("LoginUser", "Accounts");
            }

            return View(model);
        }
        public ActionResult PremiumByteyourArtPhotography()
        {
            ViewBag.Sucess = false;
            try
            {
                PremiumGenerateCloneModel model = new PremiumGenerateCloneModel();
                if (AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null)
                {
                    Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    model.NewCount = _cmn.GetNewCount(userid);
                } 
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult PremiumByteyourfiles()
        {
            try
            {
                PremiumGenerateCloneModel model = new PremiumGenerateCloneModel();
                if (AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null)
                {
                    Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    model.NewCount = _cmn.GetNewCount(userid);
                }
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ContentResult UploadRar()
        {
            var mm = "";
            string guid = Guid.NewGuid().ToString();
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                mm = hpf.FileName;
                var split = hpf.FileName.Split('.');
                if (split[1] == "rar" || split[1] == "RAR" || split[1] == "zip" || split[1] == "ZIP")
                {
                    string savedZipPath = (@"/TempBasicImages/" + guid + "_" + hpf.FileName);
                    hpf.SaveAs(Path.Combine(Server.MapPath(savedZipPath)));
                    ZipArray = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(savedZipPath)));
                    ZipPath = savedZipPath;
                }
                else
                {

                    mm = "Invalid";
                }
            }
            return Content("{\"name\":\"" + mm + "\"}");
        }


        [HttpPost]
        public ActionResult PremiumByteyourfiles(PremiumGenerateCloneModel model)
        {
            try
            {
                long len = 0, imgLength = 0;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
              
                if (ZipPath != null)
                {
                    len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(ZipPath))).Length;
                }
                if (imagepath != null)
                {
                    imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagepath))).Length;
                }
                long length = len + imgLength;
                bool result = _cmn.CheckSpace(userID, length);
                if (result == true)
                {
                    string track = RandomPassword.CreatePassword(7);
                    model.UserID = userID;
                    model.CloneID = Guid.NewGuid();
                    model.RarFilePath = ZipPath;
                    model.MatrixImageBytePath = imagepath;
                    model.TrackingNumber = track;
                    if (imagepath != null)
                    {
                        model.MatrixImageBytePath = imagepath;
                    }

                    CreateLinkPostModel post = new CreateLinkPostModel();
                    post.Category = "Files";
                    post.Channel = "Matrix";
                    post.Date = System.DateTime.Now;
                    post.NoOfChannel = 0;
                    post.Title = model.Title;
                    post.TrackingNumber = track;
                    post.Views = 0;
                    post.Downloads = 0;
                    post.UserID = userID;
                    byte[] size = null;
                    if (ZipPath != null)
                    {
                        size = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(ZipPath)));
                    }


                    if (size != null)
                    {
                        post.FileSize = CalculateFileSize.Size(size);                      
                    }
                    InterruptedFileModel intf = new InterruptedFileModel();
                    intf.CreateDate = System.DateTime.Now;
                    intf.ModifiedDate = System.DateTime.Now;
                    intf.TrackNumber = track;
                    intf.VideoPath = ZipPath;
                    if (ZipPath != null)
                    {
                        intf.FileName = ZipPath.Substring(ZipPath.IndexOf("_") + 1);
                    }
                    else
                    {
                        intf.FileName = null;
                    }
                    intf.CloneId = model.CloneID;

                    intf.UserId = userID;
                    intf.IsActive = true;
                    ZipPath = null;
                    FilePhoto = null;
                    AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                    Alldata.UserID = userID;
                    Alldata.CloneId = model.CloneID;
                    Alldata.Title = model.Title;
                    Alldata.AlbumTitle = model.AlbumTitle;
                    Alldata.Tag = model.Tags;
                    Alldata.ArtistName = model.ArtistName;
                    Alldata.UploadFilePath = intf.VideoPath;

                    Alldata.AudioFilePath = model.UploadAudioPath;
                    Alldata.MatrixFilePath = "NUll";
                    Alldata.ComposerName = model.Composer;
                    Alldata.Producer = model.Producer;
                    Alldata.Publisher = model.Publisher;
                    Alldata.InteruptionStyle = model.InterruptionStyle;
                    Alldata.AvailableForDownload = model.AvailableDownload;
                    Alldata.ExplicitContent = model.ExplicitContent;
                    Alldata.UploadImageFilePath = model.MatrixImageBytePath;
                    Alldata.UploadPDFFilePath = model.PdfFilePath;
                    Alldata.PagePercentage = model.PagePercentage;
                    Alldata.Type = model.Type;
                    Alldata.FileNames = intf.FileName;
                    Alldata.VideoFilePath = model.VideoFile;
                    Alldata.WaterMarkMatrixImagePath = "NUll";
                    Alldata.WaterMarkMatrixImageText = "NULL";
                    Alldata.VideoCategory = model.VideoFile;
                    Alldata.RARFilePath = ZipPath;
                    Alldata.MatrixImagePath = model.MatrixImageBytePath;
                    Alldata.CreatotName = model.CreatorName;
                    Alldata.TrackingNumber = track;
                    Alldata.CreatedDate = System.DateTime.Now;
                    Alldata.ModifyDate = System.DateTime.Now;
                    Alldata.IsActive = true;
                    Alldata.CatID = 5;
                    Alldata.GenCloneID = 2;
                    model.FileLength = length.ToString();
                    bool re = _premium.ByteYourFile(model, intf, post, Alldata);
                    if (re == true)
                    {
                        
                        bool res = _cmn.UpdateDataMemory(userID, length);
                    }
                
                    model.NewCount = _cmn.GetNewCount(userID);
                    return View(model);

                }
                else
                {
                    return RedirectToAction("DataPlanLimit", "Premium");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public ActionResult PhotoInterruption(string path)
        {
            try
            {
                string guid = Guid.NewGuid().ToString();
                string first = Path.Combine(Server.MapPath(FilePhoto));
                string second = Path.Combine(Server.MapPath(@"/DefaultFiles/Logo_Tech_.png"));
                string savePath = Path.Combine(Server.MapPath(@"/TempBasicImages/" + guid + "_file.jpg"));
                Bitmap b1 = new Bitmap(new FileStream(first, FileMode.Open));
                System.Drawing.Image myBitmap = new Bitmap(second);
                Graphics g1 = Graphics.FromImage(b1);
                g1.DrawImage(myBitmap, 200, 200);
                b1.Save(savePath);
                intphoto = @"/TempBasicImages/" + guid + "_file.jpg";
            }
            catch (Exception)
            {

                return RedirectToAction("Accounts", "LoginUser");
            }
            return Json(intphoto, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PendingClones()
        {
            return View();
        }


        public ActionResult PremiumChannel()
        {
            try
            {
                MessageModel model = new MessageModel();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                model.NewCount = Convert.ToInt32(_cmn.GetNewCount(userID));
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult ChangePostData(string Text)
        {
            List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
            List<CreateLinkPostModel> list = new List<CreateLinkPostModel>();
            try
            {
               
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                li = _premium.GetPostData(userID);
                for (int i = 0; i < li.Count; i++)
                {
                    if (li[i].Category == Text)
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
                            ChannelStatus = li[i].ChannelStatus,
                            MatrixImagePath = li[i].MatrixImagePath
                        });
                    }
                }
                //  ViewBag.PostDataNew = list;


            }
            catch (Exception)
            {
                RedirectToAction("LoginUser", "Accounts");
            }
            return Json(list);
        }

        public ActionResult PremiumLinkPost()
        {
            List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
            List<CreateLinkPostModel> list = new List<CreateLinkPostModel>();
            MessageModel model = new MessageModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);

                model.NewCount = Convert.ToInt32(count);
                li = _premium.GetPostData(userID);
                for (int i = 0; i < li.Count; i++)
                {
                    if (li[i].Category == "Music")
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
                }
                ViewBag.PostData = list;
                return View(model);

            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public ActionResult EditLinkPostPremium(string trackno)
        {

            var name = _premium.GetCategory(trackno);

            return RedirectToAction(name, "Premium", new { trackno });

        }

        public ActionResult Files(string trackno)
        {
            PremiumGenerateCloneModel model = new PremiumGenerateCloneModel();
            try
            {
               
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                model.NewCount = _cmn.GetNewCount(userID);
                var data = _premium.EditClone(trackno);
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
                    if (data.RarFilePath != null)
                    {
                        model.shortcatpath = data.RarFilePath.Substring(data.RarFilePath.IndexOf('_') + 1);
                    }
                    model.MatrixImageBytePath = data.MatrixImageBytePath;
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
                    model.UploadAudioPath = data.UploadAudioPath;
                    model.UploadImage = data.UploadImage;
                    model.UserID = data.UserID;
                    model.VideoFile = data.VideoFile;

                }
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }

            return View(model);
        }

     

        public ActionResult UpdateFile(PremiumGenerateCloneModel model)
        {
            try
            {
                long len = 0, imgLength = 0;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                model.UserID = userID;
               
                CreateLinkPostModel post = new CreateLinkPostModel();
                AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                InterruptedFileModel intf = new InterruptedFileModel();
                if (ZipPath != null)
                {
                    len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(ZipPath))).Length;
                    model.RarFilePath = ZipPath;
                    Alldata.RARFilePath = ZipPath;
                    intf.VideoPath = ZipPath;
                    intf.InterruptedFilePath = ZipPath;
                    intf.FileName = ZipPath.Substring(ZipPath.IndexOf('_') + 1);
                }
                else
                {
                    len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.RarFilePath))).Length;
                    Alldata.RARFilePath = model.RarFilePath;
                    intf.VideoPath =model.RarFilePath;
                    intf.FileName = model.shortcatpath.Substring(model.shortcatpath.IndexOf('_') + 1);
                    
                }
                if (imagepath != null)
                {
                    imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagepath))).Length;
                    model.MatrixImageBytePath = imagepath;
                    Alldata.MatrixImagePath =imagepath;
                }
                else
                {
                    imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.MatrixImageBytePath))).Length;
                    Alldata.MatrixImagePath = model.MatrixImageBytePath; 
                }
                long length = len + imgLength;
                  
                    byte[] size = null;
                    if (ZipPath != null)
                    {
                        size = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(ZipPath)));
                    }
                    else
                    {
                        size = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.RarFilePath)));
                    }


                    if (size != null)
                    {
                        Alldata.filesize = CalculateFileSize.Size(size);
                    }     
               
                    intf.ModifiedDate = System.DateTime.Now;
                    intf.TrackNumber = model.TrackingNumber;        
                    intf.CloneId = model.CloneID;
                    Alldata.UserID = userID;
                    Alldata.CloneId = model.CloneID;
                    Alldata.Title = model.Title;
                    Alldata.AlbumTitle = model.AlbumTitle;
                    Alldata.Tag = model.Tags;
                    Alldata.ArtistName = model.ArtistName;
                    Alldata.UploadFilePath = intf.VideoPath;
                    Alldata.AudioFilePath = model.UploadAudioPath;
                    Alldata.MatrixFilePath = "NUll";
                    Alldata.ComposerName = model.Composer;
                    Alldata.Producer = model.Producer;
                    Alldata.Publisher = model.Publisher;
                    Alldata.InteruptionStyle = model.InterruptionStyle;
                    Alldata.AvailableForDownload = model.AvailableDownload;
                    Alldata.ExplicitContent = model.ExplicitContent;
                    Alldata.UploadImageFilePath = model.MatrixImageBytePath;
                    Alldata.UploadPDFFilePath = model.PdfFilePath;
                    Alldata.PagePercentage = model.PagePercentage;
                    Alldata.Type = model.Type;
                    Alldata.FileNames = intf.FileName;
                    Alldata.VideoFilePath = model.VideoFile;
                    Alldata.WaterMarkMatrixImagePath = "NUll";
                    Alldata.WaterMarkMatrixImageText = "NULL";
                    Alldata.VideoCategory = model.VideoFile;         
                    Alldata.CreatotName = model.CreatorName;
                    Alldata.TrackingNumber = track;
                    Alldata.CreatedDate = System.DateTime.Now;
                    Alldata.ModifyDate = System.DateTime.Now;
                    Alldata.IsActive = true;
                    model.CatID = 5;
                    Alldata.CatID = 5;
                    Alldata.GenCloneID = 2;
                    bool result, re;
                    result = _cmn.CheckEditStatus(new Guid(model.UserID.ToString()), length, model.FileLength);
                    if (result == true)
                    {
                        model.FileLength = length.ToString();
                        re = _premium.UpdatePdf(model, Alldata, intf);
                    }
                    else
                    {
                        return RedirectToAction("DataPlanLimit", "Premium");
                    }
                    return RedirectToAction("PremiumLinkPost", "Premium");
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        

       
        public ActionResult Music(string trackno)
        {
            try
            {
               
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                PremiumGenerateCloneModel model = new PremiumGenerateCloneModel();
               
                model.NewCount = _cmn.GetNewCount(userID);
                var data = _premium.EditClone(trackno);
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
                    model.CatID = 1;
                    if (data.UploadAudioPath != null)
                    {
                        model.shortcatpath = data.UploadAudioPath.Substring(data.UploadAudioPath.IndexOf('_') + 1);
                    }
                    model.UploadImage = data.UploadImage;
                    model.UserID = data.UserID;
                    model.VideoFile = data.VideoFile;
                    model.MatrixImageBytePath = data.MatrixImageBytePath;
                    model.UploadAudioPath = data.UploadAudioPath;
                    model.FileLength = data.FileLength;
                    DropBind();
                }
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult UpdateMusic(PremiumGenerateCloneModel model)
        {
            try
            {
                
                long len = 0, imgLength = 0, intLength = 0;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                model.UserID = userID;
                AllGenerateCloneModel allmodel = new AllGenerateCloneModel();
                allmodel.Title = model.Title;
                allmodel.Tag = model.Tags;
                allmodel.CreatotName = model.CreatorName;
                allmodel.ComposerName = model.Composer;
                allmodel.Publisher = model.Publisher;
                allmodel.ArtistName = model.ArtistName;
                allmodel.SelectedInteruptionFile = model.SelectedIntFile;
                allmodel.PagePercentage = model.PagePercentage;
                allmodel.AvailableForDownload = model.AvailableDownload;
                allmodel.ExplicitContent = model.ExplicitContent;
                allmodel.UploadFilePath = model.UploadImagePath;
                allmodel.AudioFilePath = model.UploadAudioPath;
                allmodel.UserID = userID;
                allmodel.AlbumTitle = model.AlbumTitle;
                allmodel.CreatotName = model.CreatorName;
                allmodel.CatID = 1;
                allmodel.FileNames = model.shortcatpath;
                allmodel.GenCloneID = 2;
                string intPath;
                allmodel.CloneId = model.CloneID;
                allmodel.Producer = model.Producer;
                allmodel.InteruptionStyle = model.InterruptionStyle;
                allmodel.TrackingNumber = model.TrackingNumber;                 
                if (songname != null)
                {
                    intPath = AudioIntrepption(model.InterruptionStyle, model.SelectedIntFile, songname);
                    allmodel.UploadFilePath = intPath;
                    len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songname))).Length;
                    model.UploadAudioPath = songname;
                    allmodel.AudioFilePath = songname;
                    allmodel.UploadFilePath = intPath;
                    model.shortcatpath = songname.Substring(songname.IndexOf('_') + 1);
                    byte[] by = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songname)));
                    allmodel.filesize = CalculateFileSize.Size(by);
                }
                else
                {
                    intPath = AudioIntrepption(model.InterruptionStyle, model.SelectedIntFile, model.UploadAudioPath);
                    len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.UploadAudioPath))).Length;
                    model.UploadAudioPath = model.UploadAudioPath;
                    allmodel.AudioFilePath = model.UploadAudioPath;
                    allmodel.UploadFilePath = intPath;
                    byte[] by = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.UploadAudioPath)));
                    allmodel.filesize = CalculateFileSize.Size(by);
                }
                

                if (imagename != null)
                {
                    imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagename))).Length;
                    model.MatrixImageBytePath = imagename;
                    allmodel.MatrixImagePath = imagename;
                }
                else
                {
                    imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.MatrixImageBytePath))).Length;
                    model.MatrixImageBytePath = model.MatrixImageBytePath;
                    allmodel.MatrixImagePath = model.MatrixImageBytePath;
                }
                if (intPath != null && intPath != "")
                {
                    intLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(intPath))).Length;
                }
                long length = len + imgLength + intLength;
               
                bool re = false;
                InterruptedFileModel intModel = new InterruptedFileModel();
                intModel.InterruptedFilePath = intPath;
                intModel.FileName = model.shortcatpath;
                intModel.ModifiedDate = System.DateTime.Now;
                intModel.TrackNumber = model.TrackingNumber;
                intModel.UserId = new Guid(model.UserID.ToString());
                intModel.VideoPath = model.UploadAudioPath;
                intModel.CatId = 1;
                model.CatID = 1;
                bool result = _cmn.CheckEditStatus(intModel.UserId, length, model.FileLength);
                if (result == true)
                {
                    model.FileLength = length.ToString();
                    re = _premium.UpdatePdf(model, allmodel, intModel);
                }
                else
                {
                    return RedirectToAction("DataPlanLimit", "Premium");
                }
                return RedirectToAction("PremiumLinkPost", "Premium");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Video(string trackno)
        {
            try
            {
                
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                PremiumGenerateCloneModel model = new PremiumGenerateCloneModel();
                model.NewCount = _cmn.GetNewCount(userID);
                var data = _premium.EditClone(trackno);
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
                    if (data.VideoFile != null)
                    {
                        model.shortcatpath = data.VideoFile.Substring(data.VideoFile.IndexOf('_') + 1);
                    }
                    model.UploadImage = data.UploadImage;
                    model.UserID = data.UserID;
                    model.VideoFile = data.VideoFile;
                    model.FileLength = data.FileLength;
                    DropBind();
                }
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult UpdateVideo(PremiumGenerateCloneModel model)
        {
            try
            {
       
                bool result = true;
                long len = 0, imgLength = 0, intLength = 0;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                model.NewCount = _cmn.GetNewCount(userID);
                if (videobyte != null)
                {
                    len = videobyte.Length;
                    model.VideoFile = videopath;
                    model.shortcatpath = videopath.Substring(videopath.IndexOf('_') + 1);
                    Alldata.filesize = CalculateFileSize.Size(videobyte);
                }
                else
                {
                    len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.VideoFile))).Length;
                    Alldata.filesize = CalculateFileSize.Size(System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.VideoFile))));

                }
                if (imagename != null)
                {
                    imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagename))).Length;
                    model.MatrixImageBytePath = imagename;
                    model.shortimagepath = imagename.Substring(imagename.IndexOf('_') + 1);
                }
                else
                {
                    imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.MatrixImageBytePath))).Length;
                }
                //Messages

                long length = len + imgLength + intLength;
               
                string name1;
                bool re, res_ult;
                
                    model.UserID = userID;
                    model.CloneID = Guid.NewGuid();

                    // model.UploadImage = videoimage;
                    PremiumVideoInterruption ob = new PremiumVideoInterruption();
                    if (videopath != null)
                        name1 = ob.VideoInterruption(userID, model.SelectedIntFile, model.Interruptedfile, videopath, null, model.Title, session);
                    else
                        name1 = ob.VideoInterruption(userID, model.SelectedIntFile, model.Interruptedfile, model.VideoFile, null, model.Title, session);
                   
                    Alldata.UserID = userID;
                    Alldata.CloneId = model.CloneID;
                    Alldata.Title = model.Title;
                    Alldata.AlbumTitle = model.AlbumTitle;
                    Alldata.Tag = model.Tags;
                    Alldata.ArtistName = model.ArtistName;
                    Alldata.UploadFilePath = model.VideoFile;
                    Alldata.UploadImageFilePath = model.UploadImagePath;
                    Alldata.AudioFilePath = model.UploadAudioPath;
                    Alldata.MatrixFilePath = "NULL";
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
                    Alldata.FileNames = model.shortcatpath;
                    Alldata.VideoFilePath = model.VideoFile;
                    Alldata.WaterMarkMatrixImagePath = "NUll";
                    Alldata.WaterMarkMatrixImageText = "NULL";
                    Alldata.VideoCategory = "Video";
                    Alldata.RARFilePath = model.Producer;
                    Alldata.MatrixImagePath = model.MatrixImageBytePath;
                    Alldata.CreatotName = model.CreatorName;
                    Alldata.TrackingNumber = model.TrackingNumber;                    
                    Alldata.ModifyDate = System.DateTime.Now;
                    Alldata.IsActive = true;
                    Alldata.CatID = 2;
                    Alldata.GenCloneID = 2;
                   
                    InterruptedFileModel intModel = new InterruptedFileModel();
                    intModel.InterruptedFilePath = model.VideoFile;
                    intModel.FileName = model.shortcatpath;
                    intModel.ModifiedDate = System.DateTime.Now;
                    intModel.TrackNumber = model.TrackingNumber;
                    intModel.UserId = new Guid(model.UserID.ToString());
                    intModel.VideoPath = model.VideoFile;
                    model.CatID = 2;
                    res_ult = _cmn.CheckEditStatus(intModel.UserId, length, model.FileLength);
                    if (res_ult == true)
                    {
                        model.FileLength = length.ToString(); ;
                        re = _premium.UpdatePdf(model, Alldata, intModel);
                    }
                    else
                    {
                        return RedirectToAction("DataPlanLimit", "Premium");
                    }
                    return RedirectToAction("PremiumLinkPost", "Premium"); 
            
                
            }

            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult Photos(string trackno)
        {
            try
            {

            
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                PremiumGenerateCloneModel model = new PremiumGenerateCloneModel();
                model.NewCount = _cmn.GetNewCount(userID);
                var data = _premium.EditClone(trackno);
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
                    model.UserID = data.UserID;
                    model.CatID = 3;
                    model.UploadImagePath = data.UploadImagePath;
                    model.FileLength = data.FileLength;
                    DropBind();
                }
                return View(model);
            }


            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public ActionResult UpdatePhotos(PremiumGenerateCloneModel model)
        {
            try
            {
                if (model.Isvalid == true)
                {
                    string IntPath = "";
                    if (imagename != null)
                    {
                        if (imagename1 != null)
                        {
                            IntPath = PhotoInsertion(model.SelectedIntFile, imagename, imagename1);
                        }
                        IntPath = PhotoInsertion(model.SelectedIntFile, imagename, model.MatrixImageBytePath);
                    }
                    else if (imagename1 != null)
                    {
                        if (imagename != null)
                        {
                            IntPath = PhotoInsertion(model.SelectedIntFile, imagename, imagename1);
                        }
                        IntPath = PhotoInsertion(model.SelectedIntFile, model.UploadImagePath, imagename1);
                    }

                    else if ((imagename1 == null) && (imagename == null))
                    {
                        IntPath = PhotoInsertion(model.SelectedIntFile, model.UploadImagePath, model.MatrixImageBytePath);
                    }

                    string cap = Session["captchastring"].ToString();
                    if (model.Captcha == cap)
                    {
                        InterruptedFileModel intModel = new InterruptedFileModel();
                        long len = 0, imgLength = 0, intphotos = 0;
                        Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                  
                        model.NewCount = _cmn.GetNewCount(userID);
                        if (imagename != null)
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagename))).Length;
                            model.UploadImagePath = imagename;
                            model.shortcatpath = imagename.Substring(imagename.IndexOf('_') + 1);
                            intModel.InterruptedFilePath = model.UploadImagePath;
                        }
                        else
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.UploadImagePath))).Length;
                            intModel.InterruptedFilePath = model.UploadImagePath;
                        }
                        if (imagename1 != null)
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagename1))).Length;
                            model.MatrixImageBytePath = imagename1;
                            model.shortimagepath = imagename1.Substring(imagename1.IndexOf('_') + 1);
                        }
                        else
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.MatrixImageBytePath))).Length;
                        }
                        if (IntPath != null)
                        {
                            intphotos = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(IntPath))).Length;
                        }
                        long length = len + imgLength + intphotos;
                        
                           
                            model.UserID = userID;
                            model.CatID = 3;
                            model.CloneID = Guid.NewGuid();
                            //model.UploadImagePath = img;
                            //  model.MatrixImageBytePath = FilePhoto;
                            artphototitle = model.Title;
                            //model.TrackingNumber = trackNo;
                            //string mat = null;
                            AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                            Alldata.TrackingNumber = model.TrackingNumber;
                            Alldata.UserID = userID;
                            Alldata.CloneId = model.CloneID;
                            Alldata.Title = model.Title;
                            Alldata.AlbumTitle = model.AlbumTitle;
                            Alldata.Tag = model.Tags;
                            Alldata.ArtistName = model.ArtistName;
                            Alldata.UploadFilePath = IntPath;
                            Alldata.AudioFilePath = "NULL";
                            Alldata.MatrixFilePath = "NULL";
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
                            Alldata.FileNames = model.shortcatpath;
                            Alldata.VideoFilePath = model.VideoFile;
                            Alldata.WaterMarkMatrixImagePath = "NUll";
                            Alldata.WaterMarkMatrixImageText = "NULL";
                            Alldata.VideoCategory = "NULL";
                            Alldata.RARFilePath = model.Producer;
                            Alldata.MatrixImagePath = model.MatrixImageBytePath;
                            Alldata.CreatotName = model.CreatorName;
                            Alldata.TrackingNumber = model.TrackingNumber;                           
                            Alldata.ModifyDate = System.DateTime.Now;
                            Alldata.IsActive = true;
                            Alldata.CatID = 3;
                            Alldata.GenCloneID = 2;
                            if (IntPath != null)
                            {
                                byte[] arr = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(IntPath)));
                                Alldata.filesize = CalculateFileSize.Size(arr);
                            }
                            else
                            {
                                byte[] arr = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagename)));
                                Alldata.filesize = CalculateFileSize.Size(arr);
                            }
                           
                           
                            intModel.FileName = model.shortcatpath;
                            intModel.ModifiedDate = System.DateTime.Now;
                            intModel.TrackNumber = model.TrackingNumber;
                            intModel.UserId = new Guid(model.UserID.ToString());
                            intModel.VideoPath = model.VideoFile;
                            model.CatID = 3;
                            bool re = _cmn.CheckEditStatus(intModel.UserId, length, model.FileLength);
                            if (re == true)
                            {
                                model.FileLength = length.ToString();
                                re = _premium.UpdatePdf(model, Alldata, intModel);
                            }
                            else
                            {
                                return RedirectToAction("DataPlanLimit", "Premium");
                            }                          
                            return RedirectToAction("PremiumLinkPost", "Premium");
                        }
                        else
                        {
                            return View(model);
                        }

                    }
                    else
                    {
                        return View(model);
                    }
                
                
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");

            }
        }

        /// <summary>
        /// Pdf
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Category"></param>
        /// <returns></returns>

        public ActionResult Pdf(string trackno)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                PremiumGenerateCloneModel model = new PremiumGenerateCloneModel();
                model.NewCount = _cmn.GetNewCount(userID);
                var data = _premium.EditClone(trackno);
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
                    model.FileLength = data.FileLength;
                    DropBind();
                }
                return View(model);
            }


            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
         public ActionResult UpdatePdf(PremiumGenerateCloneModel model)
        {
            try
            {
                bool result = true;
                string track;
                string pathpdf = null;
                if (model.Isvalid == true)
                {
                    string cap = Session["captchastring"].ToString();
                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    if (model.Captcha == cap)
                    {
                        AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                         if (model.SelectedIntFile == "No Interruption")
                        {
                            model.UserID = userID;
                            model.TrackingNumber = model.TrackingNumber;
                            if (artphototitle != null)
                                model.PdfFilePath = artphototitle;
                        }
                        else
                        {
                            PdfIntrreputionModel obvpdf = new PdfIntrreputionModel();
                            if (artphototitle != null)
                                pdfpath = obvpdf.PdfIntreption(artphototitle, model.Composer,model.PagePercentage, model.Title, userID, model.Interruptedfile, model.imagepath);
                            else
                                pdfpath = obvpdf.PdfIntreption(model.PdfFilePath, model.Composer,model.PagePercentage, model.Title, userID, model.Interruptedfile, model.imagepath);
                            string[] PDF = pdfpath.Split('@');
                            pathpdf = PDF[0];                           
                            model.UserID = userID;
                            model.PdfFilePath = artphototitle;
                        }
                        long len = 0, imgLength = 0, intpdf = 0;
                     
                        model.NewCount = _cmn.GetNewCount(userID);
                        if (artphototitle != null)
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(artphototitle))).Length;
                            model.PdfFilePath = artphototitle;
                            byte[] arr = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(artphototitle)));
                            Alldata.filesize = CalculateFileSize.Size(arr);
                            Alldata.FileNames = artphototitle.Substring(artphototitle.IndexOf("_") + 1);
                        }
                        else
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.PdfFilePath))).Length;
                            byte[] arr = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.PdfFilePath)));
                            Alldata.filesize = CalculateFileSize.Size(arr);
                            Alldata.FileNames = model.PdfFilePath.Substring(model.PdfFilePath.IndexOf("_") + 1);
                        }
                        if (imagepath != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagepath))).Length;
                            model.MatrixImageBytePath = imagepath;
                        }
                        else
                        {
                            imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(model.UploadImagePath))).Length;
                        }
                        if (pathpdf != null)
                        {
                            intpdf = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(pathpdf))).Length;
                        }
                        long length = len + imgLength + intpdf;
                                                   
                          
                            Alldata.UserID = userID;
                            Alldata.CloneId = model.CloneID;
                            Alldata.Title = model.Title;
                            Alldata.AlbumTitle = model.AlbumTitle;
                        
                            Alldata.FileNames = artphototitle.Substring(artphototitle.IndexOf("_") + 1);
                            Alldata.Tag = model.Tags;
                            Alldata.ArtistName = model.ArtistName;
                            Alldata.UploadFilePath = artphototitle;
                            Alldata.UploadImageFilePath = model.UploadImagePath;
                            Alldata.AudioFilePath = model.UploadAudioPath;
                            Alldata.MatrixFilePath = "NULL";
                            Alldata.ComposerName = model.Composer;
                            Alldata.Producer = model.Producer;
                            Alldata.Publisher = model.Publisher;
                            Alldata.InteruptionStyle = model.InterruptionStyle;
                            Alldata.AvailableForDownload = model.AvailableDownload;
                            Alldata.ExplicitContent = model.ExplicitContent;
                            Alldata.UploadImageFilePath = model.UploadImagePath;
                            if (pathpdf != null)
                            {
                                Alldata.UploadPDFFilePath = pathpdf;
                            }
                            else
                            {
                                Alldata.PdfFilePath = artphototitle;
                            }
                            Alldata.PagePercentage = model.PagePercentage;
                            Alldata.Type = model.Type;
                            Alldata.VideoFilePath = model.VideoFile;
                            Alldata.WaterMarkMatrixImagePath = "NUll";
                            Alldata.WaterMarkMatrixImageText = "NULL";
                            Alldata.VideoCategory = model.VideoFile;
                            Alldata.RARFilePath = model.Producer;
                            Alldata.MatrixImagePath = img;
                            Alldata.CreatotName = model.CreatorName;
                            Alldata.TrackingNumber = model.TrackingNumber;
                            Alldata.CreatedDate = System.DateTime.Now;
                            Alldata.ModifyDate = System.DateTime.Now;
                            Alldata.IsActive = true;
                            Alldata.CatID = 4;
                            Alldata.GenCloneID = 2;
                            InterruptedFileModel ob1 = new InterruptedFileModel();
                            ob1.ModifiedDate = System.DateTime.Now;
                            ob1.TrackNumber = model.TrackingNumber;
                            ob1.VideoPath = model.PdfFilePath;
                            ob1.FileName = FilePhoto;
                            ob1.CloneId = model.CloneID;
                            ob1.VideoPath = ZipPath;
                            ob1.UserId = userID;
                        if(pathpdf!=null)
                        {
                            ob1.InterruptedFilePath = pathpdf;
                        }
                        else
                        {
                            ob1.InterruptedFilePath = model.PdfFilePath;
                        }
                        if (artphototitle != null)
                        {
                            ob1.VideoPath = artphototitle;
                        }
                        else
                        {
                            ob1.VideoPath = model.PdfFilePath;
                        }
                            
                            ob1.IsActive = true;
                            bool re = _cmn.CheckEditStatus(ob1.UserId, length, model.FileLength);
                           
                            if (re == true)
                            {
                                model.FileLength = length.ToString();
                                re = _premium.UpdatePdf(model, Alldata, ob1);
                            }
                            else
                            {
                                return RedirectToAction("DataPlanLimit", "Premium");
                            }
                            artphototitle = null;
                            imagepath = null;
                            return RedirectToAction("PremiumLinkPost", "Premium");                    }
                    }
              
                }            

            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");

            }

            return View(model);

        }




        public ActionResult SearchPostLink(string Text, string Category)
        {
            List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
            List<CreateLinkPostModel> list = new List<CreateLinkPostModel>();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                li = _premium.GetSearchRecord(userID, Text, Category);
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

            }
            catch (Exception)
            {
                RedirectToAction("LoginUser", "Accounts");
            }
            return Json(list);
        }

        public ActionResult AtoZ(string Text, string Order)
        {
            List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
            List<CreateLinkPostModel> list = new List<CreateLinkPostModel>();
            try
            {
                string oredr = Order.Trim();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                li = _premium.AtoZ(userID, Text, oredr);
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

            }
            catch (Exception)
            {

                RedirectToAction("LoginUser", "Accounts");
            }
            return Json(list);
        }
       
        public ActionResult PremiumMessage()
        {
            try
            {
              
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                MessageModel model = new MessageModel();
                string result = _cmn.GetMessageCount(userid);
                if (result != "0")
                {
                    string[] sp = result.Split('+');
                    model.TotalCredit = Convert.ToInt32(sp[0]);
                    model.NewCount = Convert.ToInt32(sp[1]);
                }
                else
                {
                    model.TotalCredit = 0;
                    model.NewCount = 0;
                }
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult DeleteRecrd(string Track)
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


        public ActionResult Premiumbytetracker()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                DataPlanDetail datamodel = new DataPlanDetail();

                datamodel = _premium.DataPlanDetailMethod(userID);
                //  model.NewCount = count;
                if (datamodel.PlanId != null)
                {
                    datamodel.FreeShow = CalculateFileSize.ConvertFromLength(datamodel.Free.ToString());
                    datamodel.UsedShow = CalculateFileSize.ConvertFromLength(datamodel.Used.ToString());
                }

                int SubSCount = 0;

                string count = _cmn.GetNewCount(userID);
                MessageModel model = new MessageModel();
                model.Free = datamodel.FreeShow;
                model.Plan = datamodel.UsedShow;
                model.NewCount = Convert.ToInt32(count);
                List<Bytetracker> li = new List<Bytetracker>();
                List<Bytetracker> list = new List<Bytetracker>();
                li = _premium.GetPostData1(userID);
                for (int i = 0; i < li.Count; i++)
                {
                    if (li[i].Category == "Music")
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
                            ChannelStatus = li[i].ChannelStatus,
                            MatrixImagePath = li[i].MatrixImagePath
                        });
                    }
                }
                foreach (var item in li)
                {
                    if (item.ChannelStatus == true)
                    {
                        SubSCount++;
                    }
                }
                model.SubsCount = SubSCount;
                ViewBag.PostData = list;
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult PremiumHistory()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
               
                MessageModel model = new MessageModel();
                string count = _cmn.GetNewCount(userID);
                model.NewCount = Convert.ToInt32(count);
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        static string SendingPath = "";
        static string Linktitle = "";
        static string Cat = "";
        static string pth = "";

        string path = "";
        public ActionResult Sendinglinktomarketingteam()
        {
            string[] path;
            string pth = "";

            PostingDataModel li = new PostingDataModel();
            try
            {
                //Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                li = _premium.GetUploadfiles(track);

                if (li.InterruptedFilePath != null && li.InterruptedFilePath != "")
                {

                    pth = li.InterruptedFilePath.Substring(li.InterruptedFilePath.IndexOf('_') + 1);
                    li.Original = li.InterruptedFilePath;
                }
                else
                {
                    int cnt = 0;
                    pth = li.VideoPath.Substring(li.VideoPath.IndexOf('_') + 1);
                    // path = li.VideoPath.Split('_');
                    li.Original = li.VideoPath;
                }

                //string pth = path[1];


                string PATH = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/" + pth;
                li.Path = PATH;
                li.InterruptedFilePath = pth;


            }

            catch (Exception ex)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }


            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
          
            string count = _cmn.GetNewCount(userID);
            li.NewCount = Convert.ToInt32(count);
            return View(li);

        }
        [HttpPost]
        public ActionResult Sendinglinktomarketingteam(PostingDataModel model)
        {
            try
            {
                int credit;

                if (model.Category == "Music")
                {
                    credit = 2;
                    model.Credits = credit;
                    return RedirectToAction("MarketTeamAudio", "Premium", new { model.Title, model.Path, model.TrackingNumber, model.Original, model.Category, model.ComposerName, model.Credits });
                }
                if (model.Category == "Video")
                {
                    credit = 2;
                    model.Credits = credit;
                    return RedirectToAction("MarketTeamVideo", "Premium", new { model.Title, model.Path, model.TrackingNumber, model.Original, model.Category, model.ComposerName, model.Credits });
                }
                if (model.Category == "Pdf")
                {
                    credit = 2;
                    model.Credits = credit;
                    return RedirectToAction("MarketTeamE_Book", "Premium", new { model.Title, model.Path, model.TrackingNumber, model.Original, model.MatrixImagePath, model.Category, model.ComposerName, model.Credits });
                }
                if (model.Category == "Photos")
                {
                    credit = 2;
                    model.Credits = credit;
                    return RedirectToAction("MarketTeamPhotography_Art", "Premium", new { model.Title, model.Path, model.TrackingNumber, model.Original, model.Category, model.ComposerName, model.Credits });
                }
                if (model.Category == "Files")
                {
                    credit = 2;
                    model.Credits = credit;
                    return RedirectToAction("MarketTeamZipFile", "Premium", new { model.Title, model.Path, model.TrackingNumber, model.Original, model.MatrixImagePath, model.Category, model.ComposerName, model.Credits });
                }
                return null;
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        static string track;
        static string category;
        public string Sendinglinktomarketing(string trackid, string cat)
        {
            track = trackid;
            category = cat;
            return "Sendinglinktomarketingteam";
        }

        public ActionResult ByteyourphotographyPrivew()
        {
            try
            {
             
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();

                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _premium.fileprivew(Trackingnumber);
                for (int i = 0; i < li.Count; i++)
                {

                    list.Add(new PremiumGenerateCloneModel()
                    {
                        Title = li[i].Title,
                        AlbumTitle = li[i].AlbumTitle,
                        ExplicitContent = li[i].ExplicitContent,
                        ArtistName = li[i].ArtistName,
                        Composer = li[i].Composer,
                        AvailableDownload = li[i].AvailableDownload,
                        TrackingNumber = li[i].TrackingNumber,
                        MatrixImageBytePath = li[i].MatrixImageBytePath,
                        UploadImagePath = li[i].UploadImagePath,
                        Interruptedfile = li[i].Interruptedfile,

                    });


                }
                ViewBag.PostData = list;
                return View(list);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult Expand(string Trackingnumber)
        {
            try
            {

                IBasic basic = DependencyResolver.Current.GetService<IBasic>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                List<Bytetracker> li = new List<Bytetracker>();
                List<Bytetracker> list = new List<Bytetracker>();
                li = basic.expand(userID, Trackingnumber);
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
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        [HttpPost]
        public ContentResult Image()
        {
            var mm = "";
            string guid = Guid.NewGuid().ToString();
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                mm = hpf.FileName;
                var split = hpf.FileName.Split('.');
                if (split[1] == "jpg" || split[1] == "JPG" || split[1] == "png" || split[1] == "PNG")
                {
                    string savedImageName = (@"/TempBasicImages/" + guid + "_" + hpf.FileName);
                    hpf.SaveAs(Path.Combine(Server.MapPath(savedImageName)));
                    imagepath = savedImageName;
                    FilePhoto = savedImageName;
                }


                else
                {

                    mm = "Invalid";
                }
            }

            return Content("{\"name\":\"" + FilePhoto + "\"}");
        }

        [HttpPost]
        public ActionResult PremiumByteyourArtPhotography(PremiumGenerateCloneModel model)
        {
            try
            {
                if (model.Isvalid == true)
                {
                    string IntPath = PhotoInsertion(model.SelectedIntFile, imagename, imagename1);
                    trackNo = RandomPassword.CreatePassword(7);
                    string cap = Session["captchastring"].ToString();
                    if (model.Captcha == cap)
                    {
                        string img = imagename;
                        imagename = null;
                        string img1 = imagename1;
                        imagename1 = null;
                        long len = 0, imgLength = 0, intphotos = 0;
                        Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                        model.NewCount = _cmn.GetNewCount(userID);
                        if (img != null)
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(img))).Length;
                        }
                        if (img1 != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(img1))).Length;
                        }
                        if (IntPath != null)
                        {
                            intphotos = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(IntPath))).Length;
                        }
                        long length = len + imgLength;
                        bool result = _cmn.CheckSpace(userID, length);
                        if (result == true)
                        {
                            
                            model.UserID = userID;
                            model.CloneID = Guid.NewGuid();
                            model.UploadImagePath = img;
                            model.MatrixImageBytePath = img1;
                            artphototitle = model.Title;
                            model.TrackingNumber = trackNo;
                            string mat = null;
                            if (img != null)
                            {
                                model.UploadImagePath = img;
                            }
                            if (img1 != null)
                            {
                                model.MatrixImageBytePath = img1;

                            }

                            InterruptedFileModel intModel = new InterruptedFileModel();
                            intModel.CloneId = Guid.NewGuid();
                            intModel.CreateDate = System.DateTime.Now;
                            intModel.InterruptedFilePath = intphoto;
                            if (intphoto != null)
                            {
                                intModel.InterruptedFilePath = intphoto;
                            }
                            if (intModel.FileName != null)
                            {
                                intModel.FileName = img.Substring(img.IndexOf("_") + 1);
                            }
                            else
                            {
                                intModel.FileName = null;
                            }
                            intModel.IsActive = true;
                            intModel.VideoPath = img;
                            intModel.ModifiedDate = System.DateTime.Now;
                            intModel.UserId = userID;
                            intModel.TrackNumber = trackNo;
                            CreateLinkPostModel post = new CreateLinkPostModel();
                            if (intphoto != null)
                            {
                                byte[] arr = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(intphoto)));
                                post.FileSize = CalculateFileSize.Size(arr);
                            }
                            else
                            {
                                if (img != null)
                                {
                                    byte[] arr = System.IO.File.ReadAllBytes(
                                        Path.Combine(Server.MapPath(img)));

                                    post.FileSize = CalculateFileSize.Size(arr);
                                }
                                else
                                {
                                    post.FileSize = null;
                                }
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
                            post.MatrixImagePath = img1;
                            AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                            Alldata.UserID = userID;
                            Alldata.CloneId = model.CloneID;
                            Alldata.Title = model.Title;
                            Alldata.AlbumTitle = model.AlbumTitle;
                            Alldata.Tag = model.Tags;
                            Alldata.ArtistName = model.ArtistName;
                            Alldata.UploadFilePath = IntPath;
                            Alldata.AudioFilePath = "NULL";
                            Alldata.MatrixFilePath = "NULL";
                            Alldata.ComposerName = model.Composer;
                            Alldata.Producer = model.Producer;
                            Alldata.Publisher = model.Publisher;
                            Alldata.InteruptionStyle = model.InterruptionStyle;
                            Alldata.AvailableForDownload = model.AvailableDownload;
                            Alldata.ExplicitContent = model.ExplicitContent;
                            Alldata.UploadImageFilePath = model.UploadAudioPath;
                            Alldata.UploadPDFFilePath = model.PdfFilePath;
                            Alldata.PagePercentage = model.PagePercentage;
                            Alldata.Type = model.Type;
                            Alldata.FileNames = intModel.FileName;
                            Alldata.VideoFilePath = model.VideoFile;
                            Alldata.WaterMarkMatrixImagePath = "NUll";
                            Alldata.WaterMarkMatrixImageText = "NULL";
                            Alldata.VideoCategory = "NULL";
                            Alldata.RARFilePath = model.Producer;
                            Alldata.MatrixImagePath = model.UploadImagePath;
                            Alldata.CreatotName = model.CreatorName;
                            Alldata.TrackingNumber = trackNo;
                            Alldata.CreatedDate = System.DateTime.Now;
                            Alldata.ModifyDate = System.DateTime.Now;
                            Alldata.IsActive = true;
                            Alldata.CatID = 3;
                            Alldata.GenCloneID = 2;
                            model.FileLength = length.ToString();
                            bool re = _premium.InsertByteYourArtAndPhoto(model, intModel, post, Alldata);
                            if (re == true)
                            {
                                bool res = _cmn.UpdateDataMemory(userID, length);
                            }
                            DropBind();
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Premium");

                            // Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>");
                        }
                    }
                }
            }

            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");

            }
            ViewBag.Sucess = true;
            return View(model);


        }
        public ActionResult PhotoartInterruption(string IntF, string FileName, string IntS)
        {
            string FilePhoto = PhotoInsertion(IntF, FileName, IntS);
            return Json(FilePhoto, JsonRequestBehavior.AllowGet);
        }
        public string PhotoInsertion(string IntF, string FileName, string IntS)
        {
            try
            {
                if (IntF == "Default")
                {

                    string first = Path.Combine(Server.MapPath(FileName));
                    string second = Path.Combine(Server.MapPath("/DefaultFiles/Logo_Tech_.png"));
                    string savePath = Path.Combine(Server.MapPath(FileName + "file.jpg"));
                    FileStream fs = new FileStream(first, FileMode.Open);
                    Bitmap b1 = new Bitmap(fs);
                    System.Drawing.Image myBitmap = new Bitmap(second);
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
                    string first = Path.Combine(Server.MapPath(FileName));
                    string second = Path.Combine(Server.MapPath(IntS));
                    string savePath = Path.Combine(Server.MapPath(FileName + "file.jpg"));
                    FileStream fs = new FileStream(first, FileMode.Open);
                    Bitmap b1 = new Bitmap(fs);
                    System.Drawing.Image myBitmap = new Bitmap(second);
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
            catch (Exception)
            {


            }
            return FilePhoto;
        }

        public ActionResult PremiumDataplan()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());

                string count = _cmn.GetNewCount(userID);

                DataPlanDetail model = new DataPlanDetail();

                model = _premium.DataPlanDetailMethod(userID);
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
                var userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var re = _cmn.ChangeDataPlan(planId, userID);
                return Json(re == true ? "Success" : "Failed");
            }
            catch (Exception)
            {
                return Json("Failed");
            }
        }

        public ActionResult EditChannelPage()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                ChannelModel model = new ChannelModel();
                model = _premium.ShowChannelData(userID);
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult MarketTeamAudio(string title, string path, string TrackingNumber, string Original, string Category, string ComposerName, int Credits)
        {

            PostingDataModel model = new PostingDataModel();
            model.Title = title;
            model.Path = path;
            model.TrackingNumber = TrackingNumber;
            model.Original = Original;
            model.Category = Category;
            model.ComposerName = ComposerName;
            model.Credits = Credits;
            return View(model);
        }

        public ActionResult ChannelPage(string UserID, string From, string PremiumID)
        {
            try
            {
                Guid userID;
                Guid pre1 = Guid.NewGuid(); ;
                if (PremiumID != null)
                {
                    pre1 = new Guid(PremiumID);
                }

                if (UserID == "")
                {
                    userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                }
                else
                {
                    userID = new Guid(UserID);
                }
                ChannelModel obj = new ChannelModel();
                var data = _premium.ShowChannelData(userID);
                if (data != null)
                {
                    if (data.ImagePath != "")
                    {
                        obj.ImagePath = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + data.ImagePath;
                    }
                    if (From == "CreditPage")
                    {
                        obj.SubscriptionStatus = true;
                    }
                    obj.UserData = data.UserData;
                    obj.ChannelId = data.ChannelId;
                    obj.UserName = data.UserName;
                    obj.premiumUserId = userID;
                    obj.UserID = pre1;
                }
                return View(obj);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        [HttpPost]
        public ActionResult ChannelPage(ChannelModel model)
        {
            IHome home = DependencyResolver.Current.GetService<IHome>();
            IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
            string encript = CryptorEngine.Encrypt(model.Password, true);
            model.premiumUserId = new Guid(model.UserID.ToString());
            model.Password = encript;
            model = home.UserLogin(model);
            if (model.SubscriptionStatus != false)
            {
                var loginUser = new LoginUser();
                loginUser.UserName = model.UserName;
                loginUser.Password = encript;
                var loginProfile = accountsService.LoginWithUser(loginUser);
                AphidSession.Current.SetIdentity(loginProfile, loginProfile.ExpirationDate.AddHours(-1));
            }
            return View(model);
        }


        public ActionResult AddToChannelPage(string trackNo)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                bool re = _premium.AddToChannel(trackNo, userID);
                return Json(re == true ? "True" : "False");
               
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult UnsubscribeChannel(string UserId, string ChannelId)
        {
          
            Guid userID = new Guid(UserId);
            Guid Channel = new Guid(ChannelId);
            bool re = _premium.UnsubscribeChannel(userID, Channel);
            return Json(re == true ? "True" : "False");
           
        }

        public ActionResult LoginUserSubscription(string UserID, string PremiumID, string ChannelId)
        {
            try
            {
                
                ChannelModel model = new ChannelModel();
                model.UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                model.ChannelId = new Guid(ChannelId);
                model.premiumUserId = new Guid(PremiumID);
                model = _premium.LoginUserSubscription(model);
                return Json(model.SubscriptionStatus == true ? "True" : "False");


            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }


        public ActionResult ShowChannel(string UserID, string PremiumID, string ChannelId)
        {

            IHome home = DependencyResolver.Current.GetService<IHome>();
            Guid userID = new Guid(UserID);
            string re = home.CheckAccountType(userID);
            if (re == "4")
            {
                return RedirectToAction("ChannelPage", "Premium", new { Userid = userID, PremiumId = PremiumID, From = ChannelId });
            }
            return Json("Success");
        }

        public ActionResult CheckSession()
        {
            string result = "";
            if (AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null)
            {
                result = "Exists";
            }
            else { result = "NotExists"; }
            return Json(result);
        }

        public ActionResult MarketTeamE_Book(string title, string path, string TrackingNumber, string Original, string MatrixImagePath, string ComposerName, string Category, int Credits)
        {

            PostingDataModel model = new PostingDataModel();
            model.Title = title;
            model.Path = path;
            model.TrackingNumber = TrackingNumber;
            model.Original = Original;
            model.MatrixImagePath = MatrixImagePath;
            model.ComposerName = ComposerName;
            model.Category = Category;
            model.Credits = Credits;
            return View(model);
        }
        public ActionResult MarketTeamPhotography_Art(string title, string path, string TrackingNumber, string Original, string ComposerName, string Category, int Credits)
        {
            PostingDataModel model = new PostingDataModel();
            model.Title = title;
            model.Path = path;
            model.TrackingNumber = TrackingNumber;
            model.Original = Original;
            model.ComposerName = ComposerName;
            model.Category = Category;
            model.Credits = Credits;

            return View(model);
        }
        public ActionResult MarketTeamVideo(string title, string path, string TrackingNumber, string Original, string ComposerName, string Category, int Credits)
        {

            PostingDataModel model = new PostingDataModel();
            model.Title = title;
            model.Path = path;
            model.TrackingNumber = TrackingNumber;
            model.Original = Original;

            model.ComposerName = ComposerName;
            model.Category = Category;
            model.Credits = Credits;

            return View(model);
        }
        public ActionResult MarketTeamZipFile(string title, string path, string TrackingNumber, string Original, string ComposerName, string Category, int Credits)
        {

            PostingDataModel model = new PostingDataModel();
            model.Title = title;
            model.Path = path;
            model.TrackingNumber = TrackingNumber;
            model.Original = Original;
            model.ComposerName = ComposerName;
            model.Category = Category;
            model.Credits = Credits;

            return View(model);
        }
        public ActionResult SendtoSubscriber(string Path, string Title, string TrackingNo, string CAT, string ComposerName, int Credits)
        {
            try
            {
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                var data = _premium.GetChannelInfo(userid);

                var result = _premium.GetSubscribeUsers(ComposerName, userid, Title, CAT, data, Path, TrackingNo, Credits);
                return Json(result == false ? "AlreadySend" : "Send Successful");
             
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult PremiumAddCategory()
        {
            return View();
        }
        public ActionResult PremiumAddDeletePlayList()
        {
            try
            {
                MessageModel model = new MessageModel();
                userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                model.NewCount = Convert.ToInt32(_cmn.GetNewCount(userID));
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult PremiumFavoritesList()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                MessageModel model = new MessageModel();
                model.NewCount = Convert.ToInt32(count);
             
                List<favourites> li = new List<favourites>();
                li = _premium.GetFavourites(userID);
                if (li.Count != 0)
                {
                    ViewBag.PostdataFavourites = li;
                }
                else
                {
                    ViewBag.PostdataFavourites = null;
                }
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }
        public ActionResult premiumNextsong()
        {
            return View();
        }
        public ActionResult PremiumErrorPage()
        {
            return View();
        }

        public ActionResult MarketTeam()
        {
            return View();
        }
        public ActionResult DeleteImage1(string value, string key)
        {
            try
            {
                
                ImageNames ob = Session["ImageSession"] as ImageNames;
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string val = value.Trim();
                if (ob.One == val)
                {
                    ob.One = null;
                    // ob.OneByte = null;
                    Session["ImageSession"] = ob;
                    bool result = _premium.DeletePremiumImage(userid, val);
                }
                else if (ob.Two == val)
                {
                    ob.Two = null;
                    // ob.TwoByte = null;
                    Session["ImageSession"] = ob;
                    bool result = _premium.DeletePremiumImage(userid, val);
                }
                else if (ob.Three == val)
                {
                    ob.Three = null;
                    // ob.ThreeByte = null;
                    Session["ImageSession"] = ob;
                    bool result = _premium.DeletePremiumImage(userid, val);
                }


            }
            catch (Exception)
            {

                RedirectToAction("LoginUser", "Accounts");
            }
            return View();
        }
        public ActionResult socialposting()
        {
            return View();
        }

        public void GetMusicPath()
        {
         
            string Trackingnumber = Session["Trackingnumber"].ToString();

            List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
            List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
            li = _premium.fileprivew(Trackingnumber);

            //li = basic.fileprivew(Trackingnumber);
            string Song_To_Preview = null;
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

                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _premium.fileprivew(trackno);
                string Video_To_Preview = null;
                //li = basic.fileprivew(trackno);
                for (int i = 0; i < li.Count; i++)
                {

                    //list.Add(new BasicGenerateCloneModel()
                    //{
                    //    Title = li[i].Title,
                    //    AlbumTitle = li[i].AlbumTitle,
                    //    ExplicitContent = li[i].ExplicitContent,
                    //    ArtistName = li[i].ArtistName,
                    //    Composer = li[i].Composer,
                    //    AvailableDownload = li[i].AvailableDownload,
                    //    TrackingNumber = li[i].TrackingNumber,
                    //    MatrixImageBytePath = li[i].MatrixImageBytePath,
                    //    UploadImagePath = li[i].UploadImagePath,
                    //    VideoFile = li[i].VideoFile,
                    //    //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                    //    //Audio=li[i].InterruptedAudioPath,
                    //    //Image=li[i].UploadImagePath,
                    //    //Video=li[i].Video,VideoFile
                    //});

                    Video_To_Preview = li[i].VideoFile;
                   //int index= Video_To_Preview.IndexOf("_");
                   //if (index>0)
                   //{
                   //    string filename=Video_To_Preview.Substring(index + 1);
                   //}
                    string baseurl = Request.Url.GetLeftPart(UriPartial.Authority);
                    Session["MusicPath"] = baseurl + li[i].Interruptedfile;

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

            List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
            List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
            li = _premium.fileprivew(Trackingnumber);
            string Video_To_Preview = null;
            for (int i = 0; i < li.Count; i++)
            {

                string baseurl = Request.Url.GetLeftPart(UriPartial.Authority);
                //Session["VideoPath"] = baseurl + li[i].UploadImagePath;
                Session["MusicPath"] = baseurl + li[i].UploadImagePath;

            }

        }

        public void GetPdfPath()
        {
            IBasic basic = DependencyResolver.Current.GetService<IBasic>();

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

        
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
            Session["Trackingnumber"] = Trackingnumber;
            if (category == "Select File")
            {
                GetMusicPath();
                return ("PremiumMusicPrivew");
 
            }
            if (category == "Music" || category == "0")
            {
                GetMusicPath();
                return ("PremiumMusicPrivew");
            }

            if (category == "Video" || category == "1")
            {
                GetVideoPath(Trackingnumber);
                return ("PremiumVideoPrivew");
            }
            if (category == "Photos" || category == "2")
            {
                return ("ByteyourphotographyPrivew");
            }
            if (category == "Files" || category == "4")
            {
                return ("PremiumFilesPrivew");
            }
            if (category == "Pdf" || category == "3")
            {
                return ("PremiumEbookPrivew");
            }
            else
            {
                return ("Premiumbytetracker");
            }


        }
        public ActionResult PremiumMusicPrivew()
        {
            try
            {
               
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();
                string Song_To_Preview = null;
                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _premium.fileprivew(Trackingnumber);
                for (int i = 0; i < li.Count; i++)
                {

                    list.Add(new PremiumGenerateCloneModel()
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
                        Interruptedfile=li[i].Interruptedfile
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
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult PremiumVideoPrivew()
        {
            try
            {
                
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();
                string Video_To_Preview = null;
                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _premium.fileprivew(Trackingnumber);
                for (int i = 0; i < li.Count; i++)
                {

                    list.Add(new PremiumGenerateCloneModel()
                    {
                        Title = li[i].Title,
                        AlbumTitle = li[i].AlbumTitle,
                        ExplicitContent = li[i].ExplicitContent,
                        ArtistName = li[i].ArtistName,
                        Composer = li[i].Composer,
                        AvailableDownload = li[i].AvailableDownload,
                        TrackingNumber = li[i].TrackingNumber,
                        MatrixImageBytePath = li[i].MatrixImageBytePath,
                        UploadImagePath = li[i].UploadImagePath,
                        VideoFile = li[i].VideoFile,
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });
                    Video_To_Preview = li[i].VideoFile;

                }

                ViewBag.Video_To_Preview = Video_To_Preview;
                ViewBag.PostData = list;
                return View(list);
            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult PremiumFilesPrivew()
        {
            try
            {
              
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();

                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _premium.fileprivew(Trackingnumber);
                for (int i = 0; i < li.Count; i++)
                {

                    list.Add(new PremiumGenerateCloneModel()
                    {
                        Title = li[i].Title,
                        AlbumTitle = li[i].AlbumTitle,
                        ExplicitContent = li[i].ExplicitContent,
                        ArtistName = li[i].ArtistName,
                        Composer = li[i].Composer,
                        AvailableDownload = li[i].AvailableDownload,
                        TrackingNumber = li[i].TrackingNumber,
                        MatrixImageBytePath = li[i].MatrixImageBytePath,
                        VideoFile = li[i].VideoFile,
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });
                }
                ViewBag.PostData = list;
                return View(list);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult PremiumEbookPrivew()
        {
            try
            {
                 Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();

                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _premium.fileprivew(Trackingnumber);
                for (int i = 0; i < li.Count; i++)
                {

                    list.Add(new PremiumGenerateCloneModel()
                    {
                        Title = li[i].Title,
                        AlbumTitle = li[i].AlbumTitle,
                        ExplicitContent = li[i].ExplicitContent,
                        ArtistName = li[i].ArtistName,
                        Composer = li[i].Composer,
                        AvailableDownload = li[i].AvailableDownload,
                        TrackingNumber = li[i].TrackingNumber,
                        MatrixImageBytePath = li[i].MatrixImageBytePath,
                        VideoFile = li[i].VideoFile,
                        UploadFilePDFPath = li[i].PdfFilePath,
                        Interruptedfile = li[i].Interruptedfile
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });


                }


                ViewBag.PostData = list;
                return View(list);
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult DeleteAudio1(string value, string key)
        {
            try
            {
               
                AudioNames ob = Session["AudioSession"] as AudioNames;
                Guid userID1 = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string val = value.Trim();
                if (ob.Audio1 == val)
                {
                    ob.Audio1 = null;
                    // ob.Audio1Byte = null;
                    Session["AudioSession"] = ob;
                    bool result = _premium.DeletePremiumAudio(userID1, val);
                }
                else if (ob.Audio2 == val)
                {
                    ob.Audio2 = null;
                    //ob.Audio2Byte = null;
                    Session["AudioSession"] = ob;
                    bool result = _premium.DeletePremiumAudio(userID1, val);
                }
                else if (ob.Audio3 == val)
                {
                    ob.Audio3 = null;
                    // ob.Audio3Byte = null;
                    Session["AudioSession"] = ob;
                    bool result = _premium.DeletePremiumAudio(userID1, val);
                }


            }
            catch (Exception)
            {

                RedirectToAction("LoginUser", "Accounts");
            }
            return View();
        }
        public ActionResult PremiumDashBoard()
        {
            return View();

        }
        public ActionResult Overview()
        {
            if (ImgByte != null)
            {
                byte[] objByteArray = ImgByte;
                return File(objByteArray, "/image/jpeg");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ContentResult UploadImage()
        {
            string name = Guid.NewGuid().ToString();
            ImageAudioBytesModel ob = new ImageAudioBytesModel();
            double min = 0.0;
            var r = new List<UploadFilesResult>();
            ImageNames ob1 = new ImageNames();
            if (Session["ImageSession"] != null)
            {
                ob1 = (ImageNames)Session["ImageSession"];
            }
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentType.Contains("image/"))
                {
                    if (hpf.ContentLength == 0)
                        continue;
                    string savedFileName = ("/TempBasicImages/" + name + "_" + hpf.FileName);
                    hpf.SaveAs(Path.Combine(Server.MapPath(savedFileName)));
                    // byte[] byteImage = System.IO.File.ReadAllBytes(savedFileName);
                    if (Session["ImageSession"] == null)
                    {
                        if (ob1.One == null)
                        {
                            ob1.One = hpf.FileName;
                            ob1.ImagePAth1 = savedFileName;
                            // ob1.OneByte = System.IO.File.ReadAllBytes(savedFileName);
                            Session["ImageSession"] = ob1;
                        }
                    }
                    else
                    {
                        ob1 = (ImageNames)Session["ImageSession"];
                        if (ob1.One == null)
                        {
                            ob1.One = hpf.FileName;
                            ob1.ImagePAth1 = savedFileName;
                            // ob1.OneByte = System.IO.File.ReadAllBytes(savedFileName);
                            Session["ImageSession"] = ob1;
                        }
                        else if (ob1.Two == null)
                        {
                            ob1.Two = hpf.FileName;
                            ob1.ImagePAth2 = savedFileName;
                            // ob1.TwoByte = System.IO.File.ReadAllBytes(savedFileName);
                            Session["ImageSession"] = ob1;
                        }
                        else
                        {
                            ob1.Three = hpf.FileName;
                            ob1.ImagePAth3 = savedFileName;
                            // ob1.ThreeByte = System.IO.File.ReadAllBytes(savedFileName);
                            Session["ImageSession"] = ob1;
                        }
                    }

                    r.Add(new UploadFilesResult()
                    {
                        Name = hpf.FileName,
                        Length = hpf.ContentLength.ToString(),
                        Type = savedFileName,
                        Duration = min.ToString(),
                        Count = val1
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

            return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"count\":\"" + r[0].Count.ToString() + "\",\"duration\":\"" + r[0].Duration + "\",\"size\":\"" + string.Format(r[0].Length) + "\"}", "application/json");

        }

        [HttpPost]
        public ContentResult UploadAudio()
        {
            double min = 0.0;
            var r = new List<UploadFilesResult>();
            AudioNames ob = new AudioNames();
            string name = Guid.NewGuid().ToString();
            if (Session["AudioSession"] != null)
            {
                ob = (AudioNames)Session["AudioSession"];
            }

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                if (hpf.FileName.Contains(".mp3") || hpf.FileName.Contains(".wma") || hpf.FileName.Contains(".MP3") || hpf.FileName.Contains(".WMA"))
                {
                    if (hpf.ContentLength == 0)
                        continue;
                    string savedFileName = ("/TempBasicImages/" + name + "_" + hpf.FileName);
                    hpf.SaveAs(Path.Combine(Server.MapPath(savedFileName)));
                    if (Session["AudioSession"] == null)
                    {
                        if (ob.Audio1 == null)
                        {
                            ob.Audio1 = hpf.FileName;
                            ob.AudioPath1 = savedFileName;
                            //ob.Audio1Byte = System.IO.File.ReadAllBytes(savedFileName);
                            Session["AudioSession"] = ob;
                        }
                    }
                    else
                    {
                        ob = (AudioNames)Session["AudioSession"];
                        if (ob.Audio1 == null)
                        {
                            ob.Audio1 = hpf.FileName;
                            ob.AudioPath1 = savedFileName;
                            //ob.Audio1Byte = System.IO.File.ReadAllBytes(savedFileName);
                            Session["AudioSession"] = ob;
                        }
                        else if (ob.Audio2 == null)
                        {
                            ob.Audio2 = hpf.FileName;
                            ob.AudioPath2 = savedFileName;
                            //  ob.Audio2Byte = System.IO.File.ReadAllBytes(savedFileName);
                            Session["AudioSession"] = ob;
                        }
                        else
                        {
                            ob.Audio3 = hpf.FileName;
                            ob.AudioPath3 = savedFileName;
                            // ob.Audio3Byte = System.IO.File.ReadAllBytes(savedFileName);
                            Session["AudioSession"] = ob;
                        }
                    }

                    string file1 = "/TempBasicImages/" + name + "_" + hpf.FileName;
                    ShellFile so = ShellFile.FromFilePath(Path.Combine(Server.MapPath(file1)));
                    double nanoseconds;
                    double.TryParse(so.Properties.System.Media.Duration.Value.ToString(), out nanoseconds);
                    Console.WriteLine("NanaoSeconds: {0}", nanoseconds);
                    if (nanoseconds > 0)
                    {

                        // double milliseconds = nanoseconds * 0.000001;
                        double seconds = Convert100NanosecondsToMilliseconds(nanoseconds) / 1000;
                        //min = seconds / 60;
                        //min = Math.Round(min, 2);
                        //string aa = min.ToString();
                        string aa = seconds.ToString();
                        string[] bb = aa.Split('.');
                        int cc = Convert.ToInt32(bb[0]);
                        if (cc >= 55 || cc <= 25)
                        {
                            System.IO.File.Delete(Path.Combine(Server.MapPath("/TempBasicImages/" + name + "_" + hpf.FileName)));
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

                            string savedFileName1 = Path.Combine(Server.MapPath("/TempBasicImages/" + name + "_" + hpf.FileName));
                            //  hpf.SaveAs(savedFileName1);
                            r.Add(new UploadFilesResult()
                            {
                                Name = "/TempBasicImages/" + name + "_" + hpf.FileName,
                                Length = hpf.ContentLength.ToString(),
                                Type = hpf.FileName,
                                Duration = min.ToString()
                            });

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

        
        public ActionResult PremiumPlaylist()
        {
            MessageModel model = new MessageModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string count = _cmn.GetNewCount(userID);
                model.NewCount = Convert.ToInt32(count);
             
                List<string> li = new List<string>();
                li = _premium.GetPlaylistNames(userID, null);
                if (li.Count != 0)
                {
                    ViewBag.PostdataPlaylist = li;
                    
                }
                else
                {
                    ViewBag.PostdataPlaylist = null;
                }
              
            }
            catch (Exception)
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
            return View(model);
        }

        public ActionResult GetPlayList(string TrackingID)
        {
            List<string> li = new List<string>();
            try
            {
               
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                li = _premium.GetPlaylistNames(UserID, TrackingID);
            }
            catch (Exception)
            {
                //               return RedirectToAction("Loginuser", "Accounts");
            }
            return Json(li);
        }

        public JsonResult GetSongList(string PlaylistName)
        {
            List<PlaylistModel> li = new List<PlaylistModel>();
            try
            {
               
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                li = _premium.GetSongList(UserID, PlaylistName);
            }
            catch (Exception)
            {
                // return RedirectToAction("Loginuser", "Accounts");
            }
            return Json(li);
        }
        public JsonResult EditPlaylist(string PlaylistName, string TrackingID)
        {
            bool result = false;
            try
            {
             
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                result = _premium.UpdatPlaylist(PlaylistName, TrackingID, UserID);
           
            }
            catch(Exception)
            {

            }
            return Json(result);
   
        }

        [HttpPost]
        public ActionResult DelSongFromPlay(string PlaylistName, string TrackingID)
        {
            bool result = false;
            try
            {
                
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                result = _premium.DelSongFromPlay(PlaylistName, TrackingID);
            }
            catch (Exception)
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
                result = _premium.AddSongToPlaylist(PlaylistName, TrackingID, UserID);
            }
            catch (Exception)
            {

            }
            return Json(result);
        }

        public string Before_PlaylistPrivew(string TrackingNumber, string Playlist_Name)
        {
            Session["Trackingnumber"] = TrackingNumber;
            Session["Playlist_Name"] = Playlist_Name;
            return "PremiumPlayListPrivew";
        }

        public ActionResult PremiumPlayListPrivew()
        {
            try
            {
              
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();
                string Playlist_Name = Session["Playlist_Name"].ToString();
                AllGenerateCloneModel record = new AllGenerateCloneModel();

                try
                {
                    record = _premium.Get_A_Record_via_trackID(Trackingnumber);
                    ViewBag.Record_To_Preview = record;
                    ViewBag.Playlist_Name = Playlist_Name;
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
        public ActionResult Fetch_Ad_Video_Data(string ad_type_id)
        {
            AdvertisementModel list = new AdvertisementModel();

            list = _premium.Fetch_Ad_Video_Data(ad_type_id);
            return Json(list);
        }

        public ActionResult deleteAccount()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                bool result = _premium.deleteAccount(userID);
                return Json(result == true ? "Success" : "Failed");
            
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult SaveChannel(string Data)
        {
            try
            {
                string userimage = null;
                if (imagepath != null)
                {
                    userimage = imagepath;
                }

             
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                string username = AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString();
                bool re = _premium.InsertChannelBiblography(UserID, Data, userimage, username);

                //  bool re = pre.InsertChannelBiblography(UserID, Data, userimage);

                return Json("Success");
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult totalplaylist()
        {
            int li = 0;
            try
            {
                
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                li = _premium.totalplaylist(UserID);
            }
            catch (Exception)
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
            return Json(li);
        }

        public JsonResult AddSongtoFav(string TrackingID)
        {
            bool result = false;
            try
            {
            
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                result = _premium.AddSongtoFav(TrackingID, UserID);
            }
            catch (Exception)
            {

            }
            return Json(result);
        }

        public JsonResult DelfromFav(string TrackingID)
        {
            bool result = false;
            try
            {
               
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

                result = _premium.DelfromFav(TrackingID, UserID);
            }
            catch (Exception)
            {

            }
            return Json(result);
        }

        public ActionResult fetchcat(string trackno)
        {

            var name = _premium.GetCategory(trackno);

            return RedirectToAction("FilePrivew", "Premium", new { Trackingnumber = trackno, category = name });

        }

        public JsonResult GetSongListmusic(string PlaylistName)
        {
            List<PlaylistModel> li = new List<PlaylistModel>();
            List<PlaylistModel> lists = new List<PlaylistModel>();
            try
            {
                
                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                li = _premium.GetSongListmusic(UserID, PlaylistName);
                foreach (var item in li)
                {
                    if (item.CatId == 1)
                    {
                        lists.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                //               return RedirectToAction("Loginuser", "Accounts");
            }
            return Json(lists);
        }
        [HttpPost]
        public ActionResult PremiumMessage(MessageModel messagemodel)
        {
            try
            {

              
                Guid userid = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                var email = Session["EmailAddress"].ToString();
                var username = AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString();
                messagemodel.sender_username = username;
                messagemodel.sender_Email = email;
                bool insert = _cmn.InsertMessageDetails(messagemodel);
                MessageModel model = new MessageModel();
                string result = _cmn.GetMessageCount(userid);
                if (result != "0")
                {
                    string[] sp = result.Split('+');
                    model.TotalCredit = Convert.ToInt32(sp[0]);
                    model.NewCount = Convert.ToInt32(sp[1]);
                }
                else
                {
                    model.TotalCredit = 0;
                    model.NewCount = 0;
                }
                return View(model);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult AlbumFinish(PremiumGenerateCloneModel CloneModel)
        {
            ViewBag.Message = null;
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
           
            CloneModel.NewCount = _cmn.GetNewCount(userID);
            try
            {
                bool result = true;
                byte[] audio = null;
                byte[] image = null;
                string guid = Guid.NewGuid().ToString();
                string audiopath = null;

                if (CloneModel.Isvalid == true)
                {
                    string intPath = AudioIntrepption(CloneModel.InterruptionStyle, CloneModel.SelectedIntFile, songname);
                    string cap = Session["captchastring"].ToString();
                    var captchatext = HttpContext.Session["captchastring"].ToString();
                    if (CloneModel.Captcha == captchatext)
                    {
                        long len = 0, imgLength = 0, intLength = 0;
                       
                        if (songname != null)
                        {
                            len = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songname))).Length;
                        }
                        if (imagename != null)
                        {
                            imgLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(imagename))).Length;
                        }
                        if (intPath != null && intPath != "")
                        {
                            intLength = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(intPath))).Length;
                        }
                        long length = len + imgLength + intLength;
                        result = _cmn.CheckSpace(userID, length);
                        if (result == true)
                        {
                           
                            CloneModel.UserID = userID;
                            CloneModel.CloneID = Guid.NewGuid();
                            CloneModel.MatrixImageBytePath = imagename;

                            if (imagename != null)
                            {
                                img = imagename;
                            }

                            InterruptedFileModel intModel = new InterruptedFileModel();
                            intModel.CloneId = Guid.NewGuid();
                            intModel.InterruptedFilePath = intPath;
                            intModel.VideoPath = songpath;
                            intModel.CreateDate = System.DateTime.Now;
                            if (songpath != null)
                            {
                                intModel.InterruptedFilePath = intPath;

                            }
                            if (songname != null)
                            {
                                intModel.VideoPath = songname;
                            }

                            intModel.FileName = songname.Substring(songname.IndexOf("_") + 1);
                            intModel.IsActive = true;

                            intModel.ModifiedDate = System.DateTime.Now;
                            intModel.UserId = userID;
                            CreateLinkPostModel post = new CreateLinkPostModel();
                            if (songpath != null)
                            {
                                byte[] by = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songpath)));
                                post.FileSize = CalculateFileSize.Size(by);


                            }
                            else
                            {
                                byte[] by = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(songname)));
                                post.FileSize = CalculateFileSize.Size(by);
                            }
                            string no = RandomPassword.CreatePassword(7);
                            //Messages
                            
                           
                            post.Category = "Music";
                            post.Channel = "Matrix";
                            post.Date = System.DateTime.Now;
                            post.Downloads = 0;
                            post.NoOfChannel = 0;
                            CloneModel.TrackingNumber = no;
                            intModel.TrackNumber = no;
                            post.Title = CloneModel.Title;
                            post.TrackingNumber = no.ToString();
                            post.Views = 0;
                            post.UserID = userID;
                            CloneModel.UploadAudioPath = audiopath;
                            CloneModel.UploadImage = image;
                            CloneModel.Type = "AlbumMusic";
                            AllGenerateCloneModel Alldata = new AllGenerateCloneModel();
                            Alldata.UserID = userID;
                            Alldata.CloneId = CloneModel.CloneID;
                            Alldata.Title = CloneModel.Title;
                            Alldata.AlbumTitle = CloneModel.AlbumTitle;
                            Alldata.Tag = CloneModel.Tags;
                            Alldata.ArtistName = CloneModel.ArtistName;
                            Alldata.UploadFilePath = intModel.InterruptedFilePath;
                            Alldata.UploadImageFilePath = CloneModel.UploadImagePath;
                            Alldata.AudioFilePath = CloneModel.UploadAudioPath;
                            Alldata.MatrixFilePath = CloneModel.MatrixImageBytePath;
                            Alldata.ComposerName = CloneModel.Composer;
                            Alldata.Producer = CloneModel.Producer;
                            Alldata.Publisher = CloneModel.Publisher;
                            Alldata.InteruptionStyle = CloneModel.InterruptionStyle;
                            Alldata.AvailableForDownload = CloneModel.AvailableDownload;
                            Alldata.ExplicitContent = CloneModel.ExplicitContent;
                            Alldata.UploadImageFilePath = CloneModel.UploadImagePath;
                            Alldata.UploadPDFFilePath = CloneModel.PdfFilePath;
                            Alldata.PagePercentage = CloneModel.PagePercentage;
                            Alldata.Type = CloneModel.Type;
                            Alldata.FileNames = intModel.FileName;
                            Alldata.VideoFilePath = CloneModel.VideoFile;
                            Alldata.WaterMarkMatrixImagePath = "NUll";
                            Alldata.WaterMarkMatrixImageText = "NULL";
                            Alldata.VideoCategory = "NULL";
                            Alldata.RARFilePath = CloneModel.Producer;
                            Alldata.MatrixImagePath = img;
                            Alldata.CreatotName = CloneModel.CreatorName;
                            Alldata.TrackingNumber = no;
                            Alldata.CreatedDate = System.DateTime.Now;
                            Alldata.ModifyDate = System.DateTime.Now;
                            Alldata.IsActive = true;
                            Alldata.CatID = 1;
                            Alldata.GenCloneID = 2;
                            CloneModel.FileLength = length.ToString();

                            ptvar1.Add(CloneModel);
                            ptvar2.Add(intModel);
                            ptvar3.Add(post);
                            ptvar4.Add(Alldata);

                            //bool re = _premium.InsertPremiumBiteMusicSingle(CloneModel, intModel, post, Alldata);
                            //if (re == true)
                            //{
                            //    bool res = _cmn.UpdateDataMemory(userID, length);
                            //}

                            //db entries start
                            int i = 0;
                            int j = 0;
                            bool re = false;
                            for (; i < ptvar1.Count(); i++)
                            {
                                re = false;
                                re = _premium.InsertPremiumBiteMusicSingle(ptvar1[i], ptvar2[i], ptvar3[i], ptvar4[i]);
                                if (re == true)
                                {
                                    bool res = _cmn.UpdateDataMemory(userID, Int64.Parse(ptvar1[i].FileLength));
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

                            ptvar1.Clear();
                            ptvar2.Clear();
                            ptvar3.Clear();
                            ptvar4.Clear();
                           
                            
                        }
                        else
                        {
                            return RedirectToAction("DataPlanLimit", "Premium");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Captcha";
                        // Response.Write("<script language='javascript' type='text/javascript'>alert('No Space to upload file');</script>");
                    }
                }
            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }
            ViewBag.MsgCount = CloneModel.NewCount;
            return RedirectToAction("PremiumAlbum", "Premium");
        }

        public ActionResult UploadMediaPremium()
        {

            return View();

        }

        public ActionResult SendVerificationMail()
        {
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
            PremiumAccountViewModel premiumData = _premium.GetPremiumAccountInfo(userID);
            Email mail = new Email();//send mail                
            mail.sendMaill(premiumData.PremiumUserID, premiumData.EmailAddress, "AphidLab", new Guid(), premiumData.UserName, "VerifyEmail");
            return View();
        }

    }
}
