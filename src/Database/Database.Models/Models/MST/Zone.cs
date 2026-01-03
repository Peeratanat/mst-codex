using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("Zone ของโครงการ")]
    [Table("Zone", Schema = Schema.MASTER)]
    public class Zone : BaseEntityWithoutMigrate
    {
        [Description("NameTH")]
        [MaxLength(100)]
        public string NameTH { get; set; }

        [Description("NameEN")]
        [MaxLength(100)]
        public string NameEN { get; set; }

        [Description("Remark")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("BGID")]
        public Guid? BGID { get; set; }
        [ForeignKey("BGID")]
        public BG BG { get; set; }
    }
}
