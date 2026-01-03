using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข ReceiptHeader")]
    [Table("ReceiptHeaderLog", Schema = Schema.FINANCE)]
    public class ReceiptHeaderLog : BaseEntityLog
    {
    }
}
