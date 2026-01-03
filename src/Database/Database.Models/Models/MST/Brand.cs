using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ข้อมูลแบรนด์")]
    [Table("Brand", Schema = Schema.MASTER)]
    public class Brand : BaseEntity
    {

        [Description("รหัสแบรนด์")]
        [MaxLength(50)]
        public string BrandNo { get; set; }
        [Description("ชื่อแบรนด์")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Description("รูปแบบเลขที่แปลง")]
        public Guid? UnitNumberFormatMasterCenterID { get; set; }
        [ForeignKey("UnitNumberFormatMasterCenterID")]
        public MST.MasterCenter UnitNumberFormat { get; set; }

    }
}
