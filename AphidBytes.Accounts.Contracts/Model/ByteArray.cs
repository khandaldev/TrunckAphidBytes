using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class ByteArray
    {
        public string Image { get; set; }
        public string Audio { get; set; }
        public string Intrepputed { get; set; }
        public string VideoPath { get; set; }
        public string FileSize { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
