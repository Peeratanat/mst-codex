using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MasterPreSalePromotionSortByParam
    {
        public MasterPreSalePromotionSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterPreSalePromotionSortBy
    {
        PromotionNo,
        Name,
        Project,
        CompanyCode,
        PromotionStatus,
        IsUsed,
        IsApproved,
        ApprovedDate,
        Updated,
        UpdatedBy
    }
}
