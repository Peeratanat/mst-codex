using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Database.Models.CMS
{
    [Description("คำนวณ Commission โอนโครงการแนวราบ")]
    [Table("CalculateHighRiseTransfer", Schema = Schema.COMMISSION)]
    public class CalculateHighRiseTransfer : BaseEntity
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

        [Description("พนักงานขายปิดการโอน")]
        public Guid? LCTransferID { get; set; }
        [ForeignKey("LCTransferID")]
        public USR.User LCTransfer { get; set; }
        [Description("ค่าคอมมิสชั่นได้รับตอนโอนของพนักงานขายปิดการโอน")]
        [Column(TypeName = "Money")]
        public decimal? LCTransferPaid { get; set; }

        [Description("ผลรวมค่าคอมมิสชั่นทั้งหมด")]
        [Column(TypeName = "Money")]
        public decimal? TotalCommissionPaid { get; set; }
    }
}
