using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AphidBytes.Accounts.Contracts;
using System.Web.Mvc;
using System.Web.Hosting;
using System.Drawing;
using AphidBytes.Accounts.Contracts.Model;
using System.IO;

namespace AphidBytes.Web.Models
{
    public class UserSponsoredVidInterruptionmodel
    {
        public string VideoInterruption(string IntStyle,string IntFile,string FileName,string Title,string session,string trackno)
        {
            var filePath = "";
            var tt = "";
            string guid = Guid.NewGuid().ToString();
            try
            {
                IUserSponsored usersp = DependencyResolver.Current.GetService<IUserSponsored>();
                List<BindDropDown> li = new List<BindDropDown>();
                if (IntFile != "Default AphidByte")
                {
                   // li = basic.GetVideoInterruptionImage(userid);
                    //byte[] gg =System.IO.File.ReadAllBytes(li[0].ImageName);
                    string imgName = li[0].ImageName;

                    filePath = HostingEnvironment.MapPath(imgName);
                }
                else
                {
                    tt = Title;
                    Title = "";
                    var defaultImage = HostingEnvironment.MapPath(@"/DefaultFiles/Logo_Tech_.png");
                    filePath = defaultImage;
                }
                Bitmap bitmap = null;
                PointF firstLocation = new PointF(10f, 10f);
                PointF secondLocation = new PointF(10f, 50f);
                PointF thirdLocation = new PointF(10f, 90f);
                PointF fourthLocation = new PointF(10f, 130f);
                using (var stream = File.OpenRead((filePath)))
                {
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                }
                   using (bitmap)
                using (var graphics = Graphics.FromImage(bitmap))
                using (var font = new Font("Arial", 20, FontStyle.Regular))
                {
                    graphics.DrawString(session.ToString(), font, Brushes.Red, firstLocation);
                    graphics.DrawString(Title.ToString(), font, Brushes.Red, secondLocation);
                    graphics.DrawString(trackno, font, Brushes.Red, thirdLocation);
                    bitmap.Save(HostingEnvironment.MapPath(@"/TempBasicImages/" + guid + "_myfile.jpg"));
                }
            }
            catch (Exception)
            {

                throw;
            }


            return @"/TempBasicImages/" + guid + "_myfile.jpg";
          
 
        }

        public string EditImage(string path, string ComposerName, string title, string trackno, Guid userid)
        {
            Guid guid = Guid.NewGuid();
            IBasic basic = DependencyResolver.Current.GetService<IBasic>();
           
            string Editpath = @"/TempBasicImages/" + guid + "_myfile.jpg";
            Bitmap bitmap = null;
            PointF firstLocation = new PointF(10f, 10f);
            PointF secondLocation = new PointF(10f, 50f);
            PointF thirdLocation = new PointF(10f, 90f);
            PointF fourthLocation = new PointF(10f, 130f);
            // Create from a stream so we don't keep a lock on the file.
            using (var stream = File.OpenRead((HostingEnvironment.MapPath(path))))
            {
                bitmap = (Bitmap)Bitmap.FromStream(stream);
            }
            using (bitmap)
            using (var graphics = Graphics.FromImage(bitmap))
            using (var font = new Font("Arial", 20, FontStyle.Regular))
            {
                graphics.DrawString(ComposerName.ToString(), font, Brushes.Red, firstLocation);
                graphics.DrawString(title.ToString(), font, Brushes.Red, secondLocation);
                graphics.DrawString(trackno, font, Brushes.Red, thirdLocation);
             
                bitmap.Save(HostingEnvironment.MapPath(@"/TempBasicImages/" + guid + "_myfile.jpg"));

            }
            return Editpath;
        }

    }
}