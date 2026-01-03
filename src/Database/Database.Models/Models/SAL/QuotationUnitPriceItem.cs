using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("รายละเอียด")]
    [Table("QuotationUnitPriceItem", Schema = Schema.SALE)]
    public class QuotationUnitPriceItem : BaseEntity
    {
        public Guid QuotationUnitPriceID { get; set; }
        [ForeignKey("QuotationUnitPriceID")]
        public QuotationUnitPrice QuotationUnitPrice { get; set; }

        [Description("ลำดับของราคา")]
        public int Order { get; set; }

        [Description("ชนิดของราคา")]
        public Guid? MasterPriceItemID { get; set; }
        [ForeignKey("MasterPriceItemID")]
        public MST.MasterPriceItem MasterPriceItem { get; set; }
        [Description("ชื่อของรายการ")]
        public string Name { get; set; }
        [Description("จำนวนหน่วย")]
        public double? PriceUnitAmount { get; set; }
        [Description("หน่วย")]
        public Guid? PriceUnitMasterCenterID { get; set; }
        [ForeignKey("PriceUnitMasterCenterID")]
        public MST.MasterCenter PriceUnit { get; set; }
        [Description("ราคาต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal? PricePerUnitAmount { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }
        [Description("เป็นรายการที่ต้องจ่ายหรือไม่")]
        public bool IsToBePay { get; set; }

        //[Description("จำนวนงวด (ถ้ามี) (รวมงวดพิเศษ)")]
        //public int? Installment { get; set; }
        //[Description("ราคาต่องวด")]
        //[Column(TypeName = "Money")]
        //public decimal? InstallmentAmount { get; set; }
        //[Description("งวดพิเศษ (eg. 1,2,10)")]
        //[MaxLength(1000)]
        //public string SpecialInstallments { get; set; }
        //[Description("ราคางวดพิเศษ (eg. 1000.00,2000.00,3000)")]
        //[MaxLength(1000)]
        //public string SpecialInstallmentAmounts { get; set; }

        [Description("ประเภทของราคา")]
        public Guid? PriceTypeMasterCenterID { get; set; }
        [ForeignKey("PriceTypeMasterCenterID")]
        public MST.MasterCenter PriceType { get; set; }


    }
}
