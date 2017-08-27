using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class Bytetracker
    {
        public string Title { get; set; }
        public string Channel { get; set; }
        public int? NoOfclones { get; set; }
        public int? Views { get; set; }
        public int? Downloads { get; set; }
        public string FileSize { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime? Date { get; set; }
        public string DateShow { get; set; }
        public string Category { get; set; }
        public Guid UserID { get; set; }
        public bool? ChannelStatus { get; set; }
        public string poststatus { get; set; }
        public string MatrixImagePath { get; set; }

    }
}
