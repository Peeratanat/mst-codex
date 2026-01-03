using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("Flow อนุมัติ Min Price หรือ Budget Promotion")]
    [Table("MinPriceBudgetWorkflow", Schema = Schema.SALE)]
    public class MinPriceBudgetWorkflow : BaseEntity
    {
        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("ใบจอง")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public Booking Booking { get; set; }

        [Description("Flow เปลี่ยนแปลงโปร")]
        public Guid? ChangePromotionWorkflowID { get; set; }
        [ForeignKey("ChangePromotionWorkflowID")]
        public PRM.ChangePromotionWorkflow ChangePromotionWorkflow { get; set; }

        [Description("Flow ย้ายแปลง")]
        public Guid? ChangeUnitWorkflowID { get; set; }
        [ForeignKey("ChangeUnitWorkflowID")]
        public SAL.ChangeUnitWorkflow ChangeUnitWorkflow { get; set; }

        [Description("ขออนุมัติ Min Price")]
        public bool IsRequestMinPrice { get; set; }
        [Description("ขออนุมัติ Budget Promotion")]
        public bool IsRequestBudgetPromotion { get; set; }

        [Description("Stage (จอง, สัญญา)")]
        public Guid? MinPriceBudgetWorkflowStageMasterCenterID { get; set; }
        [ForeignKey("MinPriceBudgetWorkflowStageMasterCenterID")]
        public MST.MasterCenter MinPriceBudgetWorkflowStage { get; set; }

        [Description("ราคาขาย")]
        [Column(TypeName = "Money")]
        public decimal SellingPrice { get; set; }
        [Description("ส่วนลดหน้าสัญญา")]
        [Column(TypeName = "Money")]
        public decimal CashDiscount { get; set; }
        [Description("ส่วนลด ณ​ วัน โอน")]
        [Column(TypeName = "Money")]
        public decimal TransferDiscount { get; set; }

        [Description("Min Price จาก Master Data")]
        [Column(TypeName = "Money")]
        public decimal MasterMinPrice { get; set; }

        public Guid? FromMasterMinPriceID { get; set; }
        [ForeignKey("FromMasterMinPriceID")]
        public PRJ.MinPrice FromMasterMinPrice { get; set; }

        [Description("Min Price ที่ขอ Approve (SellingPrice - (RequestBudgetPromotion + CashDiscount + TransferDiscount))")]
        [Column(TypeName = "Money")]
        public decimal RequestMinPrice { get; set; }
        
        [Description("Type ของ MinPrice flow (eg. Admin > 5%)")]
        public Guid? MinPriceWorkflowTypeMasterCenterID { get; set; }
        [ForeignKey("MinPriceWorkflowTypeMasterCenterID")]
        public MST.MasterCenter MinPriceWorkflowType { get; set; }

        [Description("Budget Promotion จาก Master Data")]
        [Column(TypeName = "Money")]
        public decimal MasterBudgetPromotion { get; set; }
        public Guid? FromMasterBudgetPromotionID { get; set; }
        [ForeignKey("FromMasterBudgetPromotionID")]
        public PRJ.BudgetPromotion FromMasterBudgetPromotion { get; set; }

        [Description("Budget Promotion ที่ขอ Approve (โปรขาย = ส่วนลด FGF+ราคารวมของรายการโปรโมชั่นก่อนขาย+ราคารวมของรายการโปรโมชั่นขาย+ค่าใช้จ่ายที่ AP จ่ายแทนลูกค้า | โปรโอน = ราคารวมของรายการโปรโมชั่นโอน+ค่าใช้จ่ายที่ AP จ่ายแทนลูกค้า)")]
        [Column(TypeName = "Money")]
        public decimal RequestBudgetPromotion { get; set; }

        [Description("ชนิดของโปรโมชั่นที่ขอ")]
        public Guid? BudgetPromotionTypeMasterCenterID { get; set; }
        [ForeignKey("BudgetPromotionTypeMasterCenterID")]
        public MST.MasterCenter BudgetPromotionType { get; set; }

        public Guid? SalePromotionID { get; set; }
        [ForeignKey("SalePromotionID")]
        public PRM.SalePromotion SalePromotion { get; set; }
        public Guid? TransferPromotionID { get; set; }
        [ForeignKey("TransferPromotionID")]
        public PRM.TransferPromotion TransferPromotion { get; set; }


        [Description("เหตุผลการ Reject")]
        [MaxLength(5000)]
        public string RejectComment { get; set; }
        [Description("เวลาที่ Reject")]
        public DateTime? RejectedTime { get; set; }
        [Description("Reject โดย")]
        public Guid? RejectedByUserID { get; set; }
        [ForeignKey("RejectedByUserID")]
        public USR.User RejectedBy { get; set; }

        [Description("สถานะอนุมติ")]
        public bool? IsApproved { get; set; }

        [Description("แปลงถูกยกเลิก")]
        public bool IsCancelled { get; set; }

        [Description("มีการ Recall")]
        public bool IsRecalled { get; set; }
        [Description("เวลาที่ Recall")]
        public DateTime? RecalledTime { get; set; }
        [Description("Recall โดย")]
        public Guid? RecalledByUserID { get; set; }
        [ForeignKey("RecalledByUserID")]
        public USR.User RecalledBy { get; set; }
        [Description("เหตุผลการ Recall")]
        [MaxLength(1000)]
        public string RecallReason { get; set; }


        [Description("Budget MinPrice ที่ใช้")]
        public Guid? BudgetMinPriceID { get; set; }
        [ForeignKey("BudgetMinPriceID")]
        public PRJ.BudgetMinPrice BudgetMinPrice { get; set; }

        [Description("Budget MinPrice ของแปลง ที่ใช้")]
        public Guid? BudgetMinPriceUnitID { get; set; }
        [ForeignKey("BudgetMinPriceUnitID")]
        public PRJ.BudgetMinPriceUnit BudgetMinPriceUnit { get; set; }

        [Description("เหตุผลขออนุมัติ Min Price")]
        public Guid? MinPriceRequestReasonMasterCenterID { get; set; }
        [ForeignKey("MinPriceRequestReasonMasterCenterID")]
        public MST.MasterCenter MinPriceRequestReason { get; set; }

        [Description("เหตุผลขออนุมัติ Min Price อื่นๆ")]
        [MaxLength(1000)]
        public string OtherMinPriceRequestReason { get; set; }

        [Description("Budget รวม (ขายหรือโอน ขึ้นอยู่กับชนิดของ flow)")]
        [Column(TypeName = "Money")]
        public decimal TotalBudget { get; set; }
        [Description("Budget ที่ใช้ (MasterMinPrice - RequestMinPrice)")]
        [Column(TypeName = "Money")]
        public decimal UseBudget { get; set; }

    }
}
