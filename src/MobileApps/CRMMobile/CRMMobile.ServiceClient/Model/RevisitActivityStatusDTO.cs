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
  public class RevisitActivityStatusDTO {
    /// <summary>
    /// ID ของ Activity Status
    /// </summary>
    /// <value>ID ของ Activity Status</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// รหัส
    /// </summary>
    /// <value>รหัส</value>
    [DataMember(Name="code", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "code")]
    public string Code { get; set; }

    /// <summary>
    /// รายละเอียดของ Status
    /// </summary>
    /// <value>รายละเอียดของ Status</value>
    [DataMember(Name="statusDescription", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "statusDescription")]
    public string StatusDescription { get; set; }

    /// <summary>
    /// เหตุผลอื่นๆ
    /// </summary>
    /// <value>เหตุผลอื่นๆ</value>
    [DataMember(Name="otherReason", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "otherReason")]
    public string OtherReason { get; set; }

    /// <summary>
    /// เลือกสถานะ/ไม่เลือกสถานะ
    /// </summary>
    /// <value>เลือกสถานะ/ไม่เลือกสถานะ</value>
    [DataMember(Name="isSelected", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isSelected")]
    public bool? IsSelected { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class RevisitActivityStatusDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Code: ").Append(Code).Append("\n");
      sb.Append("  StatusDescription: ").Append(StatusDescription).Append("\n");
      sb.Append("  OtherReason: ").Append(OtherReason).Append("\n");
      sb.Append("  IsSelected: ").Append(IsSelected).Append("\n");
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
