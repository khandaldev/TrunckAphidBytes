using AphidBytes.Accounts.Contracts.ContentServers;
using AphidBytes.Accounts.Contracts.Model.BaseTypes;
using AphidBytes.Core.Configuration;
using AphidBytes.Core.Images;
using AphidBytes.Core.StorageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AphidBytes.Web.Utility
{
    public class VideoUploader
    {


        public static void UploadVideo(IAccountInfo accountInfo)
        {
            string bucketName = new ConfigurationValue<string>("aws.Bucket").Value;
            AWSStorage storage = new AWSStorage(bucketName);
            if (accountInfo.ProfilePicture == null)
            {
                return;
            }

            using (var ms = new MemoryStream())
            {
                ImageUtility.ResizeSelectedAreaToJpeg(accountInfo.ProfilePicture.InputStream, ms);
                var contentServer = ContentServerManager.GetDefaultContentServer();
                var result = storage.UploadtoFromStreamlS3((Stream)ms, bucketName, "profile", accountInfo.UserName + "." + accountInfo.ProfilePicture.FileName.Split('.')[1]);
                //var imagePath = contentServer.UploadToContentServer(ms);
                if (!string.IsNullOrEmpty(result))
                {
                    accountInfo.ProfilePicturePath = result;
                }
            }
        }

    }
}