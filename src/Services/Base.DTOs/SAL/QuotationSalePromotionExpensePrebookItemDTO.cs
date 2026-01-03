using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// โปรก่อนขาย Expense
    /// </summary>
    public class QuotationSalePromotionExpensePrebookItemDTO : BaseDTO
    {
        //public Guid ID { get; set; }

        //public Guid QuotationID { get; set; }

        //public decimal Amount { get; set; }

        //public decimal SellerAmount { get; set; }

        //public decimal BuyerAmount { get; set; }

        public Guid? QuotationID { get; set; }

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
        public decimal BuyerAmount { get; set; }
        /// <summary>
        /// บริษัทจ่าย
        /// </summary>
        public decimal SellerAmount { get; set; }
        /// <summary>
        /// รายการชำระเงิน
        /// </summary>
        public MST.MasterPriceItemDTO MasterPriceItem { get; set; }
        /// <summary>
        /// ลำดับ
        /// </summary>
        public int? MasterPriceItemSeqNo { get; set; }

        public string PriceName { get; set; }

        public string MasterPriceItemDescription { get; set; }

        public string UnitName { get; set; }

        public decimal BuyerPayAmount { get; set; }
        public decimal SellerPayAmount { get; set; }

        public static QuotationPromotionExpenseDTO CreateFromModel(QuotationSalePromotionExpense model, Guid quotationUnitPriceID, DatabaseContext db)
        {
            if (model != null)
            {
                QuotationPromotionExpenseDTO result = new QuotationPromotionExpenseDTO()
                {
                    Id = model.ID,
                    ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(model.ExpenseReponsibleBy),
                    Name = model.MasterPriceItem.Detail,
                    Amount = Math.Ceiling(model.Amount),
                    BuyerPayAmount = Math.Ceiling(model.BuyerAmount),
                    SellerPayAmount = Math.Ceiling(model.SellerAmount),
                    MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(model.MasterPriceItem)
                };

                var item = db.QuotationUnitPriceItems.Include(o => o.PriceUnit).Where(o => o.QuotationUnitPriceID == quotationUnitPriceID && o.MasterPriceItemID == model.MasterPriceItemID).First();
                result.PriceUnitAmount = item.PriceUnitAmount;
                result.PricePerUnitAmount = item.PricePerUnitAmount;
                result.PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(item.PriceUnit);
                result.Order = item.Order;

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<List<QuotationPromotionExpenseDTO>> CreateDraftFromUnitAsync(Guid unitID, DatabaseContext db, decimal res)
        {
            var unit = await db.Units
                .Include(o => o.Project)
                .Include(o => o.Project.ProductType)
                .Include(o => o.WaterMeterPrice)
                .Include(o => o.ElectricMeterPrice)
                .Include(o => o.TitledeedDetails)
                .Where(o => o.ID == unitID).FirstAsync();

            var masterPriceItems = await db.MasterPriceItems.ToListAsync();
            var priceUnitMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit).ToListAsync();
            var reponsibleByCustomerMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Customer).FirstAsync();
            var reponsibleByHalfMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Half).FirstAsync();
            var priceList = await db.GetActivePriceListAsync(unitID);
            var results = new List<QuotationPromotionExpenseDTO>();
            var order = 0;

            switch (unit.Project.ProductType.Key)
            {
                ///แนวราบ
                case "1":
                    {
                        var waterMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.WaterMeter);
                        var electricMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.EletrictMeter);
                        var mortgageMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.MortgageFee);
                        var transferMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.TransferFee);
                        var commonMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.CommonFee);
                        var documentMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.DocumentFee);

                        #region Meter
                        if (unit.WaterMeterPriceID != null)
                        {
                            var water = new QuotationPromotionExpenseDTO()
                            {
                                Name = waterMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (unit.WaterMeterPrice.WaterMeterPrice != null) ? unit.WaterMeterPrice.WaterMeterPrice : 0,
                                Amount = (unit.WaterMeterPrice.WaterMeterPrice != null) ? Math.Ceiling(unit.WaterMeterPrice.WaterMeterPrice.Value) : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(waterMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };
                            if (water.ExpenseReponsibleBy.Key == "1")
                            {
                                water.BuyerPayAmount = water.Amount;
                                water.SellerPayAmount = 0;

                            }
                            else if (water.ExpenseReponsibleBy.Key == "2")
                            {
                                water.BuyerPayAmount = 0;
                                water.SellerPayAmount = water.Amount;
                            }
                            else
                            {
                                water.BuyerPayAmount = water.Amount / 2;
                                water.SellerPayAmount = water.Amount / 2;
                            }
                            results.Add(water);
                        }
                        else
                        {
                            var latestWater = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var water = new QuotationPromotionExpenseDTO()
                            {
                                Name = waterMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (latestWater.WaterMeterPrice != null) ? latestWater.WaterMeterPrice : 0,
                                Amount = Math.Ceiling((latestWater.WaterMeterPrice != null) ? latestWater.WaterMeterPrice.Value : 0),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(waterMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };
                            if (water.ExpenseReponsibleBy.Key == "1")
                            {
                                water.BuyerPayAmount = water.Amount;
                                water.SellerPayAmount = 0;

                            }
                            else if (water.ExpenseReponsibleBy.Key == "2")
                            {
                                water.BuyerPayAmount = 0;
                                water.SellerPayAmount = water.Amount;
                            }
                            else
                            {
                                water.BuyerPayAmount = water.Amount / 2;
                                water.SellerPayAmount = water.Amount / 2;
                            }
                            results.Add(water);
                        }

                        if (unit.ElectricMeterPriceID != null)
                        {
                            var electric = new QuotationPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice : 0,
                                Amount = Math.Ceiling((unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice.Value : 0),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };
                            if (electric.ExpenseReponsibleBy.Key == "1")
                            {
                                electric.BuyerPayAmount = electric.Amount;
                                electric.SellerPayAmount = 0;

                            }
                            else if (electric.ExpenseReponsibleBy.Key == "2")
                            {
                                electric.BuyerPayAmount = 0;
                                electric.SellerPayAmount = electric.Amount;
                            }
                            else
                            {
                                electric.BuyerPayAmount = electric.Amount / 2;
                                electric.SellerPayAmount = electric.Amount / 2;
                            }
                            results.Add(electric);
                        }
                        else
                        {
                            var latestElectric = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var electric = new QuotationPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice : 0,
                                Amount = Math.Ceiling((latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice.Value : 0),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };
                            if (electric.ExpenseReponsibleBy.Key == "1")
                            {
                                electric.BuyerPayAmount = electric.Amount;
                                electric.SellerPayAmount = 0;

                            }
                            else if (electric.ExpenseReponsibleBy.Key == "2")
                            {
                                electric.BuyerPayAmount = 0;
                                electric.SellerPayAmount = electric.Amount;
                            }
                            else
                            {
                                electric.BuyerPayAmount = electric.Amount / 2;
                                electric.SellerPayAmount = electric.Amount / 2;
                            }
                            results.Add(electric);
                        }
                        #endregion

                        #region Mortgage
                        var netSellingPrice = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice)?.Amount;
                        decimal amount = 0;
                        if ((netSellingPrice / 100) >= 200000)
                        {
                            amount = 200000;
                        }
                        else
                        {
                            amount = (decimal)(netSellingPrice / 100);
                        }
                        var mortgage = new QuotationPromotionExpenseDTO()
                        {
                            Name = mortgageMaster.Detail,
                            PriceUnitAmount = 1,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                            PricePerUnitAmount = netSellingPrice,
                            Amount = Math.Ceiling(amount),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        if (mortgage.ExpenseReponsibleBy.Key == "1")
                        {
                            mortgage.BuyerPayAmount = mortgage.Amount;
                            mortgage.SellerPayAmount = 0;

                        }
                        else if (mortgage.ExpenseReponsibleBy.Key == "2")
                        {
                            mortgage.BuyerPayAmount = 0;
                            mortgage.SellerPayAmount = mortgage.Amount;
                        }
                        else
                        {
                            mortgage.BuyerPayAmount = mortgage.Amount / 2;
                            mortgage.SellerPayAmount = mortgage.Amount / 2;
                        }
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

                        //var res = DbQuery.fnTS_GetBOConfigrate_TransferFeeRate.FromSql("SELECT [SAL].[fn_TSF_CALC_EstimatePriceBooking]('" + agreementID + "'," + NetSalePrice + "," + LoanPrice + ",'" + dateRate.ToString("yyyy-MM-dd") + "') as TransferFeeRate").FirstOrDefault() ?? null;


                        var transfer = new QuotationPromotionExpenseDTO()
                        {
                            Name = transferMaster.Detail,
                            PriceUnitAmount = 2,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                            PricePerUnitAmount = (res != 0 ? res : netSellingPrice),
                            Amount = Math.Ceiling((decimal)((res != 0 ? res : netSellingPrice) * 2 / 100)),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByHalfMasterCenters)
                        };
                        if (transfer.ExpenseReponsibleBy.Key == "1")
                        {
                            transfer.BuyerPayAmount = transfer.Amount;
                            transfer.SellerPayAmount = 0;

                        }
                        else if (transfer.ExpenseReponsibleBy.Key == "0")
                        {
                            transfer.BuyerPayAmount = 0;
                            transfer.SellerPayAmount = transfer.Amount;
                        }
                        else
                        {
                            transfer.BuyerPayAmount = Math.Ceiling(transfer.Amount / 2);
                            transfer.SellerPayAmount = 0;
                        }
                        results.Add(transfer);
                        #endregion

                        #region Common
                        var agreeConfig = await db.AgreementConfigs.Where(o => o.ProjectID == unit.ProjectID).FirstOrDefaultAsync();

                        var common = new QuotationPromotionExpenseDTO()
                        {
                            Name = commonMaster.Detail,
                            PriceUnitAmount = agreeConfig.PublicFundMonths,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "3")),
                            PricePerUnitAmount = agreeConfig.PublicFundRate * (decimal)(titledeedArea != null ? titledeedArea : unit.SaleArea),
                            Amount = Math.Ceiling(agreeConfig.PublicFundRate.Value * agreeConfig.PublicFundMonths.Value * (decimal)(titledeedArea != null ? titledeedArea : unit.SaleArea)),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(commonMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        if (common.ExpenseReponsibleBy.Key == "1")
                        {
                            common.BuyerPayAmount = common.Amount;
                            common.SellerPayAmount = 0;

                        }
                        else if (common.ExpenseReponsibleBy.Key == "2")
                        {
                            common.BuyerPayAmount = 0;
                            common.SellerPayAmount = common.Amount;
                        }
                        else
                        {
                            common.BuyerPayAmount = common.Amount / 2;
                            common.SellerPayAmount = common.Amount / 2;
                        }
                        results.Add(common);
                        #endregion

                        #region Document
                        var document = new QuotationPromotionExpenseDTO()
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
                        if (document.ExpenseReponsibleBy.Key == "1")
                        {
                            document.BuyerPayAmount = document.Amount;
                            document.SellerPayAmount = 0;

                        }
                        else if (document.ExpenseReponsibleBy.Key == "2")
                        {
                            document.BuyerPayAmount = 0;
                            document.SellerPayAmount = document.Amount;
                        }
                        else
                        {
                            document.BuyerPayAmount = document.Amount / 2;
                            document.SellerPayAmount = document.Amount / 2;
                        }
                        results.Add(document);
                        #endregion

                        break;
                    }
                /////แนวสูง
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
                            var electric = new QuotationPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice : 0,
                                Amount = Math.Ceiling((unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice.Value : 0),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)

                            };
                            if (electric.ExpenseReponsibleBy.Key == "1")
                            {
                                electric.BuyerPayAmount = electric.Amount;
                                electric.SellerPayAmount = 0;

                            }
                            else if (electric.ExpenseReponsibleBy.Key == "2")
                            {
                                electric.BuyerPayAmount = 0;
                                electric.SellerPayAmount = electric.Amount;
                            }
                            else
                            {
                                electric.BuyerPayAmount = electric.Amount / 2;
                                electric.SellerPayAmount = electric.Amount / 2;
                            }


                            results.Add(electric);
                        }
                        else
                        {
                            var latestElectric = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var electric = new QuotationPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "1")),
                                PricePerUnitAmount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice : 0,
                                Amount = Math.Ceiling((latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice.Value : 0),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };
                            if (electric.ExpenseReponsibleBy.Key == "1")
                            {
                                electric.BuyerPayAmount = electric.Amount;
                                electric.SellerPayAmount = 0;

                            }
                            else if (electric.ExpenseReponsibleBy.Key == "2")
                            {
                                electric.BuyerPayAmount = 0;
                                electric.SellerPayAmount = electric.Amount;
                            }
                            else
                            {
                                electric.BuyerPayAmount = electric.Amount / 2;
                                electric.SellerPayAmount = electric.Amount / 2;
                            }
                            results.Add(electric);
                        }
                        #endregion

                        #region Mortgage
                        var netSellingPrice = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice)?.Amount;
                        if (netSellingPrice >= 1000000)
                        {
                            var mortgage = new QuotationPromotionExpenseDTO()
                            {
                                Name = mortgageMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                                PricePerUnitAmount = netSellingPrice,
                                Amount = Math.Ceiling((decimal)(netSellingPrice / 100)),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };
                            if (mortgage.ExpenseReponsibleBy.Key == "1")
                            {
                                mortgage.BuyerPayAmount = mortgage.Amount;
                                mortgage.SellerPayAmount = 0;

                            }
                            else if (mortgage.ExpenseReponsibleBy.Key == "2")
                            {
                                mortgage.BuyerPayAmount = 0;
                                mortgage.SellerPayAmount = mortgage.Amount;
                            }
                            else
                            {
                                mortgage.BuyerPayAmount = mortgage.Amount / 2;
                                mortgage.SellerPayAmount = mortgage.Amount / 2;
                            }
                            results.Add(mortgage);
                        }
                        else
                        {
                            var mortgage = new QuotationPromotionExpenseDTO()
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
                            if (mortgage.ExpenseReponsibleBy.Key == "1")
                            {
                                mortgage.BuyerPayAmount = mortgage.Amount;
                                mortgage.SellerPayAmount = 0;

                            }
                            else if (mortgage.ExpenseReponsibleBy.Key == "2")
                            {
                                mortgage.BuyerPayAmount = 0;
                                mortgage.SellerPayAmount = mortgage.Amount;
                            }
                            else
                            {
                                mortgage.BuyerPayAmount = mortgage.Amount / 2;
                                mortgage.SellerPayAmount = mortgage.Amount / 2;
                            }
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

                        //if (netSellingPrice >= 1000000)
                        //{
                        //    var transfer = new QuotationPromotionExpenseDTO()
                        //    {
                        //        Name = transferMaster.Detail,
                        //        PriceUnitAmount = 2,
                        //        PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                        //        PricePerUnitAmount = (total.Value != 0 ? total.Value : netSellingPrice),
                        //        Amount = (total.Value != 0 ? total.Value : netSellingPrice) * 0.02M,
                        //        MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                        //        Order = order++,
                        //        ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByHalfMasterCenters)
                        //    };
                        //    if (transfer.ExpenseReponsibleBy.Key == "1")
                        //    {
                        //        transfer.BuyerPayAmount = transfer.Amount;
                        //        transfer.SellerPayAmount = 0;

                        //    }
                        //    else if (transfer.ExpenseReponsibleBy.Key == "0")
                        //    {
                        //        transfer.BuyerPayAmount = 0;
                        //        transfer.SellerPayAmount = transfer.Amount;
                        //    }
                        //    else
                        //    {
                        //        transfer.BuyerPayAmount = transfer.Amount / 2;
                        //        transfer.SellerPayAmount = transfer.Amount / 2;
                        //    }
                        //    results.Add(transfer);
                        //}
                        //else
                        //{
                        var transfer = new QuotationPromotionExpenseDTO()
                        {
                            Name = transferMaster.Detail,
                            PriceUnitAmount = 2,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "0")),
                            PricePerUnitAmount = res != 0 ? res : netSellingPrice,
                            Amount = Math.Ceiling((decimal)((res != 0 ? res : netSellingPrice) * (0.02M))),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByHalfMasterCenters)
                        };
                        if (transfer.ExpenseReponsibleBy.Key == "1")
                        {
                            transfer.BuyerPayAmount = transfer.Amount;
                            transfer.SellerPayAmount = 0;

                        }
                        else if (transfer.ExpenseReponsibleBy.Key == "0")
                        {
                            transfer.BuyerPayAmount = 0;
                            transfer.SellerPayAmount = transfer.Amount;
                        }
                        else
                        {
                            transfer.BuyerPayAmount = Math.Ceiling(transfer.Amount / 2);
                            transfer.SellerPayAmount = 0;
                        }
                        results.Add(transfer);
                        //}
                        #endregion

                        #region Common
                        var agreeConfig = await db.AgreementConfigs.Where(o => o.ProjectID == unit.ProjectID).FirstOrDefaultAsync();

                        var common = new QuotationPromotionExpenseDTO()
                        {
                            Name = commonMaster.Detail,
                            PriceUnitAmount = agreeConfig.PublicFundMonths,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "3")),
                            PricePerUnitAmount = agreeConfig.PublicFundRate * (decimal)(titledeedArea != null ? titledeedArea : unit.SaleArea),
                            Amount = Math.Ceiling(agreeConfig.PublicFundRate.Value * agreeConfig.PublicFundMonths.Value * (decimal)(titledeedArea != null ? titledeedArea : unit.SaleArea)),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(commonMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        if (common.ExpenseReponsibleBy.Key == "1")
                        {
                            common.BuyerPayAmount = common.Amount;
                            common.SellerPayAmount = 0;

                        }
                        else if (common.ExpenseReponsibleBy.Key == "2")
                        {
                            common.BuyerPayAmount = 0;
                            common.SellerPayAmount = common.Amount;
                        }
                        else
                        {
                            common.BuyerPayAmount = common.Amount / 2;
                            common.SellerPayAmount = common.Amount / 2;
                        }
                        results.Add(common);
                        #endregion

                        #region Document
                        var document = new QuotationPromotionExpenseDTO()
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
                        if (document.ExpenseReponsibleBy.Key == "1")
                        {
                            document.BuyerPayAmount = document.Amount;
                            document.SellerPayAmount = 0;

                        }
                        else if (document.ExpenseReponsibleBy.Key == "2")
                        {
                            document.BuyerPayAmount = 0;
                            document.SellerPayAmount = document.Amount;
                        }
                        else
                        {
                            document.BuyerPayAmount = document.Amount / 2;
                            document.SellerPayAmount = document.Amount / 2;
                        }
                        results.Add(document);
                        #endregion

                        #region First Sinking
                        var first = new QuotationPromotionExpenseDTO()
                        {
                            Name = firstSinkingMaster.Detail,
                            PriceUnitAmount = ((titledeedArea != null) ? titledeedArea : unit.SaleArea),
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "4")),
                            PricePerUnitAmount = agreeConfig.CondoFundRate,
                            Amount = Math.Ceiling(agreeConfig.CondoFundRate.Value * (decimal)((titledeedArea != null) ? titledeedArea : unit.SaleArea)),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(firstSinkingMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        if (first.ExpenseReponsibleBy.Key == "1")
                        {
                            first.BuyerPayAmount = first.Amount;
                            first.SellerPayAmount = 0;

                        }
                        else if (first.ExpenseReponsibleBy.Key == "2")
                        {
                            first.BuyerPayAmount = 0;
                            first.SellerPayAmount = first.Amount;
                        }
                        else
                        {
                            first.BuyerPayAmount = first.Amount / 2;
                            first.SellerPayAmount = first.Amount / 2;
                        }
                        results.Add(first);
                        #endregion

                        break;
                    }
            }

            return (results.Count > 0) ? results : null;
        }


        public async static Task<List<SalePromotionExpenseDTO>> CreateDraftFromUnitCommonAsync(Guid unitID, DatabaseContext db)
        {
            var unit = await db.Units
                .Include(o => o.Project)
                .Include(o => o.Project.ProductType)
                .Include(o => o.WaterMeterPrice)
                .Include(o => o.ElectricMeterPrice)
                .Include(o => o.TitledeedDetails)
                .Where(o => o.ID == unitID).FirstAsync();

            var masterPriceItems = await db.MasterPriceItems.ToListAsync();
            var priceUnitMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit).ToListAsync();
            var reponsibleByCustomerMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Customer).FirstAsync();
            var reponsibleByHalfMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Half).FirstAsync();
            var priceList = await db.GetActivePriceListAsync(unitID);
            var results = new List<SalePromotionExpenseDTO>();
            var order = 0;
            var titledeedArea = await db.TitledeedDetails.Where(o => o.UnitID == unit.ID).Select(o => o.TitledeedArea).FirstOrDefaultAsync();

            switch (unit.Project.ProductType.Key)
            {
                ///แนวราบ
                case "1":
                    {

                        var commonMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.CommonFee);
                        #region Common
                        var agreeConfig = await db.AgreementConfigs.Where(o => o.ProjectID == unit.ProjectID).FirstOrDefaultAsync();

                        var common = new SalePromotionExpenseDTO()
                        {
                            Name = commonMaster.Detail,
                            PriceUnitAmount = agreeConfig.PublicFundMonths,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "3")),
                            PricePerUnitAmount = agreeConfig.PublicFundRate * (decimal)(titledeedArea != null ? titledeedArea : unit.SaleArea),
                            Amount = Math.Ceiling(agreeConfig.PublicFundRate.Value * agreeConfig.PublicFundMonths.Value * (decimal)(titledeedArea != null ? titledeedArea : unit.SaleArea)),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(commonMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        if (common.ExpenseReponsibleBy.Key == "1")
                        {
                            common.BuyerPayAmount = common.Amount;
                            common.SellerPayAmount = 0;

                        }
                        else if (common.ExpenseReponsibleBy.Key == "2")
                        {
                            common.BuyerPayAmount = 0;
                            common.SellerPayAmount = common.Amount;
                        }
                        else
                        {
                            common.BuyerPayAmount = common.Amount / 2;
                            common.SellerPayAmount = common.Amount / 2;
                        }
                        results.Add(common);
                        #endregion

                        break;
                    }
                /////แนวสูง
                case "2":
                    {
                        var commonMaster = masterPriceItems.Find(o => o.ID == MasterPriceItemIDs.CommonFee);

                        #region Common
                        var agreeConfig = await db.AgreementConfigs.Where(o => o.ProjectID == unit.ProjectID).FirstOrDefaultAsync();

                        var common = new SalePromotionExpenseDTO()
                        {
                            Name = commonMaster.Detail,
                            PriceUnitAmount = agreeConfig.PublicFundMonths,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Find(o => o.Key == "3")),
                            PricePerUnitAmount = agreeConfig.PublicFundRate * (decimal)(titledeedArea != null ? titledeedArea : unit.SaleArea),
                            Amount = Math.Ceiling(agreeConfig.PublicFundRate.Value * agreeConfig.PublicFundMonths.Value * (decimal)(titledeedArea != null ? titledeedArea : unit.SaleArea)),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(commonMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        if (common.ExpenseReponsibleBy.Key == "1")
                        {
                            common.BuyerPayAmount = common.Amount;
                            common.SellerPayAmount = 0;

                        }
                        else if (common.ExpenseReponsibleBy.Key == "2")
                        {
                            common.BuyerPayAmount = 0;
                            common.SellerPayAmount = common.Amount;
                        }
                        else
                        {
                            common.BuyerPayAmount = common.Amount / 2;
                            common.SellerPayAmount = common.Amount / 2;
                        }
                        results.Add(common);
                        #endregion

                        break;
                    }
            }

            return (results.Count > 0) ? results : null;
        }
    }
}
