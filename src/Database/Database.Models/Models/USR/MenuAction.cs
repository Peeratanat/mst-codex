using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{
    [Description("Action ของหน้าโปรแกรม")]
    [Table("MenuAction", Schema = Schema.USER)]
    public class MenuAction : BaseEntity
    {
        [Description("MenuID ของ Menu")]
        public Guid? MenuID { get; set; }
        [ForeignKey("MenuID")]
        public Menu Menu { get; set; }

        [Description("โค้ด")]
        [MaxLength(250)]
        public string MenuActionCode { get; set; }

        [Description("ชื่อ(ไทย)")]
        [MaxLength(250)]
        public string MenuActionName { get; set; }

        [Description("ลำดับแสดงผล")]
        public int Order { get; set; }
    }
}
