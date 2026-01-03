using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Description("Log การเก็บข้อมูลการอนุมัติพิมพ์เอกสารทางนิติกรรม")]
    [Table("AprvPrintDocContract", Schema = Schema.LOG)]
    public class AprvPrintDocContract : BaseEntity
    {
        [Description("รหัสเลขที่สัญญา")]
        public Guid AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }
        
        [Description("แปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
        
        [Description("Flag การเปลี่ยนชื่อ File N -> Y")]
        [MaxLength(2)]
        [DefaultValue("N")]
        public string IsRenameFileFlag { get; set; }
        
        [Description("DateTime สำหรับการเปลี่ยนชื่อไฟล์")]
        public DateTime? RenameFileFlagDate { get; set; }
        
        [Description("ผู้ทำการเปลี่ยนแปลงชื่อ")]
        [MaxLength(50)]
        public string RenameFileBy { get; set; }
        
        [Description("Flag การเปลี่ยนชื่อ File N -> Y")]
        [MaxLength(2)]
        public string IsSentMail2LCFlag { get; set; }
        
        [Description("DateTime สำหรับการเปลี่ยนชื่อไฟล์")]
        public DateTime? SentMail2LCDate { get; set; }
        
        [Description("ผู้ทำการเปลี่ยนแปลงชื่อ")]
        [MaxLength(50)]
        public string SentMail2LCBy { get; set; }
    }
}
