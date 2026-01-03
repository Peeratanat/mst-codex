using System;
using Database.Models;

namespace MST_Promotion.Params.Filters
{
    public class MaterialSyncJobFilter
    {
        public string JobNo { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
        public BackgroundJobStatus? Status { get; set; }
    }
}
