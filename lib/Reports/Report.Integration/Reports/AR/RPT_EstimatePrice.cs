using System;
namespace Report.Integration.Reports.AR
{
    public class RPT_EstimatePrice
    {
        /// <summary>
        /// รายงานราคาประเมิน (แปลงที่โอน) / รายงานราคาประเมินของแปลงที่โอนแล้วในแต่ละเดือน
        /// </summary>

        //param
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
    }
}
