using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class SponsoredModel
    {
        public int ID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public Nullable<System.Guid> CloneId { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public string AudioFilePath { get; set; }
        public string UploadFilePath { get; set; }
        public string MatrixFilePath { get; set; }
        public string ComposerName { get; set; }
        public string Producer { get; set; }
        public string Publisher { get; set; }
        public string SelectedInteruptionFile { get; set; }
        public string InteruptionStyle { get; set; }
        public string AvailableForDownload { get; set; }
        public string ExplicitContent { get; set; }
        public string UploadImageFilePath { get; set; }
        public string UploadPDFFilePath { get; set; }
        public string PagePercentage { get; set; }
        public string Type { get; set; }
        public string PdfFilePath { get; set; }
        public string FileNames { get; set; }
        public string VideoFilePath { get; set; }
        public string WaterMarkMatrixImagePath { get; set; }
        public string WaterMarkMatrixImageText { get; set; }
        public string VideoCategory { get; set; }
        public string RARFilePath { get; set; }
        public string MatrixImagePath { get; set; }
        public string CreatotName { get; set; }
        public string TrackingNumber { get; set; }
        public Nullable<int> CatID { get; set; }
        public Nullable<int> GenCloneID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public bool IsActive { get; set; }
        public bool Isvalid { get; set; }
        public string Captcha { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public HttpPostedFileBase MatrixImage { get; set; }
        public string FileSize { get; set; }
    }
}
