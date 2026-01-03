using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("รายการค่าที่ชำระเงิน")]
    [Table("PaymentItem", Schema = Schema.FINANCE)]
    public class PaymentItem : BaseEntity
    {
        [Description("ผูกการชำระเงิน")]
        public Guid PaymentID { get; set; }
        [ForeignKey("PaymentID")]
        public Payment Payment { get; set; }
        
        //เงินที่จ่าย
        [Description("เงินที่จ่ายจริง")]
        [Column(TypeName = "Money")]
        public decimal PayAmount { get; set; }
        //เงินที่ต้องจ่าย
        [Description("เงินที่ต้องจ่าย")]
        [Column(TypeName = "Money")]
        public decimal ItemAmount { get; set; }
        //เงินที่คงเหลือต้องจ่าย
        [Description("เงินที่คงเหลือต้องจ่าย")]
        [Column(TypeName = "Money")]
        public decimal RemainAmount { get; set; }

        [Description("ค่าใช้จ่ายใน Unit ที่ต้องจ่าย")]
        public Guid? UnitPriceItemID { get; set; }
        [ForeignKey("UnitPriceItemID")]
        public SAL.UnitPriceItem UnitPriceItem { get; set; }
        [Description("งวดที่")]
        public Guid? UnitPriceInstallmentID { get; set; }
        [ForeignKey("UnitPriceInstallmentID")]
        public SAL.UnitPriceInstallment UnitPriceInstallment { get; set; }

        public Guid? MasterPriceItemID { get; set; }
        [ForeignKey("MasterPriceItemID")]
        public MST.MasterPriceItem MasterPriceItem { get; set; }

        //ลำดับงวดที่ชำระเงิน
        [Description("ลำดับงวดที่ชำระเงิน")]
        public int Period { get; set; }
    }
}
