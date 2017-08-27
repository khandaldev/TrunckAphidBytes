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
    public class Premium : IPremium
    {
        RepositoryPremium repository = new RepositoryPremium();

        public PremiumAccountViewModel GetPremiumAccountInfo(Guid userID)
        {
            Common AudioFiles = new Common();
            List<AudioFileModel> Audiofile = new List<AudioFileModel>(AudioFiles.GetAudioFiles(userID, 1));
            List<ImageFileModel> Imagefile = new List<ImageFileModel>(AudioFiles.GetImageFile(userID));
            sp_GetPremiumAccountInfo_Result aphidTiseObjectData = repository.GetPremiumAccountInfo(userID);
            PremiumAccountViewModel PremiumData = new PremiumAccountViewModel();
            if (aphidTiseObjectData != null)
            {
                PremiumData.IsActive = aphidTiseObjectData.IsActive;
                PremiumData.RecoveryEmail = aphidTiseObjectData.RecoveryEmail;
                PremiumData.Biography = aphidTiseObjectData.Biography;
                PremiumData.UserName = aphidTiseObjectData.ComposerName;
                PremiumData.AddressLine1 = aphidTiseObjectData.AddressLine1;
                PremiumData.AddressLine2 = aphidTiseObjectData.AddressLine2;
                PremiumData.Answer1 = aphidTiseObjectData.Answer1;
                PremiumData.Answer2 = aphidTiseObjectData.Answer2;
                PremiumData.City = aphidTiseObjectData.City;
                PremiumData.DOB = aphidTiseObjectData.DOB;
                PremiumData.EmailAddress = aphidTiseObjectData.EmailAddress;
                PremiumData.FirstName = aphidTiseObjectData.FirstName;
                PremiumData.LastName = aphidTiseObjectData.LastName;
                PremiumData.Phone = aphidTiseObjectData.Phone;
                PremiumData.PostalCode = aphidTiseObjectData.PostalCode;
                PremiumData.Region = aphidTiseObjectData.Region;
                PremiumData.SecurityQuestion1 = aphidTiseObjectData.SecurityQuestion1;
                PremiumData.SecurityQuestion2 = aphidTiseObjectData.SecurityQuestion2;
                PremiumData.FirstName = aphidTiseObjectData.FirstName;
                PremiumData.LastName = aphidTiseObjectData.LastName;
                PremiumData.Website = aphidTiseObjectData.Website;
                PremiumData.AccountTypeID = aphidTiseObjectData.AccountTypeID;
                PremiumData.AddressID = aphidTiseObjectData.AddressID;
                PremiumData.SecurityQuestionID = aphidTiseObjectData.SecurityQuestionID;
                PremiumData.Password = aphidTiseObjectData.Password;
                PremiumData.ConfirmPassword = aphidTiseObjectData.Password;
                PremiumData.ProfilePictureServerId = aphidTiseObjectData.PictureServerId ?? 0;
                PremiumData.ProfilePicturePath = aphidTiseObjectData.PicturePath;
                
                if (Audiofile.Count > 0)
                {
                    for (int i = 0; i < Audiofile.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (Audiofile[i].AudioFileName != "Default")
                            {
                                PremiumData.Audio1Name = Audiofile[i].AudioFileName;
                                PremiumData.SelectedAudio1 = Audiofile[i].IsActive;
                                PremiumData.Audio1Path = Audiofile[i].AudioFile;
                            }
                            else
                            {
                                PremiumData.DefaultSelectedAud = Audiofile[i].IsActive;
                            }
                        }
                        if (i == 1)
                        {
                            if (Audiofile[i].AudioFileName != "Default")
                            {
                                PremiumData.Audio2Name = Audiofile[1].AudioFileName;
                                PremiumData.SelectedAudio2 = Audiofile[1].IsActive;
                                PremiumData.Audio2Path = Audiofile[i].AudioFile;
                            }
                            else
                            {
                                PremiumData.DefaultSelectedAud = Audiofile[i].IsActive;
                            }
                        }
                        if (i == 2)
                        {
                            if (Audiofile[i].AudioFileName != "Default")
                            {
                                PremiumData.Audio3Name = Audiofile[2].AudioFileName;
                                PremiumData.SelectedAudio3 = Audiofile[2].IsActive;
                                PremiumData.Audio3Path = Audiofile[i].AudioFile;
                            }
                            else
                            {
                                PremiumData.DefaultSelectedAud = Audiofile[i].IsActive;
                            }
                        }
                        if (i == 3)
                        {
                            if (Audiofile[i].AudioFileName != "Default")
                            {
                                if (PremiumData.SelectedAudio1 == null)
                                {
                                    PremiumData.Audio1Name = Audiofile[i].AudioFileName;
                                    PremiumData.SelectedAudio1 = Audiofile[i].IsActive;
                                    PremiumData.Audio1Path = Audiofile[i].AudioFile;
                                }
                                if (PremiumData.SelectedAudio2 == null)
                                {
                                    PremiumData.Audio2Name = Audiofile[i].AudioFileName;
                                    PremiumData.SelectedAudio2 = Audiofile[i].IsActive;
                                    PremiumData.Audio2Path = Audiofile[i].AudioFile;
                                }
                                if (PremiumData.SelectedAudio3 == null)
                                {
                                    PremiumData.Audio3Name = Audiofile[i].AudioFileName;
                                    PremiumData.SelectedAudio3 = Audiofile[i].IsActive;
                                    PremiumData.Audio3Path = Audiofile[i].AudioFile;
                                }
                            }
                            else
                            {
                                PremiumData.DefaultSelectedAud = Audiofile[i].IsActive;
                            }
                        }
                    }
                }

                if (Imagefile.Count > 0)
                {
                    for (int i = 0; i < Imagefile.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (Imagefile[i].ImageFileName != "Default")
                            {
                                PremiumData.Image1Name = Imagefile[i].ImageFileName;
                                PremiumData.SelectedImage1 = Imagefile[i].IsActive;
                                PremiumData.Image1Path = Imagefile[i].ImageFile;
                            }
                            else
                            {
                                PremiumData.DefaultSelectedImg = Imagefile[i].IsActive;
                            }

                        }
                        if (i == 1)
                        {
                            if (Imagefile[i].ImageFileName != "Default")
                            {
                                PremiumData.SelectedImage2 = Imagefile[i].IsActive;
                                PremiumData.Image2Name = Imagefile[i].ImageFileName;
                                PremiumData.Image2Path = Imagefile[i].ImageFile;
                            }
                            else
                            {
                                PremiumData.DefaultSelectedImg = Imagefile[i].IsActive;
                            }
                        }
                        if (i == 2)
                        {
                            if (Imagefile[i].ImageFileName != "Default")
                            {
                                PremiumData.Image3Name = Imagefile[i].ImageFileName;
                                PremiumData.SelectedImage3 = Imagefile[i].IsActive;
                                PremiumData.Image3Path = Imagefile[i].ImageFile;
                            }
                            else
                            {
                                PremiumData.DefaultSelectedImg = Imagefile[i].IsActive;
                            }
                        }
                        if (i == 3)
                        {
                            if (Imagefile[i].ImageFileName != "Default")
                            {
                                if (PremiumData.SelectedImage1 == null)
                                {
                                    PremiumData.Image1Name = Imagefile[i].ImageFileName;
                                    PremiumData.SelectedImage1 = Imagefile[i].IsActive;
                                    PremiumData.Image1Path = Imagefile[i].ImageFile;
                                }
                                if (PremiumData.SelectedImage2 == null)
                                {
                                    PremiumData.Image2Name = Imagefile[i].ImageFileName;
                                    PremiumData.SelectedImage2 = Imagefile[i].IsActive;
                                    PremiumData.Image2Path = Imagefile[i].ImageFile;
                                }
                                if (PremiumData.SelectedImage3 == null)
                                {
                                    PremiumData.Image3Name = Imagefile[i].ImageFileName;
                                    PremiumData.SelectedImage3 = Imagefile[i].IsActive;
                                    PremiumData.Image3Path = Imagefile[i].ImageFile;
                                }
                            }
                            else
                            {
                                PremiumData.DefaultSelectedImg = Imagefile[i].IsActive;
                            }
                        }
                    }
                }
                PremiumData.PremiumUserID = userID;
                // PremiumData.Audio = aphidTiseObjectData.Audio;
                //  PremiumData.Image = aphidTiseObjectData.Image;

            }
            return PremiumData;
        }

        
        public string FetchEmailRecord(Guid userid, string rec)
        {
            return repository.FetchEmailRecord(userid, rec);
        }
        public List<AllGenerateCloneModel> GetUploadfile(string track)
        {
            return repository.GetUploadfile(track);
        }

        public bool UpdatePdf(PremiumGenerateCloneModel model, AllGenerateCloneModel allmodel, InterruptedFileModel intModel)
        {
            return repository.UpdatePdf(model,allmodel,intModel);
        }
        public List<SocialNetworkModel> SocialNetworkCat(Guid id)
        {
            return repository.Reterive(id);
        }

        public bool UpdatePremiumAccountInfo(PremiumAccountViewModel premium)
        {
            return repository.UpdatePremiumAccountInfo(premium);
        }
        public bool DeletePremiumImage(Guid userID, string val)
        {
            return repository.DeletePremiumImages(userID, val);
        }
        public bool DeletePremiumAudio(Guid userID, string val)
        {
            return repository.DeletePremiumAudio(userID, val);
        }
        public bool InsertPremiumBiteMusicSingle(PremiumGenerateCloneModel model,InterruptedFileModel intModel,CreateLinkPostModel post,AllGenerateCloneModel Alldata)
        {
            return repository.InsertCloneSingle(model,intModel,post,Alldata);
        }
        public List<BindDropDown> BindDropAudio(Guid userId)
        {
            return repository.BindDropAudio(userId);
        }
        public bool InsertPremiumbyteMusicAlbum(PremiumGenerateCloneModel model)
        {
            return repository.InsertByteMusicAlbum(model);
        }
        public bool InsertByteYourArtAndPhoto(PremiumGenerateCloneModel model,InterruptedFileModel intModel,CreateLinkPostModel post,AllGenerateCloneModel Alldata)
        {
            return repository.InsertByteYourArtAndPhoto(model,intModel,post,Alldata);
        }
        public bool InsertPremiumByteYourFiles(PremiumGenerateCloneModel model)
        {
            return repository.InsertYourFile(model);
        }
        public bool InsertByteYourVideo(PremiumGenerateCloneModel model,InterruptedFileModel mm,CreateLinkPostModel po, AllGenerateCloneModel Alldata)
        {
            return repository.InsertYourVideo(model,mm,po,Alldata);
        }

        public bool InsertPremiumByteyourEbook(PremiumGenerateCloneModel model, InterruptedFileModel ob1, CreateLinkPostModel ob,AllGenerateCloneModel Alldata)
        {
            return repository.InsertYourEbook(model,ob1,ob,Alldata);
        }
        public List<BindDropDown> BindDropImage(Guid userid)
        {
            return repository.DropBindIMage(userid);
        }
        public List<BindDropDown> GetVideoInterruptionImage(Guid UserID)
        {
            return repository.BindDropImage(UserID);
        }
        public bool updatpremiumaccount(Guid usid)
        {
            return repository.updatpremiumaccount(usid);
        }
        //public bool GetUserID(string userRecoveryMail)
        //{
        //    return repository.GetUserID(userRecoveryMail);
        //}


        public string GetPremiumWebsite(Guid userId)
        {
            return repository.GetPremiumWebsite(userId);
        }
        public List<CreateLinkPostModel> GetPostData(Guid userid)
        {
            return repository.GetPostData(userid);
        }
        public List<CreateLinkPostModel> GetSearchRecord(Guid userId, string Title, string Category)
        {
            return repository.GetSearchRecord(userId, Title, Category);
        }
        public List<CreateLinkPostModel> AtoZ(Guid userId, string Category, string order)
        {
            return repository.AtoZ(userId, Category, order);
        }
        public PremiumGenerateCloneModel EditClone(string trackNo)
        {
            return repository.EditClone(trackNo);
        }
        public List<Bytetracker> GetPostData1(Guid userId)
        {
            return repository.GetPostData1(userId);
        }
        public List<Bytetracker> expand(Guid userID, string Trackingnumber)
        {
            return repository.expand(userID, Trackingnumber);
        }


        public bool ByteYourFile(PremiumGenerateCloneModel model, InterruptedFileModel intf, CreateLinkPostModel post,AllGenerateCloneModel Alldata)
        {
            return repository.ByteYourFile(model, intf, post,Alldata);
        }

        public string GetCategory(string trackNo)
        {
            return repository.GetCategory(trackNo);
        }

        public ByteArray GetByteArray(string trackNo)
        {
            return repository.GetByteArray(trackNo);
        }
        public bool UpdateClone(PremiumGenerateCloneModel model, InterruptedFileModel mm, CreateLinkPostModel ll)
        {
            return repository.UpdateClone(model, mm, ll);
        }
        public DataPlanDetail DataPlanDetailMethod(Guid UserId)
        {
            return repository.DataPlanDetail(UserId);
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

        public AllGenerateCloneModel Get_A_Record_via_trackID(string TrackingNumber)
        {
            return repository.Get_A_Record_via_trackID(TrackingNumber);
        }
        public bool InsertPremiumTools(AllTools model, UserTool usermodel)
        {
            return repository.InsertPremiumTools(model, usermodel);
        }
        public bool InsertPremiumToolFile(AllTools model, UserTool usermodel, Filecontent filemodel)
        {
            return repository.InsertPremiumToolFile(model, usermodel, filemodel);
        }
        public bool Deletefilecontent(Filecontent filemodel, UserTool usermodel)
        {
            return repository.Deletefilecontent(filemodel, usermodel);
        }
        public List<string> RetPremiumToolFile(Guid user)
        {
            return repository.RetPremiumToolFile(user);
        }
        public string RetPremiumToolFileContent(Guid user, string filename)
        {
            return repository.RetPremiumToolFileContent(user, filename);
        }
        public List<AllTools> RetPremiumToolsInfo()
        {
            return repository.RetPremiumToolsInfo();
        }
        public List<AllTools> RetUserTools(Guid userid)
        {
            return repository.RetUserTools(userid);
        }
        public List<PremiumGenerateCloneModel> fileprivew(String trackingNumber)
        {
            return repository.Fileprivew(trackingNumber);
        }

        public AdvertisementModel Fetch_Ad_Video_Data(string ad_type_id)
        {
            return repository.Fetch_Ad_Video_Data(ad_type_id);
        }

        public bool deleteAccount(Guid USerId)
        {
            return repository.deleteAccount(USerId);
        }

        public bool InsertChannelBiblography(Guid Userid, string data, string userimage,string UserName)
        {
            return repository.InsertChannelBiblography(Userid, data, userimage, UserName);
        }
        public ChannelModel ShowChannelData(Guid userID)
        {
            return repository.ShowChannelData(userID); 
        }
        public PostingDataModel GetUploadfiles(string track)
        {

            return repository.GetUploadfiles(track);
        }
        public bool GetSubscribeUsers(string composer,Guid user,string title,string cat,string channel,string Path,string TrackingNumber,int Credits)
        {
            return repository.GetSubscribeUsers(composer,user,title,cat,channel,Path,TrackingNumber,Credits);
        }


        public bool UnsubscribeChannel(Guid UserID, Guid ChannelId)
        {
            return repository.UnsubscribeChannel(UserID, ChannelId);
        }
        public string GetChannelInfo(Guid user)
        {
            return repository.GetChannelInfo(user);
        }
       

        public ChannelModel LoginUserSubscription(ChannelModel model)
        {
            return repository.LoginUserSubscription(model);
        }

        public int totalplaylist(Guid userID)
        {
            return repository.totalplaylist(userID);
        }

        public bool AddToChannel(string trackno, Guid UserID)
        {
            return repository.AddToChannel(trackno, UserID);
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


        public bool UpdatPlaylist(string PlaylistName, string TrackingID, Guid UserID)
        {
            return repository.UpdatPlaylist(PlaylistName, TrackingID, UserID);
        }
        public bool VerifyEmailAccount(Guid usid)
        {
            return repository.VerifyMailAccount(usid);
        }
    }

}
