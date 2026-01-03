using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.PRM
{
    [Description("โปรโมชั่นรูดบัตรเครดิตที่มีให้เลือก (โอน)")]
    [Table("MasterTransferCreditCardItemLogs", Schema = Schema.PROMOTION)]
    public class MasterTransferCreditCardItemLog : BaseEntityLog
    {
        
    }
}
