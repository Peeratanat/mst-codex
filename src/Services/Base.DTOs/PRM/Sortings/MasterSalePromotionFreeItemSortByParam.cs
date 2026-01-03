using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MasterSalePromotionFreeItemSortByParam
    {
        public MasterSalePromotionFreeItemSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterSalePromotionFreeItemSortBy
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
