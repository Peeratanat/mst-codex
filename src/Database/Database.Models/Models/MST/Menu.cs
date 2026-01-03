using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("เมนูและ Sub เมนู")]
    [Table("Menu", Schema = Schema.MASTER)]
    public class Menu : BaseEntity
    {
        [Description("ชื่อ")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Description("รหัส Menu ที่เป็น Parent")]
        [ForeignKey("ParentMenuID")]
        public Guid? ParentMenuID { get; set; }
        public Menu ParentMenu { get; set; }

        [Description("รหัส Area ของ Menu")]
        [ForeignKey("MenuAreaID")]
        public Guid? MenuAreaID { get; set; }
        public MenuArea MenuArea { get; set; }

    }
}
