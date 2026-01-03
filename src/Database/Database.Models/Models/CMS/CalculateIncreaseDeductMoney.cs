using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("คำนวณ Commission ขายโครงการแนวสูง")]
    [Table("CalculateIncreaseDeductMoney", Schema = Schema.COMMISSION)]
    public class CalculateIncreaseDeductMoney : BaseEntity
    {
        [Description("ปีคำนวณคอมมิสชั่น")]
        public int PeriodYear { get; set; }
        [Description("เดือนคำนวณคอมมิสชั่น")]
        public int PeriodMonth { get; set; }

        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("พนักงานขาย")]
        public Guid? SaleUserID { get; set; }
        [ForeignKey("SaleUserID")]
        public USR.User SaleUser { get; set; }

        [Description("ค่าคอมมิสชั่นที่เพิ่ม")]
        [Column(TypeName = "Money")]
        public decimal? IncreaseAmount { get; set; }

        [Description("ค่าคอมมิสชั่นที่โดนหัก")]
        [Column(TypeName = "Money")]
        public decimal? DeductAmount { get; set; }

        [Description("ประเภทคอมมิสชั่น")]
        public Guid? CommissionTypeMasterCenterID { get; set; }
        [ForeignKey("CommissionTypeMasterCenterID")]
        public MST.MasterCenter CommissionType { get; set; }
    }
}
