using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class GeneralSettingSortByParam
    {
        public GeneralSettingSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum GeneralSettingSortBy
    {
        ActiveDate,
        ProjectID,
        IsActive,
        CreateDate,
        CreatedByName,
        LaunchStartDate,
        LaunchEndDate
    }
}
