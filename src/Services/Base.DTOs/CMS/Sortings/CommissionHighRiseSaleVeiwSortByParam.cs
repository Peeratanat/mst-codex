using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CommissionHighRiseSaleVeiwSortByParam
    {
        public CommissionHighRiseSaleVeiwSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CommissionHighRiseSaleVeiwSortBy
    {
        UnitNo,
        SaleUser,
        ProjectSaleUser,
        CommissionPercentRate,
        CommissionPercentType,
        TotalContractNetAmount,
        SignAgreementDate,
        SaleUserSalePaid,
        ProjectSaleSalePaid,
        TotalSalePaid,
        CommissionForThisMonth

    }
}
