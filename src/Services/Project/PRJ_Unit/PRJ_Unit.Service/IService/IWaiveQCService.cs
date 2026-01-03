using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_Unit.Services
{
    public interface IWaiveQCService : BaseInterfaceService
    {
        Task<WaiveQCPaging> GetWaiveQCListAsync(Guid projectID, WaiveQCFilter request, PageParam pageParam, WaiveQCSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<WaiveQCDTO> GetWaiveQCAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<WaiveQCDTO> CreateWaiveQCAsync(Guid projectID, WaiveQCDTO input);
        Task<WaiveQCDTO> UpdateWaiveQCAsync(Guid projectID, Guid? id, WaiveQCDTO input);
        Task<WaiveQCExcelDTO> ImportWaiveQCAsync(Guid projectID, FileDTO input, Guid? UserID = null);
        Task DeleteWaiveQCAsync(Guid projectID, Guid id);
        Task<FileDTO> ExportExcelWaiveQCAsync(Guid projectID, WaiveQCFilter filter, WaiveQCSortByParam sortByParam);
    }
}
