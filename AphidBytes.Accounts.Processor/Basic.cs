using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidTise.Entity;
using AphidTise.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AphidBytes.Accounts.Processor
{
    public class Basic : IBasic
    {
        RepositoryBasic repository = new RepositoryBasic();

        public BasicAccountViewModel GetBasicAccountInfo(Guid userID)
        {
            Common AudioFiles = new Common();
            //System.Collections.Generic.IEnumerable<AudioFileModel> audiofile = AudioFiles.GetAudioFiles(userID);
            List<AudioFileModel> Audiofile = new List<AudioFileModel>(AudioFiles.GetAudioFiles(userID, 1));
            List<ImageFileModel> Imagefile = new List<ImageFileModel>(AudioFiles.GetImageFile(userID));
            sp_GetBasicAccountInfo_Result aphidTiseObjectData = repository.GetBasicAccountInfo(userID);

            BasicAccountViewModel basicData = new BasicAccountViewModel();
            if (aphidTiseObjectData != null)
            {
                basicData.AddressLine1 = aphidTiseObjectData.AddressLine1;
                basicData.AddressLine2 = aphidTiseObjectData.AddressLine2;
                basicData.Answer1 = aphidTiseObjectData.Answer1;
                basicData.Answer2 = aphidTiseObjectData.Answer2;
                basicData.City = aphidTiseObjectData.City;
                basicData.DOB = aphidTiseObjectData.DOB;
                basicData.EmailAddress = aphidTiseObjectData.EmailAddress;
                basicData.FirstName = aphidTiseObjectData.FirstName;
                basicData.LastName = aphidTiseObjectData.LastName;
                basicData.Password = aphidTiseObjectData.Password;
                basicData.ConfirmPassword = aphidTiseObjectData.Password;
                basicData.Phone = aphidTiseObjectData.Phone;
                basicData.PostalCode = aphidTiseObjectData.PostalCode;
                basicData.Region = aphidTiseObjectData.Region;
                basicData.SecurityQuestion1 = aphidTiseObjectData.SecurityQuestion1;
                basicData.SecurityQuestion2 = aphidTiseObjectData.SecurityQuestion2;
                basicData.UserName = aphidTiseObjectData.UserName;
                basicData.RecoveryEmail = aphidTiseObjectData.RecoveryEmail;
                basicData.isActive = aphidTiseObjectData.IsActive;
                basicData.SocialNetworkSource = aphidTiseObjectData.SocialNetworSource;
                if (Audiofile.Count > 0)
                    foreach (dynamic item in Audiofile)
                    {
                        if (item.IsActive == true)
                        {
                            basicData.ShowSelectedAudio = item.AudioFileName;
                        }
                        if (item.AudioFileName != "Default")
                        {
                            basicData.CustomAudioSelectedNew = item.AudioFileName;
                        }
                    }

                if (Imagefile.Count > 0)
                {
                    foreach (dynamic item in Imagefile)
                    {
                        if (item.IsActive == true)
                        {
                            basicData.ShowSelectedImage = item.ImageFileName;
                            //  basicData.ImageFileName = item.ImageFileName;
                        }
                        if (item.ImageFileName != "Default")
                        {
                            basicData.CustomFileName = item.ImageFileName;
                        }

                    }
                }
                basicData.WebSiteUrl = aphidTiseObjectData.WebSiteUrl;
                basicData.AccountTypeID = aphidTiseObjectData.AccountTypeID;
                basicData.AddressID = aphidTiseObjectData.AddressID;
                basicData.SecurityQuestionID = aphidTiseObjectData.SecurityQuestionID;
                basicData.ProfilePictureServerId = aphidTiseObjectData.PictureServerId ?? 0;
                basicData.ProfilePicturePath = aphidTiseObjectData.PicturePath;
                basicData.BasicUserID = userID;

            }
            return basicData;
        }

        public bool updatbasicaccount(Guid usid)
        {
            return repository.updatbasicaccount(usid);
        }

        public string FetchEmailRecord(Guid userid, string rec)
        {
            return repository.FetchEmailRecord(userid, rec);
        }

        public bool UpdateBasicAccountInfo(BasicAccountViewModel basicmodel)
        {
            return repository.UpdateBasicAccountInfo(basicmodel);
        }

        public bool DeleteBasicImage(Guid userID)
        {
            return repository.DeleteBasicImage(userID);
        }

        public bool DeleteAudioBasic(Guid userID)
        {
            return repository.DeleteAudioBasic(userID);
        }

        public bool InsertSingleMusic(BasicGenerateCloneModel model, InterruptedFileModel imtmodel, CreateLinkPostModel post, AllGenerateCloneModel Alldata)
        {

            return repository.InsertBasicSingleMusic(model, imtmodel, post, Alldata);
        }

        public bool InsertPhotoArt(BasicGenerateCloneModel model, InterruptedFileModel intmodel, CreateLinkPostModel post, AllGenerateCloneModel Alldata)
        {
            return repository.InsertPhotoArt(model, intmodel, post, Alldata);
        }
        public List<BindDropDown> BindDropImage(Guid userId)
        {
            return repository.BindDropImage(userId);
        }
        public List<BindDropDown> BindDropVideo(Guid userid)
        {
            return repository.BindDropAudio(userid);
        }

        public bool InsertBasicByteYourVideo(BasicGenerateCloneModel model, InterruptedFileModel mm, CreateLinkPostModel ll, AllGenerateCloneModel Alldata)
        {
            return repository.InsertBasicByteYourVideo(model, mm, ll, Alldata);
        }
        public bool InsertAlbum(BasicGenerateCloneModel model)
        {
            return repository.InsertAlbum(model);
        }
        public List<BindDropDown> GetAudioForInterruption(Guid userID)
        {
            return repository.GetAudioForINterruption(userID);
        }

        public bool InsertBasicByteYourEbook(BasicGenerateCloneModel model, InterruptedFileModel ob1, CreateLinkPostModel ob, AllGenerateCloneModel Alldata)
        {
            return repository.InsertBasicByteYourEbook(model, ob1, ob, Alldata);

        }

        public List<BindDropDown> GetVideoInterruptionImage(Guid UserID)
        {
            return repository.BindDropImage(UserID);
        }

        public string GetWebsite(Guid Userid)
        {
            return repository.GetWebSite(Userid);
        }

        public List<CreateLinkPostModel> GetPostData(Guid userId)
        {
            return repository.getPostData(userId);
        }
        public List<CreateLinkPostModel> GetSearchRecord(Guid userId, string Title, string Category)
        {
            return repository.GetSearchRecord(userId, Title, Category);
        }
        public List<CreateLinkPostModel> AtoZ(Guid userId, string Category, string order)
        {
            return repository.AtoZ(userId, Category, order);

        }
        public List<Bytetracker> GetPostData1(Guid userId)
        {
            return repository.GetPostData1(userId);
        }
        public BasicGenerateCloneModel EditClone(string trackNo)
        {
            return repository.EditClone(trackNo);
        }

        public bool UpdateClone(BasicGenerateCloneModel model, InterruptedFileModel mm, AllGenerateCloneModel ll)
        {
            return repository.UpdateClone(model, mm, ll);
        }
        public List<Bytetracker> expand(Guid userID, string Trackingnumber)
        {
            return repository.expand(userID, Trackingnumber);
        }
        public string GetCategory(string trackNo)
        {
            return repository.GetCategory(trackNo);
        }

        public ByteArray GetByteArray(string trackNo)
        {
            return repository.GetByteArray(trackNo);
        }

        public bool ByteYourFile(BasicGenerateCloneModel model, InterruptedFileModel mm, CreateLinkPostModel ll, AllGenerateCloneModel Alldata)
        {
            return repository.ByteYourFile(model, mm, ll, Alldata);
        }
        public List<AllGenerateCloneModel> GetUploadfile(string track)
        {
            return repository.GetUploadfile(track);
        }
        public List<SocialNetworkModel> SocialNetworkCat(Guid id)
        {
            return repository.Reterive(id);
        }

        public List<string> GetPlaylistNames(Guid userID, string TrackingID)
        {
            return repository.GetPlaylistNames(userID, TrackingID);
        }
        public List<PlaylistModel> GetSongList(Guid UserID, string PlaylistName)
        {
            return repository.GetSongList(UserID, PlaylistName);
        }
        public List<BasicGenerateCloneModel> fileprivew(String trackingNumber)
        {
            return repository.Fileprivew(trackingNumber);
        }

        public bool DelSongFromPlay(string PlaylistName, string TrackingID)
        {
            return repository.DelSongFromPlay(PlaylistName, TrackingID);
        }
        public bool AddSongToPlaylist(string PlaylistName, string TrackingID, Guid UserID)
        {
            return repository.AddSongToPlaylist(PlaylistName, TrackingID, UserID);
        }
        //public bool AddVideoToPlaylist(string PlaylistName, string TrackingID, Guid UserID)
        //{
        //    return repository.AddVideoToPlaylist(PlaylistName, TrackingID, UserID);
        //}
        public DataPlanDetail DataPlanDetailMethod(Guid UserId)
        {
            return repository.DataPlanDetail(UserId);
        }

        public AllGenerateCloneModel Get_A_Record_via_trackID(string TrackingNumber)
        {
            return repository.Get_A_Record_via_trackID(TrackingNumber);
        }
        public AdvertisementModel Fetch_Ad_Video_Data(string ad_type_id)
        {
            return repository.Fetch_Ad_Video_Data(ad_type_id);
        }

        public string GetImageName(Guid UserID)
        {
            return repository.GetImageName(UserID);
        }

        public List<string> GetBasicImages(Guid userid)
        {
            return repository.GetBasicImages(userid);
        }

        public string GetImagePath(Guid userID, string name)
        {
            return repository.GetImagePath(userID, name);
        }

        public string GetAudioPath(Guid id, string name)
        {
            return repository.GetAudioPath(id, name);
        }

        public bool UpgradeAccount(Guid USerId)
        {
            return repository.UpgradeAccount(USerId);
        }

        public bool deleteAccount(Guid USerId)
        {
            return repository.deleteAccount(USerId);
        }

        public int totalplaylist(Guid userID)
        {
            return repository.totalplaylist(userID);
        }
        public bool AddSongtoFav(string TrackingID, Guid UserID)
        {
            return repository.AddSongtoFav(TrackingID, UserID);
        }
        public List<favourites> GetFavourites(Guid userID)
        {
            return repository.GetFavourites(userID);
        }
        public bool DelfromFav(string TrackingID, Guid userID)
        {
            return repository.DelfromFav(TrackingID, userID);
        }
        public List<Bytetracker> Getpostingdata(Guid userID, string Trackingnumber)
        {
            return repository.Getpostingdata(userID, Trackingnumber);
        }
        public List<PlaylistModel> GetSongListmusic(Guid UserID, string PlaylistName)
        {
            return repository.GetSongListmusic(UserID, PlaylistName);
        }
        //public bool InsertAlbum(BasicGenerateCloneModel basicGenerateCloneModel, InterruptedFileModel interruptedFileModel, CreateLinkPostModel createLinkPostModel, AllGenerateCloneModel allGenerateCloneModel)
        //{
        //    return repository.InsertAlbum(model, imtmodel, post, Alldata);
        //}
        public bool UpdatPlaylistBasic(string PlaylistName, string TrackingID, Guid UserID)
        {
            return repository.UpdatPlaylistBasic(PlaylistName, TrackingID, UserID);
        }

        

    }

}
