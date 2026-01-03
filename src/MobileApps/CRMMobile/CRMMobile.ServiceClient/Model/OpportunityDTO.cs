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
  public class OpportunityDTO {
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
    /// ประเมินโอกาสการขาย  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=EstimateSalesOpportunity
    /// </summary>
    /// <value>ประเมินโอกาสการขาย  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=EstimateSalesOpportunity</value>
    [DataMember(Name="estimateSalesOpportunity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "estimateSalesOpportunity")]
    public MasterCenterDropdownDTO EstimateSalesOpportunity { get; set; }

    /// <summary>
    /// โอกาสการขาย  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=SalesOpportunity
    /// </summary>
    /// <value>โอกาสการขาย  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=SalesOpportunity</value>
    [DataMember(Name="salesOpportunity", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "salesOpportunity")]
    public MasterCenterDropdownDTO SalesOpportunity { get; set; }

    /// <summary>
    /// สินค้าที่สนใจ 1
    /// </summary>
    /// <value>สินค้าที่สนใจ 1</value>
    [DataMember(Name="interestedProduct1", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "interestedProduct1")]
    public string InterestedProduct1 { get; set; }

    /// <summary>
    /// สินค้าที่สนใจ 2
    /// </summary>
    /// <value>สินค้าที่สนใจ 2</value>
    [DataMember(Name="interestedProduct2", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "interestedProduct2")]
    public string InterestedProduct2 { get; set; }

    /// <summary>
    /// สินค้าที่สนใจ 3
    /// </summary>
    /// <value>สินค้าที่สนใจ 3</value>
    [DataMember(Name="interestedProduct3", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "interestedProduct3")]
    public string InterestedProduct3 { get; set; }

    /// <summary>
    /// สถานะทำแบบสอบถาม  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=StatusQuestionaire
    /// </summary>
    /// <value>สถานะทำแบบสอบถาม  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=StatusQuestionaire</value>
    [DataMember(Name="statusQuestionaire", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "statusQuestionaire")]
    public MasterCenterDropdownDTO StatusQuestionaire { get; set; }

    /// <summary>
    /// จำนวนแปลงที่สนใจ
    /// </summary>
    /// <value>จำนวนแปลงที่สนใจ</value>
    [DataMember(Name="productQTY", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "productQTY")]
    public int? ProductQTY { get; set; }

    /// <summary>
    /// โครงการเปรียบเทียบ
    /// </summary>
    /// <value>โครงการเปรียบเทียบ</value>
    [DataMember(Name="projectCompare", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "projectCompare")]
    public string ProjectCompare { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    /// <value>Remark</value>
    [DataMember(Name="remark", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "remark")]
    public string Remark { get; set; }

    /// <summary>
    /// เหตุผลที่ซื้อ
    /// </summary>
    /// <value>เหตุผลที่ซื้อ</value>
    [DataMember(Name="buyReason", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "buyReason")]
    public string BuyReason { get; set; }

    /// <summary>
    /// วันที่เยี่ยมชม
    /// </summary>
    /// <value>วันที่เยี่ยมชม</value>
    [DataMember(Name="arriveDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "arriveDate")]
    public DateTime? ArriveDate { get; set; }

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
    /// ผู้ดูแล
    /// </summary>
    /// <value>ผู้ดูแล</value>
    [DataMember(Name="owner", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "owner")]
    public UserListDTO Owner { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class OpportunityDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Contact: ").Append(Contact).Append("\n");
      sb.Append("  Project: ").Append(Project).Append("\n");
      sb.Append("  EstimateSalesOpportunity: ").Append(EstimateSalesOpportunity).Append("\n");
      sb.Append("  SalesOpportunity: ").Append(SalesOpportunity).Append("\n");
      sb.Append("  InterestedProduct1: ").Append(InterestedProduct1).Append("\n");
      sb.Append("  InterestedProduct2: ").Append(InterestedProduct2).Append("\n");
      sb.Append("  InterestedProduct3: ").Append(InterestedProduct3).Append("\n");
      sb.Append("  StatusQuestionaire: ").Append(StatusQuestionaire).Append("\n");
      sb.Append("  ProductQTY: ").Append(ProductQTY).Append("\n");
      sb.Append("  ProjectCompare: ").Append(ProjectCompare).Append("\n");
      sb.Append("  Remark: ").Append(Remark).Append("\n");
      sb.Append("  BuyReason: ").Append(BuyReason).Append("\n");
      sb.Append("  ArriveDate: ").Append(ArriveDate).Append("\n");
      sb.Append("  AnsweredQuestionaire: ").Append(AnsweredQuestionaire).Append("\n");
      sb.Append("  TotalQuestionaire: ").Append(TotalQuestionaire).Append("\n");
      sb.Append("  Owner: ").Append(Owner).Append("\n");
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
