using Base.DTOs.MST;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRM;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class TransferExpenseListDTO
    {
        /// <summary>
        /// ข้อมูลโอน
        /// </summary>
        public TransferDropdownDTO Transfer { get; set; }
        /// <summary>
        /// คชจ.
        /// </summary>
        public MasterPriceItemDTO MasterPriceItem { get; set; }
        /// <summary>
        /// ผู้รับผิดชอบ คชจ.
        /// </summary>
        public MasterCenterDTO ExpenseReponsibleBy { get; set; }
        /// <summary>
        /// รายการ
        /// </summary>
        public string ExpenseName { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public double? Amount { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public MasterCenterDTO PriceUnitName { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย (บาท)
        /// </summary>
        public decimal? PricePerUnit { get; set; }
        /// <summary>
        /// ราคารวม (บาท)
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// ลูกค้าจ่าย (บาท)
        /// </summary>
        public decimal BuyerAmount { get; set; }
        /// <summary>
        /// บริษัทจ่าย (บาท)
        /// </summary>
        public decimal SellerAmount { get; set; }

        public async static Task<List<TransferExpenseListDTO>> CreateFromModelAsync(Guid transferID, DatabaseContext DB)
        {
            var result = new List<TransferExpenseListDTO>();

            var transferExp = await DB.TransferExpenses.Where(o => o.TransferID == transferID)
                                .Include(o => o.Transfer)
                                .Include(o => o.ExpenseReponsibleBy)
                                .Include(o => o.MasterPriceItem)
                                .Include(o => o.PriceUnit)
                                .Include(o => o.PaymentReceiver)
                                .ToListAsync() ?? new List<TransferExpense>();

            var transfer = await DB.Transfers
                                    .Where(o => o.ID == transferID)
                                    .Include(o => o.Unit)
                                    .ThenInclude(o => o.Model)
                                    .Include(o => o.Project)
                                    .ThenInclude(o => o.ProductType)
                                    .FirstOrDefaultAsync();

            if (transfer.Project.ProductType.Key == ProductTypeKeys.LowRise)
            {
                transferExp = transferExp.Where(o => o.MasterPriceItem.Key != MasterPriceItemKeys.FirstSinkingFund).ToList() ?? new List<TransferExpense>();
            }

            if (transferExp.Count > 0)
            {
                foreach (var item in transferExp)
                {
                    if (item?.MasterPriceItem?.Key == MasterPriceItemKeys.CommonFeeCharge)
                    {
                        if (transfer.Project.IsPublicFundPayProject == true)
                        {
                            result.Add(
                            new TransferExpenseListDTO
                            {
                                Transfer = TransferDropdownDTO.CreateFromModel(item.Transfer),
                                MasterPriceItem = MasterPriceItemDTO.CreateFromModel(item.MasterPriceItem),
                                ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(item.ExpenseReponsibleBy),
                                ExpenseName = item.Name + "(เหมาจ่าย)",
                                Amount = (item.PriceUnitAmount ?? 0),
                                PriceUnitName = MasterCenterDTO.CreateFromModel(item.PriceUnit),
                                PricePerUnit = (item.PricePerUnitAmount ?? 0),
                                TotalPrice = item.Amount,
                                BuyerAmount = item.BuyerAmount,
                                SellerAmount = item.SellerAmount
                            }
                        );
                        }
                        else
                        {
                            result.Add(
                            new TransferExpenseListDTO
                            {
                                Transfer = TransferDropdownDTO.CreateFromModel(item.Transfer),
                                MasterPriceItem = MasterPriceItemDTO.CreateFromModel(item.MasterPriceItem),
                                ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(item.ExpenseReponsibleBy),
                                ExpenseName = item.Name,
                                Amount = (item.PriceUnitAmount ?? 0),
                                PriceUnitName = MasterCenterDTO.CreateFromModel(item.PriceUnit),
                                PricePerUnit = (item.PricePerUnitAmount ?? 0),
                                TotalPrice = item.Amount,
                                BuyerAmount = item.BuyerAmount,
                                SellerAmount = item.SellerAmount
                            }
                        );
                        }
                    }
                    else
                    {

                        if (item?.MasterPriceItem?.Key == MasterPriceItemKeys.TransferFee)
                        {
                            //ค่าธรรมเนียมโอน
                            var feeResults = await DB.TransferFeeResults
                                            .Where(o => o.AgreementID == transfer.AgreementID).FirstOrDefaultAsync();

                            var result2 = new TransferExpenseListDTO
                            {
                                Transfer = TransferDropdownDTO.CreateFromModel(item.Transfer),
                                MasterPriceItem = MasterPriceItemDTO.CreateFromModel(item.MasterPriceItem),
                                ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(item.ExpenseReponsibleBy),
                                ExpenseName = item.Name,
                                Amount = (feeResults.TransferFeeRate ?? 0),
                                PriceUnitName = MasterCenterDTO.CreateFromModel(item.PriceUnit),
                                PricePerUnit = (feeResults.EstimateTotalPrice ?? 0),
                                TotalPrice = Math.Ceiling(((feeResults.EstimateTotalPrice ?? 0) * ((decimal)((feeResults.TransferFeeRate ?? 0) / 100)))),
                                BuyerAmount = item.BuyerAmount,
                                SellerAmount = item.SellerAmount
                            };
                            if (result2.ExpenseReponsibleBy?.Key == "0")
                            {
                                result2.SellerAmount = result2.TotalPrice;
                                result2.BuyerAmount = 0;
                            }
                            else if (result2.ExpenseReponsibleBy?.Key == "1")
                            {
                                result2.SellerAmount = 0;
                                result2.BuyerAmount = result2.TotalPrice;
                            }
                            else
                            {
                                var amount = Math.Ceiling((result2.TotalPrice / 2));

                                result2.SellerAmount = amount;
                                result2.BuyerAmount = amount;
                            }

                            result.Add(result2);


                        }

                        else
                        {
                            result.Add(
                          new TransferExpenseListDTO
                          {
                              Transfer = TransferDropdownDTO.CreateFromModel(item.Transfer),
                              MasterPriceItem = MasterPriceItemDTO.CreateFromModel(item.MasterPriceItem),
                              ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(item.ExpenseReponsibleBy),
                              ExpenseName = item.Name,
                              Amount = (item.PriceUnitAmount ?? 0),
                              PriceUnitName = MasterCenterDTO.CreateFromModel(item.PriceUnit),
                              PricePerUnit = (item.PricePerUnitAmount ?? 0),
                              TotalPrice = item.Amount,
                              BuyerAmount = item.BuyerAmount,
                              SellerAmount = item.SellerAmount
                          }
                      );
                        }

                    }

                }


                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<List<TransferExpenseListDTO>> CreateFromDrafModelAsync(Guid agreementID, DatabaseContext DB)
        {
            var result = new List<TransferExpenseListDTO>();

            var agreement = await DB.Agreements
                                    .Include(o => o.Booking)
                                    .ThenInclude(o => o.CreditBankingType)
                                    .Include(o => o.Project)
                                    .ThenInclude(o => o.ProductType)
                                    .Where(o => o.ID == agreementID).FirstOrDefaultAsync();

            //ประเภทโครงการ แนวราบ/แนวสูง
            var ProductType = (agreement.Project?.ProductType?.Key ?? "");

            //โอนสด
            var isTransferCash = agreement.Booking.CreditBankingType?.Key == CreditBankingTypeKey.TransferbyCustomer;

            var agreementConfig = await DB.AgreementConfigs.FirstOrDefaultAsync(o => o.ProjectID == agreement.ProjectID);

            var bookingID = agreement.BookingID;

            var craditBanking = await DB.CreditBankings
                  .Include(o => o.FinancialInstitution)
                  .Include(o => o.Bank)
                  .Where(o =>
                        o.BookingID == bookingID
                        && o.IsUseBank == true
                        && o.LoanStatus.Key == LoanStatusKeys.Approve
                ).FirstOrDefaultAsync();

            //ถ้าเป็นสหกรณ์ และถูกกำหนดที่หน้าMaster Data ว่าฟรีขอสินเชื่อ ในหน้าโปรโอนและหน้าโอนกรรมสิทธิ์ จะไม่มีรายการค่าจดจำนองแสดงเลย
            var IsFreeMortgage = false;
            if (
                    craditBanking?.Bank?.IsFreeMortgage == true
                //&& (
                //    booking?.CreditBankingType?.Key == CreditBankingTypeKey.LoanbyCustomer
                //    || booking?.CreditBankingType?.Key == CreditBankingTypeKey.LoanbyProject
                //)
                )
            {
                IsFreeMortgage = true;
            }

            var unitPriceModel_TP = await DB.UnitPrices
                       .Include(o => o.Booking)
                       .Include(o => o.UnitPriceStage)
                       .Where(o => o.BookingID == bookingID
                               && o.UnitPriceStage.Key == UnitPriceStageKeys.TransferPromotion
                            //&& o.IsActive
                            )
                       .OrderByDescending(o => o.Created)
                       .FirstOrDefaultAsync();

            var unitPriceModel_AG = await DB.UnitPrices
                       .Include(o => o.Booking)
                       .Include(o => o.UnitPriceStage)
                       .Where(o => o.BookingID == bookingID
                               && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement
                            //&& o.IsActive
                            )
                       .OrderByDescending(o => o.Created)
                       .FirstOrDefaultAsync();

            var IsHasTransferPromotion = false;
            if (unitPriceModel_TP != null)
            {
                IsHasTransferPromotion = true;
            }

            var unitPriceModel = unitPriceModel_TP;
            if (unitPriceModel == null)
            {
                unitPriceModel = unitPriceModel_AG;
            }

            if (unitPriceModel != null)
            {

                var listExpenseMasterPriceItemKeys = new List<string>();
                listExpenseMasterPriceItemKeys.Add(MasterPriceItemKeys.WaterMeter);
                listExpenseMasterPriceItemKeys.Add(MasterPriceItemKeys.TransferFee);
                listExpenseMasterPriceItemKeys.Add(MasterPriceItemKeys.CommonFeeCharge);
                listExpenseMasterPriceItemKeys.Add(MasterPriceItemKeys.DocumentFeeCharge);
                listExpenseMasterPriceItemKeys.Add(MasterPriceItemKeys.ElectricMeter);

                if (!isTransferCash && !IsFreeMortgage)
                {
                    listExpenseMasterPriceItemKeys.Add(MasterPriceItemKeys.MortgageFee);
                    listExpenseMasterPriceItemKeys.Add(MasterPriceItemKeys.MortgageFeeOverLoan);
                }

                if (ProductType == ProductTypeKeys.HighRise)
                {
                    listExpenseMasterPriceItemKeys.Add(MasterPriceItemKeys.FirstSinkingFund);
                }

                var unitPriceItemModel_TP = new List<UnitPriceItem>();
                if (unitPriceModel_TP != null)
                {
                    unitPriceItemModel_TP = await DB.UnitPriceItems
                                                    .Include(o => o.PriceUnit)
                                                    .Include(o => o.UnitPrice)
                                                    .Include(o => o.UnitPrice.UnitPriceStage)
                                                    .Include(o => o.MasterPriceItem)
                                                    .Where(o =>
                                                        o.UnitPriceID == unitPriceModel_TP.ID
                                                        && o.UnitPrice.UnitPriceStage.Key == UnitPriceStageKeys.TransferPromotion
                                                        && listExpenseMasterPriceItemKeys.Contains(o.MasterPriceItem.Key)
                                                    )
                                                    .ToListAsync() ?? new List<UnitPriceItem>();

                    unitPriceItemModel_TP = unitPriceItemModel_TP.Where(o => o.Amount > 0).ToList() ?? new List<UnitPriceItem>();

                }

                var unitPriceItemModel_AG = new List<UnitPriceItem>();
                if (unitPriceModel_AG != null)
                {
                    unitPriceItemModel_AG = await DB.UnitPriceItems
                                               .Include(o => o.PriceUnit)
                                               .Include(o => o.UnitPrice)
                                               .Include(o => o.UnitPrice.UnitPriceStage)
                                               .Include(o => o.MasterPriceItem)
                                               .Where(o =>
                                                   o.UnitPriceID == unitPriceModel_AG.ID
                                                   && o.UnitPrice.UnitPriceStage.Key == UnitPriceStageKeys.Agreement
                                                   && listExpenseMasterPriceItemKeys.Contains(o.MasterPriceItem.Key)
                                               )
                                               .ToListAsync() ?? new List<UnitPriceItem>();
                }

                var unitPriceItemModel = new List<UnitPriceItem>();
                unitPriceItemModel = unitPriceItemModel_TP;
                if (unitPriceItemModel_AG.Any())
                {
                    foreach (var a in unitPriceItemModel_AG)
                    {
                        var mstPriceItem = unitPriceItemModel.Find(o => o.MasterPriceItemID == a.MasterPriceItemID);
                        if (mstPriceItem == null)
                        {
                            unitPriceItemModel.Add(a);
                        }
                    }
                }
                unitPriceItemModel = unitPriceItemModel.OrderBy(o => o.MasterPriceItem.Order).ToList() ?? new List<UnitPriceItem>();

                var listSaleExpense_AG = await DB.SalePromotionExpenses
                                                .Include(o => o.SalePromotion)
                                                .Include(o => o.SalePromotion.SalePromotionStage)
                                                .Include(o => o.MasterPriceItem)
                                                .Include(o => o.ExpenseReponsibleBy)
                                                .Include(o => o.PaymentReceiver)
                                                .Where(o => o.SalePromotion.BookingID == bookingID
                                                    && o.SalePromotion.IsActive == true
                                                    && o.SalePromotion.SalePromotionStage.Key == SalePromotionStageKeys.Agreement
                                                )
                                                .ToListAsync() ?? new List<SalePromotionExpense>();

                var listSaleExpense_BK = await DB.SalePromotionExpenses
                                               .Include(o => o.SalePromotion)
                                               .Include(o => o.SalePromotion.SalePromotionStage)
                                               .Include(o => o.MasterPriceItem)
                                               .Include(o => o.ExpenseReponsibleBy)
                                               .Include(o => o.PaymentReceiver)
                                               .Where(o => o.SalePromotion.BookingID == bookingID
                                                   && o.SalePromotion.IsActive == true
                                                   && o.SalePromotion.SalePromotionStage.Key == SalePromotionStageKeys.Booking
                                               )
                                               .ToListAsync() ?? new List<SalePromotionExpense>();

                var listSaleExpense = new List<SalePromotionExpense>();
                listSaleExpense = listSaleExpense_AG;
                if (listSaleExpense.Count() == 0)
                {
                    listSaleExpense = listSaleExpense_BK;
                }

                var listTransferExpense = new List<TransferPromotionExpense>();
                listTransferExpense = await DB.TransferPromotionExpenses
                                           .Include(o => o.TransferPromotion)
                                           .Include(o => o.MasterPriceItem)
                                           .Include(o => o.ExpenseReponsibleBy)
                                           .Include(o => o.PaymentReceiver)
                                           .Where(o => o.TransferPromotion.BookingID == bookingID
                                                           && o.TransferPromotion.IsActive)
                                           .ToListAsync() ?? new List<TransferPromotionExpense>();

                listTransferExpense = listTransferExpense.Where(o => o.Amount > 0).ToList() ?? new List<TransferPromotionExpense>();

                //ค่าจดจำนอง
                var loanAmount = await DB.CreditBankings
                                    .Include(o => o.LoanStatus)
                                    .Where(o => o.BookingID == bookingID
                                            && o.LoanStatus.Key == "1"
                                            && (o.IsUseBank.HasValue && o.IsUseBank.Value))
                                    .Select(o => o.ApprovedAmount)
                                    .FirstOrDefaultAsync();

                //ราคาขาย
                var sellingPrice = unitPriceModel.SellingPrice ?? 0;

                //ราคาบ้านในสัญญา
                var agreementPrice = unitPriceModel.AgreementPrice ?? 0;

                //ค่าธรรมเนียมโอน
                var feeResults = await DB.TransferFeeResults
                                .Where(o => o.AgreementID == agreementID).FirstOrDefaultAsync();

                ////โอน
                //var transfer = await DB.Transfers
                //                .Where(o => o.AgreementID == agreementID).FirstOrDefaultAsync();

                #region กรณีที่ค่าจดจำนองฟรี แล้วค่าจดจำนองกู้เกินฟรี ให้นำกู้เกินมาบวกไว้ในค่าจดจำนองเลย

                var IsHasFreeMortgageFee = false;
                var IsHasFreeMortgageFeeOverLoan = false;

                var IsHasNotFreeMortgageFee = false;
                var IsHasNotFreeMortgageFeeOverLoan = false;

                var IsHasAGFreeMortgageFee = false;

                decimal FM_PricePerUnitAmount = 0;

                foreach (var itemZ in unitPriceItemModel)
                {
                    var saleExpense = listSaleExpense.Find(o => o.MasterPriceItemID == itemZ.MasterPriceItemID);
                    var transferExpense = listTransferExpense.Find(o => o.MasterPriceItemID == itemZ.MasterPriceItemID);

                    var ExpenseReponsibleBy = new MasterCenterDTO();

                    transferExpense = transferExpense ?? new TransferPromotionExpense();
                    if (transferExpense.Amount > 0)
                    {
                        ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(transferExpense.ExpenseReponsibleBy);
                    }
                    else if (saleExpense != null)
                    {
                        ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(saleExpense.ExpenseReponsibleBy);
                    }

                    if (
                            ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company
                            && itemZ.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                        )
                    {
                        IsHasFreeMortgageFee = true;
                    }

                    if (
                            ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Customer
                            && itemZ.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                        )
                    {
                        IsHasNotFreeMortgageFee = true;
                    }

                    if (
                            ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company
                            && itemZ.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan
                        )
                    {
                        IsHasFreeMortgageFeeOverLoan = true;
                    }

                    if (
                            ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Customer
                            && itemZ.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan
                        )
                    {
                        IsHasNotFreeMortgageFeeOverLoan = true;
                    }

                }

                foreach (var itemZ in unitPriceItemModel_AG)
                {
                    var saleExpense = listSaleExpense.Find(o => o.MasterPriceItemID == itemZ.MasterPriceItemID);

                    var ExpenseReponsibleBy = new MasterCenterDTO();

                    if (saleExpense != null)
                    {
                        ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(saleExpense.ExpenseReponsibleBy);
                    }

                    if (
                            ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company
                            && itemZ.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                        )
                    {
                        IsHasAGFreeMortgageFee = true;
                    }

                }

                if (
                        IsHasFreeMortgageFee
                        && IsHasFreeMortgageFeeOverLoan
                    //&& transfer == null
                    )
                {

                    var itemMortgageFeeX = unitPriceItemModel.Find(o => o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee) ?? new UnitPriceItem();
                    var itemMortgageFeeOverLoanX = unitPriceItemModel.Find(o => o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan) ?? new UnitPriceItem();

                    //FM_PricePerUnitAmount = Math.Ceiling((itemMortgageFeeX.PricePerUnitAmount ?? 0) + (itemMortgageFeeOverLoanX.PricePerUnitAmount ?? 0));
                    FM_PricePerUnitAmount = Math.Ceiling(agreementPrice + (itemMortgageFeeOverLoanX.PricePerUnitAmount ?? 0));

                    unitPriceItemModel.Remove(unitPriceItemModel.Find(o => o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan));
                }

                #endregion

                decimal TitledeedArea = 0;
                var titledeedDetail = DB.TitledeedDetails.Where(o => o.UnitID == agreement.UnitID).FirstOrDefault();
                if (titledeedDetail != null)
                {
                    decimal.TryParse((titledeedDetail.TitledeedArea ?? 0).ToString(), out TitledeedArea);
                }

                foreach (var itemY in unitPriceItemModel)
                {
                    var saleExpense = listSaleExpense.Find(o => o.MasterPriceItemID == itemY.MasterPriceItemID);
                    var transferExpense = listTransferExpense.Find(o => o.MasterPriceItemID == itemY.MasterPriceItemID);

                    var exps = new TransferExpenseListDTO();
                    exps.Transfer = new TransferDropdownDTO();

                    exps.MasterPriceItem = MasterPriceItemDTO.CreateFromModel(itemY.MasterPriceItem);
                    exps.ExpenseName = itemY.MasterPriceItem?.Detail;
                    exps.Amount = Math.Ceiling((itemY.PriceUnitAmount ?? 0));
                    exps.PriceUnitName = MasterCenterDTO.CreateFromModel(itemY.PriceUnit);
                    exps.PricePerUnit = Math.Ceiling((itemY.PricePerUnitAmount ?? 0));

                    transferExpense = transferExpense ?? new TransferPromotionExpense();
                    if (transferExpense.Amount > 0)
                    {
                        exps.ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(transferExpense.ExpenseReponsibleBy);
                        exps.TotalPrice = Math.Ceiling(transferExpense.Amount);
                        exps.BuyerAmount = Math.Ceiling(transferExpense.BuyerAmount);
                        exps.SellerAmount = Math.Ceiling(transferExpense.SellerAmount);
                    }
                    else if (saleExpense != null)
                    {
                        exps.ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(saleExpense.ExpenseReponsibleBy);
                        exps.TotalPrice = Math.Ceiling(saleExpense.Amount);
                        exps.BuyerAmount = Math.Ceiling(saleExpense.BuyerAmount);
                        exps.SellerAmount = Math.Ceiling(saleExpense.SellerAmount);
                    }

                    if (feeResults != null)
                    {
                        #region ค่าธรรมเนียม

                        if (itemY.MasterPriceItem?.Key == MasterPriceItemKeys.TransferFee)
                        {
                            exps.Amount = (feeResults.TransferFeeRate ?? 0);
                            exps.PricePerUnit = (feeResults.EstimateTotalPrice ?? 0);
                            exps.TotalPrice = Math.Ceiling(((feeResults.EstimateTotalPrice ?? 0) * ((decimal)((feeResults.TransferFeeRate ?? 0) / 100))));

                            if (exps.ExpenseReponsibleBy?.Key == "0")
                            {
                                exps.SellerAmount = exps.TotalPrice;
                                exps.BuyerAmount = 0;
                            }
                            else if (exps.ExpenseReponsibleBy?.Key == "1")
                            {
                                exps.SellerAmount = 0;
                                exps.BuyerAmount = exps.TotalPrice;
                            }
                            else
                            {
                                var amount = Math.Ceiling((exps.TotalPrice / 2));

                                exps.SellerAmount = amount;
                                exps.BuyerAmount = amount;
                            }
                        }

                        #endregion

                        #region ค่าจดจำนอง

                        if (itemY.MasterPriceItem?.Key == MasterPriceItemKeys.MortgageFee)
                        {
                            exps.Amount = (feeResults.MortgageRate ?? 0);

                            if (
                                    IsHasAGFreeMortgageFee
                                //IsHasFreeMortgageFee
                                //IsHasFreeMortgageFee && !IsHasTransferPromotion                                    
                                )
                            {
                                exps.PricePerUnit = agreementPrice;
                                exps.TotalPrice = Math.Ceiling(((decimal)(exps.Amount ?? 0) * ((exps.PricePerUnit ?? 0) / 100)));

                                if (exps.ExpenseReponsibleBy?.Key == "0")
                                {
                                    exps.SellerAmount = exps.TotalPrice;
                                    exps.BuyerAmount = 0;
                                }
                                else if (exps.ExpenseReponsibleBy?.Key == "1")
                                {
                                    exps.SellerAmount = 0;
                                    exps.BuyerAmount = exps.TotalPrice;
                                }
                                else
                                {
                                    var amount = Math.Ceiling((exps.TotalPrice / 2));
                                    exps.SellerAmount = amount;
                                    exps.BuyerAmount = amount;
                                }

                            }

                            /*-- Re Cal for 'PricePerUnit' has to change value --*/
                            if (true)
                            {
                                exps.TotalPrice = Math.Ceiling(((decimal)(exps.Amount ?? 0) * ((exps.PricePerUnit ?? 0) / 100)));

                                if (exps.ExpenseReponsibleBy?.Key == "0")
                                {
                                    exps.SellerAmount = exps.TotalPrice;
                                    exps.BuyerAmount = 0;
                                }
                                else if (exps.ExpenseReponsibleBy?.Key == "1")
                                {
                                    exps.SellerAmount = 0;
                                    exps.BuyerAmount = exps.TotalPrice;
                                }
                                else
                                {
                                    var amount = Math.Ceiling((exps.TotalPrice / 2));
                                    exps.SellerAmount = amount;
                                    exps.BuyerAmount = amount;
                                }

                            }

                            if (IsHasFreeMortgageFee && IsHasFreeMortgageFeeOverLoan)
                            {
                                exps.PricePerUnit = FM_PricePerUnitAmount;
                                exps.TotalPrice = Math.Ceiling(((decimal)(exps.Amount ?? 0) * ((exps.PricePerUnit ?? 0) / 100)));

                                if (exps.ExpenseReponsibleBy?.Key == "0")
                                {
                                    exps.SellerAmount = exps.TotalPrice;
                                    exps.BuyerAmount = 0;
                                }
                                else if (exps.ExpenseReponsibleBy?.Key == "1")
                                {
                                    exps.SellerAmount = 0;
                                    exps.BuyerAmount = exps.TotalPrice;
                                }
                                else
                                {
                                    var amount = Math.Ceiling((exps.TotalPrice / 2));
                                    exps.SellerAmount = amount;
                                    exps.BuyerAmount = amount;
                                }

                            }

                            #region กรณี ไม่มี Free
                            if (exps.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Customer)
                            {
                                exps.PricePerUnit = loanAmount;
                                exps.TotalPrice = Math.Ceiling(((decimal)(exps.Amount ?? 0) * ((exps.PricePerUnit ?? 0) / 100)));

                                if (agreement.Project.ProductType?.Key == ProductTypeKeys.LowRise && exps.TotalPrice > 200000) //'แนวราบต้องไม่เกิน 200000
                                {
                                    exps.TotalPrice = 200000;
                                }

                                if (exps.ExpenseReponsibleBy?.Key == "0")
                                {
                                    exps.SellerAmount = exps.TotalPrice;
                                    exps.BuyerAmount = 0;
                                }
                                else if (exps.ExpenseReponsibleBy?.Key == "1")
                                {
                                    exps.SellerAmount = 0;
                                    exps.BuyerAmount = exps.TotalPrice;
                                }
                                else
                                {
                                    var amount = Math.Ceiling((exps.TotalPrice / 2));
                                    exps.SellerAmount = amount;
                                    exps.BuyerAmount = amount;
                                }

                            }
                            #endregion

                            #region กรณี Free
                            else if (exps.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company)
                            {
                                decimal pricePerUnit_Mg = exps.PricePerUnit ?? 0;

                                //กรณีที่ค่าจดจำนองฟรี  แล้วไปแก้ไขขอสินเชื่อให้ < ค่าจำนอง ให้ระบบนำยอดขอสินเชื่อนั้นมาเลย
                                if (loanAmount < pricePerUnit_Mg)
                                {
                                    exps.PricePerUnit = loanAmount;
                                    exps.TotalPrice = Math.Ceiling(((decimal)(exps.Amount ?? 0) * ((exps.PricePerUnit ?? 0) / 100)));

                                    if (agreement.Project.ProductType?.Key == ProductTypeKeys.LowRise && exps.TotalPrice > 200000) //'แนวราบต้องไม่เกิน 200000
                                    {
                                        exps.TotalPrice = 200000;
                                    }

                                    if (exps.ExpenseReponsibleBy?.Key == "0")
                                    {
                                        exps.SellerAmount = exps.TotalPrice;
                                        exps.BuyerAmount = 0;
                                    }
                                    else if (exps.ExpenseReponsibleBy?.Key == "1")
                                    {
                                        exps.SellerAmount = 0;
                                        exps.BuyerAmount = exps.TotalPrice;
                                    }
                                    else
                                    {
                                        var amount = Math.Ceiling((exps.TotalPrice / 2));
                                        exps.SellerAmount = amount;
                                        exps.BuyerAmount = amount;
                                    }

                                }

                            }
                            #endregion

                        }

                        #endregion
                    }

                    #region ค่าส่วนกลาง
                    if (itemY.MasterPriceItem?.Key == MasterPriceItemKeys.CommonFeeCharge)
                    {
                        if (agreementConfig != null)
                        {
                            var PublicFundRate = agreementConfig.PublicFundRate ?? 0;

                            if (titledeedDetail != null)
                            {
                                var unit = await DB.Units.Where(o => o.ID == agreement.UnitID).Include(m => m.Model).Include(p => p.Project).FirstOrDefaultAsync();
                                decimal PricePerUnit = PublicFundRate * TitledeedArea;

                                if (unit.Project.IsPublicFundPayProject == true)
                                {
                                    PricePerUnit = unit.Model.PublicFundAmount ?? 0;
                                    exps.ExpenseName = exps.ExpenseName + "(เหมาจ่าย)";
                                }
                                else
                                {
                                    PricePerUnit = PublicFundRate * TitledeedArea;
                                }

                                decimal PriceUnitAmount = 0; decimal.TryParse((exps.Amount ?? 0).ToString(), out PriceUnitAmount);

                                exps.PricePerUnit = PricePerUnit;
                                exps.TotalPrice = Math.Ceiling(PricePerUnit * PriceUnitAmount);

                                if (exps.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half)
                                {
                                    exps.BuyerAmount = Math.Ceiling(exps.TotalPrice / 2);
                                    exps.SellerAmount = Math.Ceiling(exps.TotalPrice / 2);
                                }
                                else
                                {
                                    exps.BuyerAmount = exps.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Customer ? exps.TotalPrice : 0;
                                    exps.SellerAmount = exps.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company ? exps.TotalPrice : 0;
                                }

                            }

                        }

                    }
                    #endregion

                    #region ค่ากองทุนแรกเข้า
                    if (itemY.MasterPriceItem?.Key == MasterPriceItemKeys.FirstSinkingFund)
                    {
                        if (titledeedDetail != null)
                        {
                            if (TitledeedArea > 0)
                            {
                                exps.Amount = (double)TitledeedArea;
                                exps.TotalPrice = Math.Ceiling((exps.PricePerUnit ?? 0) * TitledeedArea);

                                if (exps.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half)
                                {
                                    exps.BuyerAmount = Math.Ceiling(exps.TotalPrice / 2);
                                    exps.SellerAmount = Math.Ceiling(exps.TotalPrice / 2);
                                }
                                else
                                {
                                    exps.BuyerAmount = exps.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Customer ? exps.TotalPrice : 0;
                                    exps.SellerAmount = exps.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company ? exps.TotalPrice : 0;
                                }

                            }

                        }

                    }
                    #endregion

                    result.Add(exps);

                }

                #region ค่าจดจำนอง (กรณีกู้เกิน)

                #region กรณี มี Free

                var itemMortgageFee = result.Find(o => o.MasterPriceItem?.Key == MasterPriceItemKeys.MortgageFee);
                if (itemMortgageFee != null)
                {
                    decimal pricePerUnit_Mg = itemMortgageFee.PricePerUnit ?? 0;

                    if (loanAmount > pricePerUnit_Mg)
                    {
                        var itemMortgageFeeOverLoan = result.Find(o => o.MasterPriceItem?.Key == MasterPriceItemKeys.MortgageFeeOverLoan);
                        if (itemMortgageFeeOverLoan == null)
                        {
                            decimal PricePerUnit_Mgl = Math.Ceiling(loanAmount - pricePerUnit_Mg);

                            var priceItem = await DB.MasterPriceItems.Where(o => o.Key == MasterPriceItemKeys.MortgageFeeOverLoan).FirstOrDefaultAsync();
                            var priceUnit = await DB.MasterCenters.Where(o => o.Key == PriceUnitKeys.Percent).FirstOrDefaultAsync();
                            var expenseReponsibleBy = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Customer).FirstOrDefaultAsync();

                            var exps = new TransferExpenseListDTO();
                            exps.Transfer = new TransferDropdownDTO();
                            exps.MasterPriceItem = MasterPriceItemDTO.CreateFromModel(priceItem);
                            exps.ExpenseName = priceItem?.Detail;
                            exps.Amount = itemMortgageFee.Amount;
                            exps.PriceUnitName = MasterCenterDTO.CreateFromModel(priceUnit);
                            exps.PricePerUnit = PricePerUnit_Mgl;
                            exps.ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(expenseReponsibleBy);
                            exps.TotalPrice = Math.Ceiling(((decimal)(exps.Amount ?? 0) * ((exps.PricePerUnit ?? 0) / 100)));
                            exps.BuyerAmount = exps.TotalPrice;
                            exps.SellerAmount = 0;

                            result.Add(exps);

                        }
                        else
                        {

                            decimal PricePerUnit_Mgl_Db = itemMortgageFeeOverLoan.PricePerUnit ?? 0;
                            decimal PricePerUnit_Mgl = Math.Ceiling(loanAmount - pricePerUnit_Mg);

                            if (PricePerUnit_Mgl >= PricePerUnit_Mgl_Db)
                            {

                                var priceItem = await DB.MasterPriceItems.Where(o => o.Key == MasterPriceItemKeys.MortgageFeeOverLoan).FirstOrDefaultAsync();
                                var priceUnit = await DB.MasterCenters.Where(o => o.Key == PriceUnitKeys.Percent).FirstOrDefaultAsync();
                                var expenseReponsibleBy = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Customer).FirstOrDefaultAsync();

                                var exps = new TransferExpenseListDTO();
                                exps.Transfer = new TransferDropdownDTO();
                                exps.MasterPriceItem = MasterPriceItemDTO.CreateFromModel(priceItem);
                                exps.ExpenseName = priceItem?.Detail;
                                exps.Amount = itemMortgageFee.Amount;
                                exps.PriceUnitName = MasterCenterDTO.CreateFromModel(priceUnit);
                                exps.PricePerUnit = PricePerUnit_Mgl;
                                exps.ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(expenseReponsibleBy);
                                exps.TotalPrice = Math.Ceiling(((decimal)(exps.Amount ?? 0) * ((exps.PricePerUnit ?? 0) / 100)));
                                exps.BuyerAmount = exps.TotalPrice;
                                exps.SellerAmount = 0;

                                foreach (var a in result)
                                {
                                    if (a.MasterPriceItem?.Key == MasterPriceItemKeys.MortgageFeeOverLoan)
                                    {
                                        a.Transfer = exps.Transfer;
                                        a.MasterPriceItem = exps.MasterPriceItem;
                                        a.ExpenseName = exps.ExpenseName;
                                        a.Amount = exps.Amount;
                                        a.PriceUnitName = exps.PriceUnitName;
                                        a.PricePerUnit = exps.PricePerUnit;
                                        a.ExpenseReponsibleBy = exps.ExpenseReponsibleBy;
                                        a.TotalPrice = exps.TotalPrice;
                                        a.BuyerAmount = exps.BuyerAmount;
                                        a.SellerAmount = exps.SellerAmount;

                                    }
                                }

                            }
                            else
                            {
                                result.Remove(result.Find(o => o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan));
                            }

                        }
                    }
                    else
                    {
                        if (itemMortgageFee.ExpenseReponsibleBy.Key != ExpenseReponsibleByKeys.Company)
                        {
                            var itemMortgageFeeOverLoan = result.Find(o => o.MasterPriceItem?.Key == MasterPriceItemKeys.MortgageFeeOverLoan);
                            if (itemMortgageFeeOverLoan != null)
                            {
                                result.Remove(result.Find(o => o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan));
                            }
                        }
                    }

                }

                #endregion

                #endregion

                #region ค่าส่วนกลาง (ลูกค้าจ่าย)

                var itemCommonFeeChargeCom = result.Find(o =>
                        o.MasterPriceItem?.Key == MasterPriceItemKeys.CommonFeeCharge
                        && o.ExpenseReponsibleBy?.Key == ExpenseReponsibleByKeys.Company
                    );
                if (itemCommonFeeChargeCom != null)
                {
                    var AmountMst = agreementConfig.PublicFundMonths ?? 0;
                    var AmountTrn = itemCommonFeeChargeCom.Amount ?? 0;
                    var AmountCust = AmountMst - AmountTrn;

                    if (AmountCust > 0)
                    {
                        var itemCommonFeeChargeCust = result.Find(o =>
                                o.MasterPriceItem?.Key == MasterPriceItemKeys.CommonFeeCharge
                                && o.ExpenseReponsibleBy?.Key == ExpenseReponsibleByKeys.Customer
                            );
                        if (itemCommonFeeChargeCust == null)
                        {

                            var expenseReponsibleBy = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Customer).FirstOrDefaultAsync();

                            var exps = new TransferExpenseListDTO();
                            exps.Transfer = itemCommonFeeChargeCom.Transfer;
                            exps.MasterPriceItem = itemCommonFeeChargeCom.MasterPriceItem;
                            exps.ExpenseName = itemCommonFeeChargeCom.ExpenseName;
                            exps.Amount = AmountCust;
                            exps.PriceUnitName = itemCommonFeeChargeCom.PriceUnitName;
                            exps.PricePerUnit = itemCommonFeeChargeCom.PricePerUnit;
                            exps.ExpenseReponsibleBy = MasterCenterDTO.CreateFromModel(expenseReponsibleBy);
                            exps.TotalPrice = Math.Ceiling(((decimal)(exps.Amount ?? 0) * (exps.PricePerUnit ?? 0)));
                            exps.BuyerAmount = exps.TotalPrice;
                            exps.SellerAmount = 0;

                            result.Add(exps);

                        }

                    }
                }

                #endregion

                unitPriceItemModel = new List<UnitPriceItem>();
                listSaleExpense = new List<SalePromotionExpense>();
                listTransferExpense = new List<TransferPromotionExpense>();

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref TransferExpense model)
        {
            model = model ?? new TransferExpense();

            model.TransferID = this.Transfer.Id.Value;
            model.ExpenseReponsibleByMasterCenterID = this.ExpenseReponsibleBy?.Id;
            model.MasterPriceItemID = this.MasterPriceItem?.Id;
            model.Name = this.ExpenseName;
            model.PriceUnitAmount = this.Amount;
            model.PriceUnitMasterCenterID = this.PriceUnitName?.Id;
            model.PricePerUnitAmount = this.PricePerUnit;
            model.Amount = this.TotalPrice;
            model.SellerAmount = this.SellerAmount;
            model.BuyerAmount = this.BuyerAmount;
            model.PaymentReceiverMasterCenterID = this.MasterPriceItem?.PaymentReceiver?.Id;
        }
    }
}
