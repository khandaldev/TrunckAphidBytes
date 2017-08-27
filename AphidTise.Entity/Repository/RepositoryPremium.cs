using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static AphidBytes.Core.PaymentServices.StripePackages;

namespace AphidTise.Entity.Repository
{
    public class RepositoryPremium : GenericRepository<tblPremiumAccount>
    {
        public sp_GetPremiumAccountInfo_Result GetPremiumAccountInfo(Guid userID)
        {
            try
            {
                return context.sp_GetPremiumAccountInfo(userID).SingleOrDefault();
                // context.Database.CommandTimeout= int.MaxValue;
            }
            catch { throw; }
        }

        public bool updatpremiumaccount(Guid usid)
        {

            return context.sp_updatpremiumaccount(usid, true) >= 0 ? true : false;

        }
        public string FetchEmailRecord(Guid userid, string rec)
        {
            if (rec != null)
            {
                var emaildata = context.tblPremiumAccounts.Where(m => m.PremiumUserID == userid).Select(m => m.RecoveryEmail).FirstOrDefault();
                if (emaildata == rec)
                {
                    return "Already EmailId Present";

                }
                else if (emaildata != rec)
                {
                    context.sp_updatpremiumaccount(userid, false);
                    return "New EmailId";

                }
            }

            return "";
        }

        public bool DeletePremiumImages(Guid UserID, string val)
        {
            bool deleteStatus = false;
            try
            {
                tblWaterMarkUpInterruption imgDelete = context.tblWaterMarkUpInterruptions.Where(m => m.WatermarkImageName == val && m.UserId == UserID).SingleOrDefault();
                if (imgDelete != null)
                {
                    // audDelete.AudioInterruption = null;
                    context.tblWaterMarkUpInterruptions.Remove(imgDelete);
                    if (context.SaveChanges() > 0)
                        deleteStatus = true;
                }
                return deleteStatus;
            }
            catch { throw; }
        }
        public bool Deletefilecontent(Filecontent filemodel, UserTool usermodel)
        {

            bool Deletestatus = false;
            int i = 0;
            try
            {
                //tblToolFile data = context.tblToolFiles.Where(m => (m.Toolcontent == filemodel.ToolContent) && (m.ToolID == filemodel.ToolId)).SingleOrDefault();
                var result = context.tblToolFiles.Where(m => (m.UserID == filemodel.Userid) && (m.FileName == filemodel.FileName)).SingleOrDefault();
                if (result != null)
                {
                    context.sp_ToolsInfo(null, null, 5, filemodel.ToolId, filemodel.Userid, null, null, filemodel.FileName, null, null, null);


                }
                if (i > 0)
                    Deletestatus = true;

                return Deletestatus;
            }
            catch { throw; }
        }

        public List<SocialNetworkModel> Reterive(Guid id)
        {
            try
            {
                var data = context.tblSocialNetworks.Where(m => m.Aphid == id).ToList();
                List<SocialNetworkModel> li = new List<SocialNetworkModel>();
                foreach (var item in data)
                {
                    var value = context.tblCategories.Where(m => m.CategoryID == item.Category).Single();
                    li.Add(new SocialNetworkModel()
                    {
                        category = value.CategoryName,
                    });

                }
                return li;
            }
            catch { throw; }
        }



        public bool InsertPremiumTools(AllTools model, UserTool usermodel)
        {
            bool Insertstatus = false;
            try
            {
                context.sp_ToolsInfo(model.ToolName, model.ImagePath, 2, model.ToolId, usermodel.userid, System.DateTime.Now, System.DateTime.Now, null, null, null, null);
                Insertstatus = true;
                return Insertstatus;
            }
            catch { throw; }

        }
        public bool InsertPremiumToolFile(AllTools model, UserTool usermodel, Filecontent filemodel)
        {
            try
            {
                var data = context.tblToolFiles.Where(m => m.FileName == filemodel.FileName).SingleOrDefault();
                if (data == null)
                {
                    var value = context.sp_ToolsInfo(null, null, 3, model.ToolId, usermodel.userid, null, null, filemodel.FileName, filemodel.ToolContent, null, null);
                    return true;
                }

                return false;
            }
            catch { throw; }
            //context.sp_ToolsInfo(model.ToolName, model.ImagePath, 3, model.ToolID, model.UserId, System.DateTime.Now, System.DateTime.Now, model.FileName, model.content);
            //Insertstatus = true;
            //return Insertstatus;

        }
        public List<AllTools> RetPremiumToolsInfo()
        {
            List<AllTools> li = new List<AllTools>();
            try
            {

                var data = context.tblTools.ToList();
                foreach (var item in data)
                {
                    li.Add(new AllTools()
                    {
                        ImagePath = item.Images,
                        //IsActive=item.IsActive,
                        ToolId = item.ToolID,
                        ToolINfo = item.ToolInfo,
                        ToolName = item.ToolName
                    });
                }
                return li;

            }
            catch
            {
                return li;
            }

        }
        public List<AllTools> RetUserTools(Guid UserId)
        {
            List<int> li = new List<int>();
            List<AllTools> allList = new List<AllTools>();
            try
            {
                var usertool = context.tblToolsInfoes.Where(m => m.UserID == UserId).ToList();
                foreach (var item in usertool)
                {
                    li.Add(item.ToolID);
                }
                var alltools = context.tblTools.ToList();
                foreach (var item in alltools)
                {
                    if (li.Contains(item.ToolID))
                    {
                        allList.Add(new AllTools()
                        {
                            ToolName = item.ToolName,
                            ToolINfo = item.ToolInfo,
                            ToolId = item.ToolID,
                            ImagePath = item.Images
                        });
                    }
                }
            }
            catch { throw; }
            return allList;
        }
        
        public bool GetSubscribeUsers(string composer, Guid user, string title, string cat, string channel, string Path, string TrackingNumber,int Credits)
        {
            bool Insert = false;
            var Info = context.tbl_SendLinkToMT.Select(m => m.TrackingNumber).ToList();
            for (int i = 0; i < Info.Count; i++)
            {
                if (Info[i] == TrackingNumber)
                {
                   
                    return false;
                }

            }
           
            try
            {
                string pic = "";
                var profilepic = context.tblAllGenerateClones.Where(m => m.TrackingNumber == TrackingNumber).Select(m => m.MatrixImagePath).SingleOrDefault();
                if (profilepic!=null)
                {
                    pic = profilepic; 
                }
                Guid id = new Guid(channel);

                var ByterUsr = context.tbl_ChannelSubscription.Where(m=>m.Status==true&&m.ChannelID==id).Select(m => m.ByterUserId).ToList();
                if (ByterUsr != null)
                {
                    foreach (var item in ByterUsr)
                    {
                        var dat = new tbl_ByterMessage { ByterId = item, ChannelId = id, MsgStatus = false };
                        context.tbl_ByterMessage.Add(dat);
                        context.SaveChanges();

                    }
                }
                Guid channel1 = new Guid(channel);
                var data = new tbl_SendLinkToMT { ID = 0, Category = cat, ChannelId = channel1, ComposerName = composer, Credits = Credits, Title = title, UserId = user, Path = Path, TrackingNumber = TrackingNumber, LinkToPostM = false, PremiumProfilePic = pic, MessageStatus = false, ByterUsrId = null };
                context.tbl_SendLinkToMT.Add(data);
                context.SaveChanges(); 
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
            


        }




        //public List<PremiumTools> RetPremiumTools(Guid userid)
        //{
        //    List<string> li = new List<string>();
        //    var data = context.tblToolFiles.Where(m => m.UserID == userid).Select(m=>m.ToolID).Distinct().ToList();
        //    return li;
        //}
        public List<string> RetPremiumToolFile(Guid user)
        {
            List<string> li = new List<string>();
            try
            {
                var data = context.tblToolFiles.Where(m => m.UserID == user).Select(m => m.FileName).ToList();
                for (int i = 0; i < data.Count; i++)
                {

                    li.Add(data[i].ToString());
                }
                return li;
            }
            catch { throw; }
        }
        public string RetPremiumToolFileContent(Guid user, string filename)
        {
            string text = "";
            string file = filename.Trim();
            try
            {
                var data = context.tblToolFiles.Where(m => m.UserID == user && m.FileName == file).Select(m => m.Toolcontent).SingleOrDefault();
                if (data != null)
                {
                    text = data;

                }
                return text;
            }
            catch { throw; }
        }


        public string GetPremiumWebsite(Guid ID)
        {
            string name = "";
            try
            {
                var data = context.tblPremiumAccounts.Where(m => m.PremiumUserID == ID).Select(m => m.Website).SingleOrDefault();
                if (data != null)
                {
                    name = data;
                }
                return name;
            }
            catch { throw; }
        }
        public List<BindDropDown> BindDropImage(Guid UserID)
        {
            List<BindDropDown> li = new List<BindDropDown>();
            try
            {
                var data = context.tblWaterMarkUpInterruptions.Where(m => m.UserId == UserID).ToList();
                foreach (var item in data)
                {
                    li.Add(new BindDropDown()
                    {
                        id = item.ID,
                        Value = item.WatermarkImageName,
                        ImageName = item.ImageInterruption
                    });
                }
                return li;
            }
            catch { throw; }
        }
        public bool UpdateClone(PremiumGenerateCloneModel model, InterruptedFileModel intModel, CreateLinkPostModel post)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    ////context.sp_UpdatePremiumCloneModel(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                    ////    model.AlbumTitle, model.UploadAudioPath, model.UploadImage, model.Composer, model.Producer,
                    //    model.Publisher, model.SelectedIntFile, model.InterruptionStyle,
                    //    model.AvailableDownload, model.ExplicitContent, model.Type, model.PdfFilePath, 
                    //    model.VideoFile, model.PagePercentage, model.RarFilePath, model.MatrixImageBytePath, 
                    //    model.CreatorName, model.TrackingNumber);


                    context.sp_UpdateCreateLinkPost(post.Title, post.Channel, post.NoOfChannel, post.Views,
                        post.Downloads, post.FileSize, post.TrackingNumber, post.Date, post.Category, post.UserID);

                    context.sp_UpdateIntrepputedFiles(intModel.UserId, intModel.CloneId, intModel.InterruptedFilePath,
                        intModel.CreateDate, intModel.ModifiedDate, intModel.IsActive, intModel.FileName,
                        intModel.VideoPath, intModel.TrackNumber, ""); trans.Complete();
                    return true;
                }
            }
            catch { throw; }
        }
        public bool DeletePremiumAudio(Guid userID, string val)
        {
            bool deleteStatus = false;
            try
            {
                tblAudioInterruption audDelete = context.tblAudioInterruptions.Where(m => m.FileName == val && m.UserId == userID).SingleOrDefault();
                if (audDelete != null)
                {
                    // audDelete.AudioInterruption = null;
                    context.tblAudioInterruptions.Remove(audDelete);
                    if (context.SaveChanges() > 0)
                        deleteStatus = true;
                }
                return deleteStatus;
            }
            catch { throw; }
        }

        public List<BindDropDown> BindDropAudio(Guid UserID)
        {
            List<BindDropDown> li = new List<BindDropDown>();
            try
            {
                var data = context.tblAudioInterruptions.Where(m => m.UserId == UserID && m.IsActive == true).SingleOrDefault();
                if (data != null)
                {
                    li.Add(new BindDropDown()
                    {
                        id = data.ID,
                        Value = data.FileName,
                        Name = data.FileName,
                        Path = data.AudioInterruptionFileName
                        // AudioByte = data.AudioInterruption
                    });

                }
            }
            catch { throw; }
            return li;
        }

        public List<BindDropDown> DropBindIMage(Guid Userid)
        {
            List<BindDropDown> li = new List<BindDropDown>();
            try
            {
                var data = context.tblWaterMarkUpInterruptions.Where(m => m.UserId == Userid && m.IsActive == true).SingleOrDefault();
                if (data != null)
                {
                    li.Add(new BindDropDown()
                        {
                            Name = data.WatermarkImageName,
                            ImageName = data.ImageInterruption,

                        }
                        );
                }
            }
            catch { throw; }
            return li;
        }

        public bool InsertCloneSingle(PremiumGenerateCloneModel model, InterruptedFileModel intModel, CreateLinkPostModel post, AllGenerateCloneModel Alldata)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_PremiumGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                        model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Producer,
                        model.Publisher, model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload,
                        model.ExplicitContent, model.Type, model.PdfFilePath, model.VideoFile, model.PagePercentage,
                        model.RarFilePath, model.MatrixImageBytePath, model.CreatorName, model.TrackingNumber,model.FileLength,model.CatID);


                    context.sp_InterruptedFiles(intModel.UserId, intModel.CloneId, intModel.InterruptedFilePath, intModel.CreateDate, intModel.ModifiedDate, intModel.IsActive, intModel.FileName, intModel.VideoPath, intModel.TrackNumber,intModel.CatId);

                    context.sp_CreateLinkPost(post.Title, post.Channel, post.NoOfChannel, post.Views, post.Downloads, post.FileSize, post.TrackingNumber, post.Date, post.Category,  post.UserID,post.MatrixImagePath);
                    context.sp_AllGenerateClones(Alldata.UserID, Alldata.CloneId, Alldata.Title, Alldata.Tag, Alldata.ArtistName, Alldata.AlbumTitle, Alldata.AudioFilePath, Alldata.UploadFilePath, Alldata.MatrixFilePath, Alldata.ComposerName, Alldata.Producer, Alldata.Publisher,
                       Alldata.SelectedInteruptionFile, Alldata.InteruptionStyle, Alldata.AvailableForDownload, Alldata.ExplicitContent, Alldata.UploadImageFilePath, Alldata.UploadPDFFilePath, Alldata.PagePercentage, Alldata.Type, Alldata.PdfFilePath, Alldata.FileNames,
                       Alldata.VideoFilePath, Alldata.WaterMarkMatrixImagePath, Alldata.WaterMarkMatrixImageText, Alldata.VideoCategory, Alldata.RARFilePath, Alldata.MatrixImagePath, Alldata.CreatotName, Alldata.TrackingNumber, Alldata.CatID, Alldata.GenCloneID,
                       Alldata.CreatedDate, Alldata.ModifyDate, Alldata.IsActive);
                    trans.Complete();
                    return true;
                }
            }
            catch { throw; }
        }
        public bool InsertByteMusicAlbum(PremiumGenerateCloneModel model)
        {
            try
            {
                int re = context.sp_PremiumGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                        model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Producer,
                        model.Publisher, model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload,
                        model.ExplicitContent, model.Type, model.PdfFilePath, model.VideoFile, model.PagePercentage,
                        model.RarFilePath, model.MatrixImageBytePath, model.CreatorName, model.TrackingNumber, model.FileLength,model.CatID);
                if (re == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { throw; }
        }

        public bool InsertByteYourArtAndPhoto(PremiumGenerateCloneModel model, InterruptedFileModel intModel, CreateLinkPostModel post, AllGenerateCloneModel Alldata)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_PremiumGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                       model.AlbumTitle, model.UploadAudioPath, model.UploadImagePath, model.Composer, model.Producer, model.Publisher,
                       model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent,
                       model.Type, model.PdfFilePath, model.VideoFile, model.PagePercentage, model.RarFilePath, model.MatrixImageBytePath,
                       model.CreatorName, model.TrackingNumber,model.FileLength,model.CatID);

                    context.sp_InterruptedFiles(intModel.UserId, intModel.CloneId, intModel.InterruptedFilePath, intModel.CreateDate, intModel.ModifiedDate, intModel.IsActive, intModel.FileName, intModel.VideoPath, intModel.TrackNumber,intModel.CatId);

                    context.sp_CreateLinkPost(post.Title, post.Channel, post.NoOfChannel, post.Views, post.Downloads, post.FileSize, post.TrackingNumber, post.Date, post.Category, post.UserID, post.MatrixImagePath);
                    context.sp_AllGenerateClones(Alldata.UserID, Alldata.CloneId, Alldata.Title, Alldata.Tag, Alldata.ArtistName, Alldata.AlbumTitle, Alldata.AudioFilePath, Alldata.UploadFilePath, Alldata.MatrixFilePath, Alldata.ComposerName, Alldata.Producer, Alldata.Publisher,
                       Alldata.SelectedInteruptionFile, Alldata.InteruptionStyle, Alldata.AvailableForDownload, Alldata.ExplicitContent, Alldata.UploadImageFilePath, Alldata.UploadPDFFilePath, Alldata.PagePercentage, Alldata.Type, Alldata.PdfFilePath, Alldata.FileNames,
                       Alldata.VideoFilePath, Alldata.WaterMarkMatrixImagePath, Alldata.WaterMarkMatrixImageText, Alldata.VideoCategory, Alldata.RARFilePath, Alldata.MatrixImagePath, Alldata.CreatotName, Alldata.TrackingNumber, Alldata.CatID, Alldata.GenCloneID,
                       Alldata.CreatedDate, Alldata.ModifyDate, Alldata.IsActive);
                    trans.Complete();
                    return true;
                }

            }
            catch { throw; }
        }
        public bool InsertYourFile(PremiumGenerateCloneModel model)
        {
            try
            {
                int re = context.sp_PremiumGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                    model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Producer, model.Publisher,
                    model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent, model.Type,
                    model.PdfFilePath, model.VideoFile, model.PagePercentage, model.RarFilePath, model.MatrixImageBytePath, model.CreatorName,
                    model.TrackingNumber,model.FileLength,model.CatID);
                if (re == 1)
                {
                    return true;
                }
                else { return false; }
            }
            catch { throw; }
        }

        public bool InsertYourEbook(PremiumGenerateCloneModel model, InterruptedFileModel intModel, CreateLinkPostModel post, AllGenerateCloneModel Alldata)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_PremiumGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                       model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Producer, model.Publisher,
                       model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent, model.Type,
                       model.PdfFilePath, model.VideoFile, model.PagePercentage, model.RarFilePath, model.MatrixImageBytePath, model.CreatorName,
                       model.TrackingNumber,model.FileLength,model.CatID);
                    context.sp_InterruptedFiles(intModel.UserId, intModel.CloneId, intModel.InterruptedFilePath, intModel.CreateDate, intModel.ModifiedDate, intModel.IsActive, intModel.FileName, intModel.VideoPath, intModel.TrackNumber,intModel.CatId);

                    context.sp_CreateLinkPost(post.Title, post.Channel, post.NoOfChannel, post.Views, post.Downloads, post.FileSize, post.TrackingNumber, post.Date, post.Category, post.UserID,post.MatrixImagePath);
                    context.sp_AllGenerateClones(Alldata.UserID, Alldata.CloneId, Alldata.Title, Alldata.Tag, Alldata.ArtistName, Alldata.AlbumTitle, Alldata.AudioFilePath, Alldata.UploadFilePath, Alldata.MatrixFilePath, Alldata.ComposerName, Alldata.Producer, Alldata.Publisher,
                    Alldata.SelectedInteruptionFile, Alldata.InteruptionStyle, Alldata.AvailableForDownload, Alldata.ExplicitContent, Alldata.UploadImageFilePath, Alldata.UploadPDFFilePath, Alldata.PagePercentage, Alldata.Type, Alldata.PdfFilePath, Alldata.FileNames,
                    Alldata.VideoFilePath, Alldata.WaterMarkMatrixImagePath, Alldata.WaterMarkMatrixImageText, Alldata.VideoCategory, Alldata.RARFilePath, Alldata.MatrixImagePath, Alldata.CreatotName, Alldata.TrackingNumber, Alldata.CatID, Alldata.GenCloneID,
                    Alldata.CreatedDate, Alldata.ModifyDate, Alldata.IsActive);
                    trans.Complete();

                    return true;
                }
            }
            catch { throw; }
        }

        public bool InsertYourVideo(PremiumGenerateCloneModel model, InterruptedFileModel intmodel, CreateLinkPostModel po, AllGenerateCloneModel Alldata)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    context.sp_PremiumGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName, model.AlbumTitle,
                        model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Producer, model.Publisher,
                        model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent, model.Type,
                        model.PdfFilePath, model.VideoFile, model.PagePercentage, model.RarFilePath, model.MatrixImageBytePath,
                        model.CreatorName, model.TrackingNumber,model.FileLength,model.CatID);

                    context.sp_InterruptedFiles(intmodel.UserId, intmodel.CloneId, intmodel.InterruptedFilePath, intmodel.CreateDate, intmodel.ModifiedDate, intmodel.IsActive, intmodel.FileName, intmodel.VideoPath, intmodel.TrackNumber,intmodel.CatId);

                    context.sp_CreateLinkPost(po.Title, po.Channel, po.NoOfChannel, po.Views, po.Downloads, po.FileSize, po.TrackingNumber, po.Date, po.Category, po.UserID, po.MatrixImagePath);
                    context.sp_AllGenerateClones(Alldata.UserID, Alldata.CloneId, Alldata.Title, Alldata.Tag, Alldata.ArtistName, Alldata.AlbumTitle, Alldata.AudioFilePath, Alldata.UploadFilePath, Alldata.MatrixFilePath, Alldata.ComposerName, Alldata.Producer, Alldata.Publisher,
                      Alldata.SelectedInteruptionFile, Alldata.InteruptionStyle, Alldata.AvailableForDownload, Alldata.ExplicitContent, Alldata.UploadImageFilePath, Alldata.UploadPDFFilePath, Alldata.PagePercentage, Alldata.Type, Alldata.PdfFilePath, Alldata.FileNames,
                      Alldata.VideoFilePath, Alldata.WaterMarkMatrixImagePath, Alldata.WaterMarkMatrixImageText, Alldata.VideoCategory, Alldata.RARFilePath, Alldata.MatrixImagePath, Alldata.CreatotName, Alldata.TrackingNumber, Alldata.CatID, Alldata.GenCloneID,
                      Alldata.CreatedDate, Alldata.ModifyDate, Alldata.IsActive);
                    scope.Complete();

                    return true;
                }
            }
            catch { throw; }
        }
        public string GetCategory(string trackNo)
        {
            try
            {
                var name = context.tblCreateLinkPosts.Where(m => m.TrackingNo == trackNo).Select(m => m.Category).SingleOrDefault();
                return name;
            }
            catch { throw; }
        }
        public bool InsertBtyteYourVideo(PremiumGenerateCloneModel model)
        {
            model.CloneID = Guid.NewGuid();
            try
            {
                int re = context.sp_PremiumGenerateClone(model.UserID, model.CloneID,
                    model.Title, model.Tags, model.ArtistName, model.AlbumTitle, model.UploadAudioPath,
                    model.MatrixImageBytePath, model.Composer, model.Producer,
                    model.Publisher, model.SelectedIntFile,
                    model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent,
                    model.Type, model.PdfFilePath, model.VideoFile, model.PagePercentage, 
                    model.RarFilePath, model.MatrixImageBytePath, model.CreatorName, model.TrackingNumber,model.FileLength,model.CatID);
                if (re == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { throw; }
        }

        public bool UpdatePdf(PremiumGenerateCloneModel model,AllGenerateCloneModel allmodel,InterruptedFileModel intmodel)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                context.sp_UpdatePremiumClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName, model.AlbumTitle, model.UploadAudioPath, model.Composer,
                    model.Producer, model.Publisher, model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent, model.Type, model.PdfFilePath,
                    model.VideoFile, model.PagePercentage, model.RarFilePath, model.MatrixImageBytePath, model.CreatorName, model.UploadImagePath,model.CatID,System.DateTime.Now,
                    model.Isvalid, model.TrackingNumber,model.FileLength);
                context.sp_UpdateAllGenerateClone(allmodel.UserID, allmodel.CloneId, allmodel.Title, allmodel.Tag, allmodel.ArtistName, allmodel.AlbumTitle, allmodel.AudioFilePath, allmodel.UploadFilePath,
                    allmodel.MatrixFilePath, allmodel.ComposerName, allmodel.Producer, allmodel.Publisher, allmodel.SelectedInteruptionFile, allmodel.InteruptionStyle, allmodel.AvailableForDownload,
                    allmodel.ExplicitContent, allmodel.UploadImageFilePath, allmodel.UploadPDFFilePath, allmodel.PagePercentage, allmodel.Type, allmodel.PdfFilePath, allmodel.FileNames,
                    allmodel.VideoFilePath, allmodel.WaterMarkMatrixImagePath, allmodel.WaterMarkMatrixImageText, allmodel.VideoCategory, allmodel.RARFilePath, allmodel.MatrixImagePath,
                    allmodel.CreatotName,allmodel.CatID,allmodel.GenCloneID,System.DateTime.Now, allmodel.IsActive,model.TrackingNumber);
                var dat = context.tblInterruptedFiles.Where(m => (m.TrackingNumber == model.TrackingNumber && m.UserID == model.UserID)).SingleOrDefault();
                if (dat != null)
                {
                    dat.FileName = intmodel.FileName;
                    dat.InterruptFilePath = intmodel.InterruptedFilePath;
                    dat.VideoPath = intmodel.VideoPath;
                    dat.ModifiedDate = intmodel.ModifiedDate;
                    dat.CatID = model.CatID;
                    context.SaveChanges();
                }
                var data = context.tblCreateLinkPosts.Where(m =>( m.TrackingNo == model.TrackingNumber&&m.UserId==model.UserID)).SingleOrDefault();
                if (data!=null)
                {
                    data.Title = model.Title;
                    data.FileSize = allmodel.filesize;
                    context.SaveChanges();
                }
                scope.Complete();
                return true;
              
            }
        }

        public bool UpdatePremiumAccountInfo(PremiumAccountViewModel premium)
        {
            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {

                    //Update data in Bank Account Details
                    if (premium.BankAccountID.HasValue)
                    {
                        context.sp_UpdateBankAccountDetails(premium.BankAccountID, premium.CardNumber, premium.ExpiryMonth, premium.ExpiryYear, premium.CSV, premium.NameOnCard);
                    }
                    //Update data in Personal Address
                    if (premium.AddressID.HasValue)
                    {
                        context.sp_UpdatePersonAddress(premium.AddressID, premium.AddressLine1, premium.AddressLine2, premium.City, premium.Region, premium.PostalCode);
                    }
                    //Update data in Security Queastion Answer
                    if (premium.SecurityQuestionID.HasValue)
                    {
                        context.sp_UpdateSecurityQuestions(premium.SecurityQuestionID, premium.SecurityQuestion1, premium.Answer1, premium.SecurityQuestion2, premium.Answer2);
                    }
                    //Update data in AphidTise Table
                    // context.sp_UpdateBasicAccount(basicmodel.BasicUserID,basicmodel.UserName,basicmodel.FirstName,basicmodel.LastName,basicmodel.EmailAddress,basicmodel.DOB,basicmodel.Phone,basicmodel.AudioInterruptionFile,basicmodel.Watermarks,basicmodel.ProfilePictureInBytes,basicmodel.WebSiteUrl,basicmodel

                    //context.sp_UpdatePremiumAccount(premium.PremiumUserID, premium.ComposerName, premium.ProfilePictureInBytes, premium.FirstName, premium.LastName, premium.DOB, premium.Biography, premium.Website, premium.EmailAddress, premium.RecoveryEmail, premium.Phone);

                    context.sp_UpdateUsers(premium.ProfilePicturePath, premium.ProfilePictureServerId, premium.PremiumUserID);

                    if (premium.Audio1Name != null)
                    {
                        var prm = new System.Data.Objects.ObjectParameter("id", typeof(int));
                        context.sp_InsertUpdateAudioInteruptionFile(premium.PremiumUserID, premium.Audio1Path, premium.Audio1Name, premium.SelectedAudio1, prm, 2, premium.Audio1Name);
                    }
                    if (premium.Audio2Name != null)
                    {
                        var prm = new System.Data.Objects.ObjectParameter("id", typeof(int));
                        context.sp_InsertUpdateAudioInteruptionFile(premium.PremiumUserID, premium.Audio2Path, premium.Audio2Name, premium.SelectedAudio2, prm, 2, premium.Audio2Name);
                    }
                    if (premium.Audio3Name != null)
                    {
                        var prm = new System.Data.Objects.ObjectParameter("id", typeof(int));
                        context.sp_InsertUpdateAudioInteruptionFile(premium.PremiumUserID, premium.Audio3Path, premium.Audio3Name, premium.SelectedAudio3, prm, 2, premium.Audio3Name);

                    }

                    if (premium.DefaultSelectedAud == true || premium.Audio1Name != null || premium.Audio2Name != null || premium.Audio3Name != null)
                    {
                        var prm = new System.Data.Objects.ObjectParameter("id", typeof(int));
                        context.sp_InsertUpdateAudioInteruptionFile(premium.PremiumUserID, "Default", "Default", premium.DefaultSelectedAud, prm, 2, "Default");

                    }

                    if (premium.DefaultSelectedImg == true || premium.Image1Name != null || premium.Image2Name != null || premium.Image3Name != null)
                    {
                        var prm = new System.Data.Objects.ObjectParameter("id", typeof(int));
                        context.sp_InsertUpdateImageInteruptionFile(premium.PremiumUserID, premium.DefaultImageByte, "Default", premium.DefaultSelectedImg, prm, 2, "Default");

                    }

                    if (premium.Image1Name != null)
                    {
                        var prm = new System.Data.Objects.ObjectParameter("id", typeof(int));
                        context.sp_InsertUpdateImageInteruptionFile(premium.PremiumUserID, premium.Image1Path, premium.Image1Name, premium.SelectedImage1, prm, 2, premium.Image1Name);
                    }
                    if (premium.Image2Name != null)
                    {
                        var prm = new System.Data.Objects.ObjectParameter("id", typeof(int));
                        context.sp_InsertUpdateImageInteruptionFile(premium.PremiumUserID, premium.Image2Path, premium.Image2Name, premium.SelectedImage2, prm, 2, premium.Image2Name);
                    }
                    if (premium.Image3Name != null)
                    {
                        var prm = new System.Data.Objects.ObjectParameter("id", typeof(int));
                        context.sp_InsertUpdateImageInteruptionFile(premium.PremiumUserID, premium.Image3Path, premium.Image3Name, premium.SelectedImage3, prm, 2, premium.Image3Name);
                    }
                    tranScope.Complete();
                    return true;
                }
            }
            catch { throw; }
        }

        public List<CreateLinkPostModel> GetSearchRecord(Guid userId, string Title, string Category)
        {
            List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
            try
            {
                var data = context.tblCreateLinkPosts.Where(m => m.UserId == userId && m.Category == Category).ToList();

                if (data != null)
                {

                    foreach (var item in data)
                    {
                        if (item.Title != null)
                        {
                            if (item.Title.Contains(Title))
                            {
                                if (item.IsDelete == false)
                                {
                                    li.Add(new CreateLinkPostModel()
                                    {
                                        Title = item.Title,
                                        Channel = item.Channel,
                                        NoOfChannel = item.NoOfClones,
                                        Views = item.Views,
                                        Downloads = item.Downloads,
                                        FileSize = item.FileSize,
                                        TrackingNumber = item.TrackingNo,
                                        Date = item.PostedDate,
                                        Category = item.Category,
                                        MatrixImagePath = item.MatrixImagePath
                                    });
                                }

                            }
                        }
                    }
                }
                return li;
            }
            catch { throw; }
        }

        public List<CreateLinkPostModel> GetPostData(Guid userid)
        {
            List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
            try
            {
                var data = context.tblCreateLinkPosts.Where(m => m.UserId == userid);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        if (item.IsDelete == false)
                        {
                            li.Add(new CreateLinkPostModel()
                            {
                                Title = item.Title,
                                Channel = item.Channel,
                                NoOfChannel = item.NoOfClones,
                                Views = item.Views,
                                Downloads = item.Downloads,
                                FileSize = item.FileSize,
                                TrackingNumber = item.TrackingNo,
                                Date = item.PostedDate,
                                Category = item.Category,
                                ChannelStatus = item.ChannelStatus,
                                MatrixImagePath = item.MatrixImagePath
                            });
                        }
                    }
                }
            }
            catch { throw; }
            return li;
        }
        public List<Bytetracker> expand(Guid userID, string Trackingnumber)
        {
            List<Bytetracker> li = new List<Bytetracker>();
            try
            {
                var data = context.tblCreateLinkPosts.Where(m => m.UserId == userID && m.TrackingNo == Trackingnumber).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        li.Add(new Bytetracker()
                        {
                            Title = item.Title,
                            Channel = item.Channel,
                            NoOfclones = item.NoOfClones,
                            Views = item.Views,
                            Downloads = item.Downloads,
                            FileSize = item.FileSize,
                            TrackingNumber = item.TrackingNo,
                            Date = item.PostedDate,
                            Category = item.Category
                        });
                    }
                }
            }
            catch { throw; }
            return li;
        }
        public List<Bytetracker> GetPostData1(Guid userid)
        {
            List<Bytetracker> li = new List<Bytetracker>();
            try
            {
                var data = context.tblCreateLinkPosts.Where(m => m.UserId == userid).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        if (item.IsDelete!=true)
                        {
                            li.Add(new Bytetracker()
                            {
                                Title = item.Title,
                                Channel = item.Channel,
                                NoOfclones = item.NoOfClones,
                                Views = item.Views,
                                Downloads = item.Downloads,
                                FileSize = item.FileSize,
                                TrackingNumber = item.TrackingNo,
                                Date = item.PostedDate,
                                Category = item.Category,
                                ChannelStatus = item.ChannelStatus,
                                MatrixImagePath = item.MatrixImagePath
                            });
                        }
                    }
                }
            }
            catch { throw; }
            return li;
        }
        public List<CreateLinkPostModel> AtoZ(Guid userId, string Category, string order)
        {
            string oo = order;
            if (order == "A to Z")
            {
                oo = "ASC";
            }
            else { oo = "DES"; }
            try
            {
                List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
                var data = context.sp_Sorting(Category, userId, oo);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        li.Add(new CreateLinkPostModel()
                        {
                            Title = item.Title,
                            Channel = item.Channel,
                            NoOfChannel = item.NoOfClones,
                            Views = item.Views,
                            Downloads = item.Downloads,
                            FileSize = item.FileSize,
                            TrackingNumber = item.TrackingNo,
                            Date = item.PostedDate,
                            Category = item.Category,
                           
                        });

                    }

                } return li;
            }
            catch { throw; }
            
        }

        public PremiumGenerateCloneModel EditClone(string trackNo)
        {
            // var data = context.sp_EditPremiumCloneModel(trackNo).ToList();
            try
            {
                var Udata = context.tblCreateLinkPosts.Where(m => m.TrackingNo == trackNo).SingleOrDefault();
                Guid? uid = null;
                PremiumGenerateCloneModel ob = new PremiumGenerateCloneModel();
                if (Udata != null)
                {
                    uid = Udata.UserId;

                }
                var data = context.tblPremiumGeterateClones.Where(m => m.UserID == uid && m.TrackingNumber == trackNo).SingleOrDefault();
                if (data != null)
                {

                    ob.AlbumTitle = data.AlbumTitle;
                    ob.ArtistName = data.ArtistName;
                    ob.AvailableDownload = data.AvailableForDownload;
                    ob.CloneID = data.CloneID;
                    ob.Composer = data.ComposerName;
                    ob.CreatorName = data.CreatorName;
                    ob.ExplicitContent = data.ExplicitContent;
                    ob.InterruptionStyle = data.InterruptionStyle;
                    ob.MatrixImageBytePath = data.MatrixImagePath;
                    ob.PagePercentage = data.PagePercentage;
                    ob.PdfFilePath = data.PDFFilePath;
                    ob.Producer = data.Producer;
                    ob.Publisher = data.Publisher;
                    //ob.RarFilePath = item.RARFile;
                    ob.RarFilePath = data.RARFilePAth;
                    ob.SelectedIntFile = data.SelectedInterruptionFile;
                    ob.Tags = data.Tags;
                    ob.Title = data.Title;
                    ob.TrackingNumber = data.TrackingNumber;
                    ob.Type = data.Type;
                    // ob.UploadAudioPath = item.AudioFile;
                    ob.UploadImagePath = data.ImageFile;
                    ob.UserID = data.UserID;
                    ob.VideoFile = data.VideoFilePath;
                    ob.UploadAudioPath = data.AudioFilePath;
                    ob.FileLength = data.Total_Length;
                    ob.CatID = data.CatID;
                }
                return ob;
            }
            catch { throw; }
        }

        public ByteArray GetByteArray(string trackNo)
        {
            ByteArray ob = new ByteArray();
            try
            {
                var data = context.tblBasicGenerateClones.Where(m => m.TrackingNumber == trackNo).SingleOrDefault();
                if (data != null)
                {
                    ob.Audio = data.UploadFileAudioPath;
                    ob.Image = data.MatrixImagePath;
                }
                var data1 = context.tblInterruptedFiles.Where(m => m.TrackingNumber == trackNo).SingleOrDefault();
                if (data1 != null)
                {
                    ob.Intrepputed = data1.InterruptFilePath;
                    ob.VideoPath = data1.VideoPath;
                }
                var data2 = context.tblCreateLinkPosts.Where(m => m.TrackingNo == trackNo).SingleOrDefault();
                if (data2 != null)
                {
                    ob.FileSize = data2.FileSize;
                }
                return ob;
            }
            catch { throw; }
        }
        public bool ByteYourFile(PremiumGenerateCloneModel model, InterruptedFileModel intf, CreateLinkPostModel post, AllGenerateCloneModel Alldata)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_PremiumGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                        model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Producer, model.Publisher,
                        model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent,
                        model.Type, model.PdfFilePath, model.VideoFile, model.PagePercentage, model.RarFilePath, model.MatrixImageBytePath,
                        model.CreatorName, model.TrackingNumber,model.FileLength,model.CatID);


                    context.sp_InterruptedFiles(intf.UserId, intf.CloneId, intf.InterruptedFilePath, intf.CreateDate, intf.ModifiedDate, intf.IsActive, intf.FileName, intf.VideoPath, intf.TrackNumber,intf.CatId);

                    context.sp_CreateLinkPost(post.Title, post.Channel, post.NoOfChannel, post.Views, post.Downloads, post.FileSize, post.TrackingNumber, post.Date, post.Category,  post.UserID, post.MatrixImagePath);
                    context.sp_AllGenerateClones(Alldata.UserID, Alldata.CloneId, Alldata.Title, Alldata.Tag, Alldata.ArtistName, Alldata.AlbumTitle, Alldata.AudioFilePath, Alldata.UploadFilePath, Alldata.MatrixFilePath, Alldata.ComposerName, Alldata.Producer, Alldata.Publisher,
                     Alldata.SelectedInteruptionFile, Alldata.InteruptionStyle, Alldata.AvailableForDownload, Alldata.ExplicitContent, Alldata.UploadImageFilePath, Alldata.UploadPDFFilePath, Alldata.PagePercentage, Alldata.Type, Alldata.PdfFilePath, Alldata.FileNames,
                     Alldata.VideoFilePath, Alldata.WaterMarkMatrixImagePath, Alldata.WaterMarkMatrixImageText, Alldata.VideoCategory, Alldata.RARFilePath, Alldata.MatrixImagePath, Alldata.CreatotName, Alldata.TrackingNumber, Alldata.CatID, Alldata.GenCloneID,
                     Alldata.CreatedDate, Alldata.ModifyDate, Alldata.IsActive);
                    trans.Complete();
                    return true;
                }
            }
            catch { throw; }
        }

        public DataPlanDetail DataPlanDetail(Guid UserId)
        {
            DataPlanDetail model = new DataPlanDetail();
            try
            {
                var data = context.tblDataStoragePlans.Where(m => m.UserID == UserId).SingleOrDefault();
                if (data != null)
                {
                    var plan = DataPlans.GetFromId(data.StoragePlan);
                    var free = Convert.ToInt64(data.FreeSpace); 
                    var used = Convert.ToInt64(data.UsedSpace); 

                    model.PlanId = data.StoragePlan;
                    model.PlanDescription = plan.StorageAmount.Gigabytes().ToString(); 
                    model.Expires = data.ExpireDate;
                    model.Free = free;
                    model.Used = used;
                }

                return model;
            }
            catch { throw; }
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
            int Playlist = context.tbl_PlayList.Where(m => m.UserID == UserID).Select(m => m.PlaylistName).Count();
            int count = context.tbl_PlayList.Where(m => (m.PlaylistName == PlaylistName) && (m.UserID == UserID)).Count();
            try
            {
                if(count==0)
                {
               
                tblAllGenerateClone fetch_record = context.tblAllGenerateClones.Where(m => (m.TrackingNumber == TrackingID) && (m.UserID == UserID)).SingleOrDefault();
                if (fetch_record != null)
                {
                    if (Playlist < 20)
                    {
                        tbl_PlayList tab = new tbl_PlayList();
                        tab.PlaylistName = PlaylistName;
                        tab.UserID = UserID;
                        tab.TrackingID = TrackingID;
                        tab.FileName = fetch_record.FileNames;
                        tab.Composer = fetch_record.ComposerName;
                        context.tbl_PlayList.Add(tab);
                        context.SaveChanges();
                        addStatus = true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            else
            {
                addStatus = false;
            }



            }
            catch { throw; }
            return addStatus;
        
        }
         

        public AllGenerateCloneModel Get_A_Record_via_trackID(string TrackingNumber)
        {
            AllGenerateCloneModel record = new AllGenerateCloneModel();
            try
            {
                var data = context.tblAllGenerateClones.Where(m => (m.TrackingNumber == TrackingNumber)).SingleOrDefault();
                record.TrackingNumber = data.TrackingNumber;
                record.AlbumTitle = data.AlbumTitle;
                record.ArtistName = data.ArtistName;
                record.AudioFilePath = data.AudioFilePath;
                record.MatrixImagePath = data.MatrixImagePath;
                record.CatID = data.CatID;
                record.FileNames = data.FileNames;

                return record;
            }
            catch { throw; }
        }



        public List<PremiumGenerateCloneModel> Fileprivew(string trackingNumber)
        {
            List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();

            try
            {
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
                li.AdTypeID = int.Parse(data.AdTypeID.ToString());
            }
            catch
            {
                li = null;
            }
            return li;
        }

        public bool deleteAccount(Guid userid)
        {
            bool IsSuccess = false;
            try
            {
                var data = context.tblPremiumAccounts.Where(m => m.PremiumUserID == userid).SingleOrDefault();
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

        public bool InsertChannelBiblography(Guid userid, string data, string userimage, string UserName)
        {
            try
            {
                Guid guis = Guid.NewGuid();
                var Userdata = context.tblChannelPages.Where(m => m.UserID == userid).SingleOrDefault();
                if (Userdata != null)
                {
                    if (data != null)
                    {
                        Userdata.ChannelBiography = data;
                    }

                    Userdata.ModifiedDate = System.DateTime.Now;
                    if (userimage != null)
                    {
                        Userdata.ChannelImagePath = userimage;
                    }
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
      





        public List<AllGenerateCloneModel> GetUploadfile(string track)
        {
            var data = context.tblAllGenerateClones.Where(m => m.TrackingNumber == track).ToList();
            string data1 = context.tblCreateLinkPosts.Where(m => m.TrackingNo == track).Select(m => m.FileSize).Single();
            List<AllGenerateCloneModel> Li = new List<AllGenerateCloneModel>();
            try
            {
                if (data.Count != 0)
                {
                    foreach (var item in data)
                    {
                        Li.Add(new AllGenerateCloneModel()
                        {
                            Title = item.Title,
                            UploadFilePath = item.UploadFilePath,
                            AlbumTitle = item.AlbumTitle,
                            AudioFilePath = item.AudioFilePath,

                            UploadImageFilePath = item.UploadImageFilePath,
                            UploadPDFFilePath = item.UploadPDFFilePath,
                            PdfFilePath = item.PdfFilePath,
                            FileNames = item.FileNames,
                            VideoFilePath = item.VideoFilePath,
                            CatID = item.CatID,
                            RARFilePath = item.RARFilePath,
                            Tag = item.Tag,
                            VideoCategory = item.VideoCategory,
                            filesize = data1
                        });
                    }
                }

                return Li;
            }
            catch { throw; }
        }

        public ChannelModel ShowChannelData(Guid UserId)
        {
            try
            {
                List<string> li = new List<string>();
                List<string> track = new List<string>();
                ChannelModel model = new ChannelModel();
                var imgdata = context.tblCreateLinkPosts.Where(m => m.ChannelStatus == true && m.UserId == UserId).Select(m => m.TrackingNo).ToList();
                var data = context.tblChannelPages.Where(m => m.UserID == UserId).SingleOrDefault();
                if (data != null)
                {
                    model.UserData = data.ChannelBiography;
                    model.ImagePath = data.ChannelImagePath;
                    model.ChannelId = data.ChannelID;
                    model.UserName = data.UserName;
                    model.UserID = data.UserID;
                }

                if (imgdata != null)
                {
                    foreach (var item in imgdata)
                    {
                        track.Add(item);
                        var imgpath = context.tblAllGenerateClones.Where(m => m.TrackingNumber == item).Select(m => m.MatrixImagePath).SingleOrDefault();
                        li.Add(imgpath);

                    }
                }
                for (int i = 0; i < li.Count; i++)
                {

                    model.GetType().GetProperty("Image" + i).SetValue(model, li[i]);
                    // model.Image1 = li[i];
                }
                for (int i = 0; i < track.Count; i++)
                {
                    model.GetType().GetProperty("TrackingNumber" + i).SetValue(model, track[i]);
                    // model.Image1 = li[i];
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UnsubscribeChannel(Guid Userid, Guid ChannelId)
        {
            try
            {
                var data = context.tbl_ChannelSubscription.Where(m => m.ChannelID == ChannelId && m.ByterUserId == Userid).SingleOrDefault();
                if (data != null)
                {
                    data.Status = false;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { throw; }
        }
        public string GetChannelInfo(Guid user)
        {
            string channelid = "";
            try
            {
                var data = context.tblChannelPages.Where(m => m.UserID == user).Select(m => m.ChannelID).SingleOrDefault();
                if (data != null)
                {
                    channelid = data.ToString();
                }
                return channelid;
            }
            catch { throw; }
        }

        public ChannelModel LoginUserSubscription(ChannelModel model)
        {
            try
            {
                var data = context.tbl_ChannelSubscription.Where(m => m.ChannelID == model.ChannelId && m.ByterUserId == model.UserID).SingleOrDefault();
                if (data == null)
                {
                    context.sp_ChannelSubscription(model.UserID, model.premiumUserId, "", "", true, System.DateTime.Now, model.ChannelId);
                    var channeldata = context.tblChannelPages.Where(m => m.UserID == model.premiumUserId).SingleOrDefault();
                    if (channeldata != null)
                    {
                        model.ImagePath = channeldata.ChannelImagePath;
                        model.UserData = channeldata.ChannelBiography;
                        model.UserName = channeldata.UserName;
                        model.SubscriptionStatus = true;
                        model.ChannelId = channeldata.ChannelID;
                        model.UserID = model.UserID;
                        model.premiumUserId = model.premiumUserId;
                    }

                }
                else if (data.Status == false)
                {
                    data.Status = true;
                    model.SubscriptionStatus = true;
                    context.SaveChanges();
                    //var channeldata = context.tblChannelPages.Where(m => m.UserID == model.premiumUserId).SingleOrDefault();
                    //if (channeldata != null)
                    //{
                    //    model.ImagePath = channeldata.ChannelImagePath;
                    //    model.UserData = channeldata.ChannelBiography;
                    //    model.UserName = channeldata.UserName;
                    //    model.SubscriptionStatus = true;
                    //    model.ChannelId = channeldata.ChannelID;
                    //    model.UserID = model.UserID;
                    //    model.premiumUserId = model.premiumUserId;
                    //}
                }
                return model;
            }
            catch { throw; }
        }
        public int totalplaylist(Guid userID)
        {
            var data = 0;
            try
            {
                data = context.tbl_PlayList.Where(m => (m.UserID == userID)).Select(m => m.PlaylistName).Distinct().Count();
            }
            catch (Exception e)
            {

            }
            return data;
        }


        public bool AddToChannel(string trackno, Guid UserID)
        {
            try
            {
                var data = context.tblCreateLinkPosts.Where(m => m.UserId == UserID && m.TrackingNo == trackno).SingleOrDefault();
                if (data != null)
                {
                    data.ChannelStatus = true;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public PostingDataModel GetUploadfiles(string track)
        {
            try
            {
                var data = context.sp_PostingDataMaterial(track).SingleOrDefault();
                var credit = context.tblCreditDetails.Where(m => m.Category == data.Category).Select(m => m.Credit).ToList();
                //var data1 = context.tblAllGenerateClones.Where(m => m.TrackingNumber == track).SingleOrDefault();
                PostingDataModel li = new PostingDataModel();
                if (data != null)
                {
                    li.Title = data.Title;
                    li.InterruptedFilePath = data.InterruptFilePath;
                    li.NoOfChannel = data.NoOfClones;
                    li.VideoPath = data.VideoPath;
                    li.Channel = data.Channel;
                    li.Category = data.Category;
                    li.Downloads = data.Downloads;
                    li.Views = data.Views;
                    li.FileSize = data.FileSize;
                    li.TrackingNumber = data.TrackingNo;
                    li.MatrixImagePath = data.MatrixImagePath;
                    li.ComposerName = data.ComposerName;

                }

                return li;
            }
            catch { throw; }
        }

        //public bool GetSubscribeUsers(string composer, Guid user, string title, string cat, string channel, string Path, string TrackingNumber)
        //{

        //    bool Insert = false;
        //    Guid channel1 = new Guid(channel);
        //    var Info = context.tbl_SendLinkToMT.Select(m => m.TrackingNumber).ToList();
        //    for (int i = 0; i < Info.Count; i++)
        //    {
        //        if (Info[i] == TrackingNumber)
        //        {
        //            return false;
        //        }

        //    }

        //    var data = new tbl_SendLinkToMT { ID = 0, Category = cat, ChannelId = channel1, ComposerName = composer, Credits = 0, Title = title, UserId = user, Path = Path, TrackingNumber = TrackingNumber };
        //    context.tbl_SendLinkToMT.Add(data);
        //    context.SaveChanges();
        //    return true;

        //    try
        //    {
        //        string pic = "";
        //        var profilepic = context.tblAllGenerateClones.Where(m => m.TrackingNumber == TrackingNumber).Select(m => m.MatrixImagePath).SingleOrDefault();
        //        if (profilepic != null)
        //        {
        //            pic = profilepic;
        //        }
        //        bool Insert = false;
        //        Guid channel1 = new Guid(channel);
        //        var data = new tbl_SendLinkToMT { ID = 0, Category = cat, ChannelId = channel1, ComposerName = composer, Credits = 0, Title = title, UserId = user, Path = Path, TrackingNumber = TrackingNumber, LinkToPostM = false, PremiumProfilePic = pic, MessageStatus = false };
        //        context.tbl_SendLinkToMT.Add(data);
        //        context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }




        //}







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
        public List<favourites> GetFavourites(Guid userID)
        {
            List<favourites> li = new List<favourites>();
            try
            {
                var data = context.tblFavourites.Where(m => (m.UserID == userID)).Select(m => new { m.TrackingNumber, m.FileName }).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        favourites rec = new favourites();
                        rec.TrackingNumber = item.TrackingNumber;
                        rec.FileName = item.FileName;
                        li.Add(rec);
                    }
                }
            }
            catch { throw; }
            return li;
        }

        public bool DelfromFav(string TrackingID, Guid userID)
        {
            bool deleteStatus = false;
            try
            {
                tblFavourite record_fav = context.tblFavourites.Where(m => (m.TrackingNumber == TrackingID) && (m.UserID == userID)).SingleOrDefault();
                if (record_fav != null)
                {
                    context.tblFavourites.Remove(record_fav);
                    if (context.SaveChanges() > 0)
                        deleteStatus = true;
                }
                return deleteStatus;
            }
            catch { throw; }
        }


      


        public List<Bytetracker> Getpostingdata(Guid userID, string Trackingnumber)
        {

            List<Bytetracker> li = new List<Bytetracker>();
            try
            {
                var data = context.tblCreditDetails.Where(m => m.TrackingID == Trackingnumber).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {

                        li.Add(new Bytetracker()
                        {


                            Title = item.Title,
                            Channel = item.Channel,
                            FileSize = item.File_Size,
                            TrackingNumber = item.TrackingID,
                            Date = item.CreatedDate,
                            Category = item.Category,
                            poststatus = item.IsActive.ToString(),

                        });
                    }
                }
                return li;
            }
            catch { throw; }
            
        }

        public List<PlaylistModel> GetSongListmusic(Guid userID, string playName)
        {
            List<PlaylistModel> li = new List<PlaylistModel>();
            try
            {
                var data = context.tbl_PlayList.Where(m => (m.UserID == userID) && (m.PlaylistName == playName)).Select(m => new { m.UserID, m.TrackingID, m.FileName, m.PlaylistName, m.Composer, m.CatId }).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        li.Add(new PlaylistModel()
                        {
                            Composer = item.Composer,
                            FileName = item.FileName,
                            PlaylistName = item.PlaylistName,
                            TrackingID = item.TrackingID,
                            UserID = item.UserID,
                            CatId = item.CatId,
                        });
                    }
                }
            }
            catch { throw; }
            return li;
        }

        public bool UpdatPlaylist(string PlaylistName, string TrackingID, Guid UserID)
        {
            
            try
            {
                var data = context.tbl_PlayList.FirstOrDefault(m => (m.UserID == UserID) && (m.TrackingID == TrackingID));
                if (data != null)
                {
                    data.PlaylistName = PlaylistName;
                    if ( context.SaveChanges()>0)
                    {
                        return true;
                    }
                }
            }
            catch { throw; }
            return false;
        }
        /// <summary>
        /// Verifies mail account
        /// </summary>
        /// <param name="usid"></param>
        /// <returns></returns>
        public bool VerifyMailAccount(Guid usid)
        {
            return context.sp_VerifyPremiumAccount(usid, true) >= 0 ? true : false;


        }
    }
}
