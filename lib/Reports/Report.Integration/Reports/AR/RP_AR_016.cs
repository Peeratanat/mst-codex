using System;
namespace Report.Integration.Reports.AR
{
    public class RP_AR_016
    {
        /// <summary>
        /// Receive Journal (Receipt)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string BatchID { get; set; }
        public string Receipt { get; set; }
    }
}
