using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using PagingExtensions;
using PRJ_Budget.Params.Filters;
using PRJ_Budget.Params.Outputs;

namespace PRJ_Budget.Services
{
    public interface IBudgetMinPriceService : BaseInterfaceService
    {
        Task<BudgetMinPricePaging> GetBudgetMinPriceListAsync(BudgetMinPriceFilter filter, PageParam pageParam, BudgetMinPriceSortByParam sortByParam,CancellationToken cancellationToken = default);
        Task<BudgetMinPriceDTO> SaveBudgetMinPriceAsync(BudgetMinPriceFilter filter, BudgetMinPriceDTO input);
        Task SaveBudgetMinPriceUnitListAsync(BudgetMinPriceListDTO inputs, Guid userID, bool SaveTranfer);
        Task<BudgetMinPriceUnitDTO> SaveBudgetMinPriceUnitAsync(BudgetMinPriceFilter filter, BudgetMinPriceUnitDTO input);
        Task<BudgetMinPriceQuarterlyDTO> ImportQuarterlyBudgetAsync(FileDTO input, bool notChkAmountZero);
        Task ConfirmImportQuarterlyBudgetAsync(BudgetMinPriceQuarterlyDTO input, Guid userID);
        Task<FileDTO> ExportQuarterlyBudgetAsync(BudgetMinPriceFilter filter,CancellationToken cancellationToken = default);
        Task<BudgetMinPriceTransferDTO> ImportTransferBudgetAsync(FileDTO input, bool notChkAmountZero);
        Task ConfirmImportTransferBudgetAsync(BudgetMinPriceTransferDTO inputs);
        Task<FileDTO> ExportTransferBudgetAsync(BudgetMinPriceFilter filter,CancellationToken cancellationToken = default);
    }
}
