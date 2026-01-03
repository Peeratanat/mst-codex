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
    public class BrandDTO
    {
        /// <summary>
        /// รหัสแบรนด์
        /// </summary>
        /// <value>รหัสแบรนด์</value>
        [DataMember(Name = "brandNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "brandNo")]
        public string BrandNo { get; set; }

        /// <summary>
        /// ชื่อแบรนด์
        /// </summary>
        /// <value>ชื่อแบรนด์</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// รูปแบบเลขที่แปลง  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=UnitNumberFormat
        /// </summary>
        /// <value>รูปแบบเลขที่แปลง  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=UnitNumberFormat</value>
        [DataMember(Name = "unitNumberFormat", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "unitNumberFormat")]
        public MasterCenterDropdownDTO UnitNumberFormat { get; set; }

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
            sb.Append("class BrandDTO {\n");
            sb.Append("  BrandNo: ").Append(BrandNo).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  UnitNumberFormat: ").Append(UnitNumberFormat).Append("\n");
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
