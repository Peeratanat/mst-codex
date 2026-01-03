using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.USR;

namespace Database.Models.FIN
{
    [Description("ข้อมูลช่องทางของการชำระเงิน")]
    [Table("PaymentMethodPrebook", Schema = Schema.FINANCE)]
    public class PaymentMethodPrebook : BaseEntityWithoutMigrate
    {
        [Description("ผูกกับการชำระเงิน")]
        public Guid PaymentPrebookID { get; set; }
        [ForeignKey("PaymentPrebookID")]
        public PaymentPrebook PaymentPrebook { get; set; }

        [Description("ชนิดของช่องทางการชำระเงิน")]
        public Guid? PaymentMethodTypeMasterCenterID { get; set; }
        [ForeignKey("PaymentMethodTypeMasterCenterID")]
        public MST.MasterCenter PaymentMethodType { get; set; }

        //เงินที่จ่าย
        [Description("จำนวนเงินที่จ่าย")]
        [Column(TypeName = "Money")]
        public decimal PayAmount { get; set; }

        [Description("จำนวนเงิน หลังหักค่าธรรมเนียม หัก Vat")]
        [Column(TypeName = "Money")]
        public decimal NetAmount { get; set; }

        /* ------- โครงสร้างใหม่ ณ 2019-01-17 ---------------------------- */

        [Description("เลขที่เช็ค, เลขที่บัตรเครดิต => CashierCheque,PersonalCheque,CreditCard,DebitCard")]
        [MaxLength(100)]
        public string Number { get; set; }

        [Description("บัญชีธนาคารที่โอนเข้า => เงินโอนผ่านธนาคาร,ForeignBankTransfer,QR")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        [Description("ธนาคาร => CashierCheque,PersonalCheque,CreditCard,DebitCard")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("สาขาธนาคาร => CashierCheque,PersonalCheque")]
        [MaxLength(200)]
        public string BankBranchName { get; set; }

        [Description("สั่งจ่ายให้บริษัท => CashierCheque,PersonalCheque")]
        public Guid? PayToCompanyID { get; set; }
        [ForeignKey("PayToCompanyID")]
        public MST.Company PayToCompany { get; set; }

        [Description("ผิดบัญชี หรือ ผิดบริษัท => เงินโอนผ่านธนาคาร,BillPayment,ForeignBankTransfer,QR,CashierCheque,PersonalCheque,CreditCard,DebitCard")]
        public bool? IsWrongAccount { get; set; }

        [Description("วันที่หน้าเช็ค => CashierCheque,PersonalCheque")]
        public DateTime? ChequeDate { get; set; }

        [Description("ผู้บันทึกสถานะ เช็ครอนำฝาก ที่หน้านำฝาก => CashierCheque,PersonalCheque")]
        public Guid? ChequeConfirmBy { get; set; }
        [ForeignKey("ChequeConfirmBy")]
        public User User { get; set; }

        [Description("วันที่บันทึกสถานะ เช็ครอนำฝาก ที่หน้านำฝาก => CashierCheque,PersonalCheque")]
        public DateTime? ChequeConfirmDate { get; set; }

        [Description("สถานะ เช็ครอนำฝาก ที่หน้านำฝาก 1=เช็ครอนำฝาก 0= => CashierCheque,PersonalCheque")]
        public bool? IsChequeConfirm { get; set; }

        [Column(TypeName = "Money")]
        [Description("ค่าธรรมเนียม => CashierCheque,PersonalCheque,CreditCard,DebitCard,ForeignBankTransfer,QR")]
        public decimal? Fee { get; set; }        

        //สถานะตรวจสอบค่าธรรมเนียม
        [Description("สถานะตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก => CashierCheque,PersonalCheque,CreditCard,DebitCard")]
        public bool? IsFeeConfirm { get; set; }

        [Description("วันที่ ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก => CashierCheque,PersonalCheque,CreditCard,DebitCard")]
        public DateTime? FeeConfirmDate { get; set; }

        [Description("ผู้ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก => CashierCheque,PersonalCheque,CreditCard,DebitCard")]
        public Guid? FeeConfirmByUserID { get; set; }
        [ForeignKey("FeeConfirmByUserID")]
        public User FeeConfirmByUser { get; set; }

        [Column(TypeName = "Money")]
        [Description("เปอร์เซ็นต์ธรรมเนียม => CashierCheque,PersonalCheque,CreditCard,DebitCard")]
        public decimal? FeePercent { get; set; }

        [Column(TypeName = "Money")]
        [Description("Vat => CreditCard")]
        public decimal? Vat { get; set; }

        [Column(TypeName = "Money")]
        [Description("จำนวนเงินสุทธิ หลังหัก ค่าธรรมเนียม และ Vat => CashierCheque,PersonalCheque,CreditCard,DebitCard")]
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

        [Description("นำฝากแล้ว = true")]
        public bool? IsDeposited { get; set; }

        [Description("เลขที่นำฝาก")]
        [MaxLength(100)]
        public string DepositNo { get; set; }

        [Description("วันที่นำฝาก")]
        public DateTime? DepositDate { get; set; }

        [Description("เหตุผลนำฝาก")]
        [MaxLength(5000)]
        public string DepositRemark { get; set; }



        [Description("ผู้นำฝากรายการ")]
        public Guid? DepositByUserID { get; set; }
        [ForeignKey("DepositByUserID")]
        public User DepositByUser { get; set; }
        [Description("วันที่แก้ไขนำฝาก")]
        public DateTime? DepositUpdated { get; set; }
        
    }
}
