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
    public class TypeOfRealEstateDTO
    {
        /// <summary>
        /// รหัสประเภทบ้าน
        /// </summary>
        /// <value>รหัสประเภทบ้าน</value>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// ชื่อประเภทบ้าน
        /// </summary>
        /// <value>ชื่อประเภทบ้าน</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// ลักษณะ  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=RealEstateCategory
        /// </summary>
        /// <value>ลักษณะ  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=RealEstateCategory</value>
        [DataMember(Name = "realEstateCategory", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "realEstateCategory")]
        public MasterCenterDropdownDTO RealEstateCategory { get; set; }

        /// <summary>
        /// Standard Cost
        /// </summary>
        /// <value>Standard Cost</value>
        [DataMember(Name = "standardCost", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "standardCost")]
        public double? StandardCost { get; set; }

        /// <summary>
        /// Standard Price
        /// </summary>
        /// <value>Standard Price</value>
        [DataMember(Name = "standardPrice", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "standardPrice")]
        public double? StandardPrice { get; set; }

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
            sb.Append("class TypeOfRealEstateDTO {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  RealEstateCategory: ").Append(RealEstateCategory).Append("\n");
            sb.Append("  StandardCost: ").Append(StandardCost).Append("\n");
            sb.Append("  StandardPrice: ").Append(StandardPrice).Append("\n");
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
