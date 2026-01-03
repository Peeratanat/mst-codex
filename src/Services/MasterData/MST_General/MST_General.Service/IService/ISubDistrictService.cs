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
    public interface ISubDistrictService : BaseInterfaceService
    {
        Task<SubDistrictListDTO> FindSubDistrictAsync(Guid districtID, string name, CancellationToken cancellationToken = default);
        Task<List<SubDistrictListDTO>> GetSubDistrictDropdownListAsync(string name, Guid? districtID, CancellationToken cancellationToken = default);
        Task<SubDistrictPaging> GetSubDistrictListAsync(SubDistrictFilter filter, PageParam pageParam, SubDistrictSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<SubDistrictDTO> GetSubDistrictAsync(Guid id, CancellationToken cancellationToken = default);
        Task<SubDistrictDTO> CreateSubDistrictAsync(SubDistrictDTO input);
        Task<SubDistrictDTO> UpdateSubDistrictAsync(Guid id, SubDistrictDTO input);
        Task DeleteSubDistrictAsync(Guid id);
    }
}
