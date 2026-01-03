using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class RateSettingTransferSortByParam
    {
        public RateSettingTransferSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum RateSettingTransferSortBy
    {
        ProjectID,
        ActiveDate,
        StartRange,
        EndRange,
        Amount,
        IsActive,
        CreateDate,
        CreatedByName
    }
}
