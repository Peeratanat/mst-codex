using System;
namespace Report.Integration.Reports.AR
{
    public class RP_AR_021
    {
        /// <summary>
        /// รายงานเงินโอนไม่ทราบผู้โอน (บัญชีพัก)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string BankAccount { get; set; }
        public string UserName { get; set; }
    }
}
