using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("รายการค่าใช้จ่ายในใบเสนอราคา Prebook")]
    [Table("QuotationSalePromotionExpensePrebook", Schema = Schema.PROMOTION)]
    public class QuotationSalePromotionExpensePrebook : BaseEntityWithoutMigrate
    {
        [Description("ผูกโปรโมชั่นขายในใบเสนอราคา Prebook")]
        public Guid? QuotationSalePromotionPrebookID { get; set; }
        [ForeignKey("QuotationSalePromotionPrebookID")]
        public QuotationSalePromotionPrebook QuotationSalePromotionPrebook { get; set; }

        [Description("ผูกใบเสนอราคา")]
        public Guid? QuotationID { get; set; }

        [Description("จ่ายโดยใคร (บริษัท=0, ลูกค้า=1, คนละครึ่ง=2)")]
        public Guid? ExpenseReponsibleByMasterCenterID { get; set; }
        [ForeignKey("ExpenseReponsibleByMasterCenterID")]
        public MST.MasterCenter ExpenseReponsibleBy { get; set; }

        [Description("ชนิดของราคา")]
        public Guid? MasterPriceItemID { get; set; }
        [ForeignKey("MasterPriceItemID")]
        public MST.MasterPriceItem MasterPriceItem { get; set; }

        [Description("Freetext สำหรับบันทึกค่าใช้จ่าย")]
        [MaxLength(1000)]
        public string MasterPriceItemDescription { get; set; }

        [Description("ลำดับรายการ")]
        public int? MasterPriceItemSeqNo { get; set; }

        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Description("ราคาบริษัทจ่าย")]
        [Column(TypeName = "Money")]
        public decimal SellerAmount { get; set; }

        [Description("ราคาลูกค้าจ่าย")]
        [Column(TypeName = "Money")]
        public decimal BuyerAmount { get; set; }

        [Description("PriceName")]
        [MaxLength(1000)]
        public string PriceName { get; set; }

        [Description("UnitName")]
        [MaxLength(1000)]
        public string UnitName { get; set; }
        [Description("จำนวนหน่วย")]
        public double? PriceUnitAmount { get; set; }
        [Description("ราคาต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal? PricePerUnitAmount { get; set; }

    }
}
