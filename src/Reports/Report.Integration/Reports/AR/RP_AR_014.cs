using System;
namespace Report.Integration.Reports.AR
{
    public class RP_AR_014
    {
        /// <summary>
        /// รายงานยอดคงเหลือ เงินสด เช็ค และบัตรเครดิต
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public string UserName { get; set; }
    }
}
