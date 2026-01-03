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
  public class CountryDTO {
    /// <summary>
    /// รหัสประเทศ
    /// </summary>
    /// <value>รหัสประเทศ</value>
    [DataMember(Name="code", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "code")]
    public string Code { get; set; }

    /// <summary>
    /// ชื่อประเทศ (ภาษาไทย)
    /// </summary>
    /// <value>ชื่อประเทศ (ภาษาไทย)</value>
    [DataMember(Name="nameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "nameTH")]
    public string NameTH { get; set; }

    /// <summary>
    /// ชื่อประเทศ (ภาษาอังกฤษ)
    /// </summary>
    /// <value>ชื่อประเทศ (ภาษาอังกฤษ)</value>
    [DataMember(Name="nameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "nameEN")]
    public string NameEN { get; set; }

    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// Gets or Sets UpdatedBy
    /// </summary>
    [DataMember(Name="updatedBy", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "updatedBy")]
    public string UpdatedBy { get; set; }

    /// <summary>
    /// Gets or Sets Updated
    /// </summary>
    [DataMember(Name="updated", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "updated")]
    public DateTime? Updated { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CountryDTO {\n");
      sb.Append("  Code: ").Append(Code).Append("\n");
      sb.Append("  NameTH: ").Append(NameTH).Append("\n");
      sb.Append("  NameEN: ").Append(NameEN).Append("\n");
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
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
