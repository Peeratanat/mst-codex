using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class QuestionFAQSortByParam
    {
        public QuestionFAQSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum QuestionFAQSortBy
    {
        QuestionDesc,
        AnswerDesc,
        Updated,
        UpdatedBy,
        //FAQCategory,
        Created,
        CreatedBy,
        UpsertPineconeDate,
    }
}
