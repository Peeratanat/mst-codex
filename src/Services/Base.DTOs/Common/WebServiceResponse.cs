using Newtonsoft.Json;

namespace Base.DTOs.Common
{
    public class WebServiceResponse<T>
    {
        [JsonProperty("status")]
        public WebServiceStatus Status { get; set; }
        [JsonProperty("data")]
        public T? Data { get; set; }

        public WebServiceResponse()
        {
            Status = new WebServiceStatus();
        }
    }

    public class WebServiceResModel
    {
        [JsonProperty("status")]
        public WebServiceStatus Status { get; set; }

        public WebServiceResModel()
        {
            Status = new WebServiceStatus();
        }
    }

    public class WebServiceStatus
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
