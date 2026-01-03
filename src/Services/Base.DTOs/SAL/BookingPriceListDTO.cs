using Database.Models;
using Database.Models.MasterKeys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class BookingPriceListDTO
    {
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
        /// % งวดดาวน์
        /// </summary>
        public double? PercentDown { get; set; }

        /// <summary>
        /// FGF Code
        /// </summary>
        public string FGFCode { get; set; }

        public async static Task<BookingPriceListDTO> CreateFromModelAsync(Guid bookingID, DatabaseContext db)
        {
            var unitPriceModel = await db.UnitPrices
                .Include(o => o.Booking)
                .ThenInclude(o => o.ReferContact)
                .ThenInclude(o=>o.ContactTitleTH)
                .Include(o => o.Booking)
                .ThenInclude(o => o.ReferContact)
                .ThenInclude(o => o.ContactTitleEN)
                .Include(o => o.UnitPriceStage)
                .Where(o => o.BookingID == bookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Booking ).FirstOrDefaultAsync();

            if (unitPriceModel != null)
            { 
                var result = new BookingPriceListDTO();

                result.CashDiscount = unitPriceModel.CashDiscount.HasValue ? unitPriceModel.CashDiscount.Value : 0;
                result.TransferDiscount = unitPriceModel.TransferDiscount.HasValue ? unitPriceModel.TransferDiscount.Value : 0;
                result.SellingPrice = unitPriceModel.SellingPrice.HasValue ? unitPriceModel.SellingPrice.Value : 0;
                result.NetSellingPrice = unitPriceModel.AgreementPrice.HasValue ? unitPriceModel.AgreementPrice.Value : 0;
                result.BookingAmount = unitPriceModel.BookingAmount.HasValue ? unitPriceModel.BookingAmount.Value : 0;
                result.ContractAmount = unitPriceModel.AgreementAmount.HasValue ? unitPriceModel.AgreementAmount.Value : 0;
                result.FreeDownDiscount = unitPriceModel.FreedownDiscount.HasValue ? unitPriceModel.FreedownDiscount.Value : 0;
                result.FGFDiscount = unitPriceModel.FGFDiscount.HasValue ? unitPriceModel.FGFDiscount.Value : 0;
                result.DownAmount = unitPriceModel.InstallmentAmount.HasValue ? unitPriceModel.InstallmentAmount.Value : 0;
                result.PercentDown = unitPriceModel.InstallmentPercent.HasValue ? unitPriceModel.InstallmentPercent.Value : 0;
                //result.TransferAmount = result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;

                var paymentIDs = await db.Payments
                            .Include(o => o.PaymentState)
                            .Where(o =>
                                o.BookingID == bookingID
                                && (
                                    o.PaymentState.Key == PaymentStateKeys.Booking
                                )
                            )
                            .OrderBy(o => o.Created).Select(o => o.ID).ToListAsync();

                var listAPAmounMasterPriceItemKeys = new List<string>();
                listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.BookingAmount);
                listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.ContractAmount);
                listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.InstallmentAmount);
                listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.TransferAmount);

                var TransferAmount = await db.PaymentItems
                    .Include(o => o.MasterPriceItem)
                    .Where(o => paymentIDs.Contains(o.PaymentID) && listAPAmounMasterPriceItemKeys.Contains(o.MasterPriceItem.Key)).SumAsync(o => o.PayAmount);

                result.TransferAmount = result.NetSellingPrice - TransferAmount;

                result.Installment = unitPriceModel.Installment.HasValue ? unitPriceModel.Installment.Value : 0;
                result.SpecialInstallment = unitPriceModel.SpecialInstallment.HasValue ? unitPriceModel.SpecialInstallment.Value : 0;
                result.ReferContact = CTM.ContactListDTO.CreateFromModel(unitPriceModel.Booking.ReferContact, db);

                var installments = await db.UnitPriceInstallments.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();
                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
                if (installments.Count > 0)
                {
                    var normalInstallment = installments.Where(o => o.IsSpecialInstallment == false).ToList();
                    result.NormalInstallment = normalInstallment.Count;
                    if (normalInstallment.Count == 0)
                    {
                        result.InstallmentAmount = 0;

                    }
                    else
                    { 
                     result.InstallmentAmount = normalInstallment[0].Amount;
                    }


                    foreach (var item in installments.Where(o => o.IsSpecialInstallment == true).OrderBy(o => o.Period).ToList())
                    {
                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = item.Period,
                            Amount = item.Amount
                        });
                    }
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



                var promotion = await db.SalePromotions.Where(o => o.BookingID == bookingID).FirstOrDefaultAsync();

                if (promotion != null)
                {
                    var totalPreSalePrice = await db.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == bookingID).SumAsync(o => o.Quantity * o.PricePerUnit);
                    var totalPromotionPrice = await db.SalePromotionItems.Where(o => o.SalePromotionID == promotion.ID && o.MainSalePromotionItemID==null).SumAsync(o => o.TotalPrice);
                    var totalPromotionCreditCardPrice = await db.SalePromotionCreditCardItems.Where(o => o.SalePromotionID == promotion.ID).SumAsync(o => o.TotalPrice);
                    var totalExpenseByAP = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).SumAsync(o => o.SellerAmount);
                    var totalExpenseByHalf = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).SumAsync(o => o.SellerAmount);
                    result.TotalBudgetPromotionAmount = result.FGFDiscount + totalPreSalePrice + totalPromotionPrice + totalPromotionCreditCardPrice + totalExpenseByAP + totalExpenseByHalf + result.FreeDownDiscount;

                }
                else
                {
                    result.TotalBudgetPromotionAmount = 0;
                }
                result.TotalPromotionAmount = result.CashDiscount + result.TransferDiscount + result.TotalBudgetPromotionAmount ;

                //New FGF Code 
                result.FGFCode = unitPriceModel.Booking?.FGFCode;

                return result;
            }
            else
            {
                return null;
            }
        }


        public async static Task<BookingPriceListDTO> CreateFromModelChangeProAsync(Guid bookingID, DatabaseContext db)
        {
            var unitPriceModel = await db.UnitPrices
                .Include(o => o.Booking)
                .ThenInclude(o => o.ReferContact)
                .ThenInclude(o => o.ContactTitleTH)
                .Include(o => o.Booking)
                .ThenInclude(o => o.ReferContact)
                .ThenInclude(o => o.ContactTitleEN)
                .Include(o => o.UnitPriceStage)
                .Where(o => o.BookingID == bookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Booking && o.IsActive==false).FirstOrDefaultAsync();

            if (unitPriceModel != null)
            {
                var result = new BookingPriceListDTO();

                result.CashDiscount = unitPriceModel.CashDiscount.HasValue ? unitPriceModel.CashDiscount.Value : 0;
                result.TransferDiscount = unitPriceModel.TransferDiscount.HasValue ? unitPriceModel.TransferDiscount.Value : 0;
                result.SellingPrice = unitPriceModel.SellingPrice.HasValue ? unitPriceModel.SellingPrice.Value : 0;
                result.NetSellingPrice = unitPriceModel.AgreementPrice.HasValue ? unitPriceModel.AgreementPrice.Value : 0;
                result.BookingAmount = unitPriceModel.BookingAmount.HasValue ? unitPriceModel.BookingAmount.Value : 0;
                result.ContractAmount = unitPriceModel.AgreementAmount.HasValue ? unitPriceModel.AgreementAmount.Value : 0;
                result.FreeDownDiscount = unitPriceModel.FreedownDiscount.HasValue ? unitPriceModel.FreedownDiscount.Value : 0;
                result.FGFDiscount = unitPriceModel.FGFDiscount.HasValue ? unitPriceModel.FGFDiscount.Value : 0;
                result.DownAmount = unitPriceModel.InstallmentAmount.HasValue ? unitPriceModel.InstallmentAmount.Value : 0;
                result.PercentDown = unitPriceModel.InstallmentPercent.HasValue ? unitPriceModel.InstallmentPercent.Value : 0;
                //result.TransferAmount = result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;

                var paymentIDs = await db.Payments
                            .Include(o => o.PaymentState)
                            .Where(o =>
                                o.BookingID == bookingID
                                && (
                                    o.PaymentState.Key == PaymentStateKeys.Booking
                                )
                            )
                            .OrderBy(o => o.Created).Select(o => o.ID).ToListAsync();

                var listAPAmounMasterPriceItemKeys = new List<string>();
                listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.BookingAmount);
                listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.ContractAmount);
                listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.InstallmentAmount);
                listAPAmounMasterPriceItemKeys.Add(MasterPriceItemKeys.TransferAmount);

                var TransferAmount = await db.PaymentItems
                    .Include(o => o.MasterPriceItem)
                    .Where(o => paymentIDs.Contains(o.PaymentID) && listAPAmounMasterPriceItemKeys.Contains(o.MasterPriceItem.Key)).SumAsync(o => o.PayAmount);

                result.TransferAmount = result.NetSellingPrice - TransferAmount;

                result.Installment = unitPriceModel.Installment.HasValue ? unitPriceModel.Installment.Value : 0;
                result.SpecialInstallment = unitPriceModel.SpecialInstallment.HasValue ? unitPriceModel.SpecialInstallment.Value : 0;
                result.ReferContact = CTM.ContactListDTO.CreateFromModel(unitPriceModel.Booking.ReferContact, db);

                var installments = await db.UnitPriceInstallments.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();
                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
                if (installments.Count > 0)
                {
                    var normalInstallment = installments.Where(o => o.IsSpecialInstallment == false).ToList();
                    result.NormalInstallment = normalInstallment.Count;
                    if (normalInstallment.Count == 0)
                    {
                        result.InstallmentAmount = 0;

                    }
                    else
                    {
                        result.InstallmentAmount = normalInstallment[0].Amount;
                    }
                    foreach (var item in installments.Where(o => o.IsSpecialInstallment == true).OrderBy(o => o.Period).ToList())
                    {
                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = item.Period,
                            Amount = item.Amount
                        });
                    }
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



                var promotion = await db.SalePromotions.Where(o => o.BookingID == bookingID).FirstOrDefaultAsync();

                if (promotion != null)
                {
                    var totalPreSalePrice = await db.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == bookingID).SumAsync(o => o.Quantity * o.PricePerUnit);
                    var totalPromotionPrice = await db.SalePromotionItems.Where(o => o.SalePromotionID == promotion.ID && o.MainSalePromotionItemID == null).SumAsync(o => o.TotalPrice);
                    var totalPromotionCreditCardPrice = await db.SalePromotionCreditCardItems.Where(o => o.SalePromotionID == promotion.ID).SumAsync(o => o.TotalPrice);
                    var totalExpenseByAP = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).SumAsync(o => o.SellerAmount);
                    var totalExpenseByHalf = await db.SalePromotionExpenses
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.SalePromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).SumAsync(o => o.SellerAmount);
                    result.TotalBudgetPromotionAmount = result.FGFDiscount + totalPreSalePrice + totalPromotionPrice + totalPromotionCreditCardPrice + totalExpenseByAP + totalExpenseByHalf + result.FreeDownDiscount;

                }
                else
                {
                    result.TotalBudgetPromotionAmount = 0;
                }

                result.FGFCode = unitPriceModel?.Booking?.FGFCode;
                result.TotalPromotionAmount = result.CashDiscount + result.TransferDiscount + result.TotalBudgetPromotionAmount;

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
