using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class CreditSummaryModel
    {
       public string Title { get; set; }
       public string Channel { get; set; }
       public string Category { get; set; }
       public Guid Aphid { get; set; }
       public string TrackingID { get; set; }
       public string Composer { get; set;}
       public int? Credit { get; set; }
      
        
    }
}
