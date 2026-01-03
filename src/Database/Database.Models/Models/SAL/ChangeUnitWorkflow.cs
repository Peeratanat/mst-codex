using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("การอนุมัติย้ายแปลง จอง หรือ สัญญา")]
    [Table("ChangeUnitWorkflow", Schema = Schema.SALE)]
    public class ChangeUnitWorkflow : BaseEntity
    {
        //ย้ายแปลงจอง > PriceList > LCM อนุมัติตั้งเรื่อง > Min Price
        //ย้ายแปลงสัญญา > PriceList > LCM อนุมัติตั้งเรื่อง > Min Price > LC Upload File (optional) > นิติกรรมอนุมัติ

        [Description("จากใบจอง")]
        public Guid? FromBookingID { get; set; }
        [ForeignKey("FromBookingID")]
        public Booking FromBooking { get; set; }
        [Description("ไปที่ใบจอง")]
        public Guid? ToBookingID { get; set; }
        [ForeignKey("ToBookingID")]
        public Booking ToBooking { get; set; }

        [Description("จากสัญญา")]
        public Guid? FromAgreementID { get; set; }
        [ForeignKey("FromAgreementID")]
        public Agreement FromAgreement { get; set; }
        [Description("ไปที่สัญญา")]
        public Guid? ToAgreementID { get; set; }
        [ForeignKey("ToAgreementID")]
        public Agreement ToAgreement { get; set; }

        [Description("ตำแหน่งของผู้อนุมัติตั้งเรื่อง")]
        public Guid? RequestApproverRoleID { get; set; }
        [ForeignKey("RequestApproverRoleID")]
        public USR.Role RequestApproverRole { get; set; }
        [Description("ผู้อนุมัติตั้งเรื่อง")]
        public Guid? RequestApproverUserID { get; set; }
        [ForeignKey("RequestApproverUserID")]
        public USR.User RequestApproverUser { get; set; }
        [Description("วันที่อนุมัติตั้งเรื่อง")]
        public DateTime? RequestApprovedDate { get; set; }
        [Description("สถานะอนุมัติตั้งเรื่อง")]
        public bool? IsRequestApproved { get; set; }
        [Description("เหตุผลที่ไม่อนุมัติตั้งเรื่อง")]
        [MaxLength(5000)]
        public string RequestRejectComment { get; set; }

        [Description("ตำแหน่งของผู้อนุมัติ")]
        public Guid? ApproverRoleID { get; set; }
        [ForeignKey("ApproverRoleID")]
        public USR.Role ApproverRole { get; set; }
        [Description("ผู้อนุมัติ")]
        public Guid? ApproverUserID { get; set; }
        [ForeignKey("ApproverUserID")]
        public USR.User ApproverUser { get; set; }
        [Description("วันที่อนุมัติ")]
        public DateTime? ApprovedDate { get; set; }
        [Description("สถานะอนุมัติ")]
        public bool? IsApproved { get; set; }
        [Description("เหตุผลที่ไม่อนุมัติ")]
        [MaxLength(5000)]
        public string RejectComment { get; set; }
        
        [Description("เหตุผลขออนุมัติ Min Price")]
        public Guid? MinPriceRequestReasonMasterCenterID { get; set; }
        [ForeignKey("MinPriceRequestReasonMasterCenterID")]
        public MST.MasterCenter MinPriceRequestReason { get; set; }

        [Description("เหตุผลขออนุมัติ Min Price อื่นๆ")]
        [MaxLength(1000)]
        public string OtherMinPriceRequestReason { get; set; }

        [Description("รับเงินจากแปลงเก่า")]
        [Column(TypeName = "Money")]
        public decimal? TotalPaid { get; set; }

        [Description("รหัสการ Post JV")]
        public string PostGLDocumentNo { get; set; }

        [Description("วันที่ Post JV")]
        public DateTime? PostGLDate { get; set; }
    }
}
