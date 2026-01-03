using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class BudgetPromotionSortByParam
    {
        public BudgetPromotionSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BudgetPromotionSortBy
    {
        Unit_UnitNo,
        Unit_HouseNo,
        Unit_SapwbsObject,
        Unit_SapwbsObject_P,
        Unit_SapwbsNo,
        Unit_SapwbsNo_P,
        Unit_SaleArea,
        PromotionPrice,
        PromotionTransferPrice,
        SyncJob_Status,
        TotalPrice,
        UpdatedBy,
        Updated
    }
}
