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
  public class LeadQualifyDTO {
    /// <summary>
    /// Contact
    /// </summary>
    /// <value>Contact</value>
    [DataMember(Name="contact", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contact")]
    public ContactListDTO Contact { get; set; }

    /// <summary>
    /// ที่อยู่
    /// </summary>
    /// <value>ที่อยู่</value>
    [DataMember(Name="address", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "address")]
    public ContactAddressDTO Address { get; set; }

    /// <summary>
    /// กรณีพบ Contact ที่ตรงทั้งชื่อ นามสกุล และเบอร์โทร
    /// </summary>
    /// <value>กรณีพบ Contact ที่ตรงทั้งชื่อ นามสกุล และเบอร์โทร</value>
    [DataMember(Name="hasExactContact", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "hasExactContact")]
    public bool? HasExactContact { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class LeadQualifyDTO {\n");
      sb.Append("  Contact: ").Append(Contact).Append("\n");
      sb.Append("  Address: ").Append(Address).Append("\n");
      sb.Append("  HasExactContact: ").Append(HasExactContact).Append("\n");
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
