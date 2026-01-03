using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class RateSettingFixTransferModelSortByParam
    {
        public RateSettingFixTransferModelSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum RateSettingFixTransferModelSortBy
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
