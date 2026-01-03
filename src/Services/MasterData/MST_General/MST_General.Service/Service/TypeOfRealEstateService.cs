using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using ErrorHandling;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using Common.Helper.Logging;

namespace MST_General.Services
{
    public class TypeOfRealEstateService : ITypeOfRealEstateService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public TypeOfRealEstateService(DatabaseContext db)
        {
            logModel = new LogModel("TypeOfRealEstateService", null);
            DB = db;
        }

        public async Task<List<TypeOfRealEstateDropdownDTO>> GetTypeOfRealEstateDropdownListAsync(string name, CancellationToken cancellationToken = default)
        {
            IQueryable<TypeOfRealEstate> query = DB.TypeOfRealEstates.AsNoTracking();
            var predicate = PredicateBuilder.New<TypeOfRealEstate>(true);
            if (!string.IsNullOrEmpty(name))
            {
                predicate = predicate.And(o => o.Name.Contains(name));
            }

            var results = await query.Where(predicate).OrderBy(o => o.Name).Take(100)
            .Select(o => TypeOfRealEstateDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
            return results;
        }

        public async Task<TypeOfRealEstatePaging> GetTypeOfRealEstateListAsync(TypeOfRealEstateFilter filter, PageParam pageParam, TypeOfRealEstateSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<TypeOfRealEstateQueryResult> query = DB.TypeOfRealEstates.AsNoTracking()
                .Select(o => new TypeOfRealEstateQueryResult
                {
                    TypeOfRealEstate = o,
                    RealEstateCategory = o.RealEstateCategory,
                    UpdatedBy = o.UpdatedBy
                });

            var predicate = PredicateBuilder.New<TypeOfRealEstateQueryResult>(true);
            #region Filter
            if (!string.IsNullOrEmpty(filter.Code))
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.Name.Contains(filter.Name));
            }
            if (filter.StandardCostFrom != null)
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.StandardCost >= filter.StandardCostFrom);
            }
            if (filter.StandardCostTo != null)
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.StandardCost <= filter.StandardCostTo);
            }
            if (filter.StandardCostFrom != null && filter.StandardPriceTo != null)
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.StandardCost >= filter.StandardCostFrom && x.TypeOfRealEstate.StandardCost <= filter.StandardCostTo);
            }
            if (filter.StandardPriceFrom != null)
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.StandardPrice >= filter.StandardPriceFrom);
            }
            if (filter.StandardPriceTo != null)
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.StandardPrice <= filter.StandardPriceTo);
            }
            if (filter.StandardPriceFrom != null && filter.StandardPriceTo != null)
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.StandardPrice >= filter.StandardPriceFrom && x.TypeOfRealEstate.StandardCost <= filter.StandardPriceTo);
            }
            if (!string.IsNullOrEmpty(filter.RealEstateCategoryKey))
            {
                var realEstateCategoryMasterCenter = await DB.MasterCenters.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Key == filter.RealEstateCategoryKey
                                                                       && x.MasterCenterGroupKey == "RealEstateCategory");
                predicate = predicate.And(x => x.RealEstateCategory.ID == realEstateCategoryMasterCenter.ID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                predicate = predicate.And(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                predicate = predicate.And(x => x.TypeOfRealEstate.Updated >= filter.UpdatedFrom && x.TypeOfRealEstate.Updated <= filter.UpdatedTo);
            }
            #endregion

            query = query.Where(predicate);
            TypeOfRealEstateDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging(pageParam, ref query);
            var results = await query.Select(o => TypeOfRealEstateDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);
            return new TypeOfRealEstatePaging()
            {
                PageOutput = pageOutput,
                TypeOfRealEstates = results
            };
        }

        public async Task<TypeOfRealEstateDTO> GetTypeOfRealEstateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.TypeOfRealEstates.AsNoTracking()
                .Include(o => o.RealEstateCategory)
                .FirstOrDefaultAsync(o => o.ID == id, cancellationToken);

            var result = TypeOfRealEstateDTO.CreateFromModel(model);
            return result;
        }

        public async Task<TypeOfRealEstateDTO> CreateTypeOfRealEstateAsync(TypeOfRealEstateDTO input)
        {
            await input.ValidateAsync(DB);
            TypeOfRealEstate model = new TypeOfRealEstate();
            input.ToModel(ref model);

            await DB.TypeOfRealEstates.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = GetTypeOfRealEstateAsync(model.ID).Result;
            return result;
        }

        public async Task<TypeOfRealEstateDTO> UpdateTypeOfRealEstateAsync(Guid id, TypeOfRealEstateDTO input)
        {
            var modelChk = await DB.Models.Where(o => o.TypeOfRealEstateID == id).ToListAsync();

            ValidateException ex = new ValidateException();
            if (modelChk.Count > 0)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0007").FirstOrDefaultAsync();
                string desc = typeof(TypeOfRealEstateDTO).GetProperty(nameof(TypeOfRealEstateDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            await input.ValidateAsync(DB);
            var model = await DB.TypeOfRealEstates.FindAsync(id);

            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = GetTypeOfRealEstateAsync(model.ID).Result;
            return result;
        }

        public async Task DeleteTypeOfRealEstateAsync(Guid id)
        {
            var model = await DB.Models.Where(o => o.TypeOfRealEstateID == id).ToListAsync();

            ValidateException ex = new ValidateException();
            if (model.Count > 0)
            {
                var errMsg = await DB.ErrorMessages.FirstOrDefaultAsync(o => o.Key == "ERR0055");
                var msg = errMsg?.Message;
                ex.AddError(errMsg?.Key, msg, (int)errMsg?.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            var typeOfRealEstates = await DB.TypeOfRealEstates.FindAsync(id);
            typeOfRealEstates.IsDeleted = true;
            await DB.SaveChangesAsync();
            // await DB.TypeOfRealEstates.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //           c.SetProperty(col => col.IsDeleted, true)
            //           .SetProperty(col => col.Updated, DateTime.Now)
            //       );

        }
    }
}
