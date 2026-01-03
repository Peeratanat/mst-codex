using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.USR
{
    [Description("สิทธิ์ของแต่ละ Role")]
    [Table("MenuPermission", Schema = Schema.USER)]
    public class MenuPermission : BaseEntity
    {
        [Description("Action")]
        public Guid? MenuActionID { get; set; }
        [ForeignKey("MenuActionID")]
        public MenuAction MenuAction { get; set; }

        [Description("Role ที่มีสิทธิ์")]
        public Guid? RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }
    }
}
