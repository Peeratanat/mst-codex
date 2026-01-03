using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MasterTransferCreditCardItemSortByParam
    {
        public MasterTransferCreditCardItemSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterTransferCreditCardItemSortBy
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
