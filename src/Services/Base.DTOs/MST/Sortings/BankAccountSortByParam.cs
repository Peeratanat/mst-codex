using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class BankAccountSortByParam
    {
        public BankAccountSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum BankAccountSortBy
    {
        Company,
        Bank,
        BankBranch,
        Province,
        BankAccountType,
        GLAccountNo,
        BankAccountNo,
        IsTransferAccount,
        IsDirectDebit,
        IsDirectCredit,
        IsDepositAccount,
        IsPCard,
        ServiceCode,
        MerchantID,
        GLAccountType,
        GLRefCode,
        HasVat,
        Name,
        IsActive,
        Updated,
        UpdatedBy,
        IsForeignTransfer,
        IsQRCode,
        IsBillPayment,
    }
}
