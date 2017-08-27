using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class PlaylistModel
    {
        public string FileName { get; set; }
        public string TrackingID { get; set; }
        public string PlaylistName { get; set; }
        public string Composer { get; set; }
        public Guid UserID { get; set; }
        public int CatId { get; set; }
    }
}
