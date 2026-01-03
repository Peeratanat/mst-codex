using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{
    [Description("กลุ่มของ User")]
    [Table("UserRole", Schema = Schema.USER)]
    public class UserRole : BaseEntity
    {
        [Description("ผูก User")]
        public Guid? UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        [Description("ผูกกลุ่ม")]
        public Guid? RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }

    }
}
