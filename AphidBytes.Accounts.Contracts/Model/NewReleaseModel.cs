using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class NewReleaseModel
    {
        public string Msg { get; set; }
        public string Path { get; set; }
        public int NDBID { get; set; }
    }
}
