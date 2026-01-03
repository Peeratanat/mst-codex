using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqWorkFlowMinPriceState
    {
        public Guid ID { get; set; }
        public Guid MinPriceBudgetWorkflowID { get; set; }
        public Guid BookingID { get; set; }
        public string BookingNo { get; set; }
        public Guid UnitID { get; set; }
        public Guid MinPriceWorkflowTypeMasterCenterID { get; set; }
        public string TypeName { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal RequestMinPrice { get; set; }
        public decimal RequestBudgetPromotion { get; set; }
        public string RoleCode { get; set; }
        public int Order { get; set; }
        public string StatusApprove { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string DisplayName { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedByUserID { get; set; }
        
    }
}
