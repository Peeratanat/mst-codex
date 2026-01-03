using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_012
    {
        /// <summary>
        /// รายงานยึดจอง/ยึดสัญญา/ยึดคืน
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string UserName { get; set; }
        public string UnitNumber { get; set; }
        public string HomeType { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
    }
}
