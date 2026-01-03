using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.NTF
{
    [Description("การแจ้งเตือนผ่าน Mobile Push Notification")]
    [Table("MobileNotification", Schema = Schema.NOTIFICATION)]
    public class MobileNotification : BaseEntity
    {
        [Description("ผู้รับ")]
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public USR.User User { get; set; }

        [Description("หัวข้อ")]
        [MaxLength(1000)]
        public string Subject { get; set; }
        [Description("ข้อความใน Notification")]
        [MaxLength(5000)]
        public string Message { get; set; }
        [Description("สิ่งที่ต้องทำหลังกด Notification")]
        [MaxLength(1000)]
        public string Action { get; set; }
        [Description("ข้อมูลเพิ่มเติมที่ต้องใช้หลังกด Notification")]
        [MaxLength(1000)]
        public string Params { get; set; }
        [Description("สถานะของการส่ง")]
        public SendStatus Status { get; set; }
        [Description("เลข InstallationID ที่รับการแจ้งเตือน")]
        [MaxLength(5000)]
        public string DeviceIds { get; set; }
        [Description("Error (ถ้ามี)")]
        [MaxLength(5000)]
        public string ErrorMessages { get; set; }
        [Description("ชื่อ Template ที่ใช้ส่ง")]
        [MaxLength(100)]
        public string TemplateName { get; set; }

    }
}
