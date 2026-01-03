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
  public class LeadScoreListDTO {
    /// <summary>
    /// ID ของ Score
    /// </summary>
    /// <value>ID ของ Score</value>
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
    /// ID ของ Lead scoring type (ชนิดของการให้คะแนน)
    /// </summary>
    /// <value>ID ของ Lead scoring type (ชนิดของการให้คะแนน)</value>
    [DataMember(Name="leadScoringTypeID", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "leadScoringTypeID")]
    public Guid? LeadScoringTypeID { get; set; }

    /// <summary>
    /// ลำดับ
    /// </summary>
    /// <value>ลำดับ</value>
    [DataMember(Name="order", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "order")]
    public int? Order { get; set; }

    /// <summary>
    /// หัวข้อของชนิดของการให้คะแนน
    /// </summary>
    /// <value>หัวข้อของชนิดของการให้คะแนน</value>
    [DataMember(Name="topic", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "topic")]
    public string Topic { get; set; }

    /// <summary>
    /// คะแนนที่จะได้
    /// </summary>
    /// <value>คะแนนที่จะได้</value>
    [DataMember(Name="score", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "score")]
    public double? Score { get; set; }

    /// <summary>
    /// ได้คะแนนนี้หรือไม่ (true = ได้คะแนนหัวข้อนี้/ false = ไม่ได้คะแนน)
    /// </summary>
    /// <value>ได้คะแนนนี้หรือไม่ (true = ได้คะแนนหัวข้อนี้/ false = ไม่ได้คะแนน)</value>
    [DataMember(Name="isGetScore", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isGetScore")]
    public bool? IsGetScore { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class LeadScoreListDTO {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  LeadID: ").Append(LeadID).Append("\n");
      sb.Append("  LeadScoringTypeID: ").Append(LeadScoringTypeID).Append("\n");
      sb.Append("  Order: ").Append(Order).Append("\n");
      sb.Append("  Topic: ").Append(Topic).Append("\n");
      sb.Append("  Score: ").Append(Score).Append("\n");
      sb.Append("  IsGetScore: ").Append(IsGetScore).Append("\n");
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
