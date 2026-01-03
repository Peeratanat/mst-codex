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
  public class ContactDTO {
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
    /// ประเภท Contact  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactType
    /// </summary>
    /// <value>ประเภท Contact  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactType</value>
    [DataMember(Name="contactType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactType")]
    public MasterCenterDropdownDTO ContactType { get; set; }

    /// <summary>
    /// คำนำหน้าชื่อ (ภาษาไทย)
    /// </summary>
    /// <value>คำนำหน้าชื่อ (ภาษาไทย)</value>
    [DataMember(Name="titleTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "titleTH")]
    public MasterCenterDropdownDTO TitleTH { get; set; }

    /// <summary>
    /// คำนำหน้าชื่อเพิ่มเติม
    /// </summary>
    /// <value>คำนำหน้าชื่อเพิ่มเติม</value>
    [DataMember(Name="titleExtTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "titleExtTH")]
    public string TitleExtTH { get; set; }

    /// <summary>
    /// ชื่อจริง/ชื่อบริษัท (ภาษาไทย)
    /// </summary>
    /// <value>ชื่อจริง/ชื่อบริษัท (ภาษาไทย)</value>
    [DataMember(Name="firstNameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "firstNameTH")]
    public string FirstNameTH { get; set; }

    /// <summary>
    /// ชื่อกลาง (ภาษาไทย)
    /// </summary>
    /// <value>ชื่อกลาง (ภาษาไทย)</value>
    [DataMember(Name="middleNameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "middleNameTH")]
    public string MiddleNameTH { get; set; }

    /// <summary>
    /// นามสกุล (ภาษาไทย)
    /// </summary>
    /// <value>นามสกุล (ภาษาไทย)</value>
    [DataMember(Name="lastNameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastNameTH")]
    public string LastNameTH { get; set; }

    /// <summary>
    /// ชื่อเล่น (ภาษาไทย)
    /// </summary>
    /// <value>ชื่อเล่น (ภาษาไทย)</value>
    [DataMember(Name="nickname", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "nickname")]
    public string Nickname { get; set; }

    /// <summary>
    /// คำนำหน้าชื่อ (ภาษาอังกฤษ)  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactTitleEN
    /// </summary>
    /// <value>คำนำหน้าชื่อ (ภาษาอังกฤษ)  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactTitleEN</value>
    [DataMember(Name="titleEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "titleEN")]
    public MasterCenterDropdownDTO TitleEN { get; set; }

    /// <summary>
    /// คำนำหน้าชื่อเพิ่มเติม (ภาษาอังกฤษ)
    /// </summary>
    /// <value>คำนำหน้าชื่อเพิ่มเติม (ภาษาอังกฤษ)</value>
    [DataMember(Name="titleExtEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "titleExtEN")]
    public string TitleExtEN { get; set; }

    /// <summary>
    /// ชื่อจริง (ภาษาอังกฤษ)
    /// </summary>
    /// <value>ชื่อจริง (ภาษาอังกฤษ)</value>
    [DataMember(Name="firstNameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "firstNameEN")]
    public string FirstNameEN { get; set; }

    /// <summary>
    /// ชื่อกลาง (ภาษาอังกฤษ)
    /// </summary>
    /// <value>ชื่อกลาง (ภาษาอังกฤษ)</value>
    [DataMember(Name="middleNameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "middleNameEN")]
    public string MiddleNameEN { get; set; }

    /// <summary>
    /// นามสกุล (ภาษาอังกฤษ)
    /// </summary>
    /// <value>นามสกุล (ภาษาอังกฤษ)</value>
    [DataMember(Name="lastNameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastNameEN")]
    public string LastNameEN { get; set; }

    /// <summary>
    /// สัญชาติ
    /// </summary>
    /// <value>สัญชาติ</value>
    [DataMember(Name="national", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "national")]
    public MasterCenterDropdownDTO National { get; set; }

    /// <summary>
    /// เลขที่บัตรประชาชน/Passport
    /// </summary>
    /// <value>เลขที่บัตรประชาชน/Passport</value>
    [DataMember(Name="citizenIdentityNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "citizenIdentityNo")]
    public string CitizenIdentityNo { get; set; }

    /// <summary>
    /// วันหมดอายุบัตรประชาชน
    /// </summary>
    /// <value>วันหมดอายุบัตรประชาชน</value>
    [DataMember(Name="citizenExpireDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "citizenExpireDate")]
    public DateTime? CitizenExpireDate { get; set; }

    /// <summary>
    /// วันเกิด
    /// </summary>
    /// <value>วันเกิด</value>
    [DataMember(Name="birthDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "birthDate")]
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// เพศ
    /// </summary>
    /// <value>เพศ</value>
    [DataMember(Name="gender", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "gender")]
    public MasterCenterDropdownDTO Gender { get; set; }

    /// <summary>
    /// เลขที่ภาษี
    /// </summary>
    /// <value>เลขที่ภาษี</value>
    [DataMember(Name="taxID", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "taxID")]
    public string TaxID { get; set; }

    /// <summary>
    /// เบอร์โทรศัพท์ของบริษัท (กรณีนิติบุคคล)
    /// </summary>
    /// <value>เบอร์โทรศัพท์ของบริษัท (กรณีนิติบุคคล)</value>
    [DataMember(Name="phoneNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phoneNumber")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// หมายเลขต่อของบริษัท (กรณีนิติบุคคล)
    /// </summary>
    /// <value>หมายเลขต่อของบริษัท (กรณีนิติบุคคล)</value>
    [DataMember(Name="phoneNumberExt", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phoneNumberExt")]
    public string PhoneNumberExt { get; set; }

    /// <summary>
    /// ชื่อจริงของ Contact
    /// </summary>
    /// <value>ชื่อจริงของ Contact</value>
    [DataMember(Name="contactFirstName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactFirstName")]
    public string ContactFirstName { get; set; }

    /// <summary>
    /// นางสกุลของ Contact
    /// </summary>
    /// <value>นางสกุลของ Contact</value>
    [DataMember(Name="contactLastname", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactLastname")]
    public string ContactLastname { get; set; }

    /// <summary>
    /// ID Wechat
    /// </summary>
    /// <value>ID Wechat</value>
    [DataMember(Name="weChat", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "weChat")]
    public string WeChat { get; set; }

    /// <summary>
    /// ID Whatapp
    /// </summary>
    /// <value>ID Whatapp</value>
    [DataMember(Name="whatsApp", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "whatsApp")]
    public string WhatsApp { get; set; }

    /// <summary>
    /// ID Line
    /// </summary>
    /// <value>ID Line</value>
    [DataMember(Name="lineID", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lineID")]
    public string LineID { get; set; }

    /// <summary>
    /// ลูกค้า VIP (true = เป็น/false = ไม่เป็น)
    /// </summary>
    /// <value>ลูกค้า VIP (true = เป็น/false = ไม่เป็น)</value>
    [DataMember(Name="isVIP", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isVIP")]
    public bool? IsVIP { get; set; }

    /// <summary>
    /// ลำดับของ Contact
    /// </summary>
    /// <value>ลำดับของ Contact</value>
    [DataMember(Name="order", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "order")]
    public int? Order { get; set; }

    /// <summary>
    /// เป็นคนไทยหรือไม่ (true = เป็น/false = ไม่เป็น)
    /// </summary>
    /// <value>เป็นคนไทยหรือไม่ (true = เป็น/false = ไม่เป็น)</value>
    [DataMember(Name="isThaiNationality", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isThaiNationality")]
    public bool? IsThaiNationality { get; set; }

    /// <summary>
    /// รายการอีเมลของ Contact
    /// </summary>
    /// <value>รายการอีเมลของ Contact</value>
    [DataMember(Name="contactEmails", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactEmails")]
    public List<ContactEmailDTO> ContactEmails { get; set; }

    /// <summary>
    /// รายการโทรศัพท์ของ Contact
    /// </summary>
    /// <value>รายการโทรศัพท์ของ Contact</value>
    [DataMember(Name="contactPhones", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactPhones")]
    public List<ContactPhoneDTO> ContactPhones { get; set; }

    /// <summary>
    /// ID ของ Lead สำหรับการ Qualify กรณีสร้าง Contact ใหม่
    /// </summary>
    /// <value>ID ของ Lead สำหรับการ Qualify กรณีสร้าง Contact ใหม่</value>
    [DataMember(Name="leadID", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadID")]
    public Guid? LeadID { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ContactDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  ContactNo: ").Append(ContactNo).Append("\n");
      sb.Append("  ContactType: ").Append(ContactType).Append("\n");
      sb.Append("  TitleTH: ").Append(TitleTH).Append("\n");
      sb.Append("  TitleExtTH: ").Append(TitleExtTH).Append("\n");
      sb.Append("  FirstNameTH: ").Append(FirstNameTH).Append("\n");
      sb.Append("  MiddleNameTH: ").Append(MiddleNameTH).Append("\n");
      sb.Append("  LastNameTH: ").Append(LastNameTH).Append("\n");
      sb.Append("  Nickname: ").Append(Nickname).Append("\n");
      sb.Append("  TitleEN: ").Append(TitleEN).Append("\n");
      sb.Append("  TitleExtEN: ").Append(TitleExtEN).Append("\n");
      sb.Append("  FirstNameEN: ").Append(FirstNameEN).Append("\n");
      sb.Append("  MiddleNameEN: ").Append(MiddleNameEN).Append("\n");
      sb.Append("  LastNameEN: ").Append(LastNameEN).Append("\n");
      sb.Append("  National: ").Append(National).Append("\n");
      sb.Append("  CitizenIdentityNo: ").Append(CitizenIdentityNo).Append("\n");
      sb.Append("  CitizenExpireDate: ").Append(CitizenExpireDate).Append("\n");
      sb.Append("  BirthDate: ").Append(BirthDate).Append("\n");
      sb.Append("  Gender: ").Append(Gender).Append("\n");
      sb.Append("  TaxID: ").Append(TaxID).Append("\n");
      sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
      sb.Append("  PhoneNumberExt: ").Append(PhoneNumberExt).Append("\n");
      sb.Append("  ContactFirstName: ").Append(ContactFirstName).Append("\n");
      sb.Append("  ContactLastname: ").Append(ContactLastname).Append("\n");
      sb.Append("  WeChat: ").Append(WeChat).Append("\n");
      sb.Append("  WhatsApp: ").Append(WhatsApp).Append("\n");
      sb.Append("  LineID: ").Append(LineID).Append("\n");
      sb.Append("  IsVIP: ").Append(IsVIP).Append("\n");
      sb.Append("  Order: ").Append(Order).Append("\n");
      sb.Append("  IsThaiNationality: ").Append(IsThaiNationality).Append("\n");
      sb.Append("  ContactEmails: ").Append(ContactEmails).Append("\n");
      sb.Append("  ContactPhones: ").Append(ContactPhones).Append("\n");
      sb.Append("  LeadID: ").Append(LeadID).Append("\n");
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
