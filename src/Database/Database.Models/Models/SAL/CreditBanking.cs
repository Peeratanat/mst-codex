using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ขอสินเชื่อกับธนาคาร")]
    [Table("CreditBanking", Schema = Schema.SALE)]
    public class CreditBanking : BaseEntity
    {
        [Description("ใบจอง")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("สถาบันการเงิน")]
        public Guid? FinancialInstitutionMasterCenterID { get; set; }
        [ForeignKey("FinancialInstitutionMasterCenterID")]
        public MST.MasterCenter FinancialInstitution { get; set; }

        [Description("ธนาคาร")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("จังหวัด")]
        public Guid? ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public MST.Province Province { get; set; }

        [Description("สาขา")]
        public Guid? BankBranchID { get; set; }
        [ForeignKey("BankBranchID")]
        public MST.BankBranch BankBranch { get; set; }

        [Description("ธนาคารอื่นๆ")]
        [MaxLength(1000)]
        public string OtherBank { get; set; }

        [Description("สาขาอื่นๆ")]
        [MaxLength(1000)]
        public string OtherBankBranch { get; set; }

        [Description("วันที่ขอสินเชื่อ")]
        public DateTime? LoanSubmitDate { get; set; }

        [Description("ยอดขอกู้")]
        [Column(TypeName = "Money")]
        public decimal LoanAmount { get; set; }
        [Description("ยอดอนุมัติ AP")]
        [Column(TypeName = "Money")]
        public decimal ApprovedLoanAPAmount { get; set; }
        [Description("เบี้ยประกัน")]
        [Column(TypeName = "Money")]
        public decimal InsuranceAmount { get; set; }
        [Description("เบี้ยประกันอัคคีภัย")]
        [Column(TypeName = "Money")]
        public decimal InsuranceOnFireAmount { get; set; }
        [Description("เงินหักล่วงหน้างวดแรก")]
        [Column(TypeName = "Money")]
        public decimal FirstDeductAmount { get; set; }
        [Description("เงินคืนลูกค้า")]
        [Column(TypeName = "Money")]
        public decimal ReturnCustomerAmount { get; set; }
        [Description("ยอดอนุมัติรวม (ธนาคาร)")]
        [Column(TypeName = "Money")]
        public decimal ApprovedAmount { get; set; }

        [Description("สถานะสินเชื่อ")]
        public Guid? LoanStatusMasterCenterID { get; set; }
        [ForeignKey("LoanStatusMasterCenterID")]
        public MST.MasterCenter LoanStatus { get; set; }
        [Description("วันที่ทราบผล")]
        public DateTime? ResultDate { get; set; }

        [Description("สถานะการเลือกใช้ธนาคาร")]
        public bool? IsUseBank { get; set; }
        [Description("เหตุผลธนาคาร")]
        public Guid? BankReasonMasterCenterID { get; set; }
        [ForeignKey("BankReasonMasterCenterID")]
        public MST.MasterCenter BankReason { get; set; }
        [Description("เหตุผลการเลือกใช้ธนาคาร")]
        public Guid? UseBankReasonMasterCenterID { get; set; }
        [ForeignKey("UseBankReasonMasterCenterID")]
        public MST.MasterCenter UseBankReason { get; set; }
        [Description("เหตุผลการเลือกใช้ธนาคารอื่นๆ")]
        [MaxLength(5000)]
        public string UseBankOtherReason { get; set; }
        [Description("เหตุผลการเลือกไม่ใช้ธนาคาร")]
        public Guid? NotUseBankReasonMasterCenterID { get; set; }
        [ForeignKey("NotUseBankReasonMasterCenterID")]
        public MST.MasterCenter NotUseBankReason { get; set; }
        [Description("เหตุผลการเลือกไม่ใช้ธนาคารอื่นๆ")]
        [MaxLength(5000)]
        public string NotUseBankOtherReason { get; set; }
        [Description("เหตุผลการปฏิเสธสถานะสินเชื่อ")]
        public Guid? BankRejectReasonMasterCenterID { get; set; }
        [ForeignKey("BankRejectReasonMasterCenterID")]
        public MST.MasterCenter BankRejectReason { get; set; }
        [Description("เหตุผลการรอผลสถานะสินเชื่อ")]
        public Guid? BankWaitingReasonMasterCenterID { get; set; }
        [ForeignKey("BankWaitingReasonMasterCenterID")]
        public MST.MasterCenter BankWaitingReason { get; set; }
        [Description("เหตุผลการรอผลสถานะสินเชื่ออื่นๆ")]
        [MaxLength(5000)]
        public string BankWaitingOtherReason { get; set; }


        public DateTime? DocumentCompletetDate { get;set; }

    }
}
