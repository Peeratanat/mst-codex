using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class RateSettingSaleSortByParam
    {
        public RateSettingSaleSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum RateSettingSaleSortBy
    {
        ProjectID,
        ActiveDate,
        //StartRange,
        //EndRange,
        //Amount,
        IsActive,
        CreateDate,
        CreatedByName,
        ExpireDate
    }
}
