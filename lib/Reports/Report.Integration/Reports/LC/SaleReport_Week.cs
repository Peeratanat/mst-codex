using System;
namespace Report.Integration.Reports.LC
{
    public class SaleReport_Week
    {
        /// <summary>
        /// รายงาน Sale Report Weekly
        /// </summary>

        //param
        public string HomeType { get; set; }
        public string ProductID { get; set; }
        public string UserName { get; set; }
        public int? Year { get; set; }
        public int? WeekStart { get; set; }
        public int? WeekEnd { get; set; }
    }
}
