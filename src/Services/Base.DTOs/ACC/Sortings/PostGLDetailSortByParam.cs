using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.ACC
{
    public class PostGLDetailSortByParam
    {
        public PostGLDetailSortBy? SortBy { get; set; }
        public bool Ascending { get; set; } = true;
    }

    public enum PostGLDetailSortBy
    {
        DocumentNo,
        ReferentNo,
        PostGLType,
        DocumentDate,
        Project,
        Unit,
        BankAccount,
        Amount,
        Fee,
        RemainAmount,
        CreateBy,
        Created,
        PostingDate,
        Status
    }
}