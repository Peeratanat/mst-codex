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
    public interface IHighRiseFeeService : BaseInterfaceService
    {
        Task<HighRiseFeePaging> GetHighRiseFeeListAsync(Guid projectID, HighRiseFeeFilter filter, PageParam pageParam, HighRiseFeeSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<HighRiseFeeDTO> GetHighRiseFeeAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<HighRiseFeeDTO> CreateHighRiseFeeAsync(Guid projectID, HighRiseFeeDTO input);
        Task<HighRiseFeeDTO> UpdateHighRiseFeeAsync(Guid projectID, Guid id, HighRiseFeeDTO input);
        Task<HighRiseFee> DeleteHighRiseFeeAsync(Guid projectID, Guid id);
        Task<HighRiseFeeExcelDTO> ImportHighRiseFeeAsync(Guid projectID, FileDTO input, Guid? UserID = null);
        Task<FileDTO> ExportHighRiseFeeAsync(Guid projectID, HighRiseFeeFilter filter, HighRiseFeeSortByParam sortByParam);
    }
}
