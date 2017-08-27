using System;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AphidBytes.Core.PaymentServices;
using AphidBytes.Accounts.Contracts.Model.BaseTypes;
using System.Web.Mvc;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class AphidLabAccountModel : IPaymentProvider, ISecurityQuestions, IAddressProvider, IAccountInfo
    {
        public AphidLabAccountModel()
        {
            StripeConfig = new StripeConfigurationModel
            {
                ApiKey = StripeClient.StripePublishApiKey.Value
            };
        }

        public bool isActive { get; set; }
        public string PromoCode { get; set; }
        public System.Guid? AphidlabUserID { get; set; }
        public string Website { get; set; }
        public string Ppicture { get; set; }
        public byte[] ProfilePictureInBytes { get; set; }
        public Nullable<System.Guid> BankAccountID { get; set; }
        public Nullable<System.Guid> AddressID { get; set; }
        public Nullable<System.Guid> SecurityQuestionID { get; set; }
        public Nullable<int> AccountTypeID { get; set; }
        public string RecoveryEmail { get; set; }
        public string AccountType { get; set; }
        public string StripeToken { get; set; }

        public ValidationModel Validation { get; set; } = new ValidationModel();


        #region REMOVE THESE
        public Int64? CardNumber { get; set; }
        public Int16? ExpiryMonth { get; set; }
        public Int32? ExpiryYear { get; set; }
        public string CSV { get; set; }
        #endregion

        #region IAccountInfo      

        [Required(ErrorMessage = "Please Enter Username")]
        [RegularExpression(@"^[A-z0-9_\.\-]+$", ErrorMessage = "Enter valid name")]
        [Remote("IsUserNameAvailable", "Validations")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter password")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and Confirm Password  do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        [RegularExpression(@"^[A-z]+$", ErrorMessage = "Enter valid FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [RegularExpression(@"^[A-z]+$", ErrorMessage = "Enter valid LastName")]
        public string LastName { get; set; }

        [Remote("IsEmailAlreadyRegister", "Validations")]
        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please Enter valid Email Format")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please Select Date of Birth")]
        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        [Required(ErrorMessage = "Please Enter Phone No")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\+?[\d+]{3,10}$", ErrorMessage = "Please Enter Correct Phone Number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please Select profile picture")]
        public HttpPostedFileBase ProfilePicture { get; set; }

        public int ProfilePictureServerId {get; set; }

        public string ProfilePicturePath { get; set; }

        public bool IsUserNameReadonly { get; set; }
        public bool IsPasswordReadonly { get; set; }

        public string SocialNetworkSource { get; set; }

        #endregion

        #region IAddressProvider

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }

        #endregion

        #region ISecurityQuestions

        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }

        #endregion

        #region IPaymentProvider 

        public string NameOnCard { get; set; }
        public StripeConfigurationModel StripeConfig { get; set; }
        #endregion

        
    }
}
