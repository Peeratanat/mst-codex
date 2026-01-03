using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class OfflinePaymentSortByParam
    {
        public OfflinePaymentSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum OfflinePaymentSortBy
    {
        Project,
        UnitNo,
        CustomerName,
        PayAmount,
        OfflinePaymentItem,
        ReceiveDate,
        ReceiptByName,
        TempReceiptNo,
        ReceiptNo,
        OfflinePaymentStatus,
        ConfirmedDate,
        ConfirmedByName
    }
}
