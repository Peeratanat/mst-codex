using System;
namespace Report.Integration.Reports.AG
{
    public class RP_AG_004
    {
        /// <summary>
        /// รายงานการออกจดหมายนัดโอน
        /// </summary>

        //param
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public int MailType2 { get; set; }
        public string UserName { get; set; }
        public string StatusAG { get; set; }
    }
}
