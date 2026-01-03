using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("เบิกโฉนด")]
    [Table("TitledeedRequestFlow", Schema = Schema.SALE)]
    public class TitledeedRequestFlow: BaseEntityWithoutMigrate
    {
        [Description("โครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
        [Description("Unit")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Project Unit { get; set; }
        [Description("Booking")]
        public Guid BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }
        [Description("วันที่ขอเบิกโฉนด")]
        public DateTime? RequestDate { get; set; }
        [Description("เบอร์ติดต่ิชอ LC")]
        public string LCContactPhoneNumber { get; set; }
        [Description("วันที่นัดโอน")]
        public DateTime? ScheduleTransferDate { get; set; }
        [Description("มอบอำนาจ")]
        public bool? IsAuthorized { get; set; }
        [Description("LC Remark")]
        public string LCRemark { get; set; }
        [Description("TitledeedFlowStatus")]
        public Guid TitledeedFlowStatusMasterCenterID { get; set; }
        [ForeignKey("TitledeedFlowStatusMasterCenterID")]
        public MST.MasterCenter MasterCanter { get; set; }
        [Description("Approve/Reject")]
        public bool? IsApproveOrReject { get; set; }
        [Description("User ที่ approve หรือ reject")]
        public Guid? ApproveOrRejectByUserID { get; set; }
        [Description("วันที่ ที่ approve หรือ reject")]
        public DateTime? ApproveOrRejectDate { get; set; }
        [Description("Remark ที่ approve หรือ reject")]
        public string ApproveOrRejectRemark { get; set; }
    }
}
