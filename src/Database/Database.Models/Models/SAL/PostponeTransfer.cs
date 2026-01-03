using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ใบบันทึกเลื่อนโอน")]
    [Table("PostponeTransfer", Schema = Schema.SALE)]
    public class PostponeTransfer : BaseEntity
    {
        [Description("สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("วันที่นัดโอนตามใบบันทึกเลื่อนโอน")]
        public DateTime PostponeTransferDate { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("LCM อนุมัติ")]
        public Guid? LCMAprroveID { get; set; }
        [ForeignKey("LCMAprroveID")]
        public USR.User LCMAprrove { get; set; }
        [Description("วันที่ LCM อนุมัติ")]
        public DateTime? LCMApproveDate { get; set; }

        [Description("LCM Reject")]
        public Guid? LCMRejectID { get; set; }
        [ForeignKey("LCMRejectID")]
        public USR.User LCMReject { get; set; }
        [Description("วันที่ LCM Reject")]
        public DateTime? LCMRejectDate { get; set; }

    }
}
