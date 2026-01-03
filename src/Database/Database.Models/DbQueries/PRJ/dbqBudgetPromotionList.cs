using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.PRJ
{
    public class dbqBudgetPromotionList : BaseDbQueries
    {
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public string SAPWBSNo { get; set; }
        public string SAPWBSNo_P { get; set; }
        public string SAPWBSObject { get; set; }
        public string SAPWBSObject_P { get; set; }
        public double? SaleArea { get; set; }
        public decimal? BudgetPromotionSale { get; set; }
        public decimal? BudgetPromotionTransfer { get; set; }
        public decimal? BudgetPromotionTotal { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedByDisplayName { get; set; }
        public Guid? BudgetPromotionSaleID { get; set; }
        public Guid? BudgetPromotionTransferID { get; set; }
        public Guid? BudgetPromotionSyncItemID { get; set; }
        //public Guid? BudgetPromotionSyncJobID { get; set; }
        public string BudgetPromotionSyncStatusKey { get; set; }
        public string BudgetPromotionSyncStatusName { get; set; }
        public Guid? UnitStatusID { get; set; }
        public DateTime? ActualTransferDate { get; set; }
        public bool? IsAccountApproved { get; set; }
    }
}
