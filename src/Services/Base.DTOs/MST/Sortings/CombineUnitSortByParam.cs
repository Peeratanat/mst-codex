using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class CombineUnitSortByParam
    {
        public CombineUnitSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CombineUnitSortBy
    {
        Project,
        Unit,
        UnitCombine,
        CombineDocType,
        CombineStatus,
        ApproveDate,
        ApproveBy,
        Updated,
        UpdatedBy
    }
}
