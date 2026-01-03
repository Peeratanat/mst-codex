using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class LoginParam
    {
        /// <summary>
        /// Gets or Sets GrantType
        /// </summary>
        [DataMember(Name = "grant_type", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get; set; }

        /// <summary>
        /// Gets or Sets Username
        /// </summary>
        [DataMember(Name = "username", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        [DataMember(Name = "password", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets RefreshToken
        /// </summary>
        [DataMember(Name = "refresh_token", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LoginParam {\n");
            sb.Append("  GrantType: ").Append(GrantType).Append("\n");
            sb.Append("  Username: ").Append(Username).Append("\n");
            sb.Append("  Password: ").Append(Password).Append("\n");
            sb.Append("  RefreshToken: ").Append(RefreshToken).Append("\n");
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
