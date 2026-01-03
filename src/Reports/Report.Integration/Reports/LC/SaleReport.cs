using System;
namespace Report.Integration.Reports.LC
{
    public class SaleReport
    {
        /// <summary>
        /// รายงาน Sale Report Daily
        /// </summary>

        //param
        public string HomeType { get; set; }
        public string ProductID { get; set; }
        public string UserName { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
    }
}
