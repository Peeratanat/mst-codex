using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_027
    {
        /// <summary>
        /// รายงานสรุปเหตผล ยึดจอง/ยึดสัญญา By BU
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string Projects { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
