using System;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// การตั้งเรื่องย้ายแปลงจอง
    /// </summary>
    public class BookingChangeUnitWorkflowDTO
    {
        public Guid? ChangeUnitWorkflowID { get; set; }
        /// <summary>
        /// วันที่ตั้งเรื่อง
        /// </summary>
        public DateTime? Created { get; set; }
        /// <summary>
        /// ผู้ตั้งเรื่อง
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Booking ต้นทาง
        /// </summary>
        public BookingDTO FromBooking { get; set; }
        /// <summary>
        /// Quotation ปลายทาง
        /// </summary>
        public QuotationDTO ToQuotation { get; set; }

        /// <summary>
        /// ผู้อนุมัติตั้งเรื่อง
        /// </summary>
        public UserListDTO RequestApproverUser { get; set; }
        /// <summary>
        /// วันที่อนุมัติตั้งเรื่อง
        /// </summary>
        public DateTime? RequestApprovedDate { get; set; }
        /// <summary>
        /// สถานะอนุมัติตั้งเรื่อง
        /// </summary>
        public bool? IsRequestApproved { get; set; }
        /// <summary>
        /// เหตุผลที่ไม่อนุมัติตั้งเรื่อง
        /// </summary>
        public string RequestRejectComment { get; set; }

        /// <summary>
        /// ผู้อนุมัติ
        /// </summary>
        public UserListDTO ApproverUser { get; set; }
        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? ApprovedDate { get; set; }
        /// <summary>
        /// สถานะอนุมัติ
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// เหตุผลที่ไม่อนุมัติ
        /// </summary>
        public string RejectComment { get; set; }

        /// <summary>
        /// สามารถอนุมัติได้
        /// </summary>
        public bool CanApprove { get; set; }
        /// <summary>
        /// สามารถยกเลิกได้
        /// </summary>
        public bool CanCancel { get; set; }

        public async static Task<BookingChangeUnitWorkflowDTO> CreateFromModelAsync(ChangeUnitWorkflow model, DatabaseContext db, Guid? userID = null)
        {
            if (model != null)
            {
                var result = new BookingChangeUnitWorkflowDTO()
                {
                    ChangeUnitWorkflowID = model.ID,
                    Created = model.Created,
                    CreatedBy = model.CreatedBy?.DisplayName,
                    FromBooking = await BookingDTO.CreateFromModelAsync(model.FromBooking, db),
                    ToQuotation = QuotationDTO.CreateFromModel(model.ToBooking.Quotation),
                    RequestApproverUser = UserListDTO.CreateFromModel(model.RequestApproverUser),
                    RequestApprovedDate = model.RequestApprovedDate,
                    IsRequestApproved = model.IsRequestApproved,
                    RequestRejectComment = model.RequestRejectComment,
                    ApproverUser = UserListDTO.CreateFromModel(model.ApproverUser),
                    ApprovedDate = model.ApprovedDate,
                    IsApproved = model.IsApproved,
                    RejectComment = model.RejectComment,
                };

                result.CanCancel = userID != null && model.CreatedByUserID == userID && model.IsApproved == null;
                var roleIDs = await db.UserRoles.Where(o => o.UserID == userID).Select(o => o.RoleID).ToListAsync();
                var roleIDsw = await db.UserRoles.Include(o=>o.Role).Where(o => o.UserID == userID).ToListAsync();
                var hasWaitingPriceList =
                    await db.PriceListWorkflows.Where(o => o.IsApproved == null && o.ChangeUnitWorkflowID == model.ID).AnyAsync();
                var hasWaitingMinPrice = 
                    await db.MinPriceBudgetWorkflows.Where(o => o.IsApproved == null && o.ChangeUnitWorkflowID == model.ID).AnyAsync();

                result.CanApprove = roleIDs.Contains(model.RequestApproverRoleID) && model.IsApproved == null && !hasWaitingPriceList && !hasWaitingMinPrice;

                if (roleIDsw.Count != 0)
                {
                    foreach (var item in roleIDsw)
                    {
                        if ((item.Role.Code == "Admin" || item.Role.Code == "CRMREVO_Admin") && model.IsApproved == null && !hasWaitingPriceList && !hasWaitingMinPrice)
                        {
                            result.CanApprove = true;
                        }
                    }
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
