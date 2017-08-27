using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class AdminModel
    {
        public string Msg { get; set; }
        public HttpPostedFileBase Image { get; set; }
       
        public int? ReleaseID { get; set; }
        public int DBID { get; set; }
        public string ImagePath { get; set; }
    }

   public class RegisterAdmin
   {
       public string UserName { get; set; }
       public string Password { get; set; }
       public string ConfirmPassword { get; set; }
      
   }
}
