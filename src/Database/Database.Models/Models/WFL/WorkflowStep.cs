using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.WFL
{
    [Description("ขั้นตอนของการอนุมัติ")]
    [Table("WorkflowStep", Schema = Schema.WORKFLOW)]
    public class WorkflowStep : BaseEntity
    {
        [Description("เชื่อมต่อการอนุมัติ")]
        public Guid WorkflowID { get; set; }
        [ForeignKey("WorkflowID")]
        public Workflow Workflow { get; set; }

        [Description("เงื่อนไขการอนุมัติในขั้นตอนนี้")]
        public WorkflowStepApproveCondition ApproveCondition { get; set; }

        [Description("ผลการอนุมัติ")]
        public bool? Result { get; set; }

    }
}
