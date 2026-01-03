using Base.DTOs.MST;
using Database.Models;
using Database.Models.SAL;
using ErrorHandling;
using System.Threading.Tasks;
using static Database.Models.ExtensionAttributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Base.DTOs.USR;
using Base.DTOs.PRJ;

namespace Base.DTOs.ADM
{
    public class AdminAgreementDTO : BaseDTOFromAdmin
    {
        [DeviceInformation("เลขที่ใบจอง", "")]
        public string BookingNo { get; set; }

        [DeviceInformation("โครงการ", "")]
        public ProjectDropdownDTO Project { get; set; }

        [DeviceInformation("แปลง", "")]
        public string UnitNo { get; set; }

        [DeviceInformation("เลขที่สัญญา", "")]
        public string AgreementNo { get; set; }

        [DeviceInformation("วันที่ทำสัญญา", "")]
        public DateTime? ContractDate { get; set; }

        [DeviceInformation("วันที่โอนกรรมสิทธิ์", "")]
        public DateTime? TransferOwnershipDate { get; set; }

        [DeviceInformation("วันที่ LC ลงนาม", "")]
        public DateTime? SignContractRequestDate { get; set; }

        [DeviceInformation("วันที่ Approve Sing Contract", "")]
        public DateTime? SignContractApprovedDate { get; set; }

        [DeviceInformation("ประเภทพนักงานปิดการขาย", "SAL.Booking.SaleOfficerTypeMasterCenterID")]
        public MasterCenterDropdownDTO SaleOfficerType { get; set; }

        [DeviceInformation("พนักงานปิดการขาย", "SAL.Booking.SaleUserID")]
        public UserListDTO SaleUser { get; set; }

        [DeviceInformation("Agency", "SAL.Booking.AgentID")]
        public AgentDropdownDTO Agent { get; set; }

        [DeviceInformation("ชื่อผู้ขาย (Agency)", "SAL.Booking.AgentEmployeeID")]
        public AgentEmployeeDropdownDTO AgentEmployee { get; set; }

        [DeviceInformation("พนักงานประจำโครงการ", "SAL.Booking.ProjectSaleUserID")]
        public UserListDTO ProjectSaleUser { get; set; }

        [DeviceInformation("สถานะสัญญา", "")]
        public MasterCenterDropdownDTO AgreementStatus { get; set; }

        [DeviceInformation("AgreementID", "")]
        public Guid? Id { get; set; } = new Guid();

        public static AdminAgreementDTO CreateFromModel(Agreement model)
        {
            if (model != null)
            {
                AdminAgreementDTO result = new AdminAgreementDTO {
                Id                       = model.ID,
                ProjectSaleUser          = UserListDTO.CreateFromModel( model.Booking.ProjectSaleUser),
                AgentEmployee            = AgentEmployeeDropdownDTO.CreateFromModel(model.Booking.AgentEmployee),
                Agent                    = AgentDropdownDTO.CreateFromModel(model.Booking.Agent),
                SaleUser                 = UserListDTO.CreateFromModel( model.Booking.SaleUser),
                SaleOfficerType          = MasterCenterDropdownDTO.CreateFromModel(model.Booking.SaleOfficerType),
                SignContractApprovedDate = model.SignContractApprovedDate,
                SignContractRequestDate  = model.SignContractRequestDate,
                TransferOwnershipDate    = model.TransferOwnershipDate,
                ContractDate             = model.ContractDate,
                AgreementNo              = model.AgreementNo,
                UnitNo                   = model.Unit.UnitNo,
                Project                  = ProjectDropdownDTO.CreateFromModel(model.Project),
                BookingNo                = model.Booking.BookingNo,
                AgreementStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.AgreementStatus),
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
            // สามารถแก้ได้เฉพาะ รอทำสัญญา รอลงนาม
            if (!this.AgreementStatus.Key.Equals(AgreementStatusKeys.WaitingForContract) && !this.AgreementStatus.Key.Equals(AgreementStatusKeys.WaitingForSignContract))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                var msg = "ไม่สามารถแก้ไขได้ เนืองจาก สถานะต้องเป็น รอทำสัญญา หรือ รอลงนาม";
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void UpdateToModel(ref Agreement model)
        {
            model.Booking.ProjectSaleUserID = this.ProjectSaleUser?.Id;
            model.Booking.AgentEmployeeID = this.AgentEmployee?.Id;
            model.Booking.AgentID = this.Agent?.Id;
            model.Booking.SaleUserID = this.SaleUser?.Id;
            model.Booking.SaleOfficerTypeMasterCenterID = this.SaleOfficerType?.Id;
            model.SignContractApprovedDate = this.SignContractApprovedDate;
            model.SignContractRequestDate = this.SignContractRequestDate;
            if (this.TransferOwnershipDate != null)
            {
                model.TransferOwnershipDate = this.TransferOwnershipDate.GetValueOrDefault().Date;
            }
            if (this.ContractDate != null)
            {
                model.ContractDate = this.ContractDate.GetValueOrDefault().Date;
            }
        } 
    }
}
