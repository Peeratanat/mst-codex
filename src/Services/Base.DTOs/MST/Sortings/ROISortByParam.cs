using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class ROISortByParam
    {
        public ROISortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ROISortBy
    {
        Version,
        Remark,
        Project,
        Created,
        Updated,
    }
}
