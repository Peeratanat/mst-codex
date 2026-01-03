using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CommissionHighRiseTransferVeiwSortByParam
    {
        public CommissionHighRiseTransferVeiwSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CommissionHighRiseTransferVeiwSortBy
    {
        UnitNo,
        LCTransfer,
        CommissionPercentRate,
        CommissionPercentType,
        NetSellPrice,
        TransferDate,
        LCTransferPaid,
        CommissionForThisMonth
    }
}
