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
    public class BankDTO
    {
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
        /// มีบริการบัตร Credit ไหม
        /// </summary>
        /// <value>มีบริการบัตร Credit ไหม</value>
        [DataMember(Name = "isCreditCard", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isCreditCard")]
        public bool? IsCreditCard { get; set; }

        /// <summary>
        /// เป็น Bank หรือ NonBank
        /// </summary>
        /// <value>เป็น Bank หรือ NonBank</value>
        [DataMember(Name = "isNonBank", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isNonBank")]
        public bool? IsNonBank { get; set; }

        /// <summary>
        /// เป็น Coorperative Bank หรือเปล่า
        /// </summary>
        /// <value>เป็น Coorperative Bank หรือเปล่า</value>
        [DataMember(Name = "isCoorperative", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isCoorperative")]
        public bool? IsCoorperative { get; set; }

        /// <summary>
        /// ขอสินเชื่อฟรีไหม
        /// </summary>
        /// <value>ขอสินเชื่อฟรีไหม</value>
        [DataMember(Name = "isFreeMortgage", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "isFreeMortgage")]
        public bool? IsFreeMortgage { get; set; }

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
            sb.Append("class BankDTO {\n");
            sb.Append("  BankNo: ").Append(BankNo).Append("\n");
            sb.Append("  NameTH: ").Append(NameTH).Append("\n");
            sb.Append("  NameEN: ").Append(NameEN).Append("\n");
            sb.Append("  Alias: ").Append(Alias).Append("\n");
            sb.Append("  IsCreditCard: ").Append(IsCreditCard).Append("\n");
            sb.Append("  IsNonBank: ").Append(IsNonBank).Append("\n");
            sb.Append("  IsCoorperative: ").Append(IsCoorperative).Append("\n");
            sb.Append("  IsFreeMortgage: ").Append(IsFreeMortgage).Append("\n");
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
