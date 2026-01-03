using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("BG")]
    [Table("BG", Schema = Schema.MASTER)]
    public class BG : BaseEntity
    {
        [Description("รหัส BG")]
        [MaxLength(50)]
        public string BGNo { get; set; }
        [Description("ชื่อ BG")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Description("สำหรับประเภทของโครงการ (แนวสูง แนวราบ)")]
        public Guid? ProductTypeMasterCenterID { get; set; }
        [ForeignKey("ProductTypeMasterCenterID")]
        public MST.MasterCenter ProductType { get; set; }

        public List<PRJ.Project> Projects { get; set; }
        public List<SubBG> SubBGs { get; set; }
    }
}
