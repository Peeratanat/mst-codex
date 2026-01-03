using System;
namespace Report.Integration.Reports.EX
{
    public class RPT_Executive_TotalBooking
    {
        /// <summary>
        /// Executive Total Booking Report
        /// </summary>

        //param
        public string StatusProject { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
        public string Projects { get; set; }
        public string Trans { get; set; }
    }
}
