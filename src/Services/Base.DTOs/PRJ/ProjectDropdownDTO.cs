using System;
using Base.DTOs.MST;
using Database.Models.PRJ;

namespace Base.DTOs.PRJ
{
    public class ProjectDropdownDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// ชื่อโครงการ (TH)
        /// </summary>
        public string ProjectNameTH { get; set; }
        /// <summary>
        /// ชื่อโครงการ (EN)
        /// </summary>
        public string ProjectNameEN { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectStatus
        /// สถานะโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO ProjectStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProductType
        /// สถานะโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO ProductType { get; set; }


        public MST.MasterCenterDropdownDTO ProjectType { get; set; }
        /// <summary>
        /// BG
        /// </summary>
        public MST.BGDropdownDTO BG { get; set; }
        /// <summary>
        /// Company
        /// </summary>
        public MST.CompanyDropdownDTO Company { get; set; }

        public BrandDTO Brand { get; set; }

        public static ProjectDropdownDTO CreateFromModel(Project model)
        {
            if (model != null)
            {
                var result = new ProjectDropdownDTO()
                {
                    Id = model.ID,
                    ProjectNo = model.ProjectNo,
                    ProjectNameEN = model.ProjectNameEN,
                    ProjectNameTH = model.ProjectNameTH,
                    ProjectStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProjectStatus),
                    ProductType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProductType),
                    ProjectType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProjectType),
                    Company = MST.CompanyDropdownDTO.CreateFromModel(model.Company),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    BG = MST.BGDropdownDTO.CreateFromModel(model.BG),
                    Brand = BrandDTO.CreateFromModel(model.Brand)
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static ProjectDropdownDTO CreateFromModel(Guid? projectID)
        {
            throw new NotImplementedException();
        }
    }
}
