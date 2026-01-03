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
  public class FloorDropdownDTO {
    /// <summary>
    /// Identity FloorID
    /// </summary>
    /// <value>Identity FloorID</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// ชื่อชั้น (TH)
    /// </summary>
    /// <value>ชื่อชั้น (TH)</value>
    [DataMember(Name="nameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "nameTH")]
    public string NameTH { get; set; }

    /// <summary>
    /// ชื่อชั้น (EN)
    /// </summary>
    /// <value>ชื่อชั้น (EN)</value>
    [DataMember(Name="nameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "nameEN")]
    public string NameEN { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class FloorDropdownDTO {\n");
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
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
