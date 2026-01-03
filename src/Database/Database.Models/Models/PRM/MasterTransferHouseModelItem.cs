using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Master รายการโปรโมชั่นผูกกับแบบบ้าน (โอน)")]
    [Table("MasterTransferHouseModelItem", Schema = Schema.PROMOTION)]
    public class MasterTransferHouseModelItem : BaseEntity
    {
        public Guid MasterTransferPromotionItemID { get; set; }
        [ForeignKey("MasterTransferPromotionItemID")]
        public MasterTransferPromotionItem MasterTransferPromotionItem { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public PRJ.Model Model { get; set; }
    }
}
