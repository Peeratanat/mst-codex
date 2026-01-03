using System;
namespace Report.Integration.Reports.AR
{
    public class RP_AR_017
    {
        /// <summary>
        /// JV
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string BatchID { get; set; }
        public string JV { get; set; }
    }
}
