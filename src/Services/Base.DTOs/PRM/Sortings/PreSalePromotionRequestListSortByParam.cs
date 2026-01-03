using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class PreSalePromotionRequestListSortByParam
    {
        public PreSalePromotionRequestListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum PreSalePromotionRequestListSortBy
    {
        Project,
        Unit,
        MasterPreSalePromotions_PromotionNo,
        PRCompletedDate,
        PromotionRequestPRStatus,
        RequestDate,
        Updated,
        UpdatedBy
    }
}
