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
  public class VisitorProjectDTO {
    /// <summary>
    /// จำนวนผู้เข้าออก
    /// </summary>
    /// <value>จำนวนผู้เข้าออก</value>
    [DataMember(Name="visitInOutCount", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitInOutCount")]
    public int? VisitInOutCount { get; set; }

    /// <summary>
    /// จำนวน Visitor ที่ต้อนรับแล้ว
    /// </summary>
    /// <value>จำนวน Visitor ที่ต้อนรับแล้ว</value>
    [DataMember(Name="visitorWelcomeCount", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitorWelcomeCount")]
    public int? VisitorWelcomeCount { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class VisitorProjectDTO {\n");
      sb.Append("  VisitInOutCount: ").Append(VisitInOutCount).Append("\n");
      sb.Append("  VisitorWelcomeCount: ").Append(VisitorWelcomeCount).Append("\n");
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
