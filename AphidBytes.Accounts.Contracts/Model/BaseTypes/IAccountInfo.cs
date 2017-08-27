using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model.BaseTypes
{
    public interface IAccountInfo
    {
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string EmailAddress { get; set; }
        string DOB { get; set; }
        string Phone { get; set; }
        HttpPostedFileBase ProfilePicture { get; set; }
        int ProfilePictureServerId { get; set; }
        string ProfilePicturePath { get; set; }

        Nullable<int> AccountTypeID { get; set; }
        bool IsUserNameReadonly { get; set; }
        bool IsPasswordReadonly { get; set; }

        string SocialNetworkSource { get; set; }
    }
}
