using System;
namespace Report.Integration.Reports.AR
{
    public class RP_AR_015
    {
        /// <summary>
        /// Receive Journal (Deposit)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string BatchID { get; set; }
        public string Deposit { get; set; }
    }
}
