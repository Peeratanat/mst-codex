using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("รายการ Text File ที่ PostGL ไปแล้ว")]
    [Table("GLExport", Schema = Schema.ACCOUNT)]
    public class GLExport : BaseEntity
    {
        [Description("ชื่อไฟล์")]
        public string FileName { get; set; }
        [Description("รหัสโพส")]
        public string BatchID { get; set; }
        [Description("วันที่ Export")]
        public DateTime? ExportDate { get; set; }
        [Description("คนที่ Export")]
        public string ExportBy { get; set; }

    }
}
