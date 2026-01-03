using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.NTF
{
    [Description("การแจ้งเตือนผ่านเว็บ")]
    [Table("WebNotification", Schema = Schema.NOTIFICATION)]
    public class WebNotification : BaseEntity
    {
        [Description("ผู้รับการแจ้งเตือน")]
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public USR.User User { get; set; }

        [Description("ข้อความบนการแจ้งเตือน")]
        public string Message { get; set; }
        [Description("หลังจากกด Notification ให้ทำอะไร")]
        [MaxLength(1000)]
        public string Action { get; set; }
        [Description("ข้อมูลเพิ่มตามหลังจากกด Notification")]
        [MaxLength(1000)]
        public string Params { get; set; }
        [Description("สถานะการส่ง หรือ อ่าน")]
        public SendStatus Status { get; set; }

        [Description("ชื่อ Template")]
        [MaxLength(100)]
        public string TemplateName { get; set; }

    }
}
