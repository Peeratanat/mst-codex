using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class DirectCreditDebitApprovalFormSortByParam
    {
        public DirectCreditDebitApprovalFormSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum DirectCreditDebitApprovalFormSortBy
    {
        //#DEV
        SAPCompanyID,
        ProjectNo,
        UnitNo,
        AgreementNo,
        BankName,
        AccountNO,
        ExpireDate,
        CustomerName,
        StartDate,
        UpdatedBy,
        UpdatedDate


    }
}
