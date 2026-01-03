using System;
namespace Report.Integration.Reports.TR
{
    public class NRP_TR_004_CD
    {
        /// <summary>
        /// รายละเอียดค่าใช้จ่ายในการโอน (โครงการแนวสูง)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
