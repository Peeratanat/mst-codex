using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqDirectDCDDRemainPeriod
    {
        public Guid? BookingID { get; set; } 
        public string BookingNo { get; set; }

        public Guid? UnitPriceID { get; set; }
        public int Period { get; set; }
         
        public DateTime? DueDate { get; set; }
        public decimal? ItemAmount { get; set; }
        public decimal? PayAmount { get; set; }
        public decimal? RemainAmount { get; set; }
    }
}


