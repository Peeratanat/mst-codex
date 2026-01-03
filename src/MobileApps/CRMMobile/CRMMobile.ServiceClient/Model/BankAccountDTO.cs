using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Model
{

    /// <summary>
    /// ข้อมูลบัญชีธนาคาร  Model &#x3D; BankAccount
    /// </summary>
    [DataContract]
    public class BankAccountDTO
    {
        /// <summary>
        /// บริษัท   Master/api/Companies/DropdownList
        /// </summary>
        /// <value>บริษัท   Master/api/Companies/DropdownList</value>
        [DataMember(Name = "company", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "company")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// ธนาคาร  Master/api/Banks/DropdownList
        /// </summary>
        /// <value>ธนาคาร  Master/api/Banks/DropdownList</value>
        [DataMember(Name = "bank", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bank")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// สาขาธนาคาร  Master/api/BankBranchs/DropdownList
        /// </summary>
        /// <value>สาขาธนาคาร  Master/api/BankBranchs/DropdownList</value>
        [DataMember(Name = "bankBranch", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bankBranch")]
        public BankBranchDropdownDTO BankBranch { get; set; }

        /// <summary>
        /// จังหวัด  Master/Provinces/DropdownList
        /// </summary>
        /// <value>จังหวัด  Master/Provinces/DropdownList</value>
        [DataMember(Name = "province", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "province")]
        public ProvinceListDTO Province { get; set; }

        /// <summary>
        /// ประเภทบัญชี  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=BankAccountType
        /// </summary>
        /// <value>ประเภทบัญชี  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=BankAccountType</value>
        [DataMember(Name = "bankAccountType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bankAccountType")]
        public MasterCenterDropdownDTO BankAccountType { get; set; }

        /// <summary>
        /// เลขที่บัญชี GL
        /// </summary>
        /// <value>เลขที่บัญชี GL</value>
        [DataMember(Name = "glAccountNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "glAccountNo")]
        public string GlAccountNo { get; set; }

        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        /// <value>เลขที่บัญชี</value>
        [DataMember(Name = "bankAccountNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bankAccountNo")]
        public string BankAccountNo { get; set; }

        /// <summary>
        /// บัญชีเงินโอนผ่านธนาคาร
        /// </summary>
        /// <value>บัญชีเงินโอนผ่านธนาคาร</value>
        [DataMember(Name = "isTransferAccount", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isTransferAccount")]
        public bool? IsTransferAccount { get; set; }

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
        /// บัญชีนำฝาก
        /// </summary>
        /// <value>บัญชีนำฝาก</value>
        [DataMember(Name = "isDepositAccount", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isDepositAccount")]
        public bool? IsDepositAccount { get; set; }

        /// <summary>
        /// P.Card กระทรวงการคลัง
        /// </summary>
        /// <value>P.Card กระทรวงการคลัง</value>
        [DataMember(Name = "isPCard", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isPCard")]
        public bool? IsPCard { get; set; }

        /// <summary>
        /// Service Code
        /// </summary>
        /// <value>Service Code</value>
        [DataMember(Name = "serviceCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "serviceCode")]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Merchant ID
        /// </summary>
        /// <value>Merchant ID</value>
        [DataMember(Name = "merchantID", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "merchantID")]
        public string MerchantID { get; set; }

        /// <summary>
        /// สถานะ Active
        /// </summary>
        /// <value>สถานะ Active</value>
        [DataMember(Name = "isActive", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isActive")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// ประเภทของคู่บัญชี  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=GLAccountType
        /// </summary>
        /// <value>ประเภทของคู่บัญชี  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=GLAccountType</value>
        [DataMember(Name = "glAccountType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "glAccountType")]
        public MasterCenterDropdownDTO GlAccountType { get; set; }

        /// <summary>
        /// ภาษีมูลค่าเพิ่ม
        /// </summary>
        /// <value>ภาษีมูลค่าเพิ่ม</value>
        [DataMember(Name = "hasVat", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "hasVat")]
        public bool? HasVat { get; set; }

        /// <summary>
        /// GLRefCode
        /// </summary>
        /// <value>GLRefCode</value>
        [DataMember(Name = "glRefCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "glRefCode")]
        public string GlRefCode { get; set; }

        /// <summary>
        /// ชื่อบัญชี
        /// </summary>
        /// <value>ชื่อบัญชี</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        /// <value>หมายเหตุ</value>
        [DataMember(Name = "remark", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }

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
            sb.Append("class BankAccountDTO {\n");
            sb.Append("  Company: ").Append(Company).Append("\n");
            sb.Append("  Bank: ").Append(Bank).Append("\n");
            sb.Append("  BankBranch: ").Append(BankBranch).Append("\n");
            sb.Append("  Province: ").Append(Province).Append("\n");
            sb.Append("  BankAccountType: ").Append(BankAccountType).Append("\n");
            sb.Append("  GlAccountNo: ").Append(GlAccountNo).Append("\n");
            sb.Append("  BankAccountNo: ").Append(BankAccountNo).Append("\n");
            sb.Append("  IsTransferAccount: ").Append(IsTransferAccount).Append("\n");
            sb.Append("  IsDirectDebit: ").Append(IsDirectDebit).Append("\n");
            sb.Append("  IsDirectCredit: ").Append(IsDirectCredit).Append("\n");
            sb.Append("  IsDepositAccount: ").Append(IsDepositAccount).Append("\n");
            sb.Append("  IsPCard: ").Append(IsPCard).Append("\n");
            sb.Append("  ServiceCode: ").Append(ServiceCode).Append("\n");
            sb.Append("  MerchantID: ").Append(MerchantID).Append("\n");
            sb.Append("  IsActive: ").Append(IsActive).Append("\n");
            sb.Append("  GlAccountType: ").Append(GlAccountType).Append("\n");
            sb.Append("  HasVat: ").Append(HasVat).Append("\n");
            sb.Append("  GlRefCode: ").Append(GlRefCode).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Remark: ").Append(Remark).Append("\n");
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
