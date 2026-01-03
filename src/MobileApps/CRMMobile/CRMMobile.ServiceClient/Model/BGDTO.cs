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
    public class BGDTO
    {
        /// <summary>
        /// รหัส BG
        /// </summary>
        /// <value>รหัส BG</value>
        [DataMember(Name = "bgNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bgNo")]
        public string BgNo { get; set; }

        /// <summary>
        /// ชื่อ BG
        /// </summary>
        /// <value>ชื่อ BG</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// สำหรับประเภทของโครงการ (แนวสูง แนวราบ)  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ProductType
        /// </summary>
        /// <value>สำหรับประเภทของโครงการ (แนวสูง แนวราบ)  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ProductType</value>
        [DataMember(Name = "productType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "productType")]
        public MasterCenterDropdownDTO ProductType { get; set; }

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
            sb.Append("class BGDTO {\n");
            sb.Append("  BgNo: ").Append(BgNo).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  ProductType: ").Append(ProductType).Append("\n");
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
