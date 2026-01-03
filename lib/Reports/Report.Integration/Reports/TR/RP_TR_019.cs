using System;
namespace Report.Integration.Reports.TR
{
    class RP_TR_019
    {
        /// <summary>
        /// ใบนำส่งเอกสารหลังโอนกรรมสิทธิ์
        /// </summary>

        //param
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UnitName { get; set; }
    }
}
