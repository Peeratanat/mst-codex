using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class UnitInfoSalePromotionExpenseDTO : BaseDTO
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

        public async static Task<List<UnitInfoSalePromotionExpenseDTO>> CreateDraftFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var unit = await db.Units
                .Include(o => o.Project)
                .Include(o => o.Project.ProductType)
                .Include(o => o.WaterMeterPrice)
                .Include(o => o.ElectricMeterPrice)
                .Where(o => o.ID == unitID).FirstOrDefaultAsync();

            var masterPriceItems = await db.MasterPriceItems.ToListAsync();
            var priceUnitMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit).ToListAsync();
            var reponsibleByCustomerMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Customer).FirstOrDefaultAsync();
            var reponsibleByHalfMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Half).FirstOrDefaultAsync();
            var priceList = await db.GetActivePriceListAsync(unitID);
            if (priceList == null)
                return null;

            var results = new List<UnitInfoSalePromotionExpenseDTO>();
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
                            var water = new UnitInfoSalePromotionExpenseDTO()
                            {
                                Name = waterMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (unit.WaterMeterPrice.WaterMeterPrice != null) ? unit.WaterMeterPrice.WaterMeterPrice : 0,
                                Amount = (unit.WaterMeterPrice.WaterMeterPrice != null) ? unit.WaterMeterPrice.WaterMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(waterMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(water);
                        }
                        else
                        {
                            var latestWater = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var water = new UnitInfoSalePromotionExpenseDTO()
                            {
                                Name = waterMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (latestWater.WaterMeterPrice != null) ? latestWater.WaterMeterPrice : 0,
                                Amount = (latestWater.WaterMeterPrice != null) ? latestWater.WaterMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(waterMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(water);
                        }

                        if (unit.ElectricMeterPriceID != null)
                        {
                            var electric = new UnitInfoSalePromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice : 0,
                                Amount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(electric);
                        }
                        else
                        {
                            var latestElectric = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var electric = new UnitInfoSalePromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice : 0,
                                Amount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(electric);
                        }
                        #endregion

                        #region Mortgage
                        var netSellingPrice = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice)?.Amount;
                        var mortgage = new UnitInfoSalePromotionExpenseDTO()
                        {
                            Name = mortgageMaster.Detail,
                            PriceUnitAmount = 1,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                            PricePerUnitAmount = netSellingPrice,
                            Amount = (decimal)(netSellingPrice / 100),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
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

                        var transfer = new UnitInfoSalePromotionExpenseDTO()
                        {
                            Name = transferMaster.Detail,
                            PriceUnitAmount = 2,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                            PricePerUnitAmount = total.Value,
                            Amount = total.Value * 0.02M,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByHalfMasterCenters)
                        };

                        results.Add(transfer);
                        #endregion

                        #region Common
                        var agreeConfig = await db.AgreementConfigs.Where(o => o.ProjectID == unit.ProjectID).FirstOrDefaultAsync();

                        var common = new UnitInfoSalePromotionExpenseDTO()
                        {
                            Name = commonMaster.Detail,
                            PriceUnitAmount = agreeConfig.PublicFundMonths,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "3")),
                            PricePerUnitAmount = agreeConfig.PublicFundRate,
                            Amount = agreeConfig.PublicFundRate.Value * agreeConfig.PublicFundMonths.Value,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(commonMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        results.Add(common);
                        #endregion

                        #region Document
                        var document = new UnitInfoSalePromotionExpenseDTO()
                        {
                            Name = documentMaster.Detail,
                            PriceUnitAmount = 1,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                            PricePerUnitAmount = 300,
                            Amount = 300,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(documentMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
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
                            var electric = new UnitInfoSalePromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice : 0,
                                Amount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(electric);
                        }
                        else
                        {
                            var latestElectric = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var electric = new UnitInfoSalePromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice : 0,
                                Amount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(electric);
                        }
                        #endregion

                        #region Mortgage
                        var netSellingPrice = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice)?.Amount;
                        if (netSellingPrice >= 1000000)
                        {
                            var mortgage = new UnitInfoSalePromotionExpenseDTO()
                            {
                                Name = mortgageMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                                PricePerUnitAmount = netSellingPrice,
                                Amount = (decimal)(netSellingPrice / 100),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };
                            results.Add(mortgage);
                        }
                        else
                        {
                            var mortgage = new UnitInfoSalePromotionExpenseDTO()
                            {
                                Name = mortgageMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                                PricePerUnitAmount = netSellingPrice,
                                Amount = (decimal)(netSellingPrice * (0.01M / 100)),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
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
                            var transfer = new UnitInfoSalePromotionExpenseDTO()
                            {
                                Name = transferMaster.Detail,
                                PriceUnitAmount = 2,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                                PricePerUnitAmount = total.Value,
                                Amount = total.Value * 0.02M,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByHalfMasterCenters)
                            };

                            results.Add(transfer);
                        }
                        else
                        {
                            var transfer = new UnitInfoSalePromotionExpenseDTO()
                            {
                                Name = transferMaster.Detail,
                                PriceUnitAmount = 2,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                                PricePerUnitAmount = total.Value,
                                Amount = total.Value * (0.01M / 100),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(transfer);
                        }
                        #endregion

                        #region Common
                        var agreeConfig = await db.AgreementConfigs.Where(o => o.ProjectID == unit.ProjectID).FirstOrDefaultAsync();

                        var common = new UnitInfoSalePromotionExpenseDTO()
                        {
                            Name = commonMaster.Detail,
                            PriceUnitAmount = agreeConfig.PublicFundMonths,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "3")),
                            PricePerUnitAmount = agreeConfig.PublicFundRate,
                            Amount = agreeConfig.PublicFundRate.Value * agreeConfig.PublicFundMonths.Value,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(commonMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        results.Add(common);
                        #endregion

                        #region First Sinking
                        var first = new UnitInfoSalePromotionExpenseDTO()
                        {
                            Name = firstSinkingMaster.Detail,
                            PriceUnitAmount = ((usedArea != null) ? usedArea : 0),
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "4")),
                            PricePerUnitAmount = agreeConfig.CondoFundRate,
                            Amount = agreeConfig.CondoFundRate.Value * (decimal)((usedArea != null) ? usedArea : 0),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(firstSinkingMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        results.Add(first);
                        #endregion

                        #region Document
                        var document = new UnitInfoSalePromotionExpenseDTO()
                        {
                            Name = documentMaster.Detail,
                            PriceUnitAmount = 1,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                            PricePerUnitAmount = 300,
                            Amount = 300,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(documentMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        results.Add(document);
                        #endregion



                        break;
                    }
            }

            return (results.Count > 0) ? results : null;
        }

        public async static Task<UnitInfoSalePromotionExpenseDTO> CreateFromModelAsync(SalePromotionExpense model, DatabaseContext db)
        {
            if (model != null)
            {
                var unitPrice = await db.UnitPrices.Where(o => o.BookingID == model.SalePromotion.BookingID).FirstOrDefaultAsync();
                var item = await db.UnitPriceItems.Include(o => o.PriceUnit).Where(o => o.UnitPriceID == unitPrice.ID && o.MasterPriceItemID == model.MasterPriceItemID).FirstOrDefaultAsync();
                UnitInfoSalePromotionExpenseDTO result = new UnitInfoSalePromotionExpenseDTO()
                {
                    Id = model.ID,
                    ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(model.ExpenseReponsibleBy),
                    Name = model.MasterPriceItem.Detail,
                    Amount = model.Amount,
                    BuyerPayAmount = model.BuyerAmount,
                    SellerPayAmount = model.SellerAmount,
                    MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(model.MasterPriceItem),
                    PriceUnitAmount = item.PriceUnitAmount,
                    PricePerUnitAmount = item.PricePerUnitAmount,
                    PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(item.PriceUnit),
                    Order = item.Order,
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<UnitInfoSalePromotionExpenseDTO> CreateFromModelAsync(TransferPromotionExpense model, DatabaseContext db)
        {
            if (model != null)
            {
                var unitPrice = await db.UnitPrices.Where(o => o.BookingID == model.TransferPromotion.BookingID).FirstOrDefaultAsync();
                var item = await db.UnitPriceItems.Include(o => o.PriceUnit).Where(o => o.UnitPriceID == unitPrice.ID && o.MasterPriceItemID == model.MasterPriceItemID).FirstOrDefaultAsync();
                UnitInfoSalePromotionExpenseDTO result = new UnitInfoSalePromotionExpenseDTO()
                {
                    Id = model.ID,
                    ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(model.ExpenseReponsibleBy),
                    Name = model.MasterPriceItem.Detail,
                    Amount = model.Amount,
                    BuyerPayAmount = model.BuyerAmount,
                    SellerPayAmount = model.SellerAmount,
                    MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(model.MasterPriceItem),
                    PriceUnitAmount = item.PriceUnitAmount,
                    PricePerUnitAmount = item.PricePerUnitAmount,
                    PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(item.PriceUnit),
                    Order = item.Order,
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
