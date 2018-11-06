using Amazon.S3.IO;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoveS3Folders.Core
{
    public class OperationResponse
    {
        public OperationResponse()
        {
           
        }  
        public OperationResponse(string message, S3DirectoryInfo s3DirectoryInfo)
        {
            Message = message;
            S3DirectoryInfo = s3DirectoryInfo;
        }

        public string Message { get; set; } 
        public S3DirectoryInfo S3DirectoryInfo { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
