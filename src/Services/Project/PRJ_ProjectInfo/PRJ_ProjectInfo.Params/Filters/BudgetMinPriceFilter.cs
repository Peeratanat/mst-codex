using System;
namespace PRJ_ProjectInfo.Params.Filters
{
    public class BudgetMinPriceFilter
    {
        public Guid? ProjectID { get; set; }
        public int? Year { get; set; }
        public int? Quarter { get; set; }
        public string UnitNo { get; set; }
        public decimal? AmonutTo { get; set; }
        public decimal? AmonutFrom { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedTo { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public int? UnitStatus { get; set; }
        public Guid? BG { get; set; }
        public Guid? SUBBG { get; set; }
        public bool? UnitTransfer { get; set; }
    }
}
