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
    public interface IMinPriceService : BaseInterfaceService
    {
        Task<MinPricePaging> GetMinPriceListAsync(Guid projectID, MinPriceFilter filter, PageParam pageParam, MinPriceSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<MinPriceDTO> CreateMinPriceAsync(Guid projectID, MinPriceDTO input);
        Task<MinPriceDTO> GetMinPriceAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<MinPriceDTO> UpdateMinPriceAsync(Guid projectID, Guid id, MinPriceDTO input);
        Task<MinPrice> DeleteMinPriceAsync(Guid projectID, Guid id);
        Task<FileDTO> ExportExcelMinPriceAsync(Guid projectID, MinPriceFilter filter, MinPriceSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<MinPriceExcelDTO> ImportMinPriceAsync(Guid projectID, FileDTO input, Guid? UserID = null);
        Task<FileDTO> ExportProjectMinPriceToSAPAsync(Guid projectID);
    }
}
