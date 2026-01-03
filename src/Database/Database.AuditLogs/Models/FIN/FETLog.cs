using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข FET")]
    [Table("FETLog", Schema = Schema.FINANCE)]
    public class FETLog : BaseEntityLog
    {
    }
}
