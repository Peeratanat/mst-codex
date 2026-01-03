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
    public class SubBGDropdownDTO
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// รหัส Sub BG
        /// </summary>
        /// <value>รหัส Sub BG</value>
        [DataMember(Name = "subBGNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "subBGNo")]
        public string SubBGNo { get; set; }

        /// <summary>
        /// ชื่อ Sub BG
        /// </summary>
        /// <value>ชื่อ Sub BG</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SubBGDropdownDTO {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  SubBGNo: ").Append(SubBGNo).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
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
