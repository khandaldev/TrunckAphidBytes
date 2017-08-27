using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class UserSubscribeModel
    {
        public int SubscriptionId { get; set; }
        public Guid SubscribeUserId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WebsiteUrl { get; set; }
        public string ProfilePic { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool isOwn { get; set; }
        public bool isSubscribed { get; set; }
    }
}