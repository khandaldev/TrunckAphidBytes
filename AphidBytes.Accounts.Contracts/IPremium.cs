using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts
{
    public interface IPremium
    {
        PremiumAccountViewModel GetPremiumAccountInfo(Guid UserID);
        string GetCategory(string trackNo);
        bool updatpremiumaccount(Guid usid);
        string FetchEmailRecord(Guid userid, string rec);
     
        bool UpdatePremiumAccountInfo(PremiumAccountViewModel Premiummodel);
        bool DeletePremiumImage(Guid userID,string name);
        bool DeletePremiumAudio(Guid userID, string name);
        bool InsertPremiumBiteMusicSingle(PremiumGenerateCloneModel model,InterruptedFileModel intModel,CreateLinkPostModel post,AllGenerateCloneModel Alldata);
        bool InsertPremiumbyteMusicAlbum(PremiumGenerateCloneModel model);
        List<BindDropDown> BindDropAudio(Guid UserId);
        bool InsertPremiumByteYourFiles(PremiumGenerateCloneModel model);
        bool InsertByteYourArtAndPhoto(PremiumGenerateCloneModel model,InterruptedFileModel intModel,CreateLinkPostModel post,AllGenerateCloneModel Alldata);
        bool InsertByteYourVideo(PremiumGenerateCloneModel model,InterruptedFileModel inrmodel,CreateLinkPostModel post,AllGenerateCloneModel Alldata);
        List<BindDropDown> BindDropImage(Guid UserID);
        List<BindDropDown> GetVideoInterruptionImage(Guid UserID);
        string GetPremiumWebsite(Guid userID);
        List<CreateLinkPostModel> GetPostData(Guid userId);
        List<CreateLinkPostModel> GetSearchRecord(Guid userID, string Title, string Category);
        List<CreateLinkPostModel> AtoZ(Guid userID, string Title, string order);
        PremiumGenerateCloneModel EditClone(string trackid);
        List<Bytetracker> GetPostData1(Guid userId);
        List<Bytetracker> expand(Guid userID, string Trackingnumber);
        bool ByteYourFile(PremiumGenerateCloneModel model, InterruptedFileModel intf, CreateLinkPostModel post,AllGenerateCloneModel Alldata);
        ByteArray GetByteArray(string trackNo);
        bool UpdateClone(PremiumGenerateCloneModel model, InterruptedFileModel mm, CreateLinkPostModel post);
        bool InsertPremiumByteyourEbook(PremiumGenerateCloneModel model, InterruptedFileModel ob1, CreateLinkPostModel ob,AllGenerateCloneModel Alldata);
        DataPlanDetail DataPlanDetailMethod(Guid UserId);
        List<string> GetPlaylistNames(Guid userID, string TrackingID);
        string RetPremiumToolFileContent(Guid user, string filename);
        List<string> RetPremiumToolFile(Guid user);
        List<AllTools> RetPremiumToolsInfo();
        List<AllTools> RetUserTools(Guid user);
        List<SocialNetworkModel> SocialNetworkCat(Guid id);
        bool InsertPremiumTools(AllTools model, UserTool usermodel);
        //List<PremiumTools> RetPremiumTools(Guid userid);
        bool InsertPremiumToolFile(AllTools model, UserTool usermodel, Filecontent filemodel);
        bool Deletefilecontent(Filecontent filemodel, UserTool usermodel);
        List<PlaylistModel> GetSongList(Guid UserID, string PlaylistName);
        bool DelSongFromPlay(string PlaylistName, string TrackingID);
        bool AddSongToPlaylist(string PlaylistName, string TrackingID, Guid UserID);
        bool UpdatPlaylist(string PlaylistName, string TrackingID, Guid UserID);
        AllGenerateCloneModel Get_A_Record_via_trackID(string TrackingNumber);
        List<PremiumGenerateCloneModel> fileprivew(string Trackingnumber);
        AdvertisementModel Fetch_Ad_Video_Data(string ad_type_id);
        bool deleteAccount(Guid UserId);
        bool InsertChannelBiblography(Guid UserId, string data, string userimage,string UserName);
        ChannelModel ShowChannelData(Guid userId);
        bool UnsubscribeChannel(Guid UserID, Guid ChannelID);
        ChannelModel LoginUserSubscription(ChannelModel model);
        bool AddToChannel(string trackno, Guid userid);
        PostingDataModel GetUploadfiles(string track);
        bool UpdatePdf(PremiumGenerateCloneModel model,AllGenerateCloneModel allmodel,InterruptedFileModel intModel);
       List<AllGenerateCloneModel> GetUploadfile(string track);
       int totalplaylist(Guid userID);

       bool GetSubscribeUsers(string Composer,Guid user,string title,string cat,string channel,string Path,string TrackingNumber,int Credits);
       string GetChannelInfo(Guid user);
      
       bool AddSongtoFav(string TrackingID, Guid UserID);
       List<favourites> GetFavourites(Guid userID);
       bool DelfromFav(string TrackingID, Guid UserID);
       List<Bytetracker> Getpostingdata(Guid userID, string Trackingnumber);
       List<PlaylistModel> GetSongListmusic(Guid UserID, string PlaylistName);

        bool VerifyEmailAccount(Guid usid);
    }
}
