using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CalculatePerMonthHighRiseTransferDetailSortByParam
    {
        public CalculatePerMonthHighRiseTransferDetailSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CalculatePerMonthHighRiseTransferDetailSortBy
    {
        UnitNo,
        LCCloseBy,
        LCProjectBy,
        PerRate,
        CommissionType,
        AgreementAmount,
        ActualTransferDate,
        CommissionTransferAmount,
        CommissionPayAmount
    }
}
