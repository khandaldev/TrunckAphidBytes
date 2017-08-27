using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class AdvertisementModel
    {
        public Guid AdID { get; set; }
        public Guid UserID { get; set; }
        public string Title { get; set; }
        public string AdInformation { get; set; }
        public DateTime? AdCycleFromDate { get; set; }
        public DateTime? AdCycleToDate { get; set; }
        public int? AdTypeID { get; set; }
        public string AdVideo { get; set; }
        public string AdHyperLinkUrl { get; set; }
        public string PriceToDisplay { get; set; }
        public int? CreditsID { get; set; }
        public Guid SurveyID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool IsDelete { get; set; }
        public string CompanyLogo { get; set; }
        public string AdPicture { get; set; }
        public string TrackingNumber { get; set; }
        public bool? IsActive { get; set; }
        public string Adtypename { get; set; }
        public string surveyid{ get; set; }
        public string Surveyquestion { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        

    }
}
