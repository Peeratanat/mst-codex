using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.NTF
{
    [Description("การแจ้งเตือนผ่าน SMS")]
    [Table("SmsNotification", Schema = Schema.NOTIFICATION)]
    public class SmsNotification : BaseEntity
    {
        [Description("ผู้รับการแจ้งเตือน (comma seperated)")]
        [MaxLength(5000)]
        public string PhoneNumbers { get; set; }

        [Description("ข้อความบนการแจ้งเตือน")]
        [MaxLength(5000)]
        public string Message { get; set; }
        [Description("สถานะการส่ง หรือ อ่าน")]
        public SendStatus Status { get; set; }

        [Description("ชื่อ Template")]
        [MaxLength(100)]
        public string TemplateName { get; set; }

    }
}
