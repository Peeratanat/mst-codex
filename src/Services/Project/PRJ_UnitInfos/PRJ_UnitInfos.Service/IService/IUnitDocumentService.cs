using Base.DTOs.SAL;
using PagingExtensions;
using PRJ_UnitInfos.Params.Filters;
using PRJ_UnitInfos.Params.Outputs;
using PRJ_UnitInfos.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_UnitInfos.Services
{
    public interface IUnitDocumentService : BaseInterfaceService
    {
        Task<UnitDocumentPaging> GetUnitDocumentDropdownListAsync(UnitDocumentFilter filter, PageParam pageParam, UnitDocumentSortByParam sortByParam,CancellationToken cancellationToken = default);

        Task<DocumentOwnerDTO> GetDocumentOwnerAsync(Guid BookingID,CancellationToken cancellationToken = default);
    }
}
