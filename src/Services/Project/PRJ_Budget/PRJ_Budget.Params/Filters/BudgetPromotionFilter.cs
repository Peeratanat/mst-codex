using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Budget.Params.Filters
{
    public class BudgetPromotionFilter : BaseFilter
    {
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public string WBSCRM_P { get; set; }
        public string WBSSAP_P { get; set; }
        /// <summary>
        /// พื้นที่ขาย (ผังขาย)
        /// </summary>
        public double? SaleAreaFrom { get; set; }
        /// <summary>
        /// พื้นที่ขาย (ผังขาย)
        /// </summary>
        public double? SaleAreaTo { get; set; }
        public decimal? PromotionPriceFrom { get; set; }
        public decimal? PromotionPriceTo { get; set; }
        public decimal? PromotionTransferPriceFrom { get; set; }
        public decimal? PromotionTransferPriceTo { get; set; }

        public decimal? TotalPriceFrom { get; set; }
        public decimal? TotalPriceTo { get; set; }
        public string SyncJob_StatusKey { get; set; }
    }
}
