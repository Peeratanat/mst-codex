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
  public class VisitorCreateDTO {
    /// <summary>
    /// รหัสโครงการ (branch_code)
    /// </summary>
    /// <value>รหัสโครงการ (branch_code)</value>
    [DataMember(Name="projectNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "projectNo")]
    public string ProjectNo { get; set; }

    /// <summary>
    /// เลขที่บัตรประชาชน (personalid)
    /// </summary>
    /// <value>เลขที่บัตรประชาชน (personalid)</value>
    [DataMember(Name="citizenIdentityNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "citizenIdentityNo")]
    public string CitizenIdentityNo { get; set; }

    /// <summary>
    /// คำนำหน้า (ภาษาไทย) (titlename)
    /// </summary>
    /// <value>คำนำหน้า (ภาษาไทย) (titlename)</value>
    [DataMember(Name="titleTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "titleTH")]
    public string TitleTH { get; set; }

    /// <summary>
    /// ชื่อจริง (ภาษาไทย) (firstname)
    /// </summary>
    /// <value>ชื่อจริง (ภาษาไทย) (firstname)</value>
    [DataMember(Name="firstNameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "firstNameTH")]
    public string FirstNameTH { get; set; }

    /// <summary>
    /// นามสกุล (ภาษาไทย) (lastname)
    /// </summary>
    /// <value>นามสกุล (ภาษาไทย) (lastname)</value>
    [DataMember(Name="lastNameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastNameTH")]
    public string LastNameTH { get; set; }

    /// <summary>
    /// คำนำหน้า (ภาษาอังกฤษ) (ENPrefix)
    /// </summary>
    /// <value>คำนำหน้า (ภาษาอังกฤษ) (ENPrefix)</value>
    [DataMember(Name="titleEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "titleEN")]
    public string TitleEN { get; set; }

    /// <summary>
    /// ชื่อจริง (ภาษาอังกฤษ) (ENFirstname)
    /// </summary>
    /// <value>ชื่อจริง (ภาษาอังกฤษ) (ENFirstname)</value>
    [DataMember(Name="firstNameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "firstNameEN")]
    public string FirstNameEN { get; set; }

    /// <summary>
    /// นามสกุล (ภาษาอังกฤษ) (ENLastName)
    /// </summary>
    /// <value>นามสกุล (ภาษาอังกฤษ) (ENLastName)</value>
    [DataMember(Name="lastNameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastNameEN")]
    public string LastNameEN { get; set; }

    /// <summary>
    /// เพศ (gender)
    /// </summary>
    /// <value>เพศ (gender)</value>
    [DataMember(Name="gender", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "gender")]
    public string Gender { get; set; }

    /// <summary>
    /// วันเกิด (dateofbirth)
    /// </summary>
    /// <value>วันเกิด (dateofbirth)</value>
    [DataMember(Name="birthDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "birthDate")]
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// กรุ๊ปเลือด (bloodtype)
    /// </summary>
    /// <value>กรุ๊ปเลือด (bloodtype)</value>
    [DataMember(Name="bloodType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "bloodType")]
    public string BloodType { get; set; }

    /// <summary>
    /// บ้านเลขที่ (address_houseid)
    /// </summary>
    /// <value>บ้านเลขที่ (address_houseid)</value>
    [DataMember(Name="houseNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "houseNo")]
    public string HouseNo { get; set; }

    /// <summary>
    /// หมู่ที่ (address_village)
    /// </summary>
    /// <value>หมู่ที่ (address_village)</value>
    [DataMember(Name="moo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "moo")]
    public string Moo { get; set; }

    /// <summary>
    /// หมู่บ้าน/อาคาร (ทีอยู่) (address_fullformated)
    /// </summary>
    /// <value>หมู่บ้าน/อาคาร (ทีอยู่) (address_fullformated)</value>
    [DataMember(Name="village", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "village")]
    public string Village { get; set; }

    /// <summary>
    /// ซอย (address_lane)
    /// </summary>
    /// <value>ซอย (address_lane)</value>
    [DataMember(Name="soi", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "soi")]
    public string Soi { get; set; }

    /// <summary>
    /// ถนน (address_road)
    /// </summary>
    /// <value>ถนน (address_road)</value>
    [DataMember(Name="road", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "road")]
    public string Road { get; set; }

    /// <summary>
    /// รหัสไปรษณีย์ (address_postcode)
    /// </summary>
    /// <value>รหัสไปรษณีย์ (address_postcode)</value>
    [DataMember(Name="postalCode", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "postalCode")]
    public string PostalCode { get; set; }

    /// <summary>
    /// ประเทศ (address_country)
    /// </summary>
    /// <value>ประเทศ (address_country)</value>
    [DataMember(Name="country", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "country")]
    public string Country { get; set; }

    /// <summary>
    /// จังหวัด (address_province)
    /// </summary>
    /// <value>จังหวัด (address_province)</value>
    [DataMember(Name="province", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "province")]
    public string Province { get; set; }

    /// <summary>
    /// เขต/อำเภอ (address_district)
    /// </summary>
    /// <value>เขต/อำเภอ (address_district)</value>
    [DataMember(Name="district", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "district")]
    public string District { get; set; }

    /// <summary>
    /// แขวง/ตำบล (address_subdistrict)
    /// </summary>
    /// <value>แขวง/ตำบล (address_subdistrict)</value>
    [DataMember(Name="subDistrict", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "subDistrict")]
    public string SubDistrict { get; set; }

    /// <summary>
    /// สัญชาติ (address_nationality)
    /// </summary>
    /// <value>สัญชาติ (address_nationality)</value>
    [DataMember(Name="national", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "national")]
    public string National { get; set; }

    /// <summary>
    /// สถานที่ออกบัตร (Issuer)
    /// </summary>
    /// <value>สถานที่ออกบัตร (Issuer)</value>
    [DataMember(Name="issue", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "issue")]
    public string Issue { get; set; }

    /// <summary>
    /// วันที่ออกบัตร (CardIssueDate)
    /// </summary>
    /// <value>วันที่ออกบัตร (CardIssueDate)</value>
    [DataMember(Name="issueDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "issueDate")]
    public DateTime? IssueDate { get; set; }

    /// <summary>
    /// วันที่บัตรหมดอายุ (CardExpireDate)
    /// </summary>
    /// <value>วันที่บัตรหมดอายุ (CardExpireDate)</value>
    [DataMember(Name="issueExpireDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "issueExpireDate")]
    public DateTime? IssueExpireDate { get; set; }

    /// <summary>
    /// วันที่เข้าโครงการ (in_date)
    /// </summary>
    /// <value>วันที่เข้าโครงการ (in_date)</value>
    [DataMember(Name="visitDateIn", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitDateIn")]
    public DateTime? VisitDateIn { get; set; }

    /// <summary>
    /// วันที่ออกโครงการ (out_date)
    /// </summary>
    /// <value>วันที่ออกโครงการ (out_date)</value>
    [DataMember(Name="visitDateOut", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitDateOut")]
    public DateTime? VisitDateOut { get; set; }

    /// <summary>
    /// สถานะ/ประเภทบุคคลที่เข้าโครงการ (personal_visittype)
    /// </summary>
    /// <value>สถานะ/ประเภทบุคคลที่เข้าโครงการ (personal_visittype)</value>
    [DataMember(Name="contactStatusKey", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactStatusKey")]
    public string ContactStatusKey { get; set; }

    /// <summary>
    /// ชนิดของบัตรที่ใช้ยืนยันเข้าโครงการ (personal_visitcardtype)
    /// </summary>
    /// <value>ชนิดของบัตรที่ใช้ยืนยันเข้าโครงการ (personal_visitcardtype)</value>
    [DataMember(Name="personalVisitCardTypeKey", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "personalVisitCardTypeKey")]
    public string PersonalVisitCardTypeKey { get; set; }

    /// <summary>
    /// ข้อมลูรูปถ่ายจากกล้อง (personal_visitcardimage)
    /// </summary>
    /// <value>ข้อมลูรูปถ่ายจากกล้อง (personal_visitcardimage)</value>
    [DataMember(Name="personalVisitImageFromCard", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "personalVisitImageFromCard")]
    public string PersonalVisitImageFromCard { get; set; }

    /// <summary>
    /// ข้อมลูรูปถ่ายจากกล้อง (ชื่อไฟล์)
    /// </summary>
    /// <value>ข้อมลูรูปถ่ายจากกล้อง (ชื่อไฟล์)</value>
    [DataMember(Name="personalVisitImageName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "personalVisitImageName")]
    public string PersonalVisitImageName { get; set; }

    /// <summary>
    /// รูปแบบการเยียมชมโครงการ (customer_visitvia)
    /// </summary>
    /// <value>รูปแบบการเยียมชมโครงการ (customer_visitvia)</value>
    [DataMember(Name="visitByKey", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitByKey")]
    public string VisitByKey { get; set; }

    /// <summary>
    /// ประเภทรถยนต์ (vehicle_type)
    /// </summary>
    /// <value>ประเภทรถยนต์ (vehicle_type)</value>
    [DataMember(Name="vehicleKey", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "vehicleKey")]
    public string VehicleKey { get; set; }

    /// <summary>
    /// เลขทะเบียนรถ (\"vehicle_regist)
    /// </summary>
    /// <value>เลขทะเบียนรถ (\"vehicle_regist)</value>
    [DataMember(Name="vehicleRegistrationNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "vehicleRegistrationNo")]
    public string VehicleRegistrationNo { get; set; }

    /// <summary>
    /// สีรถ (vehicle_color)
    /// </summary>
    /// <value>สีรถ (vehicle_color)</value>
    [DataMember(Name="vehicleColor", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "vehicleColor")]
    public string VehicleColor { get; set; }

    /// <summary>
    /// ยี่ห้อรถ (vehicle_brand)
    /// </summary>
    /// <value>ยี่ห้อรถ (vehicle_brand)</value>
    [DataMember(Name="vehicleBrand", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "vehicleBrand")]
    public string VehicleBrand { get; set; }

    /// <summary>
    /// Transaction ID จากเครื่องรูดบัตร (trans_id)
    /// </summary>
    /// <value>Transaction ID จากเครื่องรูดบัตร (trans_id)</value>
    [DataMember(Name="visitKioskTransactionID", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitKioskTransactionID")]
    public string VisitKioskTransactionID { get; set; }

    /// <summary>
    /// Device ID ของเครื่องรูดบัตร (deviceid)
    /// </summary>
    /// <value>Device ID ของเครื่องรูดบัตร (deviceid)</value>
    [DataMember(Name="visitKioskDeviceID", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitKioskDeviceID")]
    public string VisitKioskDeviceID { get; set; }

    /// <summary>
    /// เลขรับรายการเยี่ยมชมโครงการ (รหัสรันนิงบัตรจาก offline) (printrunning_label)
    /// </summary>
    /// <value>เลขรับรายการเยี่ยมชมโครงการ (รหัสรันนิงบัตรจาก offline) (printrunning_label)</value>
    [DataMember(Name="visitorRunning", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitorRunning")]
    public string VisitorRunning { get; set; }

    /// <summary>
    /// เบอร์โทรศัพท์
    /// </summary>
    /// <value>เบอร์โทรศัพท์</value>
    [DataMember(Name="phoneNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phoneNumber")]
    public string PhoneNumber { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class VisitorCreateDTO {\n");
      sb.Append("  ProjectNo: ").Append(ProjectNo).Append("\n");
      sb.Append("  CitizenIdentityNo: ").Append(CitizenIdentityNo).Append("\n");
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
      sb.Append("  VisitDateIn: ").Append(VisitDateIn).Append("\n");
      sb.Append("  VisitDateOut: ").Append(VisitDateOut).Append("\n");
      sb.Append("  ContactStatusKey: ").Append(ContactStatusKey).Append("\n");
      sb.Append("  PersonalVisitCardTypeKey: ").Append(PersonalVisitCardTypeKey).Append("\n");
      sb.Append("  PersonalVisitImageFromCard: ").Append(PersonalVisitImageFromCard).Append("\n");
      sb.Append("  PersonalVisitImageName: ").Append(PersonalVisitImageName).Append("\n");
      sb.Append("  VisitByKey: ").Append(VisitByKey).Append("\n");
      sb.Append("  VehicleKey: ").Append(VehicleKey).Append("\n");
      sb.Append("  VehicleRegistrationNo: ").Append(VehicleRegistrationNo).Append("\n");
      sb.Append("  VehicleColor: ").Append(VehicleColor).Append("\n");
      sb.Append("  VehicleBrand: ").Append(VehicleBrand).Append("\n");
      sb.Append("  VisitKioskTransactionID: ").Append(VisitKioskTransactionID).Append("\n");
      sb.Append("  VisitKioskDeviceID: ").Append(VisitKioskDeviceID).Append("\n");
      sb.Append("  VisitorRunning: ").Append(VisitorRunning).Append("\n");
      sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
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
