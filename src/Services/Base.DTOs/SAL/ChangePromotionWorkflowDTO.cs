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
    public class ChangePromotionWorkflowDTO : BaseDTO
    {
        /// <summary>
        /// ผู้ขอเปลี่ยนแปลง
        /// </summary>
        public USR.UserListDTO RequestByUser { get; set; }
        /// <summary>
        /// ชนิดของโปรโมชั่นที่ขอเปลี่ยนแปลง
        /// </summary>
        public MST.MasterCenterDropdownDTO PromotionType { get; set; }
        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? ApproveDate { get; set; }
        /// <summary>
        /// สถานะอนุมัติ
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// โปรโมชั่นที่ขอเปลี่ยนแปลง
        /// </summary>
        public UnitInfoSalePromotionDTO SalePromotion { get; set; }
        /// <summary>
        /// ค่าใช้จ่าย
        /// </summary>
        public List<SalePromotionExpenseDTO> Expenses { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price (กรณีติด Workflow)
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceRequestReason { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price อื่นๆ (กรณีติด Workflow)
        /// </summary>
        public string OtherMinPriceRequestReason { get; set; }

        /// <summary>
        /// สิทธิยกเลิก
        /// </summary>
        public bool? CanCancel { get; set; }

        /// <summary>
        /// สิทธิอนุมัติLCM
        /// </summary>
        public bool? LCMCanAppvoe { get; set; }

        /// <summary>
        /// สิทธิอนุมัติAG
        /// </summary>
        public bool? AGCanAppvoe { get; set; }

        /// <summary>
        /// สิทธิRejectLCM
        /// </summary>
        public bool? LCMCanReject { get; set; }

        /// <summary>
        /// สิทธิRejectAG
        /// </summary>
        public bool? AGCanReject { get; set; }

        /// <summary>
        /// สิทธิAppvoeAG
        /// </summary>
        public bool? AGcanAppvoePrint { get; set; }

        /// <summary>
        /// สิทธิRejectAG
        /// </summary>
        public bool? AGCanRejectPrint { get; set; }

        /// <summary>
        /// PriceList
        /// </summary>
        public AgreementPriceListDTO PriceList { get; set; }

        /// <summary>
        /// วันที่นัดลูกค้าเซ็นเอกสาร
        /// </summary>
        public DateTime? DueSignDocDate { get; set; }

        public async static Task<ChangePromotionWorkflowDTO> CreateFromModelAsync(ChangePromotionWorkflow model, DatabaseContext DB,Guid? userID=null)
        {
            if (model != null)
            {
                var result = new ChangePromotionWorkflowDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    RequestByUser = USR.UserListDTO.CreateFromModel(model.RequestByUser),
                    PromotionType = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionType),
                    ApproveDate = model.ApproveDate,
                    DueSignDocDate =model.DueSignDocDate,
                    IsApproved = model.IsApproved,
                    LCMCanAppvoe = model.IsApproved == null ? true : false,
                    LCMCanReject = model.IsApproved == null ? true : false,
                    AGcanAppvoePrint = model.IsApproved == true ? true : false,
                    AGCanRejectPrint = model.IsApproved == true ? true : false,
                    AGCanAppvoe = model.IsPrintApproved == true ? true : false,
                    AGCanReject = model.IsApproved == true ? true : false
                }; 

                var SalePromotion = await DB.SalePromotions
                    .Include(o => o.MasterPromotion)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.ChangePromotionWorkflowID == model.ID).FirstOrDefaultAsync();
                result.SalePromotion = await UnitInfoSalePromotionDTO.CreateFromModelAsync(SalePromotion, DB);

                result.Expenses = new List<SalePromotionExpenseDTO>();
                if (SalePromotion != null)
                {
                    var bookingID = await DB.Bookings.Where(o => o.ID == SalePromotion.BookingID).Select(o => o.ID).FirstAsync();
                    var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingID && o.IsActive==false ).OrderByDescending(o=>o.Created).Select(o => o.ID).FirstAsync();
                    var bookingExpense = await DB.SalePromotionExpenses
                        .Include(o => o.MasterPriceItem)
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == SalePromotion.ID).ToListAsync();
                    result.Expenses = bookingExpense.Select(o => SalePromotionExpenseDTO.CreateFromModel(o, unitPrice, DB)).ToList();
                }
                result.CanCancel = userID != null && model.CreatedByUserID == userID && model.IsApproved == null;
                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<ChangePromotionWorkflowDTO> CreateFromModelAsync2(ChangePromotionWorkflow model, DatabaseContext DB, Guid? userID = null)
        {
            if (model != null)
            {
                var result = new ChangePromotionWorkflowDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    RequestByUser = USR.UserListDTO.CreateFromModel(model.RequestByUser),
                    PromotionType = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionType),
                    ApproveDate = model.ApproveDate,
                    IsApproved = model.IsApproved
                };

                var SalePromotion = await DB.SalePromotions
                    .Include(o => o.MasterPromotion)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.ChangePromotionWorkflowID == model.ID).FirstOrDefaultAsync();
                result.SalePromotion = await UnitInfoSalePromotionDTO.CreateFromModelAsync(SalePromotion, DB);

                result.Expenses = new List<SalePromotionExpenseDTO>();
                if (SalePromotion != null)
                {
                    var bookingID = await DB.Bookings.Where(o => o.ID == SalePromotion.BookingID).Select(o => o.ID).FirstAsync();
                    var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingID ).Select(o => o.ID).FirstAsync();
                    var bookingExpense = await DB.SalePromotionExpenses
                        .Include(o => o.MasterPriceItem)
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == SalePromotion.ID).ToListAsync();
                    result.Expenses = bookingExpense.Select(o => SalePromotionExpenseDTO.CreateFromModel(o, unitPrice, DB)).ToList();
                }
                result.CanCancel = userID != null && model.CreatedByUserID == userID && model.IsApproved == null;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
