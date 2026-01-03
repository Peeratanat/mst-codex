using Database.Models;
using Database.Models.DbQueries.EQN;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.EXT
{
	public class ProjectPriceListDropdownDTO
	{
        public Guid? Id { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string ProjectNameEN { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        
        public int SumByProject { get; set; }


        public static ProjectPriceListDropdownDTO CreateFromModel(Project model)
        {
            if (model != null)
            {
                var result = new ProjectPriceListDropdownDTO()
                {
                    Id = model.ID,
                    ProjectNo = model.ProjectNo,
                    ProjectNameEN = model.ProjectNameEN,
                    ProjectNameTH = model.ProjectNameTH,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                };

                return result;
            }
            else
            {
                return null;
            }
        }



}
}
