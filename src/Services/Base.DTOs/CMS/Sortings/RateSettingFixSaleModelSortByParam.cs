using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class RateSettingFixSaleModelSortByParam
    {
        public RateSettingFixSaleModelSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum RateSettingFixSaleModelSortBy
    {
        ProjectID,
        ActiveDate,
        ExpireDate,
        ModelID,
        Amount,
        IsActive,
        CreateDate,
        CreatedByName
    }
}
