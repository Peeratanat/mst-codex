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
  public class VisitorQuestionnaireHistoryDTO {
    /// <summary>
    /// โครงการ
    /// </summary>
    /// <value>โครงการ</value>
    [DataMember(Name="project", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "project")]
    public ProjectDropdownDTO Project { get; set; }

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
    /// วันที่ทำรายการ
    /// </summary>
    /// <value>วันที่ทำรายการ</value>
    [DataMember(Name="questionaireDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "questionaireDate")]
    public DateTime? QuestionaireDate { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class VisitorQuestionnaireHistoryDTO {\n");
      sb.Append("  Project: ").Append(Project).Append("\n");
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
