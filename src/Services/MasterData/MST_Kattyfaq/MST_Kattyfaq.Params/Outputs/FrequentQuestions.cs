using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.MST;
using PagingExtensions;

namespace MST_Kattyfaq.Params.Outputs
{
    public class FrequentQuestionsPaging
    {
        public List<QuestionFAQDTO> FrequentQuestions { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
