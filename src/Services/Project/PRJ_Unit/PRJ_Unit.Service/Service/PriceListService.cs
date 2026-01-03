using Base.DTOs;
using Base.DTOs.PRJ;
using Common;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.LOG;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using ErrorHandling;
using ExcelExtensions;
using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using PRJ_Unit.Services.Excels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FileStorage;
namespace PRJ_Unit.Services
{
	public class PriceListService : IPriceListService
	{
		public LogModel logModel { get; set; }
		private readonly DatabaseContext DB;
		private readonly IConfiguration Configuration;
		private FileHelper FileHelper;

		public PriceListService(DatabaseContext db)
		{
			logModel = new LogModel("PriceListService", null);
			this.DB = db;

			var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
			var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
			var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
			var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
			var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
			var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
			var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

			this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
		}
		public PriceListService(IConfiguration configuration, DatabaseContext db)
		{
			this.DB = db;
			logModel = new LogModel("PriceListService", null);
			this.Configuration = configuration;

			var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
			var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
			var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
			var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
			var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
			var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
			var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

			this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
		}

		public async Task<PriceListPaging> GetPriceListsAsync(Guid projectID, PriceListFilter filter, PageParam pageParam, PriceListSortByParam sortByParam, CancellationToken cancellationToken = default)
		{
			var allUnits = await DB.Units.AsNoTracking().Where(o => o.ProjectID == projectID).Include(o => o.UnitStatus).ToListAsync(cancellationToken);
			var allUnitID = allUnits.Where(o => o.ProjectID == projectID).Select(o => o.ID).ToList();
			var allPriceList = await DB.PriceLists.AsNoTracking()
							  .Include(o => o.UpdatedBy)
							  .Where(o => allUnitID.Contains(o.UnitID)).OrderByDescending(o => o.Created)
							  .GroupBy(o => o.UnitID)
							  .Select(o => o.Select(p => p).OrderByDescending(p => p.Created).FirstOrDefault()
							 //.Select(o => o.Where(p => p.ActiveDate <= DateTime.Now).Select(p => p).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
							 ).ToListAsync(cancellationToken);
			var allPriceListIsnotnull = allPriceList.Where(o => o != null).ToList();
			var allPriceListID = allPriceListIsnotnull.Where(o => allUnitID.Contains(o.UnitID)).Select(o => o.ID).ToList();
			var allPriceListItem = await DB.PriceListItems.AsNoTracking().Where(o => allPriceListID.Contains(o.PriceListID))
														  .Include(o => o.MasterPriceItem)
														  .Include(o => o.PriceUnit)
														  .Include(o => o.PriceType)
														  .Include(o => o.UpdatedBy)
														  .ToListAsync(cancellationToken);

			var allTitleDeed = await DB.TitledeedDetails.AsNoTracking().Where(o => o.ProjectID == projectID && o.UnitID != null).GroupBy(o => o.UnitID)
														.Select(o => o.OrderByDescending(o => o.Created).First()).ToListAsync(cancellationToken);

			bool isNew = false;
			if (allPriceListID.Count > 0)
			{
				isNew = false;
			}
			else
			{
				isNew = true;
			}
			var data = GetPriceList(isNew, projectID, allUnits, allPriceListIsnotnull, allTitleDeed, allPriceListItem);
			data = data.OrderBy(x => x.Unit.SAPWBSNo).ToList();
			var results = data.Select(o => PriceListDTO.CreateFromQueryResult(o)).ToList();

			#region Fitler
			if (!string.IsNullOrEmpty(filter.UnitNo))
			{
				results = results.Where(x => (x.UnitNo == null ? string.Empty : x.UnitNo.ToLower()).Contains(filter.UnitNo.ToLower())).ToList();
			}

			#region SaleArea
			if (filter.SaleAreaFrom != null)
			{
				results = results.Where(x => x.SaleArea >= filter.SaleAreaFrom).ToList();
			}
			if (filter.SaleAreaTo != null)
			{
				results = results.Where(x => x.SaleArea <= filter.SaleAreaTo).ToList();
			}
			if (filter.SaleAreaFrom != null && filter.SaleAreaTo != null)
			{
				results = results.Where(x => x.SaleArea >= filter.SaleAreaFrom && x.SaleArea <= filter.SaleAreaTo).ToList();
			}
			#endregion

			#region TitleDeedArea
			if (filter.TitleDeedAreaFrom != null)
			{
				results = results.Where(x => x.TitleDeedArea >= filter.TitleDeedAreaFrom).ToList();
			}
			if (filter.TitleDeedAreaTo != null)
			{
				results = results.Where(x => x.TitleDeedArea <= filter.TitleDeedAreaTo).ToList();
			}
			if (filter.TitleDeedAreaFrom != null && filter.TitleDeedAreaTo != null)
			{
				results = results.Where(x => x.TitleDeedArea >= filter.TitleDeedAreaFrom && x.TitleDeedArea <= filter.TitleDeedAreaTo).ToList();
			}
			#endregion

			#region OffsetArea
			if (filter.OffsetAreaFrom != null)
			{
				results = results.Where(x => x.OffsetArea >= filter.OffsetAreaFrom).ToList();
			}
			if (filter.OffsetAreaTo != null)
			{
				results = results.Where(x => x.OffsetArea <= filter.OffsetAreaTo).ToList();
			}
			if (filter.OffsetAreaFrom != null && filter.OffsetAreaTo != null)
			{
				results = results.Where(x => x.OffsetArea >= filter.OffsetAreaFrom && x.OffsetArea <= filter.OffsetAreaTo).ToList();
			}
			#endregion

			#region OffsetAreaUnitPrice
			if (filter.OffsetAreaUnitPriceFrom != null)
			{
				results = results.Where(x => x.OffsetAreaUnitPrice >= filter.OffsetAreaUnitPriceFrom).ToList();
			}
			if (filter.OffsetAreaUnitPriceTo != null)
			{
				results = results.Where(x => x.OffsetAreaUnitPrice <= filter.OffsetAreaUnitPriceTo).ToList();
			}
			if (filter.OffsetAreaUnitPriceFrom != null && filter.OffsetAreaUnitPriceTo != null)
			{
				results = results.Where(x => x.OffsetAreaUnitPrice >= filter.OffsetAreaUnitPriceFrom && x.OffsetAreaUnitPrice <= filter.OffsetAreaUnitPriceTo).ToList();
			}
			#endregion

			#region OffsetAreaPrice
			if (filter.OffsetAreaPriceFrom != null)
			{
				results = results.Where(x => x.OffsetAreaPrice >= filter.OffsetAreaPriceFrom).ToList();
			}
			if (filter.OffsetAreaPriceTo != null)
			{
				results = results.Where(x => x.OffsetAreaPrice <= filter.OffsetAreaPriceTo).ToList();
			}
			if (filter.OffsetAreaPriceFrom != null && filter.OffsetAreaPriceTo != null)
			{
				results = results.Where(x => x.OffsetAreaPrice >= filter.OffsetAreaPriceFrom && x.OffsetAreaPrice <= filter.OffsetAreaPriceTo).ToList();
			}
			#endregion

			#region TotalSalePrice
			if (filter.TotalSalePriceFrom != null)
			{
				results = results.Where(x => x.TotalSalePrice >= filter.TotalSalePriceFrom).ToList();
			}
			if (filter.TotalSalePriceTo != null)
			{
				results = results.Where(x => x.TotalSalePrice <= filter.TotalSalePriceTo).ToList();
			}
			if (filter.TotalSalePriceFrom != null && filter.TotalSalePriceTo != null)
			{
				results = results.Where(x => x.TotalSalePrice >= filter.TotalSalePriceFrom && x.TotalSalePrice <= filter.TotalSalePriceTo).ToList();
			}
			#endregion

			#region PercentDownPayment
			if (filter.PercentDownPaymentFrom != null)
			{
				results = results.Where(x => x.PercentDownPayment >= filter.PercentDownPaymentFrom).ToList();
			}
			if (filter.PercentDownPaymentTo != null)
			{
				results = results.Where(x => x.PercentDownPayment <= filter.PercentDownPaymentTo).ToList();
			}
			if (filter.PercentDownPaymentFrom != null && filter.PercentDownPaymentTo != null)
			{
				results = results.Where(x => x.PercentDownPayment >= filter.PercentDownPaymentFrom && x.PercentDownPayment <= filter.PercentDownPaymentTo).ToList();
			}
			#endregion

			#region BookingAmount
			if (filter.BookingAmountFrom != null)
			{
				results = results.Where(x => x.BookingAmount >= filter.BookingAmountFrom).ToList();
			}
			if (filter.BookingAmountTo != null)
			{
				results = results.Where(x => x.BookingAmount <= filter.BookingAmountTo).ToList();
			}
			if (filter.BookingAmountFrom != null && filter.BookingAmountTo != null)
			{
				results = results.Where(x => x.BookingAmount >= filter.BookingAmountFrom && x.BookingAmount <= filter.BookingAmountTo).ToList();
			}
			#endregion

			#region ContractAmount
			if (filter.ContractAmountFrom != null)
			{
				results = results.Where(x => x.ContractAmount >= filter.ContractAmountFrom).ToList();
			}
			if (filter.ContractAmountTo != null)
			{
				results = results.Where(x => x.ContractAmount <= filter.ContractAmountTo).ToList();
			}
			if (filter.ContractAmountFrom != null && filter.ContractAmountTo != null)
			{
				results = results.Where(x => x.ContractAmount >= filter.ContractAmountFrom
									&& x.ContractAmount <= filter.ContractAmountTo).ToList();
			}
			#endregion

			#region DownPaymentPeriod
			if (filter.DownPaymentPeriodFrom != null)
			{
				results = results.Where(x => x.DownAmount >= Convert.ToDecimal(filter.DownPaymentPeriodFrom)).ToList();
			}
			if (filter.DownPaymentPeriodTo != null)
			{
				results = results.Where(x => x.DownAmount <= Convert.ToDecimal(filter.DownPaymentPeriodTo)).ToList();
			}
			if (filter.DownPaymentPeriodFrom != null && filter.DownPaymentPeriodTo != null)
			{
				results = results.Where(x => x.DownAmount >= Convert.ToDecimal(filter.DownPaymentPeriodFrom)
									&& x.DownAmount <= Convert.ToDecimal(filter.DownPaymentPeriodTo)).ToList();
			}
			#endregion

			#region DownPaymentPerPeriod
			if (filter.DownPaymentPerPeriodFrom != null)
			{
				results = results.Where(x => x.DownPaymentPerPeriod >= filter.DownPaymentPerPeriodFrom).ToList();
			}
			if (filter.DownPaymentPerPeriodTo != null)
			{
				results = results.Where(x => x.DownPaymentPerPeriod <= filter.DownPaymentPerPeriodTo).ToList();
			}
			if (filter.DownPaymentPerPeriodFrom != null && filter.DownPaymentPerPeriodTo != null)
			{
				results = results.Where(x => x.DownPaymentPerPeriod >= filter.DownPaymentPerPeriodFrom
									&& x.DownPaymentPerPeriod <= filter.DownPaymentPerPeriodTo).ToList();
			}
			#endregion

			#region DownAmount
			if (filter.DownAmountFrom != null)
			{
				results = results.Where(x => x.DownAmount >= filter.DownAmountFrom).ToList();
			}
			if (filter.DownAmountTo != null)
			{
				results = results.Where(x => x.DownAmount <= filter.DownAmountTo).ToList();
			}
			if (filter.DownAmountFrom != null && filter.DownAmountTo != null)
			{
				results = results.Where(x => x.DownAmount >= filter.DownAmountFrom && x.DownAmount <= filter.DownAmountTo).ToList();
			}
			#endregion
			if (!string.IsNullOrEmpty(filter.SpecialDown))
			{
				results = results.Where(x => x.SpecialDown != null && x.SpecialDown.Contains(filter.SpecialDown)).ToList();
			}

			#region SpecialDownPrice

			if (!string.IsNullOrEmpty(filter.SpecialDownPrice))
			{
				results = results.Where(x => x.SpecialDownPrice != null && x.SpecialDownPrice.Contains(filter.SpecialDownPrice)).ToList();
			}

			#endregion

			if (!string.IsNullOrEmpty(filter.UnitStatusKey))
			{
				var unitStatusMasterCenterID = (await DB.MasterCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Key == filter.UnitStatusKey
																		   && x.MasterCenterGroupKey == "UnitStatus")
																		  )?.ID;
				if (unitStatusMasterCenterID is not null)
					results = results.Where(x => x.UnitStatus?.Id == unitStatusMasterCenterID).ToList();
			}

			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				results = results.Where(x => x.UpdatedBy.Contains(filter.UpdatedBy)).ToList();
			}
			if (filter.UpdatedFrom != null)
			{
				results = results.Where(x => x.Updated >= filter.UpdatedFrom).ToList();
			}
			if (filter.UpdatedTo != null)
			{
				results = results.Where(x => x.Updated <= filter.UpdatedTo).ToList();
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				results = results.Where(x => x.Updated >= filter.UpdatedFrom && x.Updated <= filter.UpdatedTo).ToList();
			}

			#endregion

			PriceListDTO.SortBy(sortByParam, ref results);

			var pageOutput = PagingHelper.PagingList<PriceListDTO>(pageParam, ref results);

			return new PriceListPaging()
			{
				PageOutput = pageOutput,
				PriceLists = results
			};
		}

		public async Task<PriceListDTO> CreatePriceListAsync(Guid projectID, PriceListDTO input)
		{
			var unit = await DB.Units.FirstOrDefaultAsync(o => o.UnitNo == input.UnitNo && o.ProjectID == projectID);
			List<PriceListItem> listPriceListItem = new List<PriceListItem>();

			PriceList pl = new PriceList();
			pl.UnitID = unit.ID;
			pl.ActiveDate = DateTime.Now;

			var priceUnitPercentMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0"))?.ID;
			var priceUnitItemMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "2"))?.ID;
			var priceTypeHouseMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "0"))?.ID;

            #region PriceListItemSellPrice
            PriceListItem sellpricelistitem = new PriceListItem
            {
                PriceListID = pl.ID,
                Name = "ราคาขาย",
                Order = 1,
                MasterPriceItemID = MasterPriceItemIDs.SellPrice,
                Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney()),
                PriceUnitAmount = 1,
                PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney()),
                PriceUnitMasterCenterID = priceUnitItemMasterCenterID,
                PriceTypeMasterCenterID = priceTypeHouseMasterCenterID
            };
            listPriceListItem.Add(sellpricelistitem);
            #endregion

            #region PriceListItemNetSell
            PriceListItem netselllistitem = new PriceListItem
            {
                PriceListID = pl.ID,
                Name = "ราคาขายสุทธิ",
                Order = 2,
                MasterPriceItemID = MasterPriceItemIDs.NetSellPrice,
                Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney()),
                PriceUnitAmount = 1,
                PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney()),
                PriceUnitMasterCenterID = priceUnitItemMasterCenterID,
                PriceTypeMasterCenterID = priceTypeHouseMasterCenterID
            };

            listPriceListItem.Add(netselllistitem);
            #endregion

            #region PriceListItemBooking
            PriceListItem bookingpricelistitem = new PriceListItem
            {
                PriceListID = pl.ID,
                Name = "เงินจอง",
                Order = 3,
                MasterPriceItemID = MasterPriceItemIDs.BookingAmount,
                Amount = Decimal.Round(Convert.ToDecimal(input.BookingAmount).RoundMoney()),
                PriceUnitAmount = Math.Round(Convert.ToDouble(input.BookingAmount / input.TotalSalePrice)),
                PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney()),
                PriceUnitMasterCenterID = priceUnitPercentMasterCenterID,
                PriceTypeMasterCenterID = priceTypeHouseMasterCenterID
            };
            listPriceListItem.Add(bookingpricelistitem);
            #endregion

            #region PriceListItemContract
            PriceListItem contractpricelistitem = new PriceListItem
            {
                PriceListID = pl.ID,
                Name = "เงินสัญญา",
                Order = 4,
                MasterPriceItemID = MasterPriceItemIDs.ContractAmount,
                Amount = Decimal.Round(Convert.ToDecimal(input.ContractAmount).RoundMoney()),
                PriceUnitAmount = Math.Round(Convert.ToDouble(input.ContractAmount / input.TotalSalePrice)),
                PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney()),
                PriceUnitMasterCenterID = priceUnitPercentMasterCenterID,
                PriceTypeMasterCenterID = priceTypeHouseMasterCenterID
            };
            listPriceListItem.Add(contractpricelistitem);
            #endregion

            #region PriceListItemDownPrice
            PriceListItem downpricelistitem = new PriceListItem
            {
                PriceListID = pl.ID,
                Name = "เงินดาวน์",
                Installment = Convert.ToInt32(input.DownPaymentPeriod)
            };
            if (downpricelistitem.Installment == 0)
			{
				downpricelistitem.Installment = null;
			}
			downpricelistitem.Order = 5;
			downpricelistitem.MasterPriceItemID = MasterPriceItemIDs.DownAmount;

			if (input.PercentDownPayment != 0 && input.PercentDownPayment != null)
			{

				downpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(((input.TotalSalePrice * Convert.ToDecimal(input.PercentDownPayment)) / 100) - input.BookingAmount - input.ContractAmount).RoundMoney());
				downpricelistitem.PriceUnitAmount = input.PercentDownPayment / 100;
				downpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
				downpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
				downpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
				if (!string.IsNullOrEmpty(input.SpecialDown) && !string.IsNullOrEmpty(input.SpecialDownPrice))
				{
					var specialDownPrice = input.SpecialDownPrice.Split(',').Select(o => Decimal.Round(Convert.ToDecimal(o).RoundMoney())).ToList();
					decimal normalAmount = downpricelistitem.Amount;
					foreach (var item in specialDownPrice)
					{
						normalAmount -= item;
					}

					var normalInstallmentAmount = normalAmount / (downpricelistitem.Installment - specialDownPrice.Count());
					downpricelistitem.InstallmentAmount = Decimal.Round(normalInstallmentAmount.Value.RoundMoney());
					downpricelistitem.SpecialInstallments = input.SpecialDown;
					downpricelistitem.SpecialInstallmentAmounts = string.Join(",", specialDownPrice);
				}
				else
				{
					downpricelistitem.InstallmentAmount = Decimal.Round(Convert.ToDecimal(downpricelistitem.Amount / downpricelistitem.Installment).RoundMoney());
				}
			}
			else
			{
				downpricelistitem.Amount = 0;
			}

			listPriceListItem.Add(downpricelistitem);
			#endregion

			#region PriceListItemEXTRAAREAPRICE
			PriceListItem extrapricelistitem = new PriceListItem();
			extrapricelistitem.PriceListID = pl.ID;
			extrapricelistitem.Name = "ราคาพื้นที่เพิ่มลด";
			extrapricelistitem.Order = 6;
			extrapricelistitem.MasterPriceItemID = MasterPriceItemIDs.ExtraAreaPrice;

			extrapricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
			extrapricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice) / Convert.ToDecimal(unit.SaleArea));
			extrapricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
			extrapricelistitem.PriceUnitAmount = unit.SaleArea;

			listPriceListItem.Add(extrapricelistitem);

			#endregion

			await DB.PriceLists.AddAsync(pl);
			await DB.PriceListItems.AddRangeAsync(listPriceListItem);
			await DB.SaveChangesAsync();

			var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
			var priceListDataStatusMasterCenterID = await this.PriceListDataStatus(projectID);
			project.PriceListDataStatusMasterCenterID = priceListDataStatusMasterCenterID;
			await DB.SaveChangesAsync();

			var result = await this.GetPriceListAsync(projectID, pl.ID);
			return result;
		}

		public async Task<PriceListDTO> UpdatePriceListAsync(Guid projectID, Guid id2, PriceListDTO input)
		{
			var unit = await DB.Units.Include(o => o.UnitStatus).FirstOrDefaultAsync(x => x.UnitNo == input.UnitNo && x.ProjectID == projectID);
			var project = await DB.Projects.Where(o => o.ID == projectID).Include(o => o.ProductType).FirstAsync();
			var masterpricelistitem = await DB.PriceListItems.FirstOrDefaultAsync(o => o.PriceListID == input.Id && o.Order == 6);

			List<PriceListItem> listPriceListItem = new List<PriceListItem>();

			PriceList pl = new PriceList();
			pl.UnitID = unit.ID;
			pl.ActiveDate = DateTime.Now;

			var priceUnitPercentMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0"))?.ID;
			var priceUnitItemMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "2"))?.ID;
			var priceTypeHouseMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "0"))?.ID;

			#region PriceListItemSellPrice
			PriceListItem sellpricelistitem = new PriceListItem();
			sellpricelistitem.PriceListID = pl.ID;
			sellpricelistitem.Name = "ราคาขาย";
			sellpricelistitem.Order = 1;
			sellpricelistitem.MasterPriceItemID = MasterPriceItemIDs.SellPrice;
			sellpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
			sellpricelistitem.PriceUnitAmount = 1;
			sellpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
			sellpricelistitem.PriceUnitMasterCenterID = priceUnitItemMasterCenterID;
			sellpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
			listPriceListItem.Add(sellpricelistitem);
			#endregion

			#region PriceListItemNetSell
			PriceListItem netselllistitem = new PriceListItem();
			netselllistitem.PriceListID = pl.ID;
			netselllistitem.Name = "ราคาขายสุทธิ";
			netselllistitem.Order = 2;
			netselllistitem.MasterPriceItemID = MasterPriceItemIDs.NetSellPrice;
			netselllistitem.Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
			netselllistitem.PriceUnitAmount = 1;
			netselllistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
			netselllistitem.PriceUnitMasterCenterID = priceUnitItemMasterCenterID;
			netselllistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;

			listPriceListItem.Add(netselllistitem);
			#endregion

			#region PriceListItemBooking
			PriceListItem bookingpricelistitem = new PriceListItem();
			bookingpricelistitem.PriceListID = pl.ID;
			bookingpricelistitem.Name = "เงินจอง";
			bookingpricelistitem.Order = 3;
			bookingpricelistitem.MasterPriceItemID = MasterPriceItemIDs.BookingAmount;
			bookingpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.BookingAmount).RoundMoney());
			bookingpricelistitem.PriceUnitAmount = Math.Round(Convert.ToDouble(input.BookingAmount / input.TotalSalePrice));
			bookingpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
			bookingpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
			bookingpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
			listPriceListItem.Add(bookingpricelistitem);
			#endregion

			#region PriceListItemContract
			PriceListItem contractpricelistitem = new PriceListItem();
			contractpricelistitem.PriceListID = pl.ID;
			contractpricelistitem.Name = "เงินสัญญา";
			contractpricelistitem.Order = 4;
			contractpricelistitem.MasterPriceItemID = MasterPriceItemIDs.ContractAmount;
			contractpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.ContractAmount).RoundMoney());
			contractpricelistitem.PriceUnitAmount = Math.Round(Convert.ToDouble(input.ContractAmount / input.TotalSalePrice));
			contractpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
			contractpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
			contractpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
			listPriceListItem.Add(contractpricelistitem);
			#endregion

			#region PriceListItemDownPrice
			PriceListItem downpricelistitem = new PriceListItem();
			downpricelistitem.PriceListID = pl.ID;
			downpricelistitem.Name = "เงินดาวน์";
			downpricelistitem.Installment = Convert.ToInt32(input.DownPaymentPeriod);
			if (downpricelistitem.Installment == 0)
			{
				downpricelistitem.Installment = null;
			}
			downpricelistitem.Order = 5;
			downpricelistitem.MasterPriceItemID = MasterPriceItemIDs.DownAmount;

			if (input.PercentDownPayment != 0 && input.PercentDownPayment != null)
			{

				downpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(((input.TotalSalePrice * Convert.ToDecimal(input.PercentDownPayment)) / 100) - input.BookingAmount - input.ContractAmount).RoundMoney());
				downpricelistitem.PriceUnitAmount = input.PercentDownPayment / 100;
				downpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
				downpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
				downpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
				if (!string.IsNullOrEmpty(input.SpecialDown) && !string.IsNullOrEmpty(input.SpecialDownPrice))
				{
					var specialDownPrice = input.SpecialDownPrice.Split(',').Select(o => Decimal.Round(Convert.ToDecimal(o).RoundMoney())).ToList();
					decimal normalAmount = downpricelistitem.Amount;
					foreach (var item in specialDownPrice)
					{
						normalAmount -= item;
					}

					var normalInstallmentAmount = normalAmount / (downpricelistitem.Installment - specialDownPrice.Count());
					downpricelistitem.InstallmentAmount = Decimal.Round(normalInstallmentAmount.Value.RoundMoney());
					downpricelistitem.SpecialInstallments = input.SpecialDown;
					downpricelistitem.SpecialInstallmentAmounts = string.Join(",", specialDownPrice);
				}
				else
				{
					downpricelistitem.InstallmentAmount = Decimal.Round(Convert.ToDecimal(downpricelistitem.Amount / downpricelistitem.Installment).RoundMoney());
				}
			}
			else
			{
				downpricelistitem.Amount = 0;
			}

			listPriceListItem.Add(downpricelistitem);
			#endregion

			#region PriceListItemEXTRAAREAPRICE
			PriceListItem extrapricelistitem = new PriceListItem();
			extrapricelistitem.PriceListID = pl.ID;
			extrapricelistitem.Name = "ราคาพื้นที่เพิ่มลด";
			extrapricelistitem.Order = 6;
			extrapricelistitem.MasterPriceItemID = MasterPriceItemIDs.ExtraAreaPrice;

			extrapricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());

			if (project.ProductType?.Key == "1") //ราบ
			{
				extrapricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(masterpricelistitem.PricePerUnitAmount ?? 0));
			}
			else if (project.ProductType?.Key == "2") //แนวสูง
			{
				if (unit?.UnitStatus?.Key == UnitStatusKeys.Available) //รอจอง
				{
					double? areaCal = input.TitleDeedArea != null ? input.TitleDeedArea : unit.SaleArea;
					extrapricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice) / Convert.ToDecimal(areaCal));
				}
				else
				{
					extrapricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice) / Convert.ToDecimal(unit.SaleArea));
				}
			}
			extrapricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
			extrapricelistitem.PriceUnitAmount = unit.SaleArea;

			listPriceListItem.Add(extrapricelistitem);

			#endregion

			await DB.PriceLists.AddAsync(pl);
			await DB.PriceListItems.AddRangeAsync(listPriceListItem);
			await DB.SaveChangesAsync();


			var priceListDataStatusMasterCenterID = await this.PriceListDataStatus(projectID);
			project.PriceListDataStatusMasterCenterID = priceListDataStatusMasterCenterID;
			await DB.SaveChangesAsync();

			var result = await this.GetPriceListAsync(projectID, pl.ID);
			return result;
		}

		public async Task DeletePriceListAsync(Guid id)
		{
			var model = await DB.PriceLists.FindAsync(id);
			model.IsDeleted = true;
			await DB.SaveChangesAsync();
		}

		public async Task<PriceListDTO> GetPriceListAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default)
		{
			var allUnitID = await DB.Units.AsNoTracking().Where(o => o.ProjectID == projectID).Select(o => o.ID).ToListAsync(cancellationToken);
			var allUnits = await DB.Units.AsNoTracking().Where(o => o.ProjectID == projectID).Include(o => o.UnitStatus).ToListAsync(cancellationToken);
			var allPriceList = await DB.PriceLists.AsNoTracking().Include(o => o.UpdatedBy).Where(o => allUnitID.Contains(o.UnitID))
												  .GroupBy(o => o.UnitID).Select(o => o.OrderByDescending(o => o.Created).FirstOrDefault()).ToListAsync(cancellationToken);
			var allPriceListID = await DB.PriceLists.AsNoTracking().Where(o => allUnitID.Contains(o.UnitID)).Select(o => o.ID).ToListAsync(cancellationToken);
			var allPriceListItem = await DB.PriceListItems.AsNoTracking().Where(o => allPriceListID.Contains(o.PriceListID)).Include(o => o.MasterPriceItem).ToListAsync(cancellationToken);
			var allTitleDeed = await DB.TitledeedDetails.AsNoTracking().Where(o => o.ProjectID == projectID).GroupBy(o => o.UnitID).Select(o => o.OrderByDescending(o => o.Created).First()).ToListAsync(cancellationToken);

			var tempdata = (from unit in allUnits
							join priceList in allPriceList on unit.ID equals priceList.UnitID
							where priceList.ID == id
							select new TempPriceListQueryResult
							{
								Unit = unit,
								PriceList = priceList

							}).ToList();

			tempdata = tempdata.GroupBy(o => o.Unit).Select(o => new TempPriceListQueryResult
			{
				Unit = o.Key,
				Titledeed = allTitleDeed.Find(p => p.UnitID == o.Key.ID),
				//PriceList = o.Where(p => p.PriceList.ActiveDate <= DateTime.Now).Select(p => p.PriceList).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
				PriceList = o.Select(p => p.PriceList).OrderByDescending(p => p.Created).FirstOrDefault(),
			}).ToList();

			tempdata.ForEach(o => o.PriceListItems = allPriceListItem.Where(p => p.PriceListID == o.PriceList.ID)
													.Select(p => PriceListItemDTO.CreateFromModel(p))
													.OrderBy(p => p.Order)
													.ToList());

			var data = tempdata.Select(x => new PriceListQueryResult
			{
				Unit = x.Unit,
				TitleDeedArea = x.Titledeed?.TitledeedArea,
				BookingAmount = x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.BookingAmount),
				TotalSalePrice = x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.NetSellPrice),
				ContractAmount = x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.ContractAmount),
				DownAmount = x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount),
				SpecialDown = x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount).SpecialInstallments,
				SpecialDownPrice = x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount).SpecialInstallmentAmounts,
				DownPaymentPerPeriod = x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount).InstallmentAmount,
				PercentDownPayment = x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount).PriceUnitAmount == 0 ? null
						: x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount).PriceUnitAmount * 100,
				OffsetAreaUnitPrice = x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.ExtraAreaPrice).PricePerUnitAmount,
				OffsetArea = (x.Titledeed?.TitledeedArea - x.Unit?.SaleArea) == 0 ? null : (x.Titledeed?.TitledeedArea - x.Unit?.SaleArea),
				OffsetAreaPrice = (Convert.ToDecimal(x.Titledeed?.TitledeedArea - x.Unit?.SaleArea) * x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.ExtraAreaPrice).PricePerUnitAmount) == 0 ? null
				: Convert.ToDecimal(x.Titledeed?.TitledeedArea - x.Unit?.SaleArea) * x.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.ExtraAreaPrice).PricePerUnitAmount,
				PriceList = x.PriceList,
				UnitStatus = x.Unit.UnitStatus
			}).ToList();

			var result = data.Select(o => PriceListDTO.CreateFromQueryResult(o)).FirstOrDefault();
			return result;
		}
		public async Task<PriceListExcelDTO> ImportProjectPriceListAsyncTemp(Guid projectID, FileDTO input)
		{
			// Require
			var err0061 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0061");
			// Not Found
			var err0062 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0062");
			// Decimal 2 Digit
			var err0065 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0065");
			// Integer
			var err0069 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0069");
			//Format Date
			var err0071 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0071");
			// Format Comma
			var err0085 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0085");
			//
			var err0174 = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0174");

			var units = await DB.Units.Where(o => o.ProjectID == projectID).Include(o => o.Project.ProductType)
																		   .Select(o => new { o.ID, o.UnitNo, o.ProjectID, o.SAPWBSNo, o.SaleArea, o.Created, o.Project.ProductType, o.Project })
																		   .OrderBy(o => o.SAPWBSNo).ToListAsync();
			var titleDeeds = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID).Select(o => new { o.TitledeedArea, o.UnitID }).ToListAsync();
			var priceUnitPercentMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "PriceUnit" && o.Key == "0")).ID;
			var priceUnitItemMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "PriceUnit" && o.Key == "2")).ID;
			var priceTypeHouseMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "PriceType" && o.Key == "0")).ID;
			var costMinprices = await DB.MinPrices.Where(o => o.ProjectID == projectID).Select(o => new { o.Cost, o.UnitID, o.Created }).ToListAsync();

			var dt = await this.ConvertExcelToDataTable(input);
			/// Valudate Header
			if (dt.Columns.Count != 14)
			{
				throw new Exception("Invalid File Format");
			}

			var row = 2;
			var error = 0;
			//Read Excel Model

			#region Validate
			var checkNullWBSCodes = new List<string>();
			var checkNullUnitNos = new List<string>();
			var chcekNullSalePrices = new List<string>();
			var checkNullAreaPricePerUnits = new List<string>();
			var checkNullLocationPrices = new List<string>();
			var checkNullBookingAmounts = new List<string>();
			var checkNullContractAmounts = new List<string>();
			var checkNullDownPercents = new List<string>();
			var checkNullDownInstallments = new List<string>();
			var checkNullSpecialDownInstallmens = new List<string>();
			var checkNullSpecialDownInstallmentAmounts = new List<string>();
			var checkNullDocumentDates = new List<string>();

			var chcekFormatSalePrices = new List<string>();
			var checkFormatAreaPricePerUnits = new List<string>();
			var checkFormatLocationPrices = new List<string>();
			var checkFormatBookingAmounts = new List<string>();
			var checkFormatContractAmounts = new List<string>();
			var checkFormatDownPercents = new List<string>();
			var checkFormatDownInstallments = new List<string>();
			var checkFormatSpecialDownInstallmens = new List<string>();
			var checkFormatSpecialDownInstallmentAmounts = new List<string>();
			var checkFormatDocumentDates = new List<string>();

			var checkUnitNotFounds = new List<string>();
			var checkBelowSalePrices = new List<string>();
			#endregion

			var priceListExcelModels = new List<PriceListExcelModel>();
			foreach (DataRow r in dt.Rows)
			{
				var isError = false;
				var excelModel = PriceListExcelModel.CreateFromDataRow(r);
				if (string.IsNullOrEmpty(r[0]?.ToString())) //ProjectNo is NullorEmp
				{
					continue;
				}

				priceListExcelModels.Add(excelModel);

				#region Validate
				if (string.IsNullOrEmpty(excelModel.WBSCode))
				{
					checkNullWBSCodes.Add((row).ToString());
					isError = true;
				}
				if (string.IsNullOrEmpty(excelModel.UnitNo))
				{
					checkNullUnitNos.Add((row).ToString());
					isError = true;
				}
				else
				{
					var unit = units.Find(o => o.SAPWBSNo == excelModel.WBSCode && o.UnitNo == excelModel.UnitNo);
					if (unit == null)
					{
						checkUnitNotFounds.Add((row).ToString());
						isError = true;
					}
				}

				if (string.IsNullOrEmpty(r[PriceListExcelModel._sellPriceIndex].ToString()))
				{
					chcekNullSalePrices.Add((row).ToString());
					isError = true;
				}
				else
				{
					if (!r[PriceListExcelModel._sellPriceIndex].ToString().IsOnlyNumberWithMaxDigit(2))
					{
						chcekFormatSalePrices.Add((row).ToString());
						isError = true;
					}
					//CheckPriceList Below Minprice
					var unit = units.Where(o => o.SAPWBSNo == excelModel.WBSCode && o.UnitNo == excelModel.UnitNo && o.ProjectID == projectID).OrderByDescending(o => o.Created).FirstOrDefault();
					if (unit != null)
					{
						var costMinprice = costMinprices.Where(o => o.UnitID == unit.ID).OrderByDescending(o => o.Created).FirstOrDefault();
						if (costMinprice != null)
						{
							if (Convert.ToInt64(excelModel.SellPrice) < Convert.ToInt64(costMinprice.Cost))
							{
								checkBelowSalePrices.Add((row).ToString());
								isError = true;
							}
						}
					}
				}

				//if (string.IsNullOrEmpty(r[PriceListExcelModel._areaPricePerUnitIndex].ToString()))
				//{
				//	checkNullAreaPricePerUnits.Add((row).ToString());
				//	isError = true;
				//}
				//else
				//{
				//	if (!r[PriceListExcelModel._areaPricePerUnitIndex].ToString().IsOnlyNumberWithMaxDigit(2))
				//	{
				//		checkFormatAreaPricePerUnits.Add((row).ToString());
				//		isError = true;
				//	}
				//}

				//if (string.IsNullOrEmpty(r[PriceListExcelModel._locationPriceIndex].ToString()))
				//{
				//	checkNullLocationPrices.Add((row).ToString());
				//	isError = true;
				//}
				//else
				//{
				//	if (!r[PriceListExcelModel._locationPriceIndex].ToString().IsOnlyNumberWithMaxDigit(2))
				//	{
				//		checkFormatLocationPrices.Add((row).ToString());
				//		isError = true;
				//	}
				//}

				if (string.IsNullOrEmpty(r[PriceListExcelModel._bookingAmountIndex].ToString()))
				{
					checkNullBookingAmounts.Add((row).ToString());
					isError = true;
				}
				else
				{
					if (!r[PriceListExcelModel._bookingAmountIndex].ToString().IsOnlyNumberWithMaxDigit(2))
					{
						checkFormatBookingAmounts.Add((row).ToString());
						isError = true;
					}
				}

				if (string.IsNullOrEmpty(r[PriceListExcelModel._contractAmountIndex].ToString()))
				{
					checkNullContractAmounts.Add((row).ToString());
					isError = true;
				}
				else
				{
					if (!r[PriceListExcelModel._contractAmountIndex].ToString().IsOnlyNumberWithMaxDigit(2))
					{
						checkFormatContractAmounts.Add((row).ToString());
						isError = true;
					}
				}

				//if (string.IsNullOrEmpty(r[PriceListExcelModel._downPercentIndex].ToString()))
				//{
				//	//checkNullDownPercents.Add((row).ToString());
				//	//isError = true;
				//}
				//else
				//{
				//	if (!r[PriceListExcelModel._downPercentIndex].ToString().IsOnlyNumberWithMaxDigit(2))
				//	{
				//		checkFormatDownPercents.Add((row).ToString());
				//		isError = true;
				//	}
				//}

				//if (string.IsNullOrEmpty(r[PriceListExcelModel._downInstallmentIndex].ToString()))
				//{
				//	//checkNullDownInstallments.Add((row).ToString());
				//	//isError = true;
				//}
				//else
				//{
				//	if (!r[PriceListExcelModel._downInstallmentIndex].ToString().IsOnlyNumber())
				//	{
				//		checkFormatDownInstallments.Add((row).ToString());
				//		isError = true;
				//	}
				//}

				//if (string.IsNullOrEmpty(r[PriceListExcelModel._specialDownInstallmensIndex].ToString()))
				//{
				//	//checkNullSpecialDownInstallmens.Add((row).ToString());
				//	//isError = true;
				//}
				//else
				//{
				//	if (!r[PriceListExcelModel._specialDownInstallmensIndex].ToString().IsOnlyNumberWithSpecialCharacter(false, ","))
				//	{
				//		checkFormatSpecialDownInstallmens.Add((row).ToString());
				//		isError = true;
				//	}
				//}
				//if (string.IsNullOrEmpty(r[PriceListExcelModel._specialDownInstallmentAmountsIndex].ToString()))
				//{
				//	//checkFormatSpecialDownInstallmentAmounts.Add((row).ToString());
				//	//isError = true;
				//}
				//else
				//{
				//	if (!r[PriceListExcelModel._specialDownInstallmentAmountsIndex].ToString().IsOnlyNumberWithSpecialCharacter(false, ",."))
				//	{
				//		checkFormatSpecialDownInstallmentAmounts.Add((row).ToString());
				//		isError = true;
				//	}
				//}
				//if (string.IsNullOrEmpty(r[PriceListExcelModel._documentDateIndex].ToString()))
				//{
				//	//checkNullSpecialDownInstallmentAmounts.Add((row).ToString());
				//	//isError = true;
				//}
				//else
				//{
				//	if (!r[PriceListExcelModel._documentDateIndex].ToString().isFormatDateNew())
				//	{
				//		checkFormatDocumentDates.Add((row).ToString());
				//		isError = true;
				//	}
				//}

				if (isError)
				{
					error++;
				}
				#endregion

				row++;
			}

			if (checkBelowSalePrices.Any())
			{
				ValidateException vex = new ValidateException();
				var msg = "ราคาขาย ห้ามต่ำกว่าราคา Minprice";
				vex.AddError("", msg, 1);

				throw vex;
			}

			PriceListExcelDTO result = new PriceListExcelDTO { Error = 0, Success = 0, ErrorMessages = new List<string>() };

			var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
			ValidateException ex = new ValidateException();

			if (priceListExcelModels.Where(o => !string.IsNullOrEmpty(o.ProjectNo)).Any(o => o.ProjectNo != project.ProjectNo))
			{
				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
				var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ");
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}
			if (ex.HasError)
			{
				throw ex;
			}

			#region Add Validate Result

			#region Required
			if (checkNullWBSCodes.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "WBSCODE");
					msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "WBSCODE");
					msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkNullUnitNos.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "เลขที่แปลง");
					msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "เลขที่แปลง");
					msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
					result.ErrorMessages.Add(msg);
				}
			}
			if (chcekNullSalePrices.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "ราคาขาย");
					msg = msg.Replace("[row]", String.Join(",", chcekNullSalePrices));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "ราคาขาย");
					msg = msg.Replace("[row]", String.Join(",", chcekNullSalePrices));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkNullAreaPricePerUnits.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "ราคาพื้นที่ต่อหน่วย");
					msg = msg.Replace("[row]", String.Join(",", checkNullAreaPricePerUnits));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "ราคาพื้นที่ต่อหน่วย");
					msg = msg.Replace("[row]", String.Join(",", checkNullAreaPricePerUnits));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkNullLocationPrices.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "ค่าทำเล");
					msg = msg.Replace("[row]", String.Join(",", checkNullLocationPrices));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "ค่าทำเล");
					msg = msg.Replace("[row]", String.Join(",", checkNullLocationPrices));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkNullBookingAmounts.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "เงินจอง");
					msg = msg.Replace("[row]", String.Join(",", checkNullBookingAmounts));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "เงินจอง");
					msg = msg.Replace("[row]", String.Join(",", checkNullBookingAmounts));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkNullContractAmounts.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "เงินทำสัญญา");
					msg = msg.Replace("[row]", String.Join(",", checkNullContractAmounts));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "เงินทำสัญญา");
					msg = msg.Replace("[row]", String.Join(",", checkNullContractAmounts));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkNullDownPercents.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "%เงินดาวน์");
					msg = msg.Replace("[row]", String.Join(",", checkNullDownPercents));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "%เงินดาวน์");
					msg = msg.Replace("[row]", String.Join(",", checkNullDownPercents));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkNullDownInstallments.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "งวดดาวน์");
					msg = msg.Replace("[row]", String.Join(",", checkNullDownInstallments));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "งวดดาวน์");
					msg = msg.Replace("[row]", String.Join(",", checkNullDownInstallments));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkNullSpecialDownInstallmens.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "งวดดาวน์พิเศษ");
					msg = msg.Replace("[row]", String.Join(",", checkNullSpecialDownInstallmens));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "งวดดาวน์พิเศษ");
					msg = msg.Replace("[row]", String.Join(",", checkNullSpecialDownInstallmens));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkNullSpecialDownInstallmentAmounts.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "เงินดาวน์พิเศษ");
					msg = msg.Replace("[row]", String.Join(",", checkNullSpecialDownInstallmentAmounts));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "เงินดาวน์พิเศษ");
					msg = msg.Replace("[row]", String.Join(",", checkNullSpecialDownInstallmentAmounts));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkNullDocumentDates.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "วันที่ Version เอกสาร");
					msg = msg.Replace("[row]", String.Join(",", checkNullDocumentDates));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "วันที่ Version เอกสาร");
					msg = msg.Replace("[row]", String.Join(",", checkNullDocumentDates));
					result.ErrorMessages.Add(msg);
				}
			}
			#endregion

			#region NotFounds
			if (checkUnitNotFounds.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
					msg = msg.Replace("[row]", String.Join(",", checkUnitNotFounds));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
					msg = msg.Replace("[row]", String.Join(",", checkUnitNotFounds));
					result.ErrorMessages.Add(msg);
				}
			}
			#endregion

			#region Format Decimal 2 Digit
			if (chcekFormatSalePrices.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0065.Message.Replace("[column]", "ราคาขาย");
					msg = msg.Replace("[row]", String.Join(",", chcekFormatSalePrices));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0065.Message.Replace("[column]", "ราคาขาย");
					msg = msg.Replace("[row]", String.Join(",", chcekFormatSalePrices));
					result.ErrorMessages.Add(msg);
				}
			}


			if (checkFormatAreaPricePerUnits.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0065.Message.Replace("[column]", "ราคาพื้นที่ต่อหน่วย");
					msg = msg.Replace("[row]", String.Join(",", checkFormatAreaPricePerUnits));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0065.Message.Replace("[column]", "ราคาพื้นที่ต่อหน่วย");
					msg = msg.Replace("[row]", String.Join(",", checkFormatAreaPricePerUnits));
					result.ErrorMessages.Add(msg);
				}
			}

			if (checkFormatLocationPrices.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0065.Message.Replace("[column]", "ค่าทำเล");
					msg = msg.Replace("[row]", String.Join(",", checkFormatLocationPrices));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0065.Message.Replace("[column]", "ค่าทำเล");
					msg = msg.Replace("[row]", String.Join(",", checkFormatLocationPrices));
					result.ErrorMessages.Add(msg);
				}
			}

			if (checkFormatBookingAmounts.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0065.Message.Replace("[column]", "เงินจอง");
					msg = msg.Replace("[row]", String.Join(",", checkFormatBookingAmounts));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0065.Message.Replace("[column]", "เงินจอง");
					msg = msg.Replace("[row]", String.Join(",", checkFormatBookingAmounts));
					result.ErrorMessages.Add(msg);
				}
			}

			if (checkFormatContractAmounts.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0065.Message.Replace("[column]", "เงินทำสัญญา");
					msg = msg.Replace("[row]", String.Join(",", checkFormatContractAmounts));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0065.Message.Replace("[column]", "เงินทำสัญญา");
					msg = msg.Replace("[row]", String.Join(",", checkFormatContractAmounts));
					result.ErrorMessages.Add(msg);
				}
			}

			if (checkFormatDownPercents.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0065.Message.Replace("[column]", "%เงินดาวน์");
					msg = msg.Replace("[row]", String.Join(",", checkFormatDownPercents));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0065.Message.Replace("[column]", "%เงินดาวน์");
					msg = msg.Replace("[row]", String.Join(",", checkFormatDownPercents));
					result.ErrorMessages.Add(msg);
				}
			}
			#endregion

			#region Format Comma
			if (checkFormatSpecialDownInstallmens.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0085.Message.Replace("[column]", "งวดดาวน์พิเศษ");
					msg = msg.Replace("[row]", String.Join(",", checkFormatSpecialDownInstallmens));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0085.Message.Replace("[column]", "งวดดาวน์พิเศษ");
					msg = msg.Replace("[row]", String.Join(",", checkFormatSpecialDownInstallmens));
					result.ErrorMessages.Add(msg);
				}
			}
			if (checkFormatSpecialDownInstallmentAmounts.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0085.Message.Replace("[column]", "เงินดาวน์พิเศษ");
					msg = msg.Replace("[row]", String.Join(",", checkFormatSpecialDownInstallmentAmounts));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0085.Message.Replace("[column]", "เงินดาวน์พิเศษ");
					msg = msg.Replace("[row]", String.Join(",", checkFormatSpecialDownInstallmentAmounts));
					result.ErrorMessages.Add(msg);
				}
			}
			#endregion

			#region Integer
			if (checkFormatDownInstallments.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0069.Message.Replace("[column]", "งวดดาวน์");
					msg = msg.Replace("[row]", String.Join(",", checkFormatDownInstallments));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0069.Message.Replace("[column]", "งวดดาวน์");
					msg = msg.Replace("[row]", String.Join(",", checkFormatDownInstallments));
					result.ErrorMessages.Add(msg);
				}
			}
			#endregion

			#region Format Date
			if (checkFormatDocumentDates.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0071.Message.Replace("[column]", "วันที่ Version เอกสาร");
					msg = msg.Replace("[row]", String.Join(",", checkFormatDocumentDates));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0071.Message.Replace("[column]", "วันที่ Version เอกสาร");
					msg = msg.Replace("[row]", String.Join(",", checkFormatDocumentDates));
					result.ErrorMessages.Add(msg);
				}
			}
			#endregion

			#region CheckBelowMinPrice
			if (checkBelowSalePrices.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0174.Message.Replace("[column]", "ราคาขาย");
					msg = msg.Replace("[row]", String.Join(",", checkBelowSalePrices));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0174.Message.Replace("[column]", "ราคาขาย");
					msg = msg.Replace("[row]", String.Join(",", checkBelowSalePrices));
					result.ErrorMessages.Add(msg);
				}
			}
			#endregion

			#region rowError
			var rowErrors = new List<string>();
			rowErrors.AddRange(checkNullWBSCodes);
			rowErrors.AddRange(checkNullUnitNos);
			rowErrors.AddRange(chcekNullSalePrices);
			rowErrors.AddRange(checkNullAreaPricePerUnits);
			rowErrors.AddRange(checkNullLocationPrices);
			rowErrors.AddRange(checkNullBookingAmounts);
			rowErrors.AddRange(checkNullContractAmounts);
			rowErrors.AddRange(checkNullDownPercents);
			rowErrors.AddRange(checkNullDownInstallments);
			rowErrors.AddRange(checkNullSpecialDownInstallmens);
			rowErrors.AddRange(checkNullSpecialDownInstallmentAmounts);
			rowErrors.AddRange(checkNullDocumentDates);
			rowErrors.AddRange(checkUnitNotFounds);
			rowErrors.AddRange(chcekFormatSalePrices);
			rowErrors.AddRange(checkFormatAreaPricePerUnits);
			rowErrors.AddRange(checkFormatLocationPrices);
			rowErrors.AddRange(checkFormatBookingAmounts);
			rowErrors.AddRange(checkFormatContractAmounts);
			rowErrors.AddRange(checkFormatDownPercents);
			rowErrors.AddRange(checkFormatDownInstallments);
			rowErrors.AddRange(checkFormatSpecialDownInstallmens);
			rowErrors.AddRange(checkFormatSpecialDownInstallmentAmounts);
			rowErrors.AddRange(checkFormatDocumentDates);
			rowErrors.AddRange(checkBelowSalePrices);
			#endregion

			#endregion
			var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
			row = 2;
			//Update Data
			List<PriceList> listPriceList = new List<PriceList>();
			List<PriceListItem> listPriceListItem = new List<PriceListItem>();

			foreach (var item in priceListExcelModels)
			{
				if (!rowIntErrors.Contains(row))
				{
					var unit = units.Where(o => o.UnitNo == item.UnitNo && o.ProjectID == projectID && o.SAPWBSNo == item.WBSCode).FirstOrDefault();

					if (unit != null)
					{
						var titleDeed = titleDeeds.Where(o => o.UnitID == unit.ID).FirstOrDefault();

						#region PriceList
						PriceList pl = new PriceList();
						pl.UnitID = unit.ID;
						DateTime activeDate;

						if (DateTime.TryParseExact(item.DocumentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out activeDate))
						{
							pl.ActiveDate = activeDate;
						}

						listPriceList.Add(pl);
						#endregion

						#region PriceListItemSellPrice
						PriceListItem sellpricelistitem = new PriceListItem();
						sellpricelistitem.PriceListID = pl.ID;
						sellpricelistitem.Name = "ราคาขาย";
						sellpricelistitem.Order = 1;
						sellpricelistitem.MasterPriceItemID = MasterPriceItemIDs.SellPrice;
						sellpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
						sellpricelistitem.PriceUnitAmount = 1;
						sellpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.AreaPricePerUnit).RoundMoney());
						sellpricelistitem.PriceUnitMasterCenterID = priceUnitItemMasterCenterID;
						sellpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;


						listPriceListItem.Add(sellpricelistitem);
						#endregion

						#region PriceListItemNetSell
						PriceListItem netselllistitem = new PriceListItem();
						netselllistitem.PriceListID = pl.ID;
						netselllistitem.Name = "ราคาขายสุทธิ";
						netselllistitem.Order = 2;
						netselllistitem.MasterPriceItemID = MasterPriceItemIDs.NetSellPrice;
						netselllistitem.Amount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
						netselllistitem.PriceUnitAmount = 1;
						netselllistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
						netselllistitem.PriceUnitMasterCenterID = priceUnitItemMasterCenterID;
						netselllistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;

						listPriceListItem.Add(netselllistitem);
						#endregion

						#region PriceListItemBooking
						PriceListItem bookingpricelistitem = new PriceListItem();
						bookingpricelistitem.PriceListID = pl.ID;
						bookingpricelistitem.Name = "เงินจอง";
						bookingpricelistitem.Order = 3;
						bookingpricelistitem.MasterPriceItemID = MasterPriceItemIDs.BookingAmount;
						bookingpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(item.BookingAmount).RoundMoney());
						bookingpricelistitem.PriceUnitAmount = Math.Round(Convert.ToDouble(item.BookingAmount / item.SellPrice));
						bookingpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
						bookingpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
						bookingpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;

						listPriceListItem.Add(bookingpricelistitem);
						#endregion

						#region PriceListItemContract
						PriceListItem contractpricelistitem = new PriceListItem();
						contractpricelistitem.PriceListID = pl.ID;
						contractpricelistitem.Name = "เงินสัญญา";
						contractpricelistitem.Order = 4;
						contractpricelistitem.MasterPriceItemID = MasterPriceItemIDs.ContractAmount;
						contractpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(item.ContractAmount).RoundMoney());
						contractpricelistitem.PriceUnitAmount = Math.Round(Convert.ToDouble(item.ContractAmount / item.SellPrice));
						contractpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
						contractpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
						contractpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;

						listPriceListItem.Add(contractpricelistitem);
						#endregion

						#region PriceListItemDownPrice
						PriceListItem downpricelistitem = new PriceListItem();
						downpricelistitem.PriceListID = pl.ID;
						downpricelistitem.Name = "เงินดาวน์";
						downpricelistitem.Installment = item.DownInstallment;
						if (downpricelistitem.Installment == 0)
						{
							downpricelistitem.Installment = null;
						}
						downpricelistitem.Order = 5;
						downpricelistitem.MasterPriceItemID = MasterPriceItemIDs.DownAmount;

						if (item.DownPercent != 0)
						{
							downpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(((item.SellPrice * item.DownPercent) / 100) - item.ContractAmount - item.BookingAmount).RoundMoney());
							downpricelistitem.PriceUnitAmount = item.DownPercent / 100;
							downpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
							downpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
							downpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
							if (item.SpecialDownInstallments.Any() && item.SpecialDownInstallments.Any())
							{
								var specialDownPrice = item.SpecialDownInstallmentAmounts;
								decimal normalAmount = downpricelistitem.Amount;
								foreach (var specialPrice in specialDownPrice)
								{
									normalAmount -= Decimal.Round(Convert.ToDecimal(specialPrice));
								}

								var normalInstallmentAmount = normalAmount / (downpricelistitem.Installment - specialDownPrice.Count());
								downpricelistitem.InstallmentAmount = Decimal.Round(normalInstallmentAmount.Value.RoundMoney());
								downpricelistitem.SpecialInstallments = String.Join(',', item.SpecialDownInstallments);
								downpricelistitem.SpecialInstallmentAmounts = String.Join(',', item.SpecialDownInstallmentAmounts);
							}
							else
							{
								downpricelistitem.InstallmentAmount = Decimal.Round(Convert.ToDecimal(downpricelistitem.Amount / downpricelistitem.Installment).RoundMoney());
							}
						}
						else
						{
							downpricelistitem.Amount = 0;
						}



						listPriceListItem.Add(downpricelistitem);
						#endregion

						#region PriceListItemEXTRAAREAPRICE
						PriceListItem extrapricelistitem = new PriceListItem();
						extrapricelistitem.PriceListID = pl.ID;
						extrapricelistitem.Name = "ราคาพื้นที่เพิ่มลด";
						extrapricelistitem.Order = 6;
						extrapricelistitem.MasterPriceItemID = MasterPriceItemIDs.ExtraAreaPrice;

						extrapricelistitem.Amount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());

						//แนวราบ
						if (unit.Project?.ProductType?.Key == "1")
						{
							extrapricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.AreaPricePerUnit));
						}

						//แนวสูง
						if (unit.Project?.ProductType?.Key == "2")
						{
							extrapricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.SellPrice) / Convert.ToDecimal(unit.SaleArea));
						}

						var offsetArea = (titleDeed.TitledeedArea - unit.SaleArea) == 0 ? 0 : (titleDeed.TitledeedArea - unit.SaleArea);
						var offsetAreaResult = offsetArea != null ? offsetArea : 0;
						extrapricelistitem.PriceUnitAmount = offsetAreaResult * Convert.ToDouble(extrapricelistitem.PricePerUnitAmount.Value);

						extrapricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;

						listPriceListItem.Add(extrapricelistitem);

						#endregion

					}
				}
				row++;
			}
			await DB.PriceLists.AddRangeAsync(listPriceList);
			await DB.PriceListItems.AddRangeAsync(listPriceListItem);
			await this.PriceListDataStatus(projectID);
			await DB.SaveChangesAsync();

			result.Success = listPriceList.Count();
			result.Error = error;
			return result;
		}

		public async Task<PriceListExcelDTO> ImportProjectPriceListAsync(Guid projectID, FileDTO input, Guid? UserID = null)
		{
			var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
			var result = new PriceListExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>(), Messages = new List<string>() };
			//var dt = await this.ConvertExcelToDataTable(input);
			/// Valudate Header
			//if (dt.Columns.Count != 14)
			//{
			//	result.Messages.Add("Invalid File Format");
			//	result.Success = 0;
			//	return result;
			//}


			//ValidateException ex = new ValidateException();
			//if (dt.Columns.Count != 14)
			//{
			//	var msg = "Invalid File Format";
			//	ex.AddError("", msg, 1);
			//}
			//if (ex.HasError)
			//{
			//	throw ex;
			//}

			if (input.IsTemp)
			{
				string Name = "PriceList.xlsx";
				string pricelistName = $"import-project/{projectNo}/pricelist/{Name}";
				await FileHelper.MoveTempFileAsync(input.Name, pricelistName);
				result.Messages.Add("อัพโหลดไฟล์สำเร็จ กรุณารอผลทางอีเมล");
				result.Success = 1;
			}
			else
			{
				result.Messages.Add("ไม่สามารถอัพโหลดไฟล์ได้ กรุณาติดต่อ Admin");
				result.Success = 0;
			}
			ImptMstProjTran imp = new ImptMstProjTran();
			imp.CreatedByUserID = UserID;
			imp.IsDeleted = false;
			imp.ProjectID = projectID;
			imp.Import_Type = "pricelist";
			imp.Import_Status = "I";

			await DB.ImptMstProjTrans.AddAsync(imp);
			await DB.SaveChangesAsync();

			return result;
		}

		public async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
		{
			var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
			string fileName = input.Name;
			var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;
			bool hasHeader = true;
			using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
			{
				byte[] excelByte;
				if (fileExtention.ToLower() == "xls")
				{
					excelByte = XLSToXLSXConverter.Convert(stream);
				}
				else
				{
					excelByte = XLSToXLSXConverter.ReadFully(stream);
				}
				using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
				using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
				{
					var ws = pck.Workbook.Worksheets.First();
					DataTable tbl = new DataTable();
					foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
					{
						tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
					}
					var startRow = hasHeader ? 2 : 1;
					for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
					{
						var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
						DataRow row = tbl.Rows.Add();
						foreach (var cell in wsRow)
						{
							row[cell.Start.Column - 1] = cell.Text;
						}
					}

					return tbl;
				}
			}
		}

		public async Task<FileDTO> ExportExcelPriceListAsync(Guid projectID)
		{
			ExportExcel result = new ExportExcel();
			var allU = await DB.Units.Where(o => o.ProjectID == projectID)
										 .Include(o => o.UnitStatus)
										 .ToListAsync();

			var allUnits = allU.Where(o => o.UnitStatus.Key.Equals("0") && o.UnitStatus.Order == 1 && o.UnitStatus.MasterCenterGroupKey.Equals("UnitStatus")).ToList();

			var allUnitID = allUnits.Where(o => o.ProjectID == projectID).Select(o => o.ID).ToList();
			var allPriceList = await DB.PriceLists.Include(o => o.UpdatedBy)
							  .Where(o => allUnitID.Contains(o.UnitID)).OrderByDescending(o => o.Created)
							  .GroupBy(o => o.UnitID)
							  //.Select(o => o.Where(p => p.ActiveDate <= DateTime.Now).Select(p => p).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
							  .Select(o => o.Select(p => p).OrderByDescending(p => p.Created).FirstOrDefault()
							 ).ToListAsync();
			var allPriceListIsnotnull = allPriceList.Where(o => o != null).ToList();
			var allPriceListID = allPriceListIsnotnull.Where(o => allUnitID.Contains(o.UnitID)).Select(o => o.ID).ToList();
			var allPriceListItem = await DB.PriceListItems.Where(o => allPriceListID.Contains(o.PriceListID))
														  .Include(o => o.MasterPriceItem)
														  .Include(o => o.PriceUnit)
														  .Include(o => o.PriceType)
														  .ToListAsync();

			var allTitleDeed = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID && o.UnitID != null).GroupBy(o => o.UnitID)
											.Select(o => o.OrderByDescending(p => p.Created).First()).ToListAsync();

			bool isNew = false;
			if (allPriceListID.Count > 0)
			{
				isNew = false;
			}
			else
			{
				isNew = true;
			}

			//Check Prebook avaiable

			var data = GetPriceList(isNew, projectID, allUnits, allPriceListIsnotnull, allTitleDeed, allPriceListItem);

			var projectStatus = await DB.Projects.Where(o => o.ID == projectID)
										.Include(o => o.ProjectStatus).FirstOrDefaultAsync();
			if (projectStatus.ProjectStatus.Key == "0")
			{
				var PrebookAvaiable = await DB.PaymentPrebooks
											 .Include(o => o.Quotation)
											 .ThenInclude(o => o.Unit)
											 .Where(o => o.Quotation.IsPrebook == true
												 && o.Quotation.ProjectID == projectID
												 && allUnitID.Contains(o.Quotation.Unit.ID)
												 && o.IsCancel == false).Select(o => o.Quotation.Unit.ID)
											 .ToListAsync();

				data = data.Where(o => !PrebookAvaiable.Contains(o.Unit.ID)).ToList();

			}

			data = data.OrderBy(x => x.Unit.SAPWBSNo).ToList();
			var results = data.Select(o => PriceListDTO.CreateFromQueryResult(o)).ToList();

			string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_PriceList.xlsx");
			byte[] tmp = await File.ReadAllBytesAsync(path);

			using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
			using (ExcelPackage package = new ExcelPackage(stream))
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

				int _projectNoIndex = PriceListExcelModel._projectNoIndex + 1;
				int _projectNameIndex = PriceListExcelModel._projectNameIndex + 1;
				int _wbsCodeIndex = PriceListExcelModel._wbsCodeIndex + 1;
				int _unitNoIndex = PriceListExcelModel._unitNoIndex + 1;
				int _sellPriceIndex = PriceListExcelModel._sellPriceIndex + 1;
				int _areaPricePerUnitIndex = PriceListExcelModel._areaPricePerUnitIndex + 1;
				int _locationPriceIndex = PriceListExcelModel._locationPriceIndex + 1;
				int _bookingAmount = PriceListExcelModel._bookingAmountIndex + 1;
				int _contractAmountIndex = PriceListExcelModel._contractAmountIndex + 1;
				int _downPercentIndex = PriceListExcelModel._downPercentIndex + 1;
				int _downInstallmentIndex = PriceListExcelModel._downInstallmentIndex + 1;
				int _specialDownInstallmensIndex = PriceListExcelModel._specialDownInstallmensIndex + 1;
				int _specialDownInstallmentAmountsIndex = PriceListExcelModel._specialDownInstallmentAmountsIndex + 1;
				int _documentCodeIndex = PriceListExcelModel._documentDateIndex + 1;
				//ราคาขาย	ราคาพื้นที่ต่อหน่วย	ค่าทำเล	เงินจอง	เงินทำสัญญา

				var project = await DB.Projects.FirstOrDefaultAsync(x => x.ID == projectID);
				for (int c = 2; c < data.Count + 2; c++)
				{
					var unit = allUnits.Find(o => o.UnitNo == data[c - 2].Unit.UnitNo);
					worksheet.Cells[c, _projectNoIndex].Value = project?.ProjectNo;
					worksheet.Cells[c, _projectNameIndex].Value = project?.ProjectNameTH;
					worksheet.Cells[c, _wbsCodeIndex].Value = unit?.SAPWBSNo;
					worksheet.Cells[c, _unitNoIndex].Value = results[c - 2].UnitNo;

					worksheet.Cells[c, _sellPriceIndex].Value = results[c - 2].TotalSalePrice;
					worksheet.Cells[c, _areaPricePerUnitIndex].Value = results[c - 2].OffsetAreaUnitPrice;
					worksheet.Cells[c, _locationPriceIndex].Value = 0;
					worksheet.Cells[c, _bookingAmount].Value = results[c - 2].BookingAmount;
					worksheet.Cells[c, _contractAmountIndex].Value = results[c - 2].ContractAmount;
					worksheet.Cells[c, _downPercentIndex].Value = results[c - 2].PercentDownPayment;
					worksheet.Cells[c, _downInstallmentIndex].Value = results[c - 2].DownPaymentPeriod;
					worksheet.Cells[c, _specialDownInstallmensIndex].Value = results[c - 2].SpecialDown;
					worksheet.Cells[c, _specialDownInstallmentAmountsIndex].Value = results[c - 2].SpecialDownPrice;
					DateTime? datetime = results[c - 2]?.ActiveDate;
					if (datetime.HasValue)
					{
						worksheet.Cells[c, _documentCodeIndex].Style.Numberformat.Format = "dd/MM/yyyy";
						worksheet.Cells[c, _documentCodeIndex].Value = Convert.ToDateTime(results[c - 2]?.ActiveDate);
					}
				}
				//worksheet.Cells.AutoFitColumns();

				result.FileContent = package.GetAsByteArray();
				result.FileName = project.ProjectNo + "_PriceList.xlsx";
				result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			}

			Stream fileStream = new MemoryStream(result.FileContent);
			string fileName = result.FileName; //$"{Guid.NewGuid()}_{result.FileName}";
			string contentType = result.FileType;
			string filePath = $"project/{projectID}/export-excels/";
			var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
			var uploadResult = await this.FileHelper.UploadFileFromStreamWithOutGuid(fileStream, minioTempBucketName, filePath, fileName, contentType);
			return new FileDTO()
			{
				Name = result.FileName,
				Url = uploadResult.Url
			};
		}

		public List<PriceListQueryResult> GetPriceList(bool isNew, Guid? projectId, List<Unit> allUnits, List<PriceList> allPriceListIsnotnull, List<TitledeedDetail> allTitleDeed, List<PriceListItem> allPriceListItem)
		{
			//if (isNew)
			//{
			//}
			//else
			//{
			IEnumerable<TempPriceListQueryResult> tempData = (from unit in allUnits
															  join priceList in allPriceListIsnotnull on unit.ID equals priceList.UnitID into p
															  from pl in p.DefaultIfEmpty()
															  join titledeed in allTitleDeed on unit.ID equals titledeed.UnitID into t
															  from td in t.DefaultIfEmpty()
															  select new TempPriceListQueryResult
															  {
																  Unit = unit,
																  PriceList = pl,
																  Titledeed = td,
																  PriceListItems = allPriceListItem.Where(o => o.PriceListID == pl?.ID)
																					  .Select(o => PriceListItemDTO.CreateFromModel(o))
																					  .OrderBy(o => o.Order)
																					  .ToList()
															  });

			var data = tempData.Select(o => new PriceListQueryResult
			{
				Unit = o.Unit,
				TitleDeedArea = o.Titledeed?.TitledeedArea,
				BookingAmount = o.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.BookingAmount),
				TotalSalePrice = o.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.SellPrice),
				ContractAmount = o.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.ContractAmount),
				DownAmount = o.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount),
				SpecialDown = o.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount)?.SpecialInstallments,
				SpecialDownPrice = o.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount)?.SpecialInstallmentAmounts,
				DownPaymentPerPeriod = o.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount)?.InstallmentAmount,
				PercentDownPayment = o.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount)?.PriceUnitAmount == 0 ? null
				: o.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.DownAmount)?.PriceUnitAmount * 100,
				OffsetAreaUnitPrice = o.PriceListItems.Find(c => c.MasterPriceItem.Id == MasterPriceItemIDs.ExtraAreaPrice)?.PricePerUnitAmount,
				OffsetArea = (o.Titledeed?.TitledeedArea - o.Unit?.SaleArea) == 0 ? null : (o.Titledeed?.TitledeedArea - o.Unit?.SaleArea),
				OffsetAreaPrice = (Convert.ToDecimal(o.Titledeed?.TitledeedArea - o.Unit?.SaleArea) * o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemIDs.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault()) == 0 ? null : Convert.ToDecimal(o.Titledeed?.TitledeedArea - o.Unit?.SaleArea) * o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemIDs.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault(),
				PriceList = o.PriceList,
				UnitStatus = o.Unit.UnitStatus,
				AssetType = DB.MasterCenters.FirstOrDefault(k => k.ID == o.Unit.AssetTypeMasterCenterID)
			}).Where(o => o.AssetType?.Key != "6" && o.AssetType?.Key != "7")?.ToList();

			return data;
			//}
		}

		private async Task<Guid> PriceListDataStatus(Guid projectID)
		{
			var allU = await DB.Units.Where(o => o.ProjectID == projectID)
							 .Include(o => o.UnitStatus)
							 .ToListAsync();

			var allUnits = allU.Where(o => o.UnitStatus.Key.Equals("0") && o.UnitStatus.Order == 1 && o.UnitStatus.MasterCenterGroupKey.Equals("UnitStatus")).ToList();

			var allUnitID = allUnits.Where(o => o.ProjectID == projectID).Select(o => o.ID).ToList();
			var allPriceList = await DB.PriceLists.Include(o => o.UpdatedBy)
							  .Where(o => allUnitID.Contains(o.UnitID)).OrderByDescending(o => o.Created)
							  .GroupBy(o => o.UnitID)
							  //.Select(o => o.Where(p => p.ActiveDate <= DateTime.Now).Select(p => p).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
							  .Select(o => o.Select(p => p).OrderByDescending(p => p.Created).FirstOrDefault()
							 ).ToListAsync();
			var allPriceListIsnotnull = allPriceList.Where(o => o != null).ToList();
			var allPriceListID = allPriceListIsnotnull.Where(o => allUnitID.Contains(o.UnitID)).Select(o => o.ID).ToList();
			var allPriceListItem = await DB.PriceListItems.Where(o => allPriceListID.Contains(o.PriceListID))
														  .Include(o => o.MasterPriceItem)
														  .Include(o => o.PriceUnit)
														  .Include(o => o.PriceType)
														  .ToListAsync();

			var allTitleDeed = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID && o.UnitID != null).GroupBy(o => o.UnitID)
											.Select(o => o.OrderByDescending(p => p.Created).First()).ToListAsync();

			bool isNew = false;
			if (allPriceListID.Count > 0)
			{
				isNew = false;
			}
			else
			{
				isNew = true;
			}

			var data = GetPriceList(isNew, projectID, allUnits, allPriceListIsnotnull, allTitleDeed, allPriceListItem);


			var priceListDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectDataStatus && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstAsync();
			var priceListDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectDataStatus && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
			var priceListDataStatusMasterCenterID = priceListDataStatusPrepareMasterCenterID;


			//ราคาขาย	ราคาพื้นที่ต่อหน่วย	เงินจอง	เงินทำสัญญา

			if (data.TrueForAll(o =>
					//!string.IsNullOrEmpty(o.Unit?.UnitNo) //เลขที่แปลง
					//&& o.Unit?.SaleArea != null 
					o.TotalSalePrice?.Amount != 0 //ราคาขาย
					&& o.OffsetAreaUnitPrice != 0 //ราคาพื้นที่ต่อหน่วย
					&& o.BookingAmount?.Amount != 0 //เงินจอง
					&& o.ContractAmount?.Amount != 0 //เงินทำสัญญา

			//&& o.PercentDownPayment != null
			//&& o.DownAmount?.Amount != 0
			//&& o.DownAmount?.Installment != null
			//&& !string.IsNullOrEmpty(o.SpecialDown)
			//&& !string.IsNullOrEmpty(o.SpecialDownPrice)
			))
			{
				priceListDataStatusMasterCenterID = priceListDataStatusSaleMasterCenterID;
			}
			return priceListDataStatusMasterCenterID;
		}
	}
}
