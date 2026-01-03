using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_003
    {
        /// <summary>
        /// รายงานโอนกรรมสิทธิ์
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public string UserName { get; set; }
        public string UnitNumber { get; set; }
        public string TransferStatus { get; set; }
    }
}
