using System;
namespace Report.Integration.Reports.AG
{
    public class RP_AG_010
    {
        /// <summary>
        /// รายงานรายละเอียดการครบกำหนดทำสัญญา แต่ยังไม่ได้ทำสัญญา
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string Projects { get; set; }
        public string UnitNumber { get; set; }
        public string StatusAG3 { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
