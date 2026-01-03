using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.Common; 
using Common.Helper.HttpResultHelper;
using Microsoft.AspNetCore.Mvc; 
using Common.Helper.Logging;
using FileStorage;
namespace COM_File.API.Controllers
{
    [Route(HelperStatic.BaseAPIRoute)]
    [ApiController]
    public class FilesController : BaseController
    {
        public LogModel logModel { get; set; }
        private readonly IHttpResultHelper _httpResultHelper;
        public FilesController(IHttpResultHelper httpResultHelper)
        {
            logModel = new LogModel("FilesController", null);
            _httpResultHelper = httpResultHelper;
        }

        private int _expireHours = 24;


        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<FileDTO>>))]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            FileHelper fileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
            var stream = file.OpenReadStream();
            string objectName = $"{file.FileName}";
            var uploadResult =  await fileHelper.UploadFileFromStream(stream, "", objectName, file.ContentType);
             
            var result = new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url,
                IsTemp = true
            };
            return await _httpResultHelper.SuccessCustomResult(result, logModel);
        }

        [HttpPost("MultipleUpload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<FileDTO>>))]
        public async Task<IActionResult> MultipleUploadFile(List<IFormFile> files)
        {
            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            FileHelper FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
            var results = new List<FileDTO>();
            foreach (var file in files)
            {
                var stream = file.OpenReadStream();
                string objectName = $"{file.FileName}";
                var uploadResult = await FileHelper.UploadFileFromStream(stream, "", objectName, file.ContentType); 

                var result = new FileDTO()
                {
                    Name = uploadResult.Name,
                    Url = uploadResult.Url,
                    //PublicUrl = publicURL,
                    IsTemp = true
                };

                results.Add(result);
            }

            return await _httpResultHelper.SuccessCustomResult(results, logModel);
        }


    }
}
