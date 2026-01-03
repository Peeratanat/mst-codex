using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.WFL
{
    [Description("การอนุมัติเรื่องต่างๆ")]
    [Table("Workflow", Schema = Schema.WORKFLOW)]
    public class Workflow : BaseEntity
    {
        [Description("ชนิดของการอนุมัติ")]
        public Guid WorkflowTypeID { get; set; }
        [ForeignKey("WorkflowTypeID")]
        public WorkflowType WorkflowType { get; set; }

        [Description("ชื่อ Template ต้นแบบที่ Clone มา")]
        public string TemplateName { get; set; }
        [Description("ผลการอนุมัติ")]
        public bool? Result { get; set; }
        [Description("บันทึกข้อความ")]
        public string Memo { get; set; }

        [Description("ตัวชี้ที่ 1")]
        public string Ref1 { get; set; }
        [Description("ตัวชี้ที่ 2")]
        public string Ref2 { get; set; }
        [Description("ตัวชี้ที่ 3")]
        public string Ref3 { get; set; }
        [Description("ตัวชี้ที่ 4")]
        public string Ref4 { get; set; }


    }
}
