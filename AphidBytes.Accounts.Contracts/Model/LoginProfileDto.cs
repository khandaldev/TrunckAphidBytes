//using AphidTise.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AphidBytes.Accounts.Contracts.Model
{
    [DataContract]
    public class LoginProfileDto
    {
        [DataMember(Order = 1)]
        public string Username { get; set; }

        [DataMember(Order = 2)]
        public string Token { get; set; }

        [DataMember(Order = 3)]
        public string UserId { get; set; }

        [DataMember(Order = 4)]
        public string EmailAddress { get; set; }

        [DataMember(Order = 5)]
        public int? AccountTypeId { get; set; }

        [DataMember(Order = 6)]
        public bool? Status { get; set; }

        [DataMember(Order = 7)]
        public DateTime ExpirationDate { get; set; }

        [DataMember(Order = 8)]
        public string ProfilePicture { get; set; }

        [DataMember(Order = 9)]
        public Dictionary<string, bool> SocialStatus { get; set; } = new Dictionary<string, bool>();
    }
}
