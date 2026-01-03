using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class MasterCenterGroupSortByParam
    {
        public MasterCenterGroupSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterCenterGroupSortBy
    {
        Name,
        Key,
        Updated,
        UpdatedBy
    }
}
