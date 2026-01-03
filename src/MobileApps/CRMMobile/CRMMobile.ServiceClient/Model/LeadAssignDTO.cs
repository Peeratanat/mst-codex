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
  public class LeadAssignDTO {
    /// <summary>
    /// รายการ Lead
    /// </summary>
    /// <value>รายการ Lead</value>
    [DataMember(Name="leads", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leads")]
    public List<LeadListDTO> Leads { get; set; }

    /// <summary>
    /// ผู้ดูแล
    /// </summary>
    /// <value>ผู้ดูแล</value>
    [DataMember(Name="user", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "user")]
    public UserListDTO User { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class LeadAssignDTO {\n");
      sb.Append("  Leads: ").Append(Leads).Append("\n");
      sb.Append("  User: ").Append(User).Append("\n");
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
