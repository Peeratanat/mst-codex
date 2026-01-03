using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class JsonWebToken
    {
        /// <summary>
        /// Gets or Sets Token
        /// </summary>
        [DataMember(Name = "token", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or Sets Expires
        /// </summary>
        [DataMember(Name = "expires", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "expires")]
        public long? Expires { get; set; }

        /// <summary>
        /// Gets or Sets ExpiresIn
        /// </summary>
        [DataMember(Name = "expires_in", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "expires_in")]
        public long? ExpiresIn { get; set; }

        /// <summary>
        /// Gets or Sets RefreshToken
        /// </summary>
        [DataMember(Name = "refresh_token", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or Sets UserId
        /// </summary>
        [DataMember(Name = "user_id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "user_id")]
        public Guid? UserId { get; set; }

        /// <summary>
        /// Gets or Sets DisplayName
        /// </summary>
        [DataMember(Name = "display_name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class JsonWebToken {\n");
            sb.Append("  Token: ").Append(Token).Append("\n");
            sb.Append("  Expires: ").Append(Expires).Append("\n");
            sb.Append("  ExpiresIn: ").Append(ExpiresIn).Append("\n");
            sb.Append("  RefreshToken: ").Append(RefreshToken).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
