using Database.Models.MST;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MST_General.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace MST_General.Services
{
    public interface IDistrictService : BaseInterfaceService
    {
        Task<DistrictListDTO> FindDistrictAsync(Guid provinceID, string name, CancellationToken cancellationToken = default);
        Task<List<DistrictListDTO>> GetDistrictDropdownListAsync(string name, Guid? provinceID, CancellationToken cancellationToken = default);
        Task<DistrictPaging> GetDistrictListAsync(DistrictFilter filter, PageParam pageParam, DistrictSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<DistrictDTO> GetDistrictAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DistrictDTO> CreateDistrictAsync(DistrictDTO input);
        Task<DistrictDTO> UpdateDistrictAsync(Guid id, DistrictDTO input);
        Task DeleteDistrictAsync(Guid id);
    }
}
