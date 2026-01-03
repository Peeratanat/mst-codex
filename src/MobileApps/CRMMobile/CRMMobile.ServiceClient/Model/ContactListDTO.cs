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
  public class ContactListDTO {
    /// <summary>
    /// ID ของ Contact
    /// </summary>
    /// <value>ID ของ Contact</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// รหัสของ Contact
    /// </summary>
    /// <value>รหัสของ Contact</value>
    [DataMember(Name="contactNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactNo")]
    public string ContactNo { get; set; }

    /// <summary>
    /// ชื่อจริง/ชื่อบริษัท (ภาษาไทย)
    /// </summary>
    /// <value>ชื่อจริง/ชื่อบริษัท (ภาษาไทย)</value>
    [DataMember(Name="firstNameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "firstNameTH")]
    public string FirstNameTH { get; set; }

    /// <summary>
    /// นามสกุล (ภาษาไทย)
    /// </summary>
    /// <value>นามสกุล (ภาษาไทย)</value>
    [DataMember(Name="lastNameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastNameTH")]
    public string LastNameTH { get; set; }

    /// <summary>
    /// เบอร์โทรศัพท์
    /// </summary>
    /// <value>เบอร์โทรศัพท์</value>
    [DataMember(Name="phoneNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phoneNumber")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// เบอร์โทรศัพท์บ้าน
    /// </summary>
    /// <value>เบอร์โทรศัพท์บ้าน</value>
    [DataMember(Name="homeNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "homeNumber")]
    public string HomeNumber { get; set; }

    /// <summary>
    /// จำนวน Opportunity
    /// </summary>
    /// <value>จำนวน Opportunity</value>
    [DataMember(Name="opportunityCount", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "opportunityCount")]
    public int? OpportunityCount { get; set; }

    /// <summary>
    /// Last Opportunity
    /// </summary>
    /// <value>Last Opportunity</value>
    [DataMember(Name="lastOpportunityDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastOpportunityDate")]
    public DateTime? LastOpportunityDate { get; set; }

    /// <summary>
    /// วันที่สร้าง
    /// </summary>
    /// <value>วันที่สร้าง</value>
    [DataMember(Name="createdDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// วันที่แก้ไข
    /// </summary>
    /// <value>วันที่แก้ไข</value>
    [DataMember(Name="updatedDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "updatedDate")]
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// เลขที่บัตรประชาชน
    /// </summary>
    /// <value>เลขที่บัตรประชาชน</value>
    [DataMember(Name="citizenIdentityNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "citizenIdentityNo")]
    public string CitizenIdentityNo { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ContactListDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  ContactNo: ").Append(ContactNo).Append("\n");
      sb.Append("  FirstNameTH: ").Append(FirstNameTH).Append("\n");
      sb.Append("  LastNameTH: ").Append(LastNameTH).Append("\n");
      sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
      sb.Append("  HomeNumber: ").Append(HomeNumber).Append("\n");
      sb.Append("  OpportunityCount: ").Append(OpportunityCount).Append("\n");
      sb.Append("  LastOpportunityDate: ").Append(LastOpportunityDate).Append("\n");
      sb.Append("  CreatedDate: ").Append(CreatedDate).Append("\n");
      sb.Append("  UpdatedDate: ").Append(UpdatedDate).Append("\n");
      sb.Append("  CitizenIdentityNo: ").Append(CitizenIdentityNo).Append("\n");
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
