using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models.MST;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using MST_General.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace MST_General.Services
{
    public interface IProvinceService : BaseInterfaceService
    {
        Task<ProvinceListDTO> FindProvinceAsync(string name, CancellationToken cancellationToken = default);
        Task<List<ProvinceListDTO>> GetProvinceDropdownListAsync(string name, CancellationToken cancellationToken = default);
        Task<ProvincePaging> GetProvinceListAsync(ProvinceFilter filter, PageParam pageParam, ProvinceSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<ProvinceDTO> GetProvinceAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ProvinceDTO> CreateProvinceAsync(ProvinceDTO input);
        Task<ProvinceDTO> UpdateProvinceAsync(Guid id, ProvinceDTO input);
        Task<ProvinceDTO> GetProvincePostalCodeAsync(string postalCode, CancellationToken cancellationToken = default);
        Task DeleteProvinceAsync(Guid id);
    }
}
