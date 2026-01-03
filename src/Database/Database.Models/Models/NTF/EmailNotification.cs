using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.NTF
{
    [Description("การแจ้งเตือนผ่าน Email")]
    [Table("EmailNotification", Schema = Schema.NOTIFICATION)]
    public class EmailNotification : BaseEntity
    {
        [Description("หัวข้อ Email")]
        [MaxLength(1000)]
        public string Subject { get; set; }
        [Description("ข้อความใน Email")]
        public string Message { get; set; }
        [Description("ผู้รับ (comma seperated)")]
        [MaxLength(5000)]
        public string Receivers { get; set; }
        [Description("ผู้รับ cc (comma seperated)")]
        [MaxLength(5000)]
        public string CCReceivers { get; set; }
        [Description("ผู้รับ bcc (comma seperated)")]
        [MaxLength(5000)]
        public string BCCReceivers { get; set; }
        [Description("สถานะการส่ง")]
        public SendStatus Status { get; set; }
        [Description("รอบของการส่งอีกครั้ง (หากมีข้อผิดพลาด)")]
        public int Retry { get; set; }
        [Description("Error (ถ้ามี)")]
        [MaxLength(5000)]
        public string ErrorMessage { get; set; }
        [Description("ชื่อของ Template")]
        [MaxLength(100)]
        public string TemplateName { get; set; }

    }
}
