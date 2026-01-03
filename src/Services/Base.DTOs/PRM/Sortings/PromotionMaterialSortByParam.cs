using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class PromotionMaterialSortByParam
    {
        public PromotionMaterialSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum PromotionMaterialSortBy
    {
        AgreementNo,
        ItemNo,
        Plant,
        NameTH,
        NameEN,
        MaterialCode,
        Price,
        Unit,
        ExpireDate,
        Updated,
        UpdatedBy
    }
}
