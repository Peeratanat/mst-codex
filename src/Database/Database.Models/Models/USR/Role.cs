using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{
    [Description("กลุ่มผู้ใช้งาน")]
    [Table("Role", Schema = Schema.USER)]
    public class Role : BaseEntity
    {
        [Description("Code")]
        [MaxLength(100)]
        public string Code { get; set; }
        [MaxLength(100)]
        [Description("ชื่อ")]
        public string Name { get; set; }

        [Description("กลุ่มของกลุ่มผู้ใช้งาน")]
        public Guid? RoleGroupID { get; set; }
        [ForeignKey("RoleGroupID")]
        public RoleGroup RoleGroup { get; set; }

        [Description("RoleID ในระบบ AP Authorize")]
        public int? RefID { get; set; }

        [Description("Default App")]
        public string DefaultApp { get; set; }
    }
}
