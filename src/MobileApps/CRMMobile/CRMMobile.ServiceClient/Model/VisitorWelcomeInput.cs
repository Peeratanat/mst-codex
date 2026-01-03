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
  public class VisitorWelcomeInput {
    /// <summary>
    /// Gets or Sets IsWelcome
    /// </summary>
    [DataMember(Name="isWelcome", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "isWelcome")]
    public bool? IsWelcome { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class VisitorWelcomeInput {\n");
      sb.Append("  IsWelcome: ").Append(IsWelcome).Append("\n");
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
