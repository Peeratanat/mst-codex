using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.USR
{
    [Description("สิทธิ์ของแต่ละ Role")]
    [Table("DashboardPermission", Schema = Schema.USER)]
    public class DashboardPermission : BaseEntityWithoutMigrate
    {
        [Description("DashboardMenuID")]
        public Guid? DashboardMenuID { get; set; }
        [ForeignKey("DashboardMenuID")]
        public DashboardMenu DashboardMenu { get; set; }

        [Description("Role ที่มีสิทธิ์")]
        public Guid? RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }
    }
}
