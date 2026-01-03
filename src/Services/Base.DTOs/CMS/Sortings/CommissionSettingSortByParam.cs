using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CommissionSettingSortByParam
    {
        public CommissionSettingSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CommissionSettingSortBy
    {
        ProjectNo,
        ProjectName,
        NewLaunch,
        RateSettingFixSaleModel,
        RateSettingFixTransferModel,
        RateSettingFixSale,
        RateSettingFixTransfer,
        RateSettingSale,
        RateSettingTransfer,
        RateSettingAgent
    }
}
