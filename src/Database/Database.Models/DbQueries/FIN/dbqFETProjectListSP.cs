using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqFETProjectListSP : BaseDbQueries
    {
        public string ProjectNameTH { get; set; }
        public string ProjectNo { get; set; }
        public Guid? ProjectID { get; set; }
        public int? countUnit { get; set; }
        public int? countFET { get; set; }
        public decimal? SumAmountFET { get; set; }
    }
}
