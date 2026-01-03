using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CalculatePerMonthHighRiseSaleDetailSortByParam
    {
        public CalculatePerMonthHighRiseSaleDetailSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CalculatePerMonthHighRiseSaleDetailSortBy
    {
        UnitNo,
        LCCloseBy,
        LCProjectBy,
        PerRate,
        CommissionType,
        AgreementAmount,
        AgreementApproveDate,
        TransferCommissionCloseAmount,
        TransferCommissionProjectAmount,
        TransferCommissionSumAmount,
        CommissionPayAmount
    }
}
