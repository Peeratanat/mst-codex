using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.PRJ
{
    public class dbqTitleDeedStatusList
    {
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string UnitNo { get; set; }
        public string TitledeedNo { get; set; }
        public double? TitledeedArea { get; set; }
        public string AllOwnerName { get; set; }
        public double? TotalPrice { get; set; }
        public double? RepayLoan { get; set; }
    }
}
