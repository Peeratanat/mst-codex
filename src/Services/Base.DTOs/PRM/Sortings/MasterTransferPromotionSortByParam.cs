using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MasterTransferPromotionSortByParam
    {
        public MasterTransferPromotionSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterTransferPromotionSortBy
    {
        PromotionNo,
        Name,
        Project,
        StartDate,
        EndDate,
        TransferDiscount,
        PromotionStatus,
        IsUsed,
        Updated,
        UpdatedBy
    }
}
