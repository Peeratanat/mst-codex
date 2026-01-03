using System;
namespace Report.Integration.Reports.FI
{
    public class rpt_Z_BudgetReport_Monitor
    {
        /// <summary>
        /// รายงาน Monitor
        /// </summary>

        //param
        public string HomeType { get; set; }
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public string Status3 { get; set; }
        public string Status4 { get; set; }
        public string UserName { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
    }
}
