using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.USR;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class UnitInfoDTO
    {
        /// <summary>
        /// ข้อมูลโครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// ข้อมูลแปลง
        /// </summary>
        public PRJ.UnitDTO Unit { get; set; }

        /// <summary>
        /// ข้อมูลใบจอง
        /// </summary>
        public BookingDTO Booking { get; set; }

        /// <summary>
        /// ข้อมูลสัญญา
        /// </summary>
        public AgreementDTO Agreement { get; set; }

        /// <summary>
        /// เจ้าของห้อง
        /// </summary>
        public List<UnitOwner> UnitOwnerList { get; set; }

        /// <summary>
        /// LC ผู้รับผิดชอบ
        /// </summary>
        public UserDTO LCOwner { get; set; }

        /// <summary>
        /// วันที่นัดโอน
        /// </summary>
        public DateTime? ScheduleTransferDate { get; set; }

        /// <summary>
        /// วันที่โอนจริง
        /// </summary>
        public DateTime? ActualTransferDate { get; set; }

        /// <summary>
        /// LC โอน
        /// </summary>
        public UserDTO TransferSaleUser { get; set; }

        /// <summary>
        /// สถานะเครียมโอน
        /// </summary>
        public MasterCenterDTO ReadyToTransferStatus { get; set; }

        /// <summary>
        /// วันที่เตรียมโอน
        /// </summary>
        public DateTime? ReadyToTransferDate { get; set; }

        /// <summary>
        /// รับชำระเงินก่อนโอนได้หรือยัง
        /// </summary>
        public bool IsPreTransferPayment { get; set; }

        /// <summary>
        /// ข้อมูลโปรโมชั่น
        /// </summary>
        public TransferPromotionDTO TransferPromotion { get; set; }

        /// <summary>
        /// Due วันที่โอนกรรมสิทธิ์จาก Master
        /// </summary>
        public DateTime? DueTransferDate { get; set; }

        /// <summary>
        /// ปลดล็อก Due วันโอน
        /// </summary>
        public bool? IsUnLockDueTransDate { get; set; }

        public Guid? OnlinePaymentPerbookID { get; set; }

        public Guid? ChangeSalePromotionWorkflowID { get; set; }
        public Guid? ChangeTransferPromotionWorkflowID { get; set; }

        public async static Task<UnitInfoDTO> CreateFromQueryResultAsync(UnitInfoQueryResult model, DatabaseContext DB, FileHelper FileHelper)
        {
            if (model != null)
            {
                var result = new UnitInfoDTO();
                var floorPlan = new FloorPlanImageDTO();
                IQueryable<FloorPlanImage> query = DB.FloorPlanImages.Include(o => o.UpdatedBy).Where(o => o.ProjectID == model.Project.ID);
                if (!string.IsNullOrEmpty(model.Unit.FloorPlanFileName))
                {
                    query = query.Where(o => o.Name.Contains(model.Unit.FloorPlanFileName));
                    var floorPlanImages = await query.ToListAsync();

                    floorPlan = floorPlanImages.Select(async o => await FloorPlanImageDTO.CreateFromModelAsync(o, FileHelper)).Select(o => o.Result).FirstOrDefault();
                }

                IQueryable<RoomPlanImage> query2 = DB.RoomPlanImages.Include(o => o.UpdatedBy).Where(o => o.ProjectID == model.Project.ID);
                var roomPlan = new RoomPlanImageDTO();
                if (!string.IsNullOrEmpty(model.Unit.RoomPlanFileName))
                {
                    query2 = query2.Where(o => o.Name.Contains(model.Unit.RoomPlanFileName));

                    var roomPlanImages = await query2.ToListAsync();

                    roomPlan = roomPlanImages.Select(async o => await RoomPlanImageDTO.CreateFromModelAsync(o, FileHelper)).Select(o => o.Result).FirstOrDefault();
                }

                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = PRJ.UnitDTO.CreateFromModel(model.Unit, floorPlan, roomPlan);
                result.Booking = await BookingDTO.CreateFromModelAsync(model.Booking, DB);

                if (result.Project.ProductType != null && result.Project.ProductType.Key == ProductTypeKeys.HighRise)
                {
                    // เซ็ต Due วันที่โอนกรรมสิทธิ์จาก Master
                    if (model.Unit.Floor != null)
                        result.DueTransferDate = model.Unit.Floor.DueTransferDate;

                    // ตรวจสอบว่าวันที่ปัจจุบันน้อยกว่าวันที่โอนจาก Master 30 วันหรือไม่
                    var chkDate = DateTime.Now.AddDays(30);
                    if (chkDate >= result.DueTransferDate) result.IsUnLockDueTransDate = true;
                    else result.IsUnLockDueTransDate = false;
                }
                else
                {
                    result.IsUnLockDueTransDate = true;
                }

                var transferModel = await DB.Transfers.Include(o => o.TransferSale).Include(o => o.Agreement).Where(o => o.Agreement.BookingID == model.Booking.ID && o.Agreement.IsCancel == false).FirstOrDefaultAsync();

                result.ScheduleTransferDate = transferModel?.ScheduleTransferDate;
                result.ActualTransferDate = transferModel?.ActualTransferDate;
                result.TransferSaleUser = UserDTO.CreateFromModel(transferModel?.TransferSale);

                var ReadyToTransferStatus = new MasterCenter();

                if (transferModel?.TransferNo != null)
                    ReadyToTransferStatus = DB.MasterCenters.Where(o => o.MasterCenterGroup.Key == MasterCenterGroupKeys.ReadyToTransferStatus && o.Key == ReadyToTransferStatusKeys.Transferred).FirstOrDefault();
                else if (transferModel?.IsReadyToTransfer ?? false)
                    ReadyToTransferStatus = DB.MasterCenters.Where(o => o.MasterCenterGroup.Key == MasterCenterGroupKeys.ReadyToTransferStatus && o.Key == ReadyToTransferStatusKeys.Ready).FirstOrDefault();
                else
                    ReadyToTransferStatus = DB.MasterCenters.Where(o => o.MasterCenterGroup.Key == MasterCenterGroupKeys.ReadyToTransferStatus && o.Key == ReadyToTransferStatusKeys.NotReady).FirstOrDefault();

                result.ReadyToTransferStatus = MasterCenterDTO.CreateFromModel(ReadyToTransferStatus);
                result.ReadyToTransferDate = transferModel?.ReadyToTransferDate;

                result.UnitOwnerList = [];

                if (model.Booking.BookingNo != null)
                {
                    var AgreementModel = await DB.AgreementOwners
                          .Include(o => o.National)
                          .Include(o => o.ContactType)
                          .Include(o => o.ContactTitleTH)
                          .Include(o => o.ContactTitleEN)
                          .Include(o => o.Gender)
                          .Include(o => o.Agreement)
                             .ThenInclude(o => o.CreatedBy)
                          .Include(o => o.Agreement)
                             .ThenInclude(o => o.AgreementStatus)
                          .Where(o => o.Agreement.BookingID == model.Booking.ID && o.IsMainOwner == true)
                          .OrderBy(o => o.Order)
                          .ToListAsync();

                    if (AgreementModel != null)
                    {
                        result.Agreement = await AgreementDTO.CreateFromModelAsync(AgreementModel.Select(e => e.Agreement).FirstOrDefault(), null, DB);

                        result.LCOwner = UserDTO.CreateFromModel(AgreementModel.Select(e => e.Agreement?.CreatedBy).FirstOrDefault());

                        var AgreementOwner = AgreementModel.Select(o => AgreementOwnerDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();

                        if (AgreementOwner.Count != 0)
                        {
                            var UnitOwner = new UnitOwner();
                            foreach (var item in AgreementOwner)
                            {
                                UnitOwner.FirstNameTH = item.FirstNameTH;
                                UnitOwner.LastNameTH = item.LastNameTH;

                                UnitOwner.FirstNameEN = item.FirstNameEN;
                                UnitOwner.LastNameEN = item.LastNameEN;

                                UnitOwner.National = item.National?.Name;
                                UnitOwner.ContactNo = item.ContactNo;

                                UnitOwner.CitizenIdentityNo = item.CitizenIdentityNo;

                                UnitOwner.ActiveDate = item.UpdateDate;
                                UnitOwner.PhoneNumber = (item.AgreementOwnerPhones.Find(o => o.IsMain == true) ?? new())?.PhoneNumber;
                                UnitOwner.Email = (item.AgreementOwnerEmails.Find(o => o.IsMain == true) ?? new())?.Email;

                                UnitOwner.ContactType = item.ContactType;
                                UnitOwner.ContactTitleTH = item.TitleTH;
                                UnitOwner.MiddleNameTH = item.MiddleNameTH;
                                UnitOwner.IsVIP = item.IsVIP;
                                UnitOwner.IsThaiNationality = item.IsThaiNationality;
                                UnitOwner.TaxID = item.TaxID;
                                UnitOwner.PhoneNumberExt = item.PhoneNumberExt;

                                result.UnitOwnerList.Add(UnitOwner);
                            }
                        }
                        else
                        {
                            var BookingModel = await DB.BookingOwners
                                                .Include(o => o.ContactType)
                                                .Include(o => o.ContactTitleTH)
                                                .Include(o => o.ContactTitleEN)
                                                .Include(o => o.National)
                                                .Include(o => o.Gender)
                                                .Include(o => o.Booking)
                                                    .ThenInclude(o => o.CreatedBy)
                                                .Where(o => o.BookingID == model.Booking.ID && !o.IsAgreementOwner)
                                                .OrderBy(o => o.Order).ToListAsync();

                            result.LCOwner = UserDTO.CreateFromModel(BookingModel.FirstOrDefault()?.CreatedBy);

                            var BookingOwner = BookingModel.Select(o => BookingOwnerDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();

                            if (BookingOwner.Count != 0)
                            {
                                var UnitOwner = new UnitOwner();
                                foreach (var item in BookingOwner)
                                {
                                    UnitOwner.FirstNameTH = item.FirstNameTH;
                                    UnitOwner.LastNameTH = item.LastNameTH;

                                    UnitOwner.FirstNameEN = item.FirstNameEN;
                                    UnitOwner.LastNameEN = item.LastNameEN;

                                    UnitOwner.National = item.National?.Name;
                                    UnitOwner.ContactNo = item.ContactNo;
                                    UnitOwner.CitizenIdentityNo = item.CitizenIdentityNo;

                                    UnitOwner.ActiveDate = item.UpdateDate;
                                    UnitOwner.PhoneNumber = (item.ContactPhones.Find(o => o.IsMain == true) ?? new())?.PhoneNumber;
                                    UnitOwner.Email = (item.ContactEmails.Find(o => o.IsMain == true) ?? new())?.Email;

                                    UnitOwner.ContactType = item.ContactType;
                                    UnitOwner.ContactTitleTH = item.TitleTH;
                                    UnitOwner.MiddleNameTH = item.MiddleNameTH;
                                    UnitOwner.IsVIP = item.IsVIP;
                                    UnitOwner.IsThaiNationality = item.IsThaiNationality;
                                    UnitOwner.TaxID = item.TaxID;
                                    UnitOwner.PhoneNumberExt = item.PhoneNumberExt;

                                    result.UnitOwnerList.Add(UnitOwner);
                                }
                            }
                        }
                    }
                }

                //มีจ่าย onlinePayment ไหม
                var onlinePayment = await (from p in DB.OnlinePaymentPrebooks.Where(o => o.UnitID == model.Unit.ID && (o.IsCancel ?? false) == false)
                                           join h in DB.OnlinePaymentHistorys on p.OnlinePaymentHistoryID equals h.ID
                                           where h.Payment_Status == "PAID"
                                           orderby p.Created descending
                                           select p).FirstOrDefaultAsync();

                if (onlinePayment != null)
                {
                    result.OnlinePaymentPerbookID = onlinePayment.ID;

                    var quotation = DB.Quotations.IgnoreQueryFilters().Where(o => o.RefOnlinePaymentPrebookID == onlinePayment.ID).OrderByDescending(x => x.Created).FirstOrDefault();

                    if (quotation != null)
                    {
                        var booking = (from q in DB.Bookings.Where(o => o.QuotationID == quotation.ID && o.IsCancelled == true) select q).FirstOrDefault();

                        if (booking != null)
                            result.OnlinePaymentPerbookID = null;
                    }
                }

                result.ChangeSalePromotionWorkflowID = (await DB.SalePromotions
                    .Include(o => o.ChangePromotionWorkflow)
                    .Where(o => o.BookingID == result.Booking.Id)
                    .Select(o => o.ChangePromotionWorkflow)
                    .FirstOrDefaultAsync())?.ID;

                result.ChangeTransferPromotionWorkflowID = (await DB.TransferPromotions
                    .Include(o => o.ChangePromotionWorkflow)
                    .Where(o => o.BookingID == result.Booking.Id)
                    .Select(o => o.ChangePromotionWorkflow)
                    .FirstOrDefaultAsync())?.ID;

                return result;
            }
            else
                return null;
        }

        /// <summary>
        /// for TransferPromorionRequest Service
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DB"></param>
        /// <returns></returns>
        public async static Task<UnitInfoDTO> CreateFromQueryRequestAsync(UnitInfoQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new UnitInfoDTO();
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Booking = await BookingDTO.CreateFromModelAsync(model.Booking, DB);
                result.Unit = PRJ.UnitDTO.CreateFromModel(model.Unit);
                var transferModel = await DB.Transfers.Include(o => o.TransferSale).Include(o => o.Agreement)
                    .Where(o => o.Agreement.BookingID == model.Booking.ID && o.Agreement.IsCancel == false).FirstOrDefaultAsync();

                var ReadyToTransferStatus = new MasterCenter();

                if (transferModel?.TransferNo != null)
                    ReadyToTransferStatus = DB.MasterCenters.Where(o => o.MasterCenterGroup.Key == MasterCenterGroupKeys.ReadyToTransferStatus && o.Key == ReadyToTransferStatusKeys.Transferred).FirstOrDefault();
                else if (transferModel?.IsReadyToTransfer ?? false)
                    ReadyToTransferStatus = DB.MasterCenters.Where(o => o.MasterCenterGroup.Key == MasterCenterGroupKeys.ReadyToTransferStatus && o.Key == ReadyToTransferStatusKeys.Ready).FirstOrDefault();
                else // if (!(model.Transfer?.IsReadyToTransfer ?? false) || model.Transfer.TransferNo == null)
                    ReadyToTransferStatus = DB.MasterCenters.Where(o => o.MasterCenterGroup.Key == MasterCenterGroupKeys.ReadyToTransferStatus && o.Key == ReadyToTransferStatusKeys.NotReady).FirstOrDefault();

                var transferPromotion = await DB.TransferPromotions.Where(o => o.BookingID == result.Booking.Id).FirstOrDefaultAsync();
                result.TransferPromotion = TransferPromotionDTO.CreateFromModel(transferPromotion, DB);

                result.ReadyToTransferStatus = MasterCenterDTO.CreateFromModel(ReadyToTransferStatus);
                result.ReadyToTransferDate = transferModel?.ReadyToTransferDate;

                result.UnitOwnerList = new List<UnitOwner>();

                if (model.Booking.BookingNo != null)
                {
                    var AgreementModel = await DB.AgreementOwners
                          .Include(o => o.National)
                          .Include(o => o.ContactType)
                          .Include(o => o.ContactTitleTH)
                          .Include(o => o.ContactTitleEN)
                          .Include(o => o.Gender)
                          .Include(o => o.Agreement)
                             .ThenInclude(o => o.CreatedBy)
                          .Where(o => o.Agreement.BookingID == model.Booking.ID && o.IsMainOwner == true)
                          .OrderBy(o => o.Order)
                          .ToListAsync();

                    if (AgreementModel != null)
                    {
                        result.Agreement = await AgreementDTO.CreateFromModelAsync(AgreementModel.Select(e => e.Agreement).FirstOrDefault(), null, DB);

                        result.LCOwner = UserDTO.CreateFromModel(AgreementModel.Select(e => e.Agreement?.CreatedBy).FirstOrDefault());

                        var AgreementOwner = AgreementModel.Select(o => AgreementOwnerDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();

                        if (AgreementOwner.Any())
                        {
                            var UnitOwner = new UnitOwner();
                            foreach (var item in AgreementOwner)
                            {
                                UnitOwner.FirstNameTH = item.FirstNameTH;
                                UnitOwner.LastNameTH = item.LastNameTH;

                                UnitOwner.FirstNameEN = item.FirstNameEN;
                                UnitOwner.LastNameEN = item.LastNameEN;

                                UnitOwner.National = item.National?.Name;
                                UnitOwner.ContactNo = item.ContactNo;

                                UnitOwner.CitizenIdentityNo = item.CitizenIdentityNo;

                                UnitOwner.ActiveDate = item.UpdateDate;
                                UnitOwner.PhoneNumber = (item.AgreementOwnerPhones.Find(o => o.IsMain == true) ?? new())?.PhoneNumber;
                                UnitOwner.Email = (item.AgreementOwnerEmails.Find(o => o.IsMain == true) ?? new())?.Email;

                                UnitOwner.ContactType = item.ContactType;
                                UnitOwner.ContactTitleTH = item.TitleTH;
                                UnitOwner.MiddleNameTH = item.MiddleNameTH;
                                UnitOwner.IsVIP = item.IsVIP;
                                UnitOwner.IsThaiNationality = item.IsThaiNationality;
                                UnitOwner.TaxID = item.TaxID;
                                UnitOwner.PhoneNumberExt = item.PhoneNumberExt;
                                UnitOwner.TitleExtTH = item.TitleExtTH;

                                if (item.ContactType.Key == ContactTypeKeys.Legal)
                                {
                                    UnitOwner.DisplayName = item.FirstNameTH;
                                }
                                else
                                {
                                    UnitOwner.DisplayName = (!string.IsNullOrEmpty(item.TitleExtTH) ? item.TitleExtTH : item.ContactTitleTH.Name) +
                                                            item.FirstNameTH + " " +
                                                            (string.IsNullOrEmpty(item.MiddleNameTH) ? string.Empty : item.MiddleNameTH + " ") +
                                                            item.LastNameTH;
                                }

                                result.UnitOwnerList.Add(UnitOwner);
                            }
                        }
                        else
                        {
                            var BookingModel = await DB.BookingOwners
                                                .Include(o => o.ContactType)
                                                .Include(o => o.ContactTitleTH)
                                                .Include(o => o.ContactTitleEN)
                                                .Include(o => o.National)
                                                .Include(o => o.Gender)
                                                .Include(o => o.Booking)
                                                    .ThenInclude(o => o.CreatedBy)
                                                .Where(o => o.BookingID == model.Booking.ID && !o.IsAgreementOwner)
                                                .OrderBy(o => o.Order).ToListAsync();

                            result.LCOwner = UserDTO.CreateFromModel(BookingModel.FirstOrDefault()?.CreatedBy);

                            var BookingOwner = BookingModel.Select(o => BookingOwnerDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();

                            if (BookingOwner.Any())
                            {
                                var UnitOwner = new UnitOwner();
                                foreach (var item in BookingOwner)
                                {
                                    UnitOwner.FirstNameTH = item.FirstNameTH;
                                    UnitOwner.LastNameTH = item.LastNameTH;

                                    UnitOwner.FirstNameEN = item.FirstNameEN;
                                    UnitOwner.LastNameEN = item.LastNameEN;

                                    UnitOwner.National = item.National?.Name;
                                    UnitOwner.ContactNo = item.ContactNo;
                                    UnitOwner.CitizenIdentityNo = item.CitizenIdentityNo;

                                    UnitOwner.ActiveDate = item.UpdateDate;
                                    UnitOwner.PhoneNumber = (item.ContactPhones.Find(o => o.IsMain == true) ?? new())?.PhoneNumber;
                                    UnitOwner.Email = (item.ContactEmails.Find(o => o.IsMain == true) ?? new())?.Email;

                                    UnitOwner.ContactType = item.ContactType;
                                    UnitOwner.ContactTitleTH = item.TitleTH;
                                    UnitOwner.MiddleNameTH = item.MiddleNameTH;
                                    UnitOwner.IsVIP = item.IsVIP;
                                    UnitOwner.IsThaiNationality = item.IsThaiNationality;
                                    UnitOwner.TaxID = item.TaxID;
                                    UnitOwner.PhoneNumberExt = item.PhoneNumberExt;
                                    UnitOwner.TitleExtTH = item.TitleExtTH;

                                    if (item.ContactType.Key == ContactTypeKeys.Legal)
                                    {
                                        UnitOwner.DisplayName = item.FirstNameTH;
                                    }
                                    else
                                    {
                                        UnitOwner.DisplayName = (!string.IsNullOrEmpty(item.TitleExtTH) ? item.TitleExtTH : item.TitleTH.Name) +
                                                           item.FirstNameTH + " " +
                                                           (string.IsNullOrEmpty(item.MiddleNameTH) ? string.Empty : item.MiddleNameTH + " ") +
                                                           item.LastNameTH;
                                    }

                                    result.UnitOwnerList.Add(UnitOwner);
                                }
                            }
                        }
                    }
                }
                //result.UnitOwnerList = UnitInfoDTO.CreateFromQueryResultAsync()

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<UnitInfoDTO> CreateFromQueryResultForPaymentAsync(UnitInfoQueryResultForPayment model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new UnitInfoDTO
                {
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = UnitDTO.CreateFromModel(model.Unit, null, null),
                    Booking = BookingDTO.CreateFromModelForPayment(model.Booking),
                    ScheduleTransferDate = model.Transfer?.ScheduleTransferDate,
                    ActualTransferDate = model.Transfer?.ActualTransferDate,
                    TransferSaleUser = UserDTO.CreateFromModel(model.TransferSaleUser),
                    IsPreTransferPayment = !string.IsNullOrEmpty(model.Transfer?.TransferNo),
                    LCOwner = UserDTO.CreateFromModel(model.SaleUser),
                    ReadyToTransferStatus = MasterCenterDTO.CreateFromModel(model.TransferStatus),
                    ReadyToTransferDate = model.Transfer?.ReadyToTransferDate,
                    UnitOwnerList = new List<UnitOwner>()
                };

                #region Owner
                if (model.Agreement?.AgreementNo != null)
                {
                    var AgreementOwnerModel = await (from ago in DB.AgreementOwners
                          .Include(o => o.National)
                          .Include(o => o.Gender)
                          .Include(o => o.National)
                            .Where(o => o.AgreementID == model.Agreement.ID)

                                                     let agoPhone = DB.AgreementOwnerPhones.Where(o => o.AgreementOwnerID == ago.ID && o.IsMain == true).FirstOrDefault()
                                                     let agoEmail = DB.AgreementOwnerEmails.Where(o => o.AgreementOwnerID == ago.ID && o.IsMain == true).FirstOrDefault()

                                                     select new
                                                     {
                                                         AgreementOwner = ago,
                                                         Phone = agoPhone ?? new AgreementOwnerPhone(),
                                                         Email = agoEmail ?? new AgreementOwnerEmail(),
                                                     }).OrderBy(o => o.AgreementOwner.Order).ToListAsync();

                    if (AgreementOwnerModel != null)
                    {
                        foreach (var item in AgreementOwnerModel)
                        {
                            var UnitOwner = new UnitOwner();
                            UnitOwner.FirstNameTH = item.AgreementOwner.FirstNameTH;
                            UnitOwner.LastNameTH = item.AgreementOwner.LastNameTH;

                            UnitOwner.FirstNameEN = item.AgreementOwner.FirstNameEN;
                            UnitOwner.LastNameEN = item.AgreementOwner.LastNameEN;

                            UnitOwner.National = item.AgreementOwner.National?.Name;
                            UnitOwner.ContactNo = item.AgreementOwner.ContactNo;
                            UnitOwner.CitizenIdentityNo = item.AgreementOwner.CitizenIdentityNo;

                            UnitOwner.ActiveDate = item.AgreementOwner.Updated;

                            UnitOwner.PhoneNumber = item.Phone?.PhoneNumber;
                            UnitOwner.Email = item.Email?.Email;

                            UnitOwner.DisplayName = (item.AgreementOwner.IsThaiNationality) ? item.AgreementOwner.FullnameTH : item.AgreementOwner.FullnameEN;

                            result.UnitOwnerList.Add(UnitOwner);
                        }
                    }
                }
                else
                {
                    var BookingOwnerModel = await (from bo in DB.BookingOwners
                                        .Include(o => o.National)
                                        .Include(o => o.Gender)
                                        .Where(o => o.BookingID == model.Booking.ID && !o.IsAgreementOwner)

                                                   let boPhone = DB.BookingOwnerPhones.Where(o => o.BookingOwnerID == bo.ID && o.IsMain == true).FirstOrDefault()
                                                   let boEmail = DB.BookingOwnerEmails.Where(o => o.BookingOwnerID == bo.ID && o.IsMain == true).FirstOrDefault()

                                                   select new
                                                   {
                                                       BookingOwner = bo,
                                                       Phone = boPhone ?? new BookingOwnerPhone(),
                                                       Email = boEmail ?? new BookingOwnerEmail(),
                                                   }).OrderBy(o => o.BookingOwner.Order).ToListAsync();

                    if (BookingOwnerModel != null)
                    {
                        foreach (var item in BookingOwnerModel)
                        {
                            var UnitOwner = new UnitOwner();
                            UnitOwner.FirstNameTH = item.BookingOwner.FirstNameTH;
                            UnitOwner.LastNameTH = item.BookingOwner.LastNameTH;

                            UnitOwner.FirstNameEN = item.BookingOwner.FirstNameEN;
                            UnitOwner.LastNameEN = item.BookingOwner.LastNameEN;

                            UnitOwner.National = item.BookingOwner.National?.Name;
                            UnitOwner.ContactNo = item.BookingOwner.ContactNo;
                            UnitOwner.CitizenIdentityNo = item.BookingOwner.CitizenIdentityNo;

                            UnitOwner.ActiveDate = item.BookingOwner.Updated;

                            UnitOwner.PhoneNumber = item.Phone?.PhoneNumber;
                            UnitOwner.Email = item.Email?.Email;

                            UnitOwner.DisplayName = (item.BookingOwner.IsThaiNationality) ? item.BookingOwner.FullnameTH : item.BookingOwner.FullnameEN;

                            result.UnitOwnerList.Add(UnitOwner);
                        }
                    }
                }
                #endregion

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class UnitInfoQueryResult
    {
        public Unit Unit { get; set; }
        public Project Project { get; set; }
        public Booking Booking { get; set; }
    }

    public class UnitInfoQueryResultForPayment
    {
        public Unit Unit { get; set; }
        public Project Project { get; set; }
        public Booking Booking { get; set; }
        public Agreement Agreement { get; set; }
        public Transfer Transfer { get; set; }
        public User SaleUser { get; set; }
        public User TransferSaleUser { get; set; }
        public MasterCenter TransferStatus { get; set; }
    }

    public class UnitOwner
    {
        public string FirstNameTH { get; set; }
        public string LastNameTH { get; set; }
        public string FirstNameEN { get; set; }
        public string LastNameEN { get; set; }
        public string MiddleNameTH { get; set; }
        public string National { get; set; }
        public string ContactNo { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CitizenIdentityNo { get; set; }
        public string DisplayName { get; set; }
        public string TaxID { get; set; }
        public string PhoneNumberExt { get; set; }
        public bool? IsVIP { get; set; }
        public bool? IsThaiNationality { get; set; }
        public MasterCenterDropdownDTO ContactType { get; set; }
        public MasterCenterDropdownDTO ContactTitleTH { get; set; }
        public string TitleExtTH { get; set; }
    }
}
