using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ค่าธรรมเนียมเครื่องรูดบัตร")]
    [Table("EDCFee", Schema = Schema.MASTER)]
    public class EDCFee : BaseEntity
    {
        [Description("เครื่องรูดบัตรธนาคาร")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("ชนิดบัตร (Debit, Credit)")]
        public Guid? PaymentCardTypeMasterCenterID { get; set; }
        [ForeignKey("PaymentCardTypeMasterCenterID")]
        public MST.MasterCenter PaymentCardType { get; set; }

        //VISA, MASTER, JCB
        [Description("ประเภทบัตร (Visa, Master, JCB)")]
        public Guid? CreditCardTypeMasterCenterID { get; set; }
        [ForeignKey("CreditCardTypeMasterCenterID")]
        public MST.MasterCenter CreditCardType { get; set; }

        //บัตรที่ลูกค้ารูด ธนาคารเดียวกัน หรือ ต่างธนาคาร
        [Description("บัตรที่ลูกค้ารูด ธนาคารเดียวกัน หรือ ต่างธนาคาร")]
        public bool IsEDCBankCreditCard { get; set; }

        //รูดเต็ม หรือ ผ่อน 0%
        [Description("รูดเต็ม หรือ ผ่อน 0%")]
        public Guid? CreditCardPaymentTypeMasterCenterID { get; set; }
        [ForeignKey("CreditCardPaymentTypeMasterCenterID")]
        public MST.MasterCenter CreditCardPaymentType { get; set; }

        [Description("ค่าธรรมเนียม (%)")]
        public double Fee { get; set; }

    }
}
