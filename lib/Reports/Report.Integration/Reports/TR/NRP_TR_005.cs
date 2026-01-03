using System;
namespace Report.Integration.Reports.TR
{
    class NRP_TR_005
    {
        /// <summary>
        /// รายละเอียดการรับชำระเงินโอน
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public string UserName { get; set; }
    }
}
