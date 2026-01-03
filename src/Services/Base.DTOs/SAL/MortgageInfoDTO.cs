using Database.Models.SAL;
using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.MST;
using Database.Models;
using ErrorHandling;
using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Base.DTOs.USR;
using Database.Models.PRJ;

namespace Base.DTOs.SAL
{
    public class MortgageInfoDTO
    {
        [Description("ใบจอง")]
        public BookingDTO Booking { get; set; }

        [Description("สัญญา")]
        public AgreementDTO Agreement { get; set; }

        [Description("วันทีโอนกรรมสิทธิ์จริง")]
        public DateTime? ActualTransferDate { get; set; }

        [Description("ราคาในสัญญา")]
        public AgreementPriceListDTO AgreementPriceList { get; set; }

        [Description("ราคาในจอง")]
        public BookingPriceListDTO BookingPriceList { get; set; }

        [Description("ผู้ทำสัญญา")]
        public AgreementOwnerDTO AgreementOwner { get; set; }

        [Description("ผู้จอง")]
        public BookingOwnerDTO BookingOwner { get; set; }

        [Description("สถานะสัญญา")]
        public bool IsAgreementStage { get; set; }


        [Description("ราคาพื่นที่/หน่วย")]
        public decimal? OffsetAreaUnitPrice { get; set; }
        /// <summary>
        /// พื้นที่เพิ่มลด ค่าบวก = พื้นที่โฉนด > พื้นที่ขาย
        /// </summary>
        public double? OffsetArea { get; set; }
        /// <summary>
        /// ราคาขายสุทธิ
        /// </summary>
        public decimal? NetSellingPrice { get; set; }
        /// <summary>
        /// LCM
        /// GET Identity/api/Users?roleCodes=LCM&authorizeProjectIDs=7{projectID}
        /// </summary>
        public USR.UserListDTO LCMUser { get; set; }
        /// <summary>
        /// เงินโอน
        /// </summary>
        public decimal? transferAmount { get; set; }

        /// <summary>
        /// มีการโอนแล้ว
        /// </summary>
        public bool IsTransferConfirmed { get; set; }

        public async static Task<MortgageInfoDTO> CreateFromModelAsync(Guid unitId, DatabaseContext DB)
        {
            try
            {

                if (unitId != null)
                {
                    Transfer transfer = null;
                    Agreement agreement = null;
                    AgreementOwner agreementOwner = null;
                    Booking booking = null;
                    BookingOwner bookingOwner = null;
                    Guid? ProjectId = null;
                    PriceList priceList = null;
                    TitledeedDetail titledeedDetail = null;

                    var unit = await DB.Units
                        .Include(o => o.TitledeedDetails)
                                .Where(o => o.ID == unitId).FirstOrDefaultAsync();

                    if (unit != null)
                    {
                        ProjectId = unit.ProjectID;
                    }

                    booking = await DB.Bookings
                                .Include(o => o.Agent)
                                .Include(o => o.ProjectSaleUser)
                                .Include(o => o.AgentEmployee)
                                .Include(o => o.Quotation)
                                .Include(o => o.UpdatedBy)
                                .Include(o => o.BookingStatus)
                                .Include(o => o.SaleUser)
                                .Include(o => o.Model)
                                .Include(o => o.SaleOfficerType)
                                .Include(o => o.Model.TypeOfRealEstate)
                                .Include(o => o.Project)
                                .Include(o => o.Project.ProjectStatus)
                                .Include(o => o.Project.ProductType)
                                .Include(o => o.Unit)
                                .Include(o => o.Unit.TitledeedDetails)
                                .Include(o => o.Unit.UnitStatus)
                                .Include(o => o.Unit.Floor)
                                .Include(o => o.Unit.Tower)
                                .Where(o => o.Unit.ID == unitId && o.IsCancelled == false).FirstOrDefaultAsync();


                    titledeedDetail = await DB.TitledeedDetails
                                             .Where(o => o.UnitID == unitId).FirstOrDefaultAsync();

                    priceList = await DB.PriceLists
                                .Where(o => o.UnitID == unitId).OrderByDescending(o => o.ActiveDate).FirstOrDefaultAsync();

                    agreement = await DB.Agreements
                            .Include(o => o.UpdatedBy)
                            .Include(o => o.Booking)
                            .Include(o => o.Booking.Quotation)
                            .Include(o => o.Booking.BookingStatus)
                            .Include(o => o.Booking.SaleUser)
                            .Include(o => o.Booking.ProjectSaleUser)
                            .Include(o => o.Booking.Model)
                            .Include(o => o.Booking.Agent)
                            .Include(o => o.Booking.AgentEmployee)
                            .Include(o => o.Project)
                            .ThenInclude(o => o.ProjectType)
                            .Include(o => o.Unit)
                            .Where(o => o.Unit.ID == unitId && o.IsCancel == false).FirstOrDefaultAsync();

                    transfer = await DB.Transfers
                        .Where(o => o.Unit.ID == unitId && o.IsDeleted == false).FirstOrDefaultAsync();

                    if (booking != null)
                    {
                        bookingOwner = await DB.BookingOwners
                                    .Include(o => o.National)
                                    .Include(o => o.ContactType)
                                    .Include(o => o.ContactTitleTH)
                                    .Include(o => o.ContactTitleEN)
                                    .Include(o => o.Gender)
                                    .Where(o => o.BookingID == booking.ID && o.IsMainOwner == true && o.IsAgreementOwner == false)
                                    .FirstOrDefaultAsync();


                    }

                    if (agreement != null)
                    {
                        agreementOwner = await DB.AgreementOwners
                                    .Include(o => o.National)
                                    .Include(o => o.ContactType)
                                    .Include(o => o.ContactTitleTH)
                                    .Include(o => o.ContactTitleEN)
                                    .Include(o => o.Gender)
                                    .Where(o => o.AgreementID == agreement.ID && o.IsMainOwner == true)
                                    .FirstOrDefaultAsync();
                    }

                    var result = new MortgageInfoDTO();
                    result.Booking = await SAL.BookingDTO.CreateFromModelAsync(booking, DB);
                    result.Agreement = await SAL.AgreementDTO.CreateFromModelAsync(agreement, null, DB);
                    result.ActualTransferDate = transfer?.ActualTransferDate;
                    if (transfer?.IsTransferConfirmed == true)
                    {
                        result.IsTransferConfirmed = true;
                    }

                    if (priceList != null)
                    {
                        var priceListItem = await DB.PriceListItems
                               .Where(o => o.PriceListID == priceList.ID && o.MasterPriceItemID == MasterPriceItemIDs.ExtraAreaPrice).FirstOrDefaultAsync();

                        //result.OffsetAreaUnitPrice = priceListItem?.Amount == null ? 0 : priceListItem.Amount;
                    }

                    /// พื้นที่ส่วนเพิ่ม-ลด = พื้นที่โฉนด - พื้นที่ขาย 
                    if (titledeedDetail?.TitledeedArea > 0)
                    {
                        result.OffsetArea = (titledeedDetail.TitledeedArea ?? 0) - (unit.SaleArea ?? 0);
                    }
                    else
                    {
                        result.OffsetArea = 0;
                    }

                    if (agreement != null)
                    {
                        result.AgreementPriceList = await SAL.AgreementPriceListDTO.CreateFromModelAsync(agreement.ID, DB);
                        result.AgreementOwner = await SAL.AgreementOwnerDTO.CreateFromModelAsync(agreementOwner, DB);
                        result.IsAgreementStage = true;
                        result.OffsetAreaUnitPrice = Math.Ceiling((Convert.ToDecimal(result?.OffsetArea ?? 0) * (result.Agreement?.AreaPricePerUnit ?? 0)));
                    }
                    else
                    {
                        result.BookingPriceList = await SAL.BookingPriceListDTO.CreateFromModelAsync(booking.ID, DB);
                        result.BookingOwner = await SAL.BookingOwnerDTO.CreateFromModelAsync(bookingOwner, DB);
                        result.IsAgreementStage = false;
                        result.OffsetAreaUnitPrice = Math.Ceiling((Convert.ToDecimal(result?.OffsetArea ?? 0) * (result.Booking?.AreaPricePerUnit ?? 0)));
                    }


                    if (result.AgreementPriceList != null)
                    {
                        //ราคาสุทธิ = ราคาขาย + ราคาเพิ่มลด
                        result.NetSellingPrice = Math.Ceiling((result?.AgreementPriceList?.NetSellingPrice ?? 0) + (result?.OffsetAreaUnitPrice.Value ?? 0));

                        ////เงินโอน --> คำนวนจาก (ราคาขาย+ ราคาเพิ่มลด)
                        //result.transferAmount = (result?.AgreementPriceList.SellingPrice ?? 0) + (result?.OffsetAreaUnitPrice ?? 0);

                        //เงินโอน --> คำนวนจาก (ราคาขาย-(เงินจอง+เงินสัญญา+เงินดาว))
                        result.transferAmount = Math.Ceiling(
                                (result?.AgreementPriceList?.NetSellingPrice ?? 0) //ราคาขาย
                                - (
                                    (result?.AgreementPriceList?.BookingAmount ?? 0) //เงินจอง
                                    + (result?.AgreementPriceList?.ContractAmount ?? 0) //เงินสัญญา
                                    + (result?.AgreementPriceList?.DownAmount ?? 0) //เงินดาว
                                  ));
                    }
                    else
                    {
                        //ราคาสุทธิ = ราคาขาย + ราคาเพิ่มลด
                        result.NetSellingPrice = Math.Ceiling((result?.BookingPriceList?.NetSellingPrice ?? 0) + (result?.OffsetAreaUnitPrice.Value ?? 0));

                        ////เงินโอน --> คำนวนจาก (ราคาขาย+ ราคาเพิ่มลด)
                        //result.transferAmount = (result?.BookingPriceList.SellingPrice ?? 0) + (result?.OffsetAreaUnitPrice ?? 0);

                        //เงินโอน --> คำนวนจาก (ราคาขาย-(เงินจอง+เงินสัญญา+เงินดาว))
                        result.transferAmount = Math.Ceiling(
                                (result?.BookingPriceList?.NetSellingPrice ?? 0) //ราคาขาย
                                - (
                                    (result?.BookingPriceList?.BookingAmount ?? 0) //เงินจอง
                                    + (result?.BookingPriceList?.ContractAmount ?? 0) //เงินสัญญา
                                    + (result?.BookingPriceList?.DownAmount ?? 0) //เงินดาว
                                  ));
                    }

                    var lcmRoleID = await DB.Roles.Where(o => o.Code == "LCM").Select(o => o.ID).FirstAsync();
                    var lcmUsers = from r in DB.Users
                        .Where(o => o.UserAuthorizeProject_LCMs.Where(m => m.ProjectID == ProjectId).Any() &&
                                 o.UserRoles.Where(n => n.RoleID == lcmRoleID).Any())
                                   select r;

                    result.LCMUser = await lcmUsers.Select(o => UserListDTO.CreateFromModel(o)).FirstOrDefaultAsync();

                    return result ?? new MortgageInfoDTO();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
