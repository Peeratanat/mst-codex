using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CalculatePerMonthHighRiseSaleSortByParam
    {
        public CalculatePerMonthHighRiseSaleSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CalculatePerMonthHighRiseSaleSortBy
    {
        PeriodMonth,
        CalculateUserName,
        CalculateDate,
        Status,
        CommissionType
    }
}
