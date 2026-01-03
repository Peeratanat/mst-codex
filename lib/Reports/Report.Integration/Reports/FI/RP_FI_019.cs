using System;
namespace Report.Integration.Reports.FI
{
    public class RP_FI_019
    {
        /// <summary>
        /// รายงานใบจอง ระบบ Offline
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UnitNumber { get; set; }
        public string Method { get; set; }
        public string BookingOfflineStatus { get; set; }
        public string UserName { get; set; }
    }
}
