using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.NTF
{
    [Description("Template ของการส่งการแจ้งเตืแน")]
    [Table("NotificationTemplate", Schema = Schema.NOTIFICATION)]
    public class NotificationTemplate : BaseEntityWithoutKey
    {
        [Key]
        [Description("ชื่อ Template")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Description("ข้อความบนเว็บ")]
        public string WebMessage { get; set; }
        [Description("หลังจากกด Notification ให้ทำอะไร")]
        [MaxLength(1000)]
        public string WebAction { get; set; }
        [Description("ข้อมูลเพิ่มตามหลังจากกด Notification")]
        [MaxLength(1000)]
        public string WebParams { get; set; }
        [Description("หัวข้อ Email")]
        [MaxLength(1000)]
        public string EmailSubject { get; set; }
        [Description("ข้อความใน Email")]
        public string EmailMessage { get; set; }
        [Description("หัวข้อบน Mobile")]
        [MaxLength(1000)]
        public string MobileSubject { get; set; }
        [Description("ข้อความใน Mobile")]
        [MaxLength(5000)]
        public string MobileMessage { get; set; }
        [Description("หลังจากกด Notification ให้ทำอะไร")]
        [MaxLength(1000)]
        public string MobileAction { get; set; }
        [Description("ข้อมูลเพิ่มตามหลังจากกด Notification")]
        [MaxLength(1000)]
        public string MobileParams { get; set; }
        [Description("ข้อความใน SMS")]
        public string SmsMessage { get; set; }
        [Description("เปิดการแจ้งเตือนผ่าน Web")]
        public bool IsWebOpen { get; set; }
        [Description("เปิดการแจ้งเตือนผ่าน Email")]
        public bool IsEmailOpen { get; set; }
        [Description("เปิดการแจ้งเตือนผ่าน Mobile")]
        public bool IsMobileOpen { get; set; }
        [Description("เปิดการแจ้งเตือนผ่าน SMS")]
        public bool IsSmsOpen { get; set; }

    }
}
