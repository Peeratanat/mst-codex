using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class IncreaseMoneySortByParam
    {
        public IncreaseMoneySortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum IncreaseMoneySortBy
    {
        ProjectID,
        SaleUserID,
        SaleUserName,
        ActiveDate,
        Amount,
        Remark,
        CommissionType

    }
}
