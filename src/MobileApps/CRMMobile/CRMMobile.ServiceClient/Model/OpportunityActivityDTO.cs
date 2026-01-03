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
  public class OpportunityActivityDTO {
    /// <summary>
    /// ID ของ Activity
    /// </summary>
    /// <value>ID ของ Activity</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// ID ของ Opportunity
    /// </summary>
    /// <value>ID ของ Opportunity</value>
    [DataMember(Name="opportunityID", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "opportunityID")]
    public Guid? OpportunityID { get; set; }

    /// <summary>
    /// ประเภท Activity
    /// </summary>
    /// <value>ประเภท Activity</value>
    [DataMember(Name="activityType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "activityType")]
    public MasterCenterDropdownDTO ActivityType { get; set; }

    /// <summary>
    /// Dropdown List ของ Activity type (Walk) ทั้งหมด
    /// </summary>
    /// <value>Dropdown List ของ Activity type (Walk) ทั้งหมด</value>
    [DataMember(Name="activityTypeDropdownItems", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "activityTypeDropdownItems")]
    public List<MasterCenterDropdownDTO> ActivityTypeDropdownItems { get; set; }

    /// <summary>
    /// วันที่ทำจริง
    /// </summary>
    /// <value>วันที่ทำจริง</value>
    [DataMember(Name="actualDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "actualDate")]
    public DateTime? ActualDate { get; set; }

    /// <summary>
    /// วันที่ต้องทำ
    /// </summary>
    /// <value>วันที่ต้องทำ</value>
    [DataMember(Name="dueDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "dueDate")]
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// เวลาที่สะดวกติดต่อกลับ  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ConvenientTime
    /// </summary>
    /// <value>เวลาที่สะดวกติดต่อกลับ  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ConvenientTime</value>
    [DataMember(Name="convenientTime", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "convenientTime")]
    public MasterCenterDropdownDTO ConvenientTime { get; set; }

    /// <summary>
    /// วันที่นัดหมาย
    /// </summary>
    /// <value>วันที่นัดหมาย</value>
    [DataMember(Name="appointmentDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "appointmentDate")]
    public DateTime? AppointmentDate { get; set; }

    /// <summary>
    /// รายละเอียด
    /// </summary>
    /// <value>รายละเอียด</value>
    [DataMember(Name="description", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    /// <summary>
    /// ผลการติดตาม
    /// </summary>
    /// <value>ผลการติดตาม</value>
    [DataMember(Name="activityStatuses", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "activityStatuses")]
    public List<OpportunityActivityStatusDTO> ActivityStatuses { get; set; }

    /// <summary>
    /// วันที่สร้าง
    /// </summary>
    /// <value>วันที่สร้าง</value>
    [DataMember(Name="createdDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// สร้างโดย
    /// </summary>
    /// <value>สร้างโดย</value>
    [DataMember(Name="createdBy", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "createdBy")]
    public string CreatedBy { get; set; }

    /// <summary>
    /// วันที่แก้ไข
    /// </summary>
    /// <value>วันที่แก้ไข</value>
    [DataMember(Name="updatedDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "updatedDate")]
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// แก้ไขโดย
    /// </summary>
    /// <value>แก้ไขโดย</value>
    [DataMember(Name="updatedBy", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "updatedBy")]
    public string UpdatedBy { get; set; }

    /// <summary>
    /// Activity สำเร็จแล้วหรือไม่
    /// </summary>
    /// <value>Activity สำเร็จแล้วหรือไม่</value>
    [DataMember(Name="isCompleted", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isCompleted")]
    public bool? IsCompleted { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class OpportunityActivityDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  OpportunityID: ").Append(OpportunityID).Append("\n");
      sb.Append("  ActivityType: ").Append(ActivityType).Append("\n");
      sb.Append("  ActivityTypeDropdownItems: ").Append(ActivityTypeDropdownItems).Append("\n");
      sb.Append("  ActualDate: ").Append(ActualDate).Append("\n");
      sb.Append("  DueDate: ").Append(DueDate).Append("\n");
      sb.Append("  ConvenientTime: ").Append(ConvenientTime).Append("\n");
      sb.Append("  AppointmentDate: ").Append(AppointmentDate).Append("\n");
      sb.Append("  Description: ").Append(Description).Append("\n");
      sb.Append("  ActivityStatuses: ").Append(ActivityStatuses).Append("\n");
      sb.Append("  CreatedDate: ").Append(CreatedDate).Append("\n");
      sb.Append("  CreatedBy: ").Append(CreatedBy).Append("\n");
      sb.Append("  UpdatedDate: ").Append(UpdatedDate).Append("\n");
      sb.Append("  UpdatedBy: ").Append(UpdatedBy).Append("\n");
      sb.Append("  IsCompleted: ").Append(IsCompleted).Append("\n");
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
