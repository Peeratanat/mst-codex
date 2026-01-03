using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.USR;

namespace Database.Models.FIN
{
    [Description("เงินโอนไม่ทราบผู้โอน")]
    [Table("UnknownPayment", Schema = Schema.FINANCE)]
    public class UnknownPayment : BaseEntity
    {
        [Description("เลขที่ตั้งพัก/เลขที่ PI")]
        public string UnknownPaymentCode { get; set; }

        [Description("บริษัทที่รับเงิน")]
        public Guid? CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("โครงการที่รับเงิน ใช้สำหรับบันทึกบัญชี ค่าธรรมเนียม")]
        public Guid? ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("บัญชีธนาคารที่รับเงิน")]
        public Guid? BankAccountID { get; set; }

        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        [Description("ID ของ Booking กรณีที่รู้ว่าเงินนี้เป็นของห้องไหน แต่ต้องการบันทึกลงบัญชีพัก สามารถใช้ Wallet แทนได้แต่ Phase 1 ยังไม่มี Wallet")]
        public Guid? BookingID { get; set; }

        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("วันที่รับเงิน")]
        public DateTime ReceiveDate { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }
        
        [Description("สถานะรายการ เงินโอนไม่ทราบผู้โอน")]
        public Guid? UnknownPaymentStatusID { get; set; }

        [ForeignKey("UnknownPaymentStatusID")]
        public MST.MasterCenter UnknownPaymentStatus { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("หมายเหตุยกเลิก")]
        [MaxLength(1000)]
        public string CancelRemark { get; set; }

        [Description("หมายเหตุรายการด้าน SAP")]
        [MaxLength(1000)]
        public string SAPRemark { get; set; }

        //--------- เพิ่ม column สำหรับรองรับ เงินพัก จากบัตรเครดิต & เงินโอน ตปท --------------
        [Description("ชนิดของช่องทางการชำระเงิน")]
        public Guid? PaymentMethodTypeMasterCenterID { get; set; }
        [ForeignKey("PaymentMethodTypeMasterCenterID")]
        public MST.MasterCenter PaymentMethodType { get; set; }

        [Description("เลขที่เช็ค,เลขที่บัตรเครดิต => CreditCard,DebitCard")]
        [MaxLength(100)]
        public string Number { get; set; }

        [Description("ธนาคาร => CreditCard,DebitCard")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("ผิดบัญชี หรือ ผิดบริษัท => เงินโอนผ่านธนาคาร,ForeignBankTransfer,CreditCard,DebitCard")]
        public bool? IsWrongAccount { get; set; }

        [Column(TypeName = "Money")]
        [Description("ค่าธรรมเนียม => CreditCard,DebitCard,ForeignBankTransfer")]
        public decimal? Fee { get; set; }

        //สถานะตรวจสอบค่าธรรมเนียม
        [Description("สถานะตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก => CreditCard,DebitCard")]
        public bool? IsFeeConfirm { get; set; }

        [Description("วันที่ ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก => CreditCard,DebitCard")]
        public DateTime? FeeConfirmDate { get; set; }

        [Description("ผู้ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก => CreditCard,DebitCard")]
        public Guid? FeeConfirmByUserID { get; set; }
        [ForeignKey("FeeConfirmByUserID")]
        public User FeeConfirmByUser { get; set; }

        [Column(TypeName = "Money")]
        [Description("เปอร์เซ็นต์ธรรมเนียม => CreditCard,DebitCard")]
        public decimal? FeePercent { get; set; }

        [Column(TypeName = "Money")]
        [Description("Vat => CreditCard")]
        public decimal? Vat { get; set; }

        [Column(TypeName = "Money")]
        [Description("ค่าธรรมเนียม (หลัง Vat) => CreditCard,DebitCard")]
        public decimal? FeeIncludingVat { get; set; }

        [Description("เป็นบัตรต่างประเทศหรือไม่ => CreditCard")]
        public bool? IsForeignCreditCard { get; set; }

        //รูดเต็ม หรือ ผ่อน
        [Description("รูปแบบการจ่ายเงิน (รูดเต็ม หรือ ผ่อน) => CreditCard,DebitCard")]
        public Guid? CreditCardPaymentTypeMasterCenterID { get; set; }
        [ForeignKey("CreditCardPaymentTypeMasterCenterID")]
        public MST.MasterCenter CreditCardPaymentType { get; set; }

        //VISA, MASTER, JCB
        [Description("ประเภทบัตร (Visa, Master, JCB) => CreditCard,DebitCard")]
        public Guid? CreditCardTypeMasterCenterID { get; set; }
        [ForeignKey("CreditCardTypeMasterCenterID")]
        public MST.MasterCenter CreditCardType { get; set; }

        [Description("ธนาคารของเครื่องรูดบัตร => CreditCard,DebitCard")]
        public Guid? EDCBankID { get; set; }
        [ForeignKey("EDCBankID")]
        public MST.Bank EDCBank { get; set; }


        [Description("ประเภทการโอนเงินต่างประเทศ => ForeignBankTransfer")]
        public Guid? ForeignTransferTypeMasterCenterID { get; set; }
        [ForeignKey("ForeignTransferTypeMasterCenterID")]
        public MST.MasterCenter ForeignTransferType { get; set; }

        [Description("IR => ForeignBankTransfer")]
        [MaxLength(100)]
        public string IR { get; set; }

        [Description("ชื่อผู้โอน => ForeignBankTransfer")]
        [MaxLength(1000)]
        public string TransferorName { get; set; }

        [Description("ต้องขอ FET หรือไม่ => ForeignBankTransfer")]
        public bool? IsRequestFET { get; set; }

        [Description("แจ้งแก้ไข FET => ForeignBankTransfer")]
        public bool? IsNotifyFET { get; set; }

        [Description("ข้อความแจ้งเตือน => ForeignBankTransfer")]
        [MaxLength(5000)]
        public string NotifyFETMemo { get; set; }

        [Description("รหัสการ Post UN")]
        [MaxLength(100)]
        public string PostGLDocumentNo { get; set; }

        [Description("วันที่ Post UN")]
        public DateTime? PostGLDate { get; set; }


        [Description("ID รายการตั้งพัก BillPayment ")]
        public Guid? BillPaymentDetailID { get; set; }
        [ForeignKey("BillPaymentDetailID")]
        public FIN.BillPaymentDetail BillPaymentDetail { get; set; }


        [Description("ทำคืนหรือยังไม่ได้ทำคืน")]
        public bool? IsRefund { get; set; }

        [Description("วันที่การเงิน ติ๊กทำคืน")]
        public DateTime? RefundDate { get; set; }


        [Description("ผู้ทำการติ๊กทำคืนลูกค้า")]
        public Guid? RefundUserID { get; set; }
        [ForeignKey("RefundUserID")]
        public USR.User User { get; set; }

        [Description("เพื่อบอกว่าเป็นรายการการตั้งพักเงินเข้าที่บัญชีเงินพักนี้มาจาก Auto Cancel (ค้างงวดดาวน์)")]
        public bool IsAutoCC { get; set; }
        [Description("วันที่การเงิน ทำรายการ")]
        public DateTime? AutoCCDate { get; set; }
        [Description("ผู้ที่ทำรายการ")]
        public Guid? AutoCCUserID { get; set; }
        [ForeignKey("AutoCCUserID")]
        public USR.User AutoCCUser { get; set; }
    }
}
