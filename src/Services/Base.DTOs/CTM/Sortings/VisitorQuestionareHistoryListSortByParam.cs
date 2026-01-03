using System;
namespace Base.DTOs.CTM
{
    public class VisitorQuestionnaireHistoryListSortByParam
    {
        public VisitorQuestionnaireHistoryListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum VisitorQuestionnaireHistoryListSortBy
    {
        Project, StatusQuestionaire, AnsweredQuestionaire, TotalQuestionaire, QuestionaireDate
    }
}
