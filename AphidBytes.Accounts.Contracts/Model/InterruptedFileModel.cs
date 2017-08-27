using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
  public  class InterruptedFileModel
    {
        public Guid UserId { get; set; }
        public Guid? CloneId { get; set; }
        public string InterruptedFilePath { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string FileName { get; set; }
        public string FileName2 { get; set; }
        public string VideoPath { get; set; }
        public string TrackNumber { get; set; }
        public int CatId { get; set; }
        public string MatrixImagePath { get; set; }
    }
}
