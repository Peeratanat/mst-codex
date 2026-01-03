using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("ข้อมสัญญาสำหรับคำนวณคอมมิสชั่น")]
    [Table("CommissionTransfer", Schema = Schema.COMMISSION)]
    public class CommissionTransfer : BaseEntity
    {
        [Description("เดือนคำนวณคอมมิสชั่น")]
        public int PeriodMonth { get; set; }
        [Description("ปีคำนวณคอมมิสชั่น")]
        public int PeriodYear { get; set; }

        [Description("เลขที่โอน")]
        public Guid? TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }

        [Description("รหัสโครงการ")]
        public string ProjectNo { get; set; }
        [Description("รหัสแปลง/ห้อง")]
        public string UnitNo { get; set; }
        [Description("วันที่โอนจริง")]
        public DateTime? TransferDate { get; set; }
        [Description("วันที่อนุมัติเซ็นสัญญาของนิติกรรม")]
        public DateTime? SignContractApproveDate { get; set; }
       
        [Description("ราคาขายหักส่วนลดเงินสด")]
        public decimal SellingPrice { get; set; }
        [Description("ส่วนลด ณ วันโอน")]
        public decimal? TransferDiscount { get; set; }
        [Description("เงินฟรีดาวน์")]
        public decimal? FreeDownAmount { get; set; }
        [Description("ราคาขายสุทธิ")]
        public decimal? NetSellPrice { get; set; }

        [Description("พนักงานโอน")]
        public Guid? LCTransferID { get; set; }
        [ForeignKey("LCTransferID")]
        public USR.User LCTransfer { get; set; }

        [Description("เป็นการโอนลอย")]
        public bool? IsPreTransfer { get; set; }

        [Description("เกิน 45 วัน")]
        public bool? IsOver { get; set; }
    }
}
