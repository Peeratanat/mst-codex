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
  public class LeadActivityStatusDTO {
    /// <summary>
    /// รหัสของ Activity Status
    /// </summary>
    /// <value>รหัสของ Activity Status</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// รายละเอียดของ Status เช่น ลูกค้าไม่รับสาย
    /// </summary>
    /// <value>รายละเอียดของ Status เช่น ลูกค้าไม่รับสาย</value>
    [DataMember(Name="statusDescription", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "statusDescription")]
    public string StatusDescription { get; set; }

    /// <summary>
    /// ประเภทของ Activity Status  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadActivityStatusType
    /// </summary>
    /// <value>ประเภทของ Activity Status  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadActivityStatusType</value>
    [DataMember(Name="leadActivityStatusType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadActivityStatusType")]
    public MasterCenterDropdownDTO LeadActivityStatusType { get; set; }

    /// <summary>
    /// FollowUp หรือ Disqualify  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadActivityFollowUpType
    /// </summary>
    /// <value>FollowUp หรือ Disqualify  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadActivityFollowUpType</value>
    [DataMember(Name="leadActivityFollowUpType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadActivityFollowUpType")]
    public MasterCenterDropdownDTO LeadActivityFollowUpType { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class LeadActivityStatusDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  StatusDescription: ").Append(StatusDescription).Append("\n");
      sb.Append("  LeadActivityStatusType: ").Append(LeadActivityStatusType).Append("\n");
      sb.Append("  LeadActivityFollowUpType: ").Append(LeadActivityFollowUpType).Append("\n");
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
