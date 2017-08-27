using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class favourites
    {
        public Nullable<System.Guid> UserID { get; set; }
        public string FileName { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Nullable<int> IsActive { get; set; }
    }
}
