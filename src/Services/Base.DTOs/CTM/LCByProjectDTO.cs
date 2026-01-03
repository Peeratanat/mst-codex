using Database.Models;
using Database.Models.DbQueries.USR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.CTM
{
    public class LCByProjectDTO
    {
        public Guid? Id { get; set; }
        public string EmployeeNo { get; set; }
        public string DisplayName { get; set; }
        public PRJ.ProjectDropdownDTO Project { get; set; }
        public Guid? LeadUserIgnoreID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public USR.UserListDTO UpdateBy { get; set; }
        public bool Ignore { get; set; }
     



        public static LCByProjectDTO CreateFromQuery(dbqLeadUserList model, DatabaseContext db, PRJ.ProjectDropdownDTO Project)
        {
            if (model != null)
            {
                var lead = db.LeadUsersIgnore.IgnoreQueryFilters().Where(o => o.UserID == model.ID && o.ProjectID == Project.Id).FirstOrDefault();
                
                    
                if (lead != null)
                {
                    var result = new LCByProjectDTO()
                    {
                        Id = model.ID,
                        EmployeeNo = model.EmployeeNo,
                        DisplayName = model.DisplayName,
                        Project = Project,
                        Ignore = lead.IsDeleted == true ? false : true,
                        LeadUserIgnoreID = lead.ID,
                        UpdateBy = lead.UpdatedByUserID == null ? null: USR.UserListDTO.CreateFromModel(db.Users.Where(o => o.ID == lead.UpdatedByUserID).FirstOrDefault()),
                        UpdatedDate = lead.Updated,

                    };
                    return result;
                }
                else
                {
                    var result = new LCByProjectDTO()
                    {
                        Id = model.ID,
                        EmployeeNo = model.EmployeeNo,
                        DisplayName = model.DisplayName,
                        Project = Project,
                        Ignore = false,

                    };
                    return result;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
