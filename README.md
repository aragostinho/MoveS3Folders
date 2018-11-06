# MoveS3Folders
A simple console program in C# .Net 4.5 to move entire folders (keynames) in AWS S3.
This tool can move or copy easy all content inside (files/subfolders) from a bucket/keyname to another bucket/keyname.
His implementation use S3DirectoryInfo Class from namespace Amazon.S3.IO. 

For more details: https://docs.aws.amazon.com/sdkfornet1/latest/apidocs/html/T_Amazon_S3_IO_S3DirectoryInfo.htm

## Global configuration (app.config)
- AWSAccessKey:  Your access key supplied from IAM Role
- AWSSecretKey:  Your secret key supplied from IAM Role
- AWSRegion:  The region name from AWS S3 region points (ex: sa-east-1)
    
## Pre-defined configuration (app.config)
- BucketOrigin:  The source bucket where folders/files are stored.
- BucketDestination: The target bucket where folders/files will be move/copy.
- KeyNameOrigin:  The full keyname (folder) from source bucket
- KeyNameDestination: The full keyname (folder) from target bucket.  It's possible to change the path destination.
- MoveFolder:  if true move folders and files, if false copy folders and files
- OverwriteDestinationFolder: if true removed folder in destination before copy/move folder. There's no overwrite method in S3DirectoryInfo class.
    
## On-demand command line (MoveS3Folders.exe -args)
All args below will overwrite Pre-defined configuration:
- bucketOrigin:  The source bucket where folders/files are stored.
- bucketDestination: The target bucket where folders/files will be move/copy.
- keyNameOrigin:  The full keyname (folder) from source bucket
- keyNameDestination: The full keyname (folder) from target bucket.  It's possible to change the path destination.
- moveFolder:  true:  to move folders and files / false: to copy folders and files
- overwriteDestinationFolder: The region name from AWS S3 region points. If defined will overwrite AWSRegion global configuration.

```MoveS3Folders.exe bucketOrigin bucketDestination keyNameOrigin keyNameDestination moveFolder overwriteDestinationFolder```

### Example
```MoveS3Folders.exe magnadev-source magnadev-target thumb/oldfolder/02829 thumb/newfolder/02829 false true```
 
