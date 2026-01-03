using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("ใบเสร็จรับเงินชั่วคราว")]
    [Table("ReceiptTempDetail", Schema = Schema.FINANCE)]
    public class ReceiptTempDetail : BaseEntity
    {
        public Guid ReceiptTempHeaderID { get; set; }
        [ForeignKey("ReceiptTempHeaderID")]
        public ReceiptTempHeader ReceiptTempHeader { get; set; }

        [Description("รับชำระเงินค่าอะไร, จำนวนเงินที่ชำระ")]
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
    }
}
