using System.ComponentModel;
using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.IO;
using System.Text;
using static AphidBytes.Core.PaymentServices.StripePackages;
using AphidBytes.Core.Extensions;

namespace AphidTise.Entity.Repository
{
    public class RepositoryAphidLab : GenericRepository<tblAphidTiseAccount>
    {


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



        public sp_AphidLAbAccountInfo_Result GetAphidLabAccountInfo(Guid userID)
        {
            try
            {

                return context.sp_AphidLAbAccountInfo(userID).SingleOrDefault();
            }
            catch { throw; }
        }

        public bool UpdateAphidLabAccountInfo(AphidLabAccountModel aphidlabModel)
        {
            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    //Update data in Bank Account Details
                    if (aphidlabModel.BankAccountID.HasValue)
                    {
                        context.sp_UpdateBankAccountDetails(aphidlabModel.BankAccountID, aphidlabModel.CardNumber, aphidlabModel.ExpiryMonth, aphidlabModel.ExpiryYear, aphidlabModel.CSV, aphidlabModel.NameOnCard);
                    }
                    //Update data in Personal Address
                    if (aphidlabModel.AddressID.HasValue)
                    {
                        context.sp_UpdatePersonAddress(aphidlabModel.AddressID, aphidlabModel.AddressLine1, aphidlabModel.AddressLine2, aphidlabModel.City, aphidlabModel.Region, aphidlabModel.PostalCode);
                    }
                    //Update data in Security Queastion Answer
                    if (aphidlabModel.SecurityQuestionID.HasValue)
                    {
                        context.sp_UpdateSecurityQuestions(aphidlabModel.SecurityQuestionID, aphidlabModel.SecurityQuestion1, aphidlabModel.Answer1, aphidlabModel.SecurityQuestion2, aphidlabModel.Answer2);
                    }
                    context.sp_UpdateUsers(aphidlabModel.ProfilePicturePath, aphidlabModel.ProfilePictureServerId, aphidlabModel.AphidlabUserID);
                    //Update data in AphidTise Table
                    context.sp_UpdateAphidLAbAccount(aphidlabModel.AphidlabUserID, aphidlabModel.EmailAddress, aphidlabModel.FirstName, aphidlabModel.LastName,  aphidlabModel.DOB, aphidlabModel.Phone, aphidlabModel.Website, aphidlabModel.EmailAddress);
                    context.SaveChanges();
                    tranScope.Complete();
                    return true;
                }
            }
            catch { throw; }
        }

        public bool InsertAphidLabVideo(AphidLabsUpload AphidLabVideoModel, InterruptedFileModel intModel, CreateLinkPostModel post)
        {
            AphidLabVideoModel.CloneID = Guid.NewGuid();
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_InsertAphidlAbVideo(AphidLabVideoModel.UserID, AphidLabVideoModel.CloneID, AphidLabVideoModel.Titleofupload, AphidLabVideoModel.MatriximagePath, AphidLabVideoModel.VideoPath, AphidLabVideoModel.InterruptionStyle, AphidLabVideoModel.AvailableDownload, AphidLabVideoModel.ExplicitContent, AphidLabVideoModel.PasswordForDownload, AphidLabVideoModel.VideoDescription, DateTime.Now);
                    context.sp_CreateLinkPost(post.Title, post.Channel, post.NoOfChannel, post.Views, post.Downloads, post.FileSize, post.TrackingNumber, post.Date, post.Category, post.UserID, post.MatrixImagePath);
                    trans.Complete();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }


        public bool InsertAphidLabSoftware(AphidLabsUpload AphidlabSoftwaremodel, InterruptedFileModel intModel, CreateLinkPostModel post)
        {
             AphidlabSoftwaremodel.CloneID = Guid.NewGuid();
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    context.sp_InsertAphidlAbSoftware(AphidlabSoftwaremodel.UserID, AphidlabSoftwaremodel.CloneID, AphidlabSoftwaremodel.Titleofupload, AphidlabSoftwaremodel.MatriximagePath, AphidlabSoftwaremodel.AvailableDownload, AphidlabSoftwaremodel.PasswordForDownload, AphidlabSoftwaremodel.SoftwareDescription, DateTime.Now);
                    context.sp_CreateLinkPost(post.Title, post.Channel, post.NoOfChannel, post.Views, post.Downloads, post.FileSize, post.TrackingNumber, post.Date, post.Category, post.UserID, post.MatrixImagePath);
                    trans.Complete();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        //public List<AphidLabsUpload> Fileprivew(string trackingNumber)
        //{
        //    List<BasicGenerateCloneModel> li = new List<BasicGenerateCloneModel>();
 
        //    try
        //    {
        //         var name = context.tbl_AphidLabVideoUpload.Where(m => m.TrackingNumber == trackingNumber).SingleOrDefault();
                         
        //                    select new
        //                    {
        //                        pd.Title,
        //                        pd.AlbumTitle,
        //                        pd.ExplicitContent,
        //                        pd.ArtistName,
        //                        pd.Composer,
        //                        pd.AvailableForDownload,
        //                        pd.TrackingNumber,
        //                        pd.MatrixImagePath,
        //                        pd.UploadFileImagePath,
        //                        od.VideoPath,
        //                        pd.UploadFileAudioPath,
        //                        // pd.tblInterruptedBasicAudioFile,
        //                        od.InterruptFilePath,
        //                        pd.UploadFilePDFPath
        //                    }).ToList();

        //        if (data != null)
        //        {
        //            foreach (var item in data)
        //            {
        //                li.Add(new BasicGenerateCloneModel()
        //                {

        //                    Title = item.Title,
        //                    AlbumTitle = item.AlbumTitle,
        //                    ExplicitContent = item.ExplicitContent,
        //                    ArtistName = item.ArtistName,
        //                    Composer = item.Composer,
        //                    AvailableDownload = item.AvailableForDownload,
        //                    TrackingNumber = item.TrackingNumber,
        //                    MatrixImageBytePath = item.MatrixImagePath,
        //                    UploadImagePath = item.UploadFileImagePath,
        //                    VideoFile = item.VideoPath,
        //                    UploadAudioPath = item.UploadFileAudioPath,
        //                    Interruptedfile = item.InterruptFilePath,
        //                    UploadFilePDFPath = item.UploadFilePDFPath


        //                });
        //            }
        //        }

        //        return li;
        //    }
        //    catch { throw; }
        //}

        public List<AphidLabsUpload> fileprivew(string trackingNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies mail account
        /// </summary>
        /// <param name="usid"></param>
        /// <returns></returns>
        public bool VerifyMailAccount(Guid usid)
        {
            return context.sp_VerifyAphidLabAccount(usid, true) >= 0 ? true : false;
            

        }
    }
}
