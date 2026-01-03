using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class RateSettingFixSaleSortByParam
    {
        public RateSettingFixSaleSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum RateSettingFixSaleSortBy
    {
        ProjectID,
        ActiveDate,
        ExpireDate,
        Amount,
        IsActive,
        CreateDate,
        CreatedByName
    }
}
