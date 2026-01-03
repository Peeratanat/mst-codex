using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{
    [Description("กลุ่มของกลุ่มผู้ใช้งาน")]
    [Table("RoleGroup", Schema = Schema.USER)]
    public class RoleGroup : BaseEntity
    {
        [Description("ชื่อ")]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
