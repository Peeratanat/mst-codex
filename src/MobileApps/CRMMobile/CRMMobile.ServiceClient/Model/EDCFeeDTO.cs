using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Model
{

    /// <summary>
    /// ค่าธรรมเนียมเครื่องรูดบัตร  Model &#x3D; EDCFee
    /// </summary>
    [DataContract]
    public class EDCFeeDTO
    {
        /// <summary>
        /// เครื่องรูดบัตรธนาคาร  Master/api/Banks/DropdownList
        /// </summary>
        /// <value>เครื่องรูดบัตรธนาคาร  Master/api/Banks/DropdownList</value>
        [DataMember(Name = "bank", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bank")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// ชนิดบัตร  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PaymentCardType
        /// </summary>
        /// <value>ชนิดบัตร  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PaymentCardType</value>
        [DataMember(Name = "paymentCardType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "paymentCardType")]
        public MasterCenterDropdownDTO PaymentCardType { get; set; }

        /// <summary>
        /// ประเภทบัตร  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditCardType
        /// </summary>
        /// <value>ประเภทบัตร  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditCardType</value>
        [DataMember(Name = "creditCardType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "creditCardType")]
        public MasterCenterDropdownDTO CreditCardType { get; set; }

        /// <summary>
        /// บัตรที่รูด (true = แสดงชื่อธนาคาร)
        /// </summary>
        /// <value>บัตรที่รูด (true = แสดงชื่อธนาคาร)</value>
        [DataMember(Name = "isEDCBankCreditCard", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isEDCBankCreditCard")]
        public bool? IsEDCBankCreditCard { get; set; }

        /// <summary>
        /// รูปแบบการรูด  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditCardPaymentType
        /// </summary>
        /// <value>รูปแบบการรูด  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditCardPaymentType</value>
        [DataMember(Name = "creditCardPaymentType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "creditCardPaymentType")]
        public MasterCenterDropdownDTO CreditCardPaymentType { get; set; }

        /// <summary>
        /// ค่าธรรมเนียม (%)
        /// </summary>
        /// <value>ค่าธรรมเนียม (%)</value>
        [DataMember(Name = "fee", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "fee")]
        public double? Fee { get; set; }

        /// <summary>
        /// ชื่อโปรโมชั่น (บัตรเครดิต)
        /// </summary>
        /// <value>ชื่อโปรโมชั่น (บัตรเครดิต)</value>
        [DataMember(Name = "creditCardPromotionName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "creditCardPromotionName")]
        public string CreditCardPromotionName { get; set; }

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
            sb.Append("class EDCFeeDTO {\n");
            sb.Append("  Bank: ").Append(Bank).Append("\n");
            sb.Append("  PaymentCardType: ").Append(PaymentCardType).Append("\n");
            sb.Append("  CreditCardType: ").Append(CreditCardType).Append("\n");
            sb.Append("  IsEDCBankCreditCard: ").Append(IsEDCBankCreditCard).Append("\n");
            sb.Append("  CreditCardPaymentType: ").Append(CreditCardPaymentType).Append("\n");
            sb.Append("  Fee: ").Append(Fee).Append("\n");
            sb.Append("  CreditCardPromotionName: ").Append(CreditCardPromotionName).Append("\n");
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
