using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class BillPaymentDetailSortByParam
    {
        public BillPaymentWaitingSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BillPaymentWaitingSortBy
    {
        ReceiveDate,
        CustomerName,
        BankRef1,
        BankRef2,
        BankRef3,
        AgreementNO,
        Project,
        Unit,
        Payment,
        Amount,
        Status,
        Period,
        ReceiptTempNo,
        IsWrongAccount, 
        PaymentItemName,
        Company,
        PayAmountDetail
    }
}