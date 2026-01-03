using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข OfflinePaymentDetail")]
    [Table("OfflinePaymentDetailLog", Schema = Schema.FINANCE)]
    public class OfflinePaymentDetailLog : BaseEntityLog
    {
    }
}
