using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon;
using Amazon.S3.Transfer;

namespace AphidByte.Test
{
    [TestClass]
    public class AWS
    {
        static string bucketName = "aphidbytepictures";
        static string keyName = "Test";
        static string filePath = "‪C:\\csharp.png";
        static IAmazonS3 client;


        static void WritingAnObject()
        {
            try
            {
                /* PutObjectRequest putRequest1 = new PutObjectRequest
                 {
                     BucketName = bucketName,
                     Key = keyName,
                     ContentBody = "sample text"
                 };

                 PutObjectResponse response1 = client.PutObject(putRequest1);*/

                // 2. Put object-set ContentType and add metadata.
                PutObjectRequest putRequest2 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    ContentType = "image/jpg"
                };
                putRequest2.Metadata.Add("x-amz-meta-title", "someTitle");

                PutObjectResponse response2 = client.PutObject(putRequest2);

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    /*Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                        "For service sign up go to http://aws.amazon.com/s3");*/
                }
                else
                {
                    /* Console.WriteLine(
                         "Error occurred. Message:'{0}' when writing an object"
                         , amazonS3Exception.Message);*/
                }
            }
        }
        [TestMethod]
        public void TestMethod1()
        {
            sendMyFileToS3("C:\\csharp.png", "aphidbyte", "", "test.png");
           // WritingAnObject();
        }


        public bool sendMyFileToS3(string localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            // input explained :
            // localFilePath = the full local file path e.g. "c:\mydir\mysubdir\myfilename.zip"
            // bucketName : the name of the bucket in S3 ,the bucket should be alreadt created
            // subDirectoryInBucket : if this string is not empty the file will be uploaded to
            // a subdirectory with this name
            // fileNameInS3 = the file name in the S3

            // create an instance of IAmazonS3 class ,in my case i choose RegionEndpoint.EUWest1
            // you can change that to APNortheast1 , APSoutheast1 , APSoutheast2 , CNNorth1
            // SAEast1 , USEast1 , USGovCloudWest1 , USWest1 , USWest2 . this choice will not
            // store your file in a different cloud storage but (i think) it differ in performance
            // depending on your location
            IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.USEast1);

            // create a TransferUtility instance passing it the IAmazonS3 created in the first step
            TransferUtility utility = new TransferUtility(client);
            // making a TransferUtilityUploadRequest instance
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName; //no subdirectory just bucket name
            }
            else
            {   // subdirectory and bucket name
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.CannedACL = S3CannedACL.PublicRead;
            request.Key = fileNameInS3; //file name up in S3
            request.FilePath = localFilePath;//local file name           
            
            utility.Upload(request); //commensing the transfer
            
            return true; //indicate that the file was sent
        }
    }
}
