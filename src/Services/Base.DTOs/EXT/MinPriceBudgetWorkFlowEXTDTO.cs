using Base.DTOs.SAL;
using Database.Models;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.EXT
{
    public class MinPriceBudgetWorkflowEXTDTO : BaseDTO
    {
        /// <summary>
        /// สามารถ Approve ได้มั้ย
        /// </summary>
        public bool CanApprove { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// Stage (จอง, สัญญา)
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceBudgetWorkflowStage { get; set; }
        /// <summary>
        /// ราคาขาย
        /// </summary>
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// Min Price จาก Master Data
        /// </summary>
        public decimal MasterMinPrice { get; set; }
        /// <summary>
        /// Min Price ที่ขอ Approve
        /// </summary>
        public decimal RequestMinPrice { get; set; }
        /// <summary>
        /// ต่ำ Min Price
        /// </summary>
        public decimal LowerMinPrice { get; set; }
        /// <summary>
        /// Type ของ MinPrice flow (eg. Admin > 5%)
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceWorkflowType { get; set; }
        /// <summary>
        /// Budget Promotion จาก Master Data
        /// </summary>
        public decimal MasterBudgetPromotion { get; set; }
        /// <summary>
        /// Budget Promotion ที่ขอ Approve 
        /// </summary>
        public decimal RequestBudgetPromotion { get; set; }
        /// <summary>
        /// ชนิดของโปรโมชั่นที่ขอ
        /// </summary>
        public MST.MasterCenterDropdownDTO BudgetPromotionType { get; set; }
        /// <summary>
        /// Wait
        /// </summary>
        public string Wait { get; set; }
        /// <summary>
        /// Rolecode
        /// </summary>
        public string Rolecode { get; set; }
        /// <summary>
        /// การอนุมัติ (null = รอนุมัติ/ false = ไม่อนุมัติ/ true = อนุมัติ)
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// เหตุผลการ Reject
        /// </summary>
        public string RejectComment { get; set; }
        /// <summary>
        /// แปลงใหม่
        /// </summary>
        public PRJ.UnitDropdownDTO NewUnit { get; set; }

        public static async Task<MinPriceBudgetWorkflowEXTDTO> CreateFromQueryResultAsync(BudgetMinPriceWorkFlowQueryResult model, DatabaseContext db, Guid? userID)
        {
            if (model != null)
            {
                var result = new MinPriceBudgetWorkflowEXTDTO()
                {
                    Id = model.MinPriceBudgetWorkflow.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.MinPriceBudgetWorkflow.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.MinPriceBudgetWorkflow.Booking.Unit),
                    MinPriceBudgetWorkflowStage = MST.MasterCenterDropdownDTO.CreateFromModel(model.MinPriceBudgetWorkflow.MinPriceBudgetWorkflowStage),
                    SellingPrice = model.MinPriceBudgetWorkflow.SellingPrice,
                    MasterMinPrice = model.MinPriceBudgetWorkflow.MasterMinPrice,
                    RequestMinPrice = model.MinPriceBudgetWorkflow.RequestMinPrice,
                    LowerMinPrice = model.MinPriceBudgetWorkflow.MasterMinPrice - model.MinPriceBudgetWorkflow.RequestMinPrice <=0 ? 0: model.MinPriceBudgetWorkflow.MasterMinPrice - model.MinPriceBudgetWorkflow.RequestMinPrice,
                    MinPriceWorkflowType = MST.MasterCenterDropdownDTO.CreateFromModel(model.MinPriceBudgetWorkflow.MinPriceWorkflowType),
                    MasterBudgetPromotion = model.MinPriceBudgetWorkflow.MasterBudgetPromotion,
                    RequestBudgetPromotion= model.MinPriceBudgetWorkflow.RequestBudgetPromotion,
                    BudgetPromotionType = MST.MasterCenterDropdownDTO.CreateFromModel(model.MinPriceBudgetWorkflow.BudgetPromotionType),
                    Wait = model.MinPriceBudgetApproval.Role.Name,
                    Rolecode= model.MinPriceBudgetApproval.Role.Code,
                    IsApproved = model.MinPriceBudgetWorkflow.IsApproved,
                    RejectComment = model.MinPriceBudgetWorkflow.RejectComment
                };
                var user = await db.Users.Where(o => o.ID == userID).FirstOrDefaultAsync();
                if (user != null)
                {
                    var userRoles = await db.UserRoles.Where(o => o.UserID == user.ID )
                                                      .Include(o => o.Role)
                                                      .ToListAsync();
                    if (userRoles.Where(o => o.Role.Code == model.MinPriceBudgetApproval.Role.Code|| (model.MinPriceBudgetApproval.Role.Code=="LCM"&&o.Role.Code=="HOCS")).Any())
                    {
                        result.CanApprove = true;
                    }
                    if (userRoles.Where(o => o.Role.Code == "Admin").Any())
                    {
                        result.CanApprove = true;
                    }
                }
                #region check ChangeUnit

                var checkChangeUnit = await db.ChangeUnitWorkflows.Where(o => o.FromBookingID == model.MinPriceBudgetWorkflow.Booking.ID && o.IsApproved==null).Include(o=>o.ToBooking).FirstOrDefaultAsync();

                if (checkChangeUnit != null)
                {
                    var newunit = await db.Units.Where(o => o.ID == checkChangeUnit.ToBooking.UnitID).FirstOrDefaultAsync();
                    result.CanApprove = false;
                    result.NewUnit = PRJ.UnitDropdownDTO.CreateFromModel(newunit);
                }

                #endregion
                    return result;
            }
            else
            {
                return null;
            }
        }

    }
}
