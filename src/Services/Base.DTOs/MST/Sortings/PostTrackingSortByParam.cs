using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class PostTrackingSortByParam
    {
        public PostTrackingSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum PostTrackingSortBy
    {
        PostTrackingNo,
        IsUsed
    }
}
