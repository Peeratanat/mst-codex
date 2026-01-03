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
  public class VisitorHistoryDTO {
    /// <summary>
    /// โครงการ
    /// </summary>
    /// <value>โครงการ</value>
    [DataMember(Name="project", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "project")]
    public ProjectDropdownDTO Project { get; set; }

    /// <summary>
    /// วันที่เข้าโครงการ
    /// </summary>
    /// <value>วันที่เข้าโครงการ</value>
    [DataMember(Name="visitDateIn", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitDateIn")]
    public DateTime? VisitDateIn { get; set; }

    /// <summary>
    /// วันที่ออกโครงการ
    /// </summary>
    /// <value>วันที่ออกโครงการ</value>
    [DataMember(Name="visitDateOut", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "visitDateOut")]
    public DateTime? VisitDateOut { get; set; }

    /// <summary>
    /// สถานะ Opportunity (โอกาสการขาย)  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=SalesOpportunity
    /// </summary>
    /// <value>สถานะ Opportunity (โอกาสการขาย)  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=SalesOpportunity</value>
    [DataMember(Name="salesOpportunity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "salesOpportunity")]
    public MasterCenterDropdownDTO SalesOpportunity { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class VisitorHistoryDTO {\n");
      sb.Append("  Project: ").Append(Project).Append("\n");
      sb.Append("  VisitDateIn: ").Append(VisitDateIn).Append("\n");
      sb.Append("  VisitDateOut: ").Append(VisitDateOut).Append("\n");
      sb.Append("  SalesOpportunity: ").Append(SalesOpportunity).Append("\n");
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
