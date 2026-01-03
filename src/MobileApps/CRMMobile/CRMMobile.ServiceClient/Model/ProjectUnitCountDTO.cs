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
  public class ProjectUnitCountDTO {
    /// <summary>
    /// Gets or Sets Available
    /// </summary>
    [DataMember(Name="available", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "available")]
    public int? Available { get; set; }

    /// <summary>
    /// Gets or Sets Booking
    /// </summary>
    [DataMember(Name="booking", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "booking")]
    public int? Booking { get; set; }

    /// <summary>
    /// Gets or Sets Transfer
    /// </summary>
    [DataMember(Name="transfer", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "transfer")]
    public int? Transfer { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ProjectUnitCountDTO {\n");
      sb.Append("  Available: ").Append(Available).Append("\n");
      sb.Append("  Booking: ").Append(Booking).Append("\n");
      sb.Append("  Transfer: ").Append(Transfer).Append("\n");
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
