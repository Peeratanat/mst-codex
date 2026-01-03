using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
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
    /// รายละเอียดใบเบิกโปรขาย
    /// Model: SalePromotionRequest
    /// </summary>
    public class SalePromotionRequestDTO : BaseDTO
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
        public List<SalePromotionRequestItemDTO> RequestItems { get; set; }
        /// <summary>
        /// ทำเบิกแล้วหรือยัง ?
        /// </summary>
        public bool? IsDelivery { get; set; }
        /// <summary>
        /// ทำการจ่ายในระบบสต๊อคหรือยัง ?
        /// </summary>
        public bool? IsDeliveryStock { get; set; }
        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<SalePromotionItemDTO> SubItems { get; set; }

        public static async Task<SalePromotionRequestDTO> CreateFromModelAsync(SalePromotionRequest model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new SalePromotionRequestDTO
                {
                    Id = model.ID,
                    RequestNo = model.RequestNo,
                    RequestDate = model.RequestDate,
                    PrintCount = model.PrintCount,
                    PrintDate = model.PrintDate,
                    RequestStatus = MasterCenterDTO.CreateFromModel(model.PromotionRequestPRStatus)
                };

                var agreement = await DB.Agreements
                  .Include(o => o.Booking)
                  .Include(o => o.Project)
                        .ThenInclude(o => o.ProductType)
                  .Where(o => o.BookingID == model.SalePromotion.BookingID)
                  .FirstOrDefaultAsync();

                result.Agreement = await SAL.AgreementDTO.CreateFromModelAsync(agreement, null, DB);

                result.Updated = model?.Updated;

                if (model?.RefMigrateID1 == "AutoCreate")
                {
                    var bookingID = model?.SalePromotion?.BookingID;

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

                var saleDelivery = await DB.SalePromotionDeliveries
                  .Where(o => o.SalePromotionRequestID == model.ID)
                  .FirstOrDefaultAsync() ?? new SalePromotionDelivery();

                result.IsDelivery = false;
                if (!string.IsNullOrEmpty(saleDelivery.DeliveryNo))
                {
                    result.IsDelivery = true;
                }

                var itemResults = await DB.SalePromotionRequestItems
                        .Include(o => o.SalePromotionRequest)
                        .Include(o => o.SalePromotionItem)
                            .ThenInclude(o => o.SalePromotion)  //
                        .Include(o => o.SalePromotionItem.MasterPromotionItem)
                        .Include(o => o.CreatedBy)
                        .Include(o => o.UpdatedBy)
                        .Where(o => o.SalePromotionRequestID == model.ID
                                && !o.SalePromotionItem.MainSalePromotionItemID.HasValue)
                        .ToListAsync();

                result.RequestItems = new List<SalePromotionRequestItemDTO>();
                foreach (var item in itemResults)
                {
                    result.RequestItems.Add(await SalePromotionRequestItemDTO.CreateFromModelAsync(item, DB));
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
