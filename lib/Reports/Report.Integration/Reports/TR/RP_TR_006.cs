using System;
namespace Report.Integration.Reports.TR
{
    class RP_TR_006
    {
        /// <summary>
        /// รายงานสถานะธนาคาร By Unit
        /// </summary>

        //param
        public string Projects { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string StatusAG { get; set; }
        public string LoanStatus { get; set; }
        public string LoanStatus1 { get; set; }
        public string BankOnly { get; set; }
        public string UserName { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public long? DateStart3 { get; set; }
        public long? DateEnd3 { get; set; }
        public long? DateStart4 { get; set; }
        public long? DateEnd4 { get; set; }
        public string HomeType { get; set; }
        public string StatusProject { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
    }
}
