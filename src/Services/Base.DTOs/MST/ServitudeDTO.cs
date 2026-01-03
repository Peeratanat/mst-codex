using Base.DTOs.PRJ;
using Base.DTOs.SAL.Sortings;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.USR;
using FileStorage;
using static Base.DTOs.MST.LetterOfGuaranteeDTO;

namespace Base.DTOs.MST
{
    public class ServitudeDTO : BaseDTO
    {
        public Guid ID { get; set; }
        public ProjectDropdownDTO Project { get; set; }
        public string MsgTH { get; set; }
        public string MsgEN { get; set; }
        public bool? IsBooking { get; set; }
        public bool? IsAgreement { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }


        public static ServitudeDTO CreateFromModel(Servitude model, DatabaseContext db)
        {
            if (model != null)
            {

                var result = new ServitudeDTO()
                {
                    ID = model.ID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    MsgTH = model.MsgTH,
                    MsgEN = model.MsgEN,
                    IsDeleted = model.IsDeleted,
                    Created = model.Created,
                    CreatedBy = model.CreatedBy?.DisplayName,
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

        public static async Task<ServitudeDTO> CreateFromQueryResultAsync(ServitudeQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                var pj = await db.Projects.Include(o => o.ProductType).FirstOrDefaultAsync(o => o.ID == model.Project.ID);

                ServitudeDTO result = new ServitudeDTO()
                {
                    ID = model.Servitude.ID,
                    Project = ProjectDropdownDTO.CreateFromModel(pj),
                    MsgTH = model.Servitude.MsgTH,
                    MsgEN = model.Servitude.MsgEN,
                    IsBooking = model.Servitude.IsBooking,
                    IsAgreement = model.Servitude.IsAgreement,
                    IsDeleted = model.Servitude.IsDeleted,
                    Created = model.Servitude.Created,
                    CreatedBy = model.CreatedBy?.DisplayName,
                    Updated = model.Servitude.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,

                };
                return result;
            }
            else
            {
                return null;
            }
        }



        public static void SortBy(ServitudeSortByParam sortByParam, ref IQueryable<ServitudeQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ServitudeSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Servitude.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Servitude.Project.ProjectNo);
                        break;
                    case ServitudeSortBy.MsgTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Servitude.MsgTH);
                        else query = query.OrderByDescending(o => o.Servitude.MsgTH);
                        break;
                    case ServitudeSortBy.MsgEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Servitude.MsgEN);
                        else query = query.OrderByDescending(o => o.Servitude.MsgEN);
                        break;
                    case ServitudeSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Servitude.Updated);
                        else query = query.OrderByDescending(o => o.Servitude.Updated);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.Servitude.Project.ProjectNo).ThenBy(o => o.Project.ProjectNo);
            }
        }

        public class ServitudeQueryResult
        {
            public models.MST.Servitude Servitude { get; set; }
            public models.PRJ.Project Project { get; set; }
            public models.USR.User CreatedBy { get; set; }
            public models.USR.User UpdatedBy { get; set; }
        }
    }
}
