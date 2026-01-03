using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("ข้อมสัญญาสำหรับคำนวณคอมมิสชั่น")]
    [Table("CommissionContract", Schema = Schema.COMMISSION)]
    public class CommissionContract : BaseEntity
    {
        [Description("เดือนคำนวณคอมมิสชั่น")]
        public int PeriodMonth { get; set; }
        [Description("ปีคำนวณคอมมิสชั่น")]
        public int PeriodYear { get; set; }

        [Description("เลขที่สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("รหัสโครงการ")]
        public string ProjectNo { get; set; }
        [Description("รหัสแปลง/ห้อง")]
        public string UnitNo { get; set; }
        [Description("วันที่จอง")]
        public DateTime BookingDate { get; set; }
        [Description("วันที่ทำสัญญา")]
        public DateTime ContractDate { get; set; }
        [Description("วันที่อนุมัติสัญญา")]
        public DateTime? ApproveDate { get; set; }
        [Description("วันที่อนุมัติเซ็นสัญญาของนิติกรรม")]
        public DateTime? SignContractApproveDate { get; set; }
        [Description("วันที่รับเงินค่าทำสัญญา")]
        public DateTime? ReceiptDate { get; set; }
        [Description("เงินค่าทำสัญญา")]
        public decimal? ContractAmount { get; set; }       
        [Description("รับเงินค่าทำสัญญา")]
        [Column(TypeName = "Money")]
        public decimal? ContractPaidAmount { get; set; }       
        [Description("ราคาขายหักส่วนลดเงินสด")]
        [Column(TypeName = "Money")]
        public decimal SellingPrice { get; set; }
        [Description("ส่วนลด ณ วันโอน")]
        [Column(TypeName = "Money")]
        public decimal? TransferDiscount { get; set; }
        [Description("เงินฟรีดาวน์")]
        [Column(TypeName = "Money")]
        public decimal? FreeDownAmount { get; set; }

        [Description("ประเภทการยกเลิก")]
        public string CancelType { get; set; }
        [Description("วันที่ยกเลิก")]
        public DateTime? CancelDate { get; set; }

        [Description("ประเภทพนักงานปิดการขาย")]
        public Guid? SaleOfficerTypeMasterCenterID { get; set; }
        [ForeignKey("SaleOfficerTypeMasterCenterID")]
        public MST.MasterCenter SaleOfficerType { get; set; }

        [Description("Agency")]
        public Guid? AgentID { get; set; }
        [ForeignKey("AgentID")]
        public MST.Agent Agent { get; set; }

        [Description("พนักงานขายปิดการขายของAgency")]
        public Guid? AgentEmployeeID { get; set; }
        [ForeignKey("AgentEmployeeID")]
        public MST.AgentEmployee AgentEmployee { get; set; }

        [Description("พนักงานขายปิดการขาย")]
        public Guid? SaleUserID { get; set; }
        [ForeignKey("SaleUserID")]
        public USR.User SaleUser { get; set; }

        [Description("พนักงานประจำโครงการ")]
        public Guid? ProjectSaleUserID { get; set; }
        [ForeignKey("ProjectSaleUserID")]
        public USR.User ProjectSaleUser { get; set; }

        [Description("เลขที่สัญญา (เดิม)")]
        public Guid? AgreementIDReferent { get; set; }
        [ForeignKey("AgreementIDReferent")]
        public SAL.Agreement AgreementReferent { get; set; }

        [Description("ราคาขายหักส่วนลด (เดิม)")]
        [Column(TypeName = "Money")]
        public decimal? SellingPriceReferent { get; set; }
        [Description("ส่วนลด ณ วันโอน (เดิม)")]
        [Column(TypeName = "Money")]
        public decimal? TransferDiscountReferent { get; set; }

        [Description("ประเภทเวลาคำนวนณ")]
        public int CalTimeType { get; set; }

        [Description("เกิน 45 วัน")]
        public bool? IsOver { get; set; }

        [Description("คำนวน")]
        public bool? IsCalculate { get; set; }

        [Description("วันที่โอนจริง")]
        public DateTime? TransferDate { get; set; }
    }
}
