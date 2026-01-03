using Base.DTOs.MST;
using Database.Models;
using Database.Models.DbQueries.FIN;
using Database.Models.DbQueries.Finance;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRM;
using Database.Models.SAL;
using ErrorHandling;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    public class PaymentFormDTO
    {
        /// <summary>
        /// บริศัท
        /// </summary>
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// รายการที่ต้องชำระ
        /// </summary>
        public List<PaymentUnitPriceItemDTO> PaymentItems { get; set; }

        /// <summary>
        /// วิธีการชำระ
        /// </summary>
        public List<PaymentMethodDTO> PaymentMethods { get; set; }

        /// <summary>
        /// ชนิดของช่องทางชำระที่สามารถชำระได้
        /// </summary>
        public List<MasterCenterDropdownDTO> AuthorizedPaymentMethodTypes { get; set; }

        /// <summary>
        /// วันที่ชำระ
        /// </summary>
        [Description("วันที่ชำระ")]
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// ไฟล์แนบ
        /// </summary>
        public FileDTO AttachFile { get; set; }

        /// <summary>
        /// หมายเหตุ (บันทึกข้อความ)
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// หมายเหตุ (ReceiptTempHeader)
        /// </summary>
        public string CancelRemark { get; set; }

        /// <summary>
        /// ชนิดของ PaymentForm
        /// </summary>
        public PaymentFormType PaymentFormType { get; set; }

        /// <summary>
        /// สามารถเพิ่ม Payment Method ใหม่ได้
        /// </summary>
        public bool CanAddNewPaymentMethod { get; set; }

        /// <summary>
        /// สามารถแก้ไขยอดชำระเงินได้
        /// </summary>
        public bool CanEditPayAmount { get; set; }

        /// <summary>
        /// ID ของ UnknownPayment หรืออื่นๆ
        /// </summary>
        public Guid? RefID { get; set; }

        public bool? IsWrongAccount { get; set; }

        public Guid? PaymentID { get; set; }
        /// <summary>
        /// เลข UN
        /// </summary>
        public string UnknownPaymentCode { get; set; }

        public bool? IsValidateComfirm { get; set; }

        public string ValidateComfirmMsg { get; set; }

        public bool? IsPaidExcess { get; set; }
        public bool? IsReturnTranfer { get; set; }

        public bool IsFromLC { get; set; } = false;

        public Guid? BookingID { get; set; }

        public int? StatusCode { get; set; }
        public string StatusMsg { get; set; }
        public Guid? DownPaymentLetterID { get; set; }
        public string DownPaymentLetterNo { get; set; }
        public int? TotalPeriodOverDue { get; set; }
        public decimal? TotalAmountOverDue { get; set; }
        public bool? IsProjectTransferLetter { get; set; }
        public decimal? UnknowPaymentWaitingAmount { get; set; }
        public decimal? TotalPaidAfterDownPaymentDate { get; set; }
        public decimal? TotalNetPaidAmount { get; set; }

        public SAL.BookingOwnerDTO BookingOwner { get; set; }
        public bool? IsPaidBookingAmount { get; set; }
        public bool? IsPaidPartialBookingAmount { get; set; }
        public Guid? PaymentBookingID { get; set; }
        public string QuotationNo { get; set; }
        public bool? IsAlreadyAgreement { get; set; }
        public decimal? TotalAmount { get; set; }

        public static PaymentFormDTO CreateFromCheckPaidModel(sp_CheckPaidDownPayment model)
        {
            if (model != null)
            {
                PaymentFormDTO result = new PaymentFormDTO()
                {
                    StatusCode = model.StatusCode,
                    StatusMsg = model.StatusMsg,
                    DownPaymentLetterID = model.DownPaymentLetterID,
                    DownPaymentLetterNo = model.DownPaymentLetterNo,
                    TotalPeriodOverDue = model.TotalPeriodOverDue,
                    TotalAmountOverDue = model.TotalAmountOverDue,
                    IsProjectTransferLetter = model.IsProjectTransferLetter,
                    UnknowPaymentWaitingAmount = model.UnknowPaymentWaitingAmount,
                    TotalPaidAfterDownPaymentDate = model.TotalPaidAfterDownPaymentDate,
                    TotalNetPaidAmount = model.TotalNetPaidAmount
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task<PaymentFormDTO> CreateFromBookingAsync(Booking model, DatabaseContext db, DbQueryContext dbq, Guid? refID = null, PaymentFormType formType = PaymentFormType.Normal, decimal paidAmount = 0)
        {
            if (model != null)
            {
                var result = new PaymentFormDTO();

                result.Company = CompanyDropdownDTO.CreateFromModel(model.Project?.Company);
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit);
                result.PaymentItems = new List<PaymentUnitPriceItemDTO>();
                result.PaymentMethods = new List<PaymentMethodDTO>();
                result.RefID = refID;
                result.CanAddNewPaymentMethod = formType == PaymentFormType.Normal ? true : false;
                result.CanEditPayAmount = formType == PaymentFormType.Normal ? true : false;
                result.AuthorizedPaymentMethodTypes = new List<MasterCenterDropdownDTO>();

                if (formType == PaymentFormType.Normal)
                {
                    var allPaymentBooking = PaymentMethodKeys.NeedToDepositKeys;
                    var masterCenterAuthorizeds = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && allPaymentBooking.Contains(o.Key)).ToListAsync();
                    result.AuthorizedPaymentMethodTypes.AddRange(masterCenterAuthorizeds.Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o)).ToList());
                }
                else if (formType == PaymentFormType.UnknownPayment)
                {
                    var masterCenterAuthorized = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && o.Key == PaymentMethodKeys.UnknowPayment).FirstAsync();
                    result.AuthorizedPaymentMethodTypes.Add(MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized));
                    result.PaymentMethods = new List<PaymentMethodDTO> { new PaymentMethodDTO { PayAmount = paidAmount, PaymentMethodType = MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized) } };
                }

                return result;
            }
            else
            {
                return null;
            }

        }

        //public static async Task<List<PaymentFormDTO>> CreateFromBookingListAsync(List<Booking> models, DatabaseContext db, DbQueryContext dbq, Guid? refID = null, PaymentFormType formType = PaymentFormType.Normal, decimal paidAmount = 0)
        //{
        //    var paymentFormList = new List<PaymentFormDTO>();

        //    foreach (var model in models)
        //    { 
        //        if (model != null)
        //        {
        //            var result = new PaymentFormDTO();

        //            result.Company = CompanyDropdownDTO.CreateFromModel(model.Project?.Company);
        //            result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
        //            result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit);
        //            result.PaymentItems = new List<PaymentUnitPriceItemDTO>();
        //            result.PaymentMethods = new List<PaymentMethodDTO>();
        //            result.RefID = refID;
        //            result.CanAddNewPaymentMethod = formType == PaymentFormType.Normal ? true : false;
        //            result.CanEditPayAmount = formType == PaymentFormType.Normal ? true : false;
        //            result.AuthorizedPaymentMethodTypes = new List<MasterCenterDropdownDTO>();

        //            if (formType == PaymentFormType.Normal)
        //            {
        //                var allPaymentBooking = PaymentMethodKeys.NeedToDepositKeys;
        //                var masterCenterAuthorizeds = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && allPaymentBooking.Contains(o.Key)).ToListAsync();
        //                result.AuthorizedPaymentMethodTypes.AddRange(masterCenterAuthorizeds.Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o)).ToList());
        //            }
        //            else if (formType == PaymentFormType.UnknownPayment)
        //            {
        //                var masterCenterAuthorized = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && o.Key == PaymentMethodKeys.UnknowPayment).FirstAsync();
        //                result.AuthorizedPaymentMethodTypes.Add(MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized));
        //                result.PaymentMethods = new List<PaymentMethodDTO> { new PaymentMethodDTO { PayAmount = paidAmount, PaymentMethodType = MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized) } };
        //            }

        //            paymentFormList.Add(result);
        //        }
        //    }

        //    return paymentFormList;
        //}

        public static async Task<PaymentFormDTO> CreateFromQuatationAsync(Quotation model, DatabaseContext db, DbQueryContext dbq, Guid? refID = null, PaymentFormType formType = PaymentFormType.Normal, decimal paidAmount = 0)
        {
            if (model != null)
            {
                var result = new PaymentFormDTO();

                result.Company = CompanyDropdownDTO.CreateFromModel(model.Project?.Company);
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit);
                result.PaymentItems = new List<PaymentUnitPriceItemDTO>();
                result.PaymentMethods = new List<PaymentMethodDTO>();
                result.RefID = refID;
                result.CanAddNewPaymentMethod = formType == PaymentFormType.Normal ? true : false;
                result.CanEditPayAmount = formType == PaymentFormType.Normal ? true : false;
                result.AuthorizedPaymentMethodTypes = new List<MasterCenterDropdownDTO>();

                if (formType == PaymentFormType.Normal)
                {
                    var allPaymentBooking = PaymentMethodKeys.NeedToDepositKeys;
                    var masterCenterAuthorizeds = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && allPaymentBooking.Contains(o.Key)).ToListAsync();
                    result.AuthorizedPaymentMethodTypes.AddRange(masterCenterAuthorizeds.Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o)).ToList());
                }
                else if (formType == PaymentFormType.UnknownPayment)
                {
                    var masterCenterAuthorized = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && o.Key == PaymentMethodKeys.UnknowPayment).FirstAsync();
                    result.AuthorizedPaymentMethodTypes.Add(MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized));
                    result.PaymentMethods = new List<PaymentMethodDTO> { new PaymentMethodDTO { PayAmount = paidAmount, PaymentMethodType = MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized) } };
                }

                return result;
            }
            else
            {
                return null;
            }

        }

        public static PaymentFormDTO CreateFromBookingBillPayment(Booking model, DatabaseContext db, FileHelper fileHelper, Guid? refID = null, PaymentFormType formType = PaymentFormType.Normal, decimal paidAmount = 0)
        {
            if (model != null)
            {
                var result = new PaymentFormDTO();

                result.Company = new CompanyDropdownDTO { Id = model.Project.CompanyID ?? Guid.Empty };
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit);
                result.PaymentItems = new List<PaymentUnitPriceItemDTO>();
                result.PaymentMethods = new List<PaymentMethodDTO>();
                result.RefID = refID;
                result.CanAddNewPaymentMethod = formType == PaymentFormType.Normal ? true : false;
                result.CanEditPayAmount = formType == PaymentFormType.Normal ? true : false;
                result.AuthorizedPaymentMethodTypes = new List<MasterCenterDropdownDTO>();

                return result;
            }
            else
            {
                return null;
            }

        }

        public static PaymentFormDTO CreateFromSqlBookingForPayment(sqlBookingForPayment.QueryResult model)
        {
            if (model != null)
            {
                var result = new PaymentFormDTO();

                result.Company = new CompanyDropdownDTO { Id = model.CompanyID ?? Guid.Empty };
                result.Project = new PRJ.ProjectDropdownDTO();
                result.Unit = new PRJ.UnitDropdownDTO();
                result.PaymentItems = new List<PaymentUnitPriceItemDTO>();
                result.PaymentMethods = new List<PaymentMethodDTO>();
                result.CanAddNewPaymentMethod = false;
                result.CanEditPayAmount = false;
                result.AuthorizedPaymentMethodTypes = new List<MasterCenterDropdownDTO>();
                return result;
            }
            else
            {
                return null;
            }

        }

        public static async Task<PaymentFormDTO> CreateFromReceiptAsync(Booking model, DatabaseContext db, Guid PaymentID, DateTime ReceiveDate, string Remark, string CancelRemark)
        {
            if (model != null)
            {
                var result = new PaymentFormDTO();
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit);
                result.PaymentItems = new List<PaymentUnitPriceItemDTO>();
                result.Company = CompanyDropdownDTO.CreateFromModel(model.Project?.Company);
                result.Remark = Remark;
                result.CancelRemark = CancelRemark;
                result.ReceiveDate = ReceiveDate;
                var PaymentMethodsModel = await db.PaymentMethods
                    .Include(x => x.Bank)
                    .Include(x => x.BankAccount)
                    .Include(x => x.CreditCardPaymentType)
                    .Include(x => x.PaymentMethodType)
                    .Include(x => x.CreditCardType)
                    .Include(x => x.EDCBank)
                    .Include(x => x.FeeConfirmByUser)
                    .Include(x => x.ForeignTransferType)
                    .Include(x => x.PaymentMethodType)
                    .Include(x => x.PayToCompany)
                    .Include(x => x.DirectCreditDebitExportDetail)
                    .ThenInclude(x=>x.DirectCreditDebitApprovalForm)
                    .ThenInclude(x=>x.BankAccount)
                    .ThenInclude(x=>x.Bank)
                    .Include(x => x.ForeignTransferType)
                    .Include(x => x.UnknownPayment)
                    .Include(x => x.ChangeUnitWorkFlow)
                    .Include(x => x.BillPaymentDetail)
                    .Where(x => x.PaymentID == PaymentID).ToListAsync();
                result.PaymentMethods = PaymentMethodsModel.Select(x => PaymentMethodDTO.CreateFromModel(x)).ToList();
                var UnknownPayment = PaymentMethodsModel.Select(x => x.UnknownPayment).FirstOrDefault();
                if (UnknownPayment != null)
                {
                    result.UnknownPaymentCode = UnknownPayment.UnknownPaymentCode;
                }

                result.AttachFile = new FileDTO();


                result.AuthorizedPaymentMethodTypes = new List<MST.MasterCenterDropdownDTO>();
                var masterCenterAuthorizeds = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod).OrderBy(o => o.Order).ThenBy(o => o.Name).ToListAsync();

                if (masterCenterAuthorizeds.Any())
                    result.AuthorizedPaymentMethodTypes.AddRange(masterCenterAuthorizeds.Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o)).ToList());

                var UnitPriceItem = await db.PaymentItems.Where(x => x.PaymentID == PaymentID).Include(x => x.MasterPriceItem).ToListAsync();

                var MasterPriceItem = db.MasterPriceItems.ToList();

                foreach (var item in UnitPriceItem)
                {
                    int order = 0;
                    string Name = "";
                    if (item.MasterPriceItem.Key == MasterPriceItemKeys.BookingAmount)
                    {
                        order = 1;
                        Name = MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.BookingAmount).Select(e => e.Detail).FirstOrDefault();
                    }
                    else if (item.MasterPriceItem.Key == MasterPriceItemKeys.ContractAmount)
                    {
                        order = 2;
                        Name = MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.ContractAmount).Select(e => e.Detail).FirstOrDefault();
                    }
                    else if (item.MasterPriceItem.Key == MasterPriceItemKeys.AdvanceContractPayment)
                    {
                        order = 2;
                        Name = MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.AdvanceContractPayment).Select(e => e.Detail).FirstOrDefault();
                    }
                    else if (item.MasterPriceItem.Key == MasterPriceItemKeys.InstallmentAmount)
                    {
                        order = 3;
                        Name = MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.InstallmentAmount).Select(e => e.Detail).FirstOrDefault() + " งวดที่ " + item.Period;
                    }
                    else
                    {
                        order = 4;
                        Name = MasterPriceItem.Where(e => e.Key == item.MasterPriceItem.Key).Select(e => e.Detail).FirstOrDefault();
                    }

                    PaymentUnitPriceItemDTO newPaymentUnitPriceItemDTO = new PaymentUnitPriceItemDTO
                    {
                        MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(item.MasterPriceItem),
                        PayAmount = item.PayAmount,
                        Period = item.Period,
                        Order = order
                    };

                    newPaymentUnitPriceItemDTO.Name = Name;
                    result.PaymentItems.Add(newPaymentUnitPriceItemDTO);
                    result.PaymentItems = result.PaymentItems.OrderBy(e => e.Order).ThenBy(e => e.Period).ToList();
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public static PaymentUnitPriceItemDTO GetPaymentUnitPriceItemDTOByBookingID(dbqSPPaymentUnitPriceList model)
        {
            var PaymentItems = new PaymentUnitPriceItemDTO();

            if (model != null)
            {
                PaymentItems.Id = model.ID;
                PaymentItems.Name = model.Name;
                PaymentItems.ItemAmount = model.ItemAmount ?? 0;
                PaymentItems.PaidAmount = model.PaidAmount ?? 0;
                PaymentItems.PaymentDate = model.PaymentDate;
                PaymentItems.RemainAmount = model.RemainAmount ?? 0;
                PaymentItems.PayAmount = model.PayAmount ?? 0;
                PaymentItems.Order = model.Orders ?? 0;
                PaymentItems.MasterPriceItem = new MasterPriceItemDTO { Id = model.MasterPriceItemID, Key = model.MasterPriceItemKey, Detail = model.MasterPriceItemDetail, DetailEN = model.MasterPriceItemDetailEN };
                PaymentItems.Period = model.Period ?? 0;
                PaymentItems.DueDate = model.DueDate;
                PaymentItems.UnitPriceID = model.UnitPriceID;

                return PaymentItems;
            }
            else
            {
                return null;
            }
        }

        public static PaymentUnitPriceItemDTO GetPaymentUnitPriceItemV2DTOByBookingID(dbqSPPaymentUnitPriceListV2 model)
        {
            var PaymentItems = new PaymentUnitPriceItemDTO();

            if (model != null)
            {
                PaymentItems.Id = model.ID;
                PaymentItems.Name = model.Name;
                PaymentItems.ItemAmount = model.ItemAmount ?? 0;
                PaymentItems.PaidAmount = model.PaidAmount ?? 0;
                PaymentItems.PaymentDate = model.PaymentDate;
                PaymentItems.RemainAmount = model.RemainAmount ?? 0;
                PaymentItems.PayAmount = model.PayAmount ?? 0;
                PaymentItems.Order = model.Orders ?? 0;
                PaymentItems.MasterPriceItem = new MasterPriceItemDTO { Id = model.MasterPriceItemID, Key = model.MasterPriceItemKey, Detail = model.MasterPriceItemDetail, DetailEN = model.MasterPriceItemDetailEN };
                PaymentItems.Period = model.Period ?? 0;
                PaymentItems.DueDate = model.DueDate;
                PaymentItems.UnitPriceID = model.UnitPriceID;


                PaymentItems.PaymentMethodTypeID = model.PaymentMethodTypeID;
                PaymentItems.IsPaidAll = model.IsPaidAll;
                PaymentItems.IsPaidPartial = model.IsPaidPartial;
                PaymentItems.PaymentMethodKey = model.PaymentMethodKey;
                PaymentItems.PaymentMethodName = model.PaymentMethodName;

                return PaymentItems;
            }
            else
            {
                return null;
            }
        }

        public static async Task<List<PaymentUnitPriceItemDTO>> GetPaymentUnitPriceItemDTOByBookingID2(Guid BookingID, DatabaseContext db)
        {
            var PaymentItems = new List<PaymentUnitPriceItemDTO>();

            var UnitPrice = await db.UnitPrices
                                 .Include(o => o.Booking)
                                 .Include(o => o.UnitPriceStage)
                                 .Where(o => o.BookingID == BookingID).ToListAsync() ?? new List<UnitPrice>();

            var Booking = UnitPrice.Select(o => o.Booking).FirstOrDefault() ?? new Booking();
            var Agreement = await db.Agreements.Where(o => o.BookingID == BookingID).FirstOrDefaultAsync() ?? new Agreement();
            var Transfer = await db.Transfers.Where(o => o.AgreementID == Agreement.ID).FirstOrDefaultAsync() ?? new Transfer();

            var CurrentUnitPrice = UnitPrice.Where(e => e.IsActive == true).FirstOrDefault();

            var BookingPrice = UnitPrice.Where(o => o.UnitPriceStage?.Key == UnitPriceStageKeys.Booking).FirstOrDefault() ?? new UnitPrice();

            var AgreementPrice = UnitPrice.Where(o => o.UnitPriceStage?.Key == UnitPriceStageKeys.Agreement).FirstOrDefault() ?? new UnitPrice();

            var InstallmentPrice = await db.UnitPriceInstallments
                            .Include(o => o.UnitPrice)
                            .Where(o => o.UnitPrice.BookingID == BookingID && o.UnitPriceID == AgreementPrice.ID).ToListAsync();

            var OtherPrice = await (from unitPriceItem in db.UnitPriceItems.Include(o => o.MasterPriceItem).ThenInclude(o => o.UnitPriceStage)
                                    .Where(o => o.UnitPriceID == CurrentUnitPrice.ID)

                                    join salPro in db.SalePromotionExpenses.Include(o => o.SalePromotion)
                                        .Where(o => o.SalePromotion.BookingID == BookingID && o.SalePromotion.IsActive)
                                    on unitPriceItem.MasterPriceItemID equals salPro.MasterPriceItemID into salProData
                                    from salProModel in salProData.DefaultIfEmpty()

                                    select new UnitPriceItemWithPromotion
                                    {
                                        UnitPriceItem = unitPriceItem,
                                        SalePromotionExpense = salProModel ?? new SalePromotionExpense(),
                                        MasterPriceItem = unitPriceItem.MasterPriceItem,
                                        UnitPriceStage = unitPriceItem.MasterPriceItem.UnitPriceStage
                                    }).ToListAsync();

            OtherPrice = OtherPrice
               .Where(o => (o.UnitPriceStage?.Order <= CurrentUnitPrice?.UnitPriceStage?.Order || o.MasterPriceItem?.UnitPriceStage == null)
                && (o.SalePromotionExpense?.BuyerAmount ?? o.UnitPriceItem.Amount) > 0
               ).ToList();

            var UnitPayment = await db.PaymentItems
                            .Include(o => o.Payment)
                            .Include(o => o.MasterPriceItem)
                            .Where(o => o.Payment.BookingID == BookingID && o.Payment.IsCancel == false).ToListAsync();

            var UnitPaymentByMasterItem = UnitPayment
                 .GroupBy(l => new { l.MasterPriceItem, l.Period })
                 .Select(cl => new UnitPaymentResult
                 {
                     BookingID = cl.Select(x => x.Payment.BookingID).FirstOrDefault(),
                     MasterPrice = cl.Select(x => x.MasterPriceItem).FirstOrDefault(),
                     Period = cl.Select(x => x.Period).FirstOrDefault(),
                     PayAmount = cl.Sum(c => c.PayAmount),
                     PaymentDate = cl.Max(c => c.Payment.ReceiveDate)
                 }).ToList();

            var MasterPriceItem = db.MasterPriceItems.ToList();

            #region เงินจอง
            if (BookingPrice != null)
            {
                var Payment = UnitPaymentByMasterItem.Where(e => e.MasterPrice.Key == MasterPriceItemKeys.BookingAmount).FirstOrDefault() ?? new UnitPaymentResult();

                var PaymentByItem = new PaymentUnitPriceItemDTO();

                PaymentByItem.Name = MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.BookingAmount).Select(e => e.Detail).FirstOrDefault();
                PaymentByItem.ItemAmount = BookingPrice.BookingAmount ?? 0;
                PaymentByItem.PaidAmount = Payment?.PayAmount ?? 0;
                PaymentByItem.PaymentDate = Payment?.PaymentDate;
                PaymentByItem.RemainAmount = (Payment?.PayAmount > BookingPrice.BookingAmount) ? 0 : (BookingPrice.BookingAmount ?? 0) - (Payment.PayAmount ?? 0);
                PaymentByItem.PayAmount = Payment?.PayAmount ?? 0;
                PaymentByItem.Order = 1;
                PaymentByItem.MasterPriceItem = MasterPriceItemDTO.CreateFromModel(MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.BookingAmount).FirstOrDefault());
                PaymentByItem.Period = 0;
                PaymentByItem.DueDate = Booking.BookingDate;

                PaymentByItem.Id = PaymentByItem.MasterPriceItem.Id;

                PaymentItems.Add(PaymentByItem);
            }
            #endregion

            #region รับเงินก่อนทำสัญญา
            var AdvanceContractPayment = UnitPaymentByMasterItem.Where(o => o.MasterPrice.Key == MasterPriceItemKeys.AdvanceContractPayment).ToList();
            if (AdvanceContractPayment.Any())
            {
                var PaymentByItem = new PaymentUnitPriceItemDTO();

                PaymentByItem.Name = AdvanceContractPayment.FirstOrDefault().MasterPrice.Detail;
                PaymentByItem.ItemAmount = AdvanceContractPayment.Sum(o => o.PayAmount) ?? 0;
                PaymentByItem.PaidAmount = AdvanceContractPayment.Sum(o => o.PayAmount) ?? 0;
                PaymentByItem.PaymentDate = AdvanceContractPayment.Max(o => o.PaymentDate);
                PaymentByItem.RemainAmount = 0;
                PaymentByItem.PayAmount = AdvanceContractPayment.Sum(o => o.PayAmount) ?? 0;
                PaymentByItem.Order = 2;
                PaymentByItem.MasterPriceItem = MasterPriceItemDTO.CreateFromModel(AdvanceContractPayment.FirstOrDefault().MasterPrice);
                PaymentByItem.Period = 0;
                PaymentByItem.DueDate = null;

                PaymentByItem.Id = PaymentByItem.MasterPriceItem.Id;

                PaymentItems.Add(PaymentByItem);
            }
            #endregion

            #region สัญญา
            if (AgreementPrice != null && CurrentUnitPrice.UnitPriceStage.Key != UnitPriceStageKeys.Booking)
            {
                var Payment = UnitPaymentByMasterItem.Where(e => e.MasterPrice.Key == MasterPriceItemKeys.ContractAmount).FirstOrDefault() ?? new UnitPaymentResult();

                var PaymentByItem = new PaymentUnitPriceItemDTO();

                PaymentByItem.Id = AgreementPrice.ID;
                PaymentByItem.Name = MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.ContractAmount).Select(e => e.Detail).FirstOrDefault();
                PaymentByItem.ItemAmount = AgreementPrice.AgreementAmount ?? 0;
                PaymentByItem.PaidAmount = Payment?.PayAmount ?? 0;
                PaymentByItem.PaymentDate = Payment?.PaymentDate;
                PaymentByItem.RemainAmount = (AgreementPrice.AgreementAmount ?? 0) - (Payment?.PayAmount ?? 0);
                PaymentByItem.PayAmount = Payment?.PayAmount ?? 0;
                PaymentByItem.Order = 3;
                PaymentByItem.MasterPriceItem = MasterPriceItemDTO.CreateFromModel(MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.ContractAmount).FirstOrDefault());
                PaymentByItem.Period = 0;
                PaymentByItem.DueDate = Agreement.ContractDate;

                PaymentItems.Add(PaymentByItem);
            }
            #endregion

            #region ค่างวด
            if (InstallmentPrice != null && CurrentUnitPrice.UnitPriceStage.Key != UnitPriceStageKeys.Booking)
            {
                foreach (var mst in InstallmentPrice)
                {
                    var Payment = UnitPaymentByMasterItem.Where(e => e.Period == mst.Period && e.MasterPrice.Key == MasterPriceItemKeys.InstallmentAmount).FirstOrDefault();

                    var PaymentByItem = new PaymentUnitPriceItemDTO();

                    PaymentByItem.Id = mst.ID;
                    PaymentByItem.UnitPriceID = mst.UnitPriceID;
                    PaymentByItem.Name = MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.InstallmentAmount).Select(e => e.Detail).FirstOrDefault() + (mst.IsSpecialInstallment ? "พิเศษ" : "") + " งวดที่ " + mst.Period;
                    PaymentByItem.ItemAmount = mst.Amount;
                    PaymentByItem.PaidAmount = Payment?.PayAmount ?? 0;
                    PaymentByItem.PaymentDate = Payment?.PaymentDate;
                    PaymentByItem.RemainAmount = mst.Amount - (Payment?.PayAmount ?? 0);
                    PaymentByItem.PayAmount = Payment?.PayAmount ?? 0;
                    PaymentByItem.Order = 4;
                    PaymentByItem.MasterPriceItem = MasterPriceItemDTO.CreateFromModel(MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.InstallmentAmount).FirstOrDefault());
                    PaymentByItem.Period = mst.Period;
                    PaymentByItem.DueDate = mst.DueDate;

                    PaymentItems.Add(PaymentByItem);
                }
            }
            #endregion

            #region ยอดโอนกรรมสิทธิ์
            if (CurrentUnitPrice != null && CurrentUnitPrice.UnitPriceStage?.Key != UnitPriceStageKeys.Booking)
            {
                var Payment = UnitPaymentByMasterItem.Where(e => e.MasterPrice.Key == MasterPriceItemKeys.TransferAmount).FirstOrDefault() ?? new UnitPaymentResult();

                var PaymentByItem = new PaymentUnitPriceItemDTO();

                PaymentByItem.Name = MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.TransferAmount).Select(e => e.Detail).FirstOrDefault();
                PaymentByItem.ItemAmount = CurrentUnitPrice.TransferAmount ?? 0;
                PaymentByItem.PaidAmount = Payment.PayAmount ?? 0;
                PaymentByItem.PaymentDate = Payment?.PaymentDate;
                PaymentByItem.RemainAmount = (CurrentUnitPrice.TransferAmount ?? 0) - (Payment?.PayAmount ?? 0);
                PaymentByItem.PayAmount = Payment.PayAmount ?? 0;
                PaymentByItem.Order = 50;
                PaymentByItem.MasterPriceItem = MasterPriceItemDTO.CreateFromModel(MasterPriceItem.Where(e => e.Key == MasterPriceItemKeys.TransferAmount).FirstOrDefault());
                PaymentByItem.Period = 0;
                PaymentByItem.DueDate = Agreement?.TransferOwnershipDate;

                PaymentByItem.Id = PaymentByItem.MasterPriceItem.Id;

                PaymentItems.Add(PaymentByItem);
            }
            #endregion

            #region ค่าใช้จ่ายอื่นๆ
            if ((Transfer?.IsReadyToTransfer ?? false))
            {
                foreach (var mst in OtherPrice)
                {
                    var Payment = UnitPaymentByMasterItem.Where(e => e.MasterPrice.Key == mst.MasterPriceItem.Key).FirstOrDefault() ?? new UnitPaymentResult();

                    var PaymentByItem = new PaymentUnitPriceItemDTO();

                    PaymentByItem.Id = mst.UnitPriceItem.ID;
                    PaymentByItem.UnitPriceID = mst.UnitPriceItem.UnitPriceID;
                    PaymentByItem.Name = MasterPriceItem.Where(e => e.Key == mst.MasterPriceItem.Key).Select(e => e.Detail).FirstOrDefault();
                    PaymentByItem.ItemAmount = mst.SalePromotionExpense?.BuyerAmount ?? mst.UnitPriceItem.Amount;
                    PaymentByItem.PaidAmount = Payment?.PayAmount ?? 0;
                    PaymentByItem.PaymentDate = Payment?.PaymentDate;
                    PaymentByItem.RemainAmount = PaymentByItem.ItemAmount - (Payment?.PayAmount ?? 0);
                    PaymentByItem.PayAmount = Payment?.PayAmount ?? 0;
                    PaymentByItem.Order = mst.MasterPriceItem.Order + 60;
                    PaymentByItem.MasterPriceItem = MasterPriceItemDTO.CreateFromModel(mst.MasterPriceItem);
                    PaymentByItem.Period = 0;
                    //PaymentByItem.DueDate = mst.DueDate;

                    PaymentItems.Add(PaymentByItem);
                }
            }
            #endregion

            PaymentItems = PaymentItems.OrderBy(e => e.Order).ThenBy(e => e.Period).ToList();

            return PaymentItems;
        }

        public async Task ValidateBeforeUpdateAsync(int UpdateType, Guid? PaymentID, DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            if (UpdateType == 0)
                return;

            var msg = new ErrorMessage();
            bool NotFindPayment = false;
            bool ChkDeposit = false;
            bool ChkIsCancel = false;
            bool ChkPostRv = false;
            bool ChkACApprove = false;
            bool ChkChangeUnit = false;
            bool ChkPaymentStateTranfer = false;
            bool ChkOldReciveChangeUnit = false; //เป็นใบเสร็จเก่าจากการย้ายแปลง

            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0092").FirstOrDefaultAsync();

            var modelPayment = db.Payments.Include(x => x.PaymentState).Where(x => x.ID == PaymentID).FirstOrDefault();

            if (modelPayment == null)
            {
                NotFindPayment = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(modelPayment.DepositNo))
                {
                    var PaymentMethods = await db.PaymentMethods.Include(x => x.PaymentMethodType)
                        .Where(x => x.PaymentID == PaymentID && PaymentMethodKeys.IsDepositMethodType.Contains(x.PaymentMethodType.Key) && !modelPayment.QuotationID.HasValue).FirstOrDefaultAsync();

                    if (PaymentMethods != null)
                    {
                        ChkDeposit = true;
                    }
                }
                if (modelPayment.IsCancel)
                {
                    ChkIsCancel = true;
                }
                if (!string.IsNullOrEmpty(modelPayment.PostGLDocumentNo))
                {
                    ChkPostRv = true;
                }

                var TransferModel = await db.Transfers.Include(x => x.Agreement).Where(x => x.Agreement.BookingID == modelPayment.BookingID).FirstOrDefaultAsync();
                if (TransferModel != null)
                {
                    if (TransferModel.IsAccountApproved)
                    {
                        ChkACApprove = true;
                    }
                }

                var PaymentMethodChangeUnit = await db.PaymentMethods.Include(x => x.PaymentMethodType).Where(x => x.PaymentID == PaymentID && x.PaymentMethodType.Key == PaymentMethodKeys.ChangeContract).FirstOrDefaultAsync();
                if (PaymentMethodChangeUnit != null)
                {
                    ChkChangeUnit = true;
                }

                if (PaymentStateKeys.Transfer.Equals(modelPayment.PaymentState?.Key))
                {
                    ChkPaymentStateTranfer = true;
                }

                var BookingModel = await db.Bookings.Where(x => x.ID == modelPayment.BookingID).IgnoreQueryFilters().FirstOrDefaultAsync() ?? new Booking();
                if (BookingModel.IsCancelled == true || BookingModel.IsDeleted == true)
                {
                    ChkOldReciveChangeUnit = true;
                }
            }

            if (NotFindPayment || ChkDeposit || ChkIsCancel || ChkPostRv || ChkACApprove
                || ChkChangeUnit || ChkPaymentStateTranfer || ChkOldReciveChangeUnit)
            {
                if (UpdateType == 2)
                {
                    if (NotFindPayment)
                    {
                        ex.AddError(errMsg.Key, "ไม่สามารถลบรายการได้ เนื่องจากไม่พบข้อมูล", (int)errMsg.Type);
                    }
                    else if (ChkPaymentStateTranfer)
                    {
                        ex.AddError(errMsg.Key, "ไม่สามารถยกเลิกใบเสร็จได้ เนื่องจากเป็นใบเสร็จที่เกิดจากการรับชำระเงินหลังโอน", (int)errMsg.Type);
                    }
                    else if (ChkOldReciveChangeUnit)
                    {
                        ex.AddError(errMsg.Key, "ไม่สามารถยกเลิกใบเสร็จได้ เนื่องจากเป็นใบเสร็จที่ย้ายแปลงแล้ว", (int)errMsg.Type);
                    }
                    else if (ChkChangeUnit)
                    {
                        ex.AddError(errMsg.Key, "ไม่สามารถยกเลิกใบเสร็จได้ เนื่องจากเป็นใบเสร็จจากการย้ายแปลง", (int)errMsg.Type);
                    }
                    else if (ChkDeposit)
                    {
                        ex.AddError(errMsg.Key, "ใบเสร็จรายการนี้ได้นำฝากแล้ว ไม่สามารถยกเลิกได้ \n โปรดติดต่อทางการเงิน", (int)errMsg.Type);
                    }
                    else if (ChkIsCancel)
                    {
                        ex.AddError(errMsg.Key, "รายการที่เลือกถูกยกเลิกแล้ว \n ไม่สามารถดำเนินการได้", (int)errMsg.Type);
                    }
                    else if (ChkACApprove)
                    {
                        ex.AddError(errMsg.Key, "ไม่สามารถลบรายการได้ เนื่องจากเป็นห้องที่บัญชีอนุมัติแล้ว", (int)errMsg.Type);
                    }
                    else if (ChkPostRv)
                    {
                        this.IsValidateComfirm = true;
                        this.ValidateComfirmMsg = "ใบเสร็จรายการนี้ Post RV แล้ว \n ต้องการยกเลิกใบเสร็จหรือไม่";
                    }
                }
                else
                {
                    if (NotFindPayment)
                    {
                        ex.AddError(errMsg.Key, "ไม่สามารถแก้ไขรายการได้ เนื่องจากไม่พบข้อมูล", (int)errMsg.Type);
                    }
                    else if (ChkPaymentStateTranfer)
                    {
                        ex.AddError(errMsg.Key, "ไม่สามารถแก้ไขใบเสร็จได้ เนื่องจากเป็นใบเสร็จที่เกิดจากการรับชำระเงินหลังโอน", (int)errMsg.Type);
                    }
                    else if (ChkChangeUnit)
                    {
                        ex.AddError(errMsg.Key, "ไม่สามารถแก้ไขใบเสร็จได้ เนื่องจากเป็นใบเสร็จจากการย้ายแปลง", (int)errMsg.Type);
                    }
                    else if (ChkDeposit)
                    {
                        ex.AddError(errMsg.Key, "ใบเสร็จรายการนี้ได้นำฝากแล้ว \n ไม่สามารถแก้ไขได้", (int)errMsg.Type);
                    }
                    else if (ChkIsCancel)
                    {
                        ex.AddError(errMsg.Key, "รายการที่เลือกถูกยกเลิกแล้ว \n ไม่สามารถแก้ไขได้", (int)errMsg.Type);
                    }
                    else if (ChkPostRv)
                    {
                        this.IsValidateComfirm = true;
                        this.ValidateComfirmMsg = "ใบเสร็จรายการนี้ Post RV แล้ว \n ต้องการแก้ไขใบเสร็จหรือไม่";
                    }
                }
            }
            if (ex.HasError)
                throw ex;
        }

        public static async Task<PaymentFormDTO> CreateFromPaymentOfflineAsync(OfflinePaymentHeader model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new PaymentFormDTO();
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Booking?.Unit?.Project);
                result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Booking?.Unit);
                result.PaymentItems = new List<PaymentUnitPriceItemDTO>();
                result.Company = CompanyDropdownDTO.CreateFromModel(model.Booking?.Unit?.Project?.Company);
                //result.Remark = model.CancelRemark;
                result.CancelRemark = model.CancelRemark;
                result.ReceiveDate = model.ReceiveDate; 

                MasterCenter CreditCardPaymentType = db.MasterCenters.Where(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.CreditCardPaymentType) && x.Key.Equals(CreditCardPaymentTypeKeys.Full)).FirstOrDefault();
      
                var PaymentMethodsModel = await db.OfflinePaymentDetails
                    .Include(x => x.Bank)
                    .Include(x => x.BankAccount)
                    .Include(x => x.CreditCardType)
                    .Include(x => x.PaymentMethod)
                    .Include(x => x.Company)
                    .Where(x => x.OfflinePaymentHeaderID == model.ID).ToListAsync();
                result.PaymentMethods = PaymentMethodsModel.Select(x => PaymentMethodDTO.CreatePaymentOfflineFromModel(x, CreditCardPaymentType)).ToList();
                int order = 0;
                var MasterPriceItems = await db.MasterPriceItems.Where(e => e.ID == model.MasterPriceItemID).FirstOrDefaultAsync() ?? new MasterPriceItem();

                PaymentUnitPriceItemDTO newPaymentUnitPriceItemDTO = new PaymentUnitPriceItemDTO
                {
                    MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(MasterPriceItems),
                    PayAmount = model.PayAmount,
                    Order = order
                };

                newPaymentUnitPriceItemDTO.Name = MasterPriceItems.Detail;
                result.PaymentItems.Add(newPaymentUnitPriceItemDTO);
                result.PaymentItems = result.PaymentItems.OrderBy(e => e.Order).ThenBy(e => e.Period).ToList();

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public enum PaymentFormType
    {
        Normal = 0,
        UnknownPayment = 1,
        ChangeUnit = 2,
        DirectCredit = 3,
        DirectDebit = 4,
        BillPayment = 5,
        PreTransfer = 6,
    }

    public class UnitPaymentResult
    {
        public Guid BookingID { get; set; }
        public MasterPriceItem MasterPrice { get; set; }
        public int Period { get; set; }
        public decimal? PayAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
    }

    public class UnitPriceItemWithPromotion
    {
        public UnitPriceItem UnitPriceItem { get; set; }
        public MasterPriceItem MasterPriceItem { get; set; }
        public SalePromotionExpense SalePromotionExpense { get; set; }
        public MasterCenter UnitPriceStage { get; set; }
    }

    public class PaymentMethodItemText
    {
        public Guid? PaymentMethodID { get; set; }
        public string ItemText { get; set; }
    }
}
