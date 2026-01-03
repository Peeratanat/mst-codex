using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    public class PaymentUnitPriceDTO
    {
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
        public decimal? TotalBudgetPromotion { get; set; }
        /// <summary>
        /// งวดดาวน์พิเศษ
        /// </summary>
        public List<SpecialInstallmentDTO> SpecialInstallmentPeriods { get; set; }

        /// <summary>
        /// เนื้อที่เพิ่มลด
        /// </summary>
        public decimal? ExtraAreaPriceAmount { get; set; }

        /// <summary>
        /// มูลค่าบ้านสุทธิหลังหักส่วนลดทุกอย่างแล้ว ([ราคาขาย]-[มูลค่ารวมโปรโมชั่นที่ลูกค้าได้รับ]-[ส่วนลด Freedown])
        /// </summary>
        public decimal? RevenueAmount { get; set; }

        /// <summary>
        /// ยอดรวม FET
        /// </summary>
        public decimal? SumFETPayment { get; set; }

        /// <summary>
        /// Direct DD/DC Status
        /// </summary>
        public string DirectDDDCType { get; set; }

        /// <summary>
        /// Direct DD/DC Status
        /// </summary>
        public string DirectDDDCStatus { get; set; }

        /// <summary>
        /// วันที่เริ่มตัด Direct DD/DC
        /// </summary>
        public DateTime? DirectDDDCStratDate { get; set; }

        /// <summary>
        /// ยอดเงินรอ Import DD/DC
        /// </summary>
        public decimal? SumWaitingConfirmDirectDDDCAmount { get; set; }

        public async static Task<PaymentUnitPriceDTO> CreateDraftFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var priceList = await db.GetActivePriceListAsync(unitID);

            if (priceList == null)
                return null;

            var result = new PaymentUnitPriceDTO();
            result.SellingPrice = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice).Select(o => o.Amount).FirstOrDefault();
            result.NetSellingPrice = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
            result.BookingAmount = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.BookingAmount).Select(o => o.Amount).FirstOrDefault();
            result.ContractAmount = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.ContractAmount).Select(o => o.Amount).FirstOrDefault();
            result.DownAmount = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount).Select(o => o.Amount).FirstOrDefault();
            result.TransferAmount = result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;
            result.Installment = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount).Select(o => o.Installment).FirstOrDefault() ?? 0;
            result.InstallmentAmount = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault() ?? 0;

            var specialDownInstallmentStrings = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault();
            var specialDownInstallmentAmountStrings = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault();
            var specialDownInstallments = specialDownInstallmentStrings?.Split(',');
            var specialDownInstallmentAmounts = specialDownInstallmentAmountStrings?.Split(',');

            result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();

            if (specialDownInstallmentAmountStrings != null)
            {
                for (int i = 0; i < specialDownInstallments.Length; i++)
                {
                    int periodNo;
                    if (int.TryParse(specialDownInstallments[i], out periodNo))
                    {
                        decimal amount = 0;
                        if (i < specialDownInstallmentAmounts.Length)
                        {
                            decimal.TryParse(specialDownInstallmentAmounts[i], out amount);
                        }

                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = periodNo,
                            Amount = amount
                        });
                    }
                }
            }

            result.SpecialInstallment = result.SpecialInstallmentPeriods.Count;
            result.NormalInstallment = result.Installment - result.SpecialInstallment;

            return result;
        }

        public async static Task<PaymentUnitPriceDTO> CreateFromModelAsync(UnitPrice unitPriceModel, DatabaseContext db)
        {
            if (unitPriceModel != null)
            {
                var result = new PaymentUnitPriceDTO();

                var UnitInstallments = await db.UnitPriceInstallments.Include(o => o.UnitPrice).ThenInclude(o => o.UnitPriceStage)
                    .Where(o => o.UnitPrice.BookingID == unitPriceModel.BookingID && o.UnitPrice.UnitPriceStage.Key == UnitPriceStageKeys.Agreement).ToListAsync() ?? new List<UnitPriceInstallment>();

                result.CashDiscount = unitPriceModel.CashDiscount ?? 0;
                result.TransferDiscount = unitPriceModel.TransferDiscount ?? 0;
                result.SellingPrice = unitPriceModel.SellingPrice ?? 0;

                result.NetSellingPrice = unitPriceModel.AgreementPrice ?? 0;

                result.BookingAmount = unitPriceModel.BookingAmount ?? 0;
                result.ContractAmount = unitPriceModel.AgreementAmount ?? 0;
                result.FreeDownDiscount = unitPriceModel.FreedownDiscount ?? 0;
                result.FGFDiscount = unitPriceModel.FGFDiscount ?? 0;
                result.DownAmount = unitPriceModel.TotalInstallmentAmount ?? 0;
                
                result.Installment = unitPriceModel.Installment ?? 0;
                result.SpecialInstallment = unitPriceModel.SpecialInstallment ?? 0;
                

                result.NormalInstallment = unitPriceModel.NormalInstallment ?? 0;
                result.InstallmentAmount = unitPriceModel.InstallmentAmount ?? 0;

                result.ExtraAreaPriceAmount = await db.UnitPriceItems.Include(o => o.MasterPriceItem)
                    .Where(o => o.UnitPriceID == unitPriceModel.ID && o.MasterPriceItem.Key == MasterPriceItemKeys.ExtraAreaPrice)
                    .Select(o => o.Amount).FirstOrDefaultAsync();

                result.RevenueAmount = ((result.NetSellingPrice ) + (result.ExtraAreaPriceAmount??0) ) - (unitPriceModel.TransferDiscount ?? 0);
                result.TransferAmount =(result.NetSellingPrice  ) - (unitPriceModel.BookingAmount ?? 0) - (unitPriceModel.AgreementAmount ?? 0)- (unitPriceModel.TotalInstallmentAmount ?? 0) - (unitPriceModel.FreedownDiscount ?? 0);

                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
                if (UnitInstallments.Any())
                {
                    result.SpecialInstallmentPeriods
                        .AddRange(UnitInstallments.Where(o => o.IsSpecialInstallment == true).OrderBy(o => o.Period)
                            .Select(o => new SpecialInstallmentDTO
                            {
                                Period = o.Period,
                                Amount = o.Amount
                            }).ToList());
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

                result.TotalBudgetPromotion = result.CashDiscount + result.TransferDiscount + bugetPromotion?.BudgetPromotionSale?.Budget;

                result.SumFETPayment = await db.FETs.Where(e => e.BookingID == unitPriceModel.BookingID && e.IsCancel == false).Select(e => e.Amount).SumAsync();

                var DirectDDDCForm = db.DirectCreditDebitApprovalForms
                    .Include(e => e.DirectApprovalFormStatus)
                    .Include(e => e.DirectApprovalFormType)
                .Where(e => e.BookingID == unitPriceModel.BookingID).OrderByDescending(e => e.Created).Select(e => new { e.StartDate, Status = e.DirectApprovalFormStatus.Name, Type = e.DirectApprovalFormType.Key }).FirstOrDefault();

                var DirectDDDCDetail = db.DirectCreditDebitExportDetails
                    .Include(e => e.DirectCreditDebitExportHeader)
                    .Include(e => e.DirectCreditDebitExportDetailStatus)
                    .Include(e => e.UnitPriceInstallment)
                            .ThenInclude(e => e.UnitPrice)
                                .ThenInclude(e => e.Booking)
                    .Where(e => e.UnitPriceInstallment.UnitPrice.Booking.ID == unitPriceModel.BookingID
                                && e.DirectCreditDebitExportDetailStatus.Key == DirectCreditDebitExportDetailStatusKeys.Wait
                                && e.DirectCreditDebitExportHeader.ID != null 
                                );

                result.DirectDDDCType = DirectDDDCForm?.Type;
                result.DirectDDDCStatus = DirectDDDCForm?.Status;
                result.DirectDDDCStratDate = DirectDDDCForm?.StartDate;
                var DirectDDDCList = await DirectDDDCDetail.ToListAsync();
                result.SumWaitingConfirmDirectDDDCAmount = DirectDDDCList.Sum(e=>e.Amount);

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
