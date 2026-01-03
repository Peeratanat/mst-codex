using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.PRM
{
    [Description("Master รายการโปรโมชั่นโอน")]
    [Table("MasterTransferPromotionItemLogs", Schema = Schema.PROMOTION)]
    public class MasterTransferPromotionItemLog : BaseEntityLog
    {
        
    }
}
