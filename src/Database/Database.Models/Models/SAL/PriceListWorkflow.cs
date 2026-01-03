using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("Workflow การขออนุมัติ Price List")]
    [Table("PriceListWorkflow", Schema = Schema.SALE)]
    public class PriceListWorkflow : BaseEntity
    {
        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
        [Description("แปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("ใบเสนอราคา")]
        public Guid? QuotationID { get; set; }
        [ForeignKey("QuotationID")]
        public SAL.Quotation Quotation { get; set; }

        [Description("ใบจอง")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("Flow ย้ายแปลง")]
        public Guid? ChangeUnitWorkflowID { get; set; }
        [ForeignKey("ChangeUnitWorkflowID")]
        public SAL.ChangeUnitWorkflow ChangeUnitWorkflow { get; set; }

        [Description("สถานะแปลง")]
        public Guid? UnitStatusMasterCenterID { get; set; }
        [ForeignKey("UnitStatusMasterCenterID")]
        public MST.MasterCenter UnitStatus { get; set; }
        [Description("ขั้นตอน")]
        public Guid? PriceListWorkflowStageMasterCenterID { get; set; }
        [ForeignKey("PriceListWorkflowStageMasterCenterID")]
        public MST.MasterCenter PriceListWorkflowStage { get; set; }

        [Description("ราคาขายเดิม")]
        [Column(TypeName = "Money")]
        public decimal? MasterSellingPrice { get; set; }
        [Description("เงินจองเดิม")]
        [Column(TypeName = "Money")]
        public decimal? MasterBookingAmount { get; set; }
        [Description("เงินสัญญาเดิม")]
        [Column(TypeName = "Money")]
        public decimal? MasterContractAmount { get; set; }
        [Description("จำนวนงวดดาวน์เดิม")]
        [Column(TypeName = "Money")]
        public int? MasterInstallment { get; set; }
        [Description("จำนวนงวดดาวน์ปกติเดิม")]
        [Column(TypeName = "Money")]
        public int? MasterNormalInstallment { get; set; }
        [Description("ราคางวดดาวน์ปกติเดิม")]
        [Column(TypeName = "Money")]
        public decimal? MasterInstallmentAmount { get; set; }
        [Description("งวดพิเศษเดิม (eg. 1,2,10)")]
        [MaxLength(1000)]
        public string MasterSpecialInstallments { get; set; }
        [Description("ราคางวดพิเศษเดิม (eg. 1000.00,2000.00,3000)")]
        [MaxLength(1000)]
        public string MasterSpecialInstallmentAmounts { get; set; }

        [Description("ราคาขาย")]
        [Column(TypeName = "Money")]
        public decimal? SellingPrice { get; set; }
        [Description("เงินจอง")]
        [Column(TypeName = "Money")]
        public decimal? BookingAmount { get; set; }
        [Description("เงินสัญญา")]
        [Column(TypeName = "Money")]
        public decimal? ContractAmount { get; set; }
        [Description("จำนวนงวดดาวน์")]
        [Column(TypeName = "Money")]
        public int? Installment { get; set; }
        [Description("จำนวนงวดดาวน์ปกติ")]
        [Column(TypeName = "Money")]
        public int? NormalInstallment { get; set; }
        [Description("ราคางวดดาวน์ปกติ")]
        [Column(TypeName = "Money")]
        public decimal? InstallmentAmount { get; set; }
        [Description("งวดพิเศษ (eg. 1,2,10)")]
        [MaxLength(1000)]
        public string SpecialInstallments { get; set; }
        [Description("ราคางวดพิเศษ (eg. 1000.00,2000.00,3000)")]
        [MaxLength(1000)]
        public string SpecialInstallmentAmounts { get; set; }

        [Description("ผลอนุมัติ")]
        public bool? IsApproved { get; set; }
        [Description("ตำแหน่งของผู้อนุมัติ")]
        public Guid? RoleID { get; set; }
        [ForeignKey("RoleID")]
        public USR.Role Role { get; set; }
        [Description("ผู้อนุมัติ")]
        public Guid? ApprovedByUserID { get; set; }
        [ForeignKey("ApprovedByUserID")]
        public USR.User ApprovedBy { get; set; }
        [Description("เวลาที่อนุมัติ")]
        public DateTime? ApprovedTime { get; set; }

        [Description("เหตุผลการ Reject")]
        [MaxLength(5000)]
        public string RejectComment { get; set; }
    }
}
