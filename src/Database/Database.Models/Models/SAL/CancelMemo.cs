using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("Memo การยกเลิกใบจองหรือสัญญา")]
    [Table("CancelMemo", Schema = Schema.SALE)]
    public class CancelMemo : BaseEntity
    {
        [Description("ใบจอง")]
        public Guid? BookingID { get; set; }
        public Booking Booking { get; set; }
        [Description("สัญญา")]
        public Guid? AgreementID { get; set; }
        public Agreement Agreement { get; set; }
        [Description("เป็นการยกเลิกสัญญา")]
        public bool HasAgreemnt { get; set; }
  
        [Description("รูปแบบการยกเลิก (การคืนเงิน) (CancelReturnType)")]
        public Guid? CancelReturnMasterCenterID { get; set; }
        [ForeignKey("CancelReturnMasterCenterID")]
        public MST.MasterCenter CancelReturn { get; set; }
        [Description("เหตุผลที่ยกเลิกใบจอง")]
        public Guid? CancelReasonID { get; set; }
        [ForeignKey("CancelReasonID")]
        public MST.CancelReason CancelReason { get; set; }
        [Description("เหตุผลการยกเลิกอื่นๆ")]
        [MaxLength(5000)]
        public string OtherCancelReason { get; set; }
        [Description("หลักฐานการกู้เงินไม่ผ่าน")]
        [MaxLength(100)]
        public string BankRejectDocument { get; set; }
        [ForeignKey("หมายเหตุการยกเลิก")]
        [MaxLength(5000)]
        public string CancelRemark { get; set; }
        [Description("ยกเลิกโดย")]
        public Guid? CancelByUserID { get; set; }
        [ForeignKey("CancelByUserID")]
        public USR.User CancelByUser { get; set; }
        [Description("รับเงินจากลูกค้า")]
        [Column(TypeName = "Money")]
        public decimal? TotalReceivedAmount { get; set; }
        [Description("มูลค่ารายการของที่ส่งมอบไปแล้ว")]
        [Column(TypeName = "Money")]
        public decimal? TotalPromotionDeliverAmount { get; set; }
        [Description("เบื้ยปรับ")]
        [Column(TypeName = "Money")]
        public decimal? PenaltyAmount { get; set; }
        [Description("เงินคืนลูกค้า")]
        [Column(TypeName = "Money")]
        public decimal? ReturnAmount { get; set; }

        [Description("ข้อมูลการจ่ายเงินคืนลูกค้า - ธนาคาร")]
        public Guid? ReturnBankID { get; set; }
        [ForeignKey("ReturnBankID")]
        public MST.Bank ReturnBank { get; set; }

        [Description("ข้อมูลการจ่ายเงินคืนลูกค้า - บัญชีธนาคาร")]
        [MaxLength(50)]
        public string ReturnBankAccount { get; set; }
        [Description("ข้อมูลการจ่ายเงินคืนลูกค้า - สาขาจังหวัด")]
        public Guid? ReturnProvinceID { get; set; }
        [ForeignKey("ReturnProvinceID")]
        public MST.Province ReturnProvince { get; set; }
        [Description("ข้อมูลการจ่ายเงินคืนลูกค้า - สาขา")]
        public Guid? ReturnBankBranchID { get; set; }
        [ForeignKey("ReturnBankBranchID")]
        public MST.BankBranch ReturnBankBranch { get; set; }
        [Description("ข้อมูลการจ่ายเงินคืนลูกค้า - ชื่อบัญชี")]
        [MaxLength(100)]
        public string ReturnBankAccountName { get; set; }
        [Description("ข้อมูลการจ่ายเงินคืนลูกค้า - เลขบัตรประชาชน")]
        [MaxLength(50)]
        public string ReturnCitizenIdentityNo { get; set; }
        [Description("ข้อมูลการจ่ายเงินคืนลูกค้า - สำเนา Book Bank")]
        [MaxLength(1000)]
        public string ReturnBookBankFile { get; set; }
        [Description("ผู้อนุมัติ")]
        public Guid? ApproveByUserID { get; set; }
        [ForeignKey("ApproveByUserID")]
        public USR.User ApproveByUser { get; set; }
        [Description("วันที่อนุมัติ")]
        public DateTime? ApproveDate { get; set; }

        [Description("รหัสการ Post JV")]
        public string PostGLDocumentNo { get; set; }

        [Description("วันที่ Post JV")]
        public DateTime? PostGLDate { get; set; }
    }
}
