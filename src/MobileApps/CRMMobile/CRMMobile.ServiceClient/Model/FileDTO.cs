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
  public class FileDTO {
    /// <summary>
    /// Url ของไฟล์
    /// </summary>
    /// <value>Url ของไฟล์</value>
    [DataMember(Name="url", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    /// <summary>
    /// ชื่อไฟล์ (ที่เก็บบน DB)
    /// </summary>
    /// <value>ชื่อไฟล์ (ที่เก็บบน DB)</value>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// ระบุว่าไฟล์อยู่ใน temp bucket
    /// </summary>
    /// <value>ระบุว่าไฟล์อยู่ใน temp bucket</value>
    [DataMember(Name="isTemp", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isTemp")]
    public bool? IsTemp { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class FileDTO {\n");
      sb.Append("  Url: ").Append(Url).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  IsTemp: ").Append(IsTemp).Append("\n");
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
