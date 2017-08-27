using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class AphidTiseGenerateAds
    {
        public System.Guid AdID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public HttpPostedFileBase CompanyLogo { get; set; }
        public string CompanyLogoByte { get; set; }
        public string Title { get; set; }
        public string AdInformation { get; set; }
        public Nullable<System.DateTime> AdCycleFromDate { get; set; }
        public Nullable<System.DateTime> AdCycleToDate { get; set; }
        public Nullable<int> AdTypeID { get; set; }
        public string AdPictureByte { get; set; }
        public HttpPostedFileBase AdPicture { get; set; }
        public string AdVideoByte { get; set; }
        public HttpPostedFileBase AdVideo { get; set; }
        public string AdHyperLinkUrl { get; set; }
        public Nullable<decimal> PriceToDisplay { get; set; }
        public Nullable<int> CreditsID { get; set; }
       
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public System.Guid SurveyID { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string Option6 { get; set; }
        public string Option7 { get; set; }
        public string Option8 { get; set; }
        public string TrackingNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsValid { get; set; }
    }
}
