using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.CTM
{
    public class dbqGetOppListForChangeOppSP : BaseDbQueries
    {
        public Guid? ID { get; set; }
        public string ContactNo { get; set; }
        public string DisplayName { get; set; }
        public Guid? ContactID { get; set; }
        public DateTime ArriveDate { get; set; }


    }
}
