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
using Base.DTOs.USR;
using Base.DTOs.PRJ;
using Database.Models.MasterKeys;
using Database.Models.ACC;

namespace Base.DTOs.ADM
{
    public class AdminTransferDTO : BaseDTOFromAdmin
    {
        [DeviceInformation("เลขที่ใบจอง", "")]
        public string BookingNo { get; set; }

        [DeviceInformation("โครงการ", "")]
        public ProjectDropdownDTO Project { get; set; }

        [DeviceInformation("แปลง", "")]
        public string UnitNo { get; set; }

        [DeviceInformation("เลขที่สัญญา", "")]
        public string AgreementNo { get; set; }

        [DeviceInformation("เลขที่โปรโมชั่นโอน", "")]
        public string TransferPromotionNo { get; set; }

        [DeviceInformation("เลขที่โอนกรรมสิทธิ์", "")]
        public string TransferNo { get; set; }

        [DeviceInformation("วันที่นัดโอนกรรมสิทธื์", "SAL.Transfer.ScheduleTransferDate")]
        public DateTime? ScheduleTransferDate { get; set; }

        [DeviceInformation("วันที่โอนจริง", "SAL.Transfer.ActualTransferDate")]
        public DateTime? ActualTransferDate { get; set; }

        [DeviceInformation("ภาษีเงินได้นิติบุคคล", "SAL.Transfer.CompanyIncomeTax")]
        public decimal? CompanyIncomeTax { get; set; }

        [DeviceInformation("เช็คค่ามิเตอร์", "SAL.Transfer.MeterChequeMasterCenterID")]
        public MST.MasterCenterDropdownDTO MeterCheque { get; set; }

        [DeviceInformation("ภาษีเงินได้ธุรกิจเฉพาะ", "SAL.Transfer.BusinessTax")]
        public decimal? BusinessTax { get; set; }

        [DeviceInformation("ภาษีท้องถิ่น", "SAL.Transfer.LocalTax")]
        public decimal? LocalTax { get; set; }

        [DeviceInformation("รูดบัตร P-Card (กระทรวงการคลัง)", "SAL.Transfer.MinistryPCard")]
        public decimal? MinistryPCard { get; set; }

        [DeviceInformation("เงินสด/เช็คกระทรวงการคลัง", "SAL.Transfer.MinistryCashOrCheque")]
        public decimal? MinistryCashOrCheque { get; set; }
        [DeviceInformation("เจ้าของบัตร K-Cash", "SAL.Transfer.PCardUserID")]
        public USR.UserListDTO PCardUser { get; set; }
        [DeviceInformation("รวมรับเงินสดย่อย", "SAL.Transfer.PettyCashAmount")]
        public decimal? PettyCashAmount { get; set; }
        [DeviceInformation("ค่าเดินทางไป", "SAL.Transfer.GoTransportAmount")]
        public decimal? GoTransportAmount { get; set; }
        [DeviceInformation("ค่าเดินทางกลับ", "SAL.Transfer.ReturnTransportAmount")]
        public decimal? ReturnTransportAmount { get; set; }
        [DeviceInformation("ค่าเดินทางระหว่าง สนง.ที่ดิน", "SAL.Transfer.LandOfficeTransportAmount")]
        public decimal? LandOfficeTransportAmount { get; set; }
        [DeviceInformation("ค่าทางด่วนขาไป", "SAL.Transfer.GoTollWayAmount")]
        public decimal? GoTollWayAmount { get; set; }
        [DeviceInformation("ค่าทางด่วนขากลับ", "SAL.Transfer.ReturnTollWayAmount")]
        public decimal? ReturnTollWayAmount { get; set; }
        [DeviceInformation("ค่าทางด่วนระหว่าง สนง.ที่ดิน", "SAL.Transfer.LandOfficeTollWayAmount")]
        public decimal? LandOfficeTollWayAmount { get; set; }
        [DeviceInformation("รับรองเจ้าหน้าที่", "SAL.Transfer.SupportOfficerAmount")]
        public decimal? SupportOfficerAmount { get; set; }
        [DeviceInformation("ค่าถ่ายเอกสาร/Fax", "SAL.Transfer.CopyDocumentAmount")]
        public decimal? CopyDocumentAmount { get; set; }
        [DeviceInformation("สถานะโอนกรรมสิทธิ์", "SAL.Transfer.TransferStatus")]
        public MasterCenterDropdownDTO TransferStatus { get; set; }

        [DeviceInformation("TransferID", "")]
        public Guid? Id { get; set; } = new Guid();

        public static AdminTransferDTO CreateFromModel(AdminTransferTmpResult model)
        {
            if (model != null)
            {
                AdminTransferDTO result = new AdminTransferDTO
                {
                    BookingNo = model.Booking.BookingNo,
                    Project = ProjectDropdownDTO.CreateFromModel( model.Transfer.Project),
                    UnitNo = model.Transfer.Unit.UnitNo,
                    AgreementNo = model.Agreement.AgreementNo,
                    TransferPromotionNo = model.promotionNo,
                    TransferNo = model.Transfer.TransferNo,
                    ScheduleTransferDate = model.Transfer.ScheduleTransferDate,
                    ActualTransferDate = model.Transfer.ActualTransferDate,
                    CompanyIncomeTax = model.Transfer.CompanyIncomeTax,
                    MeterCheque = MasterCenterDropdownDTO.CreateFromModel(model.Transfer.MeterCheque),
                    BusinessTax = model.Transfer.BusinessTax,
                    LocalTax = model.Transfer.LocalTax,
                    MinistryPCard = model.Transfer.MinistryPCard,
                    MinistryCashOrCheque = model.Transfer.MinistryCashOrCheque,
                    PCardUser = USR.UserListDTO.CreateFromModel(model.Transfer.PCardUser),
                    PettyCashAmount = model.Transfer.PettyCashAmount,
                    GoTransportAmount = model.Transfer.GoTransportAmount,
                    ReturnTransportAmount = model.Transfer.ReturnTransportAmount,
                    LandOfficeTransportAmount = model.Transfer.LandOfficeTransportAmount,
                    GoTollWayAmount = model.Transfer.GoTollWayAmount,
                    ReturnTollWayAmount = model.Transfer.ReturnTollWayAmount,
                    LandOfficeTollWayAmount = model.Transfer.LandOfficeTollWayAmount,
                    SupportOfficerAmount = model.Transfer.SupportOfficerAmount,
                    CopyDocumentAmount = model.Transfer.CopyDocumentAmount,
                    TransferStatus =MasterCenterDropdownDTO.CreateFromModel(model.Transfer.TransferStatus),
                    Id = model.Transfer.ID,
                };
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
            //Post KM หรือยัง 
            var PostGLHeaderModel = db.PostGLHeaders.Where(x => x.ReferentID == this.Id && x.ReferentType.Equals(PostGLDocumentTypeKeys.KM) && x.IsCancel == false).FirstOrDefault() ?? new PostGLHeader();
            if (PostGLHeaderModel.ReferentID != null)
            { 
                var errMsg = db.ErrorMessages.Where(o => o.Key == "ERR0120").FirstOrDefault();
                var msg = "ไม่สามารถแก้ไขได้เนื่องจากมีการ Post KM แล้ว";
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void UpdateToModel(ref Transfer model)
        {
            model.ScheduleTransferDate = this.ScheduleTransferDate;
            model.ActualTransferDate = this.ActualTransferDate;
            model.CompanyIncomeTax = this.CompanyIncomeTax;
            model.MeterChequeMasterCenterID = this.MeterCheque?.Id;
            model.BusinessTax = this.BusinessTax;
            model.LocalTax = this.LocalTax;
            model.MinistryPCard = this.MinistryPCard;
            model.MinistryCashOrCheque = this.MinistryCashOrCheque;
            model.PCardUserID = this.PCardUser?.Id;
            model.PettyCashAmount = this.PettyCashAmount;
            model.GoTransportAmount = this.GoTransportAmount;
            model.ReturnTransportAmount = this.ReturnTransportAmount;
            model.LandOfficeTransportAmount = this.LandOfficeTransportAmount;
            model.GoTollWayAmount = this.GoTollWayAmount;
            model.ReturnTollWayAmount = this.ReturnTollWayAmount;
            model.LandOfficeTollWayAmount = this.LandOfficeTollWayAmount;
            model.SupportOfficerAmount = this.SupportOfficerAmount;
            model.CopyDocumentAmount = this.CopyDocumentAmount;
        } 
    }
    public class AdminTransferTmpResult 
    {
        public Transfer Transfer { get; set; }
        public string promotionNo { get; set; } 
        public Agreement Agreement { get; set; }
        public Booking Booking { get; set; }
    }
}
