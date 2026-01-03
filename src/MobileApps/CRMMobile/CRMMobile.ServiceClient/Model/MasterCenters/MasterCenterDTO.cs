using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Model.MasterCenters
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class MasterCenterDTO
    {
        /// <summary>
        /// กลุ่มของข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <value>กลุ่มของข้อมูลพื้นฐานทั่วไป</value>
        [DataMember(Name = "masterCenterGroup", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "masterCenterGroup")]
        public MasterCenterGroupListDTO MasterCenterGroup { get; set; }

        /// <summary>
        /// ลำดับ
        /// </summary>
        /// <value>ลำดับ</value>
        [DataMember(Name = "order", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "order")]
        public int? Order { get; set; }

        /// <summary>
        /// ชื่อ
        /// </summary>
        /// <value>ชื่อ</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// รหัส
        /// </summary>
        /// <value>รหัส</value>
        [DataMember(Name = "key", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// Active อยู่หรือไม่
        /// </summary>
        /// <value>Active อยู่หรือไม่</value>
        [DataMember(Name = "isActive", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isActive")]
        public bool? IsActive { get; set; }

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
            sb.Append("class MasterCenterDTO {\n");
            sb.Append("  MasterCenterGroup: ").Append(MasterCenterGroup).Append("\n");
            sb.Append("  Order: ").Append(Order).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  IsActive: ").Append(IsActive).Append("\n");
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
