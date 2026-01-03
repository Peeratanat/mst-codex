using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.ACC
{
    [Description("ข้อมูลประวัติการแก้ไข PostGLFormatTextFileDetail")]
    [Table("PostGLFormatTextFileDetail", Schema = Schema.ACCOUNT)]
    public class PostGLFormatTextFileDetailLog : BaseEntityLog
    {
    }
}
