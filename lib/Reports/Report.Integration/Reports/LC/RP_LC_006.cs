using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_006
    {
        /// <summary>
        /// รายงานตรวจสอบโฉนด
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string LandStatus { get; set; }
        public string UserName { get; set; }
    }
}
