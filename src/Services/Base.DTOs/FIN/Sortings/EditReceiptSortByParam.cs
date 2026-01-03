using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class EditReceiptSortByParam
    {
        public EditReceiptSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum EditReceiptSortBy
    {
        RecieptNo,
        ReceiveDate,
        BankAccount,
        Project,
        Unit,
        PaymentMethod,
        RVNumber,
        ReceiptStatus,
        PayAmount
    }
}
