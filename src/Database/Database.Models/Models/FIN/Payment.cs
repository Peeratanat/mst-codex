using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("การรับเงิน")]
    [Table("Payment", Schema = Schema.FINANCE)]
    public class Payment : BaseEntity
    {
        [Description("ผูกข้อมูลใบจอง")]
        public Guid BookingID { get; set; }

        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("บันทึกข้อความ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

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

        [Description("ID ลูกค้า")]
        public Guid? ContactID { get; set; }

        [ForeignKey("ContactID")]
        public CTM.Contact Contact { get; set; }

        [Description("ชื่อ นามสกุลลูกค้าที่แสดงบนใบเสร็จ")]
        [MaxLength(500)]
        public string ContactName { get; set; }

        [Description("เลขที่ใบรับเงินชั่วคราว")]
        [MaxLength(100)]
        public string ReceiptTempNo { get; set; }

        [Description("เลขที่ใบเสร็จรับเงินตัวจริง")]
        [MaxLength(100)]
        public string ReceiptNo { get; set; }

        [Description("สถานะยกเลิกใบเสร็จ")]
        public bool IsCancel { get; set; }

        [Description("ชื่อรายการรับชำระเงินของ Payment นี้ ถ้ามีมากกว่า 1 รายการจะ , ต่อกัน")]
        [MaxLength(1000)]
        public string PaymentItemName { get; set; }

        [Description("ประเภทรับชำระเงินของ Payment นี้ ถ้ามีมากกว่า 1 รายการจะ , ต่อกัน")]
        [MaxLength(1000)]
        public string PaymentMethodName { get; set; }


        [Description("State ของ ใบเสร็จ ตอนรับเงิน")]
        public Guid PaymentStateMasterCenterID { get; set; }

        [ForeignKey("PaymentStateMasterCenterID")]
        public MST.MasterCenter PaymentState { get; set; }

        [Description("รหัสการ Post RV")]
        [MaxLength(100)]
        public string PostGLDocumentNo { get; set; }

        [Description("วันที่ Post RV ล่าสุด")]
        public DateTime? PostGLDate { get; set; }

        [Description("รหัสการ นำฝากเงิน")]
        [MaxLength(100)]
        public string DepositNo { get; set; }

        [Description("วันที่ นำฝากเงิน ล่าสุด")]
        public DateTime? DepositDate { get; set; }

        [Description("เป็นใบเสร็จที่รับเงินจาก LC,LCM เพื่อใช้สำหรับเช็คสิทธิ์ การยกเลิกใบเสร็จ")]
        public bool IsFromLC { get; set; }

        [Description("QuotationID จาก Prebook")]
        public Guid? QuotationID { get; set; }
    }
}
