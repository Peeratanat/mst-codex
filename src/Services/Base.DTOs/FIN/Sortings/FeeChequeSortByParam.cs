using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class FeeChequeSortByParam
    {
        public FeeChequeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum FeeChequeSortBy
    {
        Project,
        ReceiveDate,
        ChequeType,
        ChequeNo,
        RecevieNo,
        UnitNo,
        FeeAmount,
        FeePercent,
        PayAmount,
        NetAmount,
        UpdatedDate,
        UpdatedByName,
        DepositStatus,
        DepositNo,
        ChequeTypeName,
        BankName,
        CompanyName
    }
}
