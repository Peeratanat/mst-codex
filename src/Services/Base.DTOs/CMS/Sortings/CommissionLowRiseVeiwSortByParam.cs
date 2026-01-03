using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CommissionLowRiseVeiwSortByParam
    {
        public CommissionLowRiseVeiwSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CommissionLowRiseVeiwSortBy
    {
        UnitNo,
        SaleUser,
        ProjectSaleUser,
        CommissionPercentRate,
        CommissionPercentType,
        TotalContractNetAmount,
        ActualTransferDate,
        SaleUserSalePaid,
        ProjectSaleSalePaid,
        TotalSalePaid,
        SaleUserNewLaunchPaid,
        ProjectSaleNewLaunchPaid,
        TotalNewLaunchPaid,
        CommissionForThisMonth
    }
}
