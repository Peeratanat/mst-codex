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
    public class MasterCenterGroupDTO
    {
        /// <summary>
        /// รหัส กลุ่มของข้อมูลทั่วไป
        /// </summary>
        /// <value>รหัส กลุ่มของข้อมูลทั่วไป</value>
        [DataMember(Name = "key", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// ชื่อ กลุ่มของข้อมูลทั่วไป
        /// </summary>
        /// <value>ชื่อ กลุ่มของข้อมูลทั่วไป</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy
        /// </summary>
        [DataMember(Name = "updatedBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "updatedBy")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Updated
        /// </summary>
        [DataMember(Name = "updated", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "updated")]
        public DateTime? Updated { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MasterCenterGroupDTO {\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  UpdatedBy: ").Append(UpdatedBy).Append("\n");
            sb.Append("  Updated: ").Append(Updated).Append("\n");
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
