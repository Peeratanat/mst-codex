using Base.DTOs.MST;
using Database.Models;
using Database.Models.SAL;
using ErrorHandling;
using System.Threading.Tasks;
using static Database.Models.ExtensionAttributes; 
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Database.Models.PRM;

namespace Base.DTOs.ADM
{
    public class AdminTransferPromotionDTO : BaseDTOFromAdmin
    {
        [DeviceInformation("เลขที่ใบจอง", "")]
        public string BookingNo { get; set; }

        [DeviceInformation("เลขที่โปรโมชั่น", "")]
        public string TransferPromotionNo { get; set; }

        [DeviceInformation("โครงการ", "")]
        public string Project { get; set; }

        [DeviceInformation("แปลง", "")]
        public string UnitNo { get; set; }

        [DeviceInformation("โอนกรรมสิทธิ์ภายในวันที่...", "PRM.TransferPromotion.TransferDateBefore")]
        public DateTime? TransferDateBefore { get; set; }

        [DeviceInformation("วันที่ให้โปรโมชั่นโอน", "PRM.TransferPromotion.TransferPromotionDate")]
        public DateTime? TransferPromotionDate { get; set; }

        [DeviceInformation("TransferPromotion ID", "")]
        public Guid? Id { get; set; } = new Guid();

        public static AdminTransferPromotionDTO CreateFromModel(TransferPromotion model )
        {
            if (model != null)
            {
                AdminTransferPromotionDTO result = new AdminTransferPromotionDTO ();
                result.Id = model.ID;
                result.BookingNo = model.Booking.BookingNo; 
                result.Project = model.Booking.Project.ProjectNo + "-" + model.Booking.Project.ProjectNameTH;
                result.UnitNo = model.Booking.Unit.UnitNo;
                result.TransferDateBefore = model.TransferDateBefore;
                result.TransferPromotionDate = model.TransferPromotionDate;
                result.TransferPromotionNo = model.TransferPromotionNo;
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateUpdateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            //// เช็คว่ามีการนำไปใช้หรือยัง
            //var GlHeader = await db.PostGLHeaders.Where(x => x.ReferentID == this.Id && x.IsCancel == false).CountAsync();
            //if (GlHeader >= 1)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    var msg = "ไม่สามารถแก้ไขได้ เนืองจาก Post JV แล้ว";
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void UpdateToModel(ref TransferPromotion model)
        {
            model.TransferDateBefore = this.TransferDateBefore;
            model.TransferPromotionDate = this.TransferPromotionDate; 
        } 
    }
}
