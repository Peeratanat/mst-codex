using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class BudgetMinPriceSortByParam
    {
        public BudgetMinPriceSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BudgetMinPriceSortBy
    {
        UnitNo,
        Amount,
        Status,
        Updated,
        UpdatedBy
    }
}
