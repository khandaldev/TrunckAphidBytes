using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class PostingDataModel
    {
        public string Title { get; set; }
        public string Channel { get; set; }
        public int? NoOfChannel { get; set; }
        public int? Views { get; set; }
        public int? Downloads { get; set; }
        public string FileSize { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime? Date { get; set; }
        public string DateShow { get; set; }
        public string Category { get; set; }
        public Guid UserID { get; set; }
        public byte[] VideoBytes { get; set; }
        public string InterruptedFilePath { get; set; }
        public string VideoPath { get; set; }
        public string Path { get; set; }
        public string Original { get; set; }
        public string MatrixImagePath { get; set; }
        public string ComposerName { get; set; }
        public int NewCount { get; set; }
        public int Credits { get; set; }
        
    }
}
