using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("คำนวณค่า Commission โอนประจำเดือนโครงการแนวสูง")]
    [Table("CalculatePerMonthHighRiseTransfer", Schema = Schema.COMMISSION)]
    public class CalculatePerMonthHighRiseTransfer : BaseEntity
    {
        [Description("เดือนคำนวณคอมมิสชั่น")]
        public int PeriodMonth { get; set; }
        [Description("ปีคำนวณคอมมิสชั่น")]
        public int PeriodYear { get; set; }

        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }


        [Description("ผลรวมมูลค่าราคาขายสุทธิในเดือนคอมมิสชั่นโอน")]
        [Column(TypeName = "Money")]
        public decimal? TotalTransferAmount { get; set; }
        [Description("ผลรวมจำนวนแปลงโอนสุทธิในเดือนคอมมิสชั่นโอน")]
        public int? TotalTransferUnit { get; set; }


        [Description("ประเภท Rate Commission (Fix/NoFix)")]
        public Guid? CommissionPercentTypeMasterCenterID { get; set; }
        [ForeignKey("CommissionPercentTypeMasterCenterID")]
        public MST.MasterCenter CommissionPercentType { get; set; }
        [Description("มูลค่า Commission Fix ต่อแปลง")]
        [Column(TypeName = "Money")]
        public decimal? CommissionFixAmount { get; set; }
        [Description("อัตรา % Commission")]
        [Column(TypeName = "Money")]
        public decimal? CommissionPercentRate { get; set; }


        [Description("สถานะอนุมัติคอมมิสชั่น")]
        public bool? IsApprove { get; set; }
        [Description("วันที่อนุมัติคอมมิสชั่น")]
        public DateTime? ApproveDate { get; set; }
        [Description("ผู้อนุมัติคอมมิสชั่น")]
        public Guid? ApproveUserBy { get; set; }
        [ForeignKey("ApproveUserBy")]
        public USR.User ApproveUser { get; set; }

        [Description("วันที่ยกเลิกอนุมัติคอมมิสชั่น")]
        public DateTime? CancelApproveDate { get; set; }
        [Description("ผู้ยกเลิกอนุมัติคอมมิสชั่น")]
        public Guid? CancelApproveUserBy { get; set; }
        [ForeignKey("CancelApproveUserBy")]
        public USR.User CancelApproveUser { get; set; }
    }
}
