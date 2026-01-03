using Base.DTOs.CTM;
using Base.DTOs.CTM.Sortings;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.DbQueries.SAL;
using Database.Models.MasterKeys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    public class BookingListDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบจอง
        /// </summary>
        public string BookingNo { get; set; }

        /// <summary>
        /// แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// ผู้จอง
        ///  Sale/api/Bookings/{bookingID}/BookingOwners/DropdownList
        /// </summary>
        public BookingOwnerDropdownDTO Owner { get; set; }

        /// <summary>
        /// วันที่จอง
        /// </summary>
        public DateTime? BookingDate { get; set; }

        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? ApproveDate { get; set; }

        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }

        /// <summary>
        /// สถานะ Booking
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=BookingStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO BookingStatus { get; set; }

        /// <summary>
        /// ลบ
        /// </summary>
        public bool? CanDelete { get; set; }

        /// <summary>
        /// สร้างใบจองจากระบบไหน
        /// </summary>
        public MST.MasterCenterDropdownDTO CreateBookingFrom { get; set; }

        /// <summary>
        /// ยืนยันโดย
        /// </summary>
        public USR.UserListDTO ConfirmBy { get; set; }

        /// <summary>
        /// วันที่ยืนยัน
        /// </summary>
        public DateTime? ConfirmDate { get; set; }

        /// <summary>
        /// ยกเลิกใบจอง
        /// </summary>
        public bool? IsCancelled { get; set; }

        /// <summary>
        /// ราคาขายสุทธิหน้าสัญญา (จำนวนเงิน)
        /// </summary>
        public decimal SellingPrice { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงินชั่วคราว
        /// </summary>
        public string ReceiptTempNo { get; set; }

        ///<summary>
        ///สถานะการขอสินเชื่อ
        ///</summary>
        public MST.MasterCenterDropdownDTO CreditBankingType { get; set; }

        /// <summary>
        /// ประเภทพนักงานปิดการขาย
        /// GET Master/api/MasterCenters?MasterCenterGroupKey=SaleOfficerType
        /// </summary>
        public MST.MasterCenterDropdownDTO SaleOfficerType { get; set; }
        /// <summary>
        /// รหัส Sale
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs=7{projectID}
        /// </summary>
        public USR.UserListDTO SaleUser { get; set; }

        /// <summary>
        /// รหัส Sale ประจำโครงการ
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs=7{projectID}
        /// </summary>
        public USR.UserListDTO ProjectSaleUser { get; set; }

        public Guid? OpportunityID { get; set; }

        public CTM.ContactDTO Contact { get; set; }
        public bool? isChangeProfile { get; set; }

        public static BookingListDTO CreateFromQueryResult(BookingQueryResult model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingListDTO()
                {
                    Id = model.Booking.ID,
                    Updated = model.Booking.Updated,
                    UpdatedBy = model.Booking.UpdatedBy?.DisplayName,
                    BookingNo = model.Booking.BookingNo,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    BookingDate = model.Booking.BookingDate,
                    ApproveDate = model.Booking.ApproveDate,
                    ContractDate = model.Booking.ContractDate,
                    BookingStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.BookingStatus),
                    CreateBookingFrom = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreateBookingFrom),
                    ConfirmBy = USR.UserListDTO.CreateFromModel(model.ConfirmBy),
                    ConfirmDate = model.Booking.ConfirmDate,
                    IsCancelled = model.Booking.IsCancelled,
                    CreditBankingType = MST.MasterCenterDropdownDTO.CreateFromModel(model.Booking.CreditBankingType),
                    OpportunityID = model.Booking.OpportunityID
                };

                var bookingOwner = DB.BookingOwners.Include(o => o.National).Where(o => o.BookingID == model.Booking.ID && o.IsMainOwner == true && o.IsAgreementOwner == false).FirstOrDefault();

                result.Owner = BookingOwnerDropdownDTO.CreateFromModel(bookingOwner);

                var payments = DB.Payments.Where(o => o.BookingID == model.Booking.ID).ToList();

                var hasMinPriceWorkflow = DB.MinPriceBudgetWorkflows.Where(o => o.BookingID == model.Booking.ID && o.IsApproved == null && !o.IsCancelled && !o.IsRecalled).Any();
                var haspricelist = DB.PriceListWorkflows.Where(o => o.BookingID == model.Booking.ID && o.IsApproved == null).Any();

                result.CanDelete = (payments.Count == 0) && (hasMinPriceWorkflow ? false : true) && (haspricelist ? false : true) && (result.BookingStatus.Key != "6") && (result.BookingStatus.Key != "3");

                var unitPriceModel = DB.UnitPrices.Where(o => o.BookingID == model.Booking.ID && o.UnitPriceStage.Key == UnitPriceStageKeys.Booking).FirstOrDefault();
                if (unitPriceModel != null)
                {
                    result.SellingPrice = unitPriceModel.AgreementPrice.HasValue ? unitPriceModel.AgreementPrice.Value : 0;
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static BookingListDTO CreateOppListFromQueryResult(BookingQueryResult model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingListDTO()
                {
                    Id = model.Booking.ID,
                    Updated = model.Booking.Updated,
                    UpdatedBy = model.Booking.UpdatedBy?.DisplayName,
                    BookingNo = model.Booking.BookingNo,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    BookingDate = model.Booking.BookingDate,
                    ApproveDate = model.Booking.ApproveDate,
                    ContractDate = model.Booking.ContractDate,
                    BookingStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.BookingStatus),
                    CreateBookingFrom = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreateBookingFrom),
                    ConfirmBy = USR.UserListDTO.CreateFromModel(model.ConfirmBy),
                    ConfirmDate = model.Booking.ConfirmDate,
                    IsCancelled = model.Booking.IsCancelled,
                    CreditBankingType = MST.MasterCenterDropdownDTO.CreateFromModel(model.Booking.CreditBankingType),
                    OpportunityID = model.Booking.OpportunityID
                };

                var bookingOwner = DB.BookingOwners.Include(o => o.National).Where(o => o.BookingID == model.Booking.ID && o.IsMainOwner == true && o.IsAgreementOwner == false).FirstOrDefault();
                var opp = DB.Opportunities.Where(o => o.ID == model.Booking.OpportunityID).FirstOrDefault() ?? new models.CTM.Opportunity();
                var contact = DB.Contacts.Where(o => o.ID == opp.ContactID).FirstOrDefault() ?? new models.CTM.Contact();
                var his = DB.OpportunityRelatedHistory.Where(o => o.BookingID == bookingOwner.BookingID).FirstOrDefault();

                result.isChangeProfile = his == null ? false : true;
                result.Owner = BookingOwnerDropdownDTO.CreateFromModel(bookingOwner);
                result.Contact = CTM.ContactDTO.CreateFromModel(contact);

                var payments = DB.Payments.Where(o => o.BookingID == model.Booking.ID).ToList();

                var hasMinPriceWorkflow = DB.MinPriceBudgetWorkflows.Where(o => o.BookingID == model.Booking.ID && o.IsApproved == null && !o.IsCancelled && !o.IsRecalled).Any();
                var haspricelist = DB.PriceListWorkflows.Where(o => o.BookingID == model.Booking.ID && o.IsApproved == null).Any();

                result.CanDelete = (payments.Count == 0) && (hasMinPriceWorkflow ? false : true) && (haspricelist ? false : true) && (result.BookingStatus.Key != "6") && (result.BookingStatus.Key != "3");

                var unitPriceModel = DB.UnitPrices.Where(o => o.BookingID == model.Booking.ID && o.UnitPriceStage.Key == UnitPriceStageKeys.Booking).FirstOrDefault();
                if (unitPriceModel != null)
                {
                    result.SellingPrice = unitPriceModel.AgreementPrice.HasValue ? unitPriceModel.AgreementPrice.Value : 0;
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static BookingListDTO CreateFromModel(models.SAL.Booking model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingListDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    BookingNo = model.BookingNo,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    BookingDate = model.BookingDate,
                    ApproveDate = model.ApproveDate,
                    ContractDate = model.ContractDate,
                    BookingStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.BookingStatus),
                    CreditBankingType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreditBankingType)
                };

                var bookingOwner = DB.BookingOwners.Include(o => o.National).Where(o => o.BookingID == model.ID && o.IsMainOwner == true).FirstOrDefault();
                result.Owner = BookingOwnerDropdownDTO.CreateFromModel(bookingOwner);
                var payments = DB.Payments.Where(o => o.BookingID == model.ID).ToList();
                result.CanDelete = payments.Any() ? false : true;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static BookingListDTO CreateFromBookingOfflineQuery(dbqBookingOfflineList model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingListDTO()
                {
                    Id = model.ID,
                    Updated = model.CreateDate,
                    UpdatedBy = "",
                    BookingNo = model.BookingNumber,
                    Unit = new PRJ.UnitDropdownDTO()
                    {
                        Id = model.UnitID,
                        UnitNo = model.UnitNumber,
                        ProjectID = model.ProjectID,
                    },
                    BookingDate = model.BookingDate,
                    ApproveDate = model.BookingDate,
                    ContractDate = model.ContractDueDate,
                    BookingStatus = new MST.MasterCenterDropdownDTO()
                    {
                        Key = "",
                        Name = model.IsConfirm.HasValue && model.IsConfirm.Value ? "ยืนยันจองแล้ว" : "รอยืนยันจอง"
                    },
                    CreateBookingFrom = new MST.MasterCenterDropdownDTO()
                    {
                        Key = "",
                        Name = model.BookingType
                    },
                    ConfirmBy = new USR.UserListDTO()
                    {
                        DisplayName = model.ConfirmBy
                    },
                    ConfirmDate = model.ConfirmDate,
                    IsCancelled = model.IsCancel,
                    ReceiptTempNo = model.ReceiptNo,
                    Owner = new BookingOwnerDropdownDTO()
                    {
                        FirstNameTH = model.FirstName,
                        LastNameTH = model.LastName
                    }
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(BookingListSortByParam sortByParam, ref IQueryable<BookingQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BookingListSortBy.BookingNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.BookingNo);
                        else query = query.OrderByDescending(o => o.Booking.BookingNo);
                        break;
                    case BookingListSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case BookingListSortBy.FullName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => (o.Owner.FirstNameTH)).ThenByDescending(o => o.Owner.LastNameTH);
                        else query = query.OrderByDescending(o => (o.Owner.FirstNameTH)).ThenByDescending(o => o.Owner.LastNameTH);
                        break;
                    case BookingListSortBy.BookingDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.BookingDate);
                        else query = query.OrderByDescending(o => o.Booking.BookingDate);
                        break;
                    case BookingListSortBy.ApproveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.ApproveDate);
                        else query = query.OrderByDescending(o => o.Booking.ApproveDate);
                        break;
                    case BookingListSortBy.ContractDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.ContractDate);
                        else query = query.OrderByDescending(o => o.Booking.ContractDate);
                        break;
                    case BookingListSortBy.BookingStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BookingStatus.Name);
                        else query = query.OrderByDescending(o => o.BookingStatus.Name);
                        break;
                    case BookingListSortBy.CreateBookingFrom:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CreateBookingFrom.Name);
                        else query = query.OrderByDescending(o => o.CreateBookingFrom.Name);
                        break;
                    case BookingListSortBy.ConfirmBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ConfirmBy.DisplayName);
                        else query = query.OrderByDescending(o => o.ConfirmBy.DisplayName);
                        break;
                    case BookingListSortBy.ConfirmDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.ConfirmDate);
                        else query = query.OrderByDescending(o => o.Booking.ConfirmDate);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.Booking.Created);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.Booking.Created);
            }
        }


        public static void SortBys(OppBookingListSortByParam sortByParam, ref IQueryable<BookingQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case OppBookingListSortBy.BookingNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.BookingNo);
                        else query = query.OrderByDescending(o => o.Booking.BookingNo);
                        break;
                    case OppBookingListSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case OppBookingListSortBy.FullName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => (o.Owner.FirstNameTH)).ThenByDescending(o => o.Owner.LastNameTH);
                        else query = query.OrderByDescending(o => (o.Owner.FirstNameTH)).ThenByDescending(o => o.Owner.LastNameTH);
                        break;
                    case OppBookingListSortBy.BookingDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.BookingDate);
                        else query = query.OrderByDescending(o => o.Booking.BookingDate);
                        break;
                    case OppBookingListSortBy.ApproveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.ApproveDate);
                        else query = query.OrderByDescending(o => o.Booking.ApproveDate);
                        break;
                    case OppBookingListSortBy.ContractDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.ContractDate);
                        else query = query.OrderByDescending(o => o.Booking.ContractDate);
                        break;
                    case OppBookingListSortBy.BookingStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BookingStatus.Name);
                        else query = query.OrderByDescending(o => o.BookingStatus.Name);
                        break;
                    case OppBookingListSortBy.CreateBookingFrom:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CreateBookingFrom.Name);
                        else query = query.OrderByDescending(o => o.CreateBookingFrom.Name);
                        break;
                    case OppBookingListSortBy.ConfirmBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ConfirmBy.DisplayName);
                        else query = query.OrderByDescending(o => o.ConfirmBy.DisplayName);
                        break;
                    case OppBookingListSortBy.ConfirmDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.ConfirmDate);
                        else query = query.OrderByDescending(o => o.Booking.ConfirmDate);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.Booking.Created);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.Booking.Created);
            }
        }
    }

    public class BookingQueryResult
    {
        public models.SAL.Booking Booking { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public models.MST.MasterCenter BookingStatus { get; set; }
        public models.MST.MasterCenter CreateBookingFrom { get; set; }
        public models.SAL.BookingOwner Owner { get; set; }
        public models.USR.User ConfirmBy { get; set; }
    }
}
