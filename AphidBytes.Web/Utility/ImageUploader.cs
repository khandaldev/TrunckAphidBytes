using Amazon.S3;
using Amazon.S3.Model;
using AphidBytes.Accounts.Contracts.ContentServers;
using AphidBytes.Accounts.Contracts.Model.BaseTypes;
using AphidBytes.Core.Configuration;
using AphidBytes.Core.Images;
using AphidBytes.Core.StorageService;
using System.IO;
using System;
using AphidBytes.Accounts.Contracts.Model;

namespace AphidBytes.Web.Utility
{
    public class ImageUploader
    {


        



        public static void UploadProfilePictureAndSetLocation(IAccountInfo accountInfo)
        {
            string bucketName=  new ConfigurationValue<string>("aws.Bucket").Value;
            AWSStorage storage = new AWSStorage(bucketName);
            if (accountInfo.ProfilePicture == null)
            {
                return;
            }

            using (var ms = new MemoryStream())
            {
                ImageUtility.ResizeSelectedAreaToJpeg(accountInfo.ProfilePicture.InputStream, ms);
                var contentServer = ContentServerManager.GetDefaultContentServer();
                var result=storage.UploadtoFromStreamlS3((Stream)ms, bucketName, "profile",accountInfo.UserName+"."+accountInfo.ProfilePicture.FileName.Split('.')[1]);
                //var imagePath = contentServer.UploadToContentServer(ms);
                if (!string.IsNullOrEmpty(result))
                {   
                    accountInfo.ProfilePicturePath = result;
                }
            }
        }

        public static bool DeleteProfileImage(string Url)
        {
            try
            {
                
                var StoragerRootURL = new ConfigurationValue<string>("aws.Url").Value;
                var bucketName = new ConfigurationValue<string>("aws.Bucket").Value;
                AWSStorage storage = new AWSStorage(bucketName);
                var objectName = Url.Replace(StoragerRootURL+"/", "").Replace(bucketName+"/", "");
                var subdir = objectName.Split('/')[0];
                var filename = objectName.Split('/')[1];
                storage.DeleteObjectS3(subdir, filename);
                return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void UploadThumbnailAudioPictureAndSetLocation(BasicGenerateCloneModel fileInfo)
        {
            string bucketName = new ConfigurationValue<string>("aws.Bucket").Value;
            AWSStorage storage = new AWSStorage(bucketName);
            if (fileInfo.MatrixImage == null)
            {
                return;
            }

            using (var ms = new MemoryStream())
            {
                ImageUtility.ResizeSelectedAreaToJpeg(fileInfo.MatrixImage.InputStream, ms);
                var contentServer = ContentServerManager.GetDefaultContentServer();
                var result = storage.UploadtoFromStreamlS3((Stream)ms, bucketName, "audiohhumbnail", fileInfo.CloneID + "." + fileInfo.MatrixImage.FileName.Split('.')[1]);
                
                if (!string.IsNullOrEmpty(result))
                {
                    fileInfo.UploadImagePath = result;
                }
            }
        }
    }
}