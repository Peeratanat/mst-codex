using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models.DbQueries.CTM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class ConsentListDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// ที่อยู่ตามทะเบียนบ้าน
        /// </summary>
        public MasterCenterDTO ReferentType { get; set; }
        /// <summary>
        /// ที่อยู่ที่ทำงาน
        /// </summary>
        public MasterCenterDTO ConsentType { get; set; }
        /// <summary>
        /// ที่อยู่ที่ทำงาน
        /// </summary>
        public string ReferentSubType { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public ProjectDropdownDTO Project { get; set; }


        /// <summary>
        /// โครงการ
        /// </summary>
        public string Fullname { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public string UpdateBy { get; set; }
        public static ConsentListDTO CreateFromQueryResult(dbqConsentListSP model, models.DatabaseContext DB)
        {
            var result = new ConsentListDTO
            {
                ID = model.ID,
                ConsentType = DB.MasterCenters.Where(o => o.ID == model.ConsentTypeMasterCenterID).Select(o => MasterCenterDTO.CreateFromModel(o)).FirstOrDefault(),
                ReferentType = DB.MasterCenters.Where(o => o.ID == model.ReferentType).Select(o => MasterCenterDTO.CreateFromModel(o)).FirstOrDefault(),
                ReferentSubType = model.ReferentSubTypeMasterCenter,
                Project = DB.Projects.Where(o => o.ID == model.ProjectID).Select(o => ProjectDropdownDTO.CreateFromModel(o)).FirstOrDefault(),
                ContactNumber=model.ContactNumber,
                Fullname =model.FullName,
                PhoneNumber =model.PhoneNumber,
                Email = model.Email,
                CreateDate =model.CreateDate,
                UpdateDate = model.UpDateDate,
                UpdateBy=model.UpdateBy

            };

            return result;
        }
    }
}
