using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข การรับเงินจาก เช็คส่วนตัว")]
    [Table("PaymentPersonalChequeLog", Schema = Schema.FINANCE)]
    public class PaymentPersonalChequeLog : BaseEntityLog
    {
    }
}
