using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class LineITSupportSortByParam
    {
        public LineITSupportSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum LineITSupportSortBy
    {
        reply_token,
        message_type,
        message_text,
        message_reply,
        created,
        createdBy,
    }
}
