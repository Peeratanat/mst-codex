 
using Minio;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileStorage
{

    public class FileHelper
    {
        private int _expireHours = 24;
        public string _minioEndpoint;
        public string _minioAccessKey;
        public string _minioSecretKey;
        public string _defaultBucket;
        public string _tempBucket;
        private bool _withSSL;
        private string _publicURL;

        public FileHelper(string minioEndpoint, string minioAccessKey, string minioSecretKey, string defaultBucket, string tempBucket, string publicURL, bool withSSL)
        {
            _minioEndpoint = minioEndpoint;
            _minioAccessKey = minioAccessKey;
            _minioSecretKey = minioSecretKey;
            _defaultBucket = defaultBucket;
            _tempBucket = tempBucket;
            _publicURL = publicURL;
            _withSSL = withSSL;
        }

        public async Task MoveTempFileAsync(string sourceObjectName, string destObjectName)
        {
            IMinioClient minio;
            minio = new MinioClient().WithEndpoint(_minioEndpoint)
                                       .WithCredentials(_minioAccessKey, _minioSecretKey)
                                       .WithSSL(_withSSL).Build();
             
            var cpSrcArgs = new CopySourceObjectArgs()
                .WithBucket(_tempBucket)
                .WithObject(sourceObjectName) ;
            var args = new CopyObjectArgs()
               .WithBucket(_defaultBucket)
               .WithObject(destObjectName)
               .WithCopyObjectSource(cpSrcArgs) ;
            await minio.CopyObjectAsync(args).ConfigureAwait(true);

        }

        public async Task<string> GetFileUrlAsync(string name)
        {
            IMinioClient minio;
            minio = new MinioClient().WithEndpoint(_minioEndpoint)
                                       .WithCredentials(_minioAccessKey, _minioSecretKey)
                                       .WithSSL(_withSSL).Build();
            PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                                      .WithBucket(_defaultBucket)
                                      .WithObject(name)
                                      .WithExpiry((int)TimeSpan.FromHours(_expireHours).TotalSeconds);
            var url = await minio.PresignedGetObjectAsync(args);

            url = ReplaceWithPublicURL(url);

            return url;
        }

        public async Task<string> GetFileUrlAsync(string bucket, string name, bool replaceUrl = true)
        {
            IMinioClient minio;
            minio = new MinioClient().WithEndpoint(_minioEndpoint)
                                       .WithCredentials(_minioAccessKey, _minioSecretKey)
                                       .WithSSL(_withSSL).Build();
            PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                                      .WithBucket(bucket)
                                      .WithObject(name)
                                      .WithExpiry((int)TimeSpan.FromHours(_expireHours).TotalSeconds);
            var url = await minio.PresignedGetObjectAsync(args);
            //url = (!string.IsNullOrEmpty(_publicURL)) ? url.Replace(_minioEndpoint, _publicURL) : url;
            if (replaceUrl)
            {
                url = ReplaceWithPublicURL(url);
            }

            return url;
        }

        public async Task<Stream?> GetStreamFromUrlAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadAsStreamAsync();
                else
                    return null;
            }
        }

        public async Task<FileUploadResult> UploadFileFromStreamWithTimestamp(Stream fileStream, string filePath, string fileName, string contentType, string timestamp = "", bool hasRVType = false)
        {
            IMinioClient minio;
            minio = new MinioClient().WithEndpoint(_minioEndpoint)
                                       .WithCredentials(_minioAccessKey, _minioSecretKey)
                                       .WithSSL(_withSSL).Build();
            var args = new BucketExistsArgs()
                .WithBucket(_defaultBucket);
            bool bucketExisted = await minio.BucketExistsAsync(args).ConfigureAwait(true);

            if (!bucketExisted)
                await minio.MakeBucketAsync( new MakeBucketArgs().WithBucket(_defaultBucket)).ConfigureAwait(true);

            timestamp = string.IsNullOrEmpty(timestamp) ? Convert.ToString(DateTime.Today.Year) + DateTime.Now.ToString("MMdd_HHmmss") : timestamp;

            string objectName;

            var fileObj = fileName.Split(".");

            if (fileObj.Length > 1)
                objectName = hasRVType ? $"{fileObj[0]}.{fileObj[1]}" : $"{fileObj[0]}_{timestamp}.{fileObj[1]}";
            else
                objectName = hasRVType ? $"{fileName}" : $"{fileName}_{timestamp}";

            objectName = Path.Combine(filePath, objectName);
            objectName = objectName.Replace('\\', '/');
            var putObjectargs = new PutObjectArgs()
                .WithBucket(_defaultBucket)
                .WithObject(objectName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType);

            await minio.PutObjectAsync(putObjectargs);

            PresignedGetObjectArgs presignedGetObjectArg = new PresignedGetObjectArgs()
                                      .WithBucket(_defaultBucket)
                                      .WithObject(objectName)
                                      .WithExpiry((int)TimeSpan.FromHours(_expireHours).TotalSeconds);

            var url = await minio.PresignedGetObjectAsync(presignedGetObjectArg);

            return new FileUploadResult()
            {
                Name = objectName,
                BucketName = _defaultBucket,
                Url = url
            };
        }

        public async Task<FileUploadResult> UploadFileFromStream(Stream fileStream, string filePath, string fileName, string contentType)
        {
            IMinioClient minio;
            minio = new MinioClient().WithEndpoint(_minioEndpoint)
                                       .WithCredentials(_minioAccessKey, _minioSecretKey)
                                       .WithSSL(_withSSL).Build();
            var args = new BucketExistsArgs()
                .WithBucket(_defaultBucket);
            bool bucketExisted = await minio.BucketExistsAsync(args).ConfigureAwait(true);

            if (!bucketExisted)
                await minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(_defaultBucket)).ConfigureAwait(true);

            string objectName = $"{Guid.NewGuid().ToString()}_{fileName}";
            objectName = Path.Combine(filePath, objectName);
            objectName = objectName.Replace('\\', '/');
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var putObjectargs = new PutObjectArgs()
                       .WithBucket(_defaultBucket)
                       .WithObject(objectName)
                       .WithStreamData(fileStream)
                       .WithObjectSize(fileStream.Length)
                       .WithContentType(contentType);
                    await minio.PutObjectAsync(putObjectargs); 
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(2000);
                    if (i == 2)
                    {
                        throw;
                    }
                }
            }
            // expire in 1 day
            PresignedGetObjectArgs presignedGetObjectArg = new PresignedGetObjectArgs()
                          .WithBucket(_defaultBucket)
                          .WithObject(objectName)
                          .WithExpiry((int)TimeSpan.FromHours(_expireHours).TotalSeconds);

            var url = await minio.PresignedGetObjectAsync(presignedGetObjectArg); 

            string env = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "").ToLower();
            if (env.ToLower() == "prd" || env.ToLower() == "production")
            {
                var pathUrl = url.Split("/");
                string publicUrl = "";
                if (pathUrl.Length > 2)
                {
                    publicUrl = "http://" + pathUrl[2];
                }
                url = url.Replace(publicUrl, _publicURL);
            }
            else
            {
                //url = ReplaceWithPublicURL(url);
            }
            return new FileUploadResult()
            {
                Name = objectName,
                BucketName = _defaultBucket,
                Url = url
            };
        }

        public async Task<FileUploadResult> UploadFileFromStreamWithOutGuid(Stream fileStream, string bucketName, string filePath, string fileName, string contentType)
        {
            IMinioClient minio;
            minio = new MinioClient().WithEndpoint(_minioEndpoint)
                                       .WithCredentials(_minioAccessKey, _minioSecretKey)
                                       .WithSSL(_withSSL).Build();
            if (string.IsNullOrEmpty(bucketName))
                bucketName = _defaultBucket;
            var args = new BucketExistsArgs()
                .WithBucket(bucketName);
            bool bucketExisted = await minio.BucketExistsAsync(args).ConfigureAwait(true);

            if (!bucketExisted)
                await minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName)).ConfigureAwait(true);

            string objectName = fileName;
            objectName = Path.Combine(filePath, objectName);
            objectName = objectName.Replace('\\', '/');
            var putObjectargs = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType);

            await minio.PutObjectAsync(putObjectargs);
            // expire in 1 day
            PresignedGetObjectArgs presignedGetObjectArg = new PresignedGetObjectArgs()
                          .WithBucket(bucketName)
                          .WithObject(objectName)
                          .WithExpiry((int)TimeSpan.FromHours(_expireHours).TotalSeconds);

            var url = await minio.PresignedGetObjectAsync(presignedGetObjectArg);
            string env = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "").ToLower();
            if (env.ToLower() == "prd" || env.ToLower() == "production")
            {
                var pathUrl = url.Split("/");
                string publicUrl = "";
                if (pathUrl.Length > 2)
                {
                    publicUrl = "http://" + pathUrl[2];
                }
                url = url.Replace(publicUrl, _publicURL);
            }
            else
            {
                //url = ReplaceWithPublicURL(url);
            }
            return new FileUploadResult()
            {
                Name = objectName,
                BucketName = bucketName,
                Url = url
            };
        }

        public async Task<List<string>?> GetListFile(string bucketName, string prefix)
        {
            IMinioClient minio;
            minio = new MinioClient().WithEndpoint(_minioEndpoint)
                                       .WithCredentials(_minioAccessKey, _minioSecretKey)
                                       .WithSSL(_withSSL).Build();
            var args = new BucketExistsArgs()
                .WithBucket(_defaultBucket);
            bool bucketExisted = await minio.BucketExistsAsync(args).ConfigureAwait(true);
             
            if (bucketExisted)
            {
                List<string> bucketKeys = new List<string>();
                var listArgs = new ListObjectsArgs()
                .WithBucket(bucketName)
                .WithPrefix(prefix)
                .WithRecursive(true)
                .WithVersions(false);
                 
                await foreach (var item in minio.ListObjectsEnumAsync(listArgs).ConfigureAwait(false))
                {
                    bucketKeys.Add(item.Key);
                }
                 

                return bucketKeys;
            }
            else
            {
                return null;
            }
        }

        public async Task MoveFileAsync(string sourceBucket, string sourceObjectName, string destBucket, string destObjectName)
        {
            IMinioClient minio;
            minio = new MinioClient().WithEndpoint(_minioEndpoint)
                                       .WithCredentials(_minioAccessKey, _minioSecretKey)
                                       .WithSSL(_withSSL).Build();
            var cpSrcArgs = new CopySourceObjectArgs()
                     .WithBucket(sourceBucket)
                     .WithObject(sourceObjectName);
            var args = new CopyObjectArgs()
                    .WithBucket(destBucket)
                    .WithObject(destObjectName)
                    .WithCopyObjectSource(cpSrcArgs);
                                await minio.CopyObjectAsync(args).ConfigureAwait(true);
        }

        public async Task MoveAndRemoveFileAsync(string sourceBucket, string sourceObjectName, string destBucket, string destObjectName)
        {
            IMinioClient minio;
            minio = new MinioClient().WithEndpoint(_minioEndpoint)
                                       .WithCredentials(_minioAccessKey, _minioSecretKey)
                                       .WithSSL(_withSSL).Build();
            var cpSrcArgs = new CopySourceObjectArgs()
                                .WithBucket(sourceBucket)
                                .WithObject(sourceObjectName);
                                        var args = new CopyObjectArgs()
               .WithBucket(destBucket)
               .WithObject(destObjectName)
               .WithCopyObjectSource(cpSrcArgs);
            await minio.CopyObjectAsync(args).ConfigureAwait(true);
            var removeargs = new RemoveObjectArgs()
            .WithBucket(sourceBucket)
                .WithObject(sourceObjectName);
            await minio.RemoveObjectAsync(removeargs).ConfigureAwait(true);
        }

        public static string GetApplicationRootPath()
        {
            //var result = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase); 
            var result = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            result = result.Replace("file:\\", string.Empty);
            result = result.Replace("file:", string.Empty);
            return result;
        }

        public async Task<string> DownLoadToTempFileAsync(string bucketName, string prefix, string filename)
        {
            string pathTempFile = Path.GetTempPath();
            string tempFilename = Guid.NewGuid() + "_" + filename;
            IMinioClient minio;
            minio = new MinioClient().WithEndpoint(_minioEndpoint)
                                       .WithCredentials(_minioAccessKey, _minioSecretKey)
                                       .WithSSL(_withSSL).Build();
            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(prefix)
                .WithFile(filename);
            await minio.GetObjectAsync(args).ConfigureAwait(true);
                                    //(stream) =>
                                    //{
                                    //    using (Stream fs = File.OpenWrite(pathTempFile + tempFilename))
                                    //    {
                                    //        stream.CopyTo(fs);
                                    //    }
                                    //});
            return pathTempFile + tempFilename;
        }

        private string ReplaceWithPublicURL(string url)
        {
            //if (!string.IsNullOrEmpty(_publicURL))
            //{
            //    url = url.Replace("https://", "");
            //    url = url.Replace("http://", "");

            //    url = url.Replace(_minioEndpoint, _publicURL);
            //}
            return url;
        }

        public string GetFileUrl(string name)
        {
            return GetFileUrlAsync(name).Result.ToString();
        }

        public string GetFileUrl(string bucket, string name)
        {
            return GetFileUrlAsync(bucket, name).Result.ToString();
        }
    }
     
}
