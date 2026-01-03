using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class ChangeLCTransferSortByParam
    {
        public ChangeLCTransferSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ChangeLCTransferSortBy
    {
        ProjectID,
        Unit,
        Transfer,
        //Agreement,
        CustomerName,
        ActiveDate,
        OldLCTransfer
    }
}
