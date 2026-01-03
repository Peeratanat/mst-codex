using Newtonsoft.Json;

namespace Base.DTOs.Auth
{
    public class IntrospecReqDTO
    {
        [JsonProperty("token")]
        public string? Token { get; set; }
    }
}
