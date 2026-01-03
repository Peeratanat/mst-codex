using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.ACC
{
    public class SAPUnitStatusSortByParam
    {
        public SAPUnitStatusSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }        
    }
    public enum SAPUnitStatusSortBy
    {
        Project,
        ProcessNow,
        ProcessLast
    }
}
