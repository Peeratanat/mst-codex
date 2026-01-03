using System;
namespace Report.Integration.Reports.FI
{
    public class RP_FI_006
    {
        /// <summary>
        /// รายงานการชำระเงินด้วยบัตรเครดิต
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string SBUID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public string UserName { get; set; }
    }
}
