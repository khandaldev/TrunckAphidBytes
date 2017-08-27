using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class LinkShareHistory
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Channel { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Track { get; set; }
        public string DateShow { get; set; }
        
    }
}
