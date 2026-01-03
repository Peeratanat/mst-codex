using System;
namespace Report.Integration.Reports.AG
{
    public class RP_AG_006
    {
        /// <summary>
        /// รายงานการจองและการทำสัญญา
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public string StatusAG { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
        public string HomeType { get; set; }
        public string StatusPeriod { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
    }
}
