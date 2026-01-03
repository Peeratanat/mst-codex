using System;
namespace Report.Integration.Reports.LC
{
    public class RPT_FollowCustomerA3
    {
        /// <summary>
        /// รายงานติดตามลูกค้า By LC (A3)
        /// </summary>

        //param
        //param
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string LCByProduct { get; set; }
        public string UserName { get; set; }
        public int? Disqualify { get; set; }
        public int? Lead { get; set; }
        public int? FirstWalk { get; set; }
        public int? Revisit { get; set; }
        public int? TaskLead1 { get; set; }
        public int? TaskLead2 { get; set; }
        public int? TaskLead3 { get; set; }
        public int? TaskLead4 { get; set; }
        public int? TaskWalk1 { get; set; }
        public int? TaskWalk2 { get; set; }
        public int? TaskWalk3 { get; set; }
        public int? TaskWalk4 { get; set; }
        public int? TaskWalk5 { get; set; }
        public int? TaskWalk6 { get; set; }
        public int? TaskRevisit1 { get; set; }
        public int? TaskRevisit2 { get; set; }
        public int? TaskRevisit3 { get; set; }
        public int? TaskRevisit4 { get; set; }
        public string SessionID { get; set; }
        public int? TaskWalkEnd { get; set; }
    }
}
