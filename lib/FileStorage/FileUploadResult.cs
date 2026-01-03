using System;
namespace FileStorage
{
    public class FileUploadResult
    {
        public string Name { get; set; }
        public string Url { get; set; }
        //public string PublicUrl { get; set; }
        public string BucketName { get; set; }
    }
}
