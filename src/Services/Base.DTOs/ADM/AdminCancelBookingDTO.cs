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

namespace Base.DTOs.ADM
{
    public class AdminCancelBookingDTO : BaseDTOFromAdmin
    {
        [DeviceInformation("เลขที่ใบจอง", "")]
        public string BookingNo { get; set; }

        [DeviceInformation("ราคาบ้าน", "")]
        public decimal? SellingPrice { get; set; } 

        [DeviceInformation("โครงการ", "")]
        public string Project { get; set; }

        [DeviceInformation("แปลง", "")]
        public string UnitNo { get; set; }

        [DeviceInformation("เลขที่สัญญา", "")]
        public string AgreementNo { get; set; }

        [DeviceInformation("รับเงินจากลูกค้า", "")]
        public decimal? TotalReceivedAmount { get; set; }

        [DeviceInformation("แบบการยกเลิก", "SAL.CancelMemo.CancelReturnMasterCenterID")]
        public MasterCenterDropdownDTO CancelReturn { get; set; }

        [DeviceInformation("เหตุผลการยกเลิก", "SAL.CancelMemo.CancelReasonID")]
        public CancelReasonDropdownDTO CancelReason { get; set; }

        [DeviceInformation("หมายเหตุ", "SAL.CancelMemo.CancelRemark")]
        public string CancelRemark { get; set; }

        [DeviceInformation("เบี้ยปรับ", "SAL.CancelMemo.PenaltyAmount")]
        public decimal? PenaltyAmount { get; set; }

        [DeviceInformation("เงินคืนลูกค้า (สุทธิ)", "SAL.CancelMemo.ReturnAmount")]
        public decimal? ReturnAmount { get; set; }

        [DeviceInformation("CancelMemo ID", "")]
        public Guid? Id { get; set; } = new Guid();

        public static AdminCancelBookingDTO CreateFromModel(CancelMemo model, UnitPrice unitPriceModel)
        {
            if (model != null)
            {
                AdminCancelBookingDTO result = new AdminCancelBookingDTO();

                result.Id = model.ID;
                result.BookingNo = model.Booking.BookingNo;
                result.SellingPrice = unitPriceModel.SellingPrice;
                result.Project = model.Booking.Project.ProjectNo + "-" + model.Booking.Project.ProjectNameTH;
                result.UnitNo = model.Booking.Unit.UnitNo;
                result.TotalReceivedAmount = model.TotalReceivedAmount;
                result.CancelReturn = MasterCenterDropdownDTO.CreateFromModel(model.CancelReturn);
                result.CancelReason = CancelReasonDropdownDTO.CreateFromModel(model.CancelReason);
                result.CancelRemark = model.CancelRemark;
                result.PenaltyAmount = model.PenaltyAmount;
                result.ReturnAmount = model.ReturnAmount;
                result.AgreementNo = model.Agreement?.AgreementNo;
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
            // เช็คว่ามีการนำไปใช้หรือยัง
            var GlHeader = await db.PostGLHeaders.Where(x => x.ReferentID == this.Id && x.IsCancel == false).CountAsync();
            if (GlHeader >= 1)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                var msg = "ไม่สามารถแก้ไขได้ เนืองจาก Post JV แล้ว";
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void UpdateToModel(ref CancelMemo model)
        {
            model.CancelReturnMasterCenterID = this.CancelReturn?.Id;
            model.CancelReasonID = this.CancelReason?.Id;
            model.CancelRemark = this.CancelRemark;
            model.PenaltyAmount = this.PenaltyAmount;
            model.ReturnAmount = this.ReturnAmount;
        } 
    }
}
