using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ContactEmailDTO {
    /// <summary>
    /// ID ของ Email
    /// </summary>
    /// <value>ID ของ Email</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    /// <value>Email</value>
    [DataMember(Name="email", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }

    /// <summary>
    /// สถานะ: อีเมลหลัก
    /// </summary>
    /// <value>สถานะ: อีเมลหลัก</value>
    [DataMember(Name="isMain", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isMain")]
    public bool? IsMain { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ContactEmailDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Email: ").Append(Email).Append("\n");
      sb.Append("  IsMain: ").Append(IsMain).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
