using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข การรับเงินจาก เงินโอนต่างชาติ")]
    [Table("PaymentForeignBankTransferLog", Schema = Schema.FINANCE)]
    public class PaymentForeignBankTransferLog : BaseEntityLog
    {
    }
}
