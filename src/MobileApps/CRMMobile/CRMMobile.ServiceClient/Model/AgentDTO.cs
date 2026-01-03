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
    public class AgentDTO
    {
        /// <summary>
        /// ชื่อ Agent ภาษาไทย (TH)
        /// </summary>
        /// <value>ชื่อ Agent ภาษาไทย (TH)</value>
        [DataMember(Name = "nameTH", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameTH")]
        public string NameTH { get; set; }

        /// <summary>
        /// ชื่อ Agent อังกฤษ (EN)
        /// </summary>
        /// <value>ชื่อ Agent อังกฤษ (EN)</value>
        [DataMember(Name = "nameEN", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameEN")]
        public string NameEN { get; set; }

        /// <summary>
        /// ที่ตั้ง
        /// </summary>
        /// <value>ที่ตั้ง</value>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// ตึก
        /// </summary>
        /// <value>ตึก</value>
        [DataMember(Name = "building", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "building")]
        public string Building { get; set; }

        /// <summary>
        /// ซอย
        /// </summary>
        /// <value>ซอย</value>
        [DataMember(Name = "soi", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "soi")]
        public string Soi { get; set; }

        /// <summary>
        /// ถนน
        /// </summary>
        /// <value>ถนน</value>
        [DataMember(Name = "road", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "road")]
        public string Road { get; set; }

        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        /// <value>รหัสไปรษณีย์</value>
        [DataMember(Name = "postalCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// จังหวัด  masterdata/api/Provinces/DropdownList
        /// </summary>
        /// <value>จังหวัด  masterdata/api/Provinces/DropdownList</value>
        [DataMember(Name = "province", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "province")]
        public ProvinceListDTO Province { get; set; }

        /// <summary>
        /// อำเภอ/เขต  masterdata/api/Districts/DropdownList
        /// </summary>
        /// <value>อำเภอ/เขต  masterdata/api/Districts/DropdownList</value>
        [DataMember(Name = "district", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "district")]
        public DistrictListDTO District { get; set; }

        /// <summary>
        /// ตำบล/แขวง  masterdata/api/SubDistricts/DropdownList
        /// </summary>
        /// <value>ตำบล/แขวง  masterdata/api/SubDistricts/DropdownList</value>
        [DataMember(Name = "subDistrict", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "subDistrict")]
        public SubDistrictListDTO SubDistrict { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        /// <value>เบอร์โทรศัพท์</value>
        [DataMember(Name = "telNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "telNo")]
        public string TelNo { get; set; }

        /// <summary>
        /// เบอร์ Fax
        /// </summary>
        /// <value>เบอร์ Fax</value>
        [DataMember(Name = "faxNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "faxNo")]
        public string FaxNo { get; set; }

        /// <summary>
        /// เว็บไซต์
        /// </summary>
        /// <value>เว็บไซต์</value>
        [DataMember(Name = "website", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "website")]
        public string Website { get; set; }

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
            sb.Append("class AgentDTO {\n");
            sb.Append("  NameTH: ").Append(NameTH).Append("\n");
            sb.Append("  NameEN: ").Append(NameEN).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  Building: ").Append(Building).Append("\n");
            sb.Append("  Soi: ").Append(Soi).Append("\n");
            sb.Append("  Road: ").Append(Road).Append("\n");
            sb.Append("  PostalCode: ").Append(PostalCode).Append("\n");
            sb.Append("  Province: ").Append(Province).Append("\n");
            sb.Append("  District: ").Append(District).Append("\n");
            sb.Append("  SubDistrict: ").Append(SubDistrict).Append("\n");
            sb.Append("  TelNo: ").Append(TelNo).Append("\n");
            sb.Append("  FaxNo: ").Append(FaxNo).Append("\n");
            sb.Append("  Website: ").Append(Website).Append("\n");
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
