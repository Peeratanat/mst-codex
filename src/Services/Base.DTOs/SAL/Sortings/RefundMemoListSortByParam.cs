using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL.Sortings
{
    public class RefundMemoListSortByParam
    {
        public RefundMemoListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum RefundMemoListSortBy
    {
        UnitNo,
        ActualTransferDate,
        FullName,
        IsRefundCustomer,
        IsRefundLegalEntity,
        IsRefundCustomerByLegalEntity,
        RefundDueDate
    }
}
