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
    public class BankDropdownDTO
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// รหัสธนาคาร
        /// </summary>
        /// <value>รหัสธนาคาร</value>
        [DataMember(Name = "bankNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "bankNo")]
        public string BankNo { get; set; }

        /// <summary>
        /// ชื่อธนาคารภาษาไทย
        /// </summary>
        /// <value>ชื่อธนาคารภาษาไทย</value>
        [DataMember(Name = "nameTH", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameTH")]
        public string NameTH { get; set; }

        /// <summary>
        /// ชื่อธนาคารภาษาอังกฤษ
        /// </summary>
        /// <value>ชื่อธนาคารภาษาอังกฤษ</value>
        [DataMember(Name = "nameEN", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameEN")]
        public string NameEN { get; set; }

        /// <summary>
        /// ชื่อย่อ
        /// </summary>
        /// <value>ชื่อย่อ</value>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class BankDropdownDTO {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  BankNo: ").Append(BankNo).Append("\n");
            sb.Append("  NameTH: ").Append(NameTH).Append("\n");
            sb.Append("  NameEN: ").Append(NameEN).Append("\n");
            sb.Append("  Alias: ").Append(Alias).Append("\n");
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
