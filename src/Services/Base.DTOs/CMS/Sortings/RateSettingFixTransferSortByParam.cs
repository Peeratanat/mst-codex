using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class RateSettingFixTransferSortByParam
    {
        public RateSettingFixTransferSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum RateSettingFixTransferSortBy
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
