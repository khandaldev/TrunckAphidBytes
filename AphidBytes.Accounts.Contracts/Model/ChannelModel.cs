using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class ChannelModel
    {
        public string  UserData { get; set; }
        public string  ImagePath { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid? UserID { get; set; }
        public bool? UserStatus { get; set; }
        public Guid? ChannelId { get; set; }
        public Guid? premiumUserId { get; set; }
        public bool  SubscriptionStatus { get; set; }
        public bool UnAuthorised { get; set; }
        public string Image0 { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string Image6 { get; set; }
        public string Image7 { get; set; }
        public string Image8 { get; set; }
        public string Image9 { get; set; }
        public string TrackingNumber0 { get; set; }
        public string TrackingNumber1 { get; set; }
        public string TrackingNumber2 { get; set; }
        public string TrackingNumber3 { get; set; }
        public string TrackingNumber4 { get; set; }
        public string TrackingNumber5 { get; set; }
        public string TrackingNumber6 { get; set; }
        public string TrackingNumber7 { get; set; }
        public string TrackingNumber8 { get; set; }
        public string TrackingNumber9 { get; set; }
      
        public Guid? byterUserId { get; set; }
       
    }
}
