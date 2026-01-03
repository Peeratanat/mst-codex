using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class BudgetPromotionSyncItemResultDTO : BaseDTO
    {
        /// <summary>
        /// Error Description
        /// </summary>
        public string ErrorDescription { get; set; }
        /// <summary>
        /// Error Code
        /// </summary>
        public string ErrorCode { get; set; }
        public static BudgetPromotionSyncItemResultDTO CreateFromModel(BudgetPromotionSyncItemResult model)
        {
            if (model != null)
            {
                var result = new BudgetPromotionSyncItemResultDTO()
                {
                    Id = model.ID,
                    ErrorDescription = model.ErrorDescription,
                    ErrorCode = model.ErrorCode,
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
