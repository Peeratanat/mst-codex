using System;
namespace Report.Integration.Reports.FI
{
    public class RP_FI_024
    {
        /// <summary>
        /// รายงานรายละเอียดรายการส่งเสริมการขาย
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
