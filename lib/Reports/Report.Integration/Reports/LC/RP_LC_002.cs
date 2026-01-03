using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_002
    {
        /// <summary>
        /// สรุปสัญญา/ตรวจสอบเลขที่บัญชี
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public long? DateStart3 { get; set; }
        public long? DateEnd3 { get; set; }
        public string UserName { get; set; }
        public string StatusAG2 { get; set; }
        public string AccountType { get; set; }
    }
}
