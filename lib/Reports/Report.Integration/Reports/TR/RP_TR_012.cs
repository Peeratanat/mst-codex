using System;
namespace Report.Integration.Reports.TR
{
    public class RP_TR_012
    {
        /// <summary>
        /// รายงานนัดโอนกรรมสิทธิ์
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string LandStatus { get; set; }
        public string UnitStatus { get; set; }
        public string LoanStatus1 { get; set; }
        public string QCStatus { get; set; }
        public string UserName { get; set; }
        public string CurrentUserId { get; set; }
        public string WorkTransferStatus { get; set; }
        public long? WorkTransferDate { get; set; }
    }
}
