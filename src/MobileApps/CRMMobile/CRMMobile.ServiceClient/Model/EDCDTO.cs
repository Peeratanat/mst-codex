using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Model
{

    /// <summary>
    /// เครื่องรูดบัตร  Model &#x3D; EDC
    /// </summary>
    [DataContract]
    public class EDCDTO
    {
        /// <summary>
        /// ธนาคาร  Master/api/Banks/DropdownList
        /// </summary>
        /// <value>ธนาคาร  Master/api/Banks/DropdownList</value>
        [DataMember(Name = "bank", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bank")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// รหัสเครื่องรูดบัตร
        /// </summary>
        /// <value>รหัสเครื่องรูดบัตร</value>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// ประเภทเครื่อง  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CardMachineType
        /// </summary>
        /// <value>ประเภทเครื่อง  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CardMachineType</value>
        [DataMember(Name = "cardMachineType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardMachineType")]
        public MasterCenterDropdownDTO CardMachineType { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        /// <value>เบอร์โทรศัพท์</value>
        [DataMember(Name = "telNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "telNo")]
        public string TelNo { get; set; }

        /// <summary>
        /// สถานะเครื่อง  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CardMachineStatus
        /// </summary>
        /// <value>สถานะเครื่อง  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CardMachineStatus</value>
        [DataMember(Name = "cardMachineStatus", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardMachineStatus")]
        public MasterCenterDropdownDTO CardMachineStatus { get; set; }

        /// <summary>
        /// โครงการ  Project/api/Projects/DropdownList
        /// </summary>
        /// <value>โครงการ  Project/api/Projects/DropdownList</value>
        [DataMember(Name = "project", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "project")]
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// บริษัท  Master/api/Companies/DropdownList
        /// </summary>
        /// <value>บริษัท  Master/api/Companies/DropdownList</value>
        [DataMember(Name = "company", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "company")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// บัญชีธนาคาร
        /// </summary>
        /// <value>บัญชีธนาคาร</value>
        [DataMember(Name = "bankAccount", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bankAccount")]
        public BankAccountDTO BankAccount { get; set; }

        /// <summary>
        /// ผู้รับเครื่อง
        /// </summary>
        /// <value>ผู้รับเครื่อง</value>
        [DataMember(Name = "receiveBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "receiveBy")]
        public string ReceiveBy { get; set; }

        /// <summary>
        /// วันที่มอบเครื่อง
        /// </summary>
        /// <value>วันที่มอบเครื่อง</value>
        [DataMember(Name = "receiveDate", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "receiveDate")]
        public DateTime? ReceiveDate { get; set; }

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
            sb.Append("class EDCDTO {\n");
            sb.Append("  Bank: ").Append(Bank).Append("\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  CardMachineType: ").Append(CardMachineType).Append("\n");
            sb.Append("  TelNo: ").Append(TelNo).Append("\n");
            sb.Append("  CardMachineStatus: ").Append(CardMachineStatus).Append("\n");
            sb.Append("  Project: ").Append(Project).Append("\n");
            sb.Append("  Company: ").Append(Company).Append("\n");
            sb.Append("  BankAccount: ").Append(BankAccount).Append("\n");
            sb.Append("  ReceiveBy: ").Append(ReceiveBy).Append("\n");
            sb.Append("  ReceiveDate: ").Append(ReceiveDate).Append("\n");
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
