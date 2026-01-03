using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลโฉนด สาธารณูปโภค")]
    [Table("TitleDeedPublicUtility", Schema = Schema.PROJECT)]
    public class TitleDeedPublicUtility : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("แปลง")]
        public string UnitNo { get; set; }

        [Description("เลขที่ดิน")]
        [MaxLength(100)]
        public string LandNo { get; set; }
        [Description("หน้าสำรวจ")]
        [MaxLength(100)]
        public string LandSurveyArea { get; set; }
        [Description("เลขระวาง")]
        [MaxLength(50)]
        public string LandPortionNo { get; set; }
        [Description("เลขที่โฉนด")]
        [MaxLength(50)]
        public string TitledeedNo { get; set; }
        [Description("เล่ม")]
        [MaxLength(100)]
        public string BookNo { get; set; }
        [Description("หน้า")]
        [MaxLength(100)]
        public string PageNo { get; set; }
        [Description("พื้นที่โฉนด")]
        public double? TitledeedArea { get; set; }
        [Description("พื้นที่ใช้สอย")]
        public double? UsedArea { get; set; }
        [Description("ลำดับ")]
        public int? Order { get; set; }
    }
}
