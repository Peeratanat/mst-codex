using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{
    [Description("กำหนดค่าเริ่มต้นโครงการให้ User")]
    [Table("UserDefaultProject", Schema = Schema.USER)]
    public class UserDefaultProject : BaseEntity
    {
        [Description("ผูก User")]
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

    }
}
