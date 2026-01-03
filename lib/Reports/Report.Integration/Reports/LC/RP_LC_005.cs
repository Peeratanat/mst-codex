using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_005
    {
        /// <summary>
        /// รายงานใบแจ้งเบิกโฉนด
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string SBUID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
        public string UnitNumber { get; set; }
    }
}
