using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CalculatePerMonthHighRiseTransferSortByParam
    {
        public CalculatePerMonthHighRiseTransferSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CalculatePerMonthHighRiseTransferSortBy
    {
        PeriodMonth,
        CalculateUserName,
        CalculateDate,
        Status,
        CommissionType
    }
}
