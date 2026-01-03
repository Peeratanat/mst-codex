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
  public class LeadActivityListDTO {
    /// <summary>
    /// ID ของ Lead Activity
    /// </summary>
    /// <value>ID ของ Lead Activity</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// ID ของ Lead
    /// </summary>
    /// <value>ID ของ Lead</value>
    [DataMember(Name="leadID", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadID")]
    public Guid? LeadID { get; set; }

    /// <summary>
    /// ประเภทของ Activity เช่น โทรติดตามลูกค้าครั้งที่ 3  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadActivityType
    /// </summary>
    /// <value>ประเภทของ Activity เช่น โทรติดตามลูกค้าครั้งที่ 3  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadActivityType</value>
    [DataMember(Name="activityType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "activityType")]
    public MasterCenterDropdownDTO ActivityType { get; set; }

    /// <summary>
    /// วันที่ต้องทำ
    /// </summary>
    /// <value>วันที่ต้องทำ</value>
    [DataMember(Name="dueDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "dueDate")]
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// วันที่ทำจริง
    /// </summary>
    /// <value>วันที่ทำจริง</value>
    [DataMember(Name="actualDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "actualDate")]
    public DateTime? ActualDate { get; set; }

    /// <summary>
    /// วันที่สร้าง
    /// </summary>
    /// <value>วันที่สร้าง</value>
    [DataMember(Name="createdDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// วันที่แก้ไข
    /// </summary>
    /// <value>วันที่แก้ไข</value>
    [DataMember(Name="updatedDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "updatedDate")]
    public DateTime? UpdatedDate { get; set; }

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
      sb.Append("class LeadActivityListDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  LeadID: ").Append(LeadID).Append("\n");
      sb.Append("  ActivityType: ").Append(ActivityType).Append("\n");
      sb.Append("  DueDate: ").Append(DueDate).Append("\n");
      sb.Append("  ActualDate: ").Append(ActualDate).Append("\n");
      sb.Append("  CreatedDate: ").Append(CreatedDate).Append("\n");
      sb.Append("  UpdatedDate: ").Append(UpdatedDate).Append("\n");
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
