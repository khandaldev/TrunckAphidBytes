using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class UserConfirmationForgrtPasswordChange
    {
        public string NewPassword { get; set; }
        public string VerifyPassword { get; set; }
    }
}
