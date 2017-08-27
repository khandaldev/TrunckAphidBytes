using System.IO;
using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AphidTise.Entity.Repository
{
    public class RepositoryHome : GenericRepository<tblReleaseUpdate>
    {
        public bool InsertRelease(AdminModel model)
        {
            try
            {
                context.sp_Release(model.ReleaseID, model.Msg, model.ImagePath);
                return true;
            }
            catch { throw; }

        }

        public bool UpdateRelease(string path, string id, string text)
        {
            bool status = false;
            try
            {
                int id1 = Convert.ToInt32(id);
                var data = context.tblReleaseUpdates.Where(m => m.ID == id1).SingleOrDefault();
                if (data != null)
                {
                    if (path != null)
                    {
                        data.Message = text;
                        data.ImagePath = path;
                        context.SaveChanges();
                    }
                    else
                    {
                        data.Message = text;

                        context.SaveChanges();
                    }
                    status = true;
                }

                else { status = false; }
                return status;
            }
            catch { throw; }
        }

        public bool deleteRecord(string id)
        {
            int id1 = Convert.ToInt32(id);
            bool deleteStatus = false;
            try
            {
                tblReleaseUpdate data = context.tblReleaseUpdates.Where(m => m.ID == id1).SingleOrDefault();
                if (data != null)
                {
                    context.tblReleaseUpdates.Remove(data);
                    if (context.SaveChanges() > 0)
                    {
                        deleteStatus = true;
                    }
                }
                return deleteStatus;
            }
            catch { throw; }
        }

        public List<AdminModel> GetNewRelease()
        {
            List<AdminModel> li = new List<AdminModel>();
            
                var data = context.sp_GetReleaseData().ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        li.Add(new AdminModel()
                        {
                            ImagePath = item.ImagePath,
                            Msg = item.Message,
                            ReleaseID = item.ReleaseId,
                            DBID = item.ID
                        });
                    }
                }
                return li;
            
           
        }

        public List<AdminModel> GetaDMINRelease()
        {
            List<AdminModel> li = new List<AdminModel>();
            try
            {
                var data = context.tblReleaseUpdates.ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        li.Add(new AdminModel()
                        {
                            ImagePath = item.ImagePath,
                            Msg = item.Message,
                            ReleaseID = item.ReleaseId,
                            DBID = item.ID
                        });
                    }
                }
                return li;
            }
            catch { throw; }
        }

        public AdvertisementModel Fetch_Ad_Video_Data(string ad_type_id)
        {
            
            AdvertisementModel li = new AdvertisementModel();
            try
            {

                DateTime _date = DateTime.Now;
                int adtype = int.Parse(ad_type_id);
                var data = context.tblAds.Where(m => (_date >= m.AdCycleFromDate) && (_date <= m.AdCycleToDate) && (adtype == m.AdTypeID)).SingleOrDefault();
                li.AdHyperLinkUrl = data.AdHyperLinkUrl;
                li.AdInformation = data.AdInformation;
                li.AdVideo = data.AdVideo;
                li.AdPicture = data.AdPicture;
                li.CreditsID = data.CreditsID.GetValueOrDefault();
                li.Title = data.Title;
                li.AdTypeID = int.Parse(data.AdTypeID.ToString());
                li.surveyid = data.SurveyID.ToString();
                
               
                
            }
               
            catch
            {
                li = null;
            }
          

            return li;
        }

        public List<searchmodel> outsearchmethod(string text, string category, string trackingnumber)
        {
            List<searchmodel> li = new List<searchmodel>();
            try
            {
                var data = context.sp_SearchAllGenerateClones(category, text, trackingnumber).ToList();
                //var data1 = context.sp_SearchCreateLinkPost(userId, Title, Category);
                if (data != null)
                {

                    foreach (var item in data)
                    {
                        li.Add(new searchmodel()
                        {
                            Title = item.Title,
                            TrackingNumber = item.TrackingNumber,
                            MatrixImagePath = item.MatrixImagePath,
                            InterupptedFilepath = item.MatrixFilePath,
                            category = item.CatID.ToString(),
                            PremiumUserId = item.UserID

                        });
                    }
                }
                return li;
            }
            catch { throw; }
        }

        public AllGenerateCloneModel GetTrack(string trackingnumber)
        {
            AllGenerateCloneModel obj = new AllGenerateCloneModel();
            try
            {
                var data = context.tblAllGenerateClones.Where(m => (m.TrackingNumber == trackingnumber)).SingleOrDefault();
                if (data != null)
                {
                    obj.AudioFilePath = data.AudioFilePath;
                    obj.UserID = data.UserID;
                }
                return obj;
            }
            catch { throw; }
        }

        public BasicGenerateCloneModel Fileprivew(string trackingNumber)
        {
            BasicGenerateCloneModel li = new BasicGenerateCloneModel();
            try
            {
                var data = context.tblAllGenerateClones.Where(m => m.TrackingNumber == trackingNumber).SingleOrDefault();
                if (data != null)
                {
                    li.Title = data.Title;
                    li.AlbumTitle = data.AlbumTitle;
                    li.ExplicitContent = data.ExplicitContent;
                    li.ArtistName = data.ArtistName;
                    li.Composer = data.ComposerName;
                    li.AvailableDownload = data.AvailableForDownload;
                    li.TrackingNumber = data.TrackingNumber;
                    li.MatrixImageBytePath = data.MatrixImagePath;
                    li.UploadImagePath = data.UploadImageFilePath;
                    li.VideoFile = data.VideoFilePath;
                    li.UploadAudioPath = data.AudioFilePath;
                    li.Interruptedfile = data.SelectedInteruptionFile;
                    li.UserID = data.UserID;
                    li.GenCloneType = data.GenCloneID.ToString();
                    li.GenCloneType = data.GenCloneID.ToString();
                    li.UploadFilePDFPath = data.UploadPDFFilePath;
                }
                return li;
            }
           catch { throw; }
        }

        public ChannelModel UserLogin(ChannelModel model)
        {
            try
            {
                Guid? PremiumId = model.premiumUserId;
                ChannelModel obj = new ChannelModel();
                using (TransactionScope tranScope = new TransactionScope())
                {

                    var data = context.tblUsers.Where(m => m.UserName == model.UserName && m.UserPassword == model.Password && m.AccountTypeID == 3).SingleOrDefault();
                    if (data != null)
                    {
                        obj.UserID = data.UserId;
                        obj.UserStatus = data.UserStatus;
                        var SubCheck = context.tbl_ChannelSubscription.Where(m => m.ChannelID == model.ChannelId && m.ByterUserId==data.UserId).SingleOrDefault();
                        if (SubCheck == null)
                        {
                            context.sp_ChannelSubscription(obj.UserID, PremiumId, "", "", true, System.DateTime.Now, model.ChannelId);
                            var channeldata = context.tblChannelPages.Where(m => m.UserID == PremiumId).SingleOrDefault();
                            if (channeldata != null)
                            {
                                obj.ImagePath = channeldata.ChannelImagePath;
                                obj.UserData = channeldata.ChannelBiography;
                                obj.UserName = channeldata.UserName;
                                obj.SubscriptionStatus = true;
                                obj.ChannelId = channeldata.ChannelID;
                                obj.UserID = data.UserId;
                                obj.premiumUserId = PremiumId;
                            }

                            var msgdat = new tbl_ByterMessage { ByterId = data.UserId, ChannelId = model.ChannelId, MsgStatus = null };
                            context.tbl_ByterMessage.Add(msgdat);
                            context.SaveChanges();
                        }
                        else
                        {
                            if (SubCheck.Status == false)
                            {
                                SubCheck.Status = true;
                                context.SaveChanges();
                            }
                            var channeldata = context.tblChannelPages.Where(m => m.UserID == PremiumId).SingleOrDefault();
                            if (channeldata != null)
                            {
                                obj.ImagePath = channeldata.ChannelImagePath;
                                obj.UserData = channeldata.ChannelBiography;
                                obj.UserName = channeldata.UserName;
                                obj.SubscriptionStatus = true;
                                obj.ChannelId = channeldata.ChannelID;
                                obj.UserID = data.UserId;
                                obj.premiumUserId = PremiumId;
                            }
                        }

                    }
                    else
                    {
                        obj.UnAuthorised = true;
                        var channeldata = context.tblChannelPages.Where(m => m.UserID == PremiumId).SingleOrDefault();
                        if (channeldata != null)
                        {
                            obj.ImagePath = channeldata.ChannelImagePath;
                            obj.UserData = channeldata.ChannelBiography;
                            obj.UserName = channeldata.UserName;
                            obj.SubscriptionStatus = false;
                            obj.ChannelId = channeldata.ChannelID;
                            obj.UserID = data.UserId;
                            obj.premiumUserId = PremiumId;
                        }
                    }

                    tranScope.Complete();
                }
                return obj;
            }

            catch (Exception)
            {

                throw;
            }
        }

        public string CheckAccountType(Guid UseriD)
        {
            try
            {

                var data = context.tblUsers.Where(m => m.UserId == UseriD).Select(m => m.AccountTypeID).SingleOrDefault();
                return data.ToString();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool AddSongtoFav(string TrackingID, Guid UserID)
        {
            bool addStatus = false;
            try
            {
                var is_record_present = context.tblFavourites.Any(m => (m.TrackingNumber == TrackingID) && (m.UserID == UserID));
                if (is_record_present == false)
                {
                    tblAllGenerateClone fetchRecord =
                        context.tblAllGenerateClones.Where(m => (m.TrackingNumber == TrackingID)).SingleOrDefault();
                    if (fetchRecord != null)
                    {
                        tblFavourite rec = new tblFavourite();
                        rec.TrackingNumber = fetchRecord.TrackingNumber;
                        rec.UserID = UserID;
                        rec.FileName = fetchRecord.FileNames;
                        context.tblFavourites.Add(rec);
                        context.SaveChanges();
                        addStatus = true;
                    }
                    else
                    {
                        //for overflow playlists
                    }
                }
                return addStatus;
            }
            catch { throw; }
        }


        public bool SubmitFeedback(Guid user, string Feedbacktxt, string TrackId, int Credits, string Path,string val)
        {
            bool IsAct = false;
            var tbl=new tbl_FeedBack();
            var data = context.tblUsers.Where(m => m.UserId == user).Select(m => m.AccountTypeID).SingleOrDefault();
            if (data==3)
            {
                var credit = new tbl_Surveys { ByterUserId = user, Credits = Credits, TrackingNo = TrackId, FeedbackText = Feedbacktxt, CreatedDate = System.DateTime.Now, ModifyDate = System.DateTime.Now,Imagepath=Path };
                context.tbl_Surveys.Add(credit);
                context.SaveChanges();
                return IsAct;
              
            }
            var da = new tbl_FeedBack { Text = Feedbacktxt, UserID = user, CreatedDate = System.DateTime.Now, UserEmail=val };

            context.tbl_FeedBack.Add(da);
            context.SaveChanges();
            if (data==3)
            {
                return IsAct;
            }
            else
            {
                return true;
            }
            
           
        }
        public string FetchEmail(Guid user)
        {
            var data = context.sp_forgetpassword().Where(m => m.BasicUserID == user).Select(m => m.EmailAddress).SingleOrDefault();

            return data;
 
        }

        public AdvertisementModel survey(Guid suyveyid)
        {
            AdvertisementModel li = new AdvertisementModel();
            try
            {
                var data = (from pd in context.tblAds
                            join od in context.tblSurveyQuestions on pd.SurveyID equals od.SurveyID
                            where pd.SurveyID == suyveyid

                            select new
                            {
                                pd.CompanyLogo,
                                od.Question,
                                od.Option1,
                                od.Option2,
                                od.Option3,
                                od.Option4,
                                od.Option5,
                                pd.CreditsID,
                                pd.TrackingNo,
                               

                            }).SingleOrDefault();

                if (data != null)
                {
 

                        li.Title = data.CompanyLogo;
                        li. Surveyquestion = data.Question;
                        li.option1 = data.Option1;
                        li. option2 = data.Option2;
                        li.CreditsID = data.CreditsID;
                        li.TrackingNumber = data.TrackingNo;


                    
                }
                

                return li;
            }
            catch { throw; }
        }
    }
}

