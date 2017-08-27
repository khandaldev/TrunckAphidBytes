using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts
{
    public interface IBasic
    {

         BasicAccountViewModel GetBasicAccountInfo(Guid userID);
         bool updatbasicaccount(Guid usid);
         string FetchEmailRecord(Guid userid, string rec);
         bool UpdateBasicAccountInfo(BasicAccountViewModel basicmodel);
        List<BindDropDown> BindDropImage(Guid UserId);
        bool DeleteBasicImage(Guid userID);
        bool DeleteAudioBasic(Guid userID);
        List<BindDropDown> BindDropVideo(Guid UserID);
        bool InsertBasicByteYourVideo(BasicGenerateCloneModel model,InterruptedFileModel mm,CreateLinkPostModel post,AllGenerateCloneModel Alldata);
        bool InsertAlbum(BasicGenerateCloneModel model);
        bool InsertSingleMusic(BasicGenerateCloneModel model,InterruptedFileModel intModel, CreateLinkPostModel post,AllGenerateCloneModel Alldata);
        bool InsertPhotoArt(BasicGenerateCloneModel model,InterruptedFileModel intmodel,CreateLinkPostModel post,AllGenerateCloneModel Alldata);
        bool InsertBasicByteYourEbook(BasicGenerateCloneModel model,InterruptedFileModel ob1,CreateLinkPostModel ob,AllGenerateCloneModel Alldata);
        List<BindDropDown> GetAudioForInterruption(Guid UserId);
        List<BindDropDown> GetVideoInterruptionImage(Guid UserID);
        string GetWebsite(Guid USerID);
        List<CreateLinkPostModel> GetPostData(Guid userID);
        List<CreateLinkPostModel> GetSearchRecord(Guid userID, string Title, string Category);
        List<CreateLinkPostModel> AtoZ(Guid userID,string Title,string order);
        List<Bytetracker> GetPostData1(Guid userID);
        BasicGenerateCloneModel EditClone(string trackNo);
        bool UpdateClone(BasicGenerateCloneModel model, InterruptedFileModel mm, AllGenerateCloneModel post);
        List<Bytetracker> expand(Guid userID, string Trackingnumber);
        string GetCategory(string trackNo);
        ByteArray GetByteArray(string trackNo);
        bool ByteYourFile(BasicGenerateCloneModel model, InterruptedFileModel mm,CreateLinkPostModel post,AllGenerateCloneModel Alldata);
        //bool Deletepost(Guid userID, string trackingnumber);
        List<AllGenerateCloneModel> GetUploadfile(string track);
        List<SocialNetworkModel> SocialNetworkCat(Guid id);
        List<string> GetPlaylistNames(Guid userID, string TrackingID);
        List<PlaylistModel> GetSongList(Guid UserID, string PlaylistName);
        bool DelSongFromPlay(string PlaylistName, string TrackingID);
        bool AddSongToPlaylist(string PlaylistName, string TrackingID, Guid UserID);
       // bool AddVideoToPlaylist(string PlaylistName, string TrackingID, Guid UserID);
        List<BasicGenerateCloneModel> fileprivew(string Trackingnumber);
        DataPlanDetail DataPlanDetailMethod(Guid UserId);
        AllGenerateCloneModel Get_A_Record_via_trackID(string TrackingNumber);
        AdvertisementModel Fetch_Ad_Video_Data(string ad_type_id);
        string GetImageName(Guid UserID);
        List<string> GetBasicImages(Guid UserId);
        string GetImagePath(Guid UserId, string Name);
        string GetAudioPath(Guid UserId, string Name);
        bool UpgradeAccount(Guid UserId);
        bool deleteAccount(Guid UserId);
        int totalplaylist(Guid userID);
        bool UpdatPlaylistBasic(string PlaylistName, string TrackingID, Guid UserID);
        bool AddSongtoFav(string TrackingID, Guid UserID);
        List<favourites> GetFavourites(Guid userID);
        bool DelfromFav(string TrackingID, Guid UserID);
        List<Bytetracker> Getpostingdata(Guid userID, string Trackingnumber);
        List<PlaylistModel> GetSongListmusic(Guid UserID, string PlaylistName);
        //bool InsertAlbum(BasicGenerateCloneModel basicGenerateCloneModel, InterruptedFileModel interruptedFileModel, CreateLinkPostModel createLinkPostModel, AllGenerateCloneModel allGenerateCloneModel);
    }
}
