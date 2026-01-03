using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class FETSortProjectListByParam
    {
        public FETSortProjectListBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum FETSortProjectListBy
    {
        Project,
        countFET,
        countUnit,
        countAmountFET
    }
}
