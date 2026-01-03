using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข DirectCreditDebitExportHeader")]
    [Table("DirectCreditDebitExportHeaderLog", Schema = Schema.FINANCE)]
    public class DirectCreditDebitExportHeaderLog : BaseEntityLog
    {
    }
}
