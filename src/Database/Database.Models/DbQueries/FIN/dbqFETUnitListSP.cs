using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqFETUnitListSP : BaseDbQueries
    {
        public string UnitNo { get; set; }
        public string CustomerName { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? BookingID { get; set; }
        public Guid? AgreementID { get; set; }
        public decimal? SumAmountFET { get; set; }

        public int? CountFET { get; set; }
    }
}
