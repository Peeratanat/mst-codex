using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Budget.Params.Filters
{
    public class ProjectsFilter : BaseFilter
    {
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// รหัสโครงการ SapCode
        /// </summary>
        public string SapCode { get; set; }
        /// <summary>
        /// โครงการภาษาไทย
        /// </summary>
        public string ProjectNameTH { get; set; }
        /// <summary>
        /// โครงการภาษาอังกฤษ
        /// </summary>
        public string ProjectNameEN { get; set; }   
        /// <summary>
        /// Brand Identity
        /// </summary>
        public Guid? BrandID { get; set; }
        /// <summary>
        /// Company Identity
        /// </summary>
        public Guid? CompanyID { get; set; }
        /// <summary>
        /// productTypeKey
        /// </summary>
        public string ProductTypeKey { get; set; }
        /// <summary>
        /// สถานะ Active ของโครงการ
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// ProjectStatusKeys (Comma seperated 1,2,3)
        /// </summary>
        public string ProjectStatusKeys { get; set; }
        /// <summary>
        /// userID
        /// </summary>
        public Guid? UserID { get; set; }
    }
}
