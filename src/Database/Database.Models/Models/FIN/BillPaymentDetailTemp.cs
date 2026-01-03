using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("รายการ Bill Payment จากธนาคาร")]
    [Table("BillPaymentDetailTemp", Schema = Schema.FINANCE)]
    public class BillPaymentDetailTemp : BaseEntity
    {
        [Description("ข้อมูลการนำเข้า Bill Payment")]
        public Guid BillPaymentHeaderID { get; set; }
        [ForeignKey("BillPaymentHeaderID")]
        public BillPaymentTemp BillPayment { get; set; }

        [Description("รหัส BatchID ของ Detail")]
        public string DetailBatchID { get; set; }

        [Description("วันที่ลูกค้าจ่ายเงิน")]
        public DateTime ReceiveDate { get; set; }

        [Description("Referenct Code จากธนาคาร 1")]
        [MaxLength(50)]
        public string BankRef1 { get; set; }

        [Description("Referenct Code จากธนาคาร 2")]
        [MaxLength(50)]
        public string BankRef2 { get; set; }

        [Description("Referenct Code จากธนาคาร 3")]
        [MaxLength(50)]
        public string BankRef3 { get; set; }

        [Description("ID ของ Booking ที่ลูกค้าชำระ Bill Payment")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("การชำระ")]
        [MaxLength(50)]
        public string PayType { get; set; }
        
        [Description("จำนวนเงินที่จ่าย")]
        [Column(TypeName = "Money")]
        public decimal PayAmount { get; set; }

        [Description("สถานะ Bill Payment")]
        public Guid? BillPaymentStatusMasterCenterID { get; set; }
        [ForeignKey("BillPaymentStatusMasterCenterID")]
        public MST.MasterCenter BillPaymentStatus { get; set; }
        
        [Description("วันที่-เวลา Reconcile หรือ confirm รายการ")]
        public DateTime? ReconcileDate { get; set; }

        [Description("ผิดบัญชี")]
        public bool IsWrongAccount { get; set; }

        [Description("กลุ่มเหตุผลการยกเลิก")]
        public Guid? BillPaymentDeleteReasonMasterCenterID { get; set; }
        [ForeignKey("BillPaymentDeleteReasonMasterCenterID")]
        public MST.MasterCenter BillPaymentDeleteReason { get; set; }

        [Description("หมายเหตุการยกเลิก")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("ชื่อลูกค้า")]
        [MaxLength(1000)]
        public string CustomerName { get; set; }
    }
}
