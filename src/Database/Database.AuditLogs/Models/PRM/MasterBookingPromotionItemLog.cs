using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.PRM
{
    [Description("Master รายการโปรโมชั่นขาย")]
    [Table("MasterBookingPromotionItemLogs", Schema = Schema.PROMOTION)]
    public class MasterBookingPromotionItemLog : BaseEntityLog
    {
        
    }
}
