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
    public class LandOfficeDTO
    {
        /// <summary>
        /// ชื่อสำนักงานที่ดิน ภาษาไทย
        /// </summary>
        /// <value>ชื่อสำนักงานที่ดิน ภาษาไทย</value>
        [DataMember(Name = "nameTH", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameTH")]
        public string NameTH { get; set; }

        /// <summary>
        /// ชื่อสำนักงานที่ดิน ภาษาอังกฤษ
        /// </summary>
        /// <value>ชื่อสำนักงานที่ดิน ภาษาอังกฤษ</value>
        [DataMember(Name = "nameEN", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameEN")]
        public string NameEN { get; set; }

        /// <summary>
        /// จังหวัด  masterdata/api/Provinces/DropdownList
        /// </summary>
        /// <value>จังหวัด  masterdata/api/Provinces/DropdownList</value>
        [DataMember(Name = "province", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "province")]
        public ProvinceListDTO Province { get; set; }

        /// <summary>
        /// อำเภอ  masterdata/api/Districts/DropdownList
        /// </summary>
        /// <value>อำเภอ  masterdata/api/Districts/DropdownList</value>
        [DataMember(Name = "district", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "district")]
        public DistrictListDTO District { get; set; }

        /// <summary>
        /// ตำบล  masterdata/api/SubDistricts/DropdownList
        /// </summary>
        /// <value>ตำบล  masterdata/api/SubDistricts/DropdownList</value>
        [DataMember(Name = "subDistrict", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "subDistrict")]
        public SubDistrictListDTO SubDistrict { get; set; }

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
            sb.Append("class LandOfficeDTO {\n");
            sb.Append("  NameTH: ").Append(NameTH).Append("\n");
            sb.Append("  NameEN: ").Append(NameEN).Append("\n");
            sb.Append("  Province: ").Append(Province).Append("\n");
            sb.Append("  District: ").Append(District).Append("\n");
            sb.Append("  SubDistrict: ").Append(SubDistrict).Append("\n");
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
