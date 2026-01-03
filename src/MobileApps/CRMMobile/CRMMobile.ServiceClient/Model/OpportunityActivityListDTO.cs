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
  public class OpportunityActivityListDTO {
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
    /// ประเภทของ Activity  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=OpportunityActivityType
    /// </summary>
    /// <value>ประเภทของ Activity  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=OpportunityActivityType</value>
    [DataMember(Name="opportunityActivityType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "opportunityActivityType")]
    public MasterCenterDropdownDTO OpportunityActivityType { get; set; }

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
    /// วันที่นัดหมาย
    /// </summary>
    /// <value>วันที่นัดหมาย</value>
    [DataMember(Name="appointmentDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "appointmentDate")]
    public DateTime? AppointmentDate { get; set; }

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
      sb.Append("class OpportunityActivityListDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  OpportunityID: ").Append(OpportunityID).Append("\n");
      sb.Append("  OpportunityActivityType: ").Append(OpportunityActivityType).Append("\n");
      sb.Append("  ActualDate: ").Append(ActualDate).Append("\n");
      sb.Append("  DueDate: ").Append(DueDate).Append("\n");
      sb.Append("  AppointmentDate: ").Append(AppointmentDate).Append("\n");
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
