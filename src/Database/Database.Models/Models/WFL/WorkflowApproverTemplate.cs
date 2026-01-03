using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.WFL
{
    [Description("Template ของผู้อนุมัติ")]
    [Table("WorkflowApproverTemplate", Schema = Schema.WORKFLOW)]
    public class WorkflowApproverTemplate : BaseEntity
    {
        [Description("เชื่อมข้อมูล Template การอนุมัติ")]
        public Guid WorkflowStepTemplateID { get; set; }
        [ForeignKey("WorkflowStepTemplateID")]
        public WorkflowStepTemplate WorkflowStepTemplate { get; set; }

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

    }
}
