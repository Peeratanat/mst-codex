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
  public class LeadDTO {
    /// <summary>
    /// ID ของ Lead
    /// </summary>
    /// <value>ID ของ Lead</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// ชื่อจริง
    /// </summary>
    /// <value>ชื่อจริง</value>
    [DataMember(Name="firstName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "firstName")]
    public string FirstName { get; set; }

    /// <summary>
    /// นามสกุล
    /// </summary>
    /// <value>นามสกุล</value>
    [DataMember(Name="lastName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastName")]
    public string LastName { get; set; }

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
    [DataMember(Name="telephone", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "telephone")]
    public string Telephone { get; set; }

    /// <summary>
    /// เบอร์โทรศัพท์ (ต่อ)
    /// </summary>
    /// <value>เบอร์โทรศัพท์ (ต่อ)</value>
    [DataMember(Name="telephoneExt", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "telephoneExt")]
    public string TelephoneExt { get; set; }

    /// <summary>
    /// อีเมล
    /// </summary>
    /// <value>อีเมล</value>
    [DataMember(Name="email", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }

    /// <summary>
    /// ประเภทของ Lead  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadType
    /// </summary>
    /// <value>ประเภทของ Lead  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadType</value>
    [DataMember(Name="leadType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadType")]
    public MasterCenterDropdownDTO LeadType { get; set; }

    /// <summary>
    /// ประเภทย่อยของ Lead
    /// </summary>
    /// <value>ประเภทย่อยของ Lead</value>
    [DataMember(Name="subLeadType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "subLeadType")]
    public string SubLeadType { get; set; }

    /// <summary>
    /// ผู้ดูแล Lead
    /// </summary>
    /// <value>ผู้ดูแล Lead</value>
    [DataMember(Name="owner", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "owner")]
    public UserListDTO Owner { get; set; }

    /// <summary>
    /// โครงการ  project/api/Projects/DropdownList
    /// </summary>
    /// <value>โครงการ  project/api/Projects/DropdownList</value>
    [DataMember(Name="project", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "project")]
    public ProjectDTO Project { get; set; }

    /// <summary>
    /// โซนที่พักอาศัย
    /// </summary>
    /// <value>โซนที่พักอาศัย</value>
    [DataMember(Name="visitZone", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitZone")]
    public string VisitZone { get; set; }

    /// <summary>
    /// มาจากสื่อ  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Advertisement
    /// </summary>
    /// <value>มาจากสื่อ  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Advertisement</value>
    [DataMember(Name="advertisement", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "advertisement")]
    public MasterCenterDropdownDTO Advertisement { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    /// <value>Remark</value>
    [DataMember(Name="remark", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "remark")]
    public string Remark { get; set; }

    /// <summary>
    /// คะแนนของ Lead
    /// </summary>
    /// <value>คะแนนของ Lead</value>
    [DataMember(Name="leadScore", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadScore")]
    public double? LeadScore { get; set; }

    /// <summary>
    /// สถานะของ Lead  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadStatus
    /// </summary>
    /// <value>สถานะของ Lead  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadStatus</value>
    [DataMember(Name="leadStatus", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadStatus")]
    public MasterCenterDropdownDTO LeadStatus { get; set; }

    /// <summary>
    /// Campaign
    /// </summary>
    /// <value>Campaign</value>
    [DataMember(Name="campaign", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "campaign")]
    public string Campaign { get; set; }

    /// <summary>
    /// บ้านเลขที่
    /// </summary>
    /// <value>บ้านเลขที่</value>
    [DataMember(Name="houseNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "houseNo")]
    public string HouseNo { get; set; }

    /// <summary>
    /// หมู่
    /// </summary>
    /// <value>หมู่</value>
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
    /// ขนาดห้อง
    /// </summary>
    /// <value>ขนาดห้อง</value>
    [DataMember(Name="roomSize", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "roomSize")]
    public string RoomSize { get; set; }

    /// <summary>
    /// ประเภทห้อง
    /// </summary>
    /// <value>ประเภทห้อง</value>
    [DataMember(Name="roomType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "roomType")]
    public string RoomType { get; set; }

    /// <summary>
    /// จำนวนคน
    /// </summary>
    /// <value>จำนวนคน</value>
    [DataMember(Name="numberOfPerson", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "numberOfPerson")]
    public string NumberOfPerson { get; set; }

    /// <summary>
    /// จำนวน Unit
    /// </summary>
    /// <value>จำนวน Unit</value>
    [DataMember(Name="numberOfUnit", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "numberOfUnit")]
    public string NumberOfUnit { get; set; }

    /// <summary>
    /// จำนวนครั้งที่ติดต่อ
    /// </summary>
    /// <value>จำนวนครั้งที่ติดต่อ</value>
    [DataMember(Name="numberOfContact", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "numberOfContact")]
    public string NumberOfContact { get; set; }

    /// <summary>
    /// อำเภอที่ทำงาน
    /// </summary>
    /// <value>อำเภอที่ทำงาน</value>
    [DataMember(Name="districtOfWorking", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "districtOfWorking")]
    public string DistrictOfWorking { get; set; }

    /// <summary>
    /// จังหวัดที่ทำงาน
    /// </summary>
    /// <value>จังหวัดที่ทำงาน</value>
    [DataMember(Name="provinceOfWorking", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "provinceOfWorking")]
    public string ProvinceOfWorking { get; set; }

    /// <summary>
    /// LeadVisitDate
    /// </summary>
    /// <value>LeadVisitDate</value>
    [DataMember(Name="leadVisitDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadVisitDate")]
    public DateTime? LeadVisitDate { get; set; }

    /// <summary>
    /// LeadVisitTime
    /// </summary>
    /// <value>LeadVisitTime</value>
    [DataMember(Name="leadVisitTime", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadVisitTime")]
    public string LeadVisitTime { get; set; }

    /// <summary>
    /// ลูกค้าต้องการให้โทรกลับหรือไม่?
    /// </summary>
    /// <value>ลูกค้าต้องการให้โทรกลับหรือไม่?</value>
    [DataMember(Name="callBack", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "callBack")]
    public bool? CallBack { get; set; }

    /// <summary>
    /// หมายเลขบัตรประชาชน
    /// </summary>
    /// <value>หมายเลขบัตรประชาชน</value>
    [DataMember(Name="citizenIdentityNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "citizenIdentityNo")]
    public string CitizenIdentityNo { get; set; }

    /// <summary>
    /// UTMSource
    /// </summary>
    /// <value>UTMSource</value>
    [DataMember(Name="utmSource", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "utmSource")]
    public string UtmSource { get; set; }

    /// <summary>
    /// UTMMedium
    /// </summary>
    /// <value>UTMMedium</value>
    [DataMember(Name="utmMedium", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "utmMedium")]
    public string UtmMedium { get; set; }

    /// <summary>
    /// UTMCampaign
    /// </summary>
    /// <value>UTMCampaign</value>
    [DataMember(Name="utmCampaign", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "utmCampaign")]
    public string UtmCampaign { get; set; }

    /// <summary>
    /// วันที่สร้าง
    /// </summary>
    /// <value>วันที่สร้าง</value>
    [DataMember(Name="createdDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Gets or Sets LeadScoreList
    /// </summary>
    [DataMember(Name="leadScoreList", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadScoreList")]
    public List<LeadScoreListDTO> LeadScoreList { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class LeadDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  FirstName: ").Append(FirstName).Append("\n");
      sb.Append("  LastName: ").Append(LastName).Append("\n");
      sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
      sb.Append("  Telephone: ").Append(Telephone).Append("\n");
      sb.Append("  TelephoneExt: ").Append(TelephoneExt).Append("\n");
      sb.Append("  Email: ").Append(Email).Append("\n");
      sb.Append("  LeadType: ").Append(LeadType).Append("\n");
      sb.Append("  SubLeadType: ").Append(SubLeadType).Append("\n");
      sb.Append("  Owner: ").Append(Owner).Append("\n");
      sb.Append("  Project: ").Append(Project).Append("\n");
      sb.Append("  VisitZone: ").Append(VisitZone).Append("\n");
      sb.Append("  Advertisement: ").Append(Advertisement).Append("\n");
      sb.Append("  Remark: ").Append(Remark).Append("\n");
      sb.Append("  LeadScore: ").Append(LeadScore).Append("\n");
      sb.Append("  LeadStatus: ").Append(LeadStatus).Append("\n");
      sb.Append("  Campaign: ").Append(Campaign).Append("\n");
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
      sb.Append("  RoomSize: ").Append(RoomSize).Append("\n");
      sb.Append("  RoomType: ").Append(RoomType).Append("\n");
      sb.Append("  NumberOfPerson: ").Append(NumberOfPerson).Append("\n");
      sb.Append("  NumberOfUnit: ").Append(NumberOfUnit).Append("\n");
      sb.Append("  NumberOfContact: ").Append(NumberOfContact).Append("\n");
      sb.Append("  DistrictOfWorking: ").Append(DistrictOfWorking).Append("\n");
      sb.Append("  ProvinceOfWorking: ").Append(ProvinceOfWorking).Append("\n");
      sb.Append("  LeadVisitDate: ").Append(LeadVisitDate).Append("\n");
      sb.Append("  LeadVisitTime: ").Append(LeadVisitTime).Append("\n");
      sb.Append("  CallBack: ").Append(CallBack).Append("\n");
      sb.Append("  CitizenIdentityNo: ").Append(CitizenIdentityNo).Append("\n");
      sb.Append("  UtmSource: ").Append(UtmSource).Append("\n");
      sb.Append("  UtmMedium: ").Append(UtmMedium).Append("\n");
      sb.Append("  UtmCampaign: ").Append(UtmCampaign).Append("\n");
      sb.Append("  CreatedDate: ").Append(CreatedDate).Append("\n");
      sb.Append("  LeadScoreList: ").Append(LeadScoreList).Append("\n");
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
