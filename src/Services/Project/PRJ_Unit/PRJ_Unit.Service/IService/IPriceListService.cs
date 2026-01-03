using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_Unit.Services
{
    public interface IPriceListService : BaseInterfaceService
    {
        Task<PriceListPaging> GetPriceListsAsync(Guid projectID, PriceListFilter request, PageParam pageParam, PriceListSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<PriceListDTO> CreatePriceListAsync(Guid projectid, PriceListDTO input);
        Task<PriceListDTO> UpdatePriceListAsync(Guid projectID, Guid id, PriceListDTO input);
        Task DeletePriceListAsync(Guid id);
        Task<PriceListDTO> GetPriceListAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<PriceListExcelDTO> ImportProjectPriceListAsync(Guid projectID, FileDTO input, Guid? UserID = null);
        Task<FileDTO> ExportExcelPriceListAsync(Guid projectID);
        Task<DataTable> ConvertExcelToDataTable(FileDTO input);
    }
}
