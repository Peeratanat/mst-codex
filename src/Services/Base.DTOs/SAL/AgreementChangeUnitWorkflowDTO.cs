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
    /// การตั้งเรื่องย้ายแปลงสัญญา
    /// </summary>
    public class AgreementChangeUnitWorkflowDTO
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
        /// Agreement ต้นทาง
        /// </summary>
        public AgreementDTO FromAgreement { get; set; }
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

        /// <summary>
        /// มี id สามารถแก้ไขได้
        /// </summary>
        public bool IsHaveId { get; set; }

        /// <summary>
        /// สามารถอนุมัติได้LCM
        /// </summary>
        public bool IscanAppoveLcm { get; set; }

        /// <summary>
        /// สามารถอนุมัติได้AG
        /// </summary>
        public bool IscanAppoveAG { get; set; }

        /// <summary>
        /// สามารถอนุมัติปริ้น
        /// </summary>
        public bool IscanPrint { get; set; }

        /// <summary>
        /// สถานะ ติด pricelist และ minprice 
        /// </summary>
        public string Wordshowstatus { get; set; }

        /// <summary>
        /// Booking ปลายทาง
        /// </summary>
        public BookingDTO ToBooking { get; set; }


        public async static Task<AgreementChangeUnitWorkflowDTO> CreateFromModelAsync(ChangeUnitWorkflow model, DatabaseContext db, Guid? userID = null)
        {
            if (model != null)
            {
                var pricelist = await db.PriceListWorkflows.Where(o => o.BookingID == model.ToBookingID && o.IsApproved == null).FirstOrDefaultAsync();
                var minprice = await db.MinPriceBudgetWorkflows.Where(o => o.BookingID == model.ToBookingID && o.IsApproved == null).FirstOrDefaultAsync();



                #region  button
                bool lcmbutton = (pricelist == null && minprice == null) && model.IsRequestApproved == null ? true : false;
                bool agbutton = (pricelist == null && minprice == null) && model.IsRequestApproved == true ? true : false;
                bool printbutton = model.IsRequestApproved == true && model.IsApproved == null ? true : false;

                #endregion
                bool isRequestApprove = (pricelist is null && minprice is null) ? model.IsRequestApproved ?? false : true;


                var result = new AgreementChangeUnitWorkflowDTO()
                {
                    ChangeUnitWorkflowID = model.ID,
                    Created = model.Created,
                    CreatedBy = model.CreatedBy?.DisplayName,
                    FromBooking = await BookingDTO.CreateFromModelAsync(model.FromBooking, db),
                    FromAgreement = await AgreementDTO.CreateFromModelAsync(model.FromAgreement, null, db),
                    ToQuotation = QuotationDTO.CreateFromModel(model.ToBooking.Quotation),
                    ToBooking = await BookingDTO.CreateFromModelAsync(model.ToBooking, db),
                    RequestApproverUser = UserListDTO.CreateFromModel(model.RequestApproverUser),
                    RequestApprovedDate = model.RequestApprovedDate,
                    IsRequestApproved = isRequestApprove,
                    RequestRejectComment = model.RequestRejectComment,
                    ApproverUser = UserListDTO.CreateFromModel(model.ApproverUser),
                    ApprovedDate = model.ApprovedDate,
                    IsApproved = model.IsApproved,
                    RejectComment = model.RejectComment,
                    IsHaveId = true,
                    IscanAppoveLcm = lcmbutton,
                    IscanAppoveAG = agbutton,
                    IscanPrint = printbutton,
                  //  Wordshowstatus = pricelist == null ? minprice == null ? "" : "(รออนุมัติ MinPrice)" : "(รออนุมัติ PriceList)",

                };

                if (pricelist != null)
                {
                    result.Wordshowstatus = "รออนุมัติ PriceList";
                }
                else if (minprice!=null)
                { 
                    result.Wordshowstatus = "รออนุมัติ MinPrice";
                }
                else if (model.IsRequestApproved == null)
                {
                    result.Wordshowstatus = "รอLCMอนุมัติ";
                }
                else if (result.IsApproved == null)
                {
                    result.Wordshowstatus = "รอนิติกรรมอนุมัติ";
                }

                result.CanCancel = userID != null && model.CreatedByUserID == userID && model.IsApproved == null;
                var roleIDs = await db.UserRoles.Where(o => o.UserID == userID).Select(o => o.RoleID).ToListAsync();
                var hasWaitingPriceList =
                    await db.PriceListWorkflows.Where(o => o.IsApproved == null && o.ChangeUnitWorkflowID == model.ID).AnyAsync();
                var hasWaitingMinPrice =
                    await db.MinPriceBudgetWorkflows.Where(o => o.IsApproved == null && o.ChangeUnitWorkflowID == model.ID).AnyAsync();
                result.CanApprove = roleIDs.Contains(model.RequestApproverRoleID) && model.IsApproved == null && !hasWaitingPriceList && !hasWaitingMinPrice;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
