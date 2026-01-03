using Database.Models.PRJ;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.CMS
{
    [Description("ช่วงเวลา Event LC Recon ของโครงการ")]
    [Table("LCReconEventPeriodByProject", Schema = Schema.COMMISSION)]
    public class LCReconEventPeriodByProject : BaseEntityWithoutMigrate
    {
        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("วันที่เริ่ม Event")]
        public DateTime? StartDate { get; set; }

        [Description("วันที่จบ Event")]
        public DateTime? EndDate { get; set; }

        [Description("Remark")]
        [MaxLength(2000)]
        public string Remark { get; set; }
    }
}
