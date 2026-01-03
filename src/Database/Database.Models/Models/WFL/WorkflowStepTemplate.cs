using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.WFL
{
    [Description("Template ของขั้นตอนของการอนุมัติ")]
    [Table("WorkflowStepTemplate", Schema = Schema.WORKFLOW)]
    public class WorkflowStepTemplate : BaseEntity
    {
        [Description("เชื่อมต่อ Template การอนุมัติ")]
        public Guid WorkflowTemplateID { get; set; }
        [ForeignKey("WorkflowTemplateID")]
        public WorkflowTemplate WorkflowTemplate { get; set; }

        [Description("เงื่อนไขการอนุมัติในขั้นตอนนี้")]
        public WorkflowStepApproveCondition ApproveCondition { get; set; }

    }
}
