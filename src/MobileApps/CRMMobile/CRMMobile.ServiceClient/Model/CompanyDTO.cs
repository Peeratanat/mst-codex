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
    public class CompanyDTO
    {
        /// <summary>
        /// รหัสริษัท
        /// </summary>
        /// <value>รหัสริษัท</value>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// ชื่อบริษัทภาษาไทย
        /// </summary>
        /// <value>ชื่อบริษัทภาษาไทย</value>
        [DataMember(Name = "nameTH", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameTH")]
        public string NameTH { get; set; }

        /// <summary>
        /// ชื่อบริษัทภาษาอังกฤษ
        /// </summary>
        /// <value>ชื่อบริษัทภาษาอังกฤษ</value>
        [DataMember(Name = "nameEN", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameEN")]
        public string NameEN { get; set; }

        /// <summary>
        /// ประจำตัวผู้เสียภาษี
        /// </summary>
        /// <value>ประจำตัวผู้เสียภาษี</value>
        [DataMember(Name = "taxID", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "taxID")]
        public string TaxID { get; set; }

        /// <summary>
        /// ที่อยู่ภาษาไทย
        /// </summary>
        /// <value>ที่อยู่ภาษาไทย</value>
        [DataMember(Name = "addressTH", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "addressTH")]
        public string AddressTH { get; set; }

        /// <summary>
        /// ที่อยู่ภาษาอังกฤษ
        /// </summary>
        /// <value>ที่อยู่ภาษาอังกฤษ</value>
        [DataMember(Name = "addressEN", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "addressEN")]
        public string AddressEN { get; set; }

        /// <summary>
        /// ชื่อตึกภาษาไทย
        /// </summary>
        /// <value>ชื่อตึกภาษาไทย</value>
        [DataMember(Name = "buildingTH", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "buildingTH")]
        public string BuildingTH { get; set; }

        /// <summary>
        /// ชื่อตึกภาษาอังกฤษ
        /// </summary>
        /// <value>ชื่อตึกภาษาอังกฤษ</value>
        [DataMember(Name = "buildingEN", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "buildingEN")]
        public string BuildingEN { get; set; }

        /// <summary>
        /// ซอยภาษาไทย
        /// </summary>
        /// <value>ซอยภาษาไทย</value>
        [DataMember(Name = "soiTH", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "soiTH")]
        public string SoiTH { get; set; }

        /// <summary>
        /// ซอยภาษาอังกฤษ
        /// </summary>
        /// <value>ซอยภาษาอังกฤษ</value>
        [DataMember(Name = "soiEN", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "soiEN")]
        public string SoiEN { get; set; }

        /// <summary>
        /// ถนนภาษาไทย
        /// </summary>
        /// <value>ถนนภาษาไทย</value>
        [DataMember(Name = "roadTH", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "roadTH")]
        public string RoadTH { get; set; }

        /// <summary>
        /// ถนนภาษาอังกฤษ
        /// </summary>
        /// <value>ถนนภาษาอังกฤษ</value>
        [DataMember(Name = "roadEN", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "roadEN")]
        public string RoadEN { get; set; }

        /// <summary>
        /// Master/SubDistricts/DropdownList  ตำบล
        /// </summary>
        /// <value>Master/SubDistricts/DropdownList  ตำบล</value>
        [DataMember(Name = "subDistrict", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "subDistrict")]
        public SubDistrictListDTO SubDistrict { get; set; }

        /// <summary>
        /// Master/Districts/DropdownList  อำเภอ
        /// </summary>
        /// <value>Master/Districts/DropdownList  อำเภอ</value>
        [DataMember(Name = "district", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "district")]
        public DistrictListDTO District { get; set; }

        /// <summary>
        /// Master/Provinces/DropdownList  จังหวัด
        /// </summary>
        /// <value>Master/Provinces/DropdownList  จังหวัด</value>
        [DataMember(Name = "province", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "province")]
        public ProvinceListDTO Province { get; set; }

        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        /// <value>รหัสไปรษณีย์</value>
        [DataMember(Name = "postalCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// เบอร์โทร
        /// </summary>
        /// <value>เบอร์โทร</value>
        [DataMember(Name = "telephone", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "telephone")]
        public string Telephone { get; set; }

        /// <summary>
        /// เบอร์แฟ๊กซ์
        /// </summary>
        /// <value>เบอร์แฟ๊กซ์</value>
        [DataMember(Name = "fax", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "fax")]
        public string Fax { get; set; }

        /// <summary>
        /// เว๊ปไซส์
        /// </summary>
        /// <value>เว๊ปไซส์</value>
        [DataMember(Name = "website", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "website")]
        public string Website { get; set; }

        /// <summary>
        /// รหัสบริษัทใน SAP
        /// </summary>
        /// <value>รหัสบริษัทใน SAP</value>
        [DataMember(Name = "sapCompanyID", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "sapCompanyID")]
        public string SapCompanyID { get; set; }

        /// <summary>
        /// ชื่อเก่าภาษาไทย
        /// </summary>
        /// <value>ชื่อเก่าภาษาไทย</value>
        [DataMember(Name = "nameTHOld", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameTHOld")]
        public string NameTHOld { get; set; }

        /// <summary>
        /// ชื่อเก่าภาษอังกฤษ
        /// </summary>
        /// <value>ชื่อเก่าภาษอังกฤษ</value>
        [DataMember(Name = "nameENOld", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameENOld")]
        public string NameENOld { get; set; }

        /// <summary>
        /// เปิดใช้ที่ระบบ CRM
        /// </summary>
        /// <value>เปิดใช้ที่ระบบ CRM</value>
        [DataMember(Name = "isUseInCRM", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isUseInCRM")]
        public bool? IsUseInCRM { get; set; }

        /// <summary>
        /// สถานะบริษัท
        /// </summary>
        /// <value>สถานะบริษัท</value>
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
            sb.Append("class CompanyDTO {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  NameTH: ").Append(NameTH).Append("\n");
            sb.Append("  NameEN: ").Append(NameEN).Append("\n");
            sb.Append("  TaxID: ").Append(TaxID).Append("\n");
            sb.Append("  AddressTH: ").Append(AddressTH).Append("\n");
            sb.Append("  AddressEN: ").Append(AddressEN).Append("\n");
            sb.Append("  BuildingTH: ").Append(BuildingTH).Append("\n");
            sb.Append("  BuildingEN: ").Append(BuildingEN).Append("\n");
            sb.Append("  SoiTH: ").Append(SoiTH).Append("\n");
            sb.Append("  SoiEN: ").Append(SoiEN).Append("\n");
            sb.Append("  RoadTH: ").Append(RoadTH).Append("\n");
            sb.Append("  RoadEN: ").Append(RoadEN).Append("\n");
            sb.Append("  SubDistrict: ").Append(SubDistrict).Append("\n");
            sb.Append("  District: ").Append(District).Append("\n");
            sb.Append("  Province: ").Append(Province).Append("\n");
            sb.Append("  PostalCode: ").Append(PostalCode).Append("\n");
            sb.Append("  Telephone: ").Append(Telephone).Append("\n");
            sb.Append("  Fax: ").Append(Fax).Append("\n");
            sb.Append("  Website: ").Append(Website).Append("\n");
            sb.Append("  SapCompanyID: ").Append(SapCompanyID).Append("\n");
            sb.Append("  NameTHOld: ").Append(NameTHOld).Append("\n");
            sb.Append("  NameENOld: ").Append(NameENOld).Append("\n");
            sb.Append("  IsUseInCRM: ").Append(IsUseInCRM).Append("\n");
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
