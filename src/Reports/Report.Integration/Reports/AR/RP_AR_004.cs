using System;
namespace Report.Integration.Reports.AR
{
    public class RP_AR_004
    {
        /// <summary>
        /// สรุปเงินค่างวดค้างชำระสะสม
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public string UserName { get; set; }
    }
}
