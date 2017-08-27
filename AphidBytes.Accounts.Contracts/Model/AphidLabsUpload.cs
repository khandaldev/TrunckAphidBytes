using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class AphidLabsUpload
    {
        public System.Guid? UserID { get; set; }
        public System.Guid CloneID { get; set; }
        public string Titleofupload { get; set; }
        public string AvailableDownload { get; set; }
        public string ExplicitContent { get; set; }
        public string VideoDescription { get; set; }
        public HttpPostedFileBase Video { get; set; }
        public string VideoPath { get; set; }
        public string InterruptionStyle { get; set; }
        public string PasswordForDownload { get; set; }
        public string SoftwareDescription { get; set; }
        public HttpPostedFileBase Software { get; set; }
        public string SoftwarePath{get;set;}
        public string MatriximagePath { get; set; }
        public HttpPostedFileBase Matriximage { get; set; }
        public string SelectedIntFile { get; set; }
        public string TrackingNumber { get; set; }
        public string MatrixImageBytePath { get; set; }
        public string NewCount { get; set; }



    }
}
