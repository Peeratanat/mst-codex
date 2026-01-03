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
  public class VisitorPurchaseHistoryDTO {
    /// <summary>
    /// วันที่
    /// </summary>
    /// <value>วันที่</value>
    [DataMember(Name="purchaseDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "purchaseDate")]
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// โครงการ
    /// </summary>
    /// <value>โครงการ</value>
    [DataMember(Name="project", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "project")]
    public ProjectDropdownDTO Project { get; set; }

    /// <summary>
    /// แปลง
    /// </summary>
    /// <value>แปลง</value>
    [DataMember(Name="unit", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "unit")]
    public UnitDropdownDTO Unit { get; set; }

    /// <summary>
    /// มูลค่า (ราคาขายสุทธิ)
    /// </summary>
    /// <value>มูลค่า (ราคาขายสุทธิ)</value>
    [DataMember(Name="netSellingPrice", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "netSellingPrice")]
    public double? NetSellingPrice { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class VisitorPurchaseHistoryDTO {\n");
      sb.Append("  PurchaseDate: ").Append(PurchaseDate).Append("\n");
      sb.Append("  Project: ").Append(Project).Append("\n");
      sb.Append("  Unit: ").Append(Unit).Append("\n");
      sb.Append("  NetSellingPrice: ").Append(NetSellingPrice).Append("\n");
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
