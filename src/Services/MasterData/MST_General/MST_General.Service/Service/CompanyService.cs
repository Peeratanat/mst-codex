using Database.Models;
using Database.Models.MST;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using MST_General.Params.Outputs;
using PagingExtensions;
using MST_General.Params.Filters;
using Common.Helper.Logging;
using LinqKit;
using System.Collections;

namespace MST_General.Service
{
    public class CompanyService : ICompanyService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public CompanyService(DatabaseContext db)
        {
            logModel = new LogModel("CompanyService", null);
            DB = db;
        }

        public async Task<List<CompanyDropdownDTO>> GetCompanyDropdownListAsync(CompanyDropdownFilter filter, CompanyDropdownSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<Company> query = DB.Companies.AsNoTracking();
            var predicate = PredicateBuilder.New<Company>(true);
            if (!string.IsNullOrEmpty(filter.Name))
            {
                var arrName = filter.Name.ToLower().Replace("-", "");
                predicate = predicate.And(o => ((o.SAPCompanyID ?? "") + (o.NameTH ?? "")).ToLower().Contains(arrName));
            }

            if (filter.CompanyID != null)
            {
                if (!(filter.IsWrongCompany ?? false))
                    predicate = predicate.And(o => o.ID == filter.CompanyID);
                else
                    predicate = predicate.And(o => o.ID != filter.CompanyID);
            }
            query = query.Where(predicate);

            CompanyDropdownDTO.SortBy(sortByParam, ref query);
            var results = await query.Select(o => CompanyDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
            return results;
        }

        public async Task<CompanyPaging> GetCompanyListAsync(CompanyFilter filter, PageParam pageParam, CompanySortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<CompanyQueryResult> query = DB.Companies.AsNoTracking().IgnoreQueryFilters().Where(o => !o.IsDeleted)
                                          .Select(x => new CompanyQueryResult
                                          {
                                              Company = x,
                                              District = x.District,
                                              SubDistrict = x.SubDistrict,
                                              Province = x.Province,
                                              UpdatedBy = x.UpdatedBy
                                          });
            var predicate = PredicateBuilder.New<CompanyQueryResult>(true);

            if (!string.IsNullOrEmpty(filter.APAuthorizeRefID))
            {
                predicate = predicate.And(x => x.Company.APAuthorizeRefID.Contains(filter.APAuthorizeRefID));
            }
            if (!string.IsNullOrEmpty(filter.Code))
            {
                predicate = predicate.And(x => x.Company.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                predicate = predicate.And(x => x.Company.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                predicate = predicate.And(x => x.Company.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.TaxID))
            {
                predicate = predicate.And(x => x.Company.TaxID.Contains(filter.TaxID));
            }
            if (!string.IsNullOrEmpty(filter.AddressTH))
            {
                predicate = predicate.And(x => x.Company.AddressTH.Contains(filter.AddressTH));
            }
            if (!string.IsNullOrEmpty(filter.AddressEN))
            {
                predicate = predicate.And(x => x.Company.AddressEN.Contains(filter.AddressEN));
            }
            if (!string.IsNullOrEmpty(filter.BuildingTH))
            {
                predicate = predicate.And(x => x.Company.BuildingTH.Contains(filter.BuildingTH));
            }
            if (!string.IsNullOrEmpty(filter.BuildingEN))
            {
                predicate = predicate.And(x => x.Company.BuildingEN.Contains(filter.BuildingEN));
            }
            if (!string.IsNullOrEmpty(filter.SoiTH))
            {
                predicate = predicate.And(x => x.Company.SoiTH.Contains(filter.SoiTH));
            }
            if (!string.IsNullOrEmpty(filter.SoiEN))
            {
                predicate = predicate.And(x => x.Company.SoiEN.Contains(filter.SoiEN));
            }
            if (!string.IsNullOrEmpty(filter.RoadTH))
            {
                predicate = predicate.And(x => x.Company.RoadTH.Contains(filter.RoadTH));
            }
            if (!string.IsNullOrEmpty(filter.RoadEN))
            {
                predicate = predicate.And(x => x.Company.RoadEN.Contains(filter.RoadEN));
            }
            if (!string.IsNullOrEmpty(filter.PostalCode))
            {
                predicate = predicate.And(x => x.Company.PostalCode.Contains(filter.PostalCode));
            }
            if (!string.IsNullOrEmpty(filter.Telephone))
            {
                predicate = predicate.And(x => x.Company.Telephone.Replace("-", "").Contains(filter.Telephone.Replace("-", "")));
            }
            if (!string.IsNullOrEmpty(filter.Fax))
            {
                predicate = predicate.And(x => x.Company.Fax.Replace("-", "").Contains(filter.Fax.Replace("-", "")));
            }
            if (!string.IsNullOrEmpty(filter.Website))
            {
                predicate = predicate.And(x => x.Company.Website.Contains(filter.Website));
            }
            if (!string.IsNullOrEmpty(filter.SapCompanyID))
            {
                predicate = predicate.And(x => x.Company.SAPCompanyID.Contains(filter.SapCompanyID));
            }
            if (!string.IsNullOrEmpty(filter.NameTHOld))
            {
                predicate = predicate.And(x => x.Company.NameTHOld.Contains(filter.NameTHOld));
            }
            if (!string.IsNullOrEmpty(filter.NameENOld))
            {
                predicate = predicate.And(x => x.Company.NameENOld.Contains(filter.NameENOld));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                predicate = predicate.And(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                predicate = predicate.And(x => x.Company.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                predicate = predicate.And(x => x.Company.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                predicate = predicate.And(x => x.Company.Updated >= filter.UpdatedFrom && x.Company.Updated <= filter.UpdatedTo);
            }
            if (filter.ProvinceID != null && filter.ProvinceID != Guid.Empty)
            {
                predicate = predicate.And(x => x.Company.ProvinceID == filter.ProvinceID);
            }
            if (filter.DistrictID != null && filter.DistrictID != Guid.Empty)
            {
                predicate = predicate.And(x => x.Company.DistrictID == filter.DistrictID);
            }
            if (filter.SubDistrictID != null && filter.SubDistrictID != Guid.Empty)
            {
                predicate = predicate.And(x => x.Company.SubDistrictID == filter.SubDistrictID);
            }
            if (filter.IsUseInCRM != null)
            {
                predicate = predicate.And(o => o.Company.IsUseInCRM == filter.IsUseInCRM);
            }
            query = query.Where(predicate);
            CompanyDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging(pageParam, ref query);

            var resultsDTO = await query.Select(o => CompanyDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

            return new CompanyPaging()
            {
                Companies = resultsDTO,
                PageOutput = pageOutput
            };
        }

        public async Task<CompanyDTO> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Companies.AsNoTracking().IgnoreQueryFilters().Include(o => o.Province)
                                .Include(o => o.District)
                                .Include(o => o.SubDistrict)
                                .Include(o => o.UpdatedBy)
                                .FirstOrDefaultAsync(o => o.ID == id && !o.IsDeleted, cancellationToken: cancellationToken);
            var result = CompanyDTO.CreateFromModel(model);
            return result;
        }

        public async Task<CompanyDTO> CreateCompanyAsync(CompanyDTO input)
        {
            Company model = new Company();
            input.ToModel(ref model);

            await DB.Companies.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await GetCompanyAsync(model.ID);
            return result;
        }

        public async Task<CompanyDTO> UpdateCompanyAsync(Guid id, CompanyDTO input)
        {
            var model = await DB.Companies.IgnoreQueryFilters().FirstOrDefaultAsync(o => !o.IsDeleted && o.ID == id);
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await GetCompanyAsync(model.ID);
            return result;
        }

        public async Task DeleteCompanyAsync(Guid id)
        {
            var model = await DB.Companies.IgnoreQueryFilters().Where(o => !o.IsDeleted).Where(o => o.ID == id).FirstOrDefaultAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            // await DB.Companies.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
            //     c.SetProperty(col => col.IsDeleted, true)
            //     .SetProperty(col => col.Updated, DateTime.Now)
            // );
        }
    }
}
