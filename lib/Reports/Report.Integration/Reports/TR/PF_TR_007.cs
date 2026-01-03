using System;
namespace Report.Integration.Reports.TR
{
    public class PF_TR_007
    {
        /// <summary>
        /// ใบรับค่าใช้จ่าย ณ. วันโอนกรรมสิทธิ์
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
