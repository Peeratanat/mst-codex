using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.PRJ
{
    [Description("แปลง")]
    [Table("UnitLog", Schema = Schema.PROJECT)]
    public class UnitLog : BaseEntityLog
    {
        
    }
}
