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
  public class BrandDropdownDTO {
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// รหัสแบรนด์
    /// </summary>
    /// <value>รหัสแบรนด์</value>
    [DataMember(Name="brandNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "brandNo")]
    public string BrandNo { get; set; }

    /// <summary>
    /// ชื่อแบรนด์
    /// </summary>
    /// <value>ชื่อแบรนด์</value>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class BrandDropdownDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  BrandNo: ").Append(BrandNo).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
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
