using System;
namespace Report.Integration.Reports.TR
{
    public class NRP_TR_004_H
    {
        /// <summary>
        /// รายละเอียดค่าใช้จ่ายในการโอน (โครงการแนวราบ)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
