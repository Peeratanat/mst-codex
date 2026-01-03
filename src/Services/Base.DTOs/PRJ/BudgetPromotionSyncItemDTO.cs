using Database.Models;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class BudgetPromotionSyncItemDTO : BaseDTO
    {
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=BudgetPromotionSyncStatus
        /// สถานะการ Sync BudgetPromotion 
        /// </summary>
        public MST.MasterCenterDropdownDTO Status { get; set; }
        /// <summary>
        /// Result
        /// </summary>
        public PRJ.BudgetPromotionSyncItemResultDTO Result { get; set; }

        public async static Task<BudgetPromotionSyncItemDTO> CreateFromModelAsync (BudgetPromotionSyncItem model, DatabaseContext db)
        {
            if (model != null)
            {
                var resultSyncItem = await db.BudgetPromotionSyncItemResults.Where(o => o.BudgetPromotionSyncItemID == model.ID).FirstOrDefaultAsync();
                var result = new BudgetPromotionSyncItemDTO()
                {
                    Id = model.ID,
                    Status = MST.MasterCenterDropdownDTO.CreateFromModel(model.BudgetPromotionSyncStatus),
                    Result = BudgetPromotionSyncItemResultDTO.CreateFromModel(resultSyncItem),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
