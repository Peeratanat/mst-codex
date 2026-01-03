using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRM;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Params.Outputs;
using Common.Helper.Logging;

namespace MST_Promotion.Services
{
	public class PromotionMaterialService  
	{
		private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }
		public PromotionMaterialService(DatabaseContext db)
		{
            logModel = new LogModel("PromotionMaterialService", null);
			this.DB = db;
		}

		/// <summary>
		/// ดึงรายการ Promotion Material ที่มาจาก SAP
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358682/preview
		/// </summary>
		/// <returns>The promotion material list async.</returns>
		/// <param name="filter">Filter.</param>
		/// <param name="pageParam">Page parameter.</param>
		/// <param name="sortByParam">Sort by parameter.</param>
		public async Task<PromotionMaterialPaging> GetPromotionMaterialListAsync(PromotionMaterialFilter filter, PageParam pageParam, PromotionMaterialSortByParam sortByParam)
		{
			var project = await DB.Projects
								.Where(o => o.ID == filter.ProjectID)
				                .Include(o => o.Company)
								.Include(o => o.ProductType)
								.FirstOrDefaultAsync();

			var statusDeleteFromSapId = await DB.MasterCenters
										.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MaterialItemStatus 
													&& o.Key == "102")
				                        .Select(o => o.ID)
										.FirstOrDefaultAsync();

			var statusExpiredFromSapId = await DB.MasterCenters
										.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MaterialItemStatus
													&& o.Key == "101")
										.Select(o => o.ID)
										.FirstOrDefaultAsync();
            var matfee = await DB.PromotionOperatingFees.Where(x => x.IsActived == true).Select(x => x.PromotionMaterialCode).ToListAsync();

            IQueryable<PromotionMaterialQueryResult> query = DB.PromotionMaterialItems
															.Where(o => !string.IsNullOrEmpty(o.AgreementNo)
																	&& !string.IsNullOrEmpty(o.ItemNo)
																	&& (!string.IsNullOrEmpty(o.MaterialCode) || o.AgreementNo.ToLower().Contains("welcomehome"))
																	&& (!string.IsNullOrEmpty(o.GLAccountNo) || o.AgreementNo.ToLower().Contains("welcomehome"))
																	&& !string.IsNullOrEmpty(o.MaterialGroupKey)
																	//&& o.ExpireDate > DateTime.Now
																	&& o.AgreementExpireDate != null && o.ExpireDate != null
																	&& compaireExpiredDate(o.AgreementExpireDate, o.ExpireDate) > DateTime.Now
																	&& o.MaterialItemStatusMasterCenterID != statusDeleteFromSapId
																	&& o.MaterialItemStatusMasterCenterID != statusExpiredFromSapId
																	&& (o.AgreementNo.ToLower().Contains("welcomehome") || !matfee.Contains(o.MaterialCode))
																	) //#region evoucher aurora ไม่ต้องแสดง item ที่เป็น ค่าดำเนินการ
															.Select(o =>
																	new PromotionMaterialQueryResult
																	{
																		PromotionMaterialItem = o,
																		UpdatedBy = o.UpdatedBy
																	});


			#region Filter
			if (project != null)
			{
				query = query.Where(o => ((o.PromotionMaterialItem.Plant == project.Plant || string.IsNullOrEmpty(o.PromotionMaterialItem.Plant)) 
								&& (o.PromotionMaterialItem.SAPCompanyID == project.Company.SAPCompanyID || (string.IsNullOrEmpty(o.PromotionMaterialItem.SAPCompanyID) && o.PromotionMaterialItem.AgreementNo.ToLower().Contains("welcomehome")))));
			}
			if (!string.IsNullOrEmpty(filter.AgreementNo))
			{
				query = query.Where(o => o.PromotionMaterialItem.AgreementNo.ToLower().Contains(filter.AgreementNo.ToLower()));
			}
			if (!string.IsNullOrEmpty(filter.ItemNo))
			{
				query = query.Where(o => o.PromotionMaterialItem.ItemNo.Contains(filter.ItemNo));
			}
			if (!string.IsNullOrEmpty(filter.Plant))
			{
				query = query.Where(o => o.PromotionMaterialItem.Plant.Contains(filter.Plant));
			}
			if (!string.IsNullOrEmpty(filter.NameTH))
			{
				query = query.Where(o => o.PromotionMaterialItem.NameTH.ToLower().Contains(filter.NameTH.ToLower()));
			}
			if (!string.IsNullOrEmpty(filter.NameEN))
			{
				query = query.Where(o => o.PromotionMaterialItem.NameEN.Contains(filter.NameEN));
			}
			if (!string.IsNullOrEmpty(filter.MaterialCode))
			{
				query = query.Where(o => o.PromotionMaterialItem.MaterialCode.Contains(filter.MaterialCode));
			}
			if (filter.PriceFrom != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Price >= filter.PriceFrom);
			}
			if (filter.PriceTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Price <= filter.PriceTo);
			}
			if (filter.PriceFrom != null && filter.PriceTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Price >= filter.PriceFrom
									&& x.PromotionMaterialItem.Price <= filter.PriceTo);
			}
			if (!string.IsNullOrEmpty(filter.Unit))
			{
				query = query.Where(o => o.PromotionMaterialItem.UnitTH.Contains(filter.Unit));
			}
			if (filter.ExpireDateFrom != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.ExpireDate >= filter.ExpireDateFrom);
			}
			if (filter.ExpireDateTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.ExpireDate <= filter.ExpireDateTo);
			}
			if (filter.ExpireDateFrom != null && filter.ExpireDateTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.ExpireDate >= filter.ExpireDateFrom
									&& x.PromotionMaterialItem.ExpireDate <= filter.ExpireDateTo);
			}
			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Updated <= filter.UpdatedTo);
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Updated >= filter.UpdatedFrom && x.PromotionMaterialItem.Updated <= filter.UpdatedTo);
			}

			#endregion

			PromotionMaterialDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<PromotionMaterialQueryResult>(pageParam, ref query);

			var queryResults = await query.ToListAsync();

			var promotionmaterialAddPrices = await DB.PromotionMaterialAddPrices.ToListAsync();
			var promotionmaterialGroups = await DB.PromotionMaterialGroups.ToListAsync();

			var results = queryResults.Select(o => PromotionMaterialDTO.CreateFromQueryResult(o, promotionmaterialAddPrices, promotionmaterialGroups, project)).ToList();

			return new PromotionMaterialPaging()
			{
				PageOutput = pageOutput,
				PromotionMaterialDTOs = results
			};
		}

		public DateTime? compaireExpiredDate(DateTime? agreementExpireDate, DateTime? expiredDate)
		{
			DateTime? dateResult = agreementExpireDate <= expiredDate ? agreementExpireDate : expiredDate;
			return dateResult;
		}


		/// <summary>
		/// อ่านไฟล์ ZRFCMM01 เพื่อไปสร้าง PromotionMaterial
		/// </summary>
		/// <param name="sapTextContent"></param>
		/// <returns></returns>
		public async Task ReadMaterialMasterFromSAPAsync(string[] sapTextContent)
		{
			var syncSaps = new List<SAP_ZRFCMM01>();
			foreach (var item in sapTextContent)
			{
				var data = item.Split(';').ToList();
				var sapModel = new SAP_ZRFCMM01
				{
					WERKS = data[0],
					MATNR = data[1],
					MTART = data[2],
					MATKL = data[3],
					MEINS = data[4],
					BSTME = data[5],
					MAKTX = data[6],
					WGBEZ = data[7],
					MTBEZ = data[8],
					BKLAS = data[9],
					KONTS = data[10],
					MSEHT = data[11],
				};
				syncSaps.Add(sapModel);
			}
			var addSaps = new List<SAP_ZRFCMM01>();
			var updateSaps = new List<SAP_ZRFCMM01>();
			foreach (var item in syncSaps)
			{
				var existedItem = await DB.SAP_ZRFCMM01s.Where(o => o.WERKS == item.WERKS && o.MATNR == item.MATNR).FirstOrDefaultAsync();
				if (existedItem == null)
				{
					var model = new SAP_ZRFCMM01();
					model.WERKS = item.WERKS;
					model.MATNR = item.MATNR;
					model.MTART = item.MTART;
					model.MATKL = item.MATKL;
					model.MEINS = item.MEINS;
					model.BSTME = item.BSTME;
					model.MAKTX = item.MAKTX;
					model.WGBEZ = item.WGBEZ;
					model.MTBEZ = item.MTBEZ;
					model.BKLAS = item.BKLAS;
					model.KONTS = item.KONTS;
					model.MSEHT = item.MSEHT;
					addSaps.Add(model);
				}
				else
				{
					existedItem.WERKS = item.WERKS;
					existedItem.MATNR = item.MATNR;
					existedItem.MTART = item.MTART;
					existedItem.MATKL = item.MATKL;
					existedItem.MEINS = item.MEINS;
					existedItem.BSTME = item.BSTME;
					existedItem.MAKTX = item.MAKTX;
					existedItem.WGBEZ = item.WGBEZ;
					existedItem.MTBEZ = item.MTBEZ;
					existedItem.BKLAS = item.BKLAS;
					existedItem.KONTS = item.KONTS;
					existedItem.MSEHT = item.MSEHT;
					updateSaps.Add(existedItem);
				}
			}
			await DB.SAP_ZRFCMM01s.AddRangeAsync(addSaps);
			DB.SAP_ZRFCMM01s.UpdateRange(updateSaps);
			await DB.SaveChangesAsync();

			var getAllDataSap = await DB.SAP_ZRFCMM01s.ToListAsync();
			var addPromotionMaterials = new List<PromotionMaterial>();
			var updatePromotionMaterials = new List<PromotionMaterial>();
			foreach (var item in getAllDataSap)
			{
				var existedItem = await DB.PromotionMaterials.Where(o => o.Plant == item.WERKS && o.Code == item.MATNR).FirstOrDefaultAsync();
				var group = await DB.PromotionMaterialGroups.Where(o => o.Key == item.MATKL/* && o.Name == item.WGBEZ*/).FirstOrDefaultAsync();
				if (existedItem == null)
				{
					var model = new PromotionMaterial();
					model.Plant = item.WERKS;
					model.Code = item.MATNR;
					model.TypeCode = item.MTART;
					model.TypeName = item.MTBEZ;

					// Group
					model.MaterialGroupKey = group?.Key;
					model.MaterialGroupName = group?.Name;
					model.PromotionMaterialGroupID = group?.ID;

					model.UnitTH = item.MSEHT;
					model.GLAccountNo = item.KONTS;
					model.UnitEN = item.MEINS;
					model.ValuationClass = item.BKLAS;
					model.UnitPO = item.BSTME;
					model.Name = item.MAKTX;

					model.IsActive = true;
					addPromotionMaterials.Add(model);
				}
				else
				{
					existedItem.Plant = item.WERKS;
					existedItem.Code = item.MATNR;
					existedItem.TypeCode = item.MTART;
					existedItem.TypeName = item.MTBEZ;

					// Group
					existedItem.MaterialGroupKey = group?.Key;
					existedItem.MaterialGroupName = group?.Name;
					existedItem.PromotionMaterialGroupID = group?.ID;

					existedItem.UnitTH = item.MSEHT;
					existedItem.GLAccountNo = item.KONTS;
					existedItem.UnitEN = item.MEINS;
					existedItem.ValuationClass = item.BKLAS;
					existedItem.UnitPO = item.BSTME;
					existedItem.Name = item.MAKTX;
					updatePromotionMaterials.Add(existedItem);
				}
			}
			await DB.PromotionMaterials.AddRangeAsync(addPromotionMaterials);
			DB.UpdateRange(updatePromotionMaterials);
			await DB.SaveChangesAsync();
		}

		/// <summary>
		/// อ่านไฟล์ ZRFCMM02 เพื่อไปสร้าง PromotionMaterialItem
		/// </summary>
		/// <param name="sapTextContent"></param>
		/// <returns></returns>
		public async Task ReadMaterialAgreementFromSAPAsync(string[] sapTextContent)
		{
			var syncSaps = new List<SAP_ZRFCMM02>();
			foreach (var item in sapTextContent)
			{
				var data = item.Split(';').ToList();
				var sapModel = new SAP_ZRFCMM02
				{
					BUKRS = data[0],
					LIFNR = data[1],
					EKORG = data[2],
					EKGRP = data[3],
					ZTERM = data[4],
					KDATB = data[5],
					KDATE = data[6],
					WERKS = data[7],
					EBELN = data[8],
					EBELP = data[9],
					MATNR = data[10],
					MAKTX = data[11],
					VAKEY = data[12],
					KNUMH = data[13],
					ERDAT = data[14],
					DATAB = data[15],
					DATBI = data[16],
					KBETR = data[17],
					KMEIN = data[18],
					MEINS = data[19],
					LOEKZ = data[20],
					SLTAX = data[21],
					THUNT = data[22],
				};
				syncSaps.Add(sapModel);
			}
			var addItemSaps = new List<SAP_ZRFCMM02>();
			var updateItemSaps = new List<SAP_ZRFCMM02>();
			foreach (var item in syncSaps)
			{
				var existedItem = await DB.SAP_ZRFCMM02s.Where(o => o.WERKS == item.WERKS
																&& o.MATNR == item.MATNR
																&& o.EBELN == item.EBELN
																&& o.EBELP == item.EBELP
																&& o.KDATB == item.KDATB
																&& o.KDATE == item.KDATE
																&& o.DATAB == item.DATAB
																&& o.DATBI == item.DATBI
															   ).FirstOrDefaultAsync();
				if (existedItem == null)
				{
					var model = new SAP_ZRFCMM02();
					model.BUKRS = item.BUKRS;
					model.LIFNR = item.LIFNR;
					model.EKORG = item.EKORG;
					model.EKGRP = item.EKGRP;
					model.ZTERM = item.ZTERM;
					model.KDATB = item.KDATB;
					model.KDATE = item.KDATE;
					model.WERKS = item.WERKS;
					model.EBELN = item.EBELN;
					model.EBELP = item.EBELP;
					model.MATNR = item.MATNR;
					model.MAKTX = item.MAKTX;
					model.VAKEY = item.VAKEY;
					model.KNUMH = item.KNUMH;
					model.ERDAT = item.ERDAT;
					model.DATAB = item.DATAB;
					model.DATBI = item.DATBI;
					model.KBETR = item.KBETR;
					model.KMEIN = item.KMEIN;
					model.MEINS = item.MEINS;
					model.LOEKZ = item.LOEKZ;
					model.SLTAX = item.SLTAX;
					model.THUNT = item.THUNT;
					addItemSaps.Add(model);
				}
				else
				{
					existedItem.BUKRS = item.BUKRS;
					existedItem.LIFNR = item.LIFNR;
					existedItem.EKORG = item.EKORG;
					existedItem.EKGRP = item.EKGRP;
					existedItem.ZTERM = item.ZTERM;
					existedItem.KDATB = item.KDATB;
					existedItem.KDATE = item.KDATE;
					existedItem.WERKS = item.WERKS;
					existedItem.EBELN = item.EBELN;
					existedItem.EBELP = item.EBELP;
					existedItem.MATNR = item.MATNR;
					existedItem.MAKTX = item.MAKTX;
					existedItem.VAKEY = item.VAKEY;
					existedItem.KNUMH = item.KNUMH;
					existedItem.ERDAT = item.ERDAT;
					existedItem.DATAB = item.DATAB;
					existedItem.DATBI = item.DATBI;
					existedItem.KBETR = item.KBETR;
					existedItem.KMEIN = item.KMEIN;
					existedItem.MEINS = item.MEINS;
					existedItem.LOEKZ = item.LOEKZ;
					existedItem.SLTAX = item.SLTAX;
					existedItem.THUNT = item.THUNT;
					updateItemSaps.Add(existedItem);
				}
			}
			await DB.SAP_ZRFCMM02s.AddRangeAsync(addItemSaps);
			DB.SAP_ZRFCMM02s.UpdateRange(updateItemSaps);
			await DB.SaveChangesAsync();

			var getAllDataSap = await DB.SAP_ZRFCMM02s.GroupBy(o => new { o.WERKS, o.EBELN, o.EBELP, o.MATNR }).ToListAsync();

			var addPromotionMaterialItems = new List<PromotionMaterialItem>();
			var updatePromotionMaterialItems = new List<PromotionMaterialItem>();


			var getAllMasterCenterMaterialItemStatus = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "MaterialItemStatus").ToListAsync();
			var getAllMasterCenterWhenPromotionReceive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive").ToListAsync();
			foreach (var item in getAllDataSap)
			{
				var plant = item.Select(o => o.WERKS).FirstOrDefault();
				var sapVarKey = item.Select(o => o.VAKEY).FirstOrDefault();
				var materialCode = item.Select(o => o.MATNR).FirstOrDefault();
				var existedItem = await DB.PromotionMaterialItems.Where(o => o.Plant == plant
														  && o.SAPVarKey == sapVarKey
														  && o.MaterialCode == materialCode
												).FirstOrDefaultAsync();
				var getItemNotExpired = item.Where(o => DateTime.ParseExact(o.DATBI, "yyyyMMdd", null) > DateTime.Now).OrderByDescending(o => o.Updated).FirstOrDefault();
				var promotionMaterial = await DB.PromotionMaterials.Where(o => o.Code == getItemNotExpired.MATNR).Include(o => o.PromotionMaterialGroup).FirstOrDefaultAsync();
				double percentMarkUP = 0;
				if (promotionMaterial != null)
				{
					var project = await DB.Projects.Where(o => o.Plant == promotionMaterial.Plant).Include(o => o.ProductType).FirstOrDefaultAsync();
					if (project == null)
					{
						percentMarkUP = 0;
					}
					else
					{
						var promotionmaterialAddPrice = await DB.PromotionMaterialAddPrices.Where(o => o.PromotionMaterialGroupID == promotionMaterial.PromotionMaterialGroupID).FirstOrDefaultAsync();
						if (project.ProductType.Key == ProductTypeKeys.LowRise)
						{
							percentMarkUP = promotionmaterialAddPrice == null ? 0 : promotionmaterialAddPrice.LowRisePercent;
						}
						else if (project.ProductType.Key == ProductTypeKeys.HighRise)
						{
							percentMarkUP = promotionmaterialAddPrice == null ? 0 : promotionmaterialAddPrice.HighRisePercent;
						}
					}
				}

				var vat = await DB.PromotionVatRates.Where(o => o.Code == getItemNotExpired.SLTAX).FirstOrDefaultAsync();

				if (existedItem == null && getItemNotExpired != null)
				{
					var model = new PromotionMaterialItem();
					model.Plant = getItemNotExpired.WERKS;
					model.ItemNo = getItemNotExpired.EBELP;
					model.AgreementNo = getItemNotExpired.EBELN;
					model.NameTH = getItemNotExpired.MAKTX;
					model.NameEN = getItemNotExpired.MAKTX;
					// PromotionMaterial
					model.MaterialCode = promotionMaterial?.Code;
					model.GLAccountNo = promotionMaterial?.GLAccountNo;
					model.MaterialGroupKey = promotionMaterial?.MaterialGroupKey;
					model.PromotionMaterialID = promotionMaterial?.ID;
					model.UnitTH = promotionMaterial?.UnitTH;
					model.UnitEN = promotionMaterial?.UnitEN;

					// Price
					model.BasePrice = Convert.ToDecimal(getItemNotExpired.KBETR);
					model.Price = (Convert.ToDecimal(getItemNotExpired.KBETR) * Convert.ToDecimal((percentMarkUP + 100) / 100)) * Convert.ToDecimal(vat.VatRate);/*((Convert.ToDecimal(vat.VatRate) * Convert.ToDecimal(getItemNotExpired.KBETR)) / 100) + Convert.ToDecimal(getItemNotExpired.KBETR);*/
					model.Vat = (vat.VatRate * 100) - 100;
					// Sap
					model.SAPCompanyID = getItemNotExpired.BUKRS;
					model.SAPPurchasingOrg = getItemNotExpired.EKORG;
					model.SAPPurchasingGroup = getItemNotExpired.EKGRP;
					model.SAPBaseUnit = getItemNotExpired.MEINS;
					model.SAPVendor = getItemNotExpired.LIFNR;
					model.SAPVarKey = getItemNotExpired.VAKEY;
					model.SAPSaleTaxCode = getItemNotExpired.SLTAX;
					model.SAPTermPaymentKey = getItemNotExpired.ZTERM;
					model.SAPDeleteIndicator = getItemNotExpired.LOEKZ;
					model.SAPConditionRecordNo = getItemNotExpired.KNUMH;
					model.SAPCreatedTime = getItemNotExpired.ERDAT != null ? DateTime.ParseExact(getItemNotExpired.ERDAT, "yyyyMMdd", null) : (DateTime?)null;
					model.IsActive = true;
					model.StartDate = DateTime.ParseExact(getItemNotExpired.DATAB, "yyyyMMdd", null);
					model.ExpireDate = DateTime.ParseExact(getItemNotExpired.DATBI, "yyyyMMdd", null);
					model.MaterialItemStatusMasterCenterID = getAllMasterCenterMaterialItemStatus.Where(o => o.Key == "1").Select(o => o.ID).FirstOrDefault();
					addPromotionMaterialItems.Add(model);
				}
				else if (existedItem != null && getItemNotExpired == null)
				{
					existedItem.MaterialItemStatusMasterCenterID = getAllMasterCenterMaterialItemStatus.Where(o => o.Key == "101").Select(o => o.ID).FirstOrDefault();
					updatePromotionMaterialItems.Add(existedItem);
				}
				else if (existedItem != null && getItemNotExpired != null)
				{
					existedItem.Plant = getItemNotExpired.WERKS;
					existedItem.ItemNo = getItemNotExpired.EBELP;
					existedItem.AgreementNo = getItemNotExpired.EBELN;
					existedItem.NameTH = getItemNotExpired.MAKTX;
					existedItem.NameEN = getItemNotExpired.MAKTX;
					// PromotionMaterial
					existedItem.MaterialCode = promotionMaterial?.Code;
					existedItem.GLAccountNo = promotionMaterial?.GLAccountNo;
					existedItem.MaterialGroupKey = promotionMaterial?.MaterialGroupKey;
					existedItem.PromotionMaterialID = promotionMaterial?.ID;
					existedItem.UnitTH = promotionMaterial?.UnitTH;
					existedItem.UnitEN = promotionMaterial?.UnitEN;
					// Price
					existedItem.BasePrice = Convert.ToDecimal(getItemNotExpired.KBETR);
					existedItem.Price = (Convert.ToDecimal(getItemNotExpired.KBETR) * Convert.ToDecimal((percentMarkUP + 100) / 100)) * Convert.ToDecimal(vat.VatRate);
					existedItem.Vat = (vat.VatRate * 100) - 100;
					// Sap
					existedItem.SAPCompanyID = getItemNotExpired.BUKRS;
					existedItem.SAPPurchasingOrg = getItemNotExpired.EKORG;
					existedItem.SAPPurchasingGroup = getItemNotExpired.EKGRP;
					existedItem.SAPBaseUnit = getItemNotExpired.MEINS;
					existedItem.SAPVendor = getItemNotExpired.LIFNR;
					existedItem.SAPVarKey = getItemNotExpired.VAKEY;
					existedItem.SAPSaleTaxCode = getItemNotExpired.SLTAX;
					existedItem.SAPTermPaymentKey = getItemNotExpired.ZTERM;
					existedItem.SAPDeleteIndicator = getItemNotExpired.LOEKZ;
					existedItem.SAPConditionRecordNo = getItemNotExpired.KNUMH;
					existedItem.SAPCreatedTime = getItemNotExpired.ERDAT != null ? DateTime.ParseExact(getItemNotExpired.ERDAT, "yyyyMMdd", null) : (DateTime?)null;
					existedItem.IsActive = true;

					existedItem.StartDate = DateTime.ParseExact(getItemNotExpired.DATAB, "yyyyMMdd", null);
					existedItem.ExpireDate = DateTime.ParseExact(getItemNotExpired.DATBI, "yyyyMMdd", null);
					if (getItemNotExpired.LOEKZ == "L")
					{
						existedItem.MaterialItemStatusMasterCenterID = (Guid?)getAllMasterCenterMaterialItemStatus.Where(o => o.Key == "102").Select(o => o.ID).FirstOrDefault();
					}
					updatePromotionMaterialItems.Add(existedItem);
				}
			}

			await DB.PromotionMaterialItems.AddRangeAsync(addPromotionMaterialItems);
			DB.PromotionMaterialItems.UpdateRange(updatePromotionMaterialItems);
			await DB.SaveChangesAsync();
		}

		public async Task<MaterialSyncJobPaging> GetMaterialSyncJobListAsync(MaterialSyncJobFilter filter, PageParam pageParam, MaterialSyncJobSortByParam sortByParam)
		{
			var query = DB.SAPMaterialSyncJobs.AsQueryable();

			if (!string.IsNullOrEmpty(filter.JobNo))
			{
				query = query.Where(o => o.JobNo.Contains(filter.JobNo));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(o => o.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(o => o.Updated <= filter.UpdatedTo);
			}
			if (filter.Status != null)
			{
				query = query.Where(o => o.Status == filter.Status);
			}

			MaterialSyncJobDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<SAPMaterialSyncJob>(pageParam, ref query);

			var queryResults = await query.ToListAsync();
			var results = queryResults.Select(o => MaterialSyncJobDTO.CreateFromModel(o)).ToList();

			return new MaterialSyncJobPaging()
			{
				PageOutput = pageOutput,
				MaterialSyncJobDTOs = results
			};
		}

		/// <summary>
		/// ดึงรายการ Promotion Material Item
		/// </summary>
		/// <returns>The promotion material Item list async.</returns>
		/// <param name="filter">Filter.</param>
		/// <param name="pageParam">Page parameter.</param>
		/// <param name="sortByParam">Sort by parameter.</param>
		public async Task<PromotionMaterialPaging> GetPromotionMaterialItemListAsync(PromotionMaterialFilter filter, PageParam pageParam, PromotionMaterialSortByParam sortByParam)
		{
			//var project = await DB.Projects.Where(o => o.ID == filter.ProjectID).Include(o => o.Company).FirstOrDefaultAsync();
			//var statusDeleteFromSap = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MaterialItemStatus && o.Key == "102").Select(o => o.ID).FirstAsync();
			IQueryable<PromotionMaterialQueryResult> query = DB.PromotionMaterialItems
														   .Where(o => o.AgreementNo == "CREDIT0")
														   .OrderBy(o => o.BrandTH)
														   .OrderBy(o => o.NameTH)
														   .OrderBy(o => o.Price)
														   .Select(o =>
																	new PromotionMaterialQueryResult
																	{
																		PromotionMaterialItem = o,
																		UpdatedBy = o.UpdatedBy
																	});
			#region Filter
			//if (project != null)
			//{
			//    query = query.Where(o => o.PromotionMaterialItem.Plant == project.Plant || string.IsNullOrEmpty(o.PromotionMaterialItem.Plant) && o.PromotionMaterialItem.SAPCompanyID == project.Company.SAPCompanyID);
			//}
			if (!string.IsNullOrEmpty(filter.AgreementNo))
			{
				query = query.Where(o => o.PromotionMaterialItem.AgreementNo.Contains(filter.AgreementNo));
			}
			if (!string.IsNullOrEmpty(filter.ItemNo))
			{
				query = query.Where(o => o.PromotionMaterialItem.ItemNo.Contains(filter.ItemNo));
			}
			if (!string.IsNullOrEmpty(filter.Plant))
			{
				query = query.Where(o => o.PromotionMaterialItem.Plant.Contains(filter.Plant));
			}
			if (!string.IsNullOrEmpty(filter.BrandTH))
			{
				query = query.Where(o => o.PromotionMaterialItem.BrandTH.Equals(filter.BrandTH));
			}
			if (!string.IsNullOrEmpty(filter.BrandEN))
			{
				query = query.Where(o => o.PromotionMaterialItem.BrandEN.Equals(filter.BrandEN));
			}
			if (!string.IsNullOrEmpty(filter.NameTH))
			{
				query = query.Where(o => o.PromotionMaterialItem.NameTH.Contains(filter.NameTH));
			}
			if (!string.IsNullOrEmpty(filter.NameEN))
			{
				query = query.Where(o => o.PromotionMaterialItem.NameEN.Contains(filter.NameEN));
			}
			if (!string.IsNullOrEmpty(filter.MaterialCode))
			{
				query = query.Where(o => o.PromotionMaterialItem.MaterialCode.Contains(filter.MaterialCode));
			}
			if (filter.PriceFrom != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Price >= filter.PriceFrom);
			}
			if (filter.PriceTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Price <= filter.PriceTo);
			}
			if (filter.PriceFrom != null && filter.PriceTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Price >= filter.PriceFrom
									&& x.PromotionMaterialItem.Price <= filter.PriceTo);
			}
			if (!string.IsNullOrEmpty(filter.Unit))
			{
				query = query.Where(o => o.PromotionMaterialItem.UnitTH.Contains(filter.Unit));
			}
			if (filter.ExpireDateFrom != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.ExpireDate >= filter.ExpireDateFrom);
			}
			if (filter.ExpireDateTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.ExpireDate <= filter.ExpireDateTo);
			}
			if (filter.ExpireDateFrom != null && filter.ExpireDateTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.ExpireDate >= filter.ExpireDateFrom
									&& x.PromotionMaterialItem.ExpireDate <= filter.ExpireDateTo);
			}
			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Updated <= filter.UpdatedTo);
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				query = query.Where(x => x.PromotionMaterialItem.Updated >= filter.UpdatedFrom && x.PromotionMaterialItem.Updated <= filter.UpdatedTo);
			}

			#endregion

			//PromotionMaterialDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<PromotionMaterialQueryResult>(pageParam, ref query);

			var queryResults = await query.ToListAsync();

			var results = queryResults.Select(o => PromotionMaterialDTO.CreateFromQueryWithBrandResult(o)).ToList();

			return new PromotionMaterialPaging()
			{
				PageOutput = pageOutput,
				PromotionMaterialDTOs = results
			};
		}

		//public async Task<List<PromotionMaterialDTO>> GetPromotionItemAsync(PromotionMaterialFilter filter, PageParam pageParam)
		//{
		//	var project = await DB.Projects
		//						.Where(o => o.ProjectNo == filter.ProjectNo)
		//						.Include(o => o.Company)
		//						.Include(o => o.ProductType)
		//						.FirstOrDefaultAsync();

		//	var query = new List<PromotionMaterialQueryResult>();
		//	if (string.IsNullOrEmpty(filter.ItemNo))
		//	{
		//		query = await (from o in DB.PromotionMaterialItems

		//					   select new PromotionMaterialQueryResult
		//					   {
		//						   PromotionMaterialItem = o

		//					   }).Where(o => o.PromotionMaterialItem.AgreementNo == filter.AgreementNo).ToListAsync();
		//	}
		//	else
		//	{
		//		query = await (from o in DB.PromotionMaterialItems

		//					   select new PromotionMaterialQueryResult
		//					   {
		//						   PromotionMaterialItem = o

		//					   }).Where(o => o.PromotionMaterialItem.AgreementNo == filter.AgreementNo && o.PromotionMaterialItem.ItemNo == filter.ItemNo)
		//					   .ToListAsync();
		//	}

		//	var results = query.Select(o => PromotionMaterialDTO.CreatePromotionMaterialItemFromModel(o)).ToList();
		//	return results;


		//}

	}
}
