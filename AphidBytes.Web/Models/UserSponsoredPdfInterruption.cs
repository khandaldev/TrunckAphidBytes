using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace AphidBytes.Web.Models
{
    public class UserSponsoredPdfInterruption
    {
        public string PdfIntreption(string pdfpath, string composer, string title, Guid userid, string IntFile)
        {
            Guid guid = Guid.NewGuid();
            IUserSponsored uspspon = DependencyResolver.Current.GetService<IUserSponsored>();
            string track = RandomPassword.CreatePassword(7);
            string outputpath = "/TempBasicImages/" + guid + "_file.pdf";
           
            try
            {
                string imgdefault = "/DefaultFiles/Logo_Tech_.png";
                string imgpath = "/DefaultFiles/as_page.png";
               
                UserSponsoredVidInterruptionmodel model = new UserSponsoredVidInterruptionmodel();
                string EditImgPath = model.EditImage(imgpath, composer, title, track, userid);

                Document document = new Document();
                PdfReader pdfReader = new PdfReader(HostingEnvironment.MapPath(pdfpath));
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(HostingEnvironment.MapPath(outputpath), FileMode.Create, FileAccess.Write, FileShare.None));
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath(EditImgPath));
                img.SetAbsolutePosition(30, 30);
                PdfContentByte waterMark;
                for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
                {
                    waterMark = pdfStamper.GetOverContent(pageIndex);
                    waterMark.AddImage(img);
                }
                //Add User Image
             
                iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath(imgdefault));
                img1.SetAbsolutePosition(10, 10);
                PdfContentByte waterMark1;
                for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
                {
                    waterMark1 = pdfStamper.GetOverContent(pageIndex);
                    waterMark1.AddImage(img1);
                }
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return outputpath + "@" + track;
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