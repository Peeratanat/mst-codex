using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายละเอียดใบเบิกโปรโอน
    /// Model: TransferPromotionRequest
    /// </summary>
    public class TransferPromotionRequestDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบเบิก
        /// </summary>
        public string RequestNo { get; set; }
        /// <summary>
        /// สัญญา
        /// </summary>
        public SAL.AgreementDTO Agreement { get; set; }
        /// <summary>
        /// วันที่ทำรายการ
        /// </summary>
        public DateTime? RequestDate { get; set; }
        /// <summary>
        /// จำนวนครั้งที่พิมพ์
        /// </summary>
        public int? PrintCount { get; set; }
        /// <summary>
        /// วันที่พิมพ์
        /// </summary>
        public DateTime? PrintDate { get; set; }
        /// <summary>
        /// สถานะการเบิกโปรโมชั่น
        /// </summary>
        public MasterCenterDTO RequestStatus { get; set; }

        /// <summary>
        /// รายการที่เบิก
        /// </summary>
        public List<TransferPromotionRequestItemDTO> RequestItems { get; set; }
        /// <summary>
        /// ทำเบิกแล้วหรือยัง ?
        /// </summary>
        public bool? IsDelivery { get; set; }
        /// <summary>
        /// ทำการจ่ายในระบบสต๊อคหรือยัง ?
        /// </summary>
        public bool? IsDeliveryStock { get; set; }

        public static async Task<TransferPromotionRequestDTO> CreateFromModelAsync(TransferPromotionRequest model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new TransferPromotionRequestDTO();
                result.Id = model.ID;
                result.RequestNo = model.RequestNo;
                result.RequestDate = model.RequestDate;
                result.PrintCount = model.PrintCount;
                result.PrintDate = model.PrintDate;
                result.RequestStatus = MasterCenterDTO.CreateFromModel(model.PromotionRequestPRStatus);

                var agreement = await DB.Agreements
                        .Include(o => o.Booking)
                        .Include(o => o.Project)
                        .ThenInclude(o => o.ProductType)
                        .Where(o => o.BookingID == model.TransferPromotion.BookingID)
                        .FirstOrDefaultAsync();

                result.Agreement = await SAL.AgreementDTO.CreateFromModelAsync(agreement, null, DB);

                result.Updated = model?.Updated;
               
                if (model?.RefMigrateID1 == "AutoCreate")
                {
                    var bookingID = model?.TransferPromotion?.BookingID;

                    var agreementID = await DB.Agreements
                                .Where(o => o.BookingID == bookingID && !o.IsCancel).Select(o => o.ID).FirstOrDefaultAsync();

                    var transferSale = await DB.Transfers.Include(o => o.TransferSale)
                                        .Where(o => o.AgreementID == agreementID && o.IsTransferConfirmed).FirstOrDefaultAsync();

                    result.UpdatedBy = transferSale == null ? model.CreatedBy?.EmployeeNo + " - " + model.CreatedBy?.DisplayName : transferSale.TransferSale?.EmployeeNo + " - " + transferSale.TransferSale?.DisplayName;
                }
                else
                {
                    result.UpdatedBy = result?.UpdatedBy == null ? model.CreatedBy?.EmployeeNo + " - " + model.CreatedBy?.DisplayName : model.UpdatedBy?.EmployeeNo + " - " + model.UpdatedBy?.DisplayName;
                }

                var transferDelivery = await DB.TransferPromotionDeliveries
                  .Where(o => o.TransferPromotionRequestID == model.ID)
                  .FirstOrDefaultAsync() ?? new TransferPromotionDelivery();

                result.IsDelivery = false;
                if (!string.IsNullOrEmpty(transferDelivery.DeliveryNo))
                {
                    result.IsDelivery = true;
                }

                var itemResults = await DB.TransferPromotionRequestItems
                        .Include(o => o.TransferPromotionRequest)
                        .Include(o => o.TransferPromotionItem)
                            .ThenInclude(o => o.TransferPromotion)
                        .Include(o => o.TransferPromotionItem.MasterPromotionItem)
                        .Include(o => o.CreatedBy)
                        .Include(o => o.UpdatedBy)
                        .Where(o => o.TransferPromotionRequestID == model.ID
                                && !o.TransferPromotionItem.MainTransferPromotionItemID.HasValue)
                        .ToListAsync();

                result.RequestItems = new List<TransferPromotionRequestItemDTO>();
                foreach (var item in itemResults)
                {
                    result.RequestItems.Add(await TransferPromotionRequestItemDTO.CreateFromModelAsync(item, DB));
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
