using System;
namespace Report.Integration.Reports.LC
{
    public class RPT_Monitor_Tracking_Followup
    {
        /// <summary>
        /// รายงาน Monitor and Tracking Followup
        /// </summary>

        //param
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public string UserName { get; set; }
        public string LCByProduct { get; set; }
        public int? Lead { get; set; }
        public int? FirstWalk { get; set; }
        public int? Revisit { get; set; }
        public string FollowUpStatus { get; set; }
    }
}
