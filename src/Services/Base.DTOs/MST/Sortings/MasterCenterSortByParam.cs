using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class MasterCenterSortByParam
    {
        public MasterCenterSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterCenterSortBy
    {
        Name,
        NameEN,
        Key,
        MasterCenterGroup,
        Order,
        IsActive,
        Updated,
        UpdatedBy
    }
}
