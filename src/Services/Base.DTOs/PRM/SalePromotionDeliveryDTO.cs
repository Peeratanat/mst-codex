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
    /// รายละเอียดใบส่งมอบโปรขาย
    /// </summary>
    public class SalePromotionDeliveryDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบส่งมอบ
        /// </summary>
        public string DeliveryNo { get; set; }
        /// <summary>
        /// เลขที่ใบเบิก
        /// </summary>
        public string RequestNo { get; set; }
        /// <summary>
        /// สัญญา
        /// </summary>
        public SAL.AgreementDTO Agreement { get; set; }
        /// <summary>
        /// วันที่ส่งมอบ
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// จำนวนครั้งที่พิมพ์
        /// </summary>
        public int? PrintCount { get; set; }
        /// <summary>
        /// วันที่พิมพ์
        /// </summary>
        public DateTime? PrintDate { get; set; }

        /// <summary>
        /// รายการที่ส่งมอบ
        /// </summary>
        public List<SalePromotionDeliveryItemDTO> DeliveryItems { get; set; }

        public List<FileDTO> FileList { get; set; }

        public static async Task<SalePromotionDeliveryDTO> CreateFromModelAsync(SalePromotionDelivery model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new SalePromotionDeliveryDTO
                {
                    Id = model.ID,
                    DeliveryNo = model.DeliveryNo,
                    RequestNo = model.SalePromotionRequest.RequestNo,
                    DeliveryDate = model.DeliveryDate,
                    PrintCount = model.PrintCount,
                    PrintDate = model.PrintDate
                };

                var requestModel = await DB.SalePromotionRequests
                                .Include(o => o.CreatedBy)
                                .Include(o => o.UpdatedBy)
                                .Include(o => o.SalePromotion)
                                .ThenInclude(o => o.Booking)
                                .Where(o => o.ID == model.SalePromotionRequestID).FirstOrDefaultAsync();

                var agreement = await DB.Agreements
                  .Include(o => o.Booking)
                     .Include(o => o.Project)
                        .ThenInclude(o => o.ProductType)
                  .Where(o => o.BookingID == model.SalePromotionRequest.SalePromotion.BookingID)
                  .FirstOrDefaultAsync();

                result.Agreement = await SAL.AgreementDTO.CreateFromModelAsync(agreement, null, DB);
                result.Updated = model?.Updated;
                result.UpdatedBy = result?.UpdatedBy == null ? model.CreatedBy?.EmployeeNo + " - " + model.CreatedBy?.DisplayName : model.UpdatedBy?.EmployeeNo + " - " + model.UpdatedBy?.DisplayName;

                var itemResults = await DB.SalePromotionDeliveryItems
                                 .Include(o => o.SalePromotionDelivery)
                                 .Include(o => o.SalePromotionRequestItem)
                                 .Include(o => o.SalePromotionRequestItem.SalePromotionItem)
                                 .Where(o => o.SalePromotionDeliveryID == model.ID).ToListAsync();


                var masterItemResults = await DB.SalePromotionRequestItems
                                    .Where(o => o.SalePromotionRequestID == model.SalePromotionRequestID).ToListAsync();

                result.DeliveryItems = new List<SalePromotionDeliveryItemDTO>();
                //foreach (var item in masterItemResults)
                //{
                //    var DeliveryItem = itemResults.Where(o => o.SalePromotionRequestItemID == item.ID).FirstOrDefault();
                //    if (DeliveryItem != null)
                //    {
                //        result.DeliveryItems.Add(await SalePromotionDeliveryItemDTO.CreateFromModelAsync(DeliveryItem, DB));
                //    }
                //    //else
                //    //{
                //    //    result.DeliveryItems.Add(await SalePromotionDeliveryItemDTO.CreateFromMasterModelAsync(item, item.ID, DB));
                //    //}
                //}

                foreach(var item in itemResults)
                {
                    result.DeliveryItems.Add(await SalePromotionDeliveryItemDTO.CreateFromModelAsync(item, DB));
                }
                result.FileList = new List<FileDTO>();
                if (!string.IsNullOrEmpty(model.AttachFileName))
                {
                    var file = new FileDTO();
                    file.Name = model.AttachFileName;
                    file.Url = model.AttachFile;
                    result.FileList.Add(file);
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
