using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
namespace AphidBytes.Web.Models
{
    public class CaptchaModel :ActionResult
    {
           
        public string GetCaptchaString(int length)
        {
            int intZero = '0';
            int intNine = '9';
            int intA = 'a';
            int intZ = 'z';
            int intb = 'A';
            int intG = 'Z';
            int intCount = 0;
            int intRandomNumber = 0;
            string strCaptchaString = "";
            
            Random random = new Random(System.DateTime.Now.Millisecond);

            while (intCount < length)
            {
                intRandomNumber = random.Next(intZero, intZ);
                if (((intRandomNumber >= intZero) && (intRandomNumber <= intNine) || (intRandomNumber >= intA) && (intRandomNumber <= intZ) || (intRandomNumber >= intb) || (intRandomNumber <= intG)))
                {
                    strCaptchaString = strCaptchaString + (char)intRandomNumber;
                    intCount = intCount + 1;
                }
            }
            
            return strCaptchaString;
        }


        public override void ExecuteResult(ControllerContext context)
        {
            
            Bitmap bmp = new Bitmap(100, 30);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            string randomString = GetCaptchaString(6);
            string regular = "[<>]+";
            string replace = "p";
            Regex rex = new Regex(regular);
            randomString = rex.Replace(randomString, replace);
            context.HttpContext.Session["captchastring"] = randomString;
            g.DrawString(randomString, new Font("Courier", 14), new SolidBrush(Color.Black), 3, 3);
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "image/jpeg";
            bmp.Save(response.OutputStream, ImageFormat.Jpeg);
            bmp.Dispose();
        }
    }
}