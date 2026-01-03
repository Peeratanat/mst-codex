using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class FETSortUnitListByParam
    {
        public FETSortUnitListBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum FETSortUnitListBy
    {
        UnitNo,
        ContactName,
        CountFET,
        SumFETAmount

    }
}
