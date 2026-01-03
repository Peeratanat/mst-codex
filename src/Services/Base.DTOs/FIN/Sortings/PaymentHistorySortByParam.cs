using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class PaymentHistorySortByParam
    {
        public PaymentHistorySortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum PaymentHistorySortBy
    {
        ReceiveDate,
        MasterPriceItem,
        Amount,
        PaymentMethodType,
        DepositHeader,
        Receipt,
        PostGL
    }
}
