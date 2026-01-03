
using Base.DTOs.MST;
using Common.Helper.Logging;
using Database.Models.MST;
using MST_General.Params.Outputs;
using MST_General.Params.Filters;
using PagingExtensions;
using System.Collections;
using MST_General.Services;

namespace MST_General.Service
{
    public interface ICompanyService: BaseInterfaceService
    {
        Task<List<CompanyDropdownDTO>> GetCompanyDropdownListAsync(CompanyDropdownFilter filter, CompanyDropdownSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<CompanyPaging> GetCompanyListAsync(CompanyFilter filter, PageParam pageParam, CompanySortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<CompanyDTO> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default);
        Task<CompanyDTO> CreateCompanyAsync(CompanyDTO input);
        Task<CompanyDTO> UpdateCompanyAsync(Guid id, CompanyDTO input);
        Task DeleteCompanyAsync(Guid id);
    }
}
