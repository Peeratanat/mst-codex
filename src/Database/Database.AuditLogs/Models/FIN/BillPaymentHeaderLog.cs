using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข BillPaymentHeader")]
    [Table("BillPaymentHeaderLog", Schema = Schema.FINANCE)]
    public class BillPaymentHeaderLog : BaseEntityLog
    {
    }
}
