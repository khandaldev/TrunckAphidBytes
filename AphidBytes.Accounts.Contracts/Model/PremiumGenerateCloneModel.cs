using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class PremiumGenerateCloneModel
    {
       public string TrackingNumber { get; set; }
        public System.Guid? UserID { get; set; }
        public System.Guid? CloneID { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public HttpPostedFileBase Audio { get; set; }
        public string UploadAudioPath { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public byte[] UploadImage { get; set; }
        public string Composer { get; set; }
        public string Producer { get; set; }
        public string Publisher { get; set; }
        public string SelectedIntFile { get; set; }
        public string InterruptionStyle { get; set; }
        public string AvailableDownload { get; set; }
        public string ExplicitContent { get; set; }
        public string Captcha { get; set; }
        public string Type { get; set; }
        public bool Isvalid { get; set; }
        public string PdfFilePath { get; set; }
        public HttpPostedFileBase Pdf { get; set; }
        public HttpPostedFileBase Video { get; set; }
        public HttpPostedFileBase Rar { get; set; }
        public string VideoFile { get; set; }
        public string PagePercentage { get; set; }
        public string RarFilePath { get; set; }
        public HttpPostedFileBase MatrixImage { get; set; }
        public string MatrixImageBytePath { get; set; }
        public string CreatorName { get; set; }
        public string UploadImagePath { get; set; }
        public string Interruptedfile { get; set; }
        public string NewCount { get; set; }
        public string UploadFilePDFPath { get; set; }
        public string shortcatpath { get; set; }
        public string shortimagepath { get; set; }
        public string FileLength { get; set; }
        public int? CatID { get; set; }
        public string imagepath { get; set; }
       

    }
}
