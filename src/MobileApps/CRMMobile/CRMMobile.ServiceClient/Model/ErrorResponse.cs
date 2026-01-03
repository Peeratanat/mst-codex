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
  public class ErrorResponse {
    /// <summary>
    /// Gets or Sets PopupErrors
    /// </summary>
    [DataMember(Name="popupErrors", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "popupErrors")]
    public List<ErrorItem> PopupErrors { get; set; }

    /// <summary>
    /// Gets or Sets FieldErrors
    /// </summary>
    [DataMember(Name="fieldErrors", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "fieldErrors")]
    public List<ErrorItem> FieldErrors { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ErrorResponse {\n");
      sb.Append("  PopupErrors: ").Append(PopupErrors).Append("\n");
      sb.Append("  FieldErrors: ").Append(FieldErrors).Append("\n");
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
