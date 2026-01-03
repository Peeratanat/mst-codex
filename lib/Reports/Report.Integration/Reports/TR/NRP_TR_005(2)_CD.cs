using System;
namespace Report.Integration.Reports.TR
{
    class NRP_TR_005_2__CD
    {
        /// <summary>
        /// รายละเอียดการรับเงินในการโอน (โครงการแนวสูง)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
