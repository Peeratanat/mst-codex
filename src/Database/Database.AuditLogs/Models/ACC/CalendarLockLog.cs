using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.ACC
{
    [Description("ข้อมูลประวัติการแก้ไข การ Lock ปิดบัญชี")]
    [Table("CalendarLockLog", Schema = Schema.ACCOUNT)]
    public class CalendarLockLog : BaseEntityLog
    {
    }
}