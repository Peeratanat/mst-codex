using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Model.MasterCenters
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class MasterCenterGroupListDTO
    {
        /// <summary>
        /// รหัส กลุ่มของข้อมูลทั่วไป
        /// </summary>
        /// <value>รหัส กลุ่มของข้อมูลทั่วไป</value>
        [DataMember(Name = "key", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// ชือ กลุ่มของข้อมูลทั่วไป
        /// </summary>
        /// <value>ชือ กลุ่มของข้อมูลทั่วไป</value>
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
            sb.Append("class MasterCenterGroupListDTO {\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
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
