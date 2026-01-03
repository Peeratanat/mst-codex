using System;
namespace Report.Integration.Reports.AR
{
    public class RP_AR_007
    {
        /// <summary>
        /// รายงานการรับเงินสะสม(ต้องออกรายการทุกหลัง)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
        public string UnitNumber { get; set; }
        public string PercentPaymennt { get; set; }
    }
}
