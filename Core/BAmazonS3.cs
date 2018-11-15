using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Amazon;
using MoveS3Folders.Core;
using Amazon.S3.IO;

namespace MoveS3Folders.Core
{
    public class BAmazonS3 : IBAmazonS3
    {
        private string _awsAccessKey;
        private string _awsSecretAccessKey;
        private AmazonS3Config config;

        private void IsValid(string awsAccessKey, string awsSecretAccessKey, string bucket, string key, RegionEndpoint region)
        {
            if (awsAccessKey.IsNullOrEmpty())
                throw new Exception("AwsAccessKey must be defined");

            if (awsSecretAccessKey.IsNullOrEmpty())
                throw new Exception("AwsSecretAccessKey must be defined");

            if (bucket.IsNullOrEmpty())
                throw new Exception("BucketOrigin must be defined");  

            if (key.IsNullOrEmpty())
                throw new Exception("KeyNameOrigin must be defined"); 

            if (region == null)
                throw new Exception("AWSRegion must be defined");
        }

        private void IsValid(string awsAccessKey, string awsSecretAccessKey, string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, RegionEndpoint region)
        {
            if (awsAccessKey.IsNullOrEmpty())
                throw new Exception("AwsAccessKey must be defined");

            if (awsSecretAccessKey.IsNullOrEmpty())
                throw new Exception("AwsSecretAccessKey must be defined");

            if (sourceBucket.IsNullOrEmpty())
                throw new Exception("BucketOrigin must be defined");

            if (destinationBucket.IsNullOrEmpty())
                throw new Exception("BucketDestination must be defined");

            if (sourceKey.IsNullOrEmpty())
                throw new Exception("KeyNameOrigin must be defined");

            if (destinationKey.IsNullOrEmpty())
                throw new Exception("KeyNameDestination must be defined");

            if (region == null)
                throw new Exception("AWSRegion must be defined");
        }

        public BAmazonS3()
        {
            _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            _awsSecretAccessKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            config = new AmazonS3Config();
            config.ServiceURL = ConfigurationManager.AppSettings["AWSRegion"];
        }

        public BAmazonS3(string pawsAccessKey, string pawsSecretAccessKey, string pRegion = null)
        {
            _awsAccessKey = pawsAccessKey;
            _awsSecretAccessKey = pawsSecretAccessKey;

            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = pRegion ?? ConfigurationManager.AppSettings["AWSRegion"];
        }

        public OperationResponse DeleteFolder(string bucket, string key, RegionEndpoint region)
        {
            var operationResponse = new OperationResponse();
            try
            {
                this.IsValid(_awsAccessKey, _awsSecretAccessKey, bucket, key, region);
                using (var client = new AmazonS3Client(_awsAccessKey, _awsSecretAccessKey, region))
                {
                    S3DirectoryInfo folder = new S3DirectoryInfo(client, bucket, key); 
                    folder.Delete(true); 
                }
                operationResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    operationResponse.Message = "Please check the provided AWS Credentials. If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3";
                    operationResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }
                else
                {
                    operationResponse.Message = amazonS3Exception.Message;
                    operationResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception oException)
            {
                operationResponse.Message = oException.Message;
                operationResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return operationResponse;
        }

        public OperationResponse MoveFolder(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, bool overwriteDestinationFolder, RegionEndpoint region)
        {
            var operationResponse = new OperationResponse();
            try
            {
                if (overwriteDestinationFolder)
                    operationResponse = this.DeleteFolder(destinationBucket, destinationKey, region);

                this.IsValid(_awsAccessKey, _awsSecretAccessKey, sourceBucket, sourceKey, destinationBucket, destinationKey, region);
                using (var client = new AmazonS3Client(_awsAccessKey, _awsSecretAccessKey, region))
                {
                    S3DirectoryInfo origin = new S3DirectoryInfo(client, sourceBucket, sourceKey.ToSlashesFileSystem());
                    S3DirectoryInfo target = new S3DirectoryInfo(client, destinationBucket, destinationKey.ToSlashesFileSystem());
                    target.Create();
                    operationResponse.S3DirectoryInfo = origin.MoveTo(target);
                }
                operationResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    operationResponse.Message = "Please check the provided AWS Credentials. If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3";
                    operationResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }
                else
                {
                    operationResponse.Message = amazonS3Exception.Message;
                    operationResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception oException)
            {
                operationResponse.Message = oException.Message;
                operationResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return operationResponse;
        }
         
        public OperationResponse CopyFolder(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, bool overwriteDestinationFolder, RegionEndpoint region)
        {
            var operationResponse = new OperationResponse();
            try
            {
                if (overwriteDestinationFolder)
                    operationResponse = this.DeleteFolder(destinationBucket, destinationKey, region);

                this.IsValid(_awsAccessKey, _awsSecretAccessKey, sourceBucket, sourceKey, destinationBucket, destinationKey, region);
                using (var client = new AmazonS3Client(_awsAccessKey, _awsSecretAccessKey, region))
                {
                    S3DirectoryInfo origin = new S3DirectoryInfo(client, sourceBucket, sourceKey.ToSlashesFileSystem());
                    S3DirectoryInfo target = new S3DirectoryInfo(client, destinationBucket, destinationKey.ToSlashesFileSystem());
                    target.Create(); 
                    operationResponse.S3DirectoryInfo = origin.CopyTo(target);
                }
                operationResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    operationResponse.Message = "Please check the provided AWS Credentials. If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3";
                    operationResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }
                else
                {
                    operationResponse.Message = amazonS3Exception.Message;
                    operationResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception oException)
            {
                operationResponse.Message = oException.Message;
                operationResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return operationResponse;
        }
    }


}
