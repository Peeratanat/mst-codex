using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_014
    {
        /// <summary>
        /// รายงานการเปลี่ยนรายละเอียดสัญญา
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public string UserName { get; set; }
        public string UnitNumber { get; set; }
    }
}
