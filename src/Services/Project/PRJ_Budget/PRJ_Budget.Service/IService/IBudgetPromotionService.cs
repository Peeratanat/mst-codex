using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_Budget.Services;
using PRJ_Budget.Params.Filters;
using PRJ_Budget.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_Budget.Services
{
    public interface IBudgetPromotionService : BaseInterfaceService
    {
        Task<BudgetPromotionPaging> GetBudgetPromotionListAsync(Guid projectID, BudgetPromotionFilter filter, PageParam pageParam, BudgetPromotionSortByParam sortByParam,CancellationToken cancellationToken = default);
        Task<BudgetPromotionDTO> GetBudgetPromotionAsync(Guid projectID, Guid unitID,CancellationToken cancellationToken = default);
        Task<BudgetPromotionDTO> CreateBudgetPromotionAsync(Guid projectID, BudgetPromotionDTO input);
        Task<BudgetPromotionDTO> UpdateBudgetPromotionAsync(Guid projectID, Guid unitID, BudgetPromotionDTO input);
        Task DeleteBudgetPromotionAsync(Guid projectID, Guid unitID);
        Task<BudgetPromotionExcelDTO> ImportBudgetPromotionAsync(Guid projectID, FileDTO input, Guid? UserID = null);
        Task<FileDTO> ExportExcelBudgetPromotionAsync(Guid projectID,CancellationToken cancellationToken = default);

    }
}
