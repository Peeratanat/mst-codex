using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.PRM
{
    [Description("Master รายการโปรโมชั่นก่อนขาย")]
    [Table("MasterPreSalePromotionItemLogs", Schema = Schema.PROMOTION)]
    public class MasterPreSalePromotionItemLog : BaseEntityLog
    {
        
    }
}
