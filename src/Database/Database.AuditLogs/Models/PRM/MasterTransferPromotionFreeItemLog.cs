using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.PRM
{
    [Description("Master รายการโปรโมชั่นโอน ที่ไม่ต้องจัดซื้อ")]
    [Table("MasterTransferPromotionFreeItemLogs", Schema = Schema.PROMOTION)]
    public class MasterTransferPromotionFreeItemLog : BaseEntityLog
    {
        
    }
}
