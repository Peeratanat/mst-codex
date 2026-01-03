using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.WFL
{
    [Description("Template การอนุมัติเรื่องต่างๆ")]
    [Table("WorkflowTemplate", Schema = Schema.WORKFLOW)]
    public class WorkflowTemplate : BaseEntity
    {
        [Description("ชนิดของการอนุมัติ")]
        public Guid WorkflowTypeID { get; set; }
        [ForeignKey("WorkflowTypeID")]
        public WorkflowType WorkflowType { get; set; }

        [Description("ชื่อ Template")]
        public string Name { get; set; }

    }
}
