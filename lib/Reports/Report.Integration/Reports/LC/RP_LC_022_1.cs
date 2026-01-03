using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_022_1
    {
        /// <summary>
        /// รายงานขอเพิ่มมิเตอร์ไฟฟ้า
        /// </summary>

        //param
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public string WaterStatus { get; set; }
        public string ElectricStatus { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public long? DateStart2 { get; set; }
        public long? DateEnd2 { get; set; }
        public long? DateStart3 { get; set; }
        public long? DateEnd3 { get; set; }
        public string UserName { get; set; }
    }
}
