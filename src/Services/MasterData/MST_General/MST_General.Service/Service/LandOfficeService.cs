using Database.Models;
using Database.Models.MST;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MST_General.Params.Outputs;
using PagingExtensions;
using Base.DTOs;
using ErrorHandling;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class LandOfficeService : ILandOfficeService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public LandOfficeService(DatabaseContext db)
        {
            logModel = new LogModel("LandOfficeService", null);
            this.DB = db;
        }

        public async Task<List<LandOfficeListDTO>> GetLandOfficeDropdownListAsync(string name, Guid? provinceID = null, CancellationToken cancellationToken = default)
        {
            IQueryable<LandOffice> query = DB.LandOffices.AsNoTracking();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }
            if (provinceID != null)
            {
                query = query.Where(o => o.SubDistricts.Any(m => m.District.ProvinceID == provinceID));
            }
            query = query.Where(o => o.NameTH != "Migrate");

            var results = await query.OrderBy(o => o.NameTH).Take(100)
                    .Select(o => LandOfficeListDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }

        public async Task<LandOfficePaging> GetLandOfficeListAsync(LandOfficeFilter filter, PageParam pageParam, LandOfficeSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<LandOfficeQueryResult> query = DB.LandOffices.AsNoTracking()
                                                        .Select(x => new LandOfficeQueryResult
                                                        {
                                                            LandOffice = x,
                                                            UpdatedBy = x.UpdatedBy
                                                        });

            #region Filter
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.LandOffice.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.LandOffice.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.LandOffice.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LandOffice.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LandOffice.Updated >= filter.UpdatedFrom && x.LandOffice.Updated <= filter.UpdatedTo);
            }
            #endregion

            var results = await query.ToListAsync(cancellationToken);

            var resultDTOs = results.Select(async o => await LandOfficeDTO.CreateFromQueryResultAsync(o, DB)).Select(o => o.Result).ToList();

            #region List Filter
            if (filter.ProvinceID != null)
            {
                resultDTOs = resultDTOs.Where(o => o.Province?.Id == filter.ProvinceID).ToList();
            }
            if (filter.DistrictID != null)
            {
                resultDTOs = resultDTOs.Where(o => o.District?.Id == filter.DistrictID).ToList();
            }
            if (filter.SubDistrictID != null)
            {
                resultDTOs = resultDTOs.Where(o => o.SubDistrict?.Id == filter.SubDistrictID).ToList();
            }
            #endregion

            LandOfficeDTO.SortByList(sortByParam, ref resultDTOs);
            var pageOutput = PagingHelper.PagingList<LandOfficeDTO>(pageParam, ref resultDTOs);

            return new LandOfficePaging()
            {
                LandOffices = resultDTOs,
                PageOutput = pageOutput
            };
        }

        public async Task<LandOfficeDTO> GetLandOfficeAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.LandOffices.AsNoTracking().Include(o => o.UpdatedBy).FirstOrDefaultAsync(x => x.ID == id, cancellationToken);
            var result = await LandOfficeDTO.CreateFromModelAsync(model, DB);
            return result;
        }

        public async Task<LandOfficeDTO> CreateLandOfficeAsync(LandOfficeDTO input)
        {
            await input.ValidateAsync(DB);
            LandOffice model = new LandOffice();
            input.ToModel(ref model);


            var runningKey = "47";
            var runningNumber = await DB.RunningNumberCounters.FirstOrDefaultAsync(o => o.Key == runningKey && o.Type == "MST.LandOffice");
            if (runningNumber != null)
            {
                runningNumber.Count = runningNumber.Count + 1;
                model.Code = runningKey;
                DB.Entry(runningNumber).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }
            else
            {
                var runningModel = new Database.Models.MST.RunningNumberCounter()
                {
                    Key = runningKey,
                    Type = "MST.LandOffice",
                    Count = 1
                };

                await DB.RunningNumberCounters.AddAsync(runningModel);
                model.Code = runningKey;
                await DB.SaveChangesAsync();
            }

            await DB.LandOffices.AddAsync(model);
            if (input.SubDistrict != null)
            {
                var subDistrict = await DB.SubDistricts.FirstOrDefaultAsync(o => o.ID == input.SubDistrict.Id);
                if (subDistrict != null)
                {
                    subDistrict.LandOfficeID = model.ID;
                    DB.Update(subDistrict);
                }
            }
            await DB.SaveChangesAsync();
            var result = await LandOfficeDTO.CreateFromModelAsync(model, DB);
            return result;
        }

        public async Task<LandOfficeDTO> UpdateLandOfficeAsync(Guid id, LandOfficeDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.LandOffices.FindAsync(id);
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var addresses = await DB.Addresses.Where(o => o.ID == id).ToListAsync();
            ValidateException ex = new ValidateException();
            if (addresses.Count == 0)
            {
                if (input.SubDistrict != null)
                {
                    var subDistrict = await DB.SubDistricts.FirstOrDefaultAsync(o => o.ID == input.SubDistrict.Id);
                    if (subDistrict != null)
                    {
                        subDistrict.LandOfficeID = model.ID;
                        DB.Update(subDistrict);
                        await DB.SaveChangesAsync();
                    }
                }
            }
            else
            {
                var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR0055");
                var msg = errMsg.Message;
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            var result = await LandOfficeDTO.CreateFromModelAsync(model, DB);
            return result;
        }

        public async Task DeleteLandOfficeAsync(Guid id)
        {
            var model = await DB.LandOffices.FindAsync(id);
            var modelSub = await DB.SubDistricts.FirstOrDefaultAsync(o => o.LandOfficeID == id);
            if (model is not null)
                model.IsDeleted = true;
            if (modelSub is not null)
                modelSub.LandOfficeID = null;
            await DB.SaveChangesAsync();


            //     await DB.LandOffices.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //        c.SetProperty(col => col.IsDeleted, true)
            //        .SetProperty(col => col.Updated, DateTime.Now)
            //    );

        }
    }
}
