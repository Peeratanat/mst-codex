using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_015
    {
        /// <summary>
        /// รายงานสินค้าของลูกค้า
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public string StatusAG { get; set; }
        public string UserName { get; set; }
    }
}
