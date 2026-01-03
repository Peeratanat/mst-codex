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
  public class ProvinceListDTO {
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// จังหวัด ภาษาไทย
    /// </summary>
    /// <value>จังหวัด ภาษาไทย</value>
    [DataMember(Name="nameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "nameTH")]
    public string NameTH { get; set; }

    /// <summary>
    /// จังหวัด ภาษาอังกฤษ
    /// </summary>
    /// <value>จังหวัด ภาษาอังกฤษ</value>
    [DataMember(Name="nameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "nameEN")]
    public string NameEN { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ProvinceListDTO {\n");
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
