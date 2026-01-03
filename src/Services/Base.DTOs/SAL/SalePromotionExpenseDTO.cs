using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.DTOs.SAL
{
    public class SalePromotionExpenseDTO : BaseDTO
    {
        /// <summary>
        /// ผู้รับผิดชอบ คชจ.
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ExpenseReponsibleBy
        /// </summary>
        public MST.MasterCenterDropdownDTO ExpenseReponsibleBy { get; set; }
        /// <summary>
        /// รายการ
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// จำนวน (หน่วย)
        /// </summary>
        public double? PriceUnitAmount { get; set; }
        /// <summary>
        /// หน่วย
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PriceUnit
        /// </summary>
        public MST.MasterCenterDropdownDTO PriceUnit { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย
        /// </summary>
        public decimal? PricePerUnitAmount { get; set; }
        /// <summary>
        /// ราคารวม
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// ลูกค้าจ่าย
        /// </summary>
        public decimal BuyerPayAmount { get; set; }
        /// <summary>
        /// บริษัทจ่าย
        /// </summary>
        public decimal SellerPayAmount { get; set; }
        /// <summary>
        /// รายการชำระเงิน
        /// </summary>
        public MST.MasterPriceItemDTO MasterPriceItem { get; set; }
        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }

        public static SalePromotionExpenseDTO CreateFromModel(SalePromotionExpense model, Guid unitPriceID, DatabaseContext db)
        {
            if (model != null)
            {
                SalePromotionExpenseDTO result = new SalePromotionExpenseDTO()
                {
                    Id = model.ID,
                    ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(model.ExpenseReponsibleBy),
                    Name = model.MasterPriceItem.Detail,
                    Amount = model.Amount,
                    BuyerPayAmount = model.BuyerAmount,
                    SellerPayAmount = model.SellerAmount,
                    MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(model.MasterPriceItem)
                };

                var item = db.UnitPriceItems.Include(o => o.PriceUnit).Where(o => o.UnitPriceID == unitPriceID && o.MasterPriceItemID == model.MasterPriceItemID).FirstOrDefault();
               
                if (item !=null)
                {
                    result.PriceUnitAmount = item?.PriceUnitAmount;
                    result.PricePerUnitAmount = item?.PricePerUnitAmount;
                    result.PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(item.PriceUnit);
                    result.Order = item.Order;
                }
                else
                {
                    result.PriceUnitAmount = null;
                    result.PricePerUnitAmount = null;
                    result.PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(null);
                    result.Order = 0;
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
