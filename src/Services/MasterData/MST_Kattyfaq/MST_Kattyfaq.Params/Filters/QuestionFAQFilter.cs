using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Kattyfaq.Params.Filters
{
    public class QuestopnFAQFilter
    {
        public string QuestionDesc { get; set; }
        public string AnswerDesc { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsSubmit { get; set; }
        public Guid? FAQCategoryMasterCenterID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? UpsertPineconeDateFrom { get; set; }
        public DateTime? UpsertPineconeDateTo { get; set; }
    }
}
