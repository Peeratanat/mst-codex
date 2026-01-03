using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("ข้อผิดพลาดในระบบ")]
    [Table("ErrorMessage", Schema = Schema.MASTER)]
    public class ErrorMessage : BaseEntityWithoutKey
    {
        [Key]
        [Description("รหัส")]
        [MaxLength(50)]
        public string Key { get; set; }
        [Description("ข้อความ Error")]
        [MaxLength(1000)]
        public string Message { get; set; }
        [Description("ชนิดของ Error")]
        public ErrorMessageType? Type { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }
    }
}
