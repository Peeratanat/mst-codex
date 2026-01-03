using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MasterTransferPromotionFreeItemSortByParam
    {
        public MasterTransferPromotionFreeItemSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterTransferPromotionFreeItemSortBy
    {
        NameTH,
        NameEN,
        Quantity,
        UnitTH,
        UnitEN,
        ReceiveDays,
        WhenPromotionReceive,
        IsShowInContract,
        Updated,
        UpdatedBy
    }
}
