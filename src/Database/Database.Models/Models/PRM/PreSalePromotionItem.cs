using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นก่อนขาย")]
    [Table("PreSalePromotionItem", Schema = Schema.PROMOTION)]
    public class PreSalePromotionItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นก่อนขาย")]
        public Guid PreSalePromotionID { get; set; }
        [ForeignKey("PreSalePromotionID")]
        public PreSalePromotion PreSalePromotion { get; set; }
        [Description("รายการเบิกโปรโมชั่นก่อนขาย")]
        public Guid PreSalePromotionRequestItemID { get; set; }
        [ForeignKey("PreSalePromotionRequestItemID")]
        public PreSalePromotionRequestItem PreSalePromotionRequestItem { get; set; }
        
        [Description("Master รายการโปรก่อนขาย")]
        public Guid? MasterPreSalePromotionItemID { get; set; }
        [ForeignKey("MasterPreSalePromotionItemID")]
        public MasterPreSalePromotionItem MasterPreSalePromotionItem { get; set; }

        [Description("จำนวน")]
        public int Quantity { get; set; }
        [Description("หน่วย")]
        [MaxLength(100)]
        public string Unit { get; set; }
        [Description("ราคาต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal PricePerUnit { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }

        [Description("IsActive")]
        public bool IsActive { get; set; }
    }
}
