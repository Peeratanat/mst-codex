using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqUnitAreaNationality
    {
        public Guid ProjectID { get; set;}
        public double TotalSaleArea { get; set; }
        public double CurrentUseEngSaleArea { get; set; }
        public double PercentSaleArea { get; set; }
        public double LimitUseEngSaleArea { get; set; }
        public dbqUnitAreaNationality()
        {

        }
    }
}
