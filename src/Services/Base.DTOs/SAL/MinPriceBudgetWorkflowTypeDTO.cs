using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class MinPriceBudgetWorkflowTypeDTO
    {
        /// <summary>
        /// Type ของ MinPrice flow (eg. Adhog > 5%)
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceWorkflowType { get; set; }
        /// <summary>
        /// สถานะ MinPrice Workflow (true = เข้าเงื่อนไข Workflow)
        /// </summary>
        public bool? IsMinPriceWorkflow { get; set; }
        /// <summary>
        /// สถานะ BudgetPromotion Workflow (true = เข้าเงื่อนไข Workflow)
        /// </summary>
        public bool? IsBudgetPromotionWorkflow { get; set; }
        /// <summary>
        /// ราคาบ้านในสัญญา
        /// </summary>
        public decimal AgreementPrice { get; set; }
        /// <summary>
        /// ส่วนลดสัญญา
        /// </summary>
        public decimal PromotionAmtAG { get; set; }
        /// <summary>
        /// ส่วนลดโปรโอน
        /// </summary>
        public decimal PromotionAmtTP { get; set; }
        /// <summary>
        /// ราคาขาย หัก ส่วนลด (สัญญา,โปรโอน) => (AgreementPrice - (PromotionAmtAG + PromotionAmtTP))
        /// </summary>
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// MinPrice
        /// </summary>
        public decimal MinPrice { get; set; }
        /// <summary>
        /// TotalMinprice (SellingPrice - MinPrice)
        /// </summary>
        public decimal TotalMinprice { get; set; }
    }
}
