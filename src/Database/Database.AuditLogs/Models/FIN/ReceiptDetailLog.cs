using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข ReceiptDetail")]
    [Table("ReceiptDetailLog", Schema = Schema.FINANCE)]
    public class ReceiptDetailLog : BaseEntityLog
    {
    }
}
