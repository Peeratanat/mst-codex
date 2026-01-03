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
    /// รายละเอียดใบส่งมอบโปรโอน
    /// </summary>
    public class TransferPromotionDeliveryDTO : BaseDTO
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
        public List<TransferPromotionDeliveryItemDTO> DeliveryItems { get; set; }

        public List<FileDTO> FileList { get; set; } 

        public static async Task<TransferPromotionDeliveryDTO> CreateFromModelAsync(TransferPromotionDelivery model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new TransferPromotionDeliveryDTO
                {
                    Id = model.ID,
                    DeliveryNo = model.DeliveryNo,
                    RequestNo = model.TransferPromotionRequest.RequestNo,
                    DeliveryDate = model.DeliveryDate,
                    PrintCount = model.PrintCount,
                    PrintDate = model.PrintDate
                };

                var agreement = await DB.Agreements
                   .Include(o => o.Booking)
                       .Include(o => o.Project)
                        .ThenInclude(o => o.ProductType)
                   .Where(o => o.BookingID == model.TransferPromotionRequest.TransferPromotion.BookingID)
                   .FirstOrDefaultAsync();

                result.Agreement = await SAL.AgreementDTO.CreateFromModelAsync(agreement, null, DB);
                result.Updated = model?.Updated;
                result.UpdatedBy = result?.UpdatedBy == null ? model.CreatedBy?.EmployeeNo + " - " + model.CreatedBy?.DisplayName : model.UpdatedBy?.EmployeeNo + " - " + model.UpdatedBy?.DisplayName;

                var itemResults = await DB.TransferPromotionDeliveryItems
                                 .Include(o => o.TransferPromotionDelivery)
                                 .Include(o => o.TransferPromotionRequestItem)
                                 .Where(o => o.TransferPromotionDeliveryID == model.ID).ToListAsync();


                var masterItemResults = await DB.TransferPromotionRequestItems
                                    .Where(o => o.TransferPromotionRequestID == model.TransferPromotionRequestID).ToListAsync();

                result.DeliveryItems = new List<TransferPromotionDeliveryItemDTO>();

                foreach (var item in itemResults)
                {
                    result.DeliveryItems.Add(await TransferPromotionDeliveryItemDTO.CreateFromModelAsync(item, DB));
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
