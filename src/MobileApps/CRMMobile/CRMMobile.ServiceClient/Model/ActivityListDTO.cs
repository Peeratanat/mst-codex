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
  public class ActivityListDTO {
    /// <summary>
    /// ID ของ Activity Task
    /// </summary>
    /// <value>ID ของ Activity Task</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// โครงการ
    /// </summary>
    /// <value>โครงการ</value>
    [DataMember(Name="project", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "project")]
    public ProjectDTO Project { get; set; }

    /// <summary>
    /// ชื่อลูกค้า
    /// </summary>
    /// <value>ชื่อลูกค้า</value>
    [DataMember(Name="contactFirstName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactFirstName")]
    public string ContactFirstName { get; set; }

    /// <summary>
    /// นามสกุลลูกค้า
    /// </summary>
    /// <value>นามสกุลลูกค้า</value>
    [DataMember(Name="contactLastName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contactLastName")]
    public string ContactLastName { get; set; }

    /// <summary>
    /// เบอร์โทรศัพท์
    /// </summary>
    /// <value>เบอร์โทรศัพท์</value>
    [DataMember(Name="phoneNumber", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "phoneNumber")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// วันที่ต้องทำ (Plan)
    /// </summary>
    /// <value>วันที่ต้องทำ (Plan)</value>
    [DataMember(Name="dueDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "dueDate")]
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// สถานะ Overdue  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskOverdueStatus
    /// </summary>
    /// <value>สถานะ Overdue  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskOverdueStatus</value>
    [DataMember(Name="overdueStatus", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "overdueStatus")]
    public MasterCenterDropdownDTO OverdueStatus { get; set; }

    /// <summary>
    /// จำนวนวันที่ Overdue (บวก = ยังไม่ overdue, ลบ = overdue แล้ว, 0 = ถึงกำหนดแล้ว)
    /// </summary>
    /// <value>จำนวนวันที่ Overdue (บวก = ยังไม่ overdue, ลบ = overdue แล้ว, 0 = ถึงกำหนดแล้ว)</value>
    [DataMember(Name="overdueDays", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "overdueDays")]
    public int? OverdueDays { get; set; }

    /// <summary>
    /// จำนวนครั้ง เช่น Revisit ครั้งที่ 2 = 2
    /// </summary>
    /// <value>จำนวนครั้ง เช่น Revisit ครั้งที่ 2 = 2</value>
    [DataMember(Name="repeatCount", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "repeatCount")]
    public int? RepeatCount { get; set; }

    /// <summary>
    /// สถานะของ Activity Task  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskStatus
    /// </summary>
    /// <value>สถานะของ Activity Task  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskStatus</value>
    [DataMember(Name="activityTaskStatus", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "activityTaskStatus")]
    public MasterCenterDropdownDTO ActivityTaskStatus { get; set; }

    /// <summary>
    /// หัวข้อของ Activity Task  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskTopic
    /// </summary>
    /// <value>หัวข้อของ Activity Task  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskTopic</value>
    [DataMember(Name="activityTaskTopic", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "activityTaskTopic")]
    public MasterCenterDropdownDTO ActivityTaskTopic { get; set; }

    /// <summary>
    /// ชนิดของ Activity Task  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskType
    /// </summary>
    /// <value>ชนิดของ Activity Task  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ActivityTaskType</value>
    [DataMember(Name="activityTaskType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "activityTaskType")]
    public MasterCenterDropdownDTO ActivityTaskType { get; set; }

    /// <summary>
    /// ชื่อของประเภท Activity ของแต่ละชนิด (Lead, Walk, Revisit)
    /// </summary>
    /// <value>ชื่อของประเภท Activity ของแต่ละชนิด (Lead, Walk, Revisit)</value>
    [DataMember(Name="activityTypeName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "activityTypeName")]
    public string ActivityTypeName { get; set; }

    /// <summary>
    /// ผู้ดูแล
    /// </summary>
    /// <value>ผู้ดูแล</value>
    [DataMember(Name="owner", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "owner")]
    public UserListDTO Owner { get; set; }

    /// <summary>
    /// Ref Lead Activity
    /// </summary>
    /// <value>Ref Lead Activity</value>
    [DataMember(Name="leadActivity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadActivity")]
    public LeadActivityListDTO LeadActivity { get; set; }

    /// <summary>
    /// Ref Opportunity Activity
    /// </summary>
    /// <value>Ref Opportunity Activity</value>
    [DataMember(Name="opportunityActivity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "opportunityActivity")]
    public OpportunityActivityListDTO OpportunityActivity { get; set; }

    /// <summary>
    /// Ref Revisit Activity
    /// </summary>
    /// <value>Ref Revisit Activity</value>
    [DataMember(Name="revisitActivity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "revisitActivity")]
    public RevisitActivityListDTO RevisitActivity { get; set; }

    /// <summary>
    /// ประเภท กรณี Lead (Call, Web, Facebook)_
    /// </summary>
    /// <value>ประเภท กรณี Lead (Call, Web, Facebook)_</value>
    [DataMember(Name="leadType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadType")]
    public MasterCenterDropdownDTO LeadType { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ActivityListDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Project: ").Append(Project).Append("\n");
      sb.Append("  ContactFirstName: ").Append(ContactFirstName).Append("\n");
      sb.Append("  ContactLastName: ").Append(ContactLastName).Append("\n");
      sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
      sb.Append("  DueDate: ").Append(DueDate).Append("\n");
      sb.Append("  OverdueStatus: ").Append(OverdueStatus).Append("\n");
      sb.Append("  OverdueDays: ").Append(OverdueDays).Append("\n");
      sb.Append("  RepeatCount: ").Append(RepeatCount).Append("\n");
      sb.Append("  ActivityTaskStatus: ").Append(ActivityTaskStatus).Append("\n");
      sb.Append("  ActivityTaskTopic: ").Append(ActivityTaskTopic).Append("\n");
      sb.Append("  ActivityTaskType: ").Append(ActivityTaskType).Append("\n");
      sb.Append("  ActivityTypeName: ").Append(ActivityTypeName).Append("\n");
      sb.Append("  Owner: ").Append(Owner).Append("\n");
      sb.Append("  LeadActivity: ").Append(LeadActivity).Append("\n");
      sb.Append("  OpportunityActivity: ").Append(OpportunityActivity).Append("\n");
      sb.Append("  RevisitActivity: ").Append(RevisitActivity).Append("\n");
      sb.Append("  LeadType: ").Append(LeadType).Append("\n");
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
