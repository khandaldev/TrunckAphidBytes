using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AphidBytes.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Core.StorageService
{
    public class AWSStorage
    {

        #region properties

        public IAmazonS3 Client { get; set; }

        public string StoragerRootURL { get; set; }
        public string AphidByteBucket { get; set; }

        #endregion

        #region contstructors
        public AWSStorage()
        {
            StoragerRootURL = new ConfigurationValue<string>("aws.Url").Value;

        }

        public AWSStorage(string BucketName)
        {
            StoragerRootURL = new ConfigurationValue<string>("aws.Url").Value;
            AphidByteBucket = BucketName;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Upload file To S3
        /// </summary>
        /// <param name="localFilePath">LocalFile</param>
        /// <param name="BucketName">Bucket Name</param>
        /// <param name="SubDirInBucket">Bucket Sub Dir</param>
        /// <param name="FileNameS3"></param>
        /// <returns></returns>
        public bool UploadtoFromLocalS3(string localFilePath, string SubDirInBucket, string FileNameS3)
        {

            try
            {
                IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.USEast1);
                TransferUtility utility = new TransferUtility(client);
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
                if (SubDirInBucket == "" || SubDirInBucket == null)
                {
                    request.BucketName = AphidByteBucket;
                }
                else
                {
                    request.BucketName = AphidByteBucket + @"/" + SubDirInBucket;
                }
                request.CannedACL = S3CannedACL.PublicRead;
                request.Key = FileNameS3;
                request.FilePath = localFilePath;
                utility.Upload(request);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public string UploadtoFromStreamlS3(Stream StreamFile, string BucketName, string SubDirInBucket, string FileNameS3)
        {

            try
            {
                IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.USEast1);
                TransferUtility utility = new TransferUtility(client);
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
                if (SubDirInBucket == "" || SubDirInBucket == null)
                {
                    request.BucketName = BucketName;
                    
                }
                else
                {
                    request.BucketName = BucketName + @"/" + SubDirInBucket;
                    
                }
                request.CannedACL = S3CannedACL.PublicRead;
                request.Key = FileNameS3;
                request.InputStream = StreamFile;

                utility.Upload(request);
                return StoragerRootURL+"/" + request.BucketName + "/"+request.Key;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool DeleteObjectS3(string SubDir, string Name)
        {

            try
            {
                IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.USEast1);
                string bucket = AphidByteBucket;
                if (SubDir == "" || SubDir == null)
                {
                    bucket = AphidByteBucket;

                }
                else
                {
                    bucket = AphidByteBucket + @"/" + SubDir;

                }
                DeleteObjectRequest req = new DeleteObjectRequest
                {
                    BucketName= bucket,
                    Key=Name

                };


                client.DeleteObject(req);
                return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion
    }
}
