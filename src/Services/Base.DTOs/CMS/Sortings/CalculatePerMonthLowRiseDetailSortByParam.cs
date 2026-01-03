using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CalculatePerMonthLowRiseDetailSortByParam
    {
        public CalculatePerMonthLowRiseDetailSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CalculatePerMonthLowRiseDetailSortBy
    {
        UnitNo,
        LCCloseBy,
        LCProjectBy,
        PerRate,
        CommissionType,
        AgreementAmount,
        ActualTransferDate,
        TransferCommissionCloseAmount,
        TransferCommissionProjectAmount,
        TransferCommissionSumAmount
    }
}
