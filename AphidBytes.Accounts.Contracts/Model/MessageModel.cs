using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class MessageModel
    {
        public int NewCount { get; set; }
        public int TotalCredit { get; set; }
        public int SubsCount { get; set; }
        public string Plan { get; set; }
        public string Free { get; set; }
        public string sender_Email { get; set; }
        public string sender_username { get; set; }
        public string receiver_Email { get; set; }
        public string receiver_username { get; set; }
        public string message_subject{get;set;}
        public string message_body{get;set;}
        public bool IsRead { get; set; }
        public List<MessageModel> listMessageModel { get; set; }
        public List<MessageModel> Outboxlist { get; set; }
        public int id { get; set; }

        public MessageModel()
        {
            listMessageModel = new List<MessageModel>();
            Outboxlist = new List<MessageModel>();
        }
       
    }
}
