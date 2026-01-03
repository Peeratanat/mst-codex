using System;
namespace Report.Integration.Reports.LC
{
    public class RP_SummaryCommission
    {
        /// <summary>
        /// รายงานสรุปยอด Commission (แนวราบ)
        /// </summary>

        //param
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
