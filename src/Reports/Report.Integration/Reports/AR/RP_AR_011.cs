using System;
namespace Report.Integration.Reports.AR
{
    public class RP_AR_011
    {
        /// <summary>
        /// รายงานใบนัดโอนกรรมสิทธิ์
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public string UserName { get; set; }
    }
}
