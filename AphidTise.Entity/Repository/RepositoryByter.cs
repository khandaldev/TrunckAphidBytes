using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AphidTise.Entity.Repository
{
    public class RepositoryByter : GenericRepository<tblByterAccount>
    {
        public sp_GetByterAccountInfo_Result GetByterAccountInfo(Guid userID)
        {
            try
            {
                return context.sp_GetByterAccountInfo(userID).SingleOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateByterAccountInfo(ByterAccountViewModel byterModel)
        {
            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {

                    //Update data in Bank Account Details
                    if (byterModel.BankAccountID.HasValue)
                    {
                        context.sp_UpdateBankAccountDetails(byterModel.BankAccountID, byterModel.CardNumber, byterModel.ExpiryMonth, byterModel.ExpiryYear, byterModel.CSV, byterModel.NameOnCard);
                    }
                    //Update data in Personal Address
                    if (byterModel.AddressID.HasValue)
                    {
                        context.sp_UpdatePersonAddress(byterModel.AddressID, byterModel.AddressLine1, byterModel.AddressLine2, byterModel.City, byterModel.Region, byterModel.PostalCode);
                    }
                    //Update data in Security Queastion Answer
                    if (byterModel.SecurityQuestionID.HasValue)
                    {
                        context.sp_UpdateSecurityQuestions(byterModel.SecurityQuestionID, byterModel.SecurityQuestion1, byterModel.Answer1, byterModel.SecurityQuestion2, byterModel.Answer2);
                    }
                    if (!string.IsNullOrWhiteSpace(byterModel.ProfilePicturePath))
                    {
                        context.sp_UpdateUsers(byterModel.ProfilePicturePath, byterModel.ProfilePictureServerId, byterModel.ByterUserID);
                    }

                    context.sp_UpdateByterAccount(byterModel.ByterUserID, byterModel.FirstName, byterModel.LastName, byterModel.EmailAddress, byterModel.DOB, byterModel.Phone, byterModel.RecoveryEmail);

                    tranScope.Complete();
                    return true;
                }
            }
            catch
            {
                throw;
            }

        }

        public List<String> GetPlaylistNames(Guid userID, string TrackingID = null)
        {
            List<String> li = new List<String>();
            try
            {
                if (TrackingID == null)
                {
                    var data = context.tbl_PlayList.Where(m => (m.UserID == userID)).Select(m => m.PlaylistName).Distinct().ToList();
                    foreach (var item in data)
                    {
                        li.Add(item);
                    }
                }
                else
                {
                    li.Clear();
                    var result = context.tbl_PlayList.Where(m => (m.TrackingID == TrackingID) && (m.UserID == userID)).Select(m => m.PlaylistName).Distinct().ToList();
                    var data = context.tbl_PlayList.Where(m => (m.UserID == userID)).Select(m => m.PlaylistName).Distinct().ToList();
                    foreach (var item in data)
                    {
                        var f = 0;
                        foreach (var t in result)
                        {
                            if (item != t)
                                f++;
                        }
                        if (f != (result.Count - 1))
                            li.Add(item);
                    }
                }
                return li;
            }
            catch { throw; }
        }


        public List<PlaylistModel> GetSongList(Guid userID, string playName)
        {
            List<PlaylistModel> li = new List<PlaylistModel>();
            try
            {
                var data = context.tbl_PlayList.Where(m => (m.UserID == userID) && (m.PlaylistName == playName)).Select(m => new { m.UserID, m.TrackingID, m.FileName, m.PlaylistName }).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        li.Add(new PlaylistModel()
                        {
                            FileName = item.FileName,
                            TrackingID = item.TrackingID,
                            PlaylistName = item.PlaylistName,
                            UserID = item.UserID,

                        });
                    }
                }
                return li;
            }
            catch { throw; }
        }


        public bool DelSongFromPlay(string PlaylistName, string TrackingID)
        {
            bool deleteStatus = false;
            try
            {
                tbl_PlayList record_play = context.tbl_PlayList.Where(m => (m.PlaylistName == PlaylistName) && (m.TrackingID == TrackingID)).SingleOrDefault();
                if (record_play != null)
                {
                    // audDelete.AudioInterruption = null;
                    context.tbl_PlayList.Remove(record_play);
                    if (context.SaveChanges() > 0)
                        deleteStatus = true;
                }
                return deleteStatus;
            }
            catch { throw; }
        }

        public bool AddSongToPlaylist(string PlaylistName, string TrackingID, Guid UserID)
        {
            bool addStatus = false;
            try
            {
                int count = context.tbl_PlayList.Where(m => (m.PlaylistName == PlaylistName) && (m.UserID == UserID)).Count();
                tbl_PlayList fetch_record = context.tbl_PlayList.Where(m => (m.TrackingID == TrackingID) && (m.UserID == UserID)).SingleOrDefault();
                if (fetch_record != null)
                {
                    if (count < 20)
                    {
                        tbl_PlayList tab = new tbl_PlayList();
                        tab.PlaylistName = PlaylistName;
                        tab.UserID = UserID;
                        tab.TrackingID = TrackingID;
                        tab.FileName = fetch_record.FileName;
                        tab.Composer = fetch_record.Composer;
                        context.tbl_PlayList.Add(tab);
                        context.SaveChanges();
                        addStatus = true;
                    }
                    else
                    {
                    }
                }
                return addStatus;
            }
            catch { throw; }
        }
        public PremiumGenerateCloneModel Get_A_Record_via_trackID(string TrackingNumber)
        {
            try
            {
                PremiumGenerateCloneModel record = new PremiumGenerateCloneModel();
                var data = context.tblPremiumGeterateClones.Where(m => (m.TrackingNumber == TrackingNumber)).SingleOrDefault();
                record.TrackingNumber = data.TrackingNumber;
                record.AlbumTitle = data.AlbumTitle;
                record.ArtistName = data.ArtistName;
                record.UploadAudioPath = data.AudioFilePath;
                record.UploadImagePath = data.ImageFile;


                return record;
            }
            catch { throw; }
        }

        public bool deleteAccount(Guid userid)
        {
            bool IsSuccess = false;
            try
            {
                var data = context.tblByterAccounts.Where(m => m.ByterUserID == userid).SingleOrDefault();
                if (data != null)
                {
                    var result = false;
                    data.IsDelete = true;
                    context.SaveChanges();
                    IsSuccess = true;

                }
            }
            catch (Exception)
            {
                IsSuccess = false;
            }
            return IsSuccess;
        }



        public List<ChannelModel> BindUserChannel(Guid userid)
        {
            List<ChannelModel> li = new List<ChannelModel>();
            try
            {

                var data = context.sp_UserChannelSubs(userid).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        li.Add(new ChannelModel()
                        {
                            ChannelId = item.ChannelID,
                            ImagePath = item.ChannelImagePath,
                            premiumUserId = item.UserID,
                            UserData = item.ChannelBiography,
                            UserName = item.UserName,
                            UserID = item.ByterUserId
                        });
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return li;
        }

        public bool ReduceMsgCount(Guid userid)
        {
            try
            {
                var data = context.tbl_ByterMessage.Where(m => m.ByterId == userid).ToList();
                if (data!=null)
                {
                    foreach (var item in data)
                    {
                        item.MsgStatus = true;
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int? GetTotalCredits(Guid user)
        {

            int? count = 0;
            try
            {
                var data = (from pd in context.tblCreditDetails
                            join od in context.tblAllGenerateClones on pd.TrackingID equals od.TrackingNumber
                            where pd.Aphid == user && pd.IsActive == true

                            select new
                            {
                                pd.Credit,

                            }).ToList();
                for (int i = 0; i < data.Count; i++)
                {

                    count = count + data[i].Credit;
                }

                return count;
            }
            catch
            {
                throw;
            }

        }



        public List<GetPostData> GetPostData(Guid userid)
        {
            try
            {
                var data = context.tbl_MTUserSubscription.Where(m => m.ByterUsrId == userid && m.LinkToPostM == false).ToList();
                List<GetPostData> li = new List<GetPostData>();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        if (item.IsPostDelete != true)
                        {
                            li.Add(new GetPostData()
                            {
                                Composer = item.ComposerName,
                                Category = item.Category,
                                Credit = item.Credits,
                                Title = item.Title,
                                TrackingId = item.TrackingNumber

                            });
                        }
                    }
                }
                //    List<string> channelid = new List<string>();
                //    Guid? Channel = null;
                //    List<GetPostData> li = new List<GetPostData>();
                //    var Subscriber = context.tbl_ChannelSubscription.Where(m => m.ByterUserId == userid && m.Status == true).ToList();
                //    for (int i = 0; i < Subscriber.Count; i++)
                //    {
                //        channelid.Add(Subscriber[i].ChannelID.ToString());
                //    }
                //foreach (var item in channelid)
                //{
                //    Guid gu = new Guid(item);
                //    var IsSubscribe = context.tbl_SendLinkToMT.Where(m => m.ChannelId == gu && m.LinkToPostM == false && m.MessageStatus == true).ToList();
                //    if (IsSubscribe != null)
                //    {
                //        foreach (var item1 in IsSubscribe)
                //        {
                //            li.Add(new GetPostData()
                //            {
                //                Composer = item1.ComposerName,
                //                Category = item1.Category,
                //                Credit = item1.Credits,
                //                Title = item1.Title,
                //                TrackingId = item1.TrackingNumber
                //            });
                //        }
                //    }
                //}


                return li;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<GetPostData> GetSortOrder(Guid user, string sort, string Class)
        {
            List<GetPostData> li = new List<GetPostData>();
            List<GetPostData> list = new List<GetPostData>();
            try
            {
                var data = (from pd in context.tblCreditDetails
                            join od in context.tblAllGenerateClones on pd.TrackingID equals od.TrackingNumber
                            where pd.Aphid == user
                            orderby pd.Title

                            select new
                            {
                                pd.IsActive,
                                pd.Title,
                                pd.Category,
                                pd.Channel,
                                pd.Credit,
                                pd.TrackingID,
                                od.ComposerName
                            }).ToList();
                var data1 = context.tbl_MTUserSubscription.Where(m => m.ByterUsrId == user).OrderBy(m => m.Title).ToList();
                if (Class == "one")
                {
                    if (data != null)
                    {
                        foreach (var item in data)
                        {

                            li.Add(new GetPostData()
                            {
                                Title = item.Title,
                                Category = item.Category,

                                TrackingId = item.TrackingID,
                                Composer = item.ComposerName,
                                Credit = item.Credit,
                                channel = item.Channel,

                                poststatus = item.IsActive.ToString()

                            });
                        }
                    }
                }
                else if (Class == "two" || Class == "three")
                {
                    if (data1 != null)
                    {
                        foreach (var item in data1)
                        {

                            list.Add(new GetPostData()
                            {
                                Title = item.Title,
                                Category = item.Category,

                                TrackingId = item.TrackingNumber,
                                Composer = item.ComposerName,
                                Credit = item.Credits,
                                Linktopost = item.LinkToPostM,


                            });
                        }

                    }

                }
                if (Class == "one")
                {
                    return li;
                }
                else
                {

                    return list;
                }


            }
            catch
            {
                throw;
            }
        }
        public List<GetPostData> GetPostData1(Guid userid, string txt, string Class)
        {
            try
            {
                List<GetPostData> li = new List<GetPostData>();
                List<GetPostData> list = new List<GetPostData>();
                
                var data = (from pd in context.tblCreditDetails
                            join od in context.tblAllGenerateClones on pd.TrackingID equals od.TrackingNumber
                            where pd.Aphid == userid && pd.Title==txt 

                            select new
                            {
                                pd.IsActive,
                                pd.Title,
                                pd.Category,
                                pd.Channel,
                                pd.Credit,
                                pd.TrackingID,
                                od.ComposerName
                            }).ToList();
                var data1 = context.tbl_MTUserSubscription.Where(m => m.ByterUsrId == userid && m.Title==txt).ToList();

                if (Class == "one")
                {
                    if (data != null)
                    {
                        foreach (var item in data)
                        {
                                li.Add(new GetPostData()
                                {
                                    Title = item.Title,
                                    Category = item.Category,
                                    TrackingId = item.TrackingID,
                                    Composer = item.ComposerName,
                                    Credit = item.Credit,
                                    channel = item.Channel,
                                    poststatus = item.IsActive.ToString()

                                });
                          
                        }
                    }
                }
                else if (Class == "two" || Class == "three")
                {
                    if (data1 != null)
                    {
                        foreach (var item in data1)
                        {
                            

                                list.Add(new GetPostData()
                                {
                                    Title = item.Title,
                                    Category = item.Category,
                                    TrackingId = item.TrackingNumber,
                                    Composer = item.ComposerName,
                                    Credit = item.Credits,
                                    Linktopost = item.LinkToPostM


                                });
                           
                        }

                    }

                }
                if (Class == "one")
                {
                    return li;
                }
                else
                {

                    return list;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<GetPostData> GetByterMessage(Guid userid)
        {
            try
            {
                List<GetPostData> li = new List<GetPostData>();
                var data = context.tbl_ChannelSubscription.Where(m => m.ByterUserId == userid && m.Status == true).Select(m => m.ChannelID).ToList();
                foreach (var item in data)
                {
                    var subData = context.tbl_SendLinkToMT.Where(m => m.ChannelId == item && m.MessageStatus == false).ToList();
                    if (subData != null)
                    {
                        foreach (var item1 in subData)
                        {
                            if (item1.IsDelete != true)
                            {
                                li.Add(new GetPostData()
                                {
                                    profileimage = item1.PremiumProfilePic,
                                    Path = item1.Path,
                                    TrackingId = item1.TrackingNumber,
                                    Category=item1.Category
                                });
                            }
                        }
                    }
                }
                return li;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateMessage(string track, Guid userid, string username)
        {
            try
            {
                var dta = context.tbl_SendLinkToMT.Where(m => m.TrackingNumber == track).SingleOrDefault();
                if (dta != null)
                {
                    context.sp_insertMTUserSubs(dta.ComposerName, dta.ChannelId, dta.Credits, dta.Category, dta.UserId, dta.Title, dta.Path, dta.LinkToPostM, dta.PremiumProfilePic, dta.TrackingNumber, dta.MessageStatus, userid, username);
                    dta.MessageStatus = true;
                    context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {

                throw;
            }
        }
      

        public bool UpdatePostQueue(string track)
        {
            try
            {
                var data = context.tbl_MTUserSubscription.Where(m => m.TrackingNumber == track).SingleOrDefault();
                if (data != null)
                {
                    data.LinkToPostM = true;
                    context.SaveChanges();
                    return true;

                }
                else { return false; }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<GetPostData> GetLinkToPostMData(Guid userid)
        {
            List<GetPostData> li = new List<GetPostData>();
            try
            {
                var data = context.tbl_MTUserSubscription.Where(m => m.ByterUsrId == userid && m.LinkToPostM == true).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        if (item.IsDelete != true)
                        {
                        li.Add(new GetPostData()
                        {

                            Composer = item.ComposerName,
                            Category = item.Category,
                            Credit = item.Credits,
                            Title = item.Title,
                            TrackingId = item.TrackingNumber

                        });
                        }
                    }
                }
                return li;
            }

            catch (Exception)
            {

                return null;
            }
        }

        public bool GetLinkPostRecord(Guid user, string track)
        {
            bool IsDelete = false;
            var data = context.tbl_MTUserSubscription.Where(m => m.TrackingNumber == track && m.ByterUsrId == user).SingleOrDefault();
            if (data != null)
            {
                data.IsDelete = true;
                context.SaveChanges();
                IsDelete = true;

            }

            return IsDelete;
        }

        public int GetByterCount(Guid UserId)
        {
            try
            {
                int count = 0;
                List<string> channelid = new List<string>();
                List<string> SubsChannel = new List<string>();
                var channel = context.tbl_ByterMessage.Where(m=>m.MsgStatus==false).ToList();
               // var postingcount = context.tbl_Message.Where(m => m.UserID == UserId).ToList();
                
                if (channel != null)
                {
                    foreach (var item in channel)
                    {
                        if (item.ByterId==UserId)
                        {
                            var scrUser = context.tbl_ChannelSubscription.Where(m => m.ByterUserId == UserId && m.ChannelID == item.ChannelId).SingleOrDefault();
                            if (scrUser != null)
                            {
                                count++;
                            }
                            
                        }
                       
                        
                    }
                  
                }
                int cont = 0;
               var data = context.tbl_Message.Where(m => m.UserID == UserId).ToList();
                if (data.Count != 0)
                {
                    foreach (var item in data)
                   {
                       if ((item.IsNew == 0)||(item.IsNew==null))
                       {
                            cont++;
                       }
                    }             
               }
                int ads = 0;
                var adscount = context.tbl_Surveys.Where(m => m.ByterUserId == UserId).ToList();
                if (adscount.Count > 0)
                {
                    
                    foreach (var item in adscount)
                    {
                        if ((item.IsNew == 0) || (item.IsNew == null))
                        {

                            ads++;
                        }
                    }
                }
                int cnt=count + cont+ads;
               //// return "0";
               return cnt;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<PremiumGenerateCloneModel> Fileprivew(string trackingNumber)
        {
            List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();

            var data = (from pd in context.tblPremiumGeterateClones
                        join od in context.tblInterruptedFiles on pd.TrackingNumber equals od.TrackingNumber
                        where pd.TrackingNumber == trackingNumber

                        select new
                        {
                            pd.Title,
                            pd.AlbumTitle,
                            pd.ExplicitContent,
                            pd.ArtistName,
                            pd.ComposerName,
                            pd.AvailableForDownload,
                            pd.TrackingNumber,
                            pd.MatrixImagePath,
                            pd.ImageFile,
                            od.VideoPath,
                            pd.AudioFilePath,
                            od.InterruptFilePath,
                            pd.PDFFilePath
                        }).ToList();

            if (data != null)
            {
                foreach (var item in data)
                {
                    li.Add(new PremiumGenerateCloneModel()
                    {

                        Title = item.Title,
                        AlbumTitle = item.AlbumTitle,
                        ExplicitContent = item.ExplicitContent,
                        ArtistName = item.ArtistName,
                        Composer = item.ComposerName,
                        AvailableDownload = item.AvailableForDownload,
                        TrackingNumber = item.TrackingNumber,
                        MatrixImageBytePath = item.MatrixImagePath,
                        UploadImagePath = item.ImageFile,
                        VideoFile = item.VideoPath,
                        UploadAudioPath = item.AudioFilePath,
                        Interruptedfile = item.InterruptFilePath,
                        PdfFilePath=item.PDFFilePath

                    });
                }
            }

            return li;
        }
        public List<ShowSelectedNetwork> Network(Guid userid, string selected, string track)
        {

            try
            {
                var data = context.sp_ByterTest(userid, selected, track).ToList();
                if (data.Count != 0)
                {
                    var result = data.Count > 0 ?
                                 data.Distinct(new BytertblSocialNetworkEqualityComparer())
                                       .Select(d => new ShowSelectedNetwork()
                                       {
                                           Id = d.Category,
                                           category = d.Channeltype
                                       }).ToList() : null;
                    return result;
                }
                return null;
            }
            catch
            {
                return null;

            }

        }


        public AllGenerateCloneModel GetUploadfile(string track, Guid id)
        {
            var data = (from pd in context.tblAllGenerateClones
                        join od in context.tblCreateLinkPosts on pd.TrackingNumber equals od.TrackingNo
                        where pd.TrackingNumber == track
                        select new
                        {
                            pd.CatID,
                            pd.Title,
                            od.FileSize,
                            pd.UploadPDFFilePath,
                            pd.UploadFilePath,
                            pd.AudioFilePath,
                            pd.AlbumTitle,
                            pd.PdfFilePath,
                            pd.RARFilePath,
                            pd.VideoCategory,
                            pd.VideoFilePath,
                            pd.Tag
                        }).SingleOrDefault();

            //var data = context.tblAllGenerateClones.Where(m => m.TrackingNumber == track).SingleOrDefault();
            AllGenerateCloneModel model = new AllGenerateCloneModel();
            if (data != null)
            {

                model.CatID = data.CatID;
                model.Title = data.Title;
                model.UploadFilePath = data.UploadFilePath;
                model.AudioFilePath = data.AudioFilePath;
                model.AlbumTitle = data.AlbumTitle;
                model.PdfFilePath = data.PdfFilePath;
                model.RARFilePath = data.RARFilePath;
                model.UploadImageFilePath = data.UploadPDFFilePath;
                model.VideoCategory = data.VideoCategory;
                model.VideoFilePath = data.VideoFilePath;
                model.Tag = data.Tag;
                model.filesize = data.FileSize;
                return model;
            }
            else
                return null;

        }

        public bool CheckMsgStatus(Guid userid, string track)
        {
            try
            {
                var data = context.tbl_MTUserSubscription.Where(m => m.ByterUsrId == userid && m.TrackingNumber == track).SingleOrDefault();
                if (data != null)
                {
                    return false;
                }
                else { return true; }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<GetPostData> GetPostedDataResult(Guid userid)
        {
            try
            {
                List<GetPostData> li = new List<GetPostData>();
                var data = (from pd in context.tblCreditDetails
                            join od in context.tblAllGenerateClones on pd.TrackingID equals od.TrackingNumber
                            where pd.Aphid == userid

                            select new
                            {
                                pd.IsActive,
                                pd.Title,
                                pd.Category,
                                pd.Channel,
                                pd.Credit,
                                pd.TrackingID,
                                od.ComposerName
                            }).ToList();

                if (data != null)
                {
                    foreach (var item in data)
                    {

                        li.Add(new GetPostData()
                        {
                            Title = item.Title,
                            Category = item.Category,
                            channel = item.Channel,
                            TrackingId = item.TrackingID,
                            Composer = item.ComposerName,
                            Credit = item.Credit,
                            poststatus = item.IsActive.ToString()

                        });
                    }
                }

                return li;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string fetch_cat(string Trackingnumber)
        {
            string cat = "0";
            try
            {
                cat = context.tblAllGenerateClones.Where(m => (m.TrackingNumber == Trackingnumber)).Select(m => (m.CatID)).SingleOrDefault().ToString();
                return cat;
            }
            catch
            {
                return cat;
            }
            return cat;
        }

        class BytertblSocialNetworkEqualityComparer : IEqualityComparer<sp_ByterTest_Result>
        {

            public bool Equals(sp_ByterTest_Result x, sp_ByterTest_Result y)
            {
                return (x.Category == y.Category) && (x.Channeltype == y.Channeltype);
            }

            public int GetHashCode(sp_ByterTest_Result obj)
            {
                return obj.Category.GetHashCode() + obj.Channeltype.GetHashCode();
            }
        }
        public string GetMessageCount(Guid userId)
        {
            try
            {
                List<int> li = new List<int>();
                int newCount = 0;
                int count = 0;
                var data = context.tbl_Message.Where(m => m.UserID == userId).ToList();
                var msgcnt = context.tbl_Surveys.Where(m => m.ByterUserId == userId).ToList();
                if (data.Count != 0)
                {
                    foreach (var item in data)
                    {
                        li.Add(Convert.ToInt32(item.CreditPoint));
                        item.IsNew = 1;
                    }
                    context.SaveChanges();

                    for (int i = 0; i < li.Count; i++)
                    {
                        count = count + li[i];
                    }
                    foreach (var item in data)
                    {
                        if (item.IsNew == 0)
                        {
                            newCount++;
                        }
                    }                    
                }
                if (msgcnt.Count > 0)
                {
                    foreach (var item in msgcnt)
                    {
                        li.Add(Convert.ToInt32(item.Credits));
                        item.IsNew = 1;
                    }
                    context.SaveChanges();

                    for (int i = 0; i < li.Count; i++)
                    {
                        count = count + li[i];
                    }
                    foreach (var item in data)
                    {
                        if ((item.IsNew == 0)||(item.IsNew==null))
                        {
                            newCount++;
                        }
                    }
                }
                    return count.ToString() + "+" + newCount.ToString();                
            }
            catch (Exception)
            {

                return null;
            }

        }

        public AllGenerateCloneModel Get_A_Record_via_trackID_new(string TrackingNumber)
        {
            
                try
                {
                    AllGenerateCloneModel record = new AllGenerateCloneModel();
                    var data = context.tblAllGenerateClones.Where(m => (m.TrackingNumber == TrackingNumber)).SingleOrDefault();
                    record.TrackingNumber = data.TrackingNumber;
                    record.AlbumTitle = data.AlbumTitle;
                    record.ArtistName = data.ArtistName;
                    record.AudioFilePath = data.AudioFilePath;
                    record.FileNames = data.FileNames;
                    record.MatrixImagePath = data.MatrixImagePath;
                    record.CatID = data.CatID;
                    return record;
                }
                catch { throw; }
             
        }

        /// <summary>
        /// Verifies mail account
        /// </summary>
        /// <param name="usid"></param>
        /// <returns></returns>
        public bool VerifyMailAccount(Guid usid)
        {
            return context.sp_VerifyByterAccount(usid, true) >= 0 ? true : false;
            //return context.sp_VerifyUser(usid, true) >= 0 ? true : false;

        }
    }
}


