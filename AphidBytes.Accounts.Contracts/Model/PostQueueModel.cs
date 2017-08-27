using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class PostQueueModel
    {
      public string Composer { get; set; }
      public string Title { get; set; }
      public int Credit { get; set; }
      public string Category { get; set; }
      public string TrackingId { get; set; }
      public string Path { get; set; }
      public Guid User { get; set; }

    }
}
    
