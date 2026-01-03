using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{
    [Description("เมนูโปรด")]
    [Table("FavoriteMenu", Schema = Schema.USER)]
    public class FavoriteMenu : BaseEntity
    {
        [Description("ผูกผู้ใช้งาน")]
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        [Description("ผูกเมนู")]
        public Guid MenuID { get; set; }
        [ForeignKey("MenuID")]
        public Menu Menu { get; set; }

    }
}
