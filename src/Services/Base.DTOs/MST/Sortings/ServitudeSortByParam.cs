using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class ServitudeSortByParam
    {
        public ServitudeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ServitudeSortBy
    {
        Project,
        MsgTH,
        MsgEN,
        Updated,

    }
}
