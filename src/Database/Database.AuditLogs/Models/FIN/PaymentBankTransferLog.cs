using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข การรับเงินจาก เงินโอนผ่านธนาคาร")]
    [Table("PaymentBankTransferLog", Schema = Schema.FINANCE)]
    public class PaymentBankTransferLog : BaseEntityLog
    {
    }
}
