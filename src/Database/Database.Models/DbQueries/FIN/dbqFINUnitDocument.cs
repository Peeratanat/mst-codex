using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqFINUnitDocument : BaseDbQueries
    {
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public Guid? CompanyID { get; set; }
        public Guid? BookingID { get; set; }
        public string DocumentNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNo { get; set; }

    }
}
