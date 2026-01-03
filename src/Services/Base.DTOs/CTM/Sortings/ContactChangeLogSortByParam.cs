using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class ContactChangeLogSortByParam
    {
        public ContactChangeLogSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ContactChangeLogSortBy
    {
        ColumnChanged,
        ValuesOld,
        ValuesNew,
        Updated,
        UpdatedBy,
    }
}
