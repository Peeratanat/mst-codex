using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นขาย")]
    [Table("SalePromotion", Schema = Schema.PROMOTION)]
    public class SalePromotion : BaseEntity
    {
        [Description("ใบจอง")]
        public Guid BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("โปรโมชั่น")]
        public Guid? MasterSalePromotionID { get; set; }
        [ForeignKey("MasterSalePromotionID")]
        public MasterSalePromotion MasterPromotion { get; set; }

        [Description("เลขที่โปรโมชั่นขาย")]
        [MaxLength(100)]
        public string SalePromotionNo { get; set; }

        [Description("โอนกรรมสิทธิ์ภายในวันที่...")]
        public DateTime? TransferDateBefore { get; set; }
        [Description("รวมมูลค่าโปรโมชั่น")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }
        [Description("ส่วนลดวันโอน")]
        [Column(TypeName = "Money")]
        public decimal? TransferDiscount { get; set; }
        [Description("ส่วนลดหน้าสัญญา")]
        [Column(TypeName = "Money")]
        public decimal? ContractDiscount { get; set; }
        [Description("ส่วนลด FGF")]
        [Column(TypeName = "Money")]
        public decimal? FGFDiscount { get; set; }
        [Description("รวมใช้ Budget Promotion")]
        [Column(TypeName = "Money")]
        public decimal BudgetAmount { get; set; }
        [Description("ผู้แนะนำ")]
        public Guid? PresentByUserID { get; set; }
        [ForeignKey("PresentByUserID")]
        public USR.User PresentByUser { get; set; }

        [Description("ขั้นตอนของโปรขาย (จอง, ทำสัญญา)")]
        public Guid? SalePromotionStageMasterCenterID { get; set; }
        [ForeignKey("SalePromotionStageMasterCenterID")]
        public MST.MasterCenter SalePromotionStage { get; set; }
        [Description("Active หรือไม่")]
        public bool IsActive { get; set; }
        [Description("Flow เปลี่ยนแปลงโปร")]
        public Guid? ChangePromotionWorkflowID { get; set; }
        [ForeignKey("ChangePromotionWorkflowID")]
        public ChangePromotionWorkflow ChangePromotionWorkflow { get; set; }

        [Description("โปรโมชั่นเก่าก่อนเปลี่ยนแปลงโปร")]
        public Guid? FromSalePromotionID { get; set; }
        [ForeignKey("FromSalePromotionID")]
        public SalePromotion FromSalePromotion { get; set; }

        [Description("UnitPrice เก่า")]
        public Guid? FromUnitPriceID { get; set; }
        [ForeignKey("FromUnitPriceID")]
        public UnitPrice FromUnitPrice { get; set; }

        [Description("UnitPrice ใหม่")]
        public Guid? ToUnitPriceID { get; set; }
        [ForeignKey("ToUnitPriceID")]
        public UnitPrice ToUnitPrice { get; set; }
    }
}
