using System;
namespace Report.Integration.Reports.LC
{
    public class RPT_Media
    {
        /// <summary>
        /// รายงานสื่อ
        /// </summary>

        //param
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
