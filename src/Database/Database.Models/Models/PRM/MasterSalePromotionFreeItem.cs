using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("Master รายการโปรโมชั่นขาย ที่ไม่ต้องจัดซื้อ")]
    [Table("MasterSalePromotionFreeItem", Schema = Schema.PROMOTION)]
    public class MasterSalePromotionFreeItem : BaseEntity
    {
        public Guid MasterSalePromotionID { get; set; }
        [ForeignKey("MasterSalePromotionID")]
        public MasterSalePromotion MasterSalePromotion { get; set; }

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

        [Description("วันที่ได้รับ")]
        public int? ReceiveDays { get; set; }
        [Description("ลูกค้าได้รับเมื่อ?")]
        public Guid? WhenPromotionReceiveMasterCenterID { get; set; }
        [ForeignKey("WhenPromotionReceiveMasterCenterID")]
        public MST.MasterCenter WhenPromotionReceive { get; set; }

        [Description("แสดงในสัญญา")]
        public bool IsShowInContract { get; set; }

        [Description("ลำดับของ Item")]
        public int Order { get; set; }
        [Description("รหัส Item")]
        public string PromotionItemNo { get; set; }

    }
}
