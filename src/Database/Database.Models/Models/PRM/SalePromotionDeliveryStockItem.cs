using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการการส่งมอบโปรโมชั่นขายจากระบบสตอค")]
    [Table("SalePromotionDeliveryStockItem", Schema = Schema.PROMOTION)]
    public class SalePromotionDeliveryStockItem : BaseEntity
    {
        [Description("ผูกรายการการส่งมอบโปรโมชั่นขาย")]
        public Guid? SalePromotionDeliveryItemID { get; set; }
        [ForeignKey("SalePromotionDeliveryItemID")]
        public SalePromotionDeliveryItem SalePromotionDeliveryItem { get; set; }

        [Description("เลขที่จากระบบ")]
        [MaxLength(100)]
        public string ReferenceStockId { get; set; }

        [Description("เลขที่ Serial")]
        [MaxLength(100)]
        public string SerialNo { get; set; }
        [Description("ประเภท Serial")]
        public bool? IsSerial { get; set; }


        [Description("เป็นของเทียบเท่า")]
        public bool? IsEqual { get; set; }
        [Description("Material Item (ของเทียบเท่า)")]
        [MaxLength(500)]
        public string MaterialItem_equal { get; set; }
        [Description("Material Name (ของเทียบเท่า)")]
        [MaxLength(500)]
        public string MaterialName_equal { get; set; }
        [Description("Material Group (ของเทียบเท่า)")]
        [MaxLength(50)]
        public string MaterialGroup_equal { get; set; }
        [Description("จำนวนที่ส่งมอบ (ของเทียบเท่า)")]
        public int? Quantity_equal { get; set; }
        [Description("ราคาต่อหน่วย (ของเทียบเท่า)")]
        public decimal? PricePerUnit_equal { get; set; }
    }
}
