using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class BookingPreSalePromotionDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่นก่อนขาย
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// รายการโปรโมชั่นก่อนขาย
        /// </summary>
        public List<BookingPreSalePromotionItemDTO> Items { get; set; }

        public async static Task<BookingPreSalePromotionDTO> CreateFromModelAsync(PreSalePromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new BookingPreSalePromotionDTO()
                {
                    PromotionNo = model.PreSalePromotionNo,
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                var items = await db.PreSalePromotionItems
                    .Include(o => o.MasterPreSalePromotionItem)
                    .Include(o=>o.PreSalePromotionRequestItem)
                    .Where(o => o.PreSalePromotionID == model.ID).ToListAsync();
                result.Items = new List<BookingPreSalePromotionItemDTO>();

                if (items.Count > 0)
                {
                    result.Items.AddRange(items.Select(o => BookingPreSalePromotionItemDTO.CreateFromModel(o)).ToList());
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
