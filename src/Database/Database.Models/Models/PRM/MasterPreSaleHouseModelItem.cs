using Database.Models.PRM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("Master รายการโปรโมชั่นผูกกับแบบบ้าน (ก่อนขาย)")]
    [Table("MasterPreSaleHouseModelItem", Schema = Schema.PROMOTION)]
    public class MasterPreSaleHouseModelItem : BaseEntity
    {
        public Guid MasterPreSalePromotionItemID { get; set; }
        [ForeignKey("MasterPreSalePromotionItemID")]
        public MasterPreSalePromotionItem MasterPreSalePromotionItem { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public PRJ.Model Model { get; set; }
    }
}
