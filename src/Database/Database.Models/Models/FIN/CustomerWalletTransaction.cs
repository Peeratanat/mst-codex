using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("รายการเดินบัญชีของกระเป๋าเงินลูกค้า")]
    [Table("CustomerWalletTransaction", Schema = Schema.FINANCE)]
    public class CustomerWalletTransaction : BaseEntity
    {
        [Description("ผูกข้อมูลกระเป่าเงินลูกค้า")]
        public Guid CustomerWalletID { get; set; }
        [ForeignKey("CustomerWalletID")]
        public CustomerWallet CustomerWallet { get; set; }

        [Description("จำนวนเงิน (ถ้าถอนเป็นลบ ฝากเป็นบวก)")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }
        [Description("ยอดเงินคงเหลือ")]
        [Column(TypeName = "Money")]
        public decimal RemainAmount { get; set; }

        [Description("ผูกข้อมูลช่องทางการชำระเงิน")]
        public Guid? PaymentTypeItemID { get; set; }
        [ForeignKey("PaymentTypeItemID")]
        public PaymentMethod PaymentTypeItem { get; set; }
    }
}
