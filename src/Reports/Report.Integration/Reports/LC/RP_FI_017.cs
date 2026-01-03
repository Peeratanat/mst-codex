using System;
namespace Report.Integration.Reports.LC
{
    public class RP_FI_017
    {
        /// <summary>
        /// รายงานประกอบรับส่งข้อมูล Direct Credit
        /// </summary>

        //param
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public string AccountStatus { get; set; }
        public string UserName { get; set; }
    }
}
