using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Base.DTOs.MST.LetterOfGuaranteeDTO;
using static Base.DTOs.MST.ServitudeDTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class ServitudeService : IServitudeService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public ServitudeService(DatabaseContext db)
        {
            logModel = new LogModel("ServitudeService", null);
            this.DB = db;
        }

        public async Task<ServitudePaging> GetServitudeListAsync(ServitudeFilter filter, PageParam pageParam, ServitudeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var query = from l in DB.Servitude.AsNoTracking()
                        join p in DB.Projects.AsNoTracking() on l.ProjectID equals p.ID into g

                        select new ServitudeQueryResult
                        {
                            Servitude = l,
                            Project = l.Project,
                            CreatedBy = l.CreatedBy,
                            UpdatedBy = l.UpdatedBy,
                        };

            #region Filter
            if (!string.IsNullOrEmpty(filter.ProjectNo))
                query = query.Where(o => o.Servitude.Project.ProjectNo == filter.ProjectNo);
            if (!string.IsNullOrEmpty(filter.MsgTH))
                query = query.Where(o => o.Servitude.MsgTH.Contains(filter.MsgTH));
            if (!string.IsNullOrEmpty(filter.MsgEN))
                query = query.Where(o => o.Servitude.MsgEN.Contains(filter.MsgEN));
            if (filter.UpdatedFrom != null)
                query = query.Where(o => o.Servitude.Updated >= filter.UpdatedFrom);
            if (filter.UpdatedTo != null)
                query = query.Where(o => o.Servitude.Updated <= filter.UpdatedTo);
            if (filter.UpdatedBy != null)
                query = query.Where(o => o.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));

            #endregion 
            ServitudeDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<ServitudeQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync(cancellationToken);
            var results = queryResults
                    .Select(async o => await ServitudeDTO.CreateFromQueryResultAsync(o, DB))
                    .Select(o => o.Result).ToList();

            return new ServitudePaging()
            {
                PageOutput = pageOutput,
                Servitude = results
            };
        }

        public async Task<ServitudeDTO> AddServitudeAsync(ServitudeDTO input)
        {
            #region Validate
            var validate = await DB.Servitude.AnyAsync(o => o.ProjectID == input.Project.Id);
            if (validate)
            {
                ValidateException ex = new ValidateException();
                ex.AddError("ERR9999", "โครงการนี้มีข้อความพิเศษ (ภาระจำยอม) อยู่แล้ว", 1);
                throw ex;
            }
            #endregion

            Servitude model = new Servitude
            {
                ProjectID = input.Project.Id,
                MsgTH = input.MsgTH,
                MsgEN = input.MsgEN,
                IsBooking = input.IsBooking ?? false,
                IsAgreement = input.IsAgreement ?? false,
                IsDeleted = false
            };


            DB.Servitude.Add(model);
            await DB.SaveChangesAsync();
            var newData = await DB.Servitude
                                .Include(o => o.Project)
                                .Include(o => o.CreatedBy)
                                .Include(o => o.UpdatedBy)
                                .FirstOrDefaultAsync(o => o.ID == model.ID);
            var result = ServitudeDTO.CreateFromModel(newData, DB);
            return result;
        }

        public async Task<ServitudeDTO> EditServitudeAsync(ServitudeDTO input)
        {

            var Servitude = await DB.Servitude.FirstOrDefaultAsync(o => o.ID == input.ID);
            Servitude.MsgTH = input.MsgTH;
            Servitude.MsgEN = input.MsgEN;
            Servitude.IsBooking = input.IsBooking ?? false;
            Servitude.IsAgreement = input.IsAgreement ?? false;
            Servitude.Updated = DateTime.Now;


            DB.Servitude.Update(Servitude);
            await DB.SaveChangesAsync();
            var newData = await DB.Servitude
                                .Include(o => o.Project)
                                .Include(o => o.CreatedBy)
                                .Include(o => o.UpdatedBy)
                                .FirstOrDefaultAsync(o => o.ID == input.ID);
            var result = ServitudeDTO.CreateFromModel(newData, DB);
            return result;
        }

        public async Task DeleteServitudeAsync(ServitudeDTO input)
        {
            var Servitude = await DB.Servitude.FindAsync(input.ID);
            Servitude.IsDeleted = true;

            DB.Servitude.Update(Servitude);
            await DB.SaveChangesAsync();


            // await DB.Servitude.Where(o => o.ID == input.ID).ExecuteUpdateAsync(c =>
            //          c.SetProperty(col => col.IsDeleted, true)
            //          .SetProperty(col => col.Updated, DateTime.Now)
            //      );
        }
    }
}
