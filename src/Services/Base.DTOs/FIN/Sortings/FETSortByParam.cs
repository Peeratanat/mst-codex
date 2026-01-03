using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class FETSortByParam
    {
        public FETSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum FETSortBy
    {
        Project,
        UnitNo,
        FETNumber,
        FETAmount,
        CustomerName,
        Company,
        FETRequester,
        DepositCode,
        ReceiveDate,
        ReceiptAmount,
        FETStatus,
        Updated,
        UpdateByuser,
        Created,
        Bank,
        PayStatus
    }
}
