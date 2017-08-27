using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public  class FeedBackModel
    {
        public Guid Userid { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public string Subject { get; set; }
    }
}
