using Amazon.S3.Model;
using MoveS3Folders.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveS3Folders.Core
{
    public static class MoveToS3
    { 

        public static OperationResponse CopyOrMoveObjects(string bucketOrigin, string bucketDestination, string keyNameOrigin, string keyNameDestination,  string moveFolder, string overWriteDestinationFolder)
        {
            string _AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            string _AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            var _regionEndPoint = Amazon.RegionEndpoint.GetBySystemName(ConfigurationManager.AppSettings["AWSRegion"]);
            bool _moveFolder = bool.Parse(moveFolder);
            bool _overWriteDestinationFolder = bool.Parse(overWriteDestinationFolder);
            string _verbOperation = _moveFolder ? "Moving" : "Copying"; 

            BAmazonS3 oBAmazonS3 = new BAmazonS3(_AWSAccessKey, _AWSSecretKey);
            Console.WriteLine($@"{_verbOperation} folder from '{bucketOrigin}/{keyNameOrigin}' to '{bucketDestination}/{bucketDestination}'");

            if (_moveFolder)
                return oBAmazonS3.MoveFolder(bucketOrigin, keyNameOrigin, bucketDestination, keyNameDestination, _overWriteDestinationFolder, _regionEndPoint);
            else
                return oBAmazonS3.CopyFolder(bucketOrigin, keyNameOrigin, bucketDestination, keyNameDestination, _overWriteDestinationFolder, _regionEndPoint);
        } 


    }


}
