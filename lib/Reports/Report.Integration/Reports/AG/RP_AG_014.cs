using System;
namespace Report.Integration.Reports.AG
{
    public class RP_AG_014
    {
        /// <summary>
        /// Min Price 2
        /// </summary>

        //param
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public long? DateStart3 { get; set; }
        public long? DateEnd3 { get; set; }
        public string UserName { get; set; }
        public int MinPriceType { get; set; }
        public string StatusAG { get; set; }
        public string HomeType { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
    }
}
