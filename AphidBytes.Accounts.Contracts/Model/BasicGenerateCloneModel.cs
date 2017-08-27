using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class BasicGenerateCloneModel
    {
        public ValidationModel Validation { get; set; } = new ValidationModel();

        public System.Guid? UserID { get; set; }
        public System.Guid CloneID { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public HttpPostedFileBase Audio { get; set; }
        public int AudioServerId { get; set; }
        public string AudioServerPath { get; set; }
        public long AudioLength { get; set; }

        public HttpPostedFileBase Audio2 { get; set; }
        public int Audio2ServerId { get; set; }
        public string Audio2ServerPath { get; set; }
        public long Audio2Length { get; set; }

        public string UploadAudioPath { get; set; }
        public string UploadAudio2Path { get; set; }

        public HttpPostedFileBase Image { get; set; }
        public int ImageServerId { get; set; }
        public string ImageServerPath { get; set; }
        public long ImageLength { get; set; }

        public string UploadImagePath { get; set; }
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
        public int PdfServerId { get; set; }
        public string PdfServerPath { get; set; }
        public long PdfLength { get; set; }

        public HttpPostedFileBase Video { get; set; }
        public int VideoServerId { get; set; }
        public string VideoServerPath { get; set; }
        public long VideoLength { get; set; }

        public HttpPostedFileBase Rar { get; set; }
        public int RarServerId { get; set; }
        public string RarServerPath { get; set; }
        public long RarLength { get; set; }

        public string VideoFile { get; set; }
        public string PagePercentage { get; set; }
        public byte[] RarFile { get; set; }
        public HttpPostedFileBase MatrixImage { get; set; }
        public int MatrixImageServerId { get; set; }
        public string MatrixImageServerPath { get; set; }
        public long MatrixImageLength { get; set; }

        public string MatrixImageBytePath { get; set; }
        public string CreatorName { get; set; }
        public HttpPostedFileBase WatermarkMatrixImage { get; set; }
        public int WatermarkMatrixImageServerId { get; set; }
        public string WatermarkMatrixImageServerPath { get; set; }
        public long WatermarkMatrixImageLength { get; set; }

        public string WatermarkMatrixImagePath { get; set; }
        public string WatermarkMatrixImageText { get; set; }
        public string VideoCategory { get; set; }
        public string SongName { get; set; }
        public string SongName2 { get; set; }
        public string InterruptedAudioPath { get; set; }
        public string TrackingNumber { get; set; }
        public string Interruptedfile { get; set; }
        public string VideoPath { get; set; }
        public string RarPath { get; set; }
        public string NewCount { get; set; }
        public string  UserName { get; set; }
        public string Password { get; set; }
        public bool?  UserStatus { get; set; }
        public string GenCloneType { get; set; }
        public string UploadFilePDFPath { get; set; }
        public string TotalLength { get; set; }
        public string shortcatpath { get; set; }
        public string shortimagepath { get; set; }
        public string RarFilePath { get; set; }
        public byte[] UploadImage { get; set; }
        public int CatId { get; set; }
        public DateTime Modified_Time { get; set; }
        public string imagepath { get; set; }

    }

    
}
