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
  public class CompanyDropdownDTO {
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// รหัสริษัท
    /// </summary>
    /// <value>รหัสริษัท</value>
    [DataMember(Name="code", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "code")]
    public string Code { get; set; }

    /// <summary>
    /// ชื่อบริษัทภาษาอังกฤษ
    /// </summary>
    /// <value>ชื่อบริษัทภาษาอังกฤษ</value>
    [DataMember(Name="nameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "nameEN")]
    public string NameEN { get; set; }

    /// <summary>
    /// ชื่อบริษัทภาษาไทย
    /// </summary>
    /// <value>ชื่อบริษัทภาษาไทย</value>
    [DataMember(Name="nameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "nameTH")]
    public string NameTH { get; set; }

    /// <summary>
    /// รหัสบริษัท SAP
    /// </summary>
    /// <value>รหัสบริษัท SAP</value>
    [DataMember(Name="sapCompanyID", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "sapCompanyID")]
    public string SapCompanyID { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CompanyDropdownDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Code: ").Append(Code).Append("\n");
      sb.Append("  NameEN: ").Append(NameEN).Append("\n");
      sb.Append("  NameTH: ").Append(NameTH).Append("\n");
      sb.Append("  SapCompanyID: ").Append(SapCompanyID).Append("\n");
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
