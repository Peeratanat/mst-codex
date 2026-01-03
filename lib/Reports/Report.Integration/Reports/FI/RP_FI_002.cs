using System;
namespace Report.Integration.Reports.FI
{
    public class RP_FI_002
    {
        /// <summary>
        /// รายงานสินค้า
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public long? DateStart { get; set; }
        public string ProductID { get; set; }
        public string UserName { get; set; }
    }
}
