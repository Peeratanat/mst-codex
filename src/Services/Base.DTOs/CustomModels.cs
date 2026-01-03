using System;
using System.Collections.Generic;
using System.Text;
using Database.Models.PRJ;
using Base.DTOs.CMS;

namespace Base.DTOs
{
    class CustomModels
    {
    }

    public class ProjectInputModel
    {
        public Guid? ProjectID { get; set; }

        public static ProjectInputModel CreateFromModel(Project model)
        {
            if (model != null)
            {
                var result = new ProjectInputModel()
                {
                    ProjectID = model.ID
                };
                return result;
            }
            else;
            {
                return null;
            }
        }
    }

    public class RateSettingSaleInputModel
    {
        public List<ProjectInputModel> ListProject { get; set; }
        public List<RateSettingSaleDTO> ListRateSettingSale { get; set; }
    }

    public class RateSettingTransferInputModel
    {
        public List<ProjectInputModel> ListProject { get; set; }
        public List<RateSettingTransferDTO> ListRateSettingTransfer { get; set; }
    }
}
