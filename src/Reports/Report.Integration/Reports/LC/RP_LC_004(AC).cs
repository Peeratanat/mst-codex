using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_004_AC_
    {
        /// <summary>
        /// รายงานรายละเอียดการรับเงิน(บัญชี)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public string Deposit { get; set; }
        public string UserName { get; set; }
        public string Method { get; set; }
        public string BankAccount { get; set; }
        public string ReceivedID { get; set; }
        public long? DateStart3 { get; set; }
        public long? DateEnd3 { get; set; }
    }
}
