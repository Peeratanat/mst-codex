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
  public class UnitDropdownDTO {
    /// <summary>
    /// Identity UnitID
    /// </summary>
    /// <value>Identity UnitID</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// เลขที่แปลง
    /// </summary>
    /// <value>เลขที่แปลง</value>
    [DataMember(Name="unitNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "unitNo")]
    public string UnitNo { get; set; }

    /// <summary>
    /// บ้านเลขที่
    /// </summary>
    /// <value>บ้านเลขที่</value>
    [DataMember(Name="houseNo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "houseNo")]
    public string HouseNo { get; set; }

    /// <summary>
    /// ตึก  Project/api/Projects/{projectID}/Towers/DropdownList
    /// </summary>
    /// <value>ตึก  Project/api/Projects/{projectID}/Towers/DropdownList</value>
    [DataMember(Name="tower", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "tower")]
    public TowerDropdownDTO Tower { get; set; }

    /// <summary>
    /// ชั้น  Project/api/Projects/{projectID}/Towers/{towerID}/Floors/DropdownList
    /// </summary>
    /// <value>ชั้น  Project/api/Projects/{projectID}/Towers/{towerID}/Floors/DropdownList</value>
    [DataMember(Name="floor", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "floor")]
    public FloorDropdownDTO Floor { get; set; }

    /// <summary>
    /// พื้นที่ใช้สอย
    /// </summary>
    /// <value>พื้นที่ใช้สอย</value>
    [DataMember(Name="usedArea", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "usedArea")]
    public double? UsedArea { get; set; }

    /// <summary>
    /// พื้นที่ขาย
    /// </summary>
    /// <value>พื้นที่ขาย</value>
    [DataMember(Name="saleArea", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "saleArea")]
    public double? SaleArea { get; set; }

    /// <summary>
    /// พื้นที่โฉนด
    /// </summary>
    /// <value>พื้นที่โฉนด</value>
    [DataMember(Name="titledeedArea", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "titledeedArea")]
    public double? TitledeedArea { get; set; }

    /// <summary>
    /// สถานะแปลง  Master/api/MasterCenters?masterCenterGroupKey=UnitStatus
    /// </summary>
    /// <value>สถานะแปลง  Master/api/MasterCenters?masterCenterGroupKey=UnitStatus</value>
    [DataMember(Name="unitStatus", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "unitStatus")]
    public MasterCenterDropdownDTO UnitStatus { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class UnitDropdownDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  UnitNo: ").Append(UnitNo).Append("\n");
      sb.Append("  HouseNo: ").Append(HouseNo).Append("\n");
      sb.Append("  Tower: ").Append(Tower).Append("\n");
      sb.Append("  Floor: ").Append(Floor).Append("\n");
      sb.Append("  UsedArea: ").Append(UsedArea).Append("\n");
      sb.Append("  SaleArea: ").Append(SaleArea).Append("\n");
      sb.Append("  TitledeedArea: ").Append(TitledeedArea).Append("\n");
      sb.Append("  UnitStatus: ").Append(UnitStatus).Append("\n");
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
