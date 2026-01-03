using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using MST_Kattyfaq.Params.Filters;
using MST_Kattyfaq.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_Kattyfaq.Services
{
    public interface IFrequentQuestionsService : BaseInterfaceService
    {
        Task<FrequentQuestionsPaging> GetQuestionFAQListAsync(QuestopnFAQFilter filter, PageParam pageParam, QuestionFAQSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<QuestionFAQDTO> AddQuestionAsync(QuestionFAQDTO input);
        Task<QuestionFAQDTO> EditQuestionAsync(QuestionFAQDTO input);
        Task<FileDTO> GenTextFileQuestionAsync(List<Guid> questuinID, CancellationToken cancellationToken = default);
        Task DeleteQuestionAsync(QuestionFAQDTO input);
    }
}
