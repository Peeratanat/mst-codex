using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.USR
{
    [Description("DashboardMenu")]
    [Table("DashboardMenu", Schema = Schema.USER)]
    public class DashboardMenu : BaseEntityWithoutMigrate
    {
        [Description("เมนูหลัก (Master/Sale/Back office)")]
        [MaxLength(250)]
        public string MasterApp { get; set; }

        [Description("MenuID ของ Menu")]
        public Guid? MenuID { get; set; }
        [ForeignKey("MenuID")]
        public Menu Menu { get; set; }

        [Description("BG")]
        public Guid? BGID { get; set; }
        [ForeignKey("BGID")]
        public MST.BG BG { get; set; }

        [Description("Sub BG")]
        public Guid? SubBGID { get; set; }
        [ForeignKey("SubBGID")]
        public MST.SubBG SubBG { get; set; }

        [Description("โค้ด")]
        [MaxLength(250)]
        public string DashboardCode { get; set; }

        [Description("ชื่อ(ไทย)")]
        [MaxLength(250)]
        public string DashboardNameTH { get; set; }

        [Description("ชื่อ(อังกฤษ)")]
        [MaxLength(250)]
        public string DashboardNameEng { get; set; }

        [Description("URL")]
        [MaxLength(500)]
        public string DashboardURL { get; set; }
    }
}
