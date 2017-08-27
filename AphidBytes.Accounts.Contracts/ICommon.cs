using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts
{
    public interface ICommon
    {
        IEnumerable<AudioFileModel> GetAudioFiles(Guid userid, int ID);
        bool UpdateDataMemory(Guid UserId, long length);
        bool CheckSpace(Guid UserId, long length);
        bool ChangeDataPlan(string plan, Guid userid);
        string GetMessageCount(Guid UserId);
        string GetNewCount(Guid UserId);
        bool Deleteitem(Guid user, string Track);        
        bool CheckEditStatus(Guid id, long length, string oldlength);
        bool InsertMessageDetails(MessageModel messagemodel);
        List<MessageModel> GetMessageData(string emailid);
        string GetReadMessage(int emailid);
        bool MessageDeleteCommon(int MessageID);
        bool UpdatePassword(Guid userid, string passwordEnc);
        bool UpdateStripeCard(Guid userid, string stripeToken);
        List<LinkShareHistory> GetDataForHistory(Guid userid);
    }
}
