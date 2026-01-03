using Newtonsoft.Json;
using System.Collections.Generic;

namespace IO.Swagger.Client
{
    public class ValidationException
    {
        [JsonProperty("PopupErrors")]
        public List<ErrorMessage> PopupErrors { get; set; }

        [JsonProperty("FieldErrors")]
        public List<ErrorMessage> FieldErrors { get; set; }
    }

    public class ErrorMessage
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
