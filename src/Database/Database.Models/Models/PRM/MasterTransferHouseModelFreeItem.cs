using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Master รายการโปรโมชั่นที่ไม่ต้องจัดซื้อผูกกับแบบบ้าน (โอน)")]
    [Table("MasterTransferHouseModelFreeItem", Schema = Schema.PROMOTION)]
    public class MasterTransferHouseModelFreeItem : BaseEntity
    {
        public Guid MasterTransferPromotionFreeItemID { get; set; }
        [ForeignKey("MasterTransferPromotionFreeItemID")]
        public MasterTransferPromotionFreeItem MasterTransferPromotionFreeItem { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public PRJ.Model Model { get; set; }
    }
}
