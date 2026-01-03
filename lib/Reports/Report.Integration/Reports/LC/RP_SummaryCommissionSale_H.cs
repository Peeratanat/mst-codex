using System;
namespace Report.Integration.Reports.LC
{
    public class RP_SummaryCommissionSale_H
    {
        /// <summary>
        /// รายงานสรุปยอด Commission ขาย (แนวสูง)
        /// </summary>

        //param
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
