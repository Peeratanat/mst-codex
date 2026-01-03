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
    public class AgentDropdownDTO
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// ชื่อ Agency ภาษาไทย (TH)
        /// </summary>
        /// <value>ชื่อ Agency ภาษาไทย (TH)</value>
        [DataMember(Name = "nameTH", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameTH")]
        public string NameTH { get; set; }

        /// <summary>
        /// ชื่อ Agency อังกฤษ (EN)
        /// </summary>
        /// <value>ชื่อ Agency อังกฤษ (EN)</value>
        [DataMember(Name = "nameEN", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nameEN")]
        public string NameEN { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AgentDropdownDTO {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  NameTH: ").Append(NameTH).Append("\n");
            sb.Append("  NameEN: ").Append(NameEN).Append("\n");
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
