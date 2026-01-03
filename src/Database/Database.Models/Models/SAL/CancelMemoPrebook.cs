using Database.Models.FIN;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("ยกเลิกห้องของลูกค้า")]
    [Table("CancelMemoPrebook", Schema = Schema.SALE)]
    public class CancelMemoPrebook : BaseEntityWithoutMigrate
    {
        [Description("ผูกใบเสนอราคา")]
        public Guid? QuotationID { get; set; }
        [ForeignKey("QuotationID")]
        public SAL.Quotation Quotation { get; set; }

        [Description("Payment Prebook ID")]
        public Guid? PaymentPrebookID { get; set; }
        [ForeignKey("PaymentPrebookID")]
        public PaymentPrebook PaymentPrebook { get; set; }

        [Description("PaymentMethodPrebook")]
        public Guid? PaymentMethodPrebookID { get; set; }
        [ForeignKey("PaymentMethodPrebookID")]
        public PaymentMethodPrebook PaymentMethodPrebook { get; set; }

        [Description("รูปแบบการยกเลิก (การคืนเงิน) (CancelReturnType)")]
        public Guid? CancelReturnMasterCenterID { get; set; }
        [ForeignKey("CancelReturnMasterCenterID")]
        public MST.MasterCenter CancelReturn { get; set; }

        [Description("เหตุผลการยกเลิก ")]
        public Guid? CancelReasonID { get; set; }
        [ForeignKey("CancelReturnMasterCenterID")]
        public MST.CancelReason CancelReason { get; set; }

        [Description("เหตุผลอื่นๆการยกเลิก")]
        public string CancelRemark { get; set; }

        [Description("ผู้ทำการยกเลิก (LCM)")]
        public Guid? CancelByUserID { get; set; }
        [ForeignKey("CancelByUserID")]
        public USR.User CancelByUser { get; set; }

        [Description("ยอดรวมมูลค่าที่รับมา")]
        public decimal? TotalReceivedAmount { get; set; }
        [Description("ยอดเงินยึดลูกค้า")]
        public decimal? PenaltyAmount { get; set; }
        [Description("ยอดเงินคืนลูกค้า")]
        public decimal? ReturnAmount { get; set; }
        [Description("เลขที่บัตรประจำตัวประชาชนลูกค้า")]
        public string ReturnCitizenIdentityNo { get; set; }
        [Description("เหตุผลอื่นๆของการยกเลิก")]
        public string OtherCancelReason { get; set; }
        [Description("bit เพื่อบอกว่ารายการนี้ทางบัญชีได้ post หรือยัง")]
        public bool? IsPostGL { get; set; }
        [Description("วันที่ Post GL")]
        public DateTime? PostGLDate { get; set; }
        [Description("หมายเลข Doc GL ที่ post ")]
        public string PostGLDocumentNo { get; set; }
        [Description("ID ผู้ post รายการของทางบัญชี")]
        public Guid? PostGLByUserID { get; set; }
        [ForeignKey("PostGLByUserID")]
        public USR.User PostGLByUser { get; set; }
        

    }
}
