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
    public class CancelReturnSettingDTO
    {
        /// <summary>
        /// Chief อนุมัติเมื่อคืนเงินน้อยกว่า (%)
        /// </summary>
        /// <value>Chief อนุมัติเมื่อคืนเงินน้อยกว่า (%)</value>
        [DataMember(Name = "chiefReturnLessThanPercent", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "chiefReturnLessThanPercent")]
        public double? ChiefReturnLessThanPercent { get; set; }

        /// <summary>
        /// หักค่าดำเนินการ (บาท)
        /// </summary>
        /// <value>หักค่าดำเนินการ (บาท)</value>
        [DataMember(Name = "handlingFee", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "handlingFee")]
        public double? HandlingFee { get; set; }

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
            sb.Append("class CancelReturnSettingDTO {\n");
            sb.Append("  ChiefReturnLessThanPercent: ").Append(ChiefReturnLessThanPercent).Append("\n");
            sb.Append("  HandlingFee: ").Append(HandlingFee).Append("\n");
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
