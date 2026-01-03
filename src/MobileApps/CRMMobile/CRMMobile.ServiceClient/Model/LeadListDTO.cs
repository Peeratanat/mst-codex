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
  public class LeadListDTO {
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
    /// ประเภทของ Lead  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadType
    /// </summary>
    /// <value>ประเภทของ Lead  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadType</value>
    [DataMember(Name="leadType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadType")]
    public MasterCenterDropdownDTO LeadType { get; set; }

    /// <summary>
    /// สื่อโฆษณา (มาจากสื่อ)  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Advertisement
    /// </summary>
    /// <value>สื่อโฆษณา (มาจากสื่อ)  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Advertisement</value>
    [DataMember(Name="advertisement", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "advertisement")]
    public MasterCenterDropdownDTO Advertisement { get; set; }

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
    /// วันที่สร้าง
    /// </summary>
    /// <value>วันที่สร้าง</value>
    [DataMember(Name="createdDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// หมายเหตุ
    /// </summary>
    /// <value>หมายเหตุ</value>
    [DataMember(Name="remark", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "remark")]
    public string Remark { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class LeadListDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  FirstName: ").Append(FirstName).Append("\n");
      sb.Append("  LastName: ").Append(LastName).Append("\n");
      sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
      sb.Append("  LeadType: ").Append(LeadType).Append("\n");
      sb.Append("  Advertisement: ").Append(Advertisement).Append("\n");
      sb.Append("  Owner: ").Append(Owner).Append("\n");
      sb.Append("  Project: ").Append(Project).Append("\n");
      sb.Append("  CreatedDate: ").Append(CreatedDate).Append("\n");
      sb.Append("  Remark: ").Append(Remark).Append("\n");
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
