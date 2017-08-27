using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace AphidBytes.Web.Models
{
    public class PremiumVideoInterruption
    {
        public string VideoInterruption(Guid userid, string IntStyle, string IntFile, string filename, string category, string title, string session)
        {
            var filePath = "";
            var tt = "";
            string PATH = "";
            Guid g = Guid.NewGuid();
            try
            {
                IPremium pre = DependencyResolver.Current.GetService<IPremium>();
                List<BindDropDown> li = new List<BindDropDown>();
                if (IntFile == "Custom Audio Interruption")
                {
                    tt = title;
                    li = pre.BindDropImage(userid);
                    filePath = HostingEnvironment.MapPath(li[0].ImageName);
                    PATH = li[0].ImageName;
                }
                else
                {
                     tt = title;
                    title = "";
                    
                    FileInfo fi = new FileInfo(HostingEnvironment.MapPath(@"/DefaultFiles/Logo_Tech_.png"));
                    fi.CopyTo(HostingEnvironment.MapPath(@"/Uploaded Files/ " + g + "_Logo_Tech_.png"), true);
                    filePath = HostingEnvironment.MapPath(@"/Uploaded Files/ " + g + "_Logo_Tech_.png");
                    PATH = "/Uploaded Files/ " + g + "_Logo_Tech_.png";
                }
                string name = pre.GetPremiumWebsite(userid);

                Bitmap bitmap = null;
                PointF firstLocation = new PointF(10f, 10f);
                PointF secondLocation = new PointF(10f, 50f);
                PointF thirdLocation = new PointF(10f, 90f);
                PointF fourthLocation = new PointF(10f, 130f);
                // Create from a stream so we don't keep a lock on the file.
                using (var stream = File.OpenRead(filePath))
                {
                    bitmap = (Bitmap)Bitmap.FromStream(stream);
                }

                using (bitmap)
                using (var graphics = Graphics.FromImage(bitmap))
                using (var font = new Font("Arial", 20, FontStyle.Regular))
                {
                    graphics.DrawString(session.ToString(), font, Brushes.Red, firstLocation);
                    graphics.DrawString(tt.ToString(), font, Brushes.Red, secondLocation);
                    graphics.DrawString("TrackingNumber", font, Brushes.Red, thirdLocation);
                    graphics.DrawString(name.ToString(), font, Brushes.Red, fourthLocation);
                    bitmap.Save(filePath);
                }

            }
            catch (Exception)
            {

                throw;
            }


            return PATH;
        }
    }
}