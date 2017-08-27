using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class ForgetPassword
    {
        public ValidationModel Validation { get; set; } = new ValidationModel();
        [Required]
        public string UserName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string VerifiedEmail { get; set; }
        public Guid Token { get; set; }
        public Guid UserId { get; set; }        
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        
        public string InsertAnswer1 { get; set; }
        public string  AccountType { get; set; }
       public string InsertAnswer2 { get; set; }
        public string ValidAnswer1 { get; set; }
        public string ValidAnswer2{get;set;}

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
