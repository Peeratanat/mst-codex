using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข การรับเงินจาก เครดิตการ์ด")]
    [Table("PaymentCreditCardLog", Schema = Schema.FINANCE)]
    public class PaymentCreditCardLog : BaseEntityLog
    {
    }
}
