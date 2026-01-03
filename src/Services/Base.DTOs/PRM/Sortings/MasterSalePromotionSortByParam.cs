using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MasterSalePromotionSortByParam
    {
        public MasterSalePromotionSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterSalePromotionSortBy
    {
        PromotionNo,
        Name,
        Project,
        StartDate,
        EndDate,
        CashDiscount,
        FGFDiscount,
        TransferDiscount,
        PromotionStatus,
        IsUsed,
        Updated,
        UpdatedBy
    }
}
