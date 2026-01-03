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
  public class TowerDropdownDTO {
    /// <summary>
    /// Identity Tower ID
    /// </summary>
    /// <value>Identity Tower ID</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// ชื่อตึก
    /// </summary>
    /// <value>ชื่อตึก</value>
    [DataMember(Name="code", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "code")]
    public string Code { get; set; }

    /// <summary>
    /// อาคารเลขที่ (TH)
    /// </summary>
    /// <value>อาคารเลขที่ (TH)</value>
    [DataMember(Name="noTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "noTH")]
    public string NoTH { get; set; }

    /// <summary>
    /// อาคารเลขที่ (EN)
    /// </summary>
    /// <value>อาคารเลขที่ (EN)</value>
    [DataMember(Name="noEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "noEN")]
    public string NoEN { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class TowerDropdownDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Code: ").Append(Code).Append("\n");
      sb.Append("  NoTH: ").Append(NoTH).Append("\n");
      sb.Append("  NoEN: ").Append(NoEN).Append("\n");
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
