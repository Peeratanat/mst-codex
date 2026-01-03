using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRJ
{
    [Description("Item ที่จะ Sync ข้อมูล Budget Promotion เข้า SAP")]
    [Table("BudgetPromotionSyncItem", Schema = Schema.PROJECT)]
    public class BudgetPromotionSyncItem : BaseEntity
    {
        public Guid? SaleBudgetPromotionID { get; set; }
        [ForeignKey("SaleBudgetPromotionID")]
        public BudgetPromotion SaleBudgetPromotion { get; set; }
        public Guid? TransferBudgetPromotionID { get; set; }
        [ForeignKey("TransferBudgetPromotionID")]
        public BudgetPromotion TransferBudgetPromotion { get; set; }

        public Guid? BudgetPromotionSyncStatusMasterCenterID { get; set; }
        [ForeignKey("BudgetPromotionSyncStatusMasterCenterID")]
        public MST.MasterCenter BudgetPromotionSyncStatus { get; set; }

        public Guid? BudgetPromotionSyncJobID { get; set; }
        [ForeignKey("BudgetPromotionSyncJobID")]
        public BudgetPromotionSyncJob BudgetPromotionSyncJob { get; set; }

        [Description("ครั้งที่ Retry")]
        public int Retry { get; set; }

        [Description("User Name From Web")]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Description("Object number (forecast) กิ่ง P")]
        [MaxLength(100)]
        public string SAPWBSObject_P { get; set; }
        [Description("Amount รวม Budget Pro โอน และ ขาย (ทศนิยม 2 ตำแหน่ง)")]
        public decimal Amount { get; set; }
        [Description("Currency")]
        [MaxLength(50)]
        public string Currency { get; set; }

    }
}
