using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts
{
    public interface IByter
    {

        ByterAccountViewModel GetByterAccountInfo(Guid userID);
        bool ReduceMsgCount(Guid UserId);
        bool UpdateByterAccountInfo(ByterAccountViewModel basicmodel);
        List<string> GetPlaylistNames(Guid userID, string TrackingID);
        List<PlaylistModel> GetSongList(Guid UserID, string PlaylistName);
        bool DelSongFromPlay(string PlaylistName, string TrackingID);
        bool AddSongToPlaylist(string PlaylistName, string TrackingID, Guid UserID);
        PremiumGenerateCloneModel Get_A_Record_via_trackID(string TrackingNumber);
        bool deleteAccount(Guid UserId);
        List<ChannelModel> BindUserChannel(Guid userid);
        List<GetPostData> GetPostData(Guid userid);
        List<GetPostData> GetPostData1(Guid user, string Text, string Class);
        List<GetPostData> GetByterMessage(Guid userid);
        bool UpdateMessage(string track,Guid UserID, string username);
        bool UpdatePostQueue(string track);
        List<GetPostData> GetLinkToPostMData(Guid userid);
         int GetByterCount(Guid userid);
        List<ShowSelectedNetwork> Network(Guid UserId, string selected,string type);
        List<PremiumGenerateCloneModel> fileprivew(string Trackingnumber);
        AllGenerateCloneModel GetUploadfile(string track, Guid userid);
        List<GetPostData> GetPostedDataResult(Guid userID);
        List<GetPostData> GetSortOrder(Guid user, string sort, string Class);
        bool GetLinkPostRecord(Guid user,string track);
        bool CheckMsgStatus(Guid userid, string trackid);
        int? GetTotalCredits(Guid user);
        string fetch_cat(string Trackingnumber);
        string GetMessageCount(Guid userId);
       AllGenerateCloneModel Get_A_Record_via_trackID_new(string TrackingNumber);

        bool VerifyEmailAccount(Guid usid);
    }
}
