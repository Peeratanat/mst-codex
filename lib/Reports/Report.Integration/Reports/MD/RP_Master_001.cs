using System;
namespace Report.Integration.Reports.MD
{
    public class RP_Master_001
    {
        /// <summary>
        /// รายงานข้อมูลโครงการ
        /// </summary>

        //param
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public Guid? BrandID { get; set; }
        public Guid? CompanyID { get; set; }
        public string ProductTypeKey { get; set; }
        public string ProjectStatusKeys { get; set; }
        public bool? IsActive { get; set; }
    }
}
