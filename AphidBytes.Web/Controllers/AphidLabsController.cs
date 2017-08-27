using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Web.Filters;
using AphidBytes.Web.Models;
using FileUploadMVC4.Models;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Text;
//using Spire.Pdf;
//using Spire.Pdf.Graphics;
using iTextSharp.text.pdf;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp;
using System.Drawing.Imaging;
using System.Web.Hosting;
using AphidBytes.Web.Session_Helper;
using AphidBytes.Web.Web;
using AphidBytes.Web.App_Code;
using AphidBytes.Core.Extensions;
using AphidBytes.Web.Utility;

namespace AphidBytes.Web.Controllers
{
    [SessionHelper]
    public class AphidLabsController : AphidController
    {
        public static byte[] ImgByte;
        static string nn = "";
        static string songname = "";
        static string img = null;
        static string imagebyte1 = null;
        static string imagebyte2 = null;
        static byte[] songbyte = null;
        static string FilePhoto = null;
        static string imagebyte = null;
        static string pdfpath = null;
        static string pdffilepath = null;
        static string songpath = null;
        static string imagepath = null;
        static string ZipPath = null;
        static string intphoto = null;
        static byte[] videobyte = null;
        static string videopath = null;
        static byte[] gg = null;
        static string session = "";
        static string trackNo = "";
        static string artphototitle = null;
        static byte[] ZipArray = null;
        static string IntrepputedAudioPath;
        private readonly IAphidLAb _aphidlabs;
        private readonly ICommon _cmn;
        private readonly IBasic _basic;
        //
        // GET: /AphidLabs/
        public AphidLabsController()
        {
            _aphidlabs = DependencyResolver.Current.GetService<IAphidLAb>();
            _cmn = DependencyResolver.Current.GetService<ICommon>();
            _basic = DependencyResolver.Current.GetService<IBasic>();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AphidLabsAccountInfo()
        {
            try
            {
                if (AphidSession.Current.AuthenticatedUser?.Identity?.Username != null)
                {
                    session = AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString();
                }
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                string count = _cmn.GetNewCount(userID);
                var model = new MessageModel { NewCount = Convert.ToInt32(count) };

                //model.NewCount = Convert.ToInt32(count);
                AphidLabAccountModel AphidLabData = _aphidlabs.GetAphidLabAccountInfo(userID);
                // Session["AccountInformation"] = aphidTiseData;
                //ImgByte = basicTiseData.ProfilePictureInBytes;
                //AphidLabData.NewCount = count;
                if (!AphidLabData.isActive || !AphidLabData.isActive)
                {
                    AphidLabData.Validation = new ValidationModel();
                    AphidLabData.Validation.AddWarning("An email to verify your account was sent, check your Inbox or Spam folder");
                }
                return View(AphidLabData);
            }
            catch (Exception exc)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        [HttpPost]
        public ActionResult AphidLabsAccountInfo(AphidLabAccountModel model)
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
                AphidLabAccountModel AphidLAbData1 = null;
                string guid = Guid.NewGuid().ToString();
                if (ModelState.IsValid)
                {
                    //Get AphidTise User Info

                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                    AphidLabAccountModel Aphid = _aphidlabs.GetAphidLabAccountInfo(userID);

                    model.AphidlabUserID = userID;// new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString());
                    Aphid.AphidlabUserID = userID;// new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString());
                                                  //Convert profile image in byte array

                    if (string.IsNullOrEmpty(Aphid.SocialNetworkSource))
                    {
                        string encryptPwd = CryptorEngine.Encrypt(model.Password, true);
                        model.Password = encryptPwd;
                    }

                    if (string.IsNullOrEmpty(Aphid.ProfilePicturePath))
                    {
                        ImageUploader.UploadProfilePictureAndSetLocation(model);
                    }
                    else
                    {
                        if (ImageUploader.DeleteProfileImage(Aphid.ProfilePicturePath))
                            ImageUploader.UploadProfilePictureAndSetLocation(model);
                    }

                    model.AddressID = Aphid.AddressID;
                    model.BankAccountID = Aphid.BankAccountID;
                    model.SecurityQuestionID = Aphid.SecurityQuestionID;
                    bool updateByter = _aphidlabs.UpdateAphidLabAccountInfo(model);
                    Guid userID1 = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                    AphidLAbData1 = _aphidlabs.GetAphidLabAccountInfo(userID);
                    //ImgByte = AphidLAbData1.ProfilePictureInBytes;
                    string count = _cmn.GetNewCount(userID1);
                    //AphidLAbData1.NewCount = count;
                }
                return View(AphidLAbData1);

            }
            catch (Exception)
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }
        public ActionResult ChangePassword()
        {
            var model = new AphidLabAccountModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                model = _aphidlabs.GetAphidLabAccountInfo(userID);
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
        public ActionResult ChangePassword(AphidLabAccountModel model)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                AphidLabAccountModel aphidLabData = _aphidlabs.GetAphidLabAccountInfo(userID);
                aphidLabData.AphidlabUserID = userID;
                var encryptPwd = CryptorEngine.Encrypt(model.Password, true);
                var success = _cmn.UpdatePassword(userID, encryptPwd);
                if (!success)
                {
                    aphidLabData.Validation.AddError("Unable to change the current password");
                }
                else
                {
                    aphidLabData.Validation.AddInformation("Successfully changed the password");
                }

                return View(aphidLabData);
            }
            catch (Exception)
            {
                return RedirectToAction("AphidLabsAccountInfo");
            }
        }
        public ActionResult CreditCardInfo()
        {
            var model = new AphidLabAccountModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                model = _aphidlabs.GetAphidLabAccountInfo(userID);
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
        public ActionResult CreditCardInfo(AphidLabAccountModel model, string stripeToken)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                AphidLabAccountModel aphidData = _aphidlabs.GetAphidLabAccountInfo(userID);
                var success = _cmn.UpdateStripeCard(userID, stripeToken);
                if (!success)
                {
                    aphidData.Validation.AddError("Unable to change the credit card information on file");
                }
                else
                {
                    aphidData.Validation.AddInformation("Successfully changed your credit card information");
                }

                return View(aphidData);
            }
            catch (Exception)
            {
                return RedirectToAction("AphidLabsAccountInfo");
            }
        }

        public ActionResult ByteTracker()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
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
                for (int i = 0; i < li.Count; i++)
                {
                    if (li[i].Category == "Video")
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
                            DateShow = li[i].DateShow

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
        public ActionResult UploadVideo()
        {
            return View();
        }
        public ActionResult UploadSoftware()
        {
            return View();
        }
        public ActionResult AphidLAbUpload()
        {
            return View();
        }
        public ActionResult DataPlan()
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
        public ActionResult Message()
        {
            return View();
        }
        public ActionResult History()
        {
            return View();
        }
        public ActionResult ChannelInformation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadSoftware(AphidLabsUpload AphidlabSoftwaremodel)
        {
            try
            {
                trackNo = RandomPassword.CreatePassword(7);
                long len = 0, imgLength = 0;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                if (videobyte != null)
                {
                    len = videobyte.Length;
                }
                if (imagepath != null)
                {

                    imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                }

                long length = len + imgLength;
                bool result = _cmn.CheckSpace(userID, length);
                if (result == true)
                {
                    AphidlabSoftwaremodel.UserID = userID;
                    AphidlabSoftwaremodel.CloneID = Guid.NewGuid();
                    AphidlabSoftwaremodel.TrackingNumber = trackNo;
                    if (videopath != "")
                    {
                        AphidlabSoftwaremodel.SoftwarePath = videopath;
                        //AphidLabVideoModel.VideoPath = AphidLabVideoModel.VideoFile;
                        // videopath = null;
                    }
                    if (imagepath != null)
                    {
                        AphidlabSoftwaremodel.MatrixImageBytePath = imagepath;
                        imagepath = null;
                    }
                    CreateLinkPostModel post = new CreateLinkPostModel();
                    InterruptedFileModel intModel = new InterruptedFileModel();
                    intModel.CloneId = Guid.NewGuid();
                    intModel.CreateDate = System.DateTime.Now;
                    //intModel.FileArray = songbyte;
                    if (intModel.FileName != null)
                    {
                        intModel.FileName = videopath.Substring(videopath.IndexOf("_") + 1);
                    }
                    else
                    {
                        intModel.FileName = null;
                    }
                    post.Category = "Software";
                    post.Channel = "Matrix";
                    post.Date = System.DateTime.Now;
                    post.Downloads = 0;
                    post.NoOfChannel = 0;
                    post.Title = AphidlabSoftwaremodel.Titleofupload;
                    post.TrackingNumber = trackNo;
                    post.Views = 0;
                    post.UserID = userID;
                    intModel.IsActive = true;
                    intModel.ModifiedDate = System.DateTime.Now;
                    intModel.UserId = userID;
                    intModel.CatId = 2;
                    intModel.TrackNumber = trackNo;
                    //if (videopath != null)
                    //{
                    //    intModel.VideoPath = videopath;
                    //    intModel.InterruptedFilePath = videopath;
                    //    videopath = null;
                    //}


                    if (videobyte != null)
                    {
                        post.FileSize = CalculateFileSize.Size(videobyte);
                    }


                    bool re = _aphidlabs.InsertAphidLabSoftware(AphidlabSoftwaremodel, intModel, post);
                    if (re == true)
                    {
                        bool res = _cmn.UpdateDataMemory(userID, length);
                    }
                    DropBind();
                    string count = _cmn.GetNewCount(userID);

                    AphidlabSoftwaremodel.NewCount = count;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();

        }
        [HttpPost]
        public ActionResult UploadVideo(AphidLabsUpload AphidLabVideoModel)
        {
            try
            {
                trackNo = RandomPassword.CreatePassword(7);
                long len = 0, imgLength = 0;
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                if (videobyte != null)
                {
                    len = videobyte.Length;
                }
                if (imagepath != null)
                {

                    imgLength = System.IO.File.ReadAllBytes(Server.MapPath(imagepath)).Length;
                }

                long length = len + imgLength;
                bool result = _cmn.CheckSpace(userID, length);
                if (result == true)
                {

                    AphidLabVideoModel.UserID = userID;
                    AphidLabVideoModel.CloneID = Guid.NewGuid();
                    AphidLabVideoModel.TrackingNumber = trackNo;
                    if (videopath != "")
                    {
                        AphidLabVideoModel.VideoPath = videopath;
                        //AphidLabVideoModel.VideoPath = AphidLabVideoModel.VideoFile;
                        // videopath = null;
                    }
                    if (imagepath != null)
                    {
                        AphidLabVideoModel.MatrixImageBytePath = imagepath;
                        imagepath = null;
                    }
                    CreateLinkPostModel post = new CreateLinkPostModel();
                    InterruptedFileModel intModel = new InterruptedFileModel();
                    intModel.CloneId = Guid.NewGuid();
                    intModel.CreateDate = System.DateTime.Now;
                    //intModel.FileArray = songbyte;
                    if (intModel.FileName != null)
                    {
                        intModel.FileName = videopath.Substring(videopath.IndexOf("_") + 1);
                    }
                    else
                    {
                        intModel.FileName = null;
                    }
                    post.Category = "Video";
                    post.Channel = "Matrix";
                    post.Date = System.DateTime.Now;
                    post.Downloads = 0;
                    post.NoOfChannel = 0;
                    post.Title = AphidLabVideoModel.Titleofupload;
                    post.TrackingNumber = trackNo;
                    post.Views = 0;
                    post.UserID = userID;
                    intModel.IsActive = true;
                    intModel.ModifiedDate = System.DateTime.Now;
                    intModel.UserId = userID;
                    intModel.CatId = 2;
                    intModel.TrackNumber = trackNo;
                    if (videopath != null)
                    {
                        intModel.VideoPath = videopath;
                        intModel.InterruptedFilePath = videopath;
                        videopath = null;
                    }


                    if (videobyte != null)
                    {
                        post.FileSize = CalculateFileSize.Size(videobyte);
                    }


                    bool re = _aphidlabs.InsertAphidLabVideo(AphidLabVideoModel, intModel, post);
                    if (re == true)
                    {
                        bool res = _cmn.UpdateDataMemory(userID, length);
                    }
                    DropBind();
                    string count = _cmn.GetNewCount(userID);

                    AphidLabVideoModel.NewCount = count;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();

        }
        public List<BindDropDown> DropBind()
        {
            List<BindDropDown> li = new List<BindDropDown>();
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
            li = _basic.BindDropImage(userID);
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
            return li;
        }
        [HttpPost]
        public ContentResult VideoMatrixImage()
        {
            HttpPostedFileBase hpf = null;
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                hpf.SaveAs(Server.MapPath(savedFileName));
                imagepath = savedFileName;
            }
            return Content("{\"name\":\"" + imagepath + "\"}");

        }

        [HttpPost]
        public ContentResult UploadVideolAb()
        {
            HttpPostedFileBase hpf = null;
            string guid = Guid.NewGuid().ToString();
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                var ConvertedPath = ConverttoMp4(hpf.FileName, hpf);
                System.IO.File.Delete(Server.MapPath("/OriginalVideo/" + hpf.FileName));
                //System.IO.File.Delete(Server.MapPath("/TempBasicImages/"+hpf.FileName));
                //string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;   //string savedFileName = (@"\TempBasicImages\" + guid + "_" + hpf.FileName);
                //hpf.SaveAs(Server.MapPath(savedFileName));
                string savedFileName = "/TempBasicImages/" + ConvertedPath.ToString();
                var aa = System.IO.File.Exists(Server.MapPath(savedFileName));
                if (aa == true)
                {
                }
                videopath = savedFileName;
                videobyte = System.IO.File.ReadAllBytes(Server.MapPath(savedFileName));
            }
            return Content("{\"name\":\"" + hpf.FileName + "$" + videopath + "\"}");

        }
        public ActionResult AphidLabChannel()
        {
            return View();
        }
        public ActionResult EditAphidLAbChannel()
        {
            return View();
        }

        public string ConverttoMp4(string fileName, HttpPostedFileBase hpf)
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
            string filepath = Server.MapPath("~/OriginalVideo/" + fileName);
            while (System.IO.File.Exists(filepath))
            {
                j = j + 1;
                int dotPos = fileName.LastIndexOf(".");
                string namewithoutext = fileName.Substring(0, dotPos);
                string ext = fileName.Substring(dotPos + 1);
                fileName = namewithoutext + j + "." + ext;
                filepath = Server.MapPath("~/OriginalVideo/" + fileName);
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
            System.IO.FileInfo a = new System.IO.FileInfo(Server.MapPath(outPutFile));
            while (a.Exists == false)
            {

            }
            long b = a.Length;
            while (i != b)
            {

            }
            string cmd = " -i \"" + inputPath + "\\" + fileName + "\" \"" + outputPath + "\\" + guid + "_" + fileName.Remove(fileName.IndexOf(".")) + ".mp4" + "\"";
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
            exepath = AppPath + "ffmpeg.exe";
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

        public ActionResult Verification(Guid id)
        {


            AphidLabAccountModel aphidlabData = _aphidlabs.GetAphidLabAccountInfo(id);
            if (aphidlabData.isActive == true)
            {
                ViewBag.Message = "you are already verified";
            }
            else
            {
                bool result = _aphidlabs.VerifyEmailAccount(id);
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
        public ActionResult SendVerificationMail()
        {
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
            AphidLabAccountModel aphidData = _aphidlabs.GetAphidLabAccountInfo(userID);
            Email mail = new Email();//send mail                
            mail.sendMaill(aphidData.AphidlabUserID.Value, aphidData.EmailAddress, "AphidLab", new Guid(), aphidData.UserName, "VerifyEmail");
            return View();
        }
    }
}
