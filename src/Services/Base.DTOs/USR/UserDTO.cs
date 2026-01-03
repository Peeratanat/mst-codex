using Database.Models.USR;
using System;

namespace Base.DTOs.USR
{
    public class UserDTO
    {
        /// <summary>
        /// ID ของ User
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// รหัสพนักงาน
        /// </summary>
        public string EmployeeNo { get; set; }
        /// <summary>
        /// ชื่อจริง
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// ชื่อจริงEN
        /// </summary>
        public string FirstNameEng { get; set; }
        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// ชื่อกลาง
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// นามสกุล
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// รูปโปรไฟล์
        /// </summary>
        public string ProfilePicture { get; set; }
        /// <summary>
        /// อีเมลล์
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// เวลาที่ Login ล่าสุด
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// เวลาที่ใช้งานล่าสุด
        /// </summary>
        public DateTime? LastActivityTime { get; set; }
        /// <summary>
        /// เบอร์โทร
        /// </summary>
        public string PhoneNo { get; set; }
        /// <summary>
        /// ไลน์ไอดี
        /// </summary>
        public string LineId { get; set; }
        /// <summary>
        /// รายงานต่อใคร
        /// </summary>
        public Guid? ReportToUserID { get; set; }

        public static UserDTO CreateFromModel(User model)
        {
            if (model != null)
            {
                var result = new UserDTO()
                {
                    Id = model.ID,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    FirstNameEng = model.FirstNameEng,
                    DisplayName = model.DisplayName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    ProfilePicture = model.ProfilePicture,
                    Email = model.Email,
                    LastLoginTime = model.LastLoginTime,
                    LastActivityTime = model.LastActivityTime,
                    PhoneNo = model.PhoneNo,
                    LineId = model.LineId,
                    ReportToUserID = model.ReportToUserID
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
