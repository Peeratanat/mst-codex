using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MasterSalePromotionCreditCardItemSortByParam
    {
        public MasterSalePromotionCreditCardItemSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterSalePromotionCreditCardItemSortBy
    {
        Bank,
        NameTH,
        NameEN,
        Fee,
        UnitTH,
        UnitEN,
        PromotionItemStatus,
        Quantity,
        Updated,
        UpdatedBy
    }
}
