using System;
namespace Report.Integration.Reports.AG
{
    public class RP_AG_R4_1
    {
        /// <summary>
        /// Summary Promotion ขาย+โอน (V2)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string SBUID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public long? DateStart3 { get; set; }
        public long? DateEnd3 { get; set; }
        public string UserName { get; set; }
    }
}
