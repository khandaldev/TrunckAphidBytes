using System.Data.Entity.Migrations;
using System.Net.Sockets;
using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.IO;
using System.Reflection;
using static AphidBytes.Core.PaymentServices.StripePackages;
using AphidBytes.Core.Extensions;

namespace AphidTise.Entity.Repository
{
    public class RepositoryBasic : GenericRepository<tblBasicAccount>
    {
        public sp_GetBasicAccountInfo_Result GetBasicAccountInfo(Guid userID)
        {
            try
            {
                return context.sp_GetBasicAccountInfo(userID).SingleOrDefault();
            }
            catch { throw; }
        }

        public bool DeleteAudioBasic(Guid userid)
        {
            bool deleteStatus = false;
            try
            {
                tblAudioInterruption audDelete = context.tblAudioInterruptions.Where(m => m.UserId == userid && m.FileName != "Default").SingleOrDefault();
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

        public List<AllGenerateCloneModel> GetUploadfile(string track)
        {
            try
            {
                var data = context.tblAllGenerateClones.Where(m => m.TrackingNumber == track).ToList();
                string data1 = context.tblCreateLinkPosts.Where(m => m.TrackingNo == track).Select(m => m.FileSize).Single();
                List<AllGenerateCloneModel> Li = new List<AllGenerateCloneModel>();
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

        public bool ByteYourFile(BasicGenerateCloneModel model, InterruptedFileModel mm, CreateLinkPostModel ll, AllGenerateCloneModel Alldata)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {

                    context.sp_BasicGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                           model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Publisher, model.Producer,
                           model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent,
                           model.UploadImagePath, model.PdfFilePath, model.PagePercentage, model.WatermarkMatrixImagePath,
                           model.WatermarkMatrixImageText, model.VideoCategory, model.TrackingNumber, model.VideoPath, model.RarPath,model.TotalLength,model.CatId,model.UploadAudio2Path);

                    context.sp_InterruptedFiles(mm.UserId, mm.CloneId, mm.InterruptedFilePath, mm.CreateDate, mm.ModifiedDate, mm.IsActive, mm.FileName, mm.VideoPath, mm.TrackNumber,model.CatId);

                    context.sp_CreateLinkPost(ll.Title, ll.Channel, ll.NoOfChannel, ll.Views, ll.Downloads, ll.FileSize, ll.TrackingNumber, ll.Date, ll.Category, ll.UserID, ll.MatrixImagePath);
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

        public bool InsertPhotoArt(BasicGenerateCloneModel model, InterruptedFileModel mm, CreateLinkPostModel ll, AllGenerateCloneModel Alldata)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_BasicGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                       model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Publisher,
                       model.Producer, model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload,
                       model.ExplicitContent, model.UploadImagePath, model.PdfFilePath, model.PagePercentage,
                       model.WatermarkMatrixImagePath, model.WatermarkMatrixImageText, model.VideoCategory, model.TrackingNumber, model.VideoPath, model.RarPath, model.TotalLength,model.CatId,model.UploadAudio2Path);

                    context.sp_InterruptedFiles(mm.UserId, mm.CloneId, mm.InterruptedFilePath, mm.CreateDate, mm.ModifiedDate, mm.IsActive, mm.FileName, mm.VideoPath, mm.TrackNumber,model.CatId);

                    context.sp_CreateLinkPost(ll.Title, ll.Channel, ll.NoOfChannel, ll.Views, ll.Downloads, ll.FileSize, ll.TrackingNumber, ll.Date, ll.Category, ll.UserID, ll.MatrixImagePath);
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

        private void FillGenerateData(AllGenerateCloneModel Source, tblAllGenerateClone target)
        {
            foreach (PropertyInfo propertyinfo in Source.GetType().GetProperties())
            {
                propertyinfo.SetValue(target, propertyinfo.GetValue(Source));
            }
        }
       
        public void UpdateBasicData(AllGenerateCloneModel model,BasicGenerateCloneModel bcmodel,InterruptedFileModel intmodel)
        {
            var data = context.tblAllGenerateClones.Where(m => (m.UserID == model.UserID && m.TrackingNumber == model.TrackingNumber)).SingleOrDefault() ;
            FillGenerateData(model, data);
            context.SaveChanges();

            
        }


        public string GetWebSite(Guid Userid)
        {
            try
            {
                var data = context.tblBasicAccounts.Where(m => m.BasicUserID == Userid).SingleOrDefault();
                if (data != null)
                {
                    var name = context.tblBasicAccounts.Where(m => m.BasicUserID == Userid).Select(m => m.WebSiteUrl).SingleOrDefault();
                    return name;
                }
                else
                {
                    var name = context.tblPremiumAccounts.Where(m => m.PremiumUserID == Userid).Select(m => m.Website).SingleOrDefault();
                    return name;
                }
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

        public List<BasicGenerateCloneModel> Fileprivew(string trackingNumber)
        {
            List<BasicGenerateCloneModel> li = new List<BasicGenerateCloneModel>();

            //var data = context.tblBasicGenerateClones.Where(m => m.TrackingNumber == trackingNumber).ToList();
            // //var data1 = context.tblInterruptedFiles.Where(m => m.TrackingNumber == trackingNumber).ToList();


            ////var customers = from customer in context.tblAccounts
            ////                join assoc in context.tblAccountAssociations on customer.AccountCode equals assoc.ChildCode
            ////                where customer.AccountType == "S" || customer.AccountType == "P"
            ////                select customer, assoc;
            try
            {
                var data = (from pd in context.tblBasicGenerateClones
                            join od in context.tblInterruptedFiles on pd.TrackingNumber equals od.TrackingNumber
                            where pd.TrackingNumber == trackingNumber

                            select new
                            {
                                pd.Title,
                                pd.AlbumTitle,
                                pd.ExplicitContent,
                                pd.ArtistName,
                                pd.Composer,
                                pd.AvailableForDownload,
                                pd.TrackingNumber,
                                pd.MatrixImagePath,
                                pd.UploadFileImagePath,
                                od.VideoPath,
                                pd.UploadFileAudioPath,
                                // pd.tblInterruptedBasicAudioFile,
                                od.InterruptFilePath,
                                pd.UploadFilePDFPath
                            }).ToList();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        li.Add(new BasicGenerateCloneModel()
                        {

                            Title = item.Title,
                            AlbumTitle = item.AlbumTitle,
                            ExplicitContent = item.ExplicitContent,
                            ArtistName = item.ArtistName,
                            Composer = item.Composer,
                            AvailableDownload = item.AvailableForDownload,
                            TrackingNumber = item.TrackingNumber,
                            MatrixImageBytePath = item.MatrixImagePath,
                            UploadImagePath = item.UploadFileImagePath,
                            VideoFile = item.VideoPath,
                            UploadAudioPath = item.UploadFileAudioPath,
                            Interruptedfile = item.InterruptFilePath,
                            UploadFilePDFPath = item.UploadFilePDFPath


                        });
                    }
                }

                return li;
            }
            catch { throw; }
        }

        public List<CreateLinkPostModel> getPostData(Guid userId)
        {
            List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
            try
            {
                var data = context.tblCreateLinkPosts.Where(m => m.UserId == userId);
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
                                MatrixImagePath = item.MatrixImagePath
                            });
                        }
                       
                    }
                }
                return li;
            }
            catch { throw; }
        }
        public List<Bytetracker> GetPostData1(Guid userId)
        {
          
            List<Bytetracker> li = new List<Bytetracker>();
            try
            {
                var data = context.tblCreateLinkPosts.Where(m => m.UserId == userId);
                
            
                if (data != null)
                {
                    foreach (var item in data)

                    {
                       if (item.IsDelete != true)
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
                                MatrixImagePath = item.MatrixImagePath
                               
                              
                            });
                        }
                    }
                }
                return li;
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
                            if (item.IsDelete==false)
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


        public List<CreateLinkPostModel> AtoZ(Guid userId, string Category, string order)
        {
            string oo = order;
            if (order == "A to Z")
            {
                oo = "ASC";
            }
            else { oo = "DES"; }
            List<CreateLinkPostModel> li = new List<CreateLinkPostModel>();
            try
            {
                var data = context.sp_Sorting(Category, userId, oo).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        if (item.IsDelete==false)
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
                                Category = item.Category
                            });

                        }
                       
                    }

                }
                return li;
            }
            catch { throw; }
        }
        public List<Bytetracker> expand(Guid userId, string Trackingnumber)
        {
            List<Bytetracker> li = new List<Bytetracker>();
            try
            {
                var data = context.tblCreateLinkPosts.Where(m => m.UserId == userId && m.TrackingNo == Trackingnumber).ToList();
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
                            Category = item.Category,
                            MatrixImagePath = item.MatrixImagePath
                        });
                    }
                }
                return li;
            }
            catch { throw; }
        }
        public BasicGenerateCloneModel EditClone(string track)
        {
             try
            {
               
            BasicGenerateCloneModel ob = new BasicGenerateCloneModel();
            var Udata = context.tblCreateLinkPosts.Where(m => m.TrackingNo == track).SingleOrDefault();
            Guid? uid = null;
            if (Udata != null)
            {
                uid = Udata.UserId;

            }
            var data = context.tblBasicGenerateClones.Where(m => m.UserID == uid && m.TrackingNumber == track).SingleOrDefault();
            if (data != null)
                {
                    
                        ob.AlbumTitle = data.AlbumTitle;
                        ob.ArtistName = data.ArtistName;
                        ob.AvailableDownload = data.AvailableForDownload;
                        ob.CloneID = data.CloneID;
                        ob.Composer = data.Composer;
                        ob.VideoPath = data.VideoPath;
                        ob.RarFilePath = data.RarPath;
                        ob.ExplicitContent = data.ExplicitContent;
                        //ob.InterruptedAudio = data[i].InterruptedAudio;
                        ob.InterruptionStyle = data.InterruptionStyle;
                        ob.MatrixImageBytePath = data.MatrixImagePath;
                        ob.PagePercentage = data.PagePercentage;
                        ob.PdfFilePath = data.UploadFilePDFPath;
                        ob.Producer = data.Producer;
                        ob.Publisher = data.Publisher;
                        // ob.RarFile = data[i].ra;
                        ob.SelectedIntFile = data.SelectIntFile;
                        //ob.SongName = data[i].so;
                        ob.Tags = data.Tags;
                        ob.Title = data.Title;
                        ob.TrackingNumber = data.TrackingNumber;
                        //ob.Type = data[i].ty;
                        ob.UploadAudioPath = data.UploadFileAudioPath;
                        ob.UploadImagePath = data.UploadFileImagePath;
                        ob.VideoCategory = data.VideoCategory;
                        ob.TotalLength = data.TotalLength;
                    }
                  return ob;
                }
        
         
            catch { throw; }
        }

        public bool UpdateClone(BasicGenerateCloneModel model, InterruptedFileModel intmodel, AllGenerateCloneModel allmodel)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_UpdateBasicCloneModel(model.UserID, model.Title, model.Tags, model.ArtistName, model.Title,
                        model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Publisher, model.Producer,
                        model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent,
                        model.UploadImagePath, model.PdfFilePath, model.PagePercentage, model.WatermarkMatrixImagePath,
                        model.WatermarkMatrixImageText, model.VideoCategory, model.TrackingNumber, model.TotalLength, model.Modified_Time
                        , model.VideoPath, model.RarPath);

                    context.sp_UpdateAllGenerateClone(allmodel.UserID, allmodel.CloneId, allmodel.Title, allmodel.Tag, allmodel.ArtistName, allmodel.AlbumTitle, allmodel.AudioFilePath, allmodel.UploadFilePath,
                   allmodel.MatrixFilePath, allmodel.ComposerName, allmodel.Producer, allmodel.Publisher, allmodel.SelectedInteruptionFile, allmodel.InteruptionStyle, allmodel.AvailableForDownload,
                   allmodel.ExplicitContent, allmodel.UploadImageFilePath, allmodel.UploadPDFFilePath, allmodel.PagePercentage, allmodel.Type, allmodel.PdfFilePath, allmodel.FileNames,
                   allmodel.VideoFilePath, allmodel.WaterMarkMatrixImagePath, allmodel.WaterMarkMatrixImageText, allmodel.VideoCategory, allmodel.RARFilePath, allmodel.MatrixImagePath,
                   allmodel.CreatotName, allmodel.CatID, allmodel.GenCloneID, System.DateTime.Now, allmodel.IsActive, model.TrackingNumber);
                    var dat = context.tblInterruptedFiles.Where(m => (m.TrackingNumber == model.TrackingNumber && m.UserID == model.UserID)).SingleOrDefault();
                    if (dat != null)
                    {
                        dat.FileName = intmodel.FileName;
                        dat.InterruptFilePath = intmodel.InterruptedFilePath;
                        dat.VideoPath = intmodel.VideoPath;
                        dat.ModifiedDate = intmodel.ModifiedDate;
                        dat.CatID =intmodel.CatId;
                        context.SaveChanges();
                    }
                    var data = context.tblCreateLinkPosts.Where(m => (m.TrackingNo == model.TrackingNumber && m.UserId == model.UserID)).SingleOrDefault();
                    if (data != null)
                    {
                        data.Title = model.Title;
                        data.FileSize = allmodel.filesize;
                        context.SaveChanges();
                    }
                    trans.Complete();
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

        public List<BindDropDown> BindDropAudio(Guid USerID)
        {
            List<BindDropDown> li = new List<BindDropDown>();
            try
            {
                var data = context.tblAudioInterruptions.Where(m => m.UserId == USerID).Select(m => m.FileName).ToString();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        li.Add(new BindDropDown()
                            {
                                Name = data
                            }
                            );
                    }

                }

                return li;
            }
            catch { throw; }
        }

        public bool DeleteBasicImage(Guid userid)
        {
            bool deleteStatus = false;
            try
            {
                tblWaterMarkUpInterruption imgDelete = context.tblWaterMarkUpInterruptions.Where(m => m.UserId == userid && m.WatermarkImageName != "Default").SingleOrDefault();
                if (imgDelete != null)
                {
                    //imgDelete.ImageInterruption = null;
                    context.tblWaterMarkUpInterruptions.Remove(imgDelete);
                    if (context.SaveChanges() > 0)
                        deleteStatus = true;
                }
                return deleteStatus;
            }
            catch { throw; }
        }

        public bool InsertBasicSingleMusic(BasicGenerateCloneModel model, InterruptedFileModel intModel, CreateLinkPostModel post, AllGenerateCloneModel Alldata)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_BasicGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                         model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Publisher, model.Producer,
                         model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent,
                         model.UploadImagePath, model.PdfFilePath, model.PagePercentage, model.WatermarkMatrixImagePath,
                         model.WatermarkMatrixImageText, model.VideoCategory, model.TrackingNumber, model.VideoPath, model.RarPath,model.TotalLength,model.CatId,model.UploadAudio2Path);

                    //if (model.InterruptedAudio != null)
                    //{
                    //    context.sp_InsertBasicInterruptedAudio(model.CloneID, model.SongName, model.InterruptedAudio, model.Type, System.DateTime.Now, System.DateTime.Now, true);
                    //}
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

        public bool InsertBasicByteYourEbook(BasicGenerateCloneModel model, InterruptedFileModel ob1, CreateLinkPostModel ob, AllGenerateCloneModel Alldata)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_BasicGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                         model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Publisher, model.Producer,
                         model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent,
                         model.UploadImagePath, model.PdfFilePath, model.PagePercentage, model.WatermarkMatrixImagePath,
                         model.WatermarkMatrixImageText, model.VideoCategory, model.TrackingNumber, model.VideoPath, model.RarPath, model.TotalLength,model.CatId,model.UploadAudio2Path);


                    context.sp_InterruptedFiles(ob1.UserId, ob1.CloneId, ob1.InterruptedFilePath, ob1.CreateDate, ob1.ModifiedDate, ob1.IsActive, ob1.FileName, ob1.VideoPath, ob1.TrackNumber, ob1.CatId);

                    context.sp_CreateLinkPost(ob.Title, ob.Channel, ob.NoOfChannel, ob.Views, ob.Downloads, ob.FileSize, ob.TrackingNumber, ob.Date, ob.Category, ob.UserID, ob.MatrixImagePath);
                    context.sp_AllGenerateClones(Alldata.UserID, Alldata.CloneId, Alldata.Title, Alldata.Tag, Alldata.ArtistName, Alldata.AlbumTitle,
                        Alldata.AudioFilePath, Alldata.UploadFilePath,
                        Alldata.MatrixFilePath, Alldata.ComposerName, Alldata.Producer, Alldata.Publisher,
                       Alldata.SelectedInteruptionFile, Alldata.InteruptionStyle, Alldata.AvailableForDownload,
                       Alldata.ExplicitContent, Alldata.UploadImageFilePath, Alldata.UploadPDFFilePath, Alldata.PagePercentage, 
                       Alldata.Type, Alldata.PdfFilePath, Alldata.FileNames,
                       Alldata.VideoFilePath, Alldata.WaterMarkMatrixImagePath,
                       Alldata.WaterMarkMatrixImageText, Alldata.VideoCategory, Alldata.RARFilePath,
                       Alldata.MatrixImagePath, Alldata.CreatotName, Alldata.TrackingNumber, Alldata.CatID, Alldata.GenCloneID,
                       Alldata.CreatedDate, Alldata.ModifyDate, Alldata.IsActive);

                    trans.Complete();
                    return true;
                }
            }
            catch { throw; }
        }


        public bool InsertBasicByteYourVideo(BasicGenerateCloneModel model, InterruptedFileModel mm, CreateLinkPostModel post, AllGenerateCloneModel Alldata)
        {
            model.CloneID = Guid.NewGuid();
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_BasicGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                       model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Publisher, model.Producer,
                       model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent,
                       model.UploadImagePath, model.PdfFilePath, model.PagePercentage, model.WatermarkMatrixImagePath,
                       model.WatermarkMatrixImageText, model.VideoCategory, model.TrackingNumber, model.VideoPath, model.RarPath, model.TotalLength,model.CatId,model.UploadAudio2Path);

                    context.sp_InterruptedFiles(mm.UserId, mm.CloneId, mm.InterruptedFilePath, mm.CreateDate, mm.ModifiedDate, mm.IsActive, mm.FileName, mm.VideoPath, mm.TrackNumber,mm.CatId);

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

        public List<BindDropDown> GetAudioForINterruption(Guid userId)
        {
            List<BindDropDown> li = new List<BindDropDown>();
            try
            {
                var data = context.tblAudioInterruptions.Where(m => m.UserId == userId).ToList();
                foreach (var item in data)
                {
                    li.Add(new BindDropDown()
                    {
                        id = item.ID,
                        AudioFileName = item.AudioInterruptionFileName,
                        Name = item.FileName
                    });
                }
                return li;
            }
            catch { throw; }
        }


        public bool InsertAlbum(BasicGenerateCloneModel model)
        {
            model.CloneID = Guid.NewGuid();
            try
            {
                int re = context.sp_BasicGenerateClone(model.UserID, model.CloneID, model.Title, model.Tags, model.ArtistName,
                      model.AlbumTitle, model.UploadAudioPath, model.MatrixImageBytePath, model.Composer, model.Publisher, model.Producer,
                      model.SelectedIntFile, model.InterruptionStyle, model.AvailableDownload, model.ExplicitContent,
                      model.UploadImagePath, model.PdfFilePath, model.PagePercentage, model.WatermarkMatrixImagePath,
                      model.WatermarkMatrixImageText, model.VideoCategory, model.TrackingNumber, model.VideoPath, model.RarPath, model.TotalLength,model.CatId,model.UploadAudio2Path);
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

        public bool updatbasicaccount(Guid usid)
        {
            
                    return context.sp_updatbasicaccount(usid, true) >= 0 ? true :false;
                  
        }
       
        public string FetchEmailRecord(Guid userid,string rec)
        {
            if (rec != null)
            {
                var emaildata = context.tblBasicAccounts.Where(m => m.BasicUserID == userid).Select(m => m.RecoveryEmail).FirstOrDefault();
                if (emaildata == rec)
                {
                    return "Already EmailId Present";

                }
                else if (emaildata != rec)
                {
                    context.sp_updatbasicaccount(userid, false);
                    return "New EmailId";

                }
            }
          
           return "";
        }

        public bool UpdateBasicAccountInfo(BasicAccountViewModel basicmodel)
        {
            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    //Update data in Bank Account Details
                    if (basicmodel.BankAccountID.HasValue)
                    {
                        context.sp_UpdateBankAccountDetails(basicmodel.BankAccountID, basicmodel.CardNumber, basicmodel.ExpiryMonth, basicmodel.ExpiryYear, basicmodel.CSV, basicmodel.NameOnCard);
                    }
                    //Update data in Personal Address
                    if (basicmodel.AddressID.HasValue)
                    {
                        context.sp_UpdatePersonAddress(basicmodel.AddressID, basicmodel.AddressLine1, basicmodel.AddressLine2, basicmodel.City, basicmodel.Region, basicmodel.PostalCode);
                    }
                    //Update data in Security Queastion Answer
                    if (basicmodel.SecurityQuestionID.HasValue)
                    {
                        context.sp_UpdateSecurityQuestions(basicmodel.SecurityQuestionID, basicmodel.SecurityQuestion1, basicmodel.Answer1, basicmodel.SecurityQuestion2, basicmodel.Answer2);
                    }
                    //Update data in AphidTise Table
                    // context.sp_UpdateBasicAccount(basicmodel.BasicUserID,basicmodel.UserName,basicmodel.FirstName,basicmodel.LastName,basicmodel.EmailAddress,basicmodel.DOB,basicmodel.Phone,basicmodel.AudioInterruptionFile,basicmodel.Watermarks,basicmodel.ProfilePictureInBytes,basicmodel.WebSiteUrl,basicmodel
                    context.sp_UpdateUsers(basicmodel.ProfilePicturePath, basicmodel.ProfilePictureServerId, basicmodel.BasicUserID);

                    context.sp_UpdateBasicAccount(basicmodel.BasicUserID, basicmodel.Password, basicmodel.FirstName, basicmodel.LastName, basicmodel.EmailAddress, basicmodel.DOB, basicmodel.Phone, basicmodel.AudioInterruption, basicmodel.SelectWatermarkInterruption, basicmodel.WebSiteUrl, basicmodel.RecoveryEmail);

                    if (basicmodel.SelectedAudioPathForInt != null)
                    {
                        var dataAudio = context.tblAudioInterruptions.Where(m => m.UserId == basicmodel.BasicUserID && m.FileName == basicmodel.SelectedAudioForInt).SingleOrDefault();
                        if (dataAudio == null)
                        {

                            context.sp_AudioInteruptionFile(basicmodel.BasicUserID, basicmodel.SelectedAudioPathForInt, basicmodel.SelectedAudioForInt, false, System.DateTime.Now, System.DateTime.Now);
                        }
                    }
                    if (basicmodel.SelectedAudio != null)
                    {
                        var dataAudio1 = context.tblAudioInterruptions.Where(m => m.UserId == basicmodel.BasicUserID).ToList();
                        List<string> liAudio = new List<string>();
                        if (dataAudio1.Count != 0)
                        {
                            foreach (var item in dataAudio1)
                            {
                                liAudio.Add(item.FileName);
                            }
                        }
                        if (liAudio.Contains(basicmodel.SelectedAudio))
                        {
                            basicmodel.Flag = "UPD";
                            foreach (var item in dataAudio1)
                            {
                                if (item.FileName == basicmodel.SelectedAudio)
                                {
                                    item.IsActive = true;
                                }
                                else { item.IsActive = false; }

                            }

                            context.SaveChanges();
                        }
                    }

                    if (basicmodel.SelectedImageNameInt != null)
                    {
                        var dataImage = context.tblWaterMarkUpInterruptions.Where(m => m.UserId == basicmodel.BasicUserID && m.WatermarkImageName == basicmodel.SelectedImageNameInt).SingleOrDefault();
                        if (dataImage == null)
                        {
                            context.sp_InsertUpdateImageInteruptionFileNew(basicmodel.BasicUserID, basicmodel.SelectedImagePathInt, basicmodel.SelectedImageNameInt, false, "INS");
                        }
                    }

                    var data = context.tblWaterMarkUpInterruptions.Where(m => m.UserId == basicmodel.BasicUserID).ToList();
                    List<string> li = new List<string>();
                    if (data != null)
                    {
                        foreach (var item in data)
                        {
                            li.Add(item.WatermarkImageName);
                        }
                    }
                    if (li.Contains(basicmodel.SelectedImage))
                    {
                        basicmodel.Flag = "UPD";
                        foreach (var item in data)
                        {
                            if (item.WatermarkImageName == basicmodel.SelectedImage)
                            {
                                item.IsActive = true;
                            }
                            else { item.IsActive = false; }

                        }

                        context.SaveChanges();
                    }



                    tranScope.Complete();
                    return true;
                }
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

        public int totalplaylist(Guid userID)
        {
            var data = 0;
            try
            {
                data = context.tbl_PlayList.Where(m => (m.UserID == userID)).Select(m => m.PlaylistName).Distinct().Count();
            }
            catch { throw; }
            return data;
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
                var is_record_present = context.tbl_PlayList.Any(m => (m.TrackingID == TrackingID) && (m.UserID == UserID) && (m.PlaylistName == PlaylistName));
                if (is_record_present == false)
                {
                    int count = context.tbl_PlayList.Where(m => (m.PlaylistName == PlaylistName) && (m.UserID == UserID)).Count();
                    tblAllGenerateClone fetchRecord = context.tblAllGenerateClones.Where(m => (m.TrackingNumber == TrackingID)).SingleOrDefault();
                    if (fetchRecord != null)
                    {
                        if (count < 20)
                        {
                            tbl_PlayList tab = new tbl_PlayList();
                            tab.PlaylistName = PlaylistName;
                            tab.UserID = UserID;
                            tab.TrackingID = TrackingID;
                            tab.FileName = fetchRecord.FileNames;
                            tab.Composer = fetchRecord.ComposerName;
                            tab.CatId = fetchRecord.CatID.Value;
                            context.tbl_PlayList.Add(tab);
                            context.SaveChanges();
                            addStatus = true;
                        }
                        else
                        {
                            //for overflow playlists
                        }
                    }
                }
                return addStatus;
            }
            catch { throw; }
        }

        //public bool AddVideoToPlaylist(string PlaylistName, string TrackingID, Guid UserID)
        //{
        //    bool addStatus = false;
        //    int count = context.tbl_PlayList.Where(m => (m.PlaylistName == PlaylistName) && (m.UserID == UserID)).Count();
        //    tblAllGenerateClone fetchRecord = context.tblAllGenerateClones.Where(m => (m.TrackingNumber == TrackingID)).SingleOrDefault();
        //    if (fetchRecord != null)
        //    {
        //        if (count < 20)
        //        {
        //            tbl_PlayList tab = new tbl_PlayList();
        //            tab.PlaylistName = PlaylistName;
        //            tab.UserID = UserID;
        //            tab.TrackingID = TrackingID;
        //            tab.FileName = fetchRecord.FileNames;
        //            tab.Composer = fetchRecord.ComposerName;
        //            context.tbl_PlayList.Add(tab);
        //            context.SaveChanges();
        //            addStatus = true;
        //        }
        //        else
        //        {
        //            //for overflow playlists
        //        }
        //    }
        //    return addStatus;
        //}

        public AllGenerateCloneModel Get_A_Record_via_trackID(string TrackingNumber)
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


        public string GetImageName(Guid UserId)
        {
            string path = "";
            try
            {
                var data = context.tblWaterMarkUpInterruptions.Where(m => m.UserId == UserId && m.WatermarkImageName != "Default" && m.IsActive == true).SingleOrDefault();
                if (data != null)
                {
                    path = data.ImageInterruption;
                }
                return path;
            }
            catch { throw; }
        }

        public List<string> GetBasicImages(Guid userid)
        {
            List<string> li = new List<string>();
            try
            {
                var data = context.tblWaterMarkUpInterruptions.Where(m => m.UserId == userid).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        li.Add(item.WatermarkImageName);
                        li.Add(item.ImageInterruption);
                    }
                }
                return li;
            }
            catch { throw; }
        }

        public string GetImagePath(Guid UserId, string Name)
        {
            string path = "";
            try
            {
                var data = context.tblWaterMarkUpInterruptions.Where(m => m.WatermarkImageName == Name && m.UserId == UserId).SingleOrDefault();
                if (data != null)
                {
                    path = data.ImageInterruption;
                }
                return path;
            }
            catch { throw; }
        }

        public string GetAudioPath(Guid UserId, string Name)
        {
            string path = "";
            try
            {
                var data = context.tblAudioInterruptions.Where(m => m.FileName == Name && m.UserId == UserId).SingleOrDefault();
                if (data != null)
                {
                    path = data.AudioInterruptionFileName;
                }
                return path;
            }
            catch { throw; }
        }

        public bool UpgradeAccount(Guid userid)
        {
            bool IsSuccess = false;
            try
            {
                var data = context.tblBasicAccounts.Where(m => m.BasicUserID == userid).SingleOrDefault();
                if (data != null)
                {
                    context.sp_InsertPremiumAccount(data.BasicUserID, data.UserName, data.Password, data.DOB, data.FirstName, data.LastName, "", data.WebSiteUrl, data.EmailAddress, data.Phone, data.AddressID, data.SecurityQuestionID, 4, data.CreateDate, System.DateTime.Now, data.IsDelete);
                    var UpdateUsers = context.tblUsers.Where(m => m.UserId == userid).SingleOrDefault();
                    if (UpdateUsers != null)
                    {
                        UpdateUsers.AccountTypeID = 4;
                        context.SaveChanges();
                    }
                    var updateBasic = context.tblBasicAccounts.Where(m => m.BasicUserID == userid).SingleOrDefault();
                    if (updateBasic != null)
                    {
                        updateBasic.AccountTypeID = 4;
                        context.SaveChanges();
                    }

                    var CopyData = context.tblBasicGenerateClones.Where(m => m.UserID == userid).ToList();
                    if (CopyData != null)
                    {
                        foreach (var item in CopyData)
                        {
                            context.sp_PremiumGenerateClone(item.UserID, item.CloneID, item.Title, item.Tags, item.ArtistName, item.AlbumTitle, item.UploadFileAudioPath, item.UploadFileImagePath,
                                item.Composer, item.Producer, item.Publisher, item.SelectIntFile, item.InterruptionStyle, item.AvailableForDownload, item.ExplicitContent, "", item.UploadFilePDFPath,
                                item.VideoPath, item.PagePercentage, item.RarPath, item.MatrixImagePath, item.Composer, item.TrackingNumber,item.TotalLength,item.CatID);
                        }
                    }
                    IsSuccess = true;
                }
            }
            catch (Exception)
            {
                IsSuccess = false;
            }
            return IsSuccess;
        }
        public bool deleteAccount(Guid userid)
        {
            bool IsSuccess = false;
            try
            {
                var data = context.tblBasicAccounts.Where(m => m.BasicUserID == userid).SingleOrDefault();
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
        public bool UpdatPlaylistBasic(string PlaylistName, string TrackingID, Guid UserID)
        {

            try
            {
                var data = context.tbl_PlayList.FirstOrDefault(m => (m.UserID == UserID) && (m.TrackingID == TrackingID));
                if (data != null)
                {
                    data.PlaylistName = PlaylistName;
                    if (context.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
            catch { throw; }
            return false;
        }
        //public bool GetUserID(string userRecoveryMail)
        //{
        //    var userId = context.tblBasicAccounts.Where(m => m.RecoveryEmail == userRecoveryMail).Select(m => m.BasicUserID).FirstOrDefault();
        //    return updatbasicaccount(userId);
        //}
    }
}
