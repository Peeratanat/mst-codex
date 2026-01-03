using System;
namespace Report.Integration.Reports.LC
{
    public class rp_commission_project
    {
        /// <summary>
        /// รายงาน Commission by Project (แนวราบ)
        /// </summary>

        //param
        public string HomeType { get; set; }
        public string ProductID { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string UserName { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
    }
}
