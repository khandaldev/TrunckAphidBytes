using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class RandomPassword
    {
        public static string CreatePassword(int length)
        {
            string valid = "123456789";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }
    }
}