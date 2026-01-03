using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{

    [Description("รายการการส่งมอบโปรโมชั่นโอน")]
    [Table("TransferPromotionDeliveryItem", Schema = Schema.PROMOTION)]
    public class TransferPromotionDeliveryItem : BaseEntity
    {
        [Description("ผูกการส่งมอบโปรโมชั่นโอน")]
        public Guid? TransferPromotionDeliveryID { get; set; }
        [ForeignKey("TransferPromotionDeliveryID")]
        public TransferPromotionDelivery TransferPromotionDelivery { get; set; }
        [Description("โปรโมชั่นที่ส่งมอบ")]
        public Guid? TransferPromotionRequestItemID { get; set; }
        [ForeignKey("TransferPromotionRequestItemID")]
        public TransferPromotionRequestItem TransferPromotionRequestItem { get; set; }
        [Description("เบิกแล้ว")]
        public int ReceiveQuantity { get; set; }
        [Description("ส่งมอบแล้ว")]
        public int DeliveryQuantity { get; set; }
        [Description("คงเหลือเบิก")]
        public int RemainingDeliveryQuantity { get; set; }
        [Description("จำนวนที่ส่งมอบ")]
        public int Quantity { get; set; }
        //[Description("เลขที่ Serial")]
        //[MaxLength(100)]
        //public string SerialNo { get; set; }
        //[Description("ประเภท Serial")]
        //public bool? IsSerial { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        //[Description("Material Item (ของเทียบเท่า)")]
        //[MaxLength(500)]
        //public string MaterialItem_equal { get; set; }
        //[Description("Material Name (ของเทียบเท่า)")]
        //[MaxLength(500)]
        //public string MaterialName_equal { get; set; }
        //[Description("ราคาต่อหน่วย (ของเทียบเท่า)")]
        //public decimal? PricePerUnit_equal { get; set; }
        //[Description("Material Group (ของเทียบเท่า)")]
        //[MaxLength(50)]
        //public string MaterialGroup_equal { get; set; }
        //[Description("จำนวนที่ส่งมอบ (ของเทียบเท่า)")]
        //public int? Quantity_equal { get; set; }
    }
}
