using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CalculatePerMonthLowRiseSortByParam
    {
        public CalculatePerMonthLowRiseSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CalculatePerMonthLowRiseSortBy
    {
        PeriodMonth,
        CalculateUserName,
        CalculateDate,
        Status,
        CommissionType
    }
}
