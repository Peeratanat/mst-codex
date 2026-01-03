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
  public class ContactPhoneDTO {
    /// <summary>
    /// ID ของเบอร์โทรศัพท์
    /// </summary>
    /// <value>ID ของเบอร์โทรศัพท์</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// ประเภทของเบอร์โทรศัพท์  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PhoneType
    /// </summary>
    /// <value>ประเภทของเบอร์โทรศัพท์  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PhoneType</value>
    [DataMember(Name="phoneType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phoneType")]
    public MasterCenterDropdownDTO PhoneType { get; set; }

    /// <summary>
    /// เบอร์โทรศัพท์
    /// </summary>
    /// <value>เบอร์โทรศัพท์</value>
    [DataMember(Name="phoneNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phoneNumber")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// หมายเลขต่อ
    /// </summary>
    /// <value>หมายเลขต่อ</value>
    [DataMember(Name="phoneNumberExt", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phoneNumberExt")]
    public string PhoneNumberExt { get; set; }

    /// <summary>
    /// รหัสประเทศ
    /// </summary>
    /// <value>รหัสประเทศ</value>
    [DataMember(Name="countryCode", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "countryCode")]
    public string CountryCode { get; set; }

    /// <summary>
    /// Gets or Sets IsMain
    /// </summary>
    [DataMember(Name="isMain", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isMain")]
    public bool? IsMain { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ContactPhoneDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  PhoneType: ").Append(PhoneType).Append("\n");
      sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
      sb.Append("  PhoneNumberExt: ").Append(PhoneNumberExt).Append("\n");
      sb.Append("  CountryCode: ").Append(CountryCode).Append("\n");
      sb.Append("  IsMain: ").Append(IsMain).Append("\n");
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
