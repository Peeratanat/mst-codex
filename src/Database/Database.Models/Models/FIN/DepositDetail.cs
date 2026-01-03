using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("รายละเอียด รายการนำฝาก")]
    [Table("DepositDetail", Schema = Schema.FINANCE)]
    public class DepositDetail : BaseEntity
    {
        [Description("ID ของ DepositHeader")]
        public Guid DepositHeaderID { get; set; }
        [ForeignKey("DepositHeaderID")]
        public DepositHeader DepositHeader { get; set; }

        [Description("ID ของ PaymentMethod")]
        public Guid? PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("จำนวนเงินที่นำฝาก")]
        [Column(TypeName = "Money")]
        public decimal PayAmount { get; set; }

        [Description("ค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal Fee { get; set; }
    }
}
