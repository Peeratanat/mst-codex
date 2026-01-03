using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข BillPaymentDetail")]
    [Table("BillPaymentDetailLog", Schema = Schema.FINANCE)]
    public class BillPaymentDetailLog : BaseEntityLog
    {
    }
}
