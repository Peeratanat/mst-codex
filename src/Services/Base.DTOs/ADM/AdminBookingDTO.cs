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
using Database.Models.MST;
using Database.Models.USR;
using Database.Models.PRM;
using Base.DTOs.USR;
using Base.DTOs.PRJ;
using Database.Models.PRJ;

namespace Base.DTOs.ADM
{
    public class AdminBookingDTO : BaseDTOFromAdmin
    {
        [DeviceInformation("เลขที่ใบจอง", "")]
        public string BookingNo { get; set; }

        [DeviceInformation("ราคาบ้าน", "")]
        public decimal? SellingPrice { get; set; }

        [DeviceInformation("โครงการ", "")]
        public ProjectDropdownDTO Project { get; set; }

        [DeviceInformation("แปลง", "")]
        public string UnitNo { get; set; }

        [DeviceInformation("วันที่จอง", "SAL.Booking.BookingDate")]
        public DateTime? BookingDate { get; set; }

        [DeviceInformation("วันที่อนุมัติ", "SAL.Booking.ApproveDate")]
        public DateTime? ApproveDate { get; set; }

        [DeviceInformation("วันที่นัดทำสัญญา", "SAL.Booking.ContractDueDate")]
        public DateTime? ContractDueDate { get; set; }

        [DeviceInformation("โอนกรรมสิทธิ์ภายในวันที่", "SAL.Booking.TransferOwnershipDate,PRM.SalePromotion.TransferDateBefore")]
        public DateTime? TransferOwnershipDate { get; set; }

        [DeviceInformation("เงินจอง", "SAL.UnitPrice.BookingAmount")]
        public decimal? BookingAmount { get; set; }

        [DeviceInformation("เงินทำสัญญา", "SAL.UnitPrice.ContractAmount")]
        public decimal? ContractAmount { get; set; }

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

        public Guid? Id { get; set; } = new Guid();

        public static AdminBookingDTO CreateFromModel(AdminBookingResult model )
        {
            if (model != null)
            {
                AdminBookingDTO result = new AdminBookingDTO
                {
                    BookingNo                 = model.booking.BookingNo,
                    SellingPrice              = model.UnitPrice.SellingPrice,
                    Project                   = ProjectDropdownDTO.CreateFromModel( model.Project),
                    UnitNo                    = model.Unit.UnitNo,
                    BookingDate               = model.booking.BookingDate,
                    ApproveDate               = model.booking.ApproveDate,
                    ContractDueDate           = model.booking.ContractDueDate,
                    TransferOwnershipDate     = model.Promotion.TransferDateBefore,
                    BookingAmount             = model.UnitPrice.BookingAmount,
                    ContractAmount            = model.UnitPrice.AgreementAmount,
                    SaleOfficerType           = MasterCenterDropdownDTO.CreateFromModel( model.SaleOfficerType),
                    SaleUser                  = UserListDTO.CreateFromModel( model.SaleUser),
                    Agent                     = AgentDropdownDTO.CreateFromModel(model.Agent),
                    AgentEmployee             = AgentEmployeeDropdownDTO.CreateFromModel( model.AgentEmployee),
                    ProjectSaleUser           = UserListDTO.CreateFromModel( model.ProjectSaleUser),
                    Id                        = model.booking.ID,
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
            // เช็คว่ามีการนำไปใช้หรือยัง
            var AgreementModel = await db.Agreements.Where(x => x.BookingID == this.Id && x.IsCancel == false).CountAsync();
            if (AgreementModel >= 1)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                var msg = "ไม่สามารถแก้ไขได้ เนื่องจากมีสัญญาแล้ว";
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void UpdateToModel(ref Booking model , ref SalePromotion salePromotions , ref UnitPrice unitPrice)
        {
            if (this.BookingDate != null)
            {
                model.BookingDate = this.BookingDate.GetValueOrDefault().Date; 
            }
            model.ApproveDate = this.ApproveDate;
            if (this.TransferOwnershipDate != null)
            {
                model.TransferOwnershipDate = this.TransferOwnershipDate.GetValueOrDefault().Date;
                salePromotions.TransferDateBefore = this.TransferOwnershipDate.GetValueOrDefault().Date;
            }
            if (this.ContractDueDate != null)
            {
                model.ContractDueDate = this.ContractDueDate.GetValueOrDefault().Date;
            }
            model.SaleOfficerTypeMasterCenterID = this.SaleOfficerType?.Id;
            model.SaleUserID = this.SaleUser?.Id;
            model.AgentID = this.Agent?.Id;
            model.AgentEmployeeID = this.AgentEmployee?.Id;
            model.ProjectSaleUserID = this.ProjectSaleUser?.Id;
             
            unitPrice.BookingAmount = this.BookingAmount;
            unitPrice.AgreementAmount = this.ContractAmount;
        }
    }

    public class AdminBookingResult
    { 
        public Booking booking { get; set; }
        public MasterCenter SaleOfficerType { get; set; }
        public User SaleUser { get; set; }
        public Agent Agent { get; set; }
        public AgentEmployee AgentEmployee { get; set; }
        public User ProjectSaleUser { get; set; }
        public SalePromotion Promotion { get; set; }
        public UnitPrice UnitPrice { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }
    }
}
