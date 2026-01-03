using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class DeductMoneySortByParam
    {
        public DeductMoneySortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum DeductMoneySortBy
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
