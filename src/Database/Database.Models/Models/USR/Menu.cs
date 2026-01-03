using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.USR
{
    [Description("เมนูโปรแกรม")]
    [Table("Menu", Schema = Schema.USER)]
    public class Menu : BaseEntity
    {
        [Description("โค้ด")]
        [MaxLength(250)]
        public string MenuCode { get; set; }

        [Description("ชื่อ(ไทย)")]
        [MaxLength(250)]
        public string MenuNameTH { get; set; }

        [Description("ชื่อ(อังกฤษ)")]
        [MaxLength(250)]
        public string MenuNameEng { get; set; }

        [Description("MenuID ของ Menu หลัก")]
        public Guid? MainMenuID { get; set; }
        [ForeignKey("MainMenuID")]
        public Menu MainMenu { get; set; }

        [Description("Module ของเมนู")]
        public Guid? ModuleID { get; set; }
        [ForeignKey("ModuleID")]
        public Module Module { get; set; }
    }
}
