using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using AphidBytes.Core.PaymentServices;
using AphidBytes.Accounts.Contracts.Model.BaseTypes;
using System.Web.Mvc;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class GooglePlusAccountViewModel : IPaymentProvider, ISecurityQuestions, IAddressProvider, IAccountInfo
    {

        public GooglePlusAccountViewModel()
        {
            StripeConfig = new StripeConfigurationModel
            {
                ApiKey = StripeClient.StripePublishApiKey.Value
            };
        }
        public string PromoCode { get; set; }
        public System.Guid AphidTiseUserID { get; set; }
        public HttpPostedFileBase CompanyLogo { get; set; }
        public byte[] CompanyLogoByter { get; set; }
        public Guid AccountIdAphid { get; set; }
        public System.Guid ByterUserID { get; set; }
        public System.Guid BasicUserID { get; set; }
        [Required(ErrorMessage = "Please Select Date of Birth")]
        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }
        public System.Guid? AphidlabUserID { get; set; }
        [Required(ErrorMessage = "Please Enter Username")]
        [RegularExpression(@"^[A-z0-9_\.\-]+$", ErrorMessage = "Enter valid name")]
        [Remote("IsUserNameAvailable", "Validations")]
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string DeveloperName { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Password)]
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
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter valid Email Format")]
        public string EmailAddress { get; set; }
        public string UserEmail { get; set; }
        public string WebSiteUrl { get; set; }
        [Required(ErrorMessage = "Please Enter Phone No")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\+?[\d+]{3,10}$",
       ErrorMessage = "Please Enter Correct Phone Number")]
        public string Phone { get; set; }

        public string Ppicture { get; set; }
        public string Byterimg { get; set; }
        public string Basicimg { get; set; }
        public string AphiTiserimg { get; set; }
        public string AphidLabimg { get; set; }
        [Required(ErrorMessage = "Please Select profile picture")]
        public HttpPostedFileBase ProfilePicture { get; set; }
        public byte[] ProfilePictureInBytes { get; set; }
        public string RecoveryEmail { get; set; }
        public Nullable<System.Guid> BankAccountID { get; set; }
        public Nullable<System.Guid> AddressID { get; set; }
        public Nullable<System.Guid> SecurityQuestionID { get; set; }
        public Nullable<int> AccountTypeID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Guid AccountIdBasic { get; set; }
        public string AudioFIle { get; set; }
        public Guid AccountID { get; set; }
        public string Informations { get; set; }
        public string Website { get; set; }
        public string ProductService { get; set; }
        //Bank Details               
        public Int64? CardNumber { get; set; }
        public Int16? ExpiryMonth { get; set; }
        public Int32? ExpiryYear { get; set; }


        public string CSV { get; set; }

        public string NameOnCard { get; set; }


        //Billing Address Details

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        //Sequirity Questions Details

        public string SecurityQuestion1 { get; set; }

        public string SecurityQuestion2 { get; set; }
        public string Answer1 { get; set; }

        public string Answer2 { get; set; }
        public string AccountType { get; set; }
        public string SelectedImage { get; set; }
        public string CustomFileName { get; set; }
        public string ImageFileName { get; set; }
        public string AudioFileName { get; set; }
        public AudioFileModel BasicAudioFile { get; set; }
        public string Flag { get; set; }
        public string SelectedAudio { get; set; }
        public bool CustomAudioSelected { get; set; }
        public bool DefaultSelected { get; set; }
        public bool CustomSelected { get; set; }
        public bool SessionCheck { get; set; }
        public bool SessionCheckAudio { get; set; }
        public ImageFileModel BasicImageFile { get; set; }
        public string SelectedAudioForInt { get; set; }
        public string SelectedAudioPathForInt { get; set; }
        public string CustomAudioSelectedNew { get; set; }
        public string ShowSelectedAudio { get; set; }
        public string SelectedImagePathInt { get; set; }
        public string SelectedImageNameInt { get; set; }
        public string ShowSelectedImage { get; set; }
        public string NewCount { get; set; }

        //  public string ProfilePicturepath { get; set; }

        public StripeConfigurationModel StripeConfig
        {
            get;

            set;
        }

        public int ProfilePictureServerId
        {
            get; set;
        }



        public bool IsUserNameReadonly
        {
            get; set;
        }

        public bool IsPasswordReadonly
        {
            get; set;
        }

        public string SocialNetworkSource
        {
            get; set;
        }

        public string ProfilePicturePath
        {
            get; set;
        }

        public string StripeToken { get; set; }
        public void LoadStripeAccountInfo()
        {
            StripeConfig = new StripeConfigurationModel
            {
                ApiKey = StripeClient.StripePublishApiKey.Value
            };
        }
        //endof
        public ValidationModel Validation { get; set; } = new ValidationModel();
    }
}
