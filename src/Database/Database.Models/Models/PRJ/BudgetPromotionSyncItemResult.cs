using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRJ
{
    [Description("ผลการ Sync Budget Promotion จาก SAP")]
    [Table("BudgetPromotionSyncItemResult", Schema = Schema.PROJECT)]
    public class BudgetPromotionSyncItemResult : BaseEntity
    {
        public Guid? BudgetPromotionSyncItemID { get; set; }
        [ForeignKey("BudgetPromotionSyncItemID")]
        public BudgetPromotionSyncItem BudgetPromotionSyncItem { get; set; }

        [Description("Flag Error")]
        public bool IsError { get; set; }
        [Description("Error Code")]
        [MaxLength(10)]
        public string ErrorCode { get; set; }
        [Description("Error Description")]
        [MaxLength(100)]
        public string ErrorDescription { get; set; }
        [Description("Flag ว่าเรียก FM Update Budget แล้ว")]
        public bool IsFMUpdateBudget { get; set; }
        [Description("Object number (forecast) กิ่ง P")]
        [MaxLength(100)]
        public string SAPWBSObject_P { get; set; }
        [Description("Last Update Budget from SAP")]
        [MaxLength(20)]
        public string LastUpdateBudgetFromSAP { get; set; }
        [Description("User SAP")]
        [MaxLength(40)]
        public string UserSAP { get; set; }
        [Description("SAP Create Date/Time")]
        public DateTime SAPCreateDateTime { get; set; }

    }
}
