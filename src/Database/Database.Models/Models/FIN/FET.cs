using Database.Models.USR;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("ข้อมูลการขอ FET")]
    [Table("FET", Schema = Schema.FINANCE)]
    public class FET : BaseEntity
    {
        [Description("ผู้ขอ")]
        public Guid? FETRequesterMasterCenterID { get; set; }

        [ForeignKey("FETRequesterMasterCenterID")]
        public MST.MasterCenter FETRequester { get; set; }

        [Description("รายการรับชำระเงิน")]
        public Guid? PaymentMethodID { get; set; }

        [ForeignKey("PaymentMethodID")]
        public FIN.PaymentMethod PaymentMethod { get; set; }


        [Description("การรับชำระเงินจากการตั้งพัก")]
        public Guid? UnknownPaymentID { get; set; }

        [ForeignKey("UnknownPaymentID")]
        public FIN.UnknownPayment UnknownPayment { get; set; }
        //[Description("ID ของการชำระเงิน")]
        //public Guid? ReferentGUID { get; set; }

        //[Description("Type ของการชำระเงิน")]
        //public string ReferentType { get; set; }
        //[MaxLength(200)]

        [Description("ID ของ Bank ที่จะขอ FET ใช้สำหรับกรณีขอ โดย AP เท่านั้น")]
        public Guid? BankID { get; set; }

        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("ID ของ Booking")]
        public Guid? BookingID { get; set; }

        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        //[Description("ID ของ Contact")]
        //public Guid? ContactID { get; set; }

        //[ForeignKey("ContactID")]
        //public CTM.Contact Contact { get; set; }

        [Description("ชื่อลูกค้า")]
        [MaxLength(2000)]
        public string CustomerName { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("สถานะการขอ FET")]
        public Guid? FETStatusMasterCenterID { get; set; }

        [ForeignKey("FETStatusMasterCenterID")]
        public MST.MasterCenter FETStatus { get; set; }

        [Description("ชื่อ File แนบ Credit Advice")]
        [MaxLength(1000)]
        public string AttachFileName { get; set; }

        [Description("Url File แนบ Credit Advice")]
        [MaxLength(1000)]
        public string AttachFileUrl { get; set; }

        [Description("หมายเหตุ Cancel")]
        [MaxLength(1000)]
        public string CancelRemark { get; set; }

        [Description("สถานะการตีกลับเอกสาร")]
        public bool? IsReject { get; set; }

        [Description("User ตีเอกสารกลับไปให้ LC")]
        public Guid? RejectByUserID { get; set; }

        [ForeignKey("RejectByUserID")]
        public User RejectByUser { get; set; }

        [Description("วันที่ การตีเอกสารกลับไปให้ LC")]
        public DateTime? RejectDate { get; set; }

        [Description("หมายเหตุ การตีเอกสารกลับไปให้ LC")]
        [MaxLength(1000)]
        public string RejectRemark { get; set; }

        //public Guid? ProjectID { get; set; }
        
        [Description("โอนสิทธิ์ใช้ไม่ได้")]
        public bool IsCancel { get; set; }
    }
}
