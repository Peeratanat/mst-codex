using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ACC
{
    [Description("Update ข้อมูลสถานะ Unit ส่งไประบบ SAP")]
    [Table("SAPUnitStatus", Schema = Schema.ACCOUNT)]
    public class SAPUnitStatus : BaseEntity
    {
        [Description("โครงการ")]
        public Guid ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("วันที่ เวลา เริ่มประมวผล")]
        public DateTime? StartDate { get; set; }

        [Description("วันที่ เวลา ประมวผลเสร็จ")]
        public DateTime? FinishDate { get; set; }

        [Description("Read/Write")]
        [MaxLength(50)]
        public string ProcessName { get; set; }

        [Description("สถานะของ Process")]
        public Guid? SAPProcessStatusMasterCenterID { get; set; }

        [ForeignKey("SAPProcessStatusMasterCenterID")]
        public MST.MasterCenter SAPProcessStatus { get; set; }

        [Description("% ความคืบหน้างาน")]
        public int PercentProgress { get; set; }

        [Description("ข้อความ")]
        [MaxLength(2000)]
        public string Message { get; set; }
    }
}
