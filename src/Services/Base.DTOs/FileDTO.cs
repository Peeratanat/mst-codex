using System.Threading.Tasks;
using FileStorage;
using Microsoft.Extensions.Configuration;

namespace Base.DTOs
{
    public class FileDTO
    {
        /// <summary>
        /// Url ของไฟล์
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// ชื่อไฟล์ (ที่เก็บบน DB)
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// ระบุว่าไฟล์อยู่ใน temp bucket
        /// </summary>
        /// <value><c>true</c> if is temp; otherwise, <c>false</c>.</value>
        public bool IsTemp { get; set; }

        public static async Task<FileDTO> CreateFromFileNameAsync(string name, FileHelper fileHelper, IConfiguration Configuration = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var url = await fileHelper.GetFileUrlAsync(name);
                var publicURL = "";
                if (Configuration != null)
                {
                    var endpoint = Configuration["Minio:Endpoint"];
                    var publicEndpoint = Configuration["Minio:PublicURL"];

                    publicURL = (!string.IsNullOrEmpty(publicEndpoint)) ? url.Replace(endpoint, publicEndpoint) : url;
                }

                var result = new FileDTO()
                {
                    //Set Data
                    Name = System.IO.Path.GetFileName(name),
                    Url = url, 
                };

                return result;
            }
            else
            {
                return null;
            }
        }


        public static async Task<FileDTO> CreateFromBucketandFileNameAsync(string bucket,string name, FileHelper fileHelper)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var url = await fileHelper.GetFileUrlAsync(bucket,name);
                var result = new FileDTO()
                {
                    //Set Data
                    Name = System.IO.Path.GetFileName(name),
                    Url = url
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
