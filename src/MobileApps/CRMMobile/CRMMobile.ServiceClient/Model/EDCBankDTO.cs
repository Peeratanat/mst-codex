using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Model
{

    /// <summary>
    /// ธนาคารเครื่องรูดบัตร  ไม่มี Model โดยตรง (Group By Bank จาก EDC)
    /// </summary>
    [DataContract]
    public class EDCBankDTO
    {
        /// <summary>
        /// ธนาคาร  Master/api/Banks/DropdownList
        /// </summary>
        /// <value>ธนาคาร  Master/api/Banks/DropdownList</value>
        [DataMember(Name = "bank", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bank")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// แก้ไขล่าสุด
        /// </summary>
        /// <value>แก้ไขล่าสุด</value>
        [DataMember(Name = "updated", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "updated")]
        public DateTime? Updated { get; set; }

        /// <summary>
        /// แก้ไขโดย
        /// </summary>
        /// <value>แก้ไขโดย</value>
        [DataMember(Name = "updatedBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "updatedBy")]
        public string UpdatedBy { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class EDCBankDTO {\n");
            sb.Append("  Bank: ").Append(Bank).Append("\n");
            sb.Append("  Updated: ").Append(Updated).Append("\n");
            sb.Append("  UpdatedBy: ").Append(UpdatedBy).Append("\n");
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
