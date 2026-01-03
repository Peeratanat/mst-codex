using Database.Models.MST;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;
using MST_General.Params.Outputs;
using Base.DTOs;

namespace MST_General.Services
{
    public interface ILandOfficeService : BaseInterfaceService
    {
        Task<List<LandOfficeListDTO>> GetLandOfficeDropdownListAsync(string name, Guid? provinceID = null, CancellationToken cancellationToken = default);
        Task<LandOfficePaging> GetLandOfficeListAsync(LandOfficeFilter filter, PageParam pageParam, LandOfficeSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<LandOfficeDTO> GetLandOfficeAsync(Guid id, CancellationToken cancellationToken = default);
        Task<LandOfficeDTO> CreateLandOfficeAsync(LandOfficeDTO input);
        Task<LandOfficeDTO> UpdateLandOfficeAsync(Guid id, LandOfficeDTO input);
        Task DeleteLandOfficeAsync(Guid id);
    }
}
