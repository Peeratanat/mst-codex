using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Item ที่เบิกโปรก่อนขาย")]
    [Table("PreSalePromotionRequestItem", Schema = Schema.PROMOTION)]
    public class PreSalePromotionRequestItem : BaseEntity
    {
        [Description("Master รายการโปรก่อนขาย")]
        public Guid? MasterPreSalePromotionItemID { get; set; }
        [ForeignKey("MasterPreSalePromotionItemID")]
        public MasterPreSalePromotionItem MasterPreSalePromotionItem { get; set; }

        [Description("แปลงที่ได้รับ")]
        public Guid? PreSalePromotionRequestUnitID { get; set; }
        [ForeignKey("PreSalePromotionRequestUnitID")]
        public PreSalePromotionRequestUnit PreSalePromotionRequestUnit { get; set; }

        [Description("ชื่อผลิตภัณฑ์ (TH)")]
        [MaxLength(1000)]
        public string NameTH { get; set; }
        [Description("ชื่อผลิตภัณฑ์ (EN)")]
        [MaxLength(1000)]
        public string NameEN { get; set; }
        [Description("จำนวน")]
        public int Quantity { get; set; }
        [Description("หน่วย (TH)")]
        [MaxLength(100)]
        public string UnitTH { get; set; }
        [Description("หน่วย (EN)")]
        [MaxLength(100)]
        public string UnitEN { get; set; }
        [Description("ราคาต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal PricePerUnit { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }
        [Description("วันที่คาดว่าจะได้รับ")]
        public DateTime? ReceiveDate { get; set; }
        [Description("เลขที่ PR")]
        [MaxLength(100)]
        public string PRNo { get; set; }
        [Description("วันที่เบิกโปรโมชั่น")]
        public DateTime? ReceivePromotionDate { get; set; }
    }
}
