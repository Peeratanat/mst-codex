using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_008
    {
        /// <summary>
        /// รายงานการรับรู้ข้อมูลการตัดบัญชีจากธนาคาร
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public string SBUID { get; set; }
        public string UserName { get; set; }
        public string StatusAG { get; set; }
    }
}
