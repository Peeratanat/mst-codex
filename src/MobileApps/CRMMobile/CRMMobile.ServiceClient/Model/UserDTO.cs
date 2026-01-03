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
    public class UserDTO
    {
        /// <summary>
        /// ID ของ User
        /// </summary>
        /// <value>ID ของ User</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// รหัสพนักงาน
        /// </summary>
        /// <value>รหัสพนักงาน</value>
        [DataMember(Name = "employeeNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "employeeNo")]
        public string EmployeeNo { get; set; }

        /// <summary>
        /// ชื่อจริง
        /// </summary>
        /// <value>ชื่อจริง</value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// ชื่อกลาง
        /// </summary>
        /// <value>ชื่อกลาง</value>
        [DataMember(Name = "middleName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "middleName")]
        public string MiddleName { get; set; }

        /// <summary>
        /// นามสกุล
        /// </summary>
        /// <value>นามสกุล</value>
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// รูปโปรไฟล์
        /// </summary>
        /// <value>รูปโปรไฟล์</value>
        [DataMember(Name = "profilePicture", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "profilePicture")]
        public string ProfilePicture { get; set; }

        /// <summary>
        /// อีเมลล์
        /// </summary>
        /// <value>อีเมลล์</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// เวลาที่ Login ล่าสุด
        /// </summary>
        /// <value>เวลาที่ Login ล่าสุด</value>
        [DataMember(Name = "lastLoginTime", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "lastLoginTime")]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// เวลาที่ใช้งานล่าสุด
        /// </summary>
        /// <value>เวลาที่ใช้งานล่าสุด</value>
        [DataMember(Name = "lastActivityTime", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "lastActivityTime")]
        public DateTime? LastActivityTime { get; set; }

        /// <summary>
        /// เบอร์โทร
        /// </summary>
        /// <value>เบอร์โทร</value>
        [DataMember(Name = "phoneNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "phoneNo")]
        public string PhoneNo { get; set; }

        /// <summary>
        /// ไลน์ไอดี
        /// </summary>
        /// <value>ไลน์ไอดี</value>
        [DataMember(Name = "lineId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "lineId")]
        public string LineId { get; set; }

        /// <summary>
        /// รายงานต่อใคร
        /// </summary>
        /// <value>รายงานต่อใคร</value>
        [DataMember(Name = "reportToUserID", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "reportToUserID")]
        public Guid? ReportToUserID { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UserDTO {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  EmployeeNo: ").Append(EmployeeNo).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  MiddleName: ").Append(MiddleName).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
            sb.Append("  ProfilePicture: ").Append(ProfilePicture).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  LastLoginTime: ").Append(LastLoginTime).Append("\n");
            sb.Append("  LastActivityTime: ").Append(LastActivityTime).Append("\n");
            sb.Append("  PhoneNo: ").Append(PhoneNo).Append("\n");
            sb.Append("  LineId: ").Append(LineId).Append("\n");
            sb.Append("  ReportToUserID: ").Append(ReportToUserID).Append("\n");
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
