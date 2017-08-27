using AphidBytes.Accounts.Contracts.ContentServers;
using AphidBytes.Accounts.Contracts.Model;
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
    public class AudioUploader
    {
        public static void UploadAudio(BasicGenerateCloneModel fileInfo)
        {
            string bucketName = new ConfigurationValue<string>("aws.Bucket").Value;
            AWSStorage storage = new AWSStorage(bucketName);
            if (fileInfo.Audio == null)
            {
                return;
            }

            using (var ms = new MemoryStream())
            {
                // ImageUtility.ResizeSelectedAreaToJpeg(fileInfo.Audio.InputStream, ms);
                fileInfo.Audio.InputStream.Position = 0;

                fileInfo.Audio.InputStream.CopyTo(ms);// System.IO.File.ReadAllBytes(Server.MapPath(songname));
                byte[] by = ms.ToArray();

                var contentServer = ContentServerManager.GetDefaultContentServer();
                var result = storage.UploadtoFromStreamlS3((Stream)ms, bucketName, "audio", fileInfo.CloneID + "." + fileInfo.Audio.FileName.Split('.')[1]);
                //var imagePath = contentServer.UploadToContentServer(ms);
                if (!string.IsNullOrEmpty(result))
                {
                    fileInfo.UploadAudioPath = result;
                    //accountInfo.ProfilePicturePath = result;
                }
            }
        }
    }
}