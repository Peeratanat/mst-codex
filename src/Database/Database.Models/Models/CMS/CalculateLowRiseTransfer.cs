using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace Database.Models.CMS
{
    [Description("คำนวณ Commission โอนโครงการแนวราบ")]
    [Table("CalculateLowRiseTransfer", Schema = Schema.COMMISSION)]
    public class CalculateLowRiseTransfer : BaseEntity
    {
        [Description("ปีคำนวณคอมมิสชั่น")]
        public int PeriodYear { get; set; }
        [Description("เดือนคำนวณคอมมิสชั่น")]
        public int PeriodMonth { get; set; }

        [Description("เลขที่โอน")]
        public Guid? TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }

        [Description("รหัสโครงการ")]
        public string ProjectNo { get; set; }
        [Description("รหัสแปลง/ห้อง")]
        public string UnitNo { get; set; }

        [Description("ประเภท Rate Commission (Fix/NoFix)")]
        public Guid? CommissionPercentTypeMasterCenterID { get; set; }
        [ForeignKey("CommissionPercentTypeMasterCenterID")]
        public MST.MasterCenter CommissionPercentType { get; set; }
        [Description("อัตรา % Commission")]
        [Column(TypeName = "Money")]
        public decimal? CommissionPercentRate { get; set; }
        [Description("ค่าคอมมิสชั่น Fix ตามแบบบ้านโอน")]
        [Column(TypeName = "Money")]
        public decimal? RateFixTransferModelAmount { get; set; }

        [Description("พนักงานขายปิดการขาย (โอน)")]
        public Guid? SaleUserID { get; set; }
        [ForeignKey("SaleUserID")]
        public USR.User SaleUser { get; set; }
        [Description("ค่าคอมมิสชั่นได้รับตอนโอนของพนักงานขายปิดการขาย (โอน)")]
        [Column(TypeName = "Money")]
        public decimal? SaleUserSalePaid { get; set; }

        [Description("พนักงานประจำโครงการ (โอน)")]
        public Guid? ProjectSaleUserID { get; set; }
        [ForeignKey("ProjectSaleUserID")]
        public USR.User ProjectSaleUser { get; set; }
        [Description("ค่าคอมมิสชั่นได้รับตอนโอนของพนักงานประจำโครงการ (โอน)")]
        [Column(TypeName = "Money")]
        public decimal? ProjectSaleSalePaid { get; set; }

        [Description("ผลรวมค่าคอมมิสชั่นทั้งหมด")]
        [Column(TypeName = "Money")]
        public decimal? TotalCommissionPaid { get; set; }
    }
}
