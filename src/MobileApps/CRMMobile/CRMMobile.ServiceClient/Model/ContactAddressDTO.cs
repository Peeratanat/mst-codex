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
  public class ContactAddressDTO {
    /// <summary>
    /// ID ของที่อยู่
    /// </summary>
    /// <value>ID ของที่อยู่</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// ID ของ Contact
    /// </summary>
    /// <value>ID ของ Contact</value>
    [DataMember(Name="contactID", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactID")]
    public Guid? ContactID { get; set; }

    /// <summary>
    /// ประเภทของที่อยู่ (ตามที่อยู่บัตรประชาชน/ติดต่อได้/ทะเบียนบ้าน/ที่ทำงาน)  master/api/MasterCenters?masterCenterGroupKey=ContactAddressType
    /// </summary>
    /// <value>ประเภทของที่อยู่ (ตามที่อยู่บัตรประชาชน/ติดต่อได้/ทะเบียนบ้าน/ที่ทำงาน)  master/api/MasterCenters?masterCenterGroupKey=ContactAddressType</value>
    [DataMember(Name="contactAddressType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactAddressType")]
    public MasterCenterDropdownDTO ContactAddressType { get; set; }

    /// <summary>
    /// โครงการ  project/api/Projects/DropdownList
    /// </summary>
    /// <value>โครงการ  project/api/Projects/DropdownList</value>
    [DataMember(Name="project", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "project")]
    public ProjectDTO Project { get; set; }

    /// <summary>
    /// บ้านเลขที่ (ภาษาไทย)
    /// </summary>
    /// <value>บ้านเลขที่ (ภาษาไทย)</value>
    [DataMember(Name="houseNoTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "houseNoTH")]
    public string HouseNoTH { get; set; }

    /// <summary>
    /// หมู่ที่ (ภาษาไทย)
    /// </summary>
    /// <value>หมู่ที่ (ภาษาไทย)</value>
    [DataMember(Name="mooTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "mooTH")]
    public string MooTH { get; set; }

    /// <summary>
    /// ชื่อหมู่บ้าน (ภาษาไทย)
    /// </summary>
    /// <value>ชื่อหมู่บ้าน (ภาษาไทย)</value>
    [DataMember(Name="villageTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "villageTH")]
    public string VillageTH { get; set; }

    /// <summary>
    /// ซอย (ภาษาไทย)
    /// </summary>
    /// <value>ซอย (ภาษาไทย)</value>
    [DataMember(Name="soiTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "soiTH")]
    public string SoiTH { get; set; }

    /// <summary>
    /// ถนน (ภาษาไทย)
    /// </summary>
    /// <value>ถนน (ภาษาไทย)</value>
    [DataMember(Name="roadTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "roadTH")]
    public string RoadTH { get; set; }

    /// <summary>
    /// บ้านเลขที่ (ภาษาอังกฤษ)
    /// </summary>
    /// <value>บ้านเลขที่ (ภาษาอังกฤษ)</value>
    [DataMember(Name="houseNoEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "houseNoEN")]
    public string HouseNoEN { get; set; }

    /// <summary>
    /// หมู่ที่ (ภาษาอังกฤษ)
    /// </summary>
    /// <value>หมู่ที่ (ภาษาอังกฤษ)</value>
    [DataMember(Name="mooEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "mooEN")]
    public string MooEN { get; set; }

    /// <summary>
    /// ชื่อหมู่บ้าน (ภาษาอังกฤษ)
    /// </summary>
    /// <value>ชื่อหมู่บ้าน (ภาษาอังกฤษ)</value>
    [DataMember(Name="villageEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "villageEN")]
    public string VillageEN { get; set; }

    /// <summary>
    /// ซอย (ภาษาอังกฤษ)
    /// </summary>
    /// <value>ซอย (ภาษาอังกฤษ)</value>
    [DataMember(Name="soiEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "soiEN")]
    public string SoiEN { get; set; }

    /// <summary>
    /// ถนน (ภาษาอังกฤษ)
    /// </summary>
    /// <value>ถนน (ภาษาอังกฤษ)</value>
    [DataMember(Name="roadEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "roadEN")]
    public string RoadEN { get; set; }

    /// <summary>
    /// รหัสไปรษณีย์
    /// </summary>
    /// <value>รหัสไปรษณีย์</value>
    [DataMember(Name="postalCode", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "postalCode")]
    public string PostalCode { get; set; }

    /// <summary>
    /// ประเทศ  Master/api/Countries
    /// </summary>
    /// <value>ประเทศ  Master/api/Countries</value>
    [DataMember(Name="country", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "country")]
    public CountryDTO Country { get; set; }

    /// <summary>
    /// จังหวัด  Master/api/Provinces/DropdownList
    /// </summary>
    /// <value>จังหวัด  Master/api/Provinces/DropdownList</value>
    [DataMember(Name="province", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "province")]
    public ProvinceListDTO Province { get; set; }

    /// <summary>
    /// อำเภอ  Master/api/Districts/DropdownList
    /// </summary>
    /// <value>อำเภอ  Master/api/Districts/DropdownList</value>
    [DataMember(Name="district", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "district")]
    public DistrictListDTO District { get; set; }

    /// <summary>
    /// ตำบล  Master/api/SubDistricts/DropdownList
    /// </summary>
    /// <value>ตำบล  Master/api/SubDistricts/DropdownList</value>
    [DataMember(Name="subDistrict", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "subDistrict")]
    public SubDistrictListDTO SubDistrict { get; set; }

    /// <summary>
    /// จังหวัด (ต่างประเทศ)
    /// </summary>
    /// <value>จังหวัด (ต่างประเทศ)</value>
    [DataMember(Name="foreignProvince", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "foreignProvince")]
    public string ForeignProvince { get; set; }

    /// <summary>
    /// อำเภอ (ต่างประเทศ)
    /// </summary>
    /// <value>อำเภอ (ต่างประเทศ)</value>
    [DataMember(Name="foreignDistrict", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "foreignDistrict")]
    public string ForeignDistrict { get; set; }

    /// <summary>
    /// ตำบล (ต่างประเทศ)
    /// </summary>
    /// <value>ตำบล (ต่างประเทศ)</value>
    [DataMember(Name="foreignSubDistrict", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "foreignSubDistrict")]
    public string ForeignSubDistrict { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ContactAddressDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  ContactID: ").Append(ContactID).Append("\n");
      sb.Append("  ContactAddressType: ").Append(ContactAddressType).Append("\n");
      sb.Append("  Project: ").Append(Project).Append("\n");
      sb.Append("  HouseNoTH: ").Append(HouseNoTH).Append("\n");
      sb.Append("  MooTH: ").Append(MooTH).Append("\n");
      sb.Append("  VillageTH: ").Append(VillageTH).Append("\n");
      sb.Append("  SoiTH: ").Append(SoiTH).Append("\n");
      sb.Append("  RoadTH: ").Append(RoadTH).Append("\n");
      sb.Append("  HouseNoEN: ").Append(HouseNoEN).Append("\n");
      sb.Append("  MooEN: ").Append(MooEN).Append("\n");
      sb.Append("  VillageEN: ").Append(VillageEN).Append("\n");
      sb.Append("  SoiEN: ").Append(SoiEN).Append("\n");
      sb.Append("  RoadEN: ").Append(RoadEN).Append("\n");
      sb.Append("  PostalCode: ").Append(PostalCode).Append("\n");
      sb.Append("  Country: ").Append(Country).Append("\n");
      sb.Append("  Province: ").Append(Province).Append("\n");
      sb.Append("  District: ").Append(District).Append("\n");
      sb.Append("  SubDistrict: ").Append(SubDistrict).Append("\n");
      sb.Append("  ForeignProvince: ").Append(ForeignProvince).Append("\n");
      sb.Append("  ForeignDistrict: ").Append(ForeignDistrict).Append("\n");
      sb.Append("  ForeignSubDistrict: ").Append(ForeignSubDistrict).Append("\n");
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
