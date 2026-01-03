using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class PriceListSortByParam
    {
        public PriceListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum PriceListSortBy
    {
        UnitNo,
        SaleArea,
        ActiveDate,
        BookingAmount,
        TotalSalePrice,
        ContractAmount,
        TitleDeedArea,
        DownAmount,
        OffsetArea,
        OffsetAreaUnitPrice,
        DownPaymentPeriod,
        DownPaymentPerPeriod,
        PercentDownPayment,
        SpecialDown,
        SpecialDownPrice,
        OffsetAreaPrice,
        UnitStatus,
        Updated,
        UpdatedBy
    }
}
