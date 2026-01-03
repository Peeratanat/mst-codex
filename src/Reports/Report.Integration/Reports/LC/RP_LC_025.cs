using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_025
    {
        /// <summary>
        /// รายงานการติดตามสัญญา
        /// </summary>

        //param
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public string StatusAG { get; set; }
        public string UserName { get; set; }
        public string StatusDoc { get; set; }
    }
}
