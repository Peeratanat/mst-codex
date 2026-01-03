using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("รายการผู้อนุมัติ Min Price หรือ Budget Promotion")]
    [Table("MinPriceBudgetApproval", Schema = Schema.SALE)]
    public class MinPriceBudgetApproval : BaseEntity
    {
        [Description]
        public Guid MinPriceBudgetWorkflowID { get; set; }
        [ForeignKey("MinPriceBudgetWorkflowID")]
        public MinPriceBudgetWorkflow MinPriceBudgetWorkflow { get; set; }
        [Description("ลำดับที่")]
        public int Order { get; set; }
        [Description("ตำแหน่งที่ต้องอนุมัติ")]
        public Guid? RoleID { get; set; }
        [ForeignKey("RoleID")]
        public USR.Role Role { get; set; }
        [ForeignKey("ผู้อนุมัติ")]
        public Guid? UserID { get; set; }
        [ForeignKey("UserID")]
        public USR.User User { get; set; }
        [Description("ผลอนุมัติ")]
        public bool? IsApproved { get; set; }
        [Description("เวลาที่อนุมัติ")]
        public DateTime? ApprovedTime { get; set; }
    }
}
