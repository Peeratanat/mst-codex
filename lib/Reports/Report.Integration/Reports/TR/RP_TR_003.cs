using System;
namespace Report.Integration.Reports.TR
{
    class RP_TR_003
    {
        /// <summary>
        /// รายงานการบันทึกรายละเอียดเช็ค
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string BankCheque { get; set; }
        public string UserName { get; set; }
        public string UnitNumber { get; set; }
    }
}
