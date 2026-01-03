using Base.DTOs.MST;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class AgreementPriceListDTO
    {
        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDTO Agreement { get; set; }
        /// <summary>
        /// ผู้แนะนำ
        /// </summary>
        public CTM.ContactListDTO ReferContact { get; set; }
        /// <summary>
        /// ราคาขาย
        /// </summary>
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// ส่วนลดเงินสด (ส่วนลดหน้าสัญญา)
        /// </summary>
        public decimal CashDiscount { get; set; }
        /// <summary>
        /// ส่วนลด ณ​ วันโอน
        /// </summary>
        public decimal TransferDiscount { get; set; }
        /// <summary>
        /// มี FreeDown หรือไม่
        /// </summary>
        public bool IsFreeDown { get; set; }
        /// <summary>
        /// ส่วนลด FreeDown
        /// </summary>
        public decimal FreeDownDiscount { get; set; }
        /// <summary>
        /// ส่วนลด FGF
        /// </summary>
        public decimal FGFDiscount { get; set; }
        /// <summary>
        /// ราคาขายสุทธิ (ราคาขายหักส่วนลด)
        /// </summary>
        public decimal NetSellingPrice { get; set; }
        /// <summary>
        /// เงินจอง
        /// </summary>
        public decimal BookingAmount { get; set; }
        /// <summary>
        /// เงินสัญญา
        /// </summary>
        public decimal ContractAmount { get; set; }
        /// <summary>
        /// เงินดาวน์
        /// </summary>
        public decimal DownAmount { get; set; }
        /// <summary>
        /// เงินโอนกรรมสิทธิ์
        /// </summary>
        public decimal TransferAmount { get; set; }
        /// <summary>
        /// จำนวนผ่อนดาวน์รวม
        /// </summary>
        public int Installment { get; set; }
        /// <summary>
        /// จำนวนงวดดาวน์ปกติ
        /// </summary>
        public int NormalInstallment { get; set; }
        /// <summary>
        /// เงินงวดดาวน์ปกติ
        /// </summary>
        public decimal InstallmentAmount { get; set; }
        /// <summary>
        /// จำนวนงวดดาวน์พิเศษ
        /// </summary>
        public int SpecialInstallment { get; set; }
        /// <summary>
        /// รวมมูลค่าโปรโมชั่น
        /// </summary>
        public decimal? TotalPromotionAmount { get; set; }
        /// <summary>
        /// รวมใช้ Budget Promotion
        /// </summary>
        public decimal? TotalBudgetPromotionAmount { get; set; }
        /// <summary>
        /// งวดดาวน์พิเศษ
        /// </summary>
        public List<SpecialInstallmentDTO> SpecialInstallmentPeriods { get; set; }
        /// <summary>
        /// วันที่เริ่มต้น
        /// </summary>
        public DateTime? InstallmentStartDate { get; set; }
        /// <summary>
        /// วันที่สิ้นสุด
        /// </summary>
        public DateTime? InstallmentEndDate { get; set; }
        /// <summary>
        /// วันที่กำหนดชำระเวินดาวน์
        /// </summary>
        public int DownDueDate { get; set; }
        /// <summary>
        /// รับเงินก่อนทำสัญญา
        /// </summary>
        public decimal? PreContractAmount { get; set; }
        /// <summary>
        /// รับเงินทำสัญญา
        /// </summary>
        public decimal? ContractAmountPaymented { get; set; }
        /// <summary>
        /// % เงินดาวน์
        /// </summary>
        public double? PercentDown { get; set; }
        /// <summary>
        /// วันที่งวดสุดท้าย
        /// </summary>
        public DateTime? SellerPayDueDate { get; set; }
        /// <summary>
        /// เงินงวดสุดท้าย
        /// </summary>
        public decimal? SellerPayAmount { get; set; }
        /// <summary>
        /// AP จ่ายงวดสุดท้าย
        /// </summary>
        public bool? IsSellerPay { get; set; }
        /// <summary>
        /// ตรวจสอบว่ามีการกดปุ่มคำนวนงวดเงินดาวน์ใหม่
        /// </summary>
        public bool? IsCalculateAgreementInstallment { get; set; }
        /// <summary>
        /// Due วันที่โอนกรรมสิทธิ์จาก Master Data
        /// </summary>
        public DateTime? DueTransferDate { get; set; }
        /// <summary>
        /// วันที่ปัจจุบัน
        /// </summary>
        public DateTime? DateNow { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProductType
        /// ประเภทของโครงการ  (แนวราบ, แนวสูง)
        /// </summary>
        public MST.MasterCenterDropdownDTO ProductType { get; set; }

        /// <summary>
        /// FGF Code
        /// </summary>
        public string FGFCode { get; set; }
        public decimal? AgreementPrice2 { get; set; }

        public async static Task<AgreementPriceListDTO> CreateFromModelAsync(Guid AgreementID, DatabaseContext db)
        {
            var model = await db.Agreements
                    .Include(o => o.Unit.Floor)
                    .Include(o => o.Project.ProductType)
                    .Where(o => o.ID == AgreementID).FirstOrDefaultAsync();

            if (model == null)
            {
                var result = new AgreementPriceListDTO();
                return result;
            }

            var unitPriceModel = await db.UnitPrices
                .Include(o => o.Booking)
                .ThenInclude(o => o.ReferContact)
                    .ThenInclude(o => o.ContactTitleTH)
                .Include(o => o.UnitPriceStage)
                .Where(o => o.BookingID == model.BookingID
                                && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement).FirstOrDefaultAsync();

            if (unitPriceModel != null)
            {
                var result = new AgreementPriceListDTO();
                result.AgreementPrice2 = model.AgreementPrice2;
                result.CashDiscount = unitPriceModel.CashDiscount.HasValue ? unitPriceModel.CashDiscount.Value : 0;
                result.TransferDiscount = unitPriceModel.TransferDiscount.HasValue ? unitPriceModel.TransferDiscount.Value : 0;
                result.SellingPrice = unitPriceModel.SellingPrice.HasValue ? unitPriceModel.SellingPrice.Value : 0;
                //result.NetSellingPrice = unitPriceModel.AgreementPrice.HasValue ? unitPriceModel.AgreementPrice.Value : 0;
                result.NetSellingPrice = result.SellingPrice - result.CashDiscount;
                result.BookingAmount = unitPriceModel.BookingAmount.HasValue ? unitPriceModel.BookingAmount.Value : 0;
                result.ContractAmount = unitPriceModel.AgreementAmount.HasValue ? unitPriceModel.AgreementAmount.Value : 0;
                result.FreeDownDiscount = unitPriceModel.FreedownDiscount.HasValue ? unitPriceModel.FreedownDiscount.Value : 0;
                result.FGFDiscount = unitPriceModel.FGFDiscount.HasValue ? unitPriceModel.FGFDiscount.Value : 0;
                result.Installment = unitPriceModel.Installment.HasValue ? unitPriceModel.Installment.Value : 0;
                result.ReferContact = CTM.ContactListDTO.CreateFromModel(unitPriceModel.Booking.ReferContact, db);
                result.ContractAmount = unitPriceModel.AgreementAmount ?? 0;

                var installments = await db.UnitPriceInstallments.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();
                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
                if (installments.Count > 0)
                {
                    var normalInstallment = installments.Where(o => o.IsSpecialInstallment == false).ToList();
                    result.NormalInstallment = normalInstallment.Count;
                    result.InstallmentAmount = unitPriceModel.InstallmentAmount.HasValue ? unitPriceModel.InstallmentAmount.Value : 0;

                    foreach (var item in installments.Where(o => o.IsSpecialInstallment == true).ToList())
                    {
                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = item.Period,
                            Amount = item.Amount
                        });
                    }

                    result.SpecialInstallment = installments.Where(o => o.IsSpecialInstallment == true).Count();

                    if (result.SpecialInstallmentPeriods.Count > 0)
                    {
                        List<SpecialInstallmentDTO> specialTemp = result.SpecialInstallmentPeriods;
                        result.SpecialInstallmentPeriods = specialTemp.OrderBy(o => o.Period).ToList();
                    }

                    result.DownDueDate = unitPriceModel.InstallmentPayDate.HasValue ? unitPriceModel.InstallmentPayDate.Value : 1;
                    if (unitPriceModel.InstallmentStartDate == null)
                    {
                        var startDownDate = model.ContractDate.HasValue ? model.ContractDate.Value.AddMonths(1) : (DateTime?)null;
                        if (startDownDate.HasValue)
                        {
                            result.InstallmentStartDate = new DateTime(startDownDate.Value.Year, startDownDate.Value.Month, result.DownDueDate);
                        }
                        else
                        {
                            result.InstallmentStartDate = null;
                        }
                    }
                    else
                    {
                        result.InstallmentStartDate = unitPriceModel.InstallmentStartDate;

                    }
                    if (unitPriceModel.InstallmentEndDate == null)
                    {
                        var endDownDate = result.InstallmentStartDate.HasValue ? result.InstallmentStartDate.Value.AddMonths(installments.Count > 0 ? installments.Count - 1 : 0) : (DateTime?)null;

                        if (endDownDate.HasValue)
                        {
                            result.InstallmentEndDate = new DateTime(endDownDate.Value.Year, endDownDate.Value.Month, result.DownDueDate);
                        }
                        else
                        {
                            result.InstallmentEndDate = null;
                        }
                    }
                    else
                    {
                        result.InstallmentEndDate = unitPriceModel.InstallmentEndDate;
                    }
                    if (unitPriceModel.InstallmentPayDate != null)
                    {
                        result.DownDueDate = unitPriceModel.InstallmentPayDate ?? 0;
                    }
                }
                else
                {
                    result.DownDueDate = 1;
                    result.InstallmentStartDate = null;
                    result.InstallmentEndDate = null;
                }

                result.Agreement = await AgreementDTO.CreateFromModelAsync(model, null, db);

                result.SellerPayDueDate = installments.Where(o => o.IsSellerPay == true).Select(o => o.DueDate).FirstOrDefault();
                result.SellerPayAmount = installments.Where(o => o.IsSellerPay == true).Select(o => o.Amount).FirstOrDefault();
                result.IsSellerPay = installments.Where(o => o.IsSellerPay == true).Select(o => o.IsSellerPay).FirstOrDefault();
                result.DownAmount = installments.Sum(o => o.Amount);
                if (result.DownAmount != 0)
                {
                    result.PercentDown = Convert.ToDouble((result.DownAmount / (result.NetSellingPrice - result.BookingAmount - result.ContractAmount))) * 100;
                }
                else
                {
                    result.PercentDown = 0;
                }
                //result.TransferAmount = result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;

                var paymentIDs = await db.Payments
                            .Include(o => o.PaymentState)
                            .Where(o => o.BookingID == model.BookingID
                                && (o.PaymentState.Key == PaymentStateKeys.Agreement))
                            .OrderBy(o => o.Created).Select(o => o.ID).ToListAsync();

                if (unitPriceModel.TransferAmount != null)
                {
                    //result.TransferAmount = (decimal)unitPriceModel.TransferAmount;
                    result.TransferAmount = (decimal)(result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount);
                }
                else
                {
                    var listAPAmounMasterPriceItemKeys = new List<string>();
                    listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.BookingAmount);
                    listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.ContractAmount);
                    listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.InstallmentAmount);
                    listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.TransferAmount);

                    var TransferAmount = await db.PaymentItems
                      .Include(o => o.MasterPriceItem)
                      .Where(o => paymentIDs.Contains(o.PaymentID) && listAPAmounMasterPriceItemKeys.Contains(o.MasterPriceItem.Key)).SumAsync(o => o.PayAmount);

                    result.TransferAmount = result.NetSellingPrice - TransferAmount;
                }

                var query = await db.BudgetPromotions
                .Include(o => o.UpdatedBy)
                .Where(o => o.ProjectID == unitPriceModel.Booking.ProjectID && o.UnitID == unitPriceModel.Booking.UnitID)
                .Select(o => new
                {
                    BudgetPromotion = o,
                    Unit = o.Unit
                }).ToListAsync();


                var temp = query.GroupBy(o => o.Unit).Select(o => new PRJ.TempBudgetPromotionQueryResult
                {
                    Unit = o.Key,
                    BudgetPromotions = o.Select(p => p.BudgetPromotion).ToList()
                }).ToList();
                var masterCenterBudgetPromotionTypeSaleID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
                var masterCenterBudgetPromotionTypeTransferID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();

                var bugetPromotion = temp.Select(o => new PRJ.BudgetPromotionQueryResult
                {
                    Unit = o.Unit,
                    BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                    BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
                }).FirstOrDefault();

                #region Old Code Set TotalAmount & BudgetAmount

                //var promotion = await db.SalePromotions.Where(o => o.BookingID == agree.BookingID).FirstAsync();
                //var totalPreSalePrice = await db.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == agree.BookingID).SumAsync(o => o.TotalPrice);
                //var totalPromotionPrice = await db.SalePromotionItems.Where(o => o.SalePromotionID == promotion.ID).SumAsync(o => o.TotalPrice);
                //var totalExpenseByAP = await db.SalePromotionExpenses
                //    .Include(o => o.ExpenseReponsibleBy)
                //    .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).SumAsync(o => o.SellerAmount);
                //var totalExpenseByHalf = await db.SalePromotionExpenses
                //    .Include(o => o.ExpenseReponsibleBy)
                //    .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).SumAsync(o => o.SellerAmount);

                //result.TotalPromotionAmount = result.CashDiscount + result.TransferDiscount + (bugetPromotion == null ? 0 : bugetPromotion.BudgetPromotionSale.Budget);
                //result.TotalBudgetPromotionAmount = result.FGFDiscount + totalPreSalePrice + totalPromotionPrice + totalExpenseByAP + totalExpenseByHalf;

                #endregion

                var promotion = await db.SalePromotions.Where(o => o.BookingID == model.BookingID).FirstOrDefaultAsync();
                //if (promotion != null)
                //{
                //    result.TotalPromotionAmount = promotion.TotalAmount;
                //    //result.TotalPromotionAmount = result.CashDiscount + result.TransferDiscount + bugetPromotion.BudgetPromotionSale?.Budget;
                //    result.TotalBudgetPromotionAmount = promotion.BudgetAmount;
                //}
                if (promotion != null)
                {
                    var totalPreSalePrice = await db.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == model.BookingID).SumAsync(o => o.Quantity * o.PricePerUnit);
                    var totalPromotionPrice = await db.SalePromotionItems.Where(o => o.SalePromotionID == promotion.ID).SumAsync(o => o.Quantity * o.PricePerUnit);
                    var totalPromotionPriceCreditCard = await db.SalePromotionCreditCardItems.Where(o => o.SalePromotionID == promotion.ID).SumAsync(o => o.Quantity * o.Fee);
                    var totalExpenseByAP = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).SumAsync(o => o.SellerAmount);
                    var totalExpenseByHalf = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).SumAsync(o => o.SellerAmount);
                    result.TotalBudgetPromotionAmount = result.FGFDiscount + totalPreSalePrice + totalPromotionPrice + (decimal)totalPromotionPriceCreditCard + totalExpenseByAP + totalExpenseByHalf + result.FreeDownDiscount;

                }
                else
                {
                    result.TotalBudgetPromotionAmount = 0;
                }
                result.TotalPromotionAmount = result.CashDiscount + result.TransferDiscount + result.TotalBudgetPromotionAmount;

                //การรับเงินก่อนทำสัญญา
                //var advanceContractPayment = await db.PaymentItems
                //  .Include(o => o.MasterPriceItem)
                //  .Include(o => o.Payment)
                //  .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.AdvanceContractPayment
                //                        && o.Payment.BookingID == agree.BookingID
                //                        && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

                var advanceContractPayment = await db.PaymentItems
                  .Include(o => o.MasterPriceItem)
                  .Include(o => o.Payment)
                     .ThenInclude(o => o.PaymentState)
                  .Where(o => o.Payment.PaymentState.Key == PaymentStateKeys.Booking
                    && o.MasterPriceItem.Key != MasterPriceItemKeys.BookingAmount
                                        && o.Payment.BookingID == model.BookingID
                                        && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

                result.PreContractAmount = advanceContractPayment;

                var ContractPayment = await db.PaymentItems
                    .Include(o => o.MasterPriceItem)
                    .Include(o => o.Payment)
                    .ThenInclude(o => o.PaymentState)
                    .Where(o =>
                        o.Payment.PaymentState.Key == PaymentStateKeys.Agreement
                        && o.MasterPriceItem.Key == MasterPriceItemKeys.ContractAmount
                        && o.Payment.BookingID == model.BookingID
                        && o.Payment.IsCancel == false
                        && o.Payment.IsDeleted == false
                        && o.IsDeleted == false)
                    .SumAsync(o => o.PayAmount);

                result.ContractAmountPaymented = ContractPayment;


                // เซ็ต Due วันที่โอนกรรมสิทธิ์จาก Master
                result.DueTransferDate = model.Unit?.Floor?.DueTransferDate;
                result.DateNow = DateTime.Now.Date;
                result.ProductType = MasterCenterDropdownDTO.CreateFromModel(model.Project.ProductType);

                //New FGF Code 
                result.FGFCode = unitPriceModel.Booking?.FGFCode;

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<AgreementPriceListDTO> CreateFromModelPriceListAsync(Guid AgreementID, DatabaseContext db)
        {
            var agree = await db.Agreements
                .Include(o => o.Project.ProductType)
                .Include(o => o.Booking)
                    .ThenInclude(o => o.ReferContact)
                    .ThenInclude(o => o.ContactTitleTH)
               .Where(o => o.ID == AgreementID).FirstOrDefaultAsync();

            var priceList = await db.PriceLists
                .Where(o => o.UnitID == agree.UnitID).OrderByDescending(o => o.ActiveDate)
                .FirstOrDefaultAsync();

            if (priceList != null)
            {
                var priceListItemModel = await db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToListAsync();
                var result = new AgreementPriceListDTO();

                #region For Net Price

                var unitPriceModel = await db.UnitPrices
                    .Include(o => o.Booking)
                    .ThenInclude(o => o.ReferContact)
                        .ThenInclude(o => o.ContactTitleTH)
                    .Include(o => o.UnitPriceStage)
                    .Where(o => o.BookingID == agree.BookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement && o.IsActive == true).FirstOrDefaultAsync();
                var cashDiscount = (decimal)0;
                var fGFDiscount = (decimal)0;
                var freeDownDiscount = (decimal)0;
                var transferDiscount = (decimal)0;

                if (unitPriceModel != null)
                {
                    cashDiscount = unitPriceModel.CashDiscount.HasValue ? unitPriceModel.CashDiscount.Value : 0;
                }
                if (unitPriceModel != null)
                {
                    fGFDiscount = unitPriceModel.FGFDiscount.HasValue ? unitPriceModel.FGFDiscount.Value : 0;
                }
                if (unitPriceModel != null)
                {
                    freeDownDiscount = unitPriceModel.FreedownDiscount.HasValue ? unitPriceModel.FreedownDiscount.Value : 0;
                }
                if (unitPriceModel != null)
                {
                    transferDiscount = unitPriceModel.TransferDiscount.HasValue ? unitPriceModel.TransferDiscount.Value : 0;
                }
                #endregion
                result.CashDiscount = cashDiscount;
                result.TransferDiscount = transferDiscount;
                result.FGFDiscount = fGFDiscount;
                result.SellingPrice = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice).Amount;
                //result.NetSellingPrice = priceListItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
                result.NetSellingPrice = result.SellingPrice - cashDiscount;
                result.BookingAmount = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.BookingAmount).Amount;
                result.ContractAmount = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.ContractAmount).Amount;
                result.DownAmount = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount).Amount;
                result.ReferContact = CTM.ContactListDTO.CreateFromModel(agree.Booking.ReferContact, db);
                result.FreeDownDiscount = freeDownDiscount;
                var downAmountItem = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount);
                result.Installment = downAmountItem.Installment.HasValue ? downAmountItem.Installment.Value : 0;
                var installments = new List<UnitPriceInstallment>();
                if (unitPriceModel != null)
                {
                    installments = await db.UnitPriceInstallments.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();

                }
                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();

                if (installments.Count > 0)
                {
                    var normalInstallment = installments.Where(o => o.IsSpecialInstallment == false).ToList();
                    result.NormalInstallment = normalInstallment.Count;
                    result.InstallmentAmount = unitPriceModel.InstallmentAmount.HasValue ? unitPriceModel.InstallmentAmount.Value : 0;

                    foreach (var item in installments.Where(o => o.IsSpecialInstallment == true).ToList())
                    {
                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = item.Period,
                            Amount = item.Amount
                        });
                    }

                    result.SpecialInstallment = installments.Where(o => o.IsSpecialInstallment == true).Count();

                    if (result.SpecialInstallmentPeriods.Count > 0)
                    {
                        List<SpecialInstallmentDTO> specialTemp = result.SpecialInstallmentPeriods;
                        result.SpecialInstallmentPeriods = specialTemp.OrderBy(o => o.Period).ToList();
                    }

                    result.DownDueDate = 1; //default วันที่ 1  

                    var startDownDate = agree.ContractDate.HasValue ? agree.ContractDate.Value.AddMonths(1) : (DateTime?)null;
                    result.InstallmentStartDate = new DateTime(startDownDate.Value.Year, startDownDate.Value.Month, result.DownDueDate);
                    var endDownDate = result.InstallmentStartDate.HasValue ? result.InstallmentStartDate.Value.AddMonths(installments.Count > 0 ? installments.Count - 1 : 0) : (DateTime?)null;

                    if (endDownDate.HasValue)
                    {
                        result.InstallmentEndDate = new DateTime(endDownDate.Value.Year, endDownDate.Value.Month, result.DownDueDate);
                    }
                    else
                    {
                        result.InstallmentEndDate = null;
                    }
                }
                else
                {
                    result.DownDueDate = 1;
                    result.InstallmentStartDate = null;
                    result.InstallmentEndDate = null;
                }

                result.Agreement = await AgreementDTO.CreateFromModelAsync(agree, null, db);

                result.PercentDown = Convert.ToDouble(result.DownAmount / result.NetSellingPrice) * 100;
                result.TransferAmount = result.SellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;

                //การรับเงินก่อนทำสัญญา
                var advanceContractPayment = await db.PaymentItems
                  .Include(o => o.MasterPriceItem)
                  .Include(o => o.Payment)
                  .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.AdvanceContractPayment
                                        && o.Payment.BookingID == agree.BookingID
                                        && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

                result.PreContractAmount = advanceContractPayment;
                var promotion = await db.SalePromotions.Where(o => o.BookingID == agree.BookingID).FirstOrDefaultAsync();

                if (promotion != null)
                {
                    var totalPreSalePrice = await db.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == agree.BookingID).SumAsync(o => o.Quantity * o.PricePerUnit);
                    var totalPromotionPrice = await db.SalePromotionItems.Where(o => o.SalePromotionID == promotion.ID).SumAsync(o => o.Quantity * o.PricePerUnit);
                    var totalPromotionPriceCreditCard = await db.SalePromotionCreditCardItems.Where(o => o.SalePromotionID == promotion.ID).SumAsync(o => o.Quantity * o.Fee);
                    var totalExpenseByAP = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).SumAsync(o => o.SellerAmount);
                    var totalExpenseByHalf = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).SumAsync(o => o.SellerAmount);
                    result.TotalBudgetPromotionAmount = result.FGFDiscount + totalPreSalePrice + totalPromotionPrice + (decimal)totalPromotionPriceCreditCard + totalExpenseByAP + totalExpenseByHalf + result.FreeDownDiscount;

                }
                else

                {
                    result.TotalBudgetPromotionAmount = 0;
                }
                result.TotalPromotionAmount = result.CashDiscount + result.TransferDiscount + result.TotalBudgetPromotionAmount;
                result.DateNow = DateTime.Now.Date;
                result.ProductType = MasterCenterDropdownDTO.CreateFromModel(agree.Project.ProductType);
                result.FGFCode = agree.Booking.FGFCode;
                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<AgreementPriceListDTO> CreateFromModelPriceListForChangeproAsync(Guid AgreementID, DatabaseContext db)
        {
            var agree = await db.Agreements
                .Include(o => o.Project.ProductType)
                .Include(o => o.Booking)
                    .ThenInclude(o => o.ReferContact)
                    .ThenInclude(o => o.ContactTitleTH)
               .Where(o => o.ID == AgreementID).FirstOrDefaultAsync();

            var priceList = await db.PriceLists
                .Where(o => o.UnitID == agree.UnitID).OrderByDescending(o => o.ActiveDate)
                .FirstOrDefaultAsync();

            if (priceList != null)
            {
                var priceListItemModel = await db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToListAsync();
                var result = new AgreementPriceListDTO();

                #region For Net Price

                var unitPriceModel = await db.UnitPrices
                    .Include(o => o.Booking)
                    .ThenInclude(o => o.ReferContact)
                        .ThenInclude(o => o.ContactTitleTH)
                    .Include(o => o.UnitPriceStage)
                    .Where(o => o.BookingID == agree.BookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement).FirstOrDefaultAsync();
                var cashDiscount = (decimal)0;
                var fGFDiscount = (decimal)0;
                var freeDownDiscount = (decimal)0;
                var transferDiscount = (decimal)0;

                if (unitPriceModel != null)
                {
                    cashDiscount = unitPriceModel.CashDiscount.HasValue ? unitPriceModel.CashDiscount.Value : 0;
                }
                if (unitPriceModel != null)
                {
                    fGFDiscount = unitPriceModel.FGFDiscount.HasValue ? unitPriceModel.FGFDiscount.Value : 0;
                }
                if (unitPriceModel != null)
                {
                    freeDownDiscount = unitPriceModel.FreedownDiscount.HasValue ? unitPriceModel.FreedownDiscount.Value : 0;
                }
                if (unitPriceModel != null)
                {
                    transferDiscount = unitPriceModel.TransferDiscount.HasValue ? unitPriceModel.TransferDiscount.Value : 0;
                }
                #endregion
                result.CashDiscount = cashDiscount;
                result.TransferDiscount = transferDiscount;
                result.FGFDiscount = fGFDiscount;
                result.SellingPrice = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice).Amount;
                //result.NetSellingPrice = priceListItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
                result.NetSellingPrice = result.SellingPrice - cashDiscount;
                result.BookingAmount = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.BookingAmount).Amount;
                result.ContractAmount = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.ContractAmount).Amount;
                result.DownAmount = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount).Amount;
                result.ReferContact = CTM.ContactListDTO.CreateFromModel(agree.Booking.ReferContact, db);
                result.FreeDownDiscount = freeDownDiscount;
                var downAmountItem = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount);
                result.Installment = downAmountItem.Installment.HasValue ? downAmountItem.Installment.Value : 0;
                var installments = new List<UnitPriceInstallment>();
                if (unitPriceModel != null)
                {
                    installments = await db.UnitPriceInstallments.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();

                }
                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();

                if (installments.Count > 0)
                {
                    var normalInstallment = installments.Where(o => o.IsSpecialInstallment == false).ToList();
                    result.NormalInstallment = normalInstallment.Count;
                    result.InstallmentAmount = unitPriceModel.InstallmentAmount.HasValue ? unitPriceModel.InstallmentAmount.Value : 0;

                    foreach (var item in installments.Where(o => o.IsSpecialInstallment == true).ToList())
                    {
                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = item.Period,
                            Amount = item.Amount
                        });
                    }

                    result.SpecialInstallment = installments.Where(o => o.IsSpecialInstallment == true).Count();

                    if (result.SpecialInstallmentPeriods.Count > 0)
                    {
                        List<SpecialInstallmentDTO> specialTemp = result.SpecialInstallmentPeriods;
                        result.SpecialInstallmentPeriods = specialTemp.OrderBy(o => o.Period).ToList();
                    }

                    result.DownDueDate = 1; //default วันที่ 1  

                    var startDownDate = agree.ContractDate.HasValue ? agree.ContractDate.Value.AddMonths(1) : (DateTime?)null;
                    result.InstallmentStartDate = new DateTime(startDownDate.Value.Year, startDownDate.Value.Month, result.DownDueDate);
                    var endDownDate = result.InstallmentStartDate.HasValue ? result.InstallmentStartDate.Value.AddMonths(installments.Count > 0 ? installments.Count - 1 : 0) : (DateTime?)null;

                    if (endDownDate.HasValue)
                    {
                        result.InstallmentEndDate = new DateTime(endDownDate.Value.Year, endDownDate.Value.Month, result.DownDueDate);
                    }
                    else
                    {
                        result.InstallmentEndDate = null;
                    }
                }
                else
                {
                    result.DownDueDate = 1;
                    result.InstallmentStartDate = null;
                    result.InstallmentEndDate = null;
                }

                result.Agreement = await AgreementDTO.CreateFromModelAsync(agree, null, db);

                result.PercentDown = Convert.ToDouble(result.DownAmount / result.NetSellingPrice) * 100;
                result.TransferAmount = result.SellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;

                //การรับเงินก่อนทำสัญญา
                var advanceContractPayment = await db.PaymentItems
                  .Include(o => o.MasterPriceItem)
                  .Include(o => o.Payment)
                  .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.AdvanceContractPayment
                                        && o.Payment.BookingID == agree.BookingID
                                        && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

                result.PreContractAmount = advanceContractPayment;
                var promotion = await db.SalePromotions.Where(o => o.BookingID == agree.BookingID).FirstOrDefaultAsync();

                if (promotion != null)
                {
                    var totalPreSalePrice = await db.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == agree.BookingID).SumAsync(o => o.Quantity * o.PricePerUnit);
                    var totalPromotionPrice = await db.SalePromotionItems.Where(o => o.SalePromotionID == promotion.ID).SumAsync(o => o.Quantity * o.PricePerUnit);
                    var totalPromotionPriceCreditCard = await db.SalePromotionCreditCardItems.Where(o => o.SalePromotionID == promotion.ID).SumAsync(o => o.Quantity * o.Fee);
                    var totalExpenseByAP = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).SumAsync(o => o.SellerAmount);
                    var totalExpenseByHalf = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).SumAsync(o => o.SellerAmount);
                    result.TotalBudgetPromotionAmount = result.FGFDiscount + totalPreSalePrice + totalPromotionPrice + (decimal)totalPromotionPriceCreditCard + totalExpenseByAP + totalExpenseByHalf + result.FreeDownDiscount;

                }
                else
                {
                    result.TotalBudgetPromotionAmount = 0;
                }
                result.TotalPromotionAmount = result.CashDiscount + result.TransferDiscount + result.TotalBudgetPromotionAmount;
                result.DateNow = DateTime.Now.Date;
                result.ProductType = MasterCenterDropdownDTO.CreateFromModel(agree.Project.ProductType);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<AgreementPriceListDTO> CreateFromModelPriceListForDaftAsync(Guid AgreementID, DatabaseContext db)
        {
            var agree = await db.Agreements
                .Include(o => o.Project.ProductType)
                .Include(o => o.Booking)
                    .ThenInclude(o => o.ReferContact)
                    .ThenInclude(o => o.ContactTitleTH)
               .Where(o => o.ID == AgreementID).FirstOrDefaultAsync();

            var priceList = await db.PriceLists
                .Where(o => o.UnitID == agree.UnitID).OrderByDescending(o => o.ActiveDate)
                .FirstOrDefaultAsync();

            if (priceList != null)
            {
                var priceListItemModel = await db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToListAsync();
                var result = new AgreementPriceListDTO();
                #region For Net Price

                var unitPriceModel = await db.UnitPrices
                    .Include(o => o.Booking)
                    .ThenInclude(o => o.ReferContact)
                        .ThenInclude(o => o.ContactTitleTH)
                    .Include(o => o.UnitPriceStage)
                    .Where(o => o.BookingID == agree.BookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement && o.IsActive == false).OrderByDescending(o => o.Created).FirstOrDefaultAsync();

                var cashDiscount = (decimal)0;
                if (unitPriceModel != null)
                {
                    cashDiscount = unitPriceModel.CashDiscount.HasValue ? unitPriceModel.CashDiscount.Value : 0;
                }

                #endregion
                result.CashDiscount = cashDiscount;
                result.TransferDiscount = unitPriceModel.TransferDiscount.HasValue ? unitPriceModel.TransferDiscount.Value : 0;
                result.FGFDiscount = unitPriceModel.FGFDiscount.HasValue ? unitPriceModel.FGFDiscount.Value : 0;
                result.SellingPrice = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice)?.Amount ?? 0;
                //result.NetSellingPrice = priceListItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
                result.NetSellingPrice = result.SellingPrice - cashDiscount;
                result.BookingAmount = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.BookingAmount)?.Amount ?? 0;
                result.ContractAmount = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.ContractAmount)?.Amount ?? 0;
                result.DownAmount = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.Amount ?? 0;
                result.ReferContact = CTM.ContactListDTO.CreateFromModel(agree.Booking.ReferContact, db);
                result.FreeDownDiscount = unitPriceModel.FreedownDiscount.HasValue ? unitPriceModel.FreedownDiscount.Value : 0;
                var downAmountItem = priceListItemModel.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount);
                result.Installment = downAmountItem.Installment.HasValue ? downAmountItem.Installment.Value : 0;
                var installments = await db.UnitPriceInstallments.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();
                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();

                if (installments.Count > 0)
                {
                    var normalInstallment = installments.Where(o => o.IsSpecialInstallment == false).ToList();
                    result.NormalInstallment = normalInstallment.Count;
                    result.InstallmentAmount = unitPriceModel.InstallmentAmount.HasValue ? unitPriceModel.InstallmentAmount.Value : 0;

                    foreach (var item in installments.Where(o => o.IsSpecialInstallment == true).ToList())
                    {
                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = item.Period,
                            Amount = item.Amount
                        });
                    }

                    result.SpecialInstallment = installments.Where(o => o.IsSpecialInstallment == true).Count();

                    if (result.SpecialInstallmentPeriods.Count > 0)
                    {
                        List<SpecialInstallmentDTO> specialTemp = result.SpecialInstallmentPeriods;
                        result.SpecialInstallmentPeriods = specialTemp.OrderBy(o => o.Period).ToList();
                    }
                }

                result.Agreement = await AgreementDTO.CreateFromModelAsync(agree, null, db);

                result.DownDueDate = 1; //default วันที่ 1  

                var startDownDate = agree.ContractDate.HasValue ? agree.ContractDate.Value.AddMonths(1) : (DateTime?)null;
                result.InstallmentStartDate = new DateTime(startDownDate.Value.Year, startDownDate.Value.Month, result.DownDueDate);
                var endDownDate = result.InstallmentStartDate.HasValue ? result.InstallmentStartDate.Value.AddMonths(installments.Count > 0 ? installments.Count - 1 : 0) : (DateTime?)null;

                if (endDownDate.HasValue)
                {
                    result.InstallmentEndDate = new DateTime(endDownDate.Value.Year, endDownDate.Value.Month, result.DownDueDate);
                }
                else
                {
                    result.InstallmentEndDate = null;
                }
                //var endDownDate = result.InstallmentStartDate.HasValue ? result.InstallmentStartDate.Value.AddMonths(installments) : (DateTime?)null;
                //result.InstallmentEndDate = new DateTime(endDownDate.Value.Year, endDownDate.Value.Month, result.DownDueDate);

                //result.PercentDown = downAmountItem.PriceUnitAmount * 100;
                result.PercentDown = Convert.ToDouble(result.DownAmount / result.NetSellingPrice) * 100;
                result.TransferAmount = result.SellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;

                //การรับเงินก่อนทำสัญญา
                var advanceContractPayment = await db.PaymentItems
                  .Include(o => o.MasterPriceItem)
                  .Include(o => o.Payment)
                  .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.AdvanceContractPayment
                                        && o.Payment.BookingID == agree.BookingID
                                        && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

                result.PreContractAmount = advanceContractPayment;
                var promotion = await db.SalePromotions.Where(o => o.BookingID == agree.BookingID).FirstOrDefaultAsync();

                if (promotion != null)
                {
                    var totalPreSalePrice = await db.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == agree.BookingID).SumAsync(o => o.Quantity * o.PricePerUnit);
                    var totalPromotionPrice = await db.SalePromotionItems.Where(o => o.SalePromotionID == promotion.ID).SumAsync(o => o.Quantity * o.PricePerUnit);
                    var totalExpenseByAP = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).SumAsync(o => o.SellerAmount);
                    var totalExpenseByHalf = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).SumAsync(o => o.SellerAmount);
                    result.TotalBudgetPromotionAmount = result.FGFDiscount + totalPreSalePrice + totalPromotionPrice + totalExpenseByAP + totalExpenseByHalf + result.FreeDownDiscount;

                }
                else
                {
                    result.TotalBudgetPromotionAmount = 0;
                }
                result.TotalPromotionAmount = result.CashDiscount + result.TransferDiscount + result.TotalBudgetPromotionAmount;
                result.DateNow = DateTime.Now.Date;
                result.ProductType = MasterCenterDropdownDTO.CreateFromModel(agree.Project.ProductType);

                result.FGFCode = agree?.Booking?.FGFCode;
                return result;
            }
            else
            {
                return null;
            }
        }

        //public async static Task<AgreementPriceListDTO> CreateFromModelAsyncs(Guid AgreementID, DatabaseContext db)
        //{
        //    var agree = await db.Agreements
        //       .Where(o => o.ID == AgreementID).FirstOrDefaultAsync();

        //    if (agree == null)
        //    {
        //        return null;
        //    }
        //    var unitPriceModel = await db.UnitPrices
        //    .Include(o => o.Booking)
        //    .ThenInclude(o => o.ReferContact)
        //    .Include(o => o.UnitPriceStage)
        //    .Where(o => o.BookingID == agree.BookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement).FirstOrDefaultAsync();
        //    var result = new AgreementPriceListDTO();
        //    if (unitPriceModel != null)
        //    {


        //                result.InstallmentStartDate = unitPriceModel.InstallmentStartDate;
        //                result.InstallmentEndDate = unitPriceModel.InstallmentEndDate;
        //                 result.DownDueDate = unitPriceModel.InstallmentPayDate ?? 0;
        //    }



        //        return result;


        //}
    }
}
