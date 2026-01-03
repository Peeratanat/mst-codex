using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM.Sortings
{
    public class PromotionRequestDeliveryListSortByParam
    {
        public PromotionRequestDeliveryListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum PromotionRequestDeliveryListSortBy
    {
        UnitNo,
        CustomerName,
        ContractDate,
        TransferDate,
        SalePromotionRequestStatus,
        TransferPromotionRequestStatus,
        SalePromotionDeliveryStatus,
        TransferPromotionDeliveryStatus
    }
}
