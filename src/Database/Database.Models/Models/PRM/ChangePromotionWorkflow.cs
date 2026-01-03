using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("การอนุมัติเปลี่ยนแปลงโปรโมชั่น")]
    [Table("ChangePromotionWorkflow", Schema = Schema.PROMOTION)]
    public class ChangePromotionWorkflow : BaseEntity
    {
        [Description("ผู้ขอเปลี่ยนแปลง")]
        public Guid? RequestByUserID { get; set; }
        [ForeignKey("RequestByUserID")]
        public USR.User RequestByUser { get; set; }
        [Description("วันที่ขอ")]
        public DateTime RequestDate { get; set; }

        [Description("ชนิดของโปรโมชั่นที่ขอเปลี่ยนแปลง")]
        public Guid PromotionTypeMasterCenterID { get; set; }
        [ForeignKey("PromotionTypeMasterCenterID")]
        public MST.MasterCenter PromotionType { get; set; }

        [Description("ขั้นตอนของการซื้อแปลง")]
        public Guid SalePromotionStageMasterCenterID { get; set; }
        [ForeignKey("SalePromotionStageMasterCenterID")]
        public MST.MasterCenter SalePromotionStage { get; set; }


        [Description("สถานะอนุมัติ")]
        public bool? IsApproved { get; set; }
        [Description("วันที่อนุมัติ")]
        public DateTime? ApproveDate { get; set; }
        [Description("ผู้อนุมัติ")]
        public Guid? ApproveByUserID { get; set; }
        [ForeignKey("ApproveByUserID")]
        public USR.User ApproveByUser { get; set; }
        [Description("วันที่ Reject")]
        public DateTime? RejectDate { get; set; }
        [Description("ผู้ Reject")]
        public Guid? RejectByUserID { get; set; }
        [ForeignKey("RejectByUserID")]
        public USR.User RejectByUser { get; set; }


        [Description("สถานะอนุมัติของนิติกรรมสัญญา")]
        public bool? ContractIsApproved { get; set; }
        [Description("วันที่อนุมัติของนิติกรรมสัญญา")]
        public DateTime? ContractApproveDate { get; set; }
        [Description("ผู้อนุมัติของนิติกรรมสัญญา")]
        public Guid? ContractApproveByUserID { get; set; }
        [ForeignKey("ContractApproveByUserID")]
        public USR.User ContractApproveByUser { get; set; }
        [Description("วันที่ Reject ของนิติกรรมสัญญา")]
        public DateTime? ContractRejectDate { get; set; }
        [Description("ผู้ Reject ของนิติกรรมสัญญา")]
        public Guid? ContractRejectByUserID { get; set; }
        [ForeignKey("ContractRejectByUserID")]
        public USR.User ContractRejectByUser { get; set; }

        [Description("วันที่นัดลูกค้าเซ็นเอกสาร")]
        public DateTime? DueSignDocDate { get; set; }

        [Description("ผู้แนะนำ")]
        public Guid? ReferContactID { get; set; }
        [ForeignKey("ReferContactID")]
        public CTM.Contact ReferContact { get; set; }

        [Description("ชื่อผู้แนะนำ (สำหรับเก็บข้อมูล Free Text)")]
        [MaxLength(1000)]
        public string ReferContactName { get; set; }

        [Description("สถานะอนุมัติพิมพ์แบบบันทึก")]
        public bool? IsPrintApproved { get; set; }
        [Description("วัน-เวลาที่อนุมัติพิมพ์แบบบันทึก")]
        public DateTime? PrintApprovedDate { get; set; }
        [Description("ผู้อนุมัติพิมพ์แบบบันทึก")]
        public Guid? PrintApprovedByUserID { get; set; }
        [ForeignKey("PrintApprovedByUserID")]
        public USR.User PrintApprovedBy { get; set; }
    }
}
