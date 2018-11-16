# MoveS3Folders
A simple console program in C# .Net 4.5 to move entire folders (keynames) in AWS S3.
This tool can move or copy easy all content inside (files/subfolders) from a bucket/keyname to another bucket/keyname.
MoveS3Folders uses S3DirectoryInfo Class from namespace Amazon.S3.IO. 

For more details: https://docs.aws.amazon.com/sdkfornet1/latest/apidocs/html/T_Amazon_S3_IO_S3DirectoryInfo.htm

## Global configuration (app.config)
- _AWSAccessKey_:  Your access key supplied from IAM Role
- _AWSSecretKey_:  Your secret key supplied from IAM Role
- _AWSRegion_:  The region name from AWS S3 region points (ex: sa-east-1)
    
## Pre-defined configuration (app.config)
- _BucketOrigin_:  The source bucket where folders/files are stored.
- _BucketDestination_: The target bucket where folders/files will be move/copy.
- _KeyNameOrigin_:  The full keyname (folder) from source bucket
- _KeyNameDestination_: The full keyname (folder) from target bucket.  It's possible to change the path destination.
- _MoveFolder_:  if true move folders and files, if false copy folders and files
- _OverwriteDestinationFolder_: if true removed folder in destination before copy/move folder. There's no overwrite method in S3DirectoryInfo class.
- _MultiThreadFileTransferring_: if true multi thread file transfering is actived ableing a huge gain of perfomance significantly reducing elapsed time.
    
## On-demand command line (MoveS3Folders.exe -args)
All args below will overwrite Pre-defined configuration:
- _bucketOrigin_:  The source bucket where folders/files are stored.
- _bucketDestination_: The target bucket where folders/files will be move/copy.
- _keyNameOrigin_:  The full keyname (folder) from source bucket
- _keyNameDestination_: The full keyname (folder) from target bucket.  It's possible to change the path destination.
- _moveFolder_:  true:  to move folders and files / false: to copy folders and files
- _overwriteDestinationFolder_: The region name from AWS S3 region points. If defined will overwrite AWSRegion global configuration.
- _multiThreadFileTransferring_: true: to active multi thread file transfering  
    
```MoveS3Folders.exe bucketOrigin bucketDestination keyNameOrigin keyNameDestination moveFolder overwriteDestinationFolder```

### Example
```MoveS3Folders.exe magnadev-source magnadev-target thumb/oldfolder/02829 thumb/newfolder/02829 false true```
 
