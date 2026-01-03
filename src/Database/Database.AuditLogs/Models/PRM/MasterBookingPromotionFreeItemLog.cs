using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.PRM
{
    [Description("Master รายการโปรโมชั่นขาย ที่ไม่ต้องจัดซื้อ")]
    [Table("MasterBookingPromotionFreeItemLogs", Schema = Schema.PROMOTION)]
    public class MasterBookingPromotionFreeItemLog : BaseEntityLog
    {
        
    }
}
