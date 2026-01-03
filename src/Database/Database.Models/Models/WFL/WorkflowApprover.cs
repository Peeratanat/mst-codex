using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.WFL
{
    [Description("ผู้อนุมัติ")]
    [Table("WorkflowApprover", Schema = Schema.WORKFLOW)]
    public class WorkflowApprover : BaseEntity
    {
        [Description("เชื่อมข้อมูลการอนุมัติ")]
        public Guid WorkflowStepID { get; set; }
        [ForeignKey("WorkflowStepID")]
        public WorkflowStep WorkflowStep { get; set; }

        [Description("ชนิดของผู้อนุมัติ (Role หรือ User)")]
        public WorkflowApproverType Type { get; set; }

        [Description("Role ของผู้อนุมัติ")]
        public Guid? RoleID { get; set; }
        [ForeignKey("RoleID")]
        public USR.Role Role { get; set; }

        [Description("ผู้อนุมัติ")]
        public Guid? ApproverID { get; set; }
        [ForeignKey("ApproverID")]
        public USR.User Approver { get; set; }

        [Description("ผลอนุมัติ")]
        public bool? Result { get; set; }

        [Description("บันทึกข้อความ")]
        public string Memo { get; set; }


    }
}
