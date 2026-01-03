using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("SubBG")]
    [Table("SubBG", Schema = Schema.MASTER)]
    public class SubBG : BaseEntity
    {
        [Description("รหัส Sub BG")]
        [MaxLength(50)]
        public string SubBGNo { get; set; }
        [Description("ชื่อ Sub BG")]
        [MaxLength(100)]
        public string Name { get; set; }
        public Guid? BGID { get; set; }
        [ForeignKey("BGID")]
        public BG BG { get; set; }

        public List<PRJ.Project> Projects { get; set; }
    }
}
