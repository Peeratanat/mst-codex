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
  public class OpportunityListDTO {
    /// <summary>
    /// ID ของ Opportunity
    /// </summary>
    /// <value>ID ของ Opportunity</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// ข้อมูล Contact
    /// </summary>
    /// <value>ข้อมูล Contact</value>
    [DataMember(Name="contact", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contact")]
    public ContactListDTO Contact { get; set; }

    /// <summary>
    /// โครงการ  project/api/Projects/DropdownList
    /// </summary>
    /// <value>โครงการ  project/api/Projects/DropdownList</value>
    [DataMember(Name="project", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "project")]
    public ProjectDTO Project { get; set; }

    /// <summary>
    /// วันที่เข้าโครงการ
    /// </summary>
    /// <value>วันที่เข้าโครงการ</value>
    [DataMember(Name="arriveDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "arriveDate")]
    public DateTime? ArriveDate { get; set; }

    /// <summary>
    /// วันที่แก้ไข
    /// </summary>
    /// <value>วันที่แก้ไข</value>
    [DataMember(Name="updatedDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "updatedDate")]
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// สถานะของ Opportunity  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=SalesOpportunity
    /// </summary>
    /// <value>สถานะของ Opportunity  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=SalesOpportunity</value>
    [DataMember(Name="salesOpportunity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "salesOpportunity")]
    public MasterCenterDropdownDTO SalesOpportunity { get; set; }

    /// <summary>
    /// LC Owner
    /// </summary>
    /// <value>LC Owner</value>
    [DataMember(Name="owner", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "owner")]
    public UserListDTO Owner { get; set; }

    /// <summary>
    /// Last Activity  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=OpportunityActivityType
    /// </summary>
    /// <value>Last Activity  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=OpportunityActivityType</value>
    [DataMember(Name="lastActivity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastActivity")]
    public MasterCenterDropdownDTO LastActivity { get; set; }

    /// <summary>
    /// จำนวน Revisit
    /// </summary>
    /// <value>จำนวน Revisit</value>
    [DataMember(Name="revisitCount", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "revisitCount")]
    public int? RevisitCount { get; set; }

    /// <summary>
    /// สถานะทำแบบสอบถาม  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=StatusQuestionaire
    /// </summary>
    /// <value>สถานะทำแบบสอบถาม  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=StatusQuestionaire</value>
    [DataMember(Name="statusQuestionaire", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "statusQuestionaire")]
    public MasterCenterDropdownDTO StatusQuestionaire { get; set; }

    /// <summary>
    /// จำนวนคำถามที่ตอบคำถามแล้ว
    /// </summary>
    /// <value>จำนวนคำถามที่ตอบคำถามแล้ว</value>
    [DataMember(Name="answeredQuestionaire", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "answeredQuestionaire")]
    public int? AnsweredQuestionaire { get; set; }

    /// <summary>
    /// จำนวนคำถามทั้งหมด
    /// </summary>
    /// <value>จำนวนคำถามทั้งหมด</value>
    [DataMember(Name="totalQuestionaire", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "totalQuestionaire")]
    public int? TotalQuestionaire { get; set; }

    /// <summary>
    /// วันที่ตอบแบบสอบถาม
    /// </summary>
    /// <value>วันที่ตอบแบบสอบถาม</value>
    [DataMember(Name="questionaireDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "questionaireDate")]
    public DateTime? QuestionaireDate { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class OpportunityListDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Contact: ").Append(Contact).Append("\n");
      sb.Append("  Project: ").Append(Project).Append("\n");
      sb.Append("  ArriveDate: ").Append(ArriveDate).Append("\n");
      sb.Append("  UpdatedDate: ").Append(UpdatedDate).Append("\n");
      sb.Append("  SalesOpportunity: ").Append(SalesOpportunity).Append("\n");
      sb.Append("  Owner: ").Append(Owner).Append("\n");
      sb.Append("  LastActivity: ").Append(LastActivity).Append("\n");
      sb.Append("  RevisitCount: ").Append(RevisitCount).Append("\n");
      sb.Append("  StatusQuestionaire: ").Append(StatusQuestionaire).Append("\n");
      sb.Append("  AnsweredQuestionaire: ").Append(AnsweredQuestionaire).Append("\n");
      sb.Append("  TotalQuestionaire: ").Append(TotalQuestionaire).Append("\n");
      sb.Append("  QuestionaireDate: ").Append(QuestionaireDate).Append("\n");
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
