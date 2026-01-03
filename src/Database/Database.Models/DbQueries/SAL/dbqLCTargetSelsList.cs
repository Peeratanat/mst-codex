using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqLCTargetSelsList
    {
        public string EmpCode { get; set; }
        public string ProjectID { get; set; }
        public int? Y { get; set; }
        public int? Q { get; set; }
        public int? UnitBook { get; set; }
        public int? UnitTransfer { get; set; }
        public string Createby { get; set; }
        public decimal? BookingAmount { get; set; }
        public decimal? TransferAmount { get; set; }

    }
}
