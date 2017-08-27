using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public  class CalculateFileSize
    {
       public static string Size(byte[] array)
       {
           string totalsize = "";

           int len = array.Length;
           float dd = len / 1024f;
           if (dd > 1024)
           {
               float mm1 = dd / 1024f;
               if (mm1 > 1024)
               {
                   float gb = mm1 / 1024f;
                   if (gb >= 1024)
                   {
                       float tb = gb / 1024f;
                       string uu = tb + "tb";
                       totalsize = uu;
                   }
                   else { string oo = gb + "GB"; totalsize = oo; }
               }
               else
               {
                   string jj = mm1 + "MB";
                   totalsize = jj;
               }
           }
           else
           {
               string vv = dd + "KB";
               totalsize = vv;
           }
           return totalsize;
       }

       public static string ConvertFromLength(string length)
       {
           string totalsize = "";
           float dd =Convert.ToInt64(length) / 1024f;
           if (dd > 1024)
           {
               float mm1 = dd / 1024f;
               if (mm1 > 1024)
               {
                   float gb = mm1 / 1024f;
                   if (gb >= 1024)
                   {
                       float tb = gb / 1024f;
                       string uu = tb + "tb";
                       totalsize = uu;
                   }
                   else { string oo = gb + "GB"; totalsize = oo; }
               }
               else
               {
                   string jj = mm1 + "MB";
                   totalsize = jj;
               }
           }
           else
           {
               string vv = dd + "KB";
               totalsize = vv;
           }
           return totalsize;
       }
       public static string ConvertTotalsize(string used,string free)
       {
           string totalsize = "";
           float dd = Convert.ToInt64(used) / 1024f;
           float ee = Convert.ToInt64(free) / 1024f;
           float total = dd + ee;
           if (total > 1024)
           {
               float mm1 = dd / 1024f;
               if (mm1 > 1024)
               {
                   float gb = mm1 / 1024f;
                   if (gb >= 1024)
                   {
                       float tb = gb / 1024f;
                       string uu = tb + "tb";
                       totalsize = uu;
                   }
                   else { string oo = gb + "GB"; totalsize = oo; }
               }
               else
               {
                   string jj = mm1 + "MB";
                   totalsize = jj;
               }
           }
           else
           {
               string vv = total + "KB";
               totalsize = vv;
           }
           return totalsize;
           
       }
       public static string getFilesizeDesc(string length)
       {
            double sizeInBytes = Convert.ToDouble(length);
           const double Terabyte = 1099511627776;
           const double Gigabyte = 1073741824;
           const double Megabyte = 1048576;
           const double Kilobyte = 1024;

           string result = string.Empty;
           double the_size = 0;
           string units = string.Empty;

           if (sizeInBytes >= Terabyte)
           {
               the_size = sizeInBytes / Terabyte;
               
               units = the_size+" Tb";
           }
           else
           {
               if (sizeInBytes >= Gigabyte)
               {
                   the_size = sizeInBytes / Gigabyte;
                   var ss = String.Format("{0:.##}", the_size);
                   units = the_size.ToString("#.##") +" Gb";
                 
               }
               else
               {
                   if (sizeInBytes >= Megabyte)
                   {
                       the_size = sizeInBytes / Megabyte;
                       units = the_size.ToString("0.00") +" Mb";
                   }
                   else
                   {
                       if (sizeInBytes >= Kilobyte)
                       {
                           the_size = sizeInBytes / Kilobyte;
                           units =the_size+" Kb";
                       }
                       else
                       {
                           the_size = sizeInBytes;
                           units = the_size+" bytes";
                       }
                   }
               }
           }

           
           return units.ToString();
       }
 
    }
}
