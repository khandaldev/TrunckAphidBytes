using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Processor;
using System.IO;
using AphidBytes.Web.Models;
using System.Drawing;
using AphidBytes.Web.Web;

namespace AphidBytes.Web.Controllers
{
    public class UserSponsoredController : AphidController
    {
        static byte[] gg = null;
        static string imagepath = "";
        static string imagebyte = "";
        static string trackno = "";
        static string songpath = "";
        static string songname = "";
        static string IntrepputedAudioPath = "";
        static string ImageByte = "";
        static string ImageByte1 = "";
        static string FilePhoto = null;
        static string intphoto = null;
        static byte[] Zip = null;
        static string ZipPath = null;
        static byte[] videobyte = null;
        static string videopath = null;
        static string session = "";
        static string artphototitle = "";
        static string pdffilepath = "";
        static string albumpath = "";
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserSponsoredFileUploadAdminView()
        {
            return View();
        }
        public ActionResult SponsoredByteYourMusic()
        {
            return View();
        }
        public ActionResult SponsoredSingle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MatrixImage()
        {
            try
            {
                var mm = "";
                HttpPostedFileBase hpf = null;
                string guid = Guid.NewGuid().ToString();
                foreach (string file in Request.Files)
                {
                    hpf = Request.Files[file] as HttpPostedFileBase;
                    var split = hpf.FileName.Split('.');
                    if (split[1] == "jpg" || split[1] == "JPG" || split[1] == "png" || split[1] == "PNG")
                    {
                        string saveimage = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                        string filename = Path.GetFileName(Request.Files[file].FileName);
                        Request.Files[file].SaveAs(Server.MapPath(saveimage));
                        imagepath = saveimage;
                        mm = imagepath;
                    }
                    else
                    {
                        mm = "InValid";
                    }
                }
                return Content("{\"name\":\"" + mm + "\"}");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "UserSponsered");
            }
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
                    hpf.SaveAs(Server.MapPath(artphototitle));
                    pdffilepath = artphototitle;
                    //  string fileinp = hpf.FileName;
                }
                else
                {

                    filename = "Invalid";
                }


            }
            return Content("{\"name\":\"" + filename + "$" + artphototitle + "\"}");
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
                    string savedImageName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                    hpf.SaveAs(Server.MapPath(savedImageName));
                    ImageByte = savedImageName;
                    mm = savedImageName;
                }
                else
                {
                    mm = "Invalid";
                }
            }
            return Content("{\"name\":\"" + mm + "\"}");
        }
        public ActionResult PreviewMatrixImage()
        {
            try
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
                        string savedImageName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                        hpf.SaveAs(Server.MapPath(savedImageName));
                        ImageByte1 = savedImageName;
                        mm = savedImageName;
                    }
                    else
                    {
                        mm = "Invalid";
                    }
                }
                return Content("{\"name\":\"" + mm + "\"}");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "UserSponsered");
            }
        }
        [HttpPost]
        public ActionResult MatrixImagePre()
        {
            try
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
            catch (Exception)
            {

                return RedirectToAction("Index", "UserSponsered");
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
                    hpf.SaveAs(Server.MapPath(savedZipPath));
                    Zip = System.IO.File.ReadAllBytes(Server.MapPath(savedZipPath));
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
        public ContentResult UploadVideo()
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
                    videopath = savedFileName;
                    // videobyte = System.IO.File.ReadAllBytes(savedFileName);
                    videobyte = System.IO.File.ReadAllBytes(Server.MapPath(savedFileName));
                }
                else
                {
                    videopath = "Invalid";
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

        public ContentResult PreviewAudio()
        {
            var val = "";
            HttpPostedFileBase hpf = null;
            string guid = Guid.NewGuid().ToString();

            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                val = hpf.FileName;
                var split = val.Split('.');
                if (split[1] == "mp3" || split[1] == "MP3" || split[1] == "wav" || split[1] == "WAV")
                {
                    string savedFileName = "/TempBasicImages/" + guid + "_" + hpf.FileName;
                    hpf.SaveAs(Server.MapPath(savedFileName));
                    songpath = savedFileName;
                    val = songpath;

                }
                else
                {
                    val = "InValid";
                }
                
               
            }
            return Content("{\"name\":\"" + val + "\"}");
        }
        public CaptchaModel ShowCaptchaImage()
        {
            return new CaptchaModel();
        }
        public ActionResult GetCaptcha()
        {

            var da = "";
            if (Session["captchastring"] != null)
            {
                da = Session["captchastring"].ToString();
            }

          
            return Json(da);
        }
        public ActionResult InterruptVideo(string IntS, string IntF, string FileName, string Title)
        {
            UserSponsoredVidInterruptionmodel model = new UserSponsoredVidInterruptionmodel();
            string name = model.VideoInterruption(IntS, IntF, FileName, Title, session, trackno);
            return Json(name + "&" + videopath, JsonRequestBehavior.AllowGet);
 
        }

        [HttpPost]
        public ActionResult SponsoredSingle(SponsoredModel model)
        {
            trackno = RandomPassword.CreatePassword(7);
            if (model.Isvalid==true)
            {
                string Intpath = AudioIntrepption(model.InteruptionStyle, model.SelectedInteruptionFile, songpath);
               
                string captcha = Session["captchastring"].ToString();
                if (model.Captcha == captcha)
                {

                    try
                    {
                        IUserSponsored usersp = DependencyResolver.Current.GetService<IUserSponsored>();
                        //model = songname.Substring(songname.IndexOf('_') + 1);
                        model.Type = "Single";
                        model.TrackingNumber = trackno;
                        model.CatID = 1;
                        model.FileNames =songname.Substring(songname.IndexOf("_") + 1);

                        if (imagepath != null)
                        {
                            model.MatrixImagePath = imagepath;
                        }
                        if (songpath != null)
                        {
                            model.AudioFilePath = songpath;
                         

                        }

                        if (gg != null)
                        {
                            model.UploadFilePath = IntrepputedAudioPath;
                            byte[] arr = System.IO.File.ReadAllBytes(Server.MapPath(Intpath));
                            model.FileSize = CalculateFileSize.Size(arr);

                        }
                        else
                        {
                            model.AudioFilePath = songpath;
                            byte[] arr = System.IO.File.ReadAllBytes(Server.MapPath(songpath));
                            model.FileSize = CalculateFileSize.Size(arr);
                        }


                        model.TrackingNumber = trackno;
                        model.IsActive = true;
                        AllGenerateCloneModel allmodel = new AllGenerateCloneModel();
                        allmodel.TrackingNumber = trackno;
                        allmodel.AudioFilePath = model.AudioFilePath;
                        allmodel.filesize = model.FileSize;
                        allmodel.UploadFilePath = model.UploadFilePath;
                        allmodel.Title = model.Title;
                        allmodel.Tag = model.Tag;
                        allmodel.ComposerName = model.ComposerName;
                        allmodel.Producer = model.Producer;
                        allmodel.Publisher = model.Publisher;
                        allmodel.AlbumTitle = model.AlbumTitle;
                        allmodel.SelectedInteruptionFile = model.SelectedInteruptionFile;
                        allmodel.InteruptionStyle = model.InteruptionStyle;
                        allmodel.AvailableForDownload = model.AvailableForDownload;
                        allmodel.ExplicitContent = model.ExplicitContent;
                        allmodel.FileNames = model.FileNames;
                        allmodel.MatrixImagePath = model.MatrixImagePath;
                        allmodel.Type="Single";
                        allmodel.IsActive = true;
                        allmodel.CatID = 1;
                        allmodel.CreatotName = model.CreatotName;
                        bool re = usersp.InsertSingleMusic(model,allmodel);



                    }

                    catch
                    {
                        return RedirectToAction("LoginUser", "Accounts");

                    }
                }
                
 
            }
            return View();
 
        }
        public ActionResult Albumaudio()
        {
            return View();
        }
        public ActionResult SponsoredAlbum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SponsoredAlbum(SponsoredModel model)
        {
            if (model.Isvalid == true)
            {
                var captcha = Session["captchastring"].ToString();
                string track = RandomPassword.CreatePassword(7);
                if (model.Captcha == captcha)
                {
                    IUserSponsored uspspon = DependencyResolver.Current.GetService<IUserSponsored>();
                    model.Type = "Album";
                    if (imagepath != null)
                    {
                        model.MatrixImagePath = imagepath;
                    }
                    if (songpath != null)
                    {
                        model.AudioFilePath = songpath;
                    }
                    if (albumpath != null)
                    {
                        
                    }
                    if (gg != null)
                    {
                        model.UploadFilePath = IntrepputedAudioPath;
                        byte[] size = System.IO.File.ReadAllBytes(Server.MapPath(IntrepputedAudioPath));
                        model.FileSize = CalculateFileSize.Size(size);
                    }
                    else
                    {
                        model.AudioFilePath = songpath;
                        byte[] size = System.IO.File.ReadAllBytes(Server.MapPath(songpath));
                        model.FileSize = CalculateFileSize.Size(size);
                    }
                    model.TrackingNumber = track;
                    model.IsActive = true;
                    AllGenerateCloneModel allmodel = new AllGenerateCloneModel();
                    bool re = uspspon.InsertAlbum(model,allmodel);

                }
            }
            return View(model);
           
        }
        public ActionResult SponsoredByteYourVideo()
        {
            return View();
                
        }
        [HttpPost]
        public ActionResult SponsoredByteYourVideo(SponsoredModel model)
        {
            try
            {
                if (model.Isvalid == true)
                {
                    string track = RandomPassword.CreatePassword(7);
                    var captcha = Session["captchastring"].ToString();
                    if (model.Captcha == captcha)
                    {
                        IUserSponsored usersp = DependencyResolver.Current.GetService<IUserSponsored>();

                        model.TrackingNumber = track;
                        model.CatID = 2;
                        model.Type = "Video";
                        model.FileNames = videopath.Substring(videopath.IndexOf("_") + 1);
                       
                        if (imagepath != null)
                        {
                            model.MatrixImagePath = imagepath;
                        }
                        if (videopath != null)
                        {
                            model.VideoFilePath = videopath;

                        }
                        if (videobyte != null)
                        {
                            model.FileSize = CalculateFileSize.Size(videobyte);
                        }
                        model.IsActive = true;
                        AllGenerateCloneModel allmodel = new AllGenerateCloneModel();
                        allmodel.Title = model.Title;
                        allmodel.Tag = model.Tag;
                        allmodel.Type = "Video";
                        allmodel.MatrixFilePath = model.MatrixFilePath;
                        allmodel.InteruptionStyle = model.InteruptionStyle;
                        allmodel.VideoFilePath = model.VideoFilePath;
                        allmodel.filesize = model.FileSize;
                        allmodel.TrackingNumber = track;
                        allmodel.CatID = 2;
                        allmodel.VideoFilePath = model.VideoFilePath;
                        allmodel.Producer = model.Producer;
                        allmodel.Publisher = model.Publisher;
                        allmodel.AlbumTitle = model.AlbumTitle;
                        allmodel.CreatotName = model.CreatotName;
                        allmodel.ExplicitContent = model.ExplicitContent;
                        allmodel.AvailableForDownload = model.AvailableForDownload;
                        allmodel.FileNames = model.FileNames;
                        allmodel.SelectedInteruptionFile = model.SelectedInteruptionFile;
                        allmodel.InteruptionStyle = model.InteruptionStyle;

                        allmodel.IsActive = true;
                        bool re = usersp.InsertByteyourVideo(model,allmodel);
                    }


                }
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "UserSponsered");
            }
        }
        public ActionResult SponsoredByteYourArtAndPhotography()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SponsoredByteYourArtAndPhotography(SponsoredModel model)
        {
            try
            {
                if (model.Isvalid == true)
                {
                    string intpath = PhotoInsertion(model.SelectedInteruptionFile, ImageByte, ImageByte1);
                    string track = RandomPassword.CreatePassword(7);
                    string captcha = Session["captchastring"].ToString();
                    if (model.Captcha == captcha)
                    {
                        IUserSponsored usersp = DependencyResolver.Current.GetService<IUserSponsored>();
                       
                        model.TrackingNumber = track;
                        model.CatID = 3;
                        model.Type = "Photo";
                        model.FileNames = ImageByte.Substring(ImageByte.IndexOf("_") + 1);
                        if (ImageByte != null)
                        {
                            model.UploadImageFilePath = ImageByte;
                        }
                        if (ImageByte1 != null)
                        {
                            model.MatrixImagePath = ImageByte1;
                        }
                        if (intphoto != null)
                        {
                            model.MatrixFilePath = intphoto;
                            byte[] arr = System.IO.File.ReadAllBytes(Server.MapPath(intphoto));
                            model.FileSize = CalculateFileSize.Size(arr);

                        }
                        model.IsActive = true;
                        AllGenerateCloneModel allmodel = new AllGenerateCloneModel();
                        allmodel.Title = model.Title;
                        allmodel.Tag = model.Tag;
                        allmodel.ExplicitContent = model.ExplicitContent;
                        allmodel.AvailableForDownload = model.AvailableForDownload;
                        allmodel.ComposerName = model.ComposerName;
                        allmodel.AlbumTitle = model.AlbumTitle;
                        allmodel.TrackingNumber = track;
                        allmodel.IsActive = true;
                        allmodel.MatrixImagePath = model.MatrixImagePath;
                        allmodel.UploadImageFilePath = model.UploadImageFilePath;
                        allmodel.MatrixFilePath = model.MatrixFilePath;
                        allmodel.FileNames = model.FileNames;
                        allmodel.Type = "Photo";
                        allmodel.SelectedInteruptionFile = model.SelectedInteruptionFile;
                        allmodel.MatrixFilePath = model.MatrixFilePath;
                        allmodel.WaterMarkMatrixImagePath = model.WaterMarkMatrixImagePath;
                        allmodel.CatID = 3;

                        bool re = usersp.InsertPhotoArt(model,allmodel);


                    }

                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");

            }
            return View(model);
 
        }
        public ActionResult PhotoInterruption(string IntF, string FileName, string IntS)
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

                    string first = Server.MapPath(FileName);
                    string second = Server.MapPath("/DefaultFiles/Logo_Tech_.png");
                    string savePath = Server.MapPath(FileName + "file.jpg");
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
                    string first = Server.MapPath(FileName);
                    string second = Server.MapPath(IntS);
                    string savePath = Server.MapPath(FileName + "file.jpg");
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
        public ActionResult SponsoredByteYourEbook()
        {
            return View();        
        }
        [HttpPost]
        public ActionResult SponsoredByteYourEbook(SponsoredModel model)
        {
            string track;
            string pathpdf = null;
            if (model.Isvalid == true)
            {
                string captcha = Session["captchastring"].ToString();
                if (model.Captcha == captcha)
                {
                    IUserSponsored uspson=DependencyResolver.Current.GetService<IUserSponsored>();
                    if (model.SelectedInteruptionFile == "No Interruption")
                    {
                        track = RandomPassword.CreatePassword(7);
                        model.TrackingNumber = track;
                        model.UploadPDFFilePath = artphototitle;
                    }
                    else
                    {
                        UserSponsoredPdfInterruption obj = new UserSponsoredPdfInterruption();
                       pdffilepath=obj.PdfIntreption(artphototitle,model.ComposerName,model.Title,default(Guid),model.SelectedInteruptionFile);
                        string[] PDF = pdffilepath.Split('@');
                        pathpdf = PDF[0];
                        string[] trackno = pdffilepath.Split('@');
                        if (Session["TRACK"] != null)
                        {
                            track = Session["TRACK"].ToString();
                        }
                        else
                        {
                            track = trackno[1];
                        }
                        byte[] size = null;
                        if (artphototitle != null)
                        {
                            model.PdfFilePath = artphototitle;
                            size = System.IO.File.ReadAllBytes(Server.MapPath(artphototitle));
                        }
                        if (pathpdf != null)
                        {
                            model.UploadPDFFilePath = pathpdf;
                            size = System.IO.File.ReadAllBytes(Server.MapPath(pathpdf));
                        }
                        if (imagepath != null)
                        {
                            model.MatrixImagePath = imagepath;
                        }
                        if (size != null)
                        {
                            model.FileSize = CalculateFileSize.Size(size);
                        }
                        model.IsActive = true;
                        model.CatID = 4;
                        model.Type = "Ebook";
                        model.FileNames = artphototitle.Substring(artphototitle.IndexOf('_') + 1);
                        AllGenerateCloneModel allmodel = new AllGenerateCloneModel();
                        allmodel.Title = model.Title;
                        allmodel.Tag = model.Title;
                        allmodel.AlbumTitle = model.AlbumTitle;
                        allmodel.Producer = model.Producer;
                        allmodel.Publisher = model.Publisher;
                        allmodel.ComposerName = model.ComposerName;
                        allmodel.MatrixImagePath = model.MatrixImagePath;
                        allmodel.UploadPDFFilePath = model.UploadPDFFilePath;
                        allmodel.PdfFilePath = model.PdfFilePath;
                        allmodel.FileNames = model.FileNames;
                        allmodel.Type = "Ebook";
                        allmodel.SelectedInteruptionFile = model.SelectedInteruptionFile;
                        allmodel.PagePercentage = model.PagePercentage;
                        allmodel.AvailableForDownload = model.AvailableForDownload;
                        allmodel.ExplicitContent = model.ExplicitContent;
                        bool re = uspson.InsertByteYourEbook(model,allmodel);

                    }
                    
                }

            }
            return View(model);
          
        }

        public ActionResult PdfInterruption(string IntF, string FileName, string Percentage, string Image, string Title, string ComposerName, string Default)
        {
            UserSponsoredPdfInterruption obvpdf = new UserSponsoredPdfInterruption();
            pdffilepath = obvpdf.PdfIntreption(artphototitle, ComposerName, Title, default(Guid), IntF);
            string[] PDF = pdffilepath.Split('@');
            string pathpdf = PDF[0];
            string trackno = PDF[1];
            Session["TRACK"] = trackno;
            return Json(pathpdf);

        }

        public ActionResult SponsoredByteYourFile()
         {
             return View();
         }
        [HttpPost]
        public ActionResult SponsoredByteYourFile(SponsoredModel model)
        {
            if (model.Isvalid == true)
            {
                string captcha = Session["captchastring"].ToString();
                //if (model.Captcha == captcha)
                //{
                    IUserSponsored uspspon = DependencyResolver.Current.GetService<IUserSponsored>();
                    string track = RandomPassword.CreatePassword(7);
                    
                    byte[] size = null;
                    if (ZipPath != null)
                    {
                        model.RARFilePath = ZipPath;
                        size = System.IO.File.ReadAllBytes(Server.MapPath(ZipPath));

                    }
                    if (size != null)
                    {
                        model.FileSize = CalculateFileSize.Size(size);
                    }
                    if (imagepath != null)
                    {
                        model.MatrixImagePath = imagepath;
                    }
                    model.FileNames = ZipPath.Substring(ZipPath.IndexOf('_') + 1);
                    model.IsActive = true;
                    model.CatID = 4;
                    model.Type = "Files";
                    AllGenerateCloneModel allmodel = new AllGenerateCloneModel();
                    allmodel.Title = model.Title;
                    allmodel.Tag = model.Tag;
                    allmodel.ComposerName = model.ComposerName;
                    allmodel.Producer = model.Producer;
                    allmodel.Publisher = model.Publisher;
                    allmodel.AvailableForDownload = model.AvailableForDownload;
                    allmodel.CreatotName = model.CreatotName;
                    allmodel.ExplicitContent = model.ExplicitContent;
                    allmodel.RARFilePath = model.RARFilePath;
                    allmodel.MatrixImagePath = model.MatrixImagePath;
                    allmodel.filesize = model.FileSize;
                    allmodel.FileNames = model.FileNames;
                    allmodel.Type = "Files";
                    allmodel.IsActive = true;
                    allmodel.CatID = 4;

                    bool re = uspspon.InsertByteYourFile(model,allmodel);

                
               
            }
            return View(model);
        }

        public ActionResult Interruption(string IntS, string IntF, string FileName)
        {
            string IntrepputedAudioPath = AudioIntrepption(IntS,IntF,FileName);
            return Json(IntrepputedAudioPath);
        }
        public string AudioIntrepption(string IntS, string IntF, string FileName)
        {
            IUserSponsored usersp = DependencyResolver.Current.GetService<IUserSponsored>();
           // Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());

            Guid guid = Guid.NewGuid();
            songname = FileName;
            if (IntS == "Default Randomized Entry" && (IntF == "No Interruption" || IntF == "Default AphidByte"))
            {
                if (IntF == "Default AphidByte")
                {
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3"));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
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

           
            if (IntS == "Producer Tag Sequence" && (IntF == "No Interruption" || IntF == "Default AphidByte"))
            {

                if (IntF == "Default AphidByte")
                {
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3"));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
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
           
            if (IntS == "Beginning of File" && (IntF == "No Interruption" || IntF == "Default AphidByte"))
            {
                if (IntF == "Default AphidByte")
                {
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3"));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
                    List<byte> int1 = new List<byte>(mainAudio);
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
               
            if (IntS == "Ending of File" && (IntF == "No Interruption" || IntF == "Default AphidByte"))
            {
                if (IntF == "Default AphidByte")
                {
                    byte[] mainAudio = System.IO.File.ReadAllBytes(Server.MapPath(@"/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3"));//Upload by User
                    byte[] intreAudio = System.IO.File.ReadAllBytes(Server.MapPath(FileName));//File Selected For Interruption
                    List<byte> int1 = new List<byte>(intreAudio);
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
                
            return IntrepputedAudioPath;
        }
     

    }
            }

           
