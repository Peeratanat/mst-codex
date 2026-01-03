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
  public class VisitorDTO {
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
    /// สถานะลูกค้า  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactStatus
    /// </summary>
    /// <value>สถานะลูกค้า  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactStatus</value>
    [DataMember(Name="contactStatus", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactStatus")]
    public MasterCenterDropdownDTO ContactStatus { get; set; }

    /// <summary>
    /// เลขที่รับ
    /// </summary>
    /// <value>เลขที่รับ</value>
    [DataMember(Name="receiveNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "receiveNumber")]
    public string ReceiveNumber { get; set; }

    /// <summary>
    /// ข้อมูลโครงการ  project/api/Projects/DropdownList
    /// </summary>
    /// <value>ข้อมูลโครงการ  project/api/Projects/DropdownList</value>
    [DataMember(Name="project", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "project")]
    public ProjectDTO Project { get; set; }

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
    /// เดินทางโดย  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=VisitBy
    /// </summary>
    /// <value>เดินทางโดย  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=VisitBy</value>
    [DataMember(Name="visitBy", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitBy")]
    public MasterCenterDropdownDTO VisitBy { get; set; }

    /// <summary>
    /// ประเภทรถยนต์  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Vehicle
    /// </summary>
    /// <value>ประเภทรถยนต์  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Vehicle</value>
    [DataMember(Name="vehicle", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "vehicle")]
    public MasterCenterDropdownDTO Vehicle { get; set; }

    /// <summary>
    /// รายละเอียดการเดินทางเพิ่มเติม
    /// </summary>
    /// <value>รายละเอียดการเดินทางเพิ่มเติม</value>
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
    /// สถานะต้อนรับลูกค้า
    /// </summary>
    /// <value>สถานะต้อนรับลูกค้า</value>
    [DataMember(Name="isWelcome", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isWelcome")]
    public bool? IsWelcome { get; set; }

    /// <summary>
    /// คำนำหน้า (ภาษาไทย)
    /// </summary>
    /// <value>คำนำหน้า (ภาษาไทย)</value>
    [DataMember(Name="titleTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "titleTH")]
    public string TitleTH { get; set; }

    /// <summary>
    /// ชื่อจริง (ภาษาไทย)
    /// </summary>
    /// <value>ชื่อจริง (ภาษาไทย)</value>
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
    /// คำนำหน้า (ภาษาอังกฤษ)
    /// </summary>
    /// <value>คำนำหน้า (ภาษาอังกฤษ)</value>
    [DataMember(Name="titleEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "titleEN")]
    public string TitleEN { get; set; }

    /// <summary>
    /// ชื่อจริง (ภาษาอังกฤษ)
    /// </summary>
    /// <value>ชื่อจริง (ภาษาอังกฤษ)</value>
    [DataMember(Name="firstNameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "firstNameEN")]
    public string FirstNameEN { get; set; }

    /// <summary>
    /// นามสกุล (ภาษาอังกฤษ)
    /// </summary>
    /// <value>นามสกุล (ภาษาอังกฤษ)</value>
    [DataMember(Name="lastNameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastNameEN")]
    public string LastNameEN { get; set; }

    /// <summary>
    /// เพศ
    /// </summary>
    /// <value>เพศ</value>
    [DataMember(Name="gender", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "gender")]
    public string Gender { get; set; }

    /// <summary>
    /// วันเกิด
    /// </summary>
    /// <value>วันเกิด</value>
    [DataMember(Name="birthDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "birthDate")]
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// กรุ๊ปเลือด
    /// </summary>
    /// <value>กรุ๊ปเลือด</value>
    [DataMember(Name="bloodType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "bloodType")]
    public string BloodType { get; set; }

    /// <summary>
    /// กรุ๊ปเลือด
    /// </summary>
    /// <value>กรุ๊ปเลือด</value>
    [DataMember(Name="houseNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "houseNo")]
    public string HouseNo { get; set; }

    /// <summary>
    /// หมู่ที่
    /// </summary>
    /// <value>หมู่ที่</value>
    [DataMember(Name="moo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "moo")]
    public string Moo { get; set; }

    /// <summary>
    /// หมู่บ้าน/อาคาร
    /// </summary>
    /// <value>หมู่บ้าน/อาคาร</value>
    [DataMember(Name="village", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "village")]
    public string Village { get; set; }

    /// <summary>
    /// ซอย
    /// </summary>
    /// <value>ซอย</value>
    [DataMember(Name="soi", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "soi")]
    public string Soi { get; set; }

    /// <summary>
    /// ถนน
    /// </summary>
    /// <value>ถนน</value>
    [DataMember(Name="road", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "road")]
    public string Road { get; set; }

    /// <summary>
    /// รหัสไปรษณีย์
    /// </summary>
    /// <value>รหัสไปรษณีย์</value>
    [DataMember(Name="postalCode", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "postalCode")]
    public string PostalCode { get; set; }

    /// <summary>
    /// ประเทศ
    /// </summary>
    /// <value>ประเทศ</value>
    [DataMember(Name="country", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "country")]
    public string Country { get; set; }

    /// <summary>
    /// จังหวัด
    /// </summary>
    /// <value>จังหวัด</value>
    [DataMember(Name="province", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "province")]
    public string Province { get; set; }

    /// <summary>
    /// เขต/อำเภอ
    /// </summary>
    /// <value>เขต/อำเภอ</value>
    [DataMember(Name="district", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "district")]
    public string District { get; set; }

    /// <summary>
    /// แขวง/ตำบล
    /// </summary>
    /// <value>แขวง/ตำบล</value>
    [DataMember(Name="subDistrict", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "subDistrict")]
    public string SubDistrict { get; set; }

    /// <summary>
    /// สัญชาติ
    /// </summary>
    /// <value>สัญชาติ</value>
    [DataMember(Name="national", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "national")]
    public string National { get; set; }

    /// <summary>
    /// Issue
    /// </summary>
    /// <value>Issue</value>
    [DataMember(Name="issue", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "issue")]
    public string Issue { get; set; }

    /// <summary>
    /// วัน Issue
    /// </summary>
    /// <value>วัน Issue</value>
    [DataMember(Name="issueDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "issueDate")]
    public DateTime? IssueDate { get; set; }

    /// <summary>
    /// วันหมดอายุ Issue
    /// </summary>
    /// <value>วันหมดอายุ Issue</value>
    [DataMember(Name="issueExpireDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "issueExpireDate")]
    public DateTime? IssueExpireDate { get; set; }

    /// <summary>
    /// เบอร์โทร
    /// </summary>
    /// <value>เบอร์โทร</value>
    [DataMember(Name="phoneNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phoneNumber")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// เป็น contact หรือไม่
    /// </summary>
    /// <value>เป็น contact หรือไม่</value>
    [DataMember(Name="isContact", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isContact")]
    public bool? IsContact { get; set; }

    /// <summary>
    /// สถานะบันทึก Opportunity (มี/ไม่มี)
    /// </summary>
    /// <value>สถานะบันทึก Opportunity (มี/ไม่มี)</value>
    [DataMember(Name="isSavedOpportunity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isSavedOpportunity")]
    public bool? IsSavedOpportunity { get; set; }

    /// <summary>
    /// ที่อยู่ที่ติดต่อได้ของ Contact
    /// </summary>
    /// <value>ที่อยู่ที่ติดต่อได้ของ Contact</value>
    [DataMember(Name="contactAddresses", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactAddresses")]
    public List<ContactAddressDTO> ContactAddresses { get; set; }

    /// <summary>
    /// ที่อยู่บัตรประชาชนของ Contact
    /// </summary>
    /// <value>ที่อยู่บัตรประชาชนของ Contact</value>
    [DataMember(Name="citizenAddress", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "citizenAddress")]
    public ContactAddressDTO CitizenAddress { get; set; }

    /// <summary>
    /// ที่อยู่ทะเบียนบ้านของ Contact
    /// </summary>
    /// <value>ที่อยู่ทะเบียนบ้านของ Contact</value>
    [DataMember(Name="homeAddress", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "homeAddress")]
    public ContactAddressDTO HomeAddress { get; set; }

    /// <summary>
    /// ที่อยู่ที่ทำงานของ Contact
    /// </summary>
    /// <value>ที่อยู่ที่ทำงานของ Contact</value>
    [DataMember(Name="workAddress", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "workAddress")]
    public ContactAddressDTO WorkAddress { get; set; }

    /// <summary>
    /// อีเมลล์ของ Contact
    /// </summary>
    /// <value>อีเมลล์ของ Contact</value>
    [DataMember(Name="contactEmail", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactEmail")]
    public ContactEmailDTO ContactEmail { get; set; }

    /// <summary>
    /// ข้อมูลไฟล์แนบ
    /// </summary>
    /// <value>ข้อมูลไฟล์แนบ</value>
    [DataMember(Name="idCardImage", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "idCardImage")]
    public FileDTO IdCardImage { get; set; }

    /// <summary>
    /// เลขทะเบียนรถ
    /// </summary>
    /// <value>เลขทะเบียนรถ</value>
    [DataMember(Name="vehicleRegistrationNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "vehicleRegistrationNo")]
    public string VehicleRegistrationNo { get; set; }

    /// <summary>
    /// สีรถ
    /// </summary>
    /// <value>สีรถ</value>
    [DataMember(Name="vehicleColor", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "vehicleColor")]
    public string VehicleColor { get; set; }

    /// <summary>
    /// ยี่ห้อรถ
    /// </summary>
    /// <value>ยี่ห้อรถ</value>
    [DataMember(Name="vehicleBrand", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "vehicleBrand")]
    public string VehicleBrand { get; set; }

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
      sb.Append("class VisitorDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Contact: ").Append(Contact).Append("\n");
      sb.Append("  ContactStatus: ").Append(ContactStatus).Append("\n");
      sb.Append("  ReceiveNumber: ").Append(ReceiveNumber).Append("\n");
      sb.Append("  Project: ").Append(Project).Append("\n");
      sb.Append("  VisitDateIn: ").Append(VisitDateIn).Append("\n");
      sb.Append("  VisitDateOut: ").Append(VisitDateOut).Append("\n");
      sb.Append("  VisitBy: ").Append(VisitBy).Append("\n");
      sb.Append("  Vehicle: ").Append(Vehicle).Append("\n");
      sb.Append("  VehicleDescription: ").Append(VehicleDescription).Append("\n");
      sb.Append("  WalkStatus: ").Append(WalkStatus).Append("\n");
      sb.Append("  Owner: ").Append(Owner).Append("\n");
      sb.Append("  IsWelcome: ").Append(IsWelcome).Append("\n");
      sb.Append("  TitleTH: ").Append(TitleTH).Append("\n");
      sb.Append("  FirstNameTH: ").Append(FirstNameTH).Append("\n");
      sb.Append("  LastNameTH: ").Append(LastNameTH).Append("\n");
      sb.Append("  TitleEN: ").Append(TitleEN).Append("\n");
      sb.Append("  FirstNameEN: ").Append(FirstNameEN).Append("\n");
      sb.Append("  LastNameEN: ").Append(LastNameEN).Append("\n");
      sb.Append("  Gender: ").Append(Gender).Append("\n");
      sb.Append("  BirthDate: ").Append(BirthDate).Append("\n");
      sb.Append("  BloodType: ").Append(BloodType).Append("\n");
      sb.Append("  HouseNo: ").Append(HouseNo).Append("\n");
      sb.Append("  Moo: ").Append(Moo).Append("\n");
      sb.Append("  Village: ").Append(Village).Append("\n");
      sb.Append("  Soi: ").Append(Soi).Append("\n");
      sb.Append("  Road: ").Append(Road).Append("\n");
      sb.Append("  PostalCode: ").Append(PostalCode).Append("\n");
      sb.Append("  Country: ").Append(Country).Append("\n");
      sb.Append("  Province: ").Append(Province).Append("\n");
      sb.Append("  District: ").Append(District).Append("\n");
      sb.Append("  SubDistrict: ").Append(SubDistrict).Append("\n");
      sb.Append("  National: ").Append(National).Append("\n");
      sb.Append("  Issue: ").Append(Issue).Append("\n");
      sb.Append("  IssueDate: ").Append(IssueDate).Append("\n");
      sb.Append("  IssueExpireDate: ").Append(IssueExpireDate).Append("\n");
      sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
      sb.Append("  IsContact: ").Append(IsContact).Append("\n");
      sb.Append("  IsSavedOpportunity: ").Append(IsSavedOpportunity).Append("\n");
      sb.Append("  ContactAddresses: ").Append(ContactAddresses).Append("\n");
      sb.Append("  CitizenAddress: ").Append(CitizenAddress).Append("\n");
      sb.Append("  HomeAddress: ").Append(HomeAddress).Append("\n");
      sb.Append("  WorkAddress: ").Append(WorkAddress).Append("\n");
      sb.Append("  ContactEmail: ").Append(ContactEmail).Append("\n");
      sb.Append("  IdCardImage: ").Append(IdCardImage).Append("\n");
      sb.Append("  VehicleRegistrationNo: ").Append(VehicleRegistrationNo).Append("\n");
      sb.Append("  VehicleColor: ").Append(VehicleColor).Append("\n");
      sb.Append("  VehicleBrand: ").Append(VehicleBrand).Append("\n");
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
