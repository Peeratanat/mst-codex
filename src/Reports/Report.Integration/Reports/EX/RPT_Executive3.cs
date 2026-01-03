using System;
namespace Report.Integration.Reports.EX
{
    public class RPT_Executive3
    {
        /// <summary>
        /// Executive Sales Summary Report By BU (ราคาขายหักส่วนลดวันโอน)
        /// </summary>

        //param
        public string BatchID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string HomeType { get; set; }
        public string StatusProject { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
        public string UserName { get; set; }
    }
}
