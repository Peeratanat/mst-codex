using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นขายในใบเสนอราคา")]
    [Table("QuotationSalePromotionPrebook", Schema = Schema.PROMOTION)]
    public class QuotationSalePromotionPrebook : BaseEntityWithoutMigrate
    {
        [Description("ผูกใบเสนอราคา")]
        public Guid QuotationID { get; set; }
        [ForeignKey("QuotationID")]
        public SAL.Quotation Quotation { get; set; }

        [Description("ผูกโปรโมชั่น")]
        public Guid? MasterSalePromotionID { get; set; }
        [ForeignKey("MasterSalePromotionID")]
        public MasterSalePromotion MasterPromotion { get; set; }

        [Description("ลำดับ")]
        public int? SeqNo { get; set; }

        [Description("PromotionDescription")]
        [MaxLength(5000)]
        public string PromotionDescription { get; set; }

        [Description("Quantity")]
        public int? Quantity { get; set; }


        [Description("UnitName")]
        [MaxLength(5000)]
        public string UnitName { get; set; }

        [Description("PricePerUnit")]
        [Column(TypeName = "Money")]
        public decimal PricePerUnit { get; set; }

        [Description("TotalPrice")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }


    }
}
