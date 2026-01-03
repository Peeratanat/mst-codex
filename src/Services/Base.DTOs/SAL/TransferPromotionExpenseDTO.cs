using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class TransferPromotionExpenseDTO : BaseDTO
    {
        /// <summary>
        /// ผู้รับผิดชอบ คชจ.
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ExpenseReponsibleBy
        /// </summary>
        public MST.MasterCenterDropdownDTO ExpenseReponsibleBy { get; set; }
        /// <summary>
        /// รายการ
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// จำนวน (หน่วย)
        /// </summary>
        public double? PriceUnitAmount { get; set; }
        /// <summary>
        /// หน่วย
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PriceUnit
        /// </summary>
        public MST.MasterCenterDropdownDTO PriceUnit { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย
        /// </summary>
        public decimal? PricePerUnitAmount { get; set; }
        /// <summary>
        /// ราคารวม
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// ลูกค้าจ่าย
        /// </summary>
        public decimal BuyerPayAmount { get; set; }
        /// <summary>
        /// บริษัทจ่าย
        /// </summary>
        public decimal SellerPayAmount { get; set; }
        /// <summary>
        /// รายการชำระเงิน
        /// </summary>
        public MST.MasterPriceItemDTO MasterPriceItem { get; set; }
        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// PaymentReceiverMasterCenter
        /// </summary>
        public Guid? PaymentReceiverMasterCenterID { get; set; }

        /// <summary>
        /// PriceListAmount ดึงมาจากหน้า Price List
        /// </summary>
        public decimal? PriceListAmount { get; set; }

        /// <summary>
        /// PriceList
        /// </summary>
        public PriceListDTO PriceList { get; set; }

        /// <summary>
        /// PriceListItem
        /// </summary>
        public PriceListItemDTO PriceListItem { get; set; }
        /// <summary>
        /// PriceListItem
        /// </summary>
        public CreditBankingDTO CreditBanking { get; set; }

        public async static Task<TransferPromotionExpenseDTO> CreateFromModelAsync(TransferPromotionExpense model, UnitPriceItem unitPriceItem, Guid unitPriceID, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new TransferPromotionExpenseDTO();
                result.Id = model.ID;
                result.ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(model.ExpenseReponsibleBy);
                result.Name = model.MasterPriceItem?.Detail;
                result.Amount = Math.Round(model.Amount);
                result.BuyerPayAmount = Math.Round(model.BuyerAmount);
                result.SellerPayAmount = Math.Round(model.SellerAmount);
                result.MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(model.MasterPriceItem);

                if (unitPriceItem != null)
                {
                    result.PriceUnitAmount = unitPriceItem?.PriceUnitAmount;
                    result.PricePerUnitAmount = unitPriceItem?.PricePerUnitAmount;
                    result.PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(unitPriceItem?.PriceUnit);
                    result.Order = unitPriceItem?.Order ?? 0;
                    result.PaymentReceiverMasterCenterID = unitPriceItem?.MasterPriceItem?.PaymentReceiverMasterCenterID;
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<List<TransferPromotionExpenseDTO>> CreateDraftFromUnitAsync(Guid? agreementId, DatabaseContext db)
        {
            var agreement = await db.Agreements
                        .Include(o => o.Unit)
                        .Include(o => o.Booking)
                        .Where(o => o.ID == agreementId).FirstOrDefaultAsync();


            var unit = await db.Units
                .Include(o => o.Project)
                .Include(o => o.Project.ProductType)
                .Include(o => o.WaterMeterPrice)
                .Where(o => o.ID == agreement.UnitID).FirstAsync();

            var masterPriceItems = await db.MasterPriceItems.ToListAsync();
            var priceUnitMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit).ToListAsync();
            var reponsibleByCustomerMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Customer).FirstAsync();
            var reponsibleByHalfMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Half).FirstAsync();
            var priceList = await db.GetActivePriceListAsync(unit.ID);
            var results = new List<TransferPromotionExpenseDTO>();
            var order = 0;

            switch (unit.Project.ProductType.Key)
            {
                case "1":
                    {
                        var waterMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemIDs.WaterMeter).First();
                        var electricMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.EletrictMeter);
                        var mortgageMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.MortgageFee);
                        var transferMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.TransferFee);
                        var commonMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.CommonFee);
                        var documentMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.DocumentFee);

                        #region Meter
                        if (unit.WaterMeterPriceID != null)
                        {
                            var water = new TransferPromotionExpenseDTO()
                            {
                                Name = waterMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (unit.WaterMeterPrice.WaterMeterPrice != null) ? unit.WaterMeterPrice.WaterMeterPrice : 0,
                                Amount = (unit.WaterMeterPrice.WaterMeterPrice != null) ? unit.WaterMeterPrice.WaterMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(waterMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                                PaymentReceiverMasterCenterID = waterMaster.PaymentReceiverMasterCenterID
                            };

                            results.Add(water);
                        }
                        else
                        {
                            var latestWater = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var water = new TransferPromotionExpenseDTO()
                            {
                                Name = waterMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (latestWater.WaterMeterPrice != null) ? latestWater.WaterMeterPrice : 0,
                                Amount = (latestWater.WaterMeterPrice != null) ? latestWater.WaterMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(waterMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                                PaymentReceiverMasterCenterID = waterMaster.PaymentReceiverMasterCenterID
                            };

                            results.Add(water);
                        }

                        if (unit.ElectricMeterPriceID != null)
                        {
                            var electric = new TransferPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice : 0,
                                Amount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                                PaymentReceiverMasterCenterID = electricMaster.PaymentReceiverMasterCenterID
                            };

                            results.Add(electric);
                        }
                        else
                        {
                            var latestElectric = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var electric = new TransferPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice : 0,
                                Amount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                                PaymentReceiverMasterCenterID = electricMaster.PaymentReceiverMasterCenterID
                            };

                            results.Add(electric);
                        }
                        #endregion

                        #region Mortgage
                        var netSellingPrice = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.NetSellPrice)?.Amount;
                        var mortgage = new TransferPromotionExpenseDTO()
                        {
                            Name = mortgageMaster.Detail,
                            PriceUnitAmount = 1,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                            PricePerUnitAmount = netSellingPrice,
                            Amount = (decimal)(netSellingPrice / 100),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                            PaymentReceiverMasterCenterID = mortgageMaster.PaymentReceiverMasterCenterID
                        };
                        results.Add(mortgage);
                        #endregion

                        #region Transfer
                        var titledeedArea = await db.TitledeedDetails.Where(o => o.UnitID == unit.ID).Select(o => o.TitledeedArea).FirstOrDefaultAsync();
                        var estimatePriceArea = await db.LowRiseFees.Where(o => o.UnitID == unit.ID).Select(o => o.EstimatePriceArea).FirstOrDefaultAsync();
                        var usedArea = unit.UsedArea;
                        var price = await db.LowRiseBuildingPriceFees.Where(o => o.UnitID == unit.ID).Select(o => o.Price).FirstOrDefaultAsync();
                        var bo = await db.BOConfigurations.FirstOrDefaultAsync();
                        var depreciationYear = 0M;
                        if (bo != null)
                        {
                            if (bo.DepreciationYear != null)
                            {
                                depreciationYear = (decimal)bo.DepreciationYear;
                            }
                        }

                        var titledeedCal = (decimal)((titledeedArea != null) ? titledeedArea : 0) * ((estimatePriceArea != null) ? estimatePriceArea : 0);
                        var usedAreaCal = (decimal)((usedArea != null) ? usedArea : 0) * ((price != null) ? price : 0);
                        var depreciationCal = ((depreciationYear != 0) ? depreciationYear / 100 : 0) * ((decimal)((usedArea != null) ? usedArea : 0) * ((price != null) ? price : 0));
                        var total = titledeedCal + usedAreaCal - depreciationCal;

                        var transfer = new TransferPromotionExpenseDTO()
                        {
                            Name = transferMaster.Detail,
                            PriceUnitAmount = 2,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                            PricePerUnitAmount = total.Value,
                            Amount = total.Value * 0.02M,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByHalfMasterCenters),
                            PaymentReceiverMasterCenterID = transferMaster.PaymentReceiverMasterCenterID
                        };

                        results.Add(transfer);
                        #endregion

                        #region Common
                        var agreeConfig = await db.AgreementConfigs.Where(o => o.ProjectID == unit.ProjectID).FirstOrDefaultAsync();

                        var common = new TransferPromotionExpenseDTO()
                        {
                            Name = commonMaster.Detail,
                            PriceUnitAmount = agreeConfig.PublicFundMonths,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "3")),
                            PricePerUnitAmount = agreeConfig.PublicFundRate,
                            Amount = agreeConfig.PublicFundRate.Value * agreeConfig.PublicFundMonths.Value,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(commonMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                            PaymentReceiverMasterCenterID = commonMaster.PaymentReceiverMasterCenterID
                        };
                        results.Add(common);
                        #endregion

                        #region Document
                        var document = new TransferPromotionExpenseDTO()
                        {
                            Name = documentMaster.Detail,
                            PriceUnitAmount = 1,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                            PricePerUnitAmount = 300,
                            Amount = 300,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(documentMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                            PaymentReceiverMasterCenterID = documentMaster.PaymentReceiverMasterCenterID
                        };
                        results.Add(document);
                        #endregion

                        break;
                    }
                case "2":
                    {
                        var electricMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.EletrictMeter);
                        var mortgageMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.MortgageFee);
                        var transferMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.TransferFee);
                        var commonMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.CommonFee);
                        var firstSinkingMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.FirstSinkingFund);
                        var documentMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.DocumentFee);

                        #region Meter
                        if (unit.ElectricMeterPriceID != null)
                        {
                            var electric = new TransferPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice : 0,
                                Amount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                                PaymentReceiverMasterCenterID = electricMaster.PaymentReceiverMasterCenterID
                            };

                            results.Add(electric);
                        }
                        else
                        {
                            var latestElectric = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var electric = new TransferPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice : 0,
                                Amount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                                PaymentReceiverMasterCenterID = electricMaster.PaymentReceiverMasterCenterID
                            };

                            results.Add(electric);
                        }
                        #endregion

                        #region Mortgage
                        var netSellingPrice = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.NetSellPrice)?.Amount;
                        if (netSellingPrice >= 1000000)
                        {
                            var mortgage = new TransferPromotionExpenseDTO()
                            {
                                Name = mortgageMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                                PricePerUnitAmount = netSellingPrice,
                                Amount = (decimal)(netSellingPrice / 100),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                                PaymentReceiverMasterCenterID = mortgageMaster.PaymentReceiverMasterCenterID
                            };
                            results.Add(mortgage);
                        }
                        else
                        {
                            var mortgage = new TransferPromotionExpenseDTO()
                            {
                                Name = mortgageMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                                PricePerUnitAmount = netSellingPrice,
                                Amount = (decimal)(netSellingPrice * (0.01M / 100)),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                                PaymentReceiverMasterCenterID = mortgageMaster.PaymentReceiverMasterCenterID
                            };
                            results.Add(mortgage);
                        }
                        #endregion

                        #region Transfer
                        var titledeedArea = await db.TitledeedDetails.Where(o => o.UnitID == unit.ID).Select(o => o.TitledeedArea).FirstOrDefaultAsync();
                        var estimatePriceArea = await db.LowRiseFees.Where(o => o.UnitID == unit.ID).Select(o => o.EstimatePriceArea).FirstOrDefaultAsync();
                        var usedArea = unit.UsedArea;
                        var price = await db.LowRiseBuildingPriceFees.Where(o => o.UnitID == unit.ID).Select(o => o.Price).FirstOrDefaultAsync();
                        var bo = await db.BOConfigurations.FirstOrDefaultAsync();
                        var depreciationYear = 0M;
                        if (bo != null)
                        {
                            if (bo.DepreciationYear != null)
                            {
                                depreciationYear = (decimal)bo.DepreciationYear;
                            }
                        }

                        var titledeedCal = (decimal)((titledeedArea != null) ? titledeedArea : 0) * ((estimatePriceArea != null) ? estimatePriceArea : 0);
                        var usedAreaCal = (decimal)((usedArea != null) ? usedArea : 0) * ((price != null) ? price : 0);
                        var depreciationCal = ((depreciationYear != 0) ? depreciationYear / 100 : 0) * ((decimal)((usedArea != null) ? usedArea : 0) * ((price != null) ? price : 0));
                        var total = titledeedCal + usedAreaCal - depreciationCal;

                        if (netSellingPrice >= 1000000)
                        {
                            var transfer = new TransferPromotionExpenseDTO()
                            {
                                Name = transferMaster.Detail,
                                PriceUnitAmount = 2,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                                PricePerUnitAmount = total.Value,
                                Amount = total.Value * 0.02M,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByHalfMasterCenters),
                                PaymentReceiverMasterCenterID = transferMaster.PaymentReceiverMasterCenterID
                            };

                            results.Add(transfer);
                        }
                        else
                        {
                            var transfer = new TransferPromotionExpenseDTO()
                            {
                                Name = transferMaster.Detail,
                                PriceUnitAmount = 2,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                                PricePerUnitAmount = total.Value,
                                Amount = total.Value * (0.01M / 100),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                                PaymentReceiverMasterCenterID = transferMaster.PaymentReceiverMasterCenterID
                            };

                            results.Add(transfer);
                        }
                        #endregion

                        #region Common
                        var agreeConfig = await db.AgreementConfigs.Where(o => o.ProjectID == unit.ProjectID).FirstOrDefaultAsync();

                        var common = new TransferPromotionExpenseDTO()
                        {
                            Name = commonMaster.Detail,
                            PriceUnitAmount = agreeConfig.PublicFundMonths,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "3")),
                            PricePerUnitAmount = agreeConfig.PublicFundRate,
                            Amount = agreeConfig.PublicFundRate.Value * agreeConfig.PublicFundMonths.Value,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(commonMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                            PaymentReceiverMasterCenterID = commonMaster.PaymentReceiverMasterCenterID
                        };
                        results.Add(common);
                        #endregion

                        #region Document
                        var document = new TransferPromotionExpenseDTO()
                        {
                            Name = documentMaster.Detail,
                            PriceUnitAmount = 1,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                            PricePerUnitAmount = 300,
                            Amount = 300,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(documentMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                            PaymentReceiverMasterCenterID = documentMaster.PaymentReceiverMasterCenterID
                        };
                        results.Add(document);
                        #endregion

                        #region First Sinking
                        var first = new TransferPromotionExpenseDTO()
                        {
                            Name = firstSinkingMaster.Detail,
                            PriceUnitAmount = ((usedArea != null) ? usedArea : 0),
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "4")),
                            PricePerUnitAmount = agreeConfig.CondoFundRate,
                            Amount = agreeConfig.CondoFundRate.Value * (decimal)((usedArea != null) ? usedArea : 0),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(firstSinkingMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters),
                            PaymentReceiverMasterCenterID = firstSinkingMaster.PaymentReceiverMasterCenterID
                        };
                        results.Add(first);
                        #endregion

                        break;
                    }
            }

            return (results.Count > 0) ? results : null;
        }

        public static TransferPromotionExpenseDTO CreateFromModel(SalePromotionExpense model, UnitPriceItem unitPriceItem, Guid unitPriceID, DatabaseContext db, DbQueryContext dbQuery)
        {
            if (model != null)
            {
                TransferPromotionExpenseDTO result = new TransferPromotionExpenseDTO();
                result.Id = model.ID;
                result.ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(model.ExpenseReponsibleBy);
                result.Name = model.MasterPriceItem.Detail;
                result.Amount = model.Amount;
                result.BuyerPayAmount = model.BuyerAmount;
                result.SellerPayAmount = model.SellerAmount;
                result.MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(model.MasterPriceItem);

                if (unitPriceItem != null)
                {
                    result.PriceUnitAmount = unitPriceItem?.PriceUnitAmount;
                    result.PricePerUnitAmount = unitPriceItem?.PricePerUnitAmount;
                    result.PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(unitPriceItem.PriceUnit);
                    result.Order = unitPriceItem.Order;
                }

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
