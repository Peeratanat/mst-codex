using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("เหตุผลในการยกเลิกจองหรือสัญญา")]
    [Table("CancelReason", Schema = Schema.MASTER)]
    public class CancelReason : BaseEntity
    {
        [Description("รหัส")]
        [MaxLength(100)]
        public string Key { get; set; }

        [Description("เหตุผล")]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Description("กลุ่มของเหตุผล")]
        public Guid? GroupOfCancelReasonMasterCenterID { get; set; }
        [ForeignKey("GroupOfCancelReasonMasterCenterID")]
        public MST.MasterCenter GroupOfCancelReason { get; set; }

        [Description("รูปแบบการอนุมัติ")]
        public Guid? CancelApproveFlowMasterCenterID { get; set; }
        [ForeignKey("CancelApproveFlowMasterCenterID")]
        public MST.MasterCenter CancelApproveFlow { get; set; }

    }
}
