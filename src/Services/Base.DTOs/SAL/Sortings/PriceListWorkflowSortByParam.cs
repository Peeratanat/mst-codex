using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL.Sortings
{
    public class PriceListWorkflowSortByParam
    {
        public PriceListWorkflowSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum PriceListWorkflowSortBy
    {
        Project,
        UnitNo,
        UnitStatus,
        PriceListWorkflowStage,
    }
}
