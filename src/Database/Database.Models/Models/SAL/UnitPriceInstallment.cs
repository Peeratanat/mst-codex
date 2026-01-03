using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("รายการราคางวดของ Unit")]
    [Table("UnitPriceInstallment", Schema = Schema.SALE)]
    public class UnitPriceInstallment : BaseEntity
    {
        [Description("งวดที่")]
        public int Period { get; set; }

        //Installment
        [Description("ผ่อนค่าอะไร")]
        public Guid? UnitPriceID { get; set; }
        [ForeignKey("UnitPriceID")]
        public UnitPrice UnitPrice { get; set; }

        [Description("เป็นงวดพิเศษ")]
        public bool IsSpecialInstallment { get; set; }

        [Description("วันที่จ่ายล่าสุด")]
        public DateTime? PayDate { get; set; }

        [Description("จ่ายครบแล้ว")]
        public bool? IsPaid { get; set; }

        [Description("วันที่ต้องจ่าย")]
        public DateTime? DueDate { get; set; }

        [Description("ราคา")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Description("เงินที่จ่ายแล้ว")]
        [Column(TypeName = "Money")]
        public decimal PaidAmount { get; set; }

        [Description("บริษัทจ่ายให้")]
        public bool IsSellerPay { get; set; }
    }
}
