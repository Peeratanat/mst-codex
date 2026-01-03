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
    public class BOConfigurationDTO
    {
        /// <summary>
        /// ภาษีมูลค่าเพิ่ม (%)
        /// </summary>
        /// <value>ภาษีมูลค่าเพิ่ม (%)</value>
        [DataMember(Name = "vat", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "vat")]
        public double? Vat { get; set; }

        /// <summary>
        /// BOI
        /// </summary>
        /// <value>BOI</value>
        [DataMember(Name = "boiAmount", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "boiAmount")]
        public double? BoiAmount { get; set; }

        /// <summary>
        /// ภาษีเงินได้ (%)
        /// </summary>
        /// <value>ภาษีเงินได้ (%)</value>
        [DataMember(Name = "incomeTaxPercent", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incomeTaxPercent")]
        public double? IncomeTaxPercent { get; set; }

        /// <summary>
        /// ภาษีธุรกิจเฉพาะ (%)
        /// </summary>
        /// <value>ภาษีธุรกิจเฉพาะ (%)</value>
        [DataMember(Name = "businessTaxPercent", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "businessTaxPercent")]
        public double? BusinessTaxPercent { get; set; }

        /// <summary>
        /// ภาษีท้องถิ่น (%)
        /// </summary>
        /// <value>ภาษีท้องถิ่น (%)</value>
        [DataMember(Name = "localTaxPercent", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "localTaxPercent")]
        public double? LocalTaxPercent { get; set; }

        /// <summary>
        /// เบี้ยปรับย้ายห้อง
        /// </summary>
        /// <value>เบี้ยปรับย้ายห้อง</value>
        [DataMember(Name = "unitTransferFee", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "unitTransferFee")]
        public double? UnitTransferFee { get; set; }

        /// <summary>
        /// บัญชีเงินขาดเกิด
        /// </summary>
        /// <value>บัญชีเงินขาดเกิด</value>
        [DataMember(Name = "adjustAccount", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "adjustAccount")]
        public double? AdjustAccount { get; set; }

        /// <summary>
        /// บัญชีภาษีขาย
        /// </summary>
        /// <value>บัญชีภาษีขาย</value>
        [DataMember(Name = "taxAccount", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "taxAccount")]
        public double? TaxAccount { get; set; }

        /// <summary>
        /// อัตราค่าเสื่อมราคาต่อปี
        /// </summary>
        /// <value>อัตราค่าเสื่อมราคาต่อปี</value>
        [DataMember(Name = "depreciationYear", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "depreciationYear")]
        public double? DepreciationYear { get; set; }

        /// <summary>
        /// ยกเลิกแบบคืนเงิน
        /// </summary>
        /// <value>ยกเลิกแบบคืนเงิน</value>
        [DataMember(Name = "voidRefund", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "voidRefund")]
        public double? VoidRefund { get; set; }

        /// <summary>
        /// อัตราค่าธรรมเนียมโอน
        /// </summary>
        /// <value>อัตราค่าธรรมเนียมโอน</value>
        [DataMember(Name = "transferFeeRate", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "transferFeeRate")]
        public double? TransferFeeRate { get; set; }

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
            sb.Append("class BOConfigurationDTO {\n");
            sb.Append("  Vat: ").Append(Vat).Append("\n");
            sb.Append("  BoiAmount: ").Append(BoiAmount).Append("\n");
            sb.Append("  IncomeTaxPercent: ").Append(IncomeTaxPercent).Append("\n");
            sb.Append("  BusinessTaxPercent: ").Append(BusinessTaxPercent).Append("\n");
            sb.Append("  LocalTaxPercent: ").Append(LocalTaxPercent).Append("\n");
            sb.Append("  UnitTransferFee: ").Append(UnitTransferFee).Append("\n");
            sb.Append("  AdjustAccount: ").Append(AdjustAccount).Append("\n");
            sb.Append("  TaxAccount: ").Append(TaxAccount).Append("\n");
            sb.Append("  DepreciationYear: ").Append(DepreciationYear).Append("\n");
            sb.Append("  VoidRefund: ").Append(VoidRefund).Append("\n");
            sb.Append("  TransferFeeRate: ").Append(TransferFeeRate).Append("\n");
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
