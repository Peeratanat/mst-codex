using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Master รายการโปรโมชั่นที่ไม่ต้องจัดซื้อผูกกับแบบบ้าน (ขาย)")]
    [Table("MasterSaleHouseModelFreeItem", Schema = Schema.PROMOTION)]
    public class MasterSaleHouseModelFreeItem : BaseEntity
    {
        public Guid MasterSalePromotionFreeItemID { get; set; }
        [ForeignKey("MasterSalePromotionFreeItemID")]
        public MasterSalePromotionFreeItem MasterSalePromotionFreeItem { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public PRJ.Model Model { get; set; }
    }
}
