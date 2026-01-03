using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// ใบจอง
    /// Model: Booking
    /// </summary>
    public class BookingDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบเสนอราคา
        /// </summary>
        public string QuotationNo { get; set; }
        /// <summary>
        /// เลขที่ใบจอง
        /// </summary>
        public string BookingNo { get; set; }
        /// <summary>
        /// สถานะใบจอง
        /// </summary>
        public MST.MasterCenterDropdownDTO BookingStatus { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แบบบ้าน
        /// </summary>
        public PRJ.ModelDropdownDTO Model { get; set; }
        /// <summary>
        /// พื้นที่ขาย
        /// </summary>
        public double? SaleArea { get; set; }
        /// <summary>
        /// วันที่จอง
        /// </summary>
        public DateTime? BookingDate { get; set; }
        /// <summary>
        /// วันที่นัดทำสัญญา
        /// </summary>
        public DateTime? ContractDueDate { get; set; }
        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? ApproveDate { get; set; }
        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? TransferOwnershipDate { get; set; }
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
        /// รหัส Agent
        /// GET Master/api/Agents/DropdownList
        /// </summary>
        public MST.AgentDropdownDTO Agent { get; set; }
        /// <summary>
        /// รหัสพนักงาน Agent
        /// GET Master/api/AgentEmployees/DropdownList?agentID={agentID}
        /// </summary>
        public MST.AgentEmployeeDropdownDTO AgentEmployee { get; set; }
        /// <summary>
        /// รหัส Sale ประจำโครงการ
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs=7{projectID}
        /// </summary>
        public USR.UserListDTO ProjectSaleUser { get; set; }
        /// <summary>
        /// สร้างใบจองจากระบบไหน
        /// </summary>
        public MST.MasterCenterDropdownDTO CreateBookingFrom { get; set; }
        /// <summary>
        /// สถานะการชำระเงิน (null = ยังไม่ได้ชำระ/ false = ชำระเงินจองไม่ครบ / true = ชำระเงินจองครบแล้ว)
        /// </summary>
        public bool? IsPaid { get; set; }
        /// <summary>
        /// Min Price Workflow
        /// </summary>
        public MinPriceBudgetWorkflowDTO MinPriceBudgetWorkflow { get; set; }
        /// <summary>
        /// บันทึก
        /// </summary>
        public bool? CanSave { get; set; }
        /// <summary>
        /// ชำระเงิน
        /// </summary>
        public bool? CanPay { get; set; }
        /// <summary>
        /// พิมพ์
        /// </summary>
        public bool? CanPrint { get; set; }
        /// <summary>
        /// เลือกผู้ทำสัญญา
        /// </summary>
        public bool? CanManageAgreementOwner { get; set; }
        /// <summary>
        /// ทำแบบสอบถาม
        /// </summary>
        public bool? CanQuestionnaire { get; set; }
        /// <summary>
        /// ย้ายแปลง
        /// </summary>
        public bool? CanChangeUnit { get; set; }
        /// <summary>
        /// ยกเลิกการจอง
        /// </summary>
        public bool? CanCancel { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price (กรณีติด Workflow)
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceRequestReason { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price อื่นๆ (กรณีติด Workflow)
        /// </summary>
        public string OtherMinPriceRequestReason { get; set; }
        /// <summary>
        /// เงินที่ชำระแล้ว
        /// </summary>
        public decimal? TotalPayAmount { get; set; }

        /// <summary>
        /// สถานะการขอสินเชื่อ
        /// </summary>
        public MST.MasterCenterDropdownDTO CreditBankingType { get; set; }
        /// <summary>
        /// ราคาพื้นที่ต่อหน่วย
        /// </summary>
        public decimal? AreaPricePerUnit { get; set; }
        /// <summary>
        /// ราคาพื้นที่เพิ่มลด
        /// </summary>
        public decimal? OffsetAreaPrice { get; set; }
        /// <summary>
        /// ใบเสนอราคา
        /// </summary>
        public Guid? QuotationID { get; set; }
        /// <summary>
        /// มีการโอนแล้ว
        /// </summary>
        public bool IsTransferConfirmed { get; set; }

        /// <summary>
        /// UnitNoBefor
        /// </summary>
        public string UnitNoBefor { get; set; }

        /// <summary>
        /// วันที่แก้ไขขอสินเชื่อ
        /// </summary>
        public DateTime? CreditBankingDate { get; set; }
        /// <summary>
        /// ผู้แก้ไขขอสินเชื่อ
        /// </summary>
        public UserDTO CreditBankingByUser { get; set; }

        /// <summary>
        /// รัะหว่างเปลี่ยนแปลงโปรติดmin
        /// </summary>
        public bool? Ischangepromin { get; set; }

        /// <summary>
        /// ปลดล็อกวันจอง
        /// </summary>
        public bool? UnlockBooking { get; set; }

        /// <summary>
        /// IsPreBook
        /// </summary>
        public bool? IsPreBook { get; set; }

        /// <summary>
        /// Quotation ConvertFromPrebook
        /// </summary>
        public bool? IsFromPreBook { get; set; }
        public bool? IsCanConvert { get; set; }

        public bool? IsHasOwner { get; set; }
        public Guid? PaymentID { get; set; }
        public bool? IsFromOnlinePayment { get; set; }
        public Guid? OnlinePaymentContactID { get; set; }

        public async static Task<BookingDTO> CreateFromModelAsync(models.SAL.Booking model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var BookingPayAmount = await DB.PaymentItems
              .Include(o => o.MasterPriceItem)
              .Include(o => o.Payment)
              .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.BookingAmount && o.Payment.BookingID == model.ID && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

                var BookingAmount = await DB.UnitPrices
                      .Include(o => o.UnitPriceStage)
                      .Where(o => o.UnitPriceStage.Key == UnitPriceStageKeys.Booking && o.BookingID == model.ID)
                      .Select(o => o.BookingAmount).FirstOrDefaultAsync();

                // ถ้า null = มีการยกเลิก
                var checkBackward = await DB.Payments
                    .Where(o => o.BookingID == model.ID && !o.IsDeleted && !o.IsCancel && o.QuotationID.HasValue).FirstOrDefaultAsync();


                var bookingOwner = await DB.BookingOwners.Where(o => o.BookingID == model.ID).AnyAsync();

                bool? IsPaid2;
                bool? IsCanQuestionnaire = false;
                bool? Cansave = true;
                bool CanCancel = false;
                bool? CanConvert = false;
                if (BookingPayAmount == 0)
                {
                    IsPaid2 = null;
                    CanConvert = true;
                }

                else if (BookingPayAmount >= BookingAmount)
                {
                    IsPaid2 = true;
                    IsCanQuestionnaire = true;
                    CanCancel = true;
                    CanConvert = false;
                }
                else
                {
                    IsPaid2 = false;
                    IsCanQuestionnaire = false;
                    CanCancel = true;
                    if (model.Quotation?.RefQuotaionIDPrebook != null)
                    {
                        if (checkBackward != null)
                        {
                            CanConvert = false;
                        } 
                        else
                        {
                            CanConvert = true;
                        }
                    } 
                }

                if (IsPaid2 != null)
                {
                    Cansave = false;
                }
                else
                {
                    if (model.BookingStatus?.Key != BookingStatusKeys.Booking && model.BookingStatus?.Key != BookingStatusKeys.WaitForBookingConfirmation)
                    {
                        Cansave = false;
                    }
                }

                var result = new BookingDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy == null ? model.CreatedBy?.DisplayName : model.UpdatedBy?.DisplayName,
                    QuotationNo = model.Quotation?.QuotationNo,
                    QuotationID = model.Quotation?.ID,
                    IsFromPreBook = model.Quotation?.RefQuotaionIDPrebook != null ? true : false,
                    BookingNo = model.BookingNo,
                    BookingStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.BookingStatus),
                    Unit = await PRJ.UnitDropdownDTO.CreateFromModelAsync(model.Unit, DB),
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Model = PRJ.ModelDropdownDTO.CreateFromModel(model.Model),
                    SaleArea = model.SaleArea,
                    BookingDate = model.BookingDate,
                    ContractDueDate = model.ContractDueDate,
                    ApproveDate = model.ApproveDate,
                    ContractDate = model.ContractDueDate,
                    SaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.SaleOfficerType),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    Agent = MST.AgentDropdownDTO.CreateFromModel(model.Agent),
                    AgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(model.AgentEmployee),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.ProjectSaleUser),
                    CreateBookingFrom = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreateBookingFrom),
                    IsPaid = IsPaid2,
                    TransferOwnershipDate = model.TransferOwnershipDate,
                    CanSave = Cansave,
                    CanCancel = CanCancel,
                    CanQuestionnaire = IsCanQuestionnaire,
                    CreditBankingType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreditBankingType),
                    CreditBankingDate = model.CreditBankingDate,
                    CreditBankingByUser = UserDTO.CreateFromModel(model.CreditBankingByUser),
                    IsCanConvert = CanConvert,
                };

                if (model.ChangeFromBookingID != null)
                {
                    var unitbefor = await DB.Bookings.IgnoreQueryFilters().Include(o => o.Unit).Where(o => o.ID == model.ChangeFromBookingID).FirstOrDefaultAsync();
                    result.UnitNoBefor = unitbefor.Unit.UnitNo;
                }

                #region CanPay//CanPrint//CanManageAgreementOwner
                var waitingForApprovePriceListMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus
                                                                                   && o.Key == BookingStatusKeys.WaitingForApprovePriceList)
                                                                         .Select(o => o.ID)
                                                                         .FirstAsync();
                var waitingForApproveMinPriceMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus
                                                                    && o.Key == BookingStatusKeys.WaitingForApproveMinPrice)
                                                          .Select(o => o.ID)
                                                          .FirstAsync();


                if (!string.IsNullOrEmpty(model.BookingNo)
                    && (model.BookingStatusMasterCenterID != waitingForApprovePriceListMasterCenterID
                            && model.BookingStatusMasterCenterID != waitingForApproveMinPriceMasterCenterID))
                {
                    result.CanPay = string.IsNullOrEmpty(await DB.IsCanPayAsync(model.ID));
                    result.CanPrint = true;
                    result.CanManageAgreementOwner = true;
                }
                else
                {
                    result.CanPay = false;
                    result.CanPrint = false;
                    result.CanManageAgreementOwner = true;
                }
                #endregion

                #region CanCancel
                //var payments = await DB.Payments.Where(o => o.BookingID == model.ID).ToListAsync();
                //if (!string.IsNullOrEmpty(model.BookingNo)
                //   && payments.Any()
                //   && (model.BookingStatusMasterCenterID != waitingForApprovePriceListMasterCenterID
                //           && model.BookingStatusMasterCenterID != waitingForApproveMinPriceMasterCenterID)
                //   )
                //{
                //    result.CanCancel = true;
                //}
                //else
                //{
                //    result.CanCancel = false;
                //}
                #endregion

                #region CanChangeUnit
                //if (!string.IsNullOrEmpty(model.BookingNo))
                //{
                //    if (BookingPayAmount >= BookingAmount)
                //    {
                //        result.CanChangeUnit = true;
                //    }
                //    else
                //    {
                //        result.CanChangeUnit = false;
                //    }

                //}
                //else
                //{
                //    result.CanChangeUnit = false;
                //}
                #endregion

                #region formchangepro

                var changepromin = await DB.SalePromotions
                .Include(o => o.MasterPromotion)
                .Include(o => o.UpdatedBy)
                .Where(o => o.BookingID == model.ID && o.ChangePromotionWorkflowID != null && o.IsActive == false)
                .OrderByDescending(o => o.Created).FirstOrDefaultAsync();

                if (changepromin != null)
                {
                    result.Ischangepromin = true;
                }
                else
                {
                    result.Ischangepromin = false;
                }
                #endregion

                var workflow = await DB.MinPriceBudgetWorkflows
                .Include(o => o.Project)
                .Include(o => o.MinPriceBudgetWorkflowStage)
                .Include(o => o.MinPriceWorkflowType)
                .Include(o => o.BudgetPromotionType)
                .Include(o => o.Booking)
                .ThenInclude(o => o.Unit)
                .Where(o => o.BookingID == model.ID && o.IsCancelled == false)
                .OrderByDescending(o => o.Created).FirstOrDefaultAsync();
                if (workflow != null)
                {
                    result.MinPriceBudgetWorkflow = MinPriceBudgetWorkflowDTO.CreateFromModel(workflow);
                }

                result.TotalPayAmount = await DB.Payments.Where(o => o.BookingID == model.ID && o.IsCancel == false).SumAsync(o => o.TotalAmount);

                #region AreaPricePerUnit

                var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == model.ID && o.IsActive == true).FirstOrDefaultAsync();
                if (unitPrice != null)
                {
                    var TitledeedDetail = await DB.TitledeedDetails.Where(o => o.UnitID == model.UnitID).FirstOrDefaultAsync();
                    if (TitledeedDetail != null)
                    {
                        var Unit = await DB.Units.Where(o => o.ID == model.UnitID).FirstOrDefaultAsync();
                        if (Unit != null)
                        {
                            //result.Unit = UnitDropdownDTO.CreateFromModel(Unit);

                            var TitleDeed = PRJ.TitleDeedDTO.CreateFromModel(TitledeedDetail);
                            double? AddOnArea = (TitleDeed?.TitledeedArea ?? 0.00) - (Unit?.SaleArea ?? 0.00);

                            //if (TitleDeed?.TitledeedArea != null)
                            //{
                            //    result.Unit.UsedArea = TitledeedDetail?.TitledeedArea;
                            //}
                            //else
                            //{
                            //    result.Unit.UsedArea = Unit?.SaleArea;
                            //}

                            var productTypeId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProductType && o.Key == ProductTypeKeys.LowRise).Select(o => o.ID).FirstOrDefaultAsync();


                            var Project = await DB.Projects.Include(o => o.ProductType).Where(o => o.ID == model.ProjectID).FirstOrDefaultAsync();
                            if (Project != null)
                            {
                                result.Project = ProjectDropdownDTO.CreateFromModel(Project);

                                if (result.Project.ProductType?.Id == productTypeId)
                                {
                                    // Case แนวราบ
                                    var pricelistId = await DB.PriceLists.Where(o => o.UnitID == model.UnitID).OrderByDescending(o => o.ActiveDate).Select(o => o.ID).FirstOrDefaultAsync();
                                    var pricelistItem = await DB.PriceListItems.Where(o => o.PriceListID == pricelistId && o.Order == 6).Select(o => o.PricePerUnitAmount).FirstOrDefaultAsync();
                                    result.AreaPricePerUnit = pricelistItem ?? 0;
                                }
                                else
                                {
                                    // Case แนวสูง
                                    //result.AreaPricePerUnit = ((unitPrice.SellingPrice ?? 0) / (decimal)(TitleDeed.TitledeedArea != null ? TitleDeed.TitledeedArea : Unit?.SaleArea != null ? Unit?.SaleArea : 1));
                                    result.AreaPricePerUnit = Math.Ceiling(((unitPrice.SellingPrice ?? 0) / (decimal)(model.SaleArea != null ? model.SaleArea : 1)));
                                }
                            }

                            result.OffsetAreaPrice = Math.Ceiling(((result.AreaPricePerUnit ?? 0) * (decimal)(AddOnArea ?? 1)));
                        }
                    }
                }
                #endregion

                #region IsTransferConfirmed for check Creditbanking

                var IsTransferConfirmed = false;
                var agreement = await DB.Agreements.Where(o => o.BookingID == model.ID && o.IsCancel == false).FirstOrDefaultAsync();
                if (agreement != null)
                {
                    var transfer = await DB.Transfers.Where(o => o.AgreementID == agreement.ID).FirstOrDefaultAsync();
                    if (transfer != null)
                    {
                        if (!string.IsNullOrEmpty(transfer.TransferNo))
                        {
                            IsTransferConfirmed = true;
                        }
                    }
                }
                result.IsTransferConfirmed = IsTransferConfirmed;


                #endregion

                #region UnlockBookingdate
                if (model.Project != null)
                {
                    if (model.Project.StartUnlockBookingDate != null && model.Project.EndUnlockBookingDate != null)
                    {
                        var today = DateTime.Now.Date;
                        if (today >= model.Project.StartUnlockBookingDate && today <= model.Project.EndUnlockBookingDate)
                        {
                            result.UnlockBooking = true;
                        }
                        else
                        {
                            result.UnlockBooking = false;
                        }
                    }
                    else
                    {
                        result.UnlockBooking = false;
                    }
                }
                { 
                        result.UnlockBooking = false;
                }

                #endregion

                result.IsHasOwner = bookingOwner;

                result.PaymentID = await DB.Payments.Where(o => o.BookingID == model.ID).Select(o => o.ID).FirstOrDefaultAsync();
                #region online Payment
                if(model.Quotation != null)
                { 
                    var onlinePayment = await DB.OnlinePaymentPrebooks.Where(o => model.Quotation.RefOnlinePaymentPrebookID == o.ID).FirstOrDefaultAsync();
                    if (onlinePayment != null && (onlinePayment.IsCancel??false) == false )
                    {
                        result.IsFromOnlinePayment = true; 
                        result.OnlinePaymentContactID = onlinePayment.ContactID;
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

        public static BookingDTO CreateFromModelForPayment(Booking model)
        {
            if (model != null)
            {
                var result = new BookingDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy == null ? model.CreatedBy?.DisplayName : model.UpdatedBy?.DisplayName,
                    QuotationNo = model.Quotation?.QuotationNo,
                    BookingNo = model.BookingNo,
                    BookingStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.BookingStatus),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Model = PRJ.ModelDropdownDTO.CreateFromModel(model.Model),
                    SaleArea = model.SaleArea,
                    BookingDate = model.BookingDate,
                    ContractDueDate = model.ContractDueDate,
                    ApproveDate = model.ApproveDate,
                    ContractDate = model.ContractDueDate,
                    SaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.SaleOfficerType),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    Agent = MST.AgentDropdownDTO.CreateFromModel(model.Agent),
                    AgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(model.AgentEmployee),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.ProjectSaleUser),
                    CreateBookingFrom = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreateBookingFrom),
                    TransferOwnershipDate = model.TransferOwnershipDate,
                    CreditBankingType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreditBankingType),
                    CreditBankingDate = model.CreditBankingDate,
                    CreditBankingByUser = UserDTO.CreateFromModel(model.CreditBankingByUser)
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static BookingDTO CreateFromModel(models.SAL.Booking model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    QuotationNo = model.Quotation?.QuotationNo,
                    BookingNo = model.BookingNo,
                    BookingStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.BookingStatus),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Model = PRJ.ModelDropdownDTO.CreateFromModel(model.Model),
                    SaleArea = model.SaleArea,
                    BookingDate = model.BookingDate,
                    ContractDueDate = model.ContractDueDate,
                    ApproveDate = model.ApproveDate,
                    ContractDate = model.ContractDueDate,
                    SaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.SaleOfficerType),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    Agent = MST.AgentDropdownDTO.CreateFromModel(model.Agent),
                    AgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(model.AgentEmployee),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.ProjectSaleUser),
                    CreateBookingFrom = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreateBookingFrom),
                    IsPaid = model.IsPaid,
                    TransferOwnershipDate = model.TransferOwnershipDate,
                    CanSave = true,
                    CanQuestionnaire = false,
                    CreditBankingType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreditBankingType),
                    CreditBankingDate = model.CreditBankingDate,
                    CreditBankingByUser = UserDTO.CreateFromModel(model.CreditBankingByUser)
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
