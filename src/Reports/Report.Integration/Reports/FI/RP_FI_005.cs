using System;
namespace Report.Integration.Reports.FI
{
    public class RP_FI_005
    {
        /// <summary>
        /// รายงานใบจอง
        /// </summary>

        //param
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public string ProductID { get; set; }
        public string CompanyID { get; set; }
        public string StatusAG { get; set; }
        public string UserName { get; set; }
        public string UnitStatus { get; set; }
    }
}
