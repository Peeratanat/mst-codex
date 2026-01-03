using System;
namespace Report.Integration.Reports.AR
{
    public class RP_AR_001
    {
        /// <summary>
        /// รายงานประมาณการค่างวด (ประมาณการ)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string StatusAG { get; set; }
        public string UserName { get; set; }
    }
}
