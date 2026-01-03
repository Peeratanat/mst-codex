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
  public class AddressDTO {
    /// <summary>
    /// ที่อยู่ที่ติดต่อได้
    /// </summary>
    /// <value>ที่อยู่ที่ติดต่อได้</value>
    [DataMember(Name="contactAddress", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactAddress")]
    public List<ContactAddressDTO> ContactAddress { get; set; }

    /// <summary>
    /// ที่อยู่ตามบัตรประชาชน
    /// </summary>
    /// <value>ที่อยู่ตามบัตรประชาชน</value>
    [DataMember(Name="citizenAddress", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "citizenAddress")]
    public ContactAddressDTO CitizenAddress { get; set; }

    /// <summary>
    /// ที่อยู่ตามทะเบียนบ้าน
    /// </summary>
    /// <value>ที่อยู่ตามทะเบียนบ้าน</value>
    [DataMember(Name="homeAddress", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "homeAddress")]
    public ContactAddressDTO HomeAddress { get; set; }

    /// <summary>
    /// ที่อยู่ที่ทำงาน
    /// </summary>
    /// <value>ที่อยู่ที่ทำงาน</value>
    [DataMember(Name="workAddress", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "workAddress")]
    public ContactAddressDTO WorkAddress { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class AddressDTO {\n");
      sb.Append("  ContactAddress: ").Append(ContactAddress).Append("\n");
      sb.Append("  CitizenAddress: ").Append(CitizenAddress).Append("\n");
      sb.Append("  HomeAddress: ").Append(HomeAddress).Append("\n");
      sb.Append("  WorkAddress: ").Append(WorkAddress).Append("\n");
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
