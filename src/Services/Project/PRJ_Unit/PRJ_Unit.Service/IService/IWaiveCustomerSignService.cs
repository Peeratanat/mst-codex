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
    public interface IWaiveCustomerSignService : BaseInterfaceService
    {
        Task<WaiveCustomerSignPaging> GetWaiveCustomerSignListAsync(Guid projectID, WaiveCustomerSignFilter filter, PageParam pageParam, WaiveCustomerSignSortByParam sortByParam,CancellationToken cancellationToken = default);
        Task<WaiveCustomerSignDTO> GetWaiveCustomerSignAsync(Guid projectID, Guid id,CancellationToken cancellationToken = default);
        Task<WaiveCustomerSignDTO> CreateWaiveCustomerSignAsync(Guid projectID, WaiveCustomerSignDTO input);
        Task<WaiveCustomerSignDTO> UpdateWaiveCustomerSignAsync(Guid projectID, Guid? id, WaiveCustomerSignDTO input);
        Task<WaiveQC> DeleteWaiveCustomerSignAsync(Guid projectID, Guid id);
        Task<WaiveCustomerSignExcelDTO> ImportWaiveCustomerSignAsync(Guid projectID, FileDTO input, Guid? UserID = null);
        Task<FileDTO> ExportExcelWaiveCustomerSignAsync(Guid projectID, WaiveCustomerSignFilter filter, WaiveCustomerSignSortByParam sortByParam);
    }
}
