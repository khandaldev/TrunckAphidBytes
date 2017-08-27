using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class PurchaseHistoryModel
    {
        public string ItemName { get; set; }
        public int PurchaseNumber { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; }
        public string TrackingNumber { get; set; }

    }
}
