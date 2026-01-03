using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข การรับเงินจาก เดบิตการ์ด")]
    [Table("PaymentDebitCardLog", Schema = Schema.FINANCE)]
    public class PaymentDebitCardLog : BaseEntityLog
    {
    }
}
