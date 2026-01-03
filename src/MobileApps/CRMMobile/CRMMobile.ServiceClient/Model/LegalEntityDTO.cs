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
    public class LegalEntityDTO
    {
        /// <summary>
        /// ชื่อ นิติบุคคลอาคารชุด ภาษาไทย (TH)
        /// </summary>
        /// <value>ชื่อ นิติบุคคลอาคารชุด ภาษาไทย (TH)</value>
        [DataMember(Name = "nameTH", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameTH")]
        public string NameTH { get; set; }

        /// <summary>
        /// ชื่อ นิติบุคคลอาคารชุด อังกฤษ (EN)
        /// </summary>
        /// <value>ชื่อ นิติบุคคลอาคารชุด อังกฤษ (EN)</value>
        [DataMember(Name = "nameEN", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameEN")]
        public string NameEN { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        /// <value>ธนาคาร</value>
        [DataMember(Name = "bank", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bank")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// ประเภทบัญชี  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=BankAccountType
        /// </summary>
        /// <value>ประเภทบัญชี  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=BankAccountType</value>
        [DataMember(Name = "bankAccountType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bankAccountType")]
        public MasterCenterDropdownDTO BankAccountType { get; set; }

        /// <summary>
        /// เลขบัญชีธนาคาร
        /// </summary>
        /// <value>เลขบัญชีธนาคาร</value>
        [DataMember(Name = "bankAccountNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bankAccountNo")]
        public string BankAccountNo { get; set; }

        /// <summary>
        /// สถานะ Active/InActive
        /// </summary>
        /// <value>สถานะ Active/InActive</value>
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
            sb.Append("class LegalEntityDTO {\n");
            sb.Append("  NameTH: ").Append(NameTH).Append("\n");
            sb.Append("  NameEN: ").Append(NameEN).Append("\n");
            sb.Append("  Bank: ").Append(Bank).Append("\n");
            sb.Append("  BankAccountType: ").Append(BankAccountType).Append("\n");
            sb.Append("  BankAccountNo: ").Append(BankAccountNo).Append("\n");
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
