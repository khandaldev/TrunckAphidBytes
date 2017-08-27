using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class LoginUser
    {
        [Required(ErrorMessage = "*")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage="*")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
        public Guid UserID { get; set; }
        public Nullable<int> AccountTypeID { get; set; }
        public bool? Status { get; set; }

        public ValidationModel Validation { get; set; } = new ValidationModel();
    }
}
