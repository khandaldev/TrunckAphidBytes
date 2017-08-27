using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidTise.Entity;
using AphidTise.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Processor
{
    public class Byter : IByter
    {
        RepositoryByter repository = new RepositoryByter();

        public ByterAccountViewModel GetByterAccountInfo(Guid userID)
        {
            sp_GetByterAccountInfo_Result byterObjectData = repository.GetByterAccountInfo(userID);
            ByterAccountViewModel byterData = new ByterAccountViewModel();
            if (byterObjectData != null)
            {
                byterData.AddressLine1 = byterObjectData.AddressLine1;
                byterData.AddressLine2 = byterObjectData.AddressLine2;
                byterData.Answer1 = byterObjectData.Answer1;
                byterData.Answer2 = byterObjectData.Answer2;
                byterData.City = byterObjectData.City;
                byterData.DOB = byterObjectData.DOB;
                byterData.EmailAddress = byterObjectData.EmailAddress;
                byterData.FirstName = byterObjectData.FirstName;
                byterData.LastName = byterObjectData.LastName;
                byterData.Phone = byterObjectData.Phone;
                byterData.PostalCode = byterObjectData.PostalCode;
                byterData.Region = byterObjectData.Region;
                byterData.SecurityQuestion1 = byterObjectData.SecurityQuestion1;
                byterData.SecurityQuestion2 = byterObjectData.SecurityQuestion2;
                byterData.UserName = byterObjectData.UserName;
                byterData.AccountTypeID = byterObjectData.AccountTypeID;
                byterData.AddressID = byterObjectData.AddressID;
                byterData.SecurityQuestionID = byterObjectData.SecurityQuestionID;
                byterData.ProfilePictureServerId = byterObjectData.PictureServerId ?? 0;
                byterData.ProfilePicturePath = byterObjectData.PicturePath;
                byterData.SocialNetworkSource = byterObjectData.SocialNetworSource;
                byterData.isActive = byterObjectData.IsActive ?? false;
                byterData.ByterUserID = userID;
            }
            return byterData;
        }
        public bool UpdateByterAccountInfo(ByterAccountViewModel bytermodel)
        {
            return repository.UpdateByterAccountInfo(bytermodel);
        }

        public List<string> GetPlaylistNames(Guid userID, string TrackingID)
        {
            return repository.GetPlaylistNames(userID, TrackingID);
        }
        public List<PlaylistModel> GetSongList(Guid UserID, string PlaylistName)
        {
            return repository.GetSongList(UserID, PlaylistName);
        }
        public bool DelSongFromPlay(string PlaylistName, string TrackingID)
        {
            return repository.DelSongFromPlay(PlaylistName, TrackingID);
        }
        public bool AddSongToPlaylist(string PlaylistName, string TrackingID, Guid UserID)
        {
            return repository.AddSongToPlaylist(PlaylistName, TrackingID, UserID);
        }
        public PremiumGenerateCloneModel Get_A_Record_via_trackID(string TrackingNumber)
        {
            return repository.Get_A_Record_via_trackID(TrackingNumber);
        }
        public bool deleteAccount(Guid USerId)
        {
            return repository.deleteAccount(USerId);
        }
        public List<ChannelModel> BindUserChannel(Guid userid)
        {
            return repository.BindUserChannel(userid);
        }
        public List<GetPostData> GetPostData(Guid Userid)
        {
            return repository.GetPostData(Userid);
        }
        public List<GetPostData> GetByterMessage(Guid Userid)
        {
            return repository.GetByterMessage(Userid);
        }
        public bool UpdateMessage(string track, Guid userid, string username)
        {
            return repository.UpdateMessage(track, userid, username);
        }
        public bool UpdatePostQueue(string track)
        {
            return repository.UpdatePostQueue(track);
        }
       


        public List<GetPostData> GetLinkToPostMData(Guid userid)
        {
            return repository.GetLinkToPostMData(userid);
        }
        public List<ShowSelectedNetwork> Network(Guid UserId, string selected, string type)
        {
            return repository.Network(UserId, selected,type);
        }

        public bool GetLinkPostRecord(Guid user,string track)
        {
            return repository.GetLinkPostRecord(user,track);
        }


        public int GetByterCount(Guid Userid)
        {
            return repository.GetByterCount(Userid);
        }

        public List<PremiumGenerateCloneModel> fileprivew(String trackingNumber)
        {
            return repository.Fileprivew(trackingNumber);
        }

        public AllGenerateCloneModel GetUploadfile(string track, Guid userid)
        {
            return repository.GetUploadfile(track, userid);
        }

        public List<GetPostData> GetPostedDataResult(Guid userid)
        {
            return repository.GetPostedDataResult(userid);
        }
        public List<GetPostData> GetPostData1(Guid user, string Text, string Class)
        {
            return repository.GetPostData1(user, Text, Class);
        }
        public List<GetPostData> GetSortOrder(Guid user, string sort, string Class)
        {
            return repository.GetSortOrder(user, sort, Class);
        }

        public bool CheckMsgStatus(Guid userid, string track)
        {
            return repository.CheckMsgStatus(userid, track);
        }
        public int? GetTotalCredits(Guid user)
        {
            return repository.GetTotalCredits(user);
        }

        public bool ReduceMsgCount(Guid userid)
        {
           return repository.ReduceMsgCount(userid);
        }

        public string fetch_cat(string Trackingnumber)
        {
            return repository.fetch_cat(Trackingnumber);
        }
        public string GetMessageCount(Guid userId)
        {
            return repository.GetMessageCount(userId);
        }
        public AllGenerateCloneModel Get_A_Record_via_trackID_new(string TrackingNumber)
        {
            return repository.Get_A_Record_via_trackID_new(TrackingNumber);
        }

        public bool VerifyEmailAccount(Guid usid)
        {
            return repository.VerifyMailAccount(usid);
        }
    }
}
