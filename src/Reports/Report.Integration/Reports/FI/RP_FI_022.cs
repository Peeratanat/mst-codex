using System;
namespace Report.Integration.Reports.FI
{
    public class RP_FI_022
    {
        /// <summary>
        /// รายงานประมาณการเงินเข้า - ตามวันโอนจริง
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string LandStatus { get; set; }
        public string UnitStatus { get; set; }
        public string LoanStatus1 { get; set; }
        public string UserName { get; set; }
        public string CurrentUserID { get; set; }
    }
}
