using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Model
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class CancelReasonDTO
    {
        /// <summary>
        /// รหัส
        /// </summary>
        /// <value>รหัส</value>
        [DataMember(Name = "key", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// เหตุผล
        /// </summary>
        /// <value>เหตุผล</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// กลุ่มของเหตุผล  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=GroupOfCancelReason
        /// </summary>
        /// <value>กลุ่มของเหตุผล  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=GroupOfCancelReason</value>
        [DataMember(Name = "groupOfCancelReason", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "groupOfCancelReason")]
        public MasterCenterDropdownDTO GroupOfCancelReason { get; set; }

        /// <summary>
        /// รูปแบบการอนุมัติ  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CancelApproveFlow
        /// </summary>
        /// <value>รูปแบบการอนุมัติ  masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CancelApproveFlow</value>
        [DataMember(Name = "cancelApproveFlow", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cancelApproveFlow")]
        public MasterCenterDropdownDTO CancelApproveFlow { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy
        /// </summary>
        [DataMember(Name = "updatedBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "updatedBy")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Updated
        /// </summary>
        [DataMember(Name = "updated", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "updated")]
        public DateTime? Updated { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CancelReasonDTO {\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  GroupOfCancelReason: ").Append(GroupOfCancelReason).Append("\n");
            sb.Append("  CancelApproveFlow: ").Append(CancelApproveFlow).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  UpdatedBy: ").Append(UpdatedBy).Append("\n");
            sb.Append("  Updated: ").Append(Updated).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
