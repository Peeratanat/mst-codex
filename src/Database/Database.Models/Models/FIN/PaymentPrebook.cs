using Database.Models.USR;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("การรับเงิน")]
    [Table("PaymentPrebook", Schema = Schema.FINANCE)]
    public class PaymentPrebook : BaseEntityWithoutMigrate
    {
        [Description("ผูกข้อมูลเสนอราคา")]
        public Guid QuotationID { get; set; }
        [ForeignKey("QuotationID")]
        public SAL.Quotation Quotation { get; set; }

        [Description("ไฟล์แนบ")]
        [MaxLength(1000)]
        public string AttachFile { get; set; }

        [Description("ชื่อไฟล์แนบ")]
        [MaxLength(1000)]
        public string AttachFileName { get; set; }

        [Description("วันที่รับเงิน")]
        public DateTime ReceiveDate { get; set; }

        [Description("จำนวนเงินทั้งหมด")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        [Description("เลขที่ใบรับเงินชั่วคราว")]
        [MaxLength(100)]
        public string ReceiptPrebookNo { get; set; }

        [Description("สถานะยกเลิกใบเสร็จ")]
        public bool IsCancel { get; set; }

        [Description("ชื่อรายการรับชำระเงินของ Payment นี้ ถ้ามีมากกว่า 1 รายการจะ , ต่อกัน")]
        [MaxLength(1000)]
        public string PaymentItemName { get; set; }

        [Description("ประเภทรับชำระเงินของ Payment นี้ ถ้ามีมากกว่า 1 รายการจะ , ต่อกัน")]
        [MaxLength(1000)]
        public string PaymentMethodName { get; set; }

        [Description("รหัสการ Post UN")]
        [MaxLength(100)]
        public string PostGLDocumentNo { get; set; }

        [Description("วันที่ Post UN ล่าสุด")]
        public DateTime? PostGLDate { get; set; }

        [Description("เป็นใบเสร็จที่รับเงินจาก LC,LCM เพื่อใช้สำหรับเช็คสิทธิ์ การยกเลิกใบเสร็จ")]
        public bool IsFromLC { get; set; }

        [Description("Customer FirstName")]
        [MaxLength(1000)]
        public string CustomerFirstName { get; set; }

        [Description("Customer LastName")]
        [MaxLength(1000)]
        public string CustomerLastName { get; set; }

        [Description("Customer CitizenIdentity No")]
        [MaxLength(1000)]
        public string CustomerCitizenIdentityNo { get; set; }

        [Description("บันทึกข้อความ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("เหตุผลการยกเลิก")]
        [MaxLength(5000)]
        public string RejectReason { get; set; }


        [Description("ผู้ post รายการของทางบัญชี")]
        public Guid? PostGLByUserID { get; set; }
        [ForeignKey("PostGLByUserID")]
        public User PostGLByUser { get; set; }

        [Description("เพื่อบอกว่ารายการนี้ทางบัญชีได้ post หรือยัง")]
        public bool? IsPostGL { get; set; }

        [Description("วัน/เวลาที่ยกเลิก")]
        public DateTime? CancelDate { get; set; }


        [Description("flag ย้ายแปลง")]
        public bool IsChangeUnit { get; set; }

        [Description("flag ยกเลิกจองจริง")]
        public bool IsCancelRealBook { get; set; }
        public bool IsCancelAgreement { get; set; }

    }
}
