using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("Setting ทั่วไปของ Commission")]
    [Table("GeneralSetting", Schema = Schema.COMMISSION)]
    public class GeneralSetting : BaseEntity
    {
        [Description("วันที่ Active")]
        public DateTime? ActiveDate { get; set; }

        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("เมื่อ Launch โครงการจะได้รับเงิน")]
        [Column(TypeName = "Money")]
        public decimal AfterLaunchAmount { get; set; } //ใช้สำหรับแนวราบ
        [Description("โครงการตั้งแต่วันที่")]
        public DateTime? LaunchStartDate { get; set; }
        [Description("โครงการสิ้นสุดวันที่")]
        public DateTime? LaunchEndDate { get; set; }

        [Description("สถานะ")]
        public bool IsActive { get; set; }

    }
}
