using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข การรับเงินจาก เงินสด เก็บ lOg ที่ table Paymentmethod")]
    [Table("PaymentCashLog", Schema = Schema.FINANCE)]
    public class PaymentCashLog : BaseEntityLog
    {
    }
}
