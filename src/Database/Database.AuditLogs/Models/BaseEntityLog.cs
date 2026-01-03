using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Database.AuditLogs
{
    public class BaseEntityLog
    {
        public BaseEntityLog()
        {
            if (this.ID == Guid.Empty)
            {
                this.ID = Guid.NewGuid();
            }
        }

        public Guid ID { get; set; }

        [Description("Key ของสิ่งที่ log")]
        public Guid KeyValue { get; set; }
        [Description("ค่าก่อนแก้ไข")]
        public string OldValues { get; set; }
        [Description("ค่าหลังแก้ไข")]
        public string NewValues { get; set; }

        [Description("วันที่สร้าง")]
        public DateTime? Created { get; set; }
        [Description("สร้างโดย")]
        [MaxLength(100)]
        public Guid? CreateBy { get; set; }
    }
}
