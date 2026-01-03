using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("รายละเอียด ใบเสร็จรับเงินตัวจริง")]
    [Table("ReceiptDetail", Schema = Schema.FINANCE)]
    public class ReceiptDetail : BaseEntity
    {
        public Guid ReceiptHeaderID { get; set; }
        [ForeignKey("ReceiptHeaderID")]
        public FIN.ReceiptHeader ReceiptHeader { get; set; }

        [Description("รับชำระเงินค่าอะไร")]
        public Guid? PaymentItemID { get; set; }
        [ForeignKey("PaymentItemID")]
        public PaymentItem PaymentItem { get; set; }

        [Description("รายละเอียด")]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Description("รายละเอียด (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string DescriptionEN { get; set; }

        [Description("จำนวนเงินที่ชำระ")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Description("จำนวนเงิน(Th)")]
        public string AmountTh { get; set; }

        [Description("จำนวนเงิน(Eng)")]
        public string AmountEn { get; set; }

        //[Description("Link DepositDetail")]
        //public Guid? DepositDetailID { get; set; }
        //[ForeignKey("DepositDetailID")]
        //public DepositDetail DepositDetail { get; set; }

        //[Description("เลขที่นำฝาก")]
        //[MaxLength(1000)]
        //public string DepositNo { get; set; }

        [Description("ประเภทการรับเงิน เงินสด เช็ค")]
        public Guid? PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("ชื่อประเภทการรับเงิน")]
        [MaxLength(1000)]
        public string PaymentMethodName { get; set; }
        [Description("ชื่อประเภทการรับเงิน EN")]
        [MaxLength(1000)]
        public string PaymentMethodNameEN { get; set; }

        [Description("ID ธนาคาร")]
        public Guid? BankID { get; set; }

        [Description("ชื่อธนาคาร")]
        [MaxLength(1000)]
        public string BankName { get; set; }

        [Description("ชื่อธนาคาร En")]
        [MaxLength(1000)]
        public string BankNameEn { get; set; }

        [Description("สาขาธนาคาร")]
        [MaxLength(1000)]
        public string BankBranch { get; set; }

        [Description("เลขที่เช็ค,เลขที่บัตรเครดิต")]
        [MaxLength(1000)]
        public string Number { get; set; }

        [Description("วันที่หน้าเช็ค")]
        [MaxLength(1000)]
        public string ChequeDate { get; set; }

        [Description("จำนวนเงินหน้าเช็ค")]
        [Column(TypeName = "Money")]
        public decimal ChequeAmount { get; set; }

        [Description("ID นำฝาก")]
        public Guid? DepositID { get; set; }

        [Description("เลขที่นำฝาก")]
        [MaxLength(100)]
        public string DepositNo { get; set; }
    }
}
