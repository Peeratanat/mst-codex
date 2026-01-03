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
  public class ContactSimilarPopupDTO {
    /// <summary>
    /// ข้อมูล Contact ที่ใกล้เคียง
    /// </summary>
    /// <value>ข้อมูล Contact ที่ใกล้เคียง</value>
    [DataMember(Name="contactSimilars", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactSimilars")]
    public List<ContactSimilarDTO> ContactSimilars { get; set; }

    /// <summary>
    /// สถานะการสร้าง Contact ใหม่ (true = สร้างได้, false = สร้างไม่ได้)
    /// </summary>
    /// <value>สถานะการสร้าง Contact ใหม่ (true = สร้างได้, false = สร้างไม่ได้)</value>
    [DataMember(Name="canCreateNewContact", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "canCreateNewContact")]
    public bool? CanCreateNewContact { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ContactSimilarPopupDTO {\n");
      sb.Append("  ContactSimilars: ").Append(ContactSimilars).Append("\n");
      sb.Append("  CanCreateNewContact: ").Append(CanCreateNewContact).Append("\n");
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
