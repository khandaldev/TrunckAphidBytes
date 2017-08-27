using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class HistorySubscription
    {
        public int ChannelId { get; set; }
        public DateTime SubscribeDate { get; set; }
        public int ByterUserId { get; set; }
        public int PremiumUserId { get; set; }
    }
}
