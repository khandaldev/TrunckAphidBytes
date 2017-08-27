using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.Hosting;
using System.Drawing;
using AphidBytes.Accounts.Contracts;
using System.Web.Mvc;
using System.Drawing.Drawing2D;
using AphidBytes.Accounts.Contracts.Model;
namespace AphidBytes.Web.Models
{
    public class PdfIntrreputionModel
    {
        //    public string PdfIntreption(string pdfpath, string composer, string title, Guid userid, string IntFile, string imagepath)
        //    {
        //        Guid guid = Guid.NewGuid();
        //        IBasic basic = DependencyResolver.Current.GetService<IBasic>();
        //        string track = RandomPassword.CreatePassword(7);
        //        string outputpath = "/TempBasicImages/" + guid + "_file.pdf";
        //       // int id = Convert.ToInt32(IntFile);
        //        try
        //        {
        //            string imgdefault = "/DefaultFiles/Logo_Tech_.png";
        //            string imgpath = "/DefaultFiles/as_page.png";
        //           // Bitmap b = new Bitmap(HostingEnvironment.MapPath(imgdefault));
        //           //System.Drawing.Image i = resizeImage(b, new Size(100, 100));
        //            VideoInterruptionModel model = new VideoInterruptionModel();
        //            string EditImgPath = model.EditImage(imgpath, composer, title, track, userid, IntFile, imagepath);

        //            Document document = new Document();
        //            PdfReader pdfReader = new PdfReader(HostingEnvironment.MapPath(pdfpath));
        //            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(HostingEnvironment.MapPath(outputpath), FileMode.Create, FileAccess.Write, FileShare.None));
        //            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath(EditImgPath));
        //            img.SetAbsolutePosition(10, 10);
        //            PdfContentByte waterMark;
        //            for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++) 
        //            {
        //                waterMark = pdfStamper.GetOverContent(pageIndex);
        //                waterMark.AddImage(img);
        //            }
        //            //Add User Image
        //            if (IntFile == "Custom Interruption")
        //            {
        //                imgdefault = basic.GetImageName(userid);
        //            }
        //            if (imgdefault != "")
        //            {
        //                iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath(imgdefault));
        //                img1.SetAbsolutePosition(100, 100);
        //                PdfContentByte waterMark1;
        //                for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
        //                {
        //                    waterMark1 = pdfStamper.GetOverContent(pageIndex);
        //                    waterMark1.AddImage(img1);
        //                }
        //                pdfStamper.FormFlattening = true;
        //                pdfStamper.Close();
        //            }

        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //        return outputpath+"@"+track;
        //    }


        //    private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        //    {
        //        int sourceWidth = imgToResize.Width;
        //        int sourceHeight = imgToResize.Height;
        //        float nPercent = 0;
        //        float nPercentW = 0;
        //        float nPercentH = 0;
        //        nPercentW = ((float)size.Width / (float)sourceWidth);
        //        nPercentH = ((float)size.Height / (float)sourceHeight);
        //        if (nPercentH < nPercentW)
        //            nPercent = nPercentH;
        //        else
        //            nPercent = nPercentW;
        //        int destWidth = (int)(sourceWidth * nPercent);
        //        int destHeight = (int)(sourceHeight * nPercent);
        //        Bitmap b = new Bitmap(destWidth, destHeight);
        //        Graphics g = Graphics.FromImage((System.Drawing.Image)b);
        //        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        //        g.Dispose();
        //        return (System.Drawing.Image)b;

        //    }


        //}
        //}
        public string PdfIntreption(string pdfpath, string composer,string Percentage, string title, Guid userid, string IntF, string imagepath)
        {

            Guid guid = Guid.NewGuid();
            IBasic basic = DependencyResolver.Current.GetService<IBasic>();
            string track = RandomPassword.CreatePassword(7);
            string outputpath = "/TempBasicImages/" + guid + "_file.pdf";
            // int id = Convert.ToInt32(IntFile);
            //IntF = Session["IntF"]; 
            //try
            //{
            if (IntF == "No Interruption" || IntF == "Default AphidByte" || IntF == "Custom Audio Interruption")
            {
                
                    string imgdefault = "/DefaultFiles/Logo_Tech_.png";
                    string imgpath = "/DefaultFiles/as_page.png";
                    // Bitmap b = new Bitmap(HostingEnvironment.MapPath(imgdefault));
                    //System.Drawing.Image i = resizeImage(b, new Size(100, 100));
                    VideoInterruptionModel model = new VideoInterruptionModel();
                    string EditImgPath = model.EditImage(imgpath, composer, title, track, userid, IntF, imagepath);

                    Document document = new Document();
                    if (IntF == "Default AphidByte")
                    {
                        PdfReader pdfReader = new PdfReader(HostingEnvironment.MapPath(pdfpath));
                        PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(HostingEnvironment.MapPath(outputpath), FileMode.Create, FileAccess.Write, FileShare.None));
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath(imgdefault));
                        if (Percentage == "25%")
                        {
                            img.SetAbsolutePosition(20, 50);
                            PdfContentByte waterMark;
                            for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
                            {
                                waterMark = pdfStamper.GetOverContent(pageIndex);
                                waterMark.AddImage(img);

                                pdfStamper.FormFlattening = true;
                                pdfStamper.Close();
                            }
 
                        }
                        else if (Percentage == "50%")
                        {
                            img.SetAbsolutePosition(100, 300); 
                            PdfContentByte waterMark;
                            for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
                            {
                                waterMark = pdfStamper.GetOverContent(pageIndex);
                                waterMark.AddImage(img);

                                pdfStamper.FormFlattening = true;
                                pdfStamper.Close();
                            }

                        }

                        else if (Percentage == "75%")
                        {
                            img.SetAbsolutePosition(150, 100);
                            PdfContentByte waterMark;
                            for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
                            {
                                waterMark = pdfStamper.GetOverContent(pageIndex);
                                waterMark.AddImage(img);

                                pdfStamper.FormFlattening = true;
                                pdfStamper.Close();
                            }

                        }
                   
                }

                return outputpath + "@" + track;
            }

            if (IntF == "Custom Interruption")
            {
                PdfReader pdfReader = new PdfReader(HostingEnvironment.MapPath(pdfpath));
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(HostingEnvironment.MapPath(outputpath), FileMode.Create, FileAccess.Write, FileShare.None));
                //iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath(imgdefault));
                iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath(imagepath));
                img1.SetAbsolutePosition(100, 100);
                PdfContentByte waterMark1;
                for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
                {
                    waterMark1 = pdfStamper.GetOverContent(pageIndex);
                    waterMark1.AddImage(img1);

                    pdfStamper.FormFlattening = true;
                    pdfStamper.Close();
                }
            }


            //catch (Exception)
            //{

            //    throw;
            //}
            {
                return outputpath + "@" + track;
            }

        }


        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;

        }


    }
}