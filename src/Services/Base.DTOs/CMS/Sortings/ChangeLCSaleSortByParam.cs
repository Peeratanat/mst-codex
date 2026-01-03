using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class ChangeLCSaleSortByParam
    {
        public ChangeLCSaleSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ChangeLCSaleSortBy
    {
        ProjectID,
        Unit,
        //BookingNo,
        Agreement,
        CustomerName,
        ActiveDate,
        OldSaleOfficerType,
        OldAgent,
        OldAgentEmployee,
        OldSaleUser,
        OldProjectSaleUser
    }
}
