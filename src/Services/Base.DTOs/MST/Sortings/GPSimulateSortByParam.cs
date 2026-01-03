using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class GPSimulateSortByParam
    {
        public GPSimulateSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum GPSimulateSortBy
    {
        Version,
        Remark,
        Project,
        Created,
        Updated,
    }
}
