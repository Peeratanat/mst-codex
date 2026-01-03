using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.PRM
{
    [Description("โปรโมชั่นรูดบัตรเครดิตที่มีให้เลือก (ขาย)")]
    [Table("MasterBookingCreditCardItemLog", Schema = Schema.PROMOTION)]
    public class MasterBookingCreditCardItemLog : BaseEntityLog
    {
        
    }
}
