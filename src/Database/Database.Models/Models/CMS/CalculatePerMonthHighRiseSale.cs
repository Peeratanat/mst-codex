using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("คำนวณค่า Commission ขายประจำเดือนโครงการแนวสูง")]
    [Table("CalculatePerMonthHighRiseSale", Schema = Schema.COMMISSION)]
    public class CalculatePerMonthHighRiseSale : BaseEntity
    {
        [Description("เดือนคำนวณคอมมิสชั่น")]
        public int PeriodMonth { get; set; }
        [Description("ปีคำนวณคอมมิสชั่น")]
        public int PeriodYear { get; set; }

        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

    
        [Description("ผลรวมมูลค่าสัญญาที่ขายได้ในเดือนคอมมิสชั่น")]
        [Column(TypeName = "Money")]
        public decimal? TotalContractAmount { get; set; }
        [Description("ผลรวมจำนวนแปลงที่ขายได้ในเดือนคอมมิสชั่น")]
        public int? TotalContractUnit { get; set; }
        [Description("ผลรวมมูลค่าสัญญาที่ยกเลิกในเดือนคอมมิสชั่น")]
        [Column(TypeName = "Money")]
        public decimal? TotalContractCancelAmount { get; set; }
        [Description("ผลรวมจำนวนแปลงที่ยกเลิกในเดือนคอมมิสชั่น")]
        public int? TotalContractCancelUnit { get; set; }
        [Description("ผลรวมมูลค่าสัญญาสุทธิในเดือนคอมมิสชั่น (ขายได้-ยกเลิก)")]
        [Column(TypeName = "Money")]
        public decimal? TotalContractNetAmount { get; set; }
        [Description("ผลรวมจำนวนแปลงสุทธิในเดือนคอมมิสชั่น (ขายได้-ยกเลิก)")]
        public int? TotalContractNetUnit { get; set; }


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
