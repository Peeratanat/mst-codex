using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.USR
{
    [Description("กำหนดสิทธิ์เข้าถึงโครงการให้ User")]
    [Table("UserAuthorizeProject", Schema = Schema.USER)]
    public class UserAuthorizeProject : BaseEntity
    {
        [Description("ผูก User")]
        public Guid? UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
    }
}
