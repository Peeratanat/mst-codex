using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข การรับเงินจาก Cashier Cheque")]
    [Table("PaymentCashierChequeLog", Schema = Schema.FINANCE)]
    public class PaymentCashierChequeLog : BaseEntityLog
    {
    }
}
