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
    public class BankBranchDTO
    {
        /// <summary>
        /// ธนาคาร  Master/Banks/DropdownList
        /// </summary>
        /// <value>ธนาคาร  Master/Banks/DropdownList</value>
        [DataMember(Name = "bank", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bank")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// ชื่อสาขา
        /// </summary>
        /// <value>ชื่อสาขา</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// ที่อยู่
        /// </summary>
        /// <value>ที่อยู่</value>
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
        /// Master/Provinces/DropdownList
        /// </summary>
        /// <value>Master/Provinces/DropdownList</value>
        [DataMember(Name = "province", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "province")]
        public ProvinceListDTO Province { get; set; }

        /// <summary>
        /// รหัสไปรษ์ณีย์
        /// </summary>
        /// <value>รหัสไปรษ์ณีย์</value>
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
        /// บัญชี Credit
        /// </summary>
        /// <value>บัญชี Credit</value>
        [DataMember(Name = "isCreditBank", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isCreditBank")]
        public bool? IsCreditBank { get; set; }

        /// <summary>
        /// บัญชี Direct Debit
        /// </summary>
        /// <value>บัญชี Direct Debit</value>
        [DataMember(Name = "isDirectDebit", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isDirectDebit")]
        public bool? IsDirectDebit { get; set; }

        /// <summary>
        /// บัญชี Direct Credit
        /// </summary>
        /// <value>บัญชี Direct Credit</value>
        [DataMember(Name = "isDirectCredit", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isDirectCredit")]
        public bool? IsDirectCredit { get; set; }

        /// <summary>
        /// รหัสสาขา
        /// </summary>
        /// <value>รหัสสาขา</value>
        [DataMember(Name = "areaCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "areaCode")]
        public string AreaCode { get; set; }

        /// <summary>
        /// รหัส ID แบงค์อันเก่า
        /// </summary>
        /// <value>รหัส ID แบงค์อันเก่า</value>
        [DataMember(Name = "oldBankID", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "oldBankID")]
        public string OldBankID { get; set; }

        /// <summary>
        /// รหัสสาขาอันเก่า
        /// </summary>
        /// <value>รหัสสาขาอันเก่า</value>
        [DataMember(Name = "oldBranchID", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "oldBranchID")]
        public string OldBranchID { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        /// <value>IsActive</value>
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
            sb.Append("class BankBranchDTO {\n");
            sb.Append("  Bank: ").Append(Bank).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  Building: ").Append(Building).Append("\n");
            sb.Append("  Soi: ").Append(Soi).Append("\n");
            sb.Append("  Road: ").Append(Road).Append("\n");
            sb.Append("  SubDistrict: ").Append(SubDistrict).Append("\n");
            sb.Append("  District: ").Append(District).Append("\n");
            sb.Append("  Province: ").Append(Province).Append("\n");
            sb.Append("  PostalCode: ").Append(PostalCode).Append("\n");
            sb.Append("  Telephone: ").Append(Telephone).Append("\n");
            sb.Append("  Fax: ").Append(Fax).Append("\n");
            sb.Append("  IsCreditBank: ").Append(IsCreditBank).Append("\n");
            sb.Append("  IsDirectDebit: ").Append(IsDirectDebit).Append("\n");
            sb.Append("  IsDirectCredit: ").Append(IsDirectCredit).Append("\n");
            sb.Append("  AreaCode: ").Append(AreaCode).Append("\n");
            sb.Append("  OldBankID: ").Append(OldBankID).Append("\n");
            sb.Append("  OldBranchID: ").Append(OldBranchID).Append("\n");
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
