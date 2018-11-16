using Amazon;
using Amazon.S3.Model;
using MoveS3Folders.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveS3Folders.Core
{
    public interface IBAmazonS3
    {
        OperationResponse DeleteFolder(string bucket, string key, RegionEndpoint region);
        OperationResponse CopyFiles(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey,bool moveFolder, bool overwriteDestinationFolder, RegionEndpoint region);
        OperationResponse CopyFolder(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, bool overwriteDestinationFolder, RegionEndpoint region);
        OperationResponse MoveFolder(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey, bool overwriteDestinationFolder, RegionEndpoint region);
    }
}
