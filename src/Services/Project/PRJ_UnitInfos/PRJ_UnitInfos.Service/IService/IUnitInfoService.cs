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
    public interface IUnitInfoService : BaseInterfaceService
    {
        Task<UnitInfoListPaging> GetUnitInfoListAsync(UnitInfoListFilter filter, PageParam pageParam, UnitInfoListSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<UnitInfoListPagingByContact> GetUnitInfoListByContactAsync(UnitInfoListFilterByContact filter, PageParam pageParam, UnitInfoListSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<UnitInfoDTO> GetUnitInfoAsync(Guid unitID, CancellationToken cancellationToken = default);
        Task<UnitInfoDTO> GetUnitInfoForPaymentAsync(Guid unitID, CancellationToken cancellationToken = default);
        Task<UnitInfoPreSalePromotionDTO> GetUnitInfoPreSalePromotionAsync(Guid unitID, CancellationToken cancellationToken = default);
        Task<UnitInfoSalePromotionDTO> GetUnitInfoSalePromotionAsync(Guid unitID, CancellationToken cancellationToken = default);
        Task<List<UnitInfoSalePromotionExpenseDTO>> GetUnitInfoPromotionExpensesAsync(Guid unitID, CancellationToken cancellationToken = default);
        Task<UnitInfoPriceListDTO> GetPriceListAsync(Guid unitID, CancellationToken cancellationToken = default);
        Task<UnitInfoStatusCountDTO> GetUnitInfoCountAsync(Guid? projectID, CancellationToken cancellationToken = default);
        Task<string> CheckPrebookAsync(Guid? unitID);

    }
}
