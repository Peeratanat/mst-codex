using Database.Models;
using Database.Models.DbQueries.EXT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
    public class MasterProjectDTO
    {
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }

        public static MasterProjectDTO CreateFromQuery(dbqGetProjectList model)
        {
            if (model != null)
            {
                var result = new MasterProjectDTO();
                result.ProjectID = model.ProjectID;
                result.ProjectName = model.ProjectName;

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
