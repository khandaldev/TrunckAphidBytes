using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
     public class searchmodel
    {
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public string MatrixFilePath { get; set; }
        public string MatrixImagePath { get; set; }
        public string UploadImageFilePath { get; set; }
        public string TrackingNumber { get; set; }
        public string UploadedTo { get; set; }
        public string InterupptedFilepath { get; set; }
        public string category { get; set; }
        public int AccountTypeId { get; set; }
        public Guid? PremiumUserId { get; set; }
    }
}
