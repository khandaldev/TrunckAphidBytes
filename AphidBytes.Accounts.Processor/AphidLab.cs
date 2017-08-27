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
    public class AphidLab : IAphidLAb
    {
         RepositoryAphidLab repository = new RepositoryAphidLab();

         public DataPlanDetail DataPlanDetailMethod(Guid UserId)
         {
             return repository.DataPlanDetail(UserId);
         }
         public AphidLabAccountModel GetAphidLabAccountInfo(Guid userID)
         {
            Common AudioFiles = new Common();
            
            List<ImageFileModel> Imagefile = new List<ImageFileModel>(AudioFiles.GetImageFile(userID));
            sp_AphidLAbAccountInfo_Result aphidTiseObjectData = repository.GetAphidLabAccountInfo(userID);

            AphidLabAccountModel AphidLabData = new AphidLabAccountModel();
            if (aphidTiseObjectData != null)
            {
                AphidLabData.AddressLine1 = aphidTiseObjectData.AddressLine1;
                AphidLabData.AddressLine2 = aphidTiseObjectData.AddressLine2;
                AphidLabData.Answer1 = aphidTiseObjectData.Answer1;
                AphidLabData.Answer2 = aphidTiseObjectData.Answer2;
                AphidLabData.City = aphidTiseObjectData.City;
                AphidLabData.DOB = aphidTiseObjectData.DOB;
                AphidLabData.FirstName = aphidTiseObjectData.Firstname;
                AphidLabData.LastName = aphidTiseObjectData.Lastname;
                AphidLabData.Phone = aphidTiseObjectData.Phonenumber;
                AphidLabData.PostalCode = aphidTiseObjectData.PostalCode;
                AphidLabData.Region = aphidTiseObjectData.Region;
                AphidLabData.SecurityQuestion1 = aphidTiseObjectData.SecurityQuestion1;
                AphidLabData.SecurityQuestion2 = aphidTiseObjectData.SecurityQuestion2;
                AphidLabData.UserName = aphidTiseObjectData.DeveloperName;
                AphidLabData.Website = aphidTiseObjectData.WebsiteUrl;
                AphidLabData.AccountTypeID = aphidTiseObjectData.Accountid;
                AphidLabData.AddressID = aphidTiseObjectData.AddressId;
                AphidLabData.SecurityQuestionID = aphidTiseObjectData.SecurityQuestionID;
                AphidLabData.EmailAddress = aphidTiseObjectData.UserEmail;
                AphidLabData.SocialNetworkSource = aphidTiseObjectData.SocialNetworSource;
                AphidLabData.AphidlabUserID = userID;
            }
            return AphidLabData;
         }
         public bool UpdateAphidLabAccountInfo(AphidLabAccountModel model)
         {
             return repository.UpdateAphidLabAccountInfo(model);
         }
         public bool InsertAphidLabVideo(AphidLabsUpload AphidLabVideoModel, InterruptedFileModel intModel, CreateLinkPostModel post)
         {
             return repository.InsertAphidLabVideo(AphidLabVideoModel, intModel, post);
         }
         public bool InsertAphidLabSoftware(AphidLabsUpload AphidlabSoftwaremodel, InterruptedFileModel intModel, CreateLinkPostModel post)
         {
             return repository.InsertAphidLabSoftware(AphidlabSoftwaremodel, intModel, post);
         }
         public List<AphidLabsUpload> fileprivew(String trackingNumber)
         {
             return repository.fileprivew(trackingNumber);
         }

        public bool VerifyEmailAccount(Guid usid)
        {
            return repository.VerifyMailAccount(usid);
        }
    }
}
