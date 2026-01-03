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
  public class ProjectDTO {
    /// <summary>
    /// รหัสโครงการ
    /// </summary>
    /// <value>รหัสโครงการ</value>
    [DataMember(Name="projectNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "projectNo")]
    public string ProjectNo { get; set; }

    /// <summary>
    /// ชื่อโครงการ (TH)
    /// </summary>
    /// <value>ชื่อโครงการ (TH)</value>
    [DataMember(Name="projectNameTH", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "projectNameTH")]
    public string ProjectNameTH { get; set; }

    /// <summary>
    /// ชื่อโครงการ (EN)
    /// </summary>
    /// <value>ชื่อโครงการ (EN)</value>
    [DataMember(Name="projectNameEN", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "projectNameEN")]
    public string ProjectNameEN { get; set; }

    /// <summary>
    /// แบรนด์  Master/api/Brands/DropdownList
    /// </summary>
    /// <value>แบรนด์  Master/api/Brands/DropdownList</value>
    [DataMember(Name="brand", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "brand")]
    public BrandDropdownDTO Brand { get; set; }

    /// <summary>
    /// บริษัท  Master/api/Companies/DropdownList
    /// </summary>
    /// <value>บริษัท  Master/api/Companies/DropdownList</value>
    [DataMember(Name="company", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "company")]
    public CompanyDropdownDTO Company { get; set; }

    /// <summary>
    /// Master/api/MasterCenters?masterCenterGroupKey=ProductType  ประเภทของโครงการ  (แนวราบ, แนวสูง)
    /// </summary>
    /// <value>Master/api/MasterCenters?masterCenterGroupKey=ProductType  ประเภทของโครงการ  (แนวราบ, แนวสูง)</value>
    [DataMember(Name="productType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "productType")]
    public MasterCenterDropdownDTO ProductType { get; set; }

    /// <summary>
    /// Master/api/MasterCenters?masterCenterGroupKey=ProjectStatus  สถานะโครงการ
    /// </summary>
    /// <value>Master/api/MasterCenters?masterCenterGroupKey=ProjectStatus  สถานะโครงการ</value>
    [DataMember(Name="projectStatus", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "projectStatus")]
    public MasterCenterDropdownDTO ProjectStatus { get; set; }

    /// <summary>
    /// สถานะ Active
    /// </summary>
    /// <value>สถานะ Active</value>
    [DataMember(Name="isActive", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isActive")]
    public bool? IsActive { get; set; }

    /// <summary>
    /// SapCode
    /// </summary>
    /// <value>SapCode</value>
    [DataMember(Name="sapCode", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "sapCode")]
    public string SapCode { get; set; }

    /// <summary>
    /// Plant
    /// </summary>
    /// <value>Plant</value>
    [DataMember(Name="plant", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "plant")]
    public string Plant { get; set; }

    /// <summary>
    /// จำนวน Unit
    /// </summary>
    /// <value>จำนวน Unit</value>
    [DataMember(Name="unitCount", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "unitCount")]
    public ProjectUnitCountDTO UnitCount { get; set; }

    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// Gets or Sets UpdatedBy
    /// </summary>
    [DataMember(Name="updatedBy", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "updatedBy")]
    public string UpdatedBy { get; set; }

    /// <summary>
    /// Gets or Sets Updated
    /// </summary>
    [DataMember(Name="updated", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "updated")]
    public DateTime? Updated { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ProjectDTO {\n");
      sb.Append("  ProjectNo: ").Append(ProjectNo).Append("\n");
      sb.Append("  ProjectNameTH: ").Append(ProjectNameTH).Append("\n");
      sb.Append("  ProjectNameEN: ").Append(ProjectNameEN).Append("\n");
      sb.Append("  Brand: ").Append(Brand).Append("\n");
      sb.Append("  Company: ").Append(Company).Append("\n");
      sb.Append("  ProductType: ").Append(ProductType).Append("\n");
      sb.Append("  ProjectStatus: ").Append(ProjectStatus).Append("\n");
      sb.Append("  IsActive: ").Append(IsActive).Append("\n");
      sb.Append("  SapCode: ").Append(SapCode).Append("\n");
      sb.Append("  Plant: ").Append(Plant).Append("\n");
      sb.Append("  UnitCount: ").Append(UnitCount).Append("\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  UpdatedBy: ").Append(UpdatedBy).Append("\n");
      sb.Append("  Updated: ").Append(Updated).Append("\n");
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
