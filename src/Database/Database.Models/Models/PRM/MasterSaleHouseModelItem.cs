using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Master รายการโปรโมชั่นผูกกับแบบบ้าน (ขาย)")]
    [Table("MasterSaleHouseModelItem", Schema = Schema.PROMOTION)]
    public class MasterSaleHouseModelItem : BaseEntity
    {
        public Guid MasterSalePromotionItemID { get; set; }
        [ForeignKey("MasterSalePromotionItemID")]
        public MasterSalePromotionItem MasterSalePromotionItem { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public PRJ.Model Model { get; set; }
    }
}
