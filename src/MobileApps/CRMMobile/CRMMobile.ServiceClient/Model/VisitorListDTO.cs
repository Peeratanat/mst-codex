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
  public class VisitorListDTO {
    /// <summary>
    /// ID ของ Visitor
    /// </summary>
    /// <value>ID ของ Visitor</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// ข้อมูลของ Contact
    /// </summary>
    /// <value>ข้อมูลของ Contact</value>
    [DataMember(Name="contact", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contact")]
    public ContactListDTO Contact { get; set; }

    /// <summary>
    /// เลขที่รับ
    /// </summary>
    /// <value>เลขที่รับ</value>
    [DataMember(Name="receiveNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "receiveNumber")]
    public string ReceiveNumber { get; set; }

    /// <summary>
    /// ชื่อ (ภาษาไทย)
    /// </summary>
    /// <value>ชื่อ (ภาษาไทย)</value>
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
    /// เดินทางโดย  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=VisitBy
    /// </summary>
    /// <value>เดินทางโดย  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=VisitBy</value>
    [DataMember(Name="visitBy", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitBy")]
    public MasterCenterDropdownDTO VisitBy { get; set; }

    /// <summary>
    /// รายละเอียด
    /// </summary>
    /// <value>รายละเอียด</value>
    [DataMember(Name="vehicleDescription", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "vehicleDescription")]
    public string VehicleDescription { get; set; }

    /// <summary>
    /// สถานะ Walk  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=WalkStatus
    /// </summary>
    /// <value>สถานะ Walk  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=WalkStatus</value>
    [DataMember(Name="walkStatus", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "walkStatus")]
    public MasterCenterDropdownDTO WalkStatus { get; set; }

    /// <summary>
    /// ผู้ดูแล
    /// </summary>
    /// <value>ผู้ดูแล</value>
    [DataMember(Name="owner", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "owner")]
    public UserListDTO Owner { get; set; }

    /// <summary>
    /// วันที่เข้าโครงการ
    /// </summary>
    /// <value>วันที่เข้าโครงการ</value>
    [DataMember(Name="visitDateIn", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitDateIn")]
    public DateTime? VisitDateIn { get; set; }

    /// <summary>
    /// วันที่ออกโครงการ
    /// </summary>
    /// <value>วันที่ออกโครงการ</value>
    [DataMember(Name="visitDateOut", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitDateOut")]
    public DateTime? VisitDateOut { get; set; }

    /// <summary>
    /// ไฟล์แนบ
    /// </summary>
    /// <value>ไฟล์แนบ</value>
    [DataMember(Name="file", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "file")]
    public FileDTO _File { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class VisitorListDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Contact: ").Append(Contact).Append("\n");
      sb.Append("  ReceiveNumber: ").Append(ReceiveNumber).Append("\n");
      sb.Append("  FirstNameTH: ").Append(FirstNameTH).Append("\n");
      sb.Append("  LastNameTH: ").Append(LastNameTH).Append("\n");
      sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
      sb.Append("  VisitBy: ").Append(VisitBy).Append("\n");
      sb.Append("  VehicleDescription: ").Append(VehicleDescription).Append("\n");
      sb.Append("  WalkStatus: ").Append(WalkStatus).Append("\n");
      sb.Append("  Owner: ").Append(Owner).Append("\n");
      sb.Append("  VisitDateIn: ").Append(VisitDateIn).Append("\n");
      sb.Append("  VisitDateOut: ").Append(VisitDateOut).Append("\n");
      sb.Append("  _File: ").Append(_File).Append("\n");
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
