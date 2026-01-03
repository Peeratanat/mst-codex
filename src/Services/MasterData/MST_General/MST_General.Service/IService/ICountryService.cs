using Base.DTOs.MST;
using Database.Models.MST;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using PagingExtensions;

namespace MST_General.Services
{
    public interface ICountryService : BaseInterfaceService
    {
        Task<List<CountryDTO>> GetCountryDropdownListAsync(CountryFilter filter, CancellationToken cancellationToken = default);
        Task<CountryPaging> GetCountryListAsync(CountryFilter filter, PageParam pageParam, CountrySortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<CountryDTO> GetCountryAsync(Guid id, CancellationToken cancellationToken = default);
        Task<CountryDTO> FindCountryAsync(string code, CancellationToken cancellationToken = default);
        Task<CountryDTO> CreateCountryAsync(CountryDTO input);
        Task<CountryDTO> UpdateCountryAsync(Guid id, CountryDTO input);
        Task DeleteCountryAsync(Guid id);
    }
}
