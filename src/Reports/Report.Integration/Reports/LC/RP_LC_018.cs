using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_018
    {
        /// <summary>
        /// รายงานของแถมในโปรโมชั่นโอนกรรมสิทธิ์
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string P_ID { get; set; }
        public string PromotionID { get; set; }
        public string UserName { get; set; }
    }
}
