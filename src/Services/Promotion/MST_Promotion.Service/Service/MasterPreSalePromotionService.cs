using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models;
using Database.Models.DbQueries.PRM;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRM;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Params.Outputs;
using Common.Helper.Logging;
using System.Data.Common;
using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace MST_Promotion.Services
{
	public class MasterPreSalePromotionService : IMasterPreSalePromotionService
	{
		private readonly DatabaseContext DB;
		public LogModel logModel { get; set; }

		public MasterPreSalePromotionService(DatabaseContext db)
		{
			logModel = new LogModel("MasterPreSalePromotionService", null);
			this.DB = db;
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358687/preview
		/// </summary>
		/// <returns>The master pre sale promotion async.</returns>
		/// <param name="input">Input.</param>
		public async Task<MasterPreSalePromotionDTO> CreateMasterPreSalePromotionAsync(MasterPreSalePromotionDTO input)
		{
			await input.ValidateAsync(DB);
			var masterCenterPromotionStatusActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
																	  .Select(o => o.ID)
																	  .FirstAsync();
			var masterCenterPromotionStatusInActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
																	  .Select(o => o.ID)
																	  .FirstAsync();

			MasterPreSalePromotion model = new MasterPreSalePromotion();
			input.ToModel(ref model);

			string year = Convert.ToString(DateTime.Today.Year);
			string key = string.Join("", "PP", input.Project?.ProjectNo, year[2], year[3]);

			var type = "PRM.MasterPreSalePromotion";
			var runningno = await DB.RunningNumberCounters.Where(o => o.Key == key && o.Type == type).FirstOrDefaultAsync();
			if (runningno == null)
			{
				var runningNumberCounter = new RunningNumberCounter
				{
					Key = key,
					Type = type,
					Count = 1
				};
				await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
				await DB.SaveChangesAsync();

				model.PromotionNo = key + runningNumberCounter.Count.ToString("000");
				runningNumberCounter.Count++;
				DB.Entry(runningNumberCounter).State = EntityState.Modified;
				await DB.SaveChangesAsync();
			}
			else
			{
				model.PromotionNo = key + runningno.Count.ToString("000");
				runningno.Count++;
				DB.Entry(runningno).State = EntityState.Modified;
				await DB.SaveChangesAsync();
			}

			if (input.PromotionStatus?.Id == masterCenterPromotionStatusActiveID)
			{
				var allPromotionStatusActive = await DB.MasterPreSalePromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != model.ID && o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
																			 .ToListAsync();

				allPromotionStatusActive.ForEach(o => o.PromotionStatusMasterCenterID = masterCenterPromotionStatusInActiveID);
				DB.MasterPreSalePromotions.UpdateRange(allPromotionStatusActive);
				await DB.SaveChangesAsync();
			}

			await DB.MasterPreSalePromotions.AddAsync(model);
			await DB.SaveChangesAsync();
			var result = await this.GetMasterPreSalePromotionDetailAsync(model.ID);
			return result;
		}

		/// <summary>
		/// ดึง Dropdown Master โปรก่อนขาย
		/// </summary>
		/// <param name="promotionNo"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public async Task<List<MasterPreSalePromotionDropdownDTO>> GetMasterPreSalePromotionDropdownAsync(string promotionNo = null, string name = null, CancellationToken cancellationToken = default)
		{
			var query = DB.MasterPreSalePromotions.AsQueryable().AsNoTracking();
			if (!string.IsNullOrEmpty(promotionNo))
			{
				query = query.Where(o => o.PromotionNo.Contains(promotionNo));
				query = query.OrderBy(o => o.PromotionNo);
			}
			if (!string.IsNullOrEmpty(name))
			{
				query = query.Where(o => o.Name.Contains(name));
				query = query.OrderBy(o => o.Name);
			}
			query = query.Take(100);

			var results = await query.Select(o => MasterPreSalePromotionDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
			return results;
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358687/preview
		/// </summary>
		/// <returns>The master pre sale promotion list async.</returns>
		/// <param name="filter">Filter.</param>
		/// <param name="pageParam">Page parameter.</param>
		/// <param name="sortByParam">Sort by parameter.</param>
		public async Task<MasterPreSalePromotionPaging> GetMasterPreSalePromotionListAsync(MasterPreSalePromotionListFilter filter, PageParam pageParam, MasterPreSalePromotionSortByParam sortByParam, CancellationToken cancellationToken = default)
		{
			IQueryable<MasterPreSalePromotionQueryResult> query = DB.MasterPreSalePromotions.AsNoTracking().Select(o =>
																	  new MasterPreSalePromotionQueryResult
																	  {
																		  Project = o.Project,
																		  MasterPreSalePromotion = o,
																		  PromotionStatus = o.PromotionStatus,
																		  UpdatedBy = o.UpdatedBy
																	  });

			#region Filter
			if (!string.IsNullOrEmpty(filter.PromotionNo))
			{
				query = query.Where(o => o.MasterPreSalePromotion.PromotionNo.Contains(filter.PromotionNo));
			}
			if (!string.IsNullOrEmpty(filter.CompanyCode))
			{
				query = query.Where(o => o.MasterPreSalePromotion.CompanyCode.Contains(filter.CompanyCode));
			}
			if (!string.IsNullOrEmpty(filter.Name))
			{
				query = query.Where(o => o.MasterPreSalePromotion.Name.Contains(filter.Name));
			}
			if (filter.IsUsed != null)
			{
				query = query.Where(o => o.MasterPreSalePromotion.IsUsed == filter.IsUsed);
			}
			if (filter.IsApproved != null)
			{
				query = query.Where(o => o.MasterPreSalePromotion.IsApproved == filter.IsApproved);
			}
			if (filter.ApprovedDateFrom != null)
			{
				query = query.Where(x => x.MasterPreSalePromotion.ApprovedDate >= filter.ApprovedDateFrom);
			}
			if (filter.ApprovedDateTo != null)
			{
				query = query.Where(x => x.MasterPreSalePromotion.ApprovedDate <= filter.ApprovedDateTo);
			}
			if (filter.ApprovedDateFrom != null && filter.ApprovedDateTo != null)
			{
				query = query.Where(x => x.MasterPreSalePromotion.ApprovedDate >= filter.ApprovedDateFrom
									&& x.MasterPreSalePromotion.ApprovedDate <= filter.ApprovedDateTo);
			}
			if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
			{
				query = query.Where(o => o.Project.ID == filter.ProjectID);
			}
			if (!string.IsNullOrEmpty(filter.PromotionStatusKey))
			{
				var promotionStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.PromotionStatusKey
																	  && x.MasterCenterGroupKey == "PromotionStatus")
																	 .Select(x => x.ID).FirstAsync();
				query = query.Where(x => x.MasterPreSalePromotion.PromotionStatusMasterCenterID == promotionStatusMasterCenterID);
			}
			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(x => x.MasterPreSalePromotion.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(x => x.MasterPreSalePromotion.Updated <= filter.UpdatedTo);
			}

			#endregion

			MasterPreSalePromotionDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<MasterPreSalePromotionQueryResult>(pageParam, ref query);

			var queryResults = await query.ToListAsync(cancellationToken);

			var results = queryResults.Select(o => MasterPreSalePromotionDTO.CreateFromQueryResult(o)).ToList();

			return new MasterPreSalePromotionPaging()
			{
				PageOutput = pageOutput,
				MasterPreSalePromotionDTOs = results
			};
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358688/preview
		/// </summary>
		/// <returns>The master pre sale promotion detail async.</returns>
		/// <param name="id">MasterPreSalePromotion.ID</param>
		public async Task<MasterPreSalePromotionDTO> GetMasterPreSalePromotionDetailAsync(Guid id, CancellationToken cancellationToken = default)
		{
			var model = await DB.MasterPreSalePromotions.Where(o => o.ID == id)
														.Include(o => o.Project)
														 .ThenInclude(o => o.Company)
														 .Include(o => o.PromotionStatus)
														 .Include(o => o.UpdatedBy)
														.FirstAsync(cancellationToken);
			var result = await MasterPreSalePromotionDTO.CreateFromModelAsync(model, DB);
			return result;
		}

		public async Task<MasterPreSalePromotionDTO> GetActiveMasterPreSalePromotionDetailAsync(Guid projectID, CancellationToken cancellationToken = default)
		{
			var model = await DB.MasterPreSalePromotions
				.Where(o => o.ProjectID == projectID && o.PromotionStatus.Key == PromotionStatusKeys.Active)
				.Include(o => o.Project)
				.Include(o => o.UpdatedBy)
				.FirstOrDefaultAsync(cancellationToken);
			var result = await MasterPreSalePromotionDTO.CreateFromModelAsync(model, DB);
			return result;
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358688/preview
		/// </summary>
		/// <returns>The master pre sale promotion async.</returns>
		/// <param name="input">Input.</param>
		public async Task<MasterPreSalePromotionDTO> UpdateMasterPreSalePromotionAsync(Guid id, MasterPreSalePromotionDTO input)
		{
			await input.ValidateAsync(DB, true);
			var model = await DB.MasterPreSalePromotions.Where(o => o.ID == id).FirstAsync();
			var masterCenterPromotionStatusActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
													  .Select(o => o.ID)
													  .FirstAsync();
			var masterCenterPromotionStatusInActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
													  .Select(o => o.ID)
													  .FirstAsync();
			var countPromotionStatusActive = await DB.MasterPreSalePromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != id
																					&& o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
																			 .CountAsync();
			if (input.PromotionStatus?.Id == masterCenterPromotionStatusActiveID)
			{
				if (countPromotionStatusActive < 1)
				{
					input.ToModel(ref model);
				}
				else
				{
					var allPromotionStatusActive = await DB.MasterPreSalePromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != id
																		&& o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
																 .ToListAsync();

					allPromotionStatusActive.ForEach(o => o.PromotionStatusMasterCenterID = masterCenterPromotionStatusInActiveID);
					DB.MasterPreSalePromotions.UpdateRange(allPromotionStatusActive);
					await DB.SaveChangesAsync();
					input.ToModel(ref model);
				}
			}
			else
			{
				input.ToModel(ref model);
			}

			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();
			var result = await this.GetMasterPreSalePromotionDetailAsync(model.ID);
			return result;
		}

		/// <summary>
		/// ลบ Master โปรก่อนขาย
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358687/preview
		/// </summary>
		/// <returns>The master pre sale promotion async.</returns>
		/// <param name="id">Identifier.</param>
		public async Task DeleteMasterPreSalePromotionAsync(Guid id)
		{
			var model = await DB.MasterPreSalePromotions.FirstOrDefaultAsync(x => x.ID == id);
			model.IsDeleted = true;
			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master pre sale promotion item list async.</returns>
		/// <param name="pageParam">Page parameter.</param>
		/// <param name="sortByParam">Sort by parameter.</param>
		public async Task<MasterPreSalePromotionItemPaging> GetMasterPreSalePromotionItemListAsync(Guid masterPreSalePromotionID, PageParam pageParam, MasterPreSalePromotionItemSortByParam sortByParam, CancellationToken cancellationToken = default)
		{
			IQueryable<MasterPreSalePromotionItemQueryResult> query = DB.MasterPreSalePromotionItems.AsNoTracking().Include(o => o.PromotionMaterialItem)
																	 .Where(o => o.MasterPreSalePromotionID == masterPreSalePromotionID)
																	 .Select(o =>
																	 new MasterPreSalePromotionItemQueryResult
																	 {
																		 PromotionMaterialItem = o.PromotionMaterialItem,
																		 MasterPreSalePromotionItem = o,
																		 PromotionItemStatus = o.PromotionItemStatus,
																		 WhenPromotionReceive = o.WhenPromotionReceive,
																		 UpdatedBy = o.UpdatedBy,
																		 Created = o.Created

																	 });
			var statusDeleteFromSap = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MaterialItemStatus && o.Key == "102")
									.Select(o => o.ID).FirstOrDefaultAsync(cancellationToken);

			MasterPreSalePromotionItemDTO.SortBy(sortByParam, ref query);



			//var queryResults = await query.OrderByDescending(o => o.MasterPreSalePromotionItem.IsUsed == false).ToListAsync();
			var queryResults = await query.ToListAsync(cancellationToken);
			//var results = queryResults.Select(o => MasterPreSalePromotionItemDTO.CreateFromQueryResult(o)).ToList();
			var results = queryResults.Select(o => MasterPreSalePromotionItemDTO.CreateFromQueryResult(o, statusDeleteFromSap, DB)).ToList();
			List<MasterPreSalePromotionItemDTO> subItems = new List<MasterPreSalePromotionItemDTO>();
			foreach (var item in results.ToList())
			{
				if (item.MainPromotionItemID != null)
				{
					subItems.Add(item);
					results.Remove(item);
				}
			}

			var i = 0;
			foreach (var item in results.ToList())
			{
				var subs = subItems.Where(o => o.MainPromotionItemID == item.Id).ToList();
				if (subs.Count() > 0)
				{
					MasterPreSalePromotionItemDTO.SortByDTO(sortByParam, ref subs);
					results.InsertRange(i + 1, subs);
					i++;
					i += subs.Count();
				}
				else
				{
					i++;
				}
			}

			var pageOutput = PagingHelper.PagingList<MasterPreSalePromotionItemDTO>(pageParam, ref results);

			return new MasterPreSalePromotionItemPaging()
			{
				PageOutput = pageOutput,
				MasterPreSalePromotionItemDTOs = results
			};
		}

		/// <summary>
		/// เรียกใช้ตอนกดปุ่ม "บันทึก"
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master pre sale promotion item list async.</returns>
		/// <param name="inputs">Inputs.</param>
		public async Task<List<MasterPreSalePromotionItemDTO>> UpdateMasterPreSalePromotionItemListAsync(Guid masterPreSalePromotionID, List<MasterPreSalePromotionItemDTO> inputs)
		{
			foreach (var item in inputs)
			{
				await item.ValidateAsync(DB);
			}
			var listMasterPreSalePromotionItems = new List<MasterPreSalePromotionItem>();
			var allMasterPreSalePromotionItems = await DB.MasterPreSalePromotionItems.Where(o => o.MasterPreSalePromotionID == masterPreSalePromotionID).ToListAsync();
			foreach (var item in inputs)
			{
				var existingItem = allMasterPreSalePromotionItems.Where(o => o.ID == item.Id).FirstOrDefault();
				if (existingItem != null)
				{
					item.ToModel(ref existingItem);
					//if (existingItem.MainPromotionItemID == null)
					//{
					existingItem.TotalPrice = (int)Math.Ceiling(existingItem.Quantity * existingItem.PricePerUnit);
					//}
					listMasterPreSalePromotionItems.Add(existingItem);
				}
			}

			DB.UpdateRange(listMasterPreSalePromotionItems);
			await DB.SaveChangesAsync();

			var queryResults = await DB.MasterPreSalePromotionItems
										.Where(o => o.MasterPreSalePromotionID == masterPreSalePromotionID)
										.Include(o => o.PromotionMaterialItem)
										.Include(o => o.WhenPromotionReceive)
										.Include(o => o.PromotionItemStatus)
										.Include(o => o.UpdatedBy)
										.ToListAsync();

			var results = queryResults.Select(o => MasterPreSalePromotionItemDTO.CreateFromModel(o)).ToList();
			return results;
		}

		public async Task<MasterPreSalePromotionItemDTO> UpdateMasterPreSalePromotionItemAsync(Guid masterPreSalePromotionID, Guid masterPreSalePromotionItemID, MasterPreSalePromotionItemDTO input)
		{
			await input.ValidateAsync(DB);
			decimal sumPriceChildOld = DB.MasterPreSalePromotionItems.Where(o => o.MainPromotionItemID == input.MainPromotionItemID).Sum(o => o.TotalPrice);
			var model = await DB.MasterPreSalePromotionItems.Where(o => o.MasterPreSalePromotionID == masterPreSalePromotionID && o.ID == masterPreSalePromotionItemID).FirstOrDefaultAsync();
			input.ToModel(ref model);
			if (model.MainPromotionItemID == null) //parent
			{
				model.PricePerUnit = (int)Math.Ceiling(model.PricePerUnit);
			}
			else
			{
				model.PricePerUnit = (int)Math.Ceiling(model.PricePerUnit);
				model.TotalPrice = (int)Math.Ceiling(model.Quantity * model.PricePerUnit);
			}


			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();



			//IsChild UpdateParent PricePerUnit && TotalPrice
			if (model.MainPromotionItemID != null)
			{
				decimal sumPriceChildNew = DB.MasterPreSalePromotionItems.Where(o => o.MainPromotionItemID == input.MainPromotionItemID).Sum(o => o.TotalPrice);
				var masterMain = await DB.MasterPreSalePromotionItems.Where(o => o.ID == input.MainPromotionItemID).FirstOrDefaultAsync();
				masterMain.PricePerUnit = (int)Math.Ceiling((masterMain.PricePerUnit - sumPriceChildOld) + sumPriceChildNew);
				masterMain.TotalPrice = (int)Math.Ceiling((masterMain.TotalPrice - sumPriceChildOld) + sumPriceChildNew);

				DB.UpdateRange(masterMain);
				await DB.SaveChangesAsync();
			}
			else
			{
				//Update Child Status follow parent
				if (model.ID != null)
				{
					var childList = await DB.MasterPreSalePromotionItems.Where(o => o.MainPromotionItemID == model.ID).ToListAsync();
					var childMasterPreSalePromotionItem = new List<MasterPreSalePromotionItem>();
					foreach (MasterPreSalePromotionItem mpsp in childList)
					{
						mpsp.WhenPromotionReceiveMasterCenterID = input.WhenPromotionReceive?.Id;
						childMasterPreSalePromotionItem.Add(mpsp);
					}
					DB.UpdateRange(childMasterPreSalePromotionItem);
					await DB.SaveChangesAsync();
				}
			}

			if (input.PromotionItemStatus?.Key == "0") //InActive
			{
				var promotionItemStatusID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "0").Select(o => o.ID).FirstOrDefaultAsync();
				//Update Inactive all
				if (model.MainPromotionItemID != null) //input child >> update main and child
				{
					var main = await DB.MasterPreSalePromotionItems.Where(o => o.ID == model.MainPromotionItemID).FirstOrDefaultAsync();
					main.PromotionItemStatusMasterCenterID = promotionItemStatusID;
					DB.Entry(main).State = EntityState.Modified;

					var childList = await DB.MasterPreSalePromotionItems.Where(o => o.MainPromotionItemID == model.MainPromotionItemID).ToListAsync();
					var childMasterPreSalePromotionItem = new List<MasterPreSalePromotionItem>();
					foreach (MasterPreSalePromotionItem mpsp in childList)
					{
						mpsp.PromotionItemStatusMasterCenterID = promotionItemStatusID;
						childMasterPreSalePromotionItem.Add(mpsp);
					}
					DB.UpdateRange(childMasterPreSalePromotionItem);
					await DB.SaveChangesAsync();
				}
				else //input main >> update child
				{
					if (model.ID != null)
					{
						var childList = await DB.MasterPreSalePromotionItems.Where(o => o.MainPromotionItemID == model.ID).ToListAsync();
						var childMasterPreSalePromotionItem = new List<MasterPreSalePromotionItem>();
						foreach (MasterPreSalePromotionItem mpsp in childList)
						{
							mpsp.PromotionItemStatusMasterCenterID = promotionItemStatusID;
							childMasterPreSalePromotionItem.Add(mpsp);
						}
						DB.UpdateRange(childMasterPreSalePromotionItem);
						await DB.SaveChangesAsync();
					}
				}
			}

			var dataResult = await DB.MasterPreSalePromotionItems.Where(o => o.MasterPreSalePromotionID == masterPreSalePromotionID
																		&& o.ID == masterPreSalePromotionItemID)
																  .Include(o => o.PromotionMaterialItem)
																  .Include(o => o.WhenPromotionReceive)
																  .Include(o => o.PromotionItemStatus)
																  .Include(o => o.UpdatedBy)
																  .FirstAsync();
			var result = MasterPreSalePromotionItemDTO.CreateFromModel(dataResult);
			return result;
		}

		/// <summary>
		/// ลบทีละอัน ตรงปุ่ม ถังขยะ
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master pre sale promotion item async.</returns>
		/// <param name="id">Identifier.</param>
		public async Task DeleteMasterPreSalePromotionItemAsync(Guid id)
		{
			var model = await DB.MasterPreSalePromotionItems.Where(x => x.ID == id).FirstAsync();
			model.IsDeleted = true;
			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();

			//Update Parent
			if (model.MainPromotionItemID != null)
			{
				var modelMain = await DB.MasterPreSalePromotionItems.Where(x => x.ID == model.MainPromotionItemID).FirstAsync();
				//modelMain.PricePerUnit = (int)Math.Ceiling(modelMain.PricePerUnit - model.TotalPrice);
				modelMain.TotalPrice = (int)Math.Ceiling(modelMain.TotalPrice - model.TotalPrice);
				DB.UpdateRange(modelMain);
				await DB.SaveChangesAsync();

			}
		}

		/// <summary>
		/// เพิ่มรายการโปรโมชั่นโดยเลือกจาก Material ที่ดึงจาก SAP
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358682/preview
		/// </summary>
		/// <returns>The master pre sale promotion item from material async.</returns>
		/// <param name="inputs">ส่งเฉพาะ Item ที่เลือก</param>
		public async Task<List<MasterPreSalePromotionItemDTO>> CreateMasterPreSalePromotionItemFromMaterialAsync(Guid masterPreSalePromotionID, List<PromotionMaterialDTO> inputs)
		{
			var masterPreSalePromotionItemsCreate = new List<MasterPreSalePromotionItem>();
			var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
			var whenPromotionReceiveAfterContract = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
			foreach (var item in inputs)
			{
				await ValidatePromotionMaterial(masterPreSalePromotionID, item);

				//if (await MasterPreSalePromotionMaterialHasValue(item, masterPreSalePromotionID))
				//{
				//	continue;
				//}

				MasterPreSalePromotionItem model = new MasterPreSalePromotionItem();
				item.ToMasterPreSalePromotionItemModel(ref model);
				model.MasterPreSalePromotionID = masterPreSalePromotionID;
				model.MainPromotionItemID = null;
				model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
				model.WhenPromotionReceiveMasterCenterID = whenPromotionReceiveAfterContract.ID;
				var promotionMaterialItem = await DB.PromotionMaterialItems.Where(o => o.ID == item.Id).FirstOrDefaultAsync();
				// SapInfo
				if (promotionMaterialItem != null)
				{
					model.StartDate = promotionMaterialItem.StartDate;
					//model.ExpireDate = promotionMaterialItem.ExpireDate;
					model.ItemNo = promotionMaterialItem.ItemNo;
					model.BrandEN = promotionMaterialItem.BrandEN;
					model.BrandTH = promotionMaterialItem.BrandTH;
					model.UnitEN = promotionMaterialItem.UnitEN;
					model.RemarkTH = promotionMaterialItem.RemarkTH;
					model.RemarkEN = promotionMaterialItem.RemarkEN;
					model.SpecEN = promotionMaterialItem.SpecEN;
					model.SpecTH = promotionMaterialItem.SpecTH;
					model.Plant = promotionMaterialItem.Plant;
					model.SAPCompanyID = promotionMaterialItem.SAPCompanyID;
					model.AgreementNo = promotionMaterialItem.AgreementNo;
					model.SAPPurchasingOrg = promotionMaterialItem.SAPPurchasingOrg;
					model.MaterialGroupKey = promotionMaterialItem.MaterialGroupKey;
					model.DocType = promotionMaterialItem.DocType;
					model.GLAccountNo = promotionMaterialItem.GLAccountNo;
					model.MaterialPrice = promotionMaterialItem.Price;
					model.MaterialBasePrice = promotionMaterialItem.BasePrice;
					model.Vat = promotionMaterialItem.Vat;
					model.SAPSaleTaxCode = promotionMaterialItem.SAPSaleTaxCode;
					model.SAPBaseUnit = promotionMaterialItem.SAPBaseUnit;
					model.SAPVendor = promotionMaterialItem.SAPVendor;
					model.SAPPurchasingGroup = promotionMaterialItem.SAPPurchasingGroup;
					model.MaterialCode = promotionMaterialItem.MaterialCode;
				}
				masterPreSalePromotionItemsCreate.Add(model);
			}
			await DB.AddRangeAsync(masterPreSalePromotionItemsCreate);
			await DB.SaveChangesAsync();

			var createdIDs = masterPreSalePromotionItemsCreate.Select(o => o.ID).ToList();
			var dataResult = await DB.MasterPreSalePromotionItems.Where(o => createdIDs.Contains(o.ID))
																  .Include(o => o.PromotionMaterialItem)
																  .Include(o => o.WhenPromotionReceive)
																  .Include(o => o.PromotionItemStatus)
																  .Include(o => o.UpdatedBy)
																  .ToListAsync();

			var result = dataResult.Select(o => MasterPreSalePromotionItemDTO.CreateFromModel(o)).ToList();
			return result;
		}

		/// <summary>
		/// เพิ่มรายการย่อยจากการเลือก Material 
		/// รายการหลักกับรายการย่อยใช้ DTO เดียวกัน เหมือนกับ Model
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358682/preview
		/// </summary>
		/// <returns>The sub master pre sale promotion item from material async.</returns>
		/// <param name="inputs">ส่งมาเฉพาะรายการที่เลือกมาเท่านั้น</param>
		public async Task<List<MasterPreSalePromotionItemDTO>> CreateSubMasterPreSalePromotionItemFromMaterialAsync(Guid masterPreSalePromotionID, Guid mainMasterPreSalePromotionItemID, List<PromotionMaterialDTO> inputs)
		{
			var masterPreSalePromotionItemsCreate = new List<MasterPreSalePromotionItem>();
			var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
			var whenPromotionReceiveAfterContract = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
			decimal sumPriceChildOld = DB.MasterPreSalePromotionItems.Where(o => o.MainPromotionItemID == mainMasterPreSalePromotionItemID).Sum(o => o.TotalPrice);

			foreach (var item in inputs)
			{
				await ValidatePromotionMaterial(masterPreSalePromotionID, item);
				if (await SubMasterPreSalePromotionMaterialHasValue(item, mainMasterPreSalePromotionItemID, masterPreSalePromotionID))
				{
					continue;
				}
				MasterPreSalePromotionItem model = new MasterPreSalePromotionItem();
				item.ToMasterPreSalePromotionItemModel(ref model);
				model.MasterPreSalePromotionID = masterPreSalePromotionID;
				model.MainPromotionItemID = mainMasterPreSalePromotionItemID;
				model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
				model.WhenPromotionReceiveMasterCenterID = whenPromotionReceiveAfterContract.ID;

				var promotionMaterialItem = await DB.PromotionMaterialItems.Where(o => o.ID == item.Id).FirstOrDefaultAsync();
				// SapInfo
				if (promotionMaterialItem != null)
				{
					model.StartDate = promotionMaterialItem.StartDate;
					//model.ExpireDate = promotionMaterialItem.ExpireDate;
					model.ItemNo = promotionMaterialItem.ItemNo;
					model.BrandEN = promotionMaterialItem.BrandEN;
					model.BrandTH = promotionMaterialItem.BrandTH;
					model.UnitEN = promotionMaterialItem.UnitEN;
					model.RemarkTH = promotionMaterialItem.RemarkTH;
					model.RemarkEN = promotionMaterialItem.RemarkEN;
					model.SpecEN = promotionMaterialItem.SpecEN;
					model.SpecTH = promotionMaterialItem.SpecTH;
					model.Plant = promotionMaterialItem.Plant;
					model.SAPCompanyID = promotionMaterialItem.SAPCompanyID;
					model.AgreementNo = promotionMaterialItem.AgreementNo;
					model.SAPPurchasingOrg = promotionMaterialItem.SAPPurchasingOrg;
					model.MaterialGroupKey = promotionMaterialItem.MaterialGroupKey;
					model.DocType = promotionMaterialItem.DocType;
					model.GLAccountNo = promotionMaterialItem.GLAccountNo;
					model.MaterialPrice = promotionMaterialItem.Price;
					model.MaterialBasePrice = promotionMaterialItem.BasePrice;
					model.Vat = promotionMaterialItem.Vat;
					model.SAPSaleTaxCode = promotionMaterialItem.SAPSaleTaxCode;
					model.SAPBaseUnit = promotionMaterialItem.SAPBaseUnit;
					model.SAPVendor = promotionMaterialItem.SAPVendor;
					model.SAPPurchasingGroup = promotionMaterialItem.SAPPurchasingGroup;
					model.MaterialCode = promotionMaterialItem.MaterialCode;
					//model.TotalPrice = (int)Math.Ceiling(promotionMaterialItem.Price);
					//model.PricePerUnit = (int)Math.Ceiling(promotionMaterialItem.Price);
				}
				masterPreSalePromotionItemsCreate.Add(model);
			}
			await DB.AddRangeAsync(masterPreSalePromotionItemsCreate);
			await DB.SaveChangesAsync();

			//Calc Main
			//decimal sumPriceChildNew = DB.MasterPreSalePromotionItems.Where(o => o.MainPromotionItemID == mainMasterPreSalePromotionItemID).Sum(o => o.TotalPrice);
			//var masterMain = await DB.MasterPreSalePromotionItems.Where(o => o.ID == mainMasterPreSalePromotionItemID).FirstOrDefaultAsync();
			//int pricePerUnit = (int)Math.Ceiling((masterMain.PricePerUnit - sumPriceChildOld) + sumPriceChildNew);
			//int totalPrice = (int)Math.Ceiling((masterMain.TotalPrice - sumPriceChildOld) + sumPriceChildNew);
			//masterMain.PricePerUnit = pricePerUnit;
			//masterMain.TotalPrice = totalPrice;

			decimal sumPriceChildNew = DB.MasterPreSalePromotionItems.Where(o => o.MainPromotionItemID == mainMasterPreSalePromotionItemID).Sum(o => o.TotalPrice);
			var masterMain = await DB.MasterPreSalePromotionItems.Where(o => o.ID == mainMasterPreSalePromotionItemID).FirstOrDefaultAsync();
			int totalPrice = (int)Math.Ceiling((masterMain.TotalPrice - sumPriceChildOld) + sumPriceChildNew);
			masterMain.TotalPrice = totalPrice;

			DB.UpdateRange(masterMain);
			await DB.SaveChangesAsync();

			var createdIDs = masterPreSalePromotionItemsCreate.Select(o => o.ID).ToList();
			var dataResult = await DB.MasterPreSalePromotionItems.Where(o => createdIDs.Contains(o.ID))
																  .Include(o => o.PromotionMaterialItem)
																  .Include(o => o.WhenPromotionReceive)
																  .Include(o => o.PromotionItemStatus)
																  .Include(o => o.UpdatedBy)
																  .ToListAsync();

			var result = dataResult.Select(o => MasterPreSalePromotionItemDTO.CreateFromModel(o)).ToList();
			return result;
		}

		/// <summary>
		/// ดึงแบบบ้านจาก Item โปรโอน
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
		/// </summary>
		/// <returns>The master pre sale promotion item model list async.</returns>
		/// <param name="masterPreSalePromotionItemID">Master pre sale promotion item identifier.</param>
		public async Task<List<ModelListDTO>> GetMasterPreSalePromotionItemModelListAsync(Guid masterPreSalePromotionItemID, CancellationToken cancellationToken = default)
		{
			var listModel = await DB.MasterPreSaleHouseModelItems.AsNoTracking().Where(o => o.MasterPreSalePromotionItemID == masterPreSalePromotionItemID).ToListAsync(cancellationToken);
			var results = new List<ModelListDTO>();
			foreach (var item in listModel)
			{
				ModelQueryResult model = await DB.Models.Where(o => o.ID == item.ModelID)
														.Select(o =>
																new ModelQueryResult
																{
																	Model = o,
																	ModelShortName = o.ModelShortName,
																	ModelType = o.ModelType,
																	ModelUnitType = o.ModelUnitType,
																	TypeOfRealEstate = o.TypeOfRealEstate,
																	WaterElectricMeterPrice = DB.WaterElectricMeterPrices.Where(p => p.ModelID == o.ID).OrderByDescending(p => p.Version).FirstOrDefault()
																})
														.FirstOrDefaultAsync(cancellationToken);
				if (model != null)
				{
					results.Add(ModelListDTO.CreateFromQueryResult(model));
				}
			}
			return results;
		}

		/// <summary>
		/// เพิ่มแบบบ้านเข้าไปใน Item โปรโอน
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
		/// </summary>
		/// <returns>The master pre sale promotion item model list async.</returns>
		/// <param name="masterPreSalePromotionItemID">Master pre sale promotion item identifier.</param>
		/// <param name="inputs">Inputs.</param>
		public async Task<List<ModelListDTO>> AddMasterPreSalePromotionItemModelListAsync(Guid masterPreSalePromotionItemID, List<ModelListDTO> inputs)
		{
			var masterPreSaleHouseModelItemsCreate = new List<MasterPreSaleHouseModelItem>();
			var masterPreSaleHouseModelItemsUpdate = new List<MasterPreSaleHouseModelItem>();
			var masterPreSaleHouseModelItemsDelete = new List<MasterPreSaleHouseModelItem>();
			var masterPreSaleHouseModelItems = await DB.MasterPreSaleHouseModelItems.Where(o => o.MasterPreSalePromotionItemID == masterPreSalePromotionItemID).ToListAsync();

			foreach (var item in inputs)
			{
				var existingItem = masterPreSaleHouseModelItems.Where(o => o.ModelID == item.Id).FirstOrDefault();
				if (existingItem == null)
				{
					MasterPreSaleHouseModelItem model = new MasterPreSaleHouseModelItem();
					model.MasterPreSalePromotionItemID = masterPreSalePromotionItemID;
					model.ModelID = item.Id;
					masterPreSaleHouseModelItemsCreate.Add(model);
				}
				else
				{
					masterPreSaleHouseModelItemsUpdate.Add(existingItem);
				}
			}
			foreach (var item in masterPreSaleHouseModelItems)
			{
				var existingInput = inputs.Where(o => o.Id == item.ModelID).FirstOrDefault();
				if (existingInput == null)
				{
					item.IsDeleted = true;
					masterPreSaleHouseModelItemsDelete.Add(item);
				}
			}
			DB.UpdateRange(masterPreSaleHouseModelItemsUpdate);
			DB.UpdateRange(masterPreSaleHouseModelItemsDelete);
			await DB.AddRangeAsync(masterPreSaleHouseModelItemsCreate);
			await DB.SaveChangesAsync();

			var results = await this.GetMasterPreSalePromotionItemModelListAsync(masterPreSalePromotionItemID);
			return results;
		}

		public async Task<MasterPreSalePromotionDTO> CloneMasterPreSalePromotionAsync(Guid id)
		{

			var statusDeleteFromSapId = await DB.MasterCenters
										.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MaterialItemStatus
													&& o.Key == "102")
										.Select(o => o.ID)
										.FirstOrDefaultAsync();

			var statusActiveId = await DB.MasterCenters
							.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionItemStatus
										&& o.Key == "1")
							.Select(o => o.ID)
							.FirstOrDefaultAsync();

			var promotionMaterialGroups = await DB.PromotionMaterialGroups.ToListAsync();
			var promotionMaterialAddPrices = await DB.PromotionMaterialAddPrices.ToListAsync();


			var masterPreSalePromotion = await DB.MasterPreSalePromotions.Where(o => o.ID == id).Include(o => o.Project).FirstAsync();
			var masterPreSalePromotionItems = await DB.MasterPreSalePromotionItems.Where(o => o.MasterPreSalePromotionID == id
																							&& !string.IsNullOrEmpty(o.ItemNo)
																							&& (!string.IsNullOrEmpty(o.MaterialCode) || o.AgreementNo.ToLower().Contains("welcomehome"))
																							&& (!string.IsNullOrEmpty(o.GLAccountNo) || o.AgreementNo.ToLower().Contains("welcomehome"))
																							&& !string.IsNullOrEmpty(o.MaterialGroupKey)
																							&& o.ExpireDate > DateTime.Now
																							&& o.PromotionItemStatusMasterCenterID == statusActiveId
																							&& o.PromotionMaterialItemID != null //1
																						 ).ToListAsync();
			//2
			var promotionMaterialItems = new List<PromotionMaterialItem>();
			if (masterPreSalePromotionItems.Count > 0)
			{
				var promotionMaterialItemsID = masterPreSalePromotionItems.Select(o => o.PromotionMaterialItemID).ToList();
				promotionMaterialItems = await DB.PromotionMaterialItems.Where(o => promotionMaterialItemsID.Contains(o.ID)).ToListAsync();
			}

			var promotionStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == "0"
																   && x.MasterCenterGroupKey == "PromotionStatus")
																  .Select(x => x.ID).FirstAsync();

			var productType = await DB.MasterCenters.Where(o => o.ID == masterPreSalePromotion.Project.ProductTypeMasterCenterID).FirstOrDefaultAsync();

			var newMasterPreSalePromotion = new MasterPreSalePromotion
			{
				Name = masterPreSalePromotion.Name,
				ProjectID = masterPreSalePromotion.ProjectID,
				CompanyCode = masterPreSalePromotion.CompanyCode,
				Plant = masterPreSalePromotion.Plant,
				IsApproved = masterPreSalePromotion.IsApproved,
				ApprovedDate = masterPreSalePromotion.ApprovedDate,
				PromotionStatusMasterCenterID = promotionStatusMasterCenterID,
				IsUsed = false,
				RefMigrateID1 = "ClonedFromProm.No : " + masterPreSalePromotion.PromotionNo?.ToString()
			};

			string year = Convert.ToString(DateTime.Today.Year);
			var key = "PP" + masterPreSalePromotion.Project?.ProjectNo + year[2] + year[3];
			var type = "PRM.MasterPreSalePromotion";
			var runningno = await DB.RunningNumberCounters.Where(o => o.Key == key && o.Type == type).FirstOrDefaultAsync();
			if (runningno == null)
			{
				var runningNumberCounter = new RunningNumberCounter
				{
					Key = key,
					Type = type,
					Count = 1
				};
				await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
				await DB.SaveChangesAsync();

				newMasterPreSalePromotion.PromotionNo = key + runningNumberCounter.Count.ToString("000");
				runningNumberCounter.Count++;
				DB.Entry(runningNumberCounter).State = EntityState.Modified;
				await DB.SaveChangesAsync();
			}
			else
			{
				newMasterPreSalePromotion.PromotionNo = key + runningno.Count.ToString("000");
				runningno.Count++;
				DB.Entry(runningno).State = EntityState.Modified;
				await DB.SaveChangesAsync();
			}

			var newMasterPreSalePromotionItems = new List<MasterPreSalePromotionItem>();
			var newMasterPreSaleHouseModelItems = new List<MasterPreSaleHouseModelItem>();
			var listPromotionMaterialitem = new List<Guid?>();
			//Add ParentPromotionItems.
			foreach (var item in masterPreSalePromotionItems.Where(o => o.MainPromotionItemID == null))
			{
				//3
				var sapSaleTaxCode = promotionMaterialItems.Where(o => o.ID == item.PromotionMaterialItemID).Select(o => o.SAPSaleTaxCode).FirstOrDefault();
				var masterItem = promotionMaterialItems.Where(o => o.ID == item.PromotionMaterialItemID).FirstOrDefault();
				double percentMarkUP = 0;
				double totalPriceInCludeVat = 0;
				var promotionmaterialGroup = promotionMaterialGroups.Where(o => o.Key == item.MaterialGroupKey).FirstOrDefault();
				var promotionmaterialAddPrice = promotionmaterialGroup != null ? promotionMaterialAddPrices.Where(o => o.PromotionMaterialGroupID == promotionmaterialGroup.ID).FirstOrDefault() : null;

				if (productType.Key.Equals("1"))
				{
					percentMarkUP = promotionmaterialAddPrice == null ? 0 : promotionmaterialAddPrice.LowRisePercent;
				}
				else
				{
					percentMarkUP = promotionmaterialAddPrice == null ? 0 : promotionmaterialAddPrice.HighRisePercent;
				}

				//var vat7Pct = item.SAPSaleTaxCode == "VX"
				//			 || string.IsNullOrEmpty(item.SAPSaleTaxCode) ? 0 : Convert.ToDecimal(1.07);
				var vat7Pct = sapSaleTaxCode == "VX"
							 || string.IsNullOrEmpty(sapSaleTaxCode) ? 0 : Convert.ToDecimal(1.07);

				//case vat7 = 0 : (BasePrice) + (BasePrice * (Low/HighRisePercent / 100))
				//case vat7 = 7 : (BasePrice * 1.07) + ((BasePrice * 1.07) * (Low/HighRisePercent / 100))
				var pricePerUnitIncludeVat7 = vat7Pct == 0 ?
							   (masterItem.BasePrice) +
							   (((masterItem.BasePrice) * Convert.ToDecimal(percentMarkUP)) / 100) :
							   (masterItem.BasePrice * Convert.ToDecimal(1.07)) +
							   (((masterItem.BasePrice * Convert.ToDecimal(1.07)) * Convert.ToDecimal(percentMarkUP)) / 100);

				totalPriceInCludeVat += (int)Math.Ceiling(pricePerUnitIncludeVat7);

				// check duplicate promotion
				var checkSubPromotionitem = await DB.MasterPreSalePromotionItems.Where(o => o.MainPromotionItemID == item.ID).ToListAsync();
				if (checkSubPromotionitem.Count == 0)
				{
					if (listPromotionMaterialitem.Contains(item.PromotionMaterialItemID))
					{
						continue;
					}
					listPromotionMaterialitem.Add(item.PromotionMaterialItemID);
				}

				string sqlQuery = sqlDupPreSalePromotion.QueryString;

				using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
				DynamicParameters ParamList = sqlDupPreSalePromotion.QueryFilter(ref sqlQuery, id);
				CommandDefinition commandDefinition = new(
													  commandText: sqlQuery,
													  parameters: ParamList,
													  transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
													  commandType: CommandType.Text);
				var getDupSalePromo = (await cmd.Connection.QueryAsync<sqlDupPreSalePromotion.QueryResult>(commandDefinition))?.ToList() ?? new();
				var listGetDupSalePromo = getDupSalePromo.Select(o => o.MasterPreSalePromoItemID).ToList().FirstOrDefault();

				//if (listGetDupSalePromo == item.ID)
				//{
				//	continue;
				//}

				var model = new MasterPreSalePromotionItem
				{
					MasterPreSalePromotionID = newMasterPreSalePromotion.ID,
					NameTH = item.NameTH,
					NameEN = item.NameEN,
					Quantity = item.Quantity,
					UnitTH = item.UnitTH,
					UnitEN = item.UnitEN,
					//PricePerUnit = item.PricePerUnit,
					PricePerUnit = (int)Math.Ceiling(pricePerUnitIncludeVat7),
					//TotalPrice = item.TotalPrice,
					ReceiveDays = item.ReceiveDays,
					WhenPromotionReceiveMasterCenterID = item.WhenPromotionReceiveMasterCenterID,
					IsPurchasing = item.IsPurchasing,
					IsShowInContract = item.IsShowInContract,
					PromotionItemStatusMasterCenterID = item.PromotionItemStatusMasterCenterID,
					ExpireDate = item.ExpireDate,
					PromotionMaterialItemID = item.PromotionMaterialItemID,
					MainPromotionItemID = null,
					IsUsed = false
				};

				//Sap info
				model.StartDate = item.StartDate;
				model.ItemNo = item.ItemNo;
				model.BrandEN = item.BrandEN;
				model.BrandTH = item.BrandTH;
				model.UnitEN = item.UnitEN;
				model.RemarkTH = item.RemarkTH;
				model.RemarkEN = item.RemarkEN;
				model.SpecEN = item.SpecEN;
				model.SpecTH = item.SpecTH;
				model.Plant = item.Plant;
				model.SAPCompanyID = item.SAPCompanyID;
				model.AgreementNo = item.AgreementNo;
				model.SAPPurchasingOrg = item.SAPPurchasingOrg;
				model.MaterialGroupKey = item.MaterialGroupKey;
				model.DocType = item.DocType;
				model.GLAccountNo = item.GLAccountNo;
				model.MaterialPrice = masterItem.Price;
				model.MaterialBasePrice = masterItem.BasePrice;
				model.Vat = masterItem.Vat;
				model.SAPSaleTaxCode = masterItem.SAPSaleTaxCode;
				model.SAPBaseUnit = item.SAPBaseUnit;
				model.SAPVendor = item.SAPVendor;
				model.SAPPurchasingGroup = item.SAPPurchasingGroup;
				model.MaterialCode = item.MaterialCode;

				var masterPreSaleHouseItem = await DB.MasterPreSaleHouseModelItems.Where(o => o.MasterPreSalePromotionItemID == item.ID).ToListAsync();
				foreach (var house in masterPreSaleHouseItem)
				{
					var newhouse = new MasterPreSaleHouseModelItem
					{
						MasterPreSalePromotionItemID = model.ID,
						ModelID = house.ModelID
					};
					newMasterPreSaleHouseModelItems.Add(newhouse);
				}
				//newMasterPreSalePromotionItems.Add(model);

				//Add SubPromotionItems
				var listSub = masterPreSalePromotionItems.Where(o => o.MainPromotionItemID == item.ID).ToList();
				foreach (var item1 in listSub)
				{

					var sapSaleTaxCodeSub = promotionMaterialItems.Where(o => o.ID == item1.PromotionMaterialItemID).Select(o => o.SAPSaleTaxCode).FirstOrDefault();
					var masterSubItem = promotionMaterialItems.Where(o => o.ID == item1.PromotionMaterialItemID).FirstOrDefault();
					//var subvat7Pct = item1.SAPSaleTaxCode == "VX" || string.IsNullOrEmpty(item1.SAPSaleTaxCode) ? 0 : Convert.ToDecimal(1.07);
					var subvat7Pct = sapSaleTaxCodeSub == "VX" || string.IsNullOrEmpty(sapSaleTaxCodeSub) ? 0 : Convert.ToDecimal(1.07);

					//case vat7 = 0 : (BasePrice) + (BasePrice * (Low/HighRisePercent / 100))
					//case vat7 = 7 : (BasePrice * 1.07) + ((BasePrice * 1.07) * (Low/HighRisePercent / 100))
					var subpricePerUnitIncludeVat7 = subvat7Pct == 0 ?
								   (masterSubItem.BasePrice) +
								   (((masterSubItem.BasePrice) * Convert.ToDecimal(percentMarkUP)) / 100) :
								   (masterSubItem.BasePrice * Convert.ToDecimal(1.07)) +
								   (((masterSubItem.BasePrice * Convert.ToDecimal(1.07)) * Convert.ToDecimal(percentMarkUP)) / 100);

					totalPriceInCludeVat += (int)Math.Ceiling(subpricePerUnitIncludeVat7);

					var modelSub = new MasterPreSalePromotionItem
					{
						MasterPreSalePromotionID = newMasterPreSalePromotion.ID,
						NameTH = item1.NameTH,
						NameEN = item1.NameEN,
						Quantity = item1.Quantity,
						UnitTH = item1.UnitTH,
						UnitEN = item1.UnitEN,
						//PricePerUnit = item1.PricePerUnit,
						//TotalPrice = item1.TotalPrice,
						PricePerUnit = (int)Math.Ceiling(subpricePerUnitIncludeVat7),
						TotalPrice = (int)Math.Ceiling(subpricePerUnitIncludeVat7),
						ReceiveDays = item1.ReceiveDays,
						WhenPromotionReceiveMasterCenterID = item1.WhenPromotionReceiveMasterCenterID,
						IsPurchasing = item1.IsPurchasing,
						IsShowInContract = item1.IsShowInContract,
						PromotionItemStatusMasterCenterID = item1.PromotionItemStatusMasterCenterID,
						ExpireDate = item1.ExpireDate,
						MainPromotionItemID = model.ID,
						PromotionMaterialItemID = item1.PromotionMaterialItemID,
						IsUsed = false
					};
					//Sap info
					modelSub.StartDate = item1.StartDate;
					modelSub.ItemNo = item1.ItemNo;
					modelSub.BrandEN = item1.BrandEN;
					modelSub.BrandTH = item1.BrandTH;
					modelSub.UnitEN = item1.UnitEN;
					modelSub.RemarkTH = item1.RemarkTH;
					modelSub.RemarkEN = item1.RemarkEN;
					modelSub.SpecEN = item1.SpecEN;
					modelSub.SpecTH = item1.SpecTH;
					modelSub.Plant = item1.Plant;
					modelSub.SAPCompanyID = item1.SAPCompanyID;
					modelSub.AgreementNo = item1.AgreementNo;
					modelSub.SAPPurchasingOrg = item1.SAPPurchasingOrg;
					modelSub.MaterialGroupKey = item1.MaterialGroupKey;
					modelSub.DocType = item1.DocType;
					modelSub.GLAccountNo = item1.GLAccountNo;
					modelSub.MaterialPrice = masterSubItem.Price;
					modelSub.MaterialBasePrice = masterSubItem.BasePrice;
					modelSub.Vat = masterSubItem.Vat;
					modelSub.SAPSaleTaxCode = masterSubItem.SAPSaleTaxCode;
					modelSub.SAPBaseUnit = item1.SAPBaseUnit;
					modelSub.SAPVendor = item1.SAPVendor;
					modelSub.SAPPurchasingGroup = item1.SAPPurchasingGroup;
					modelSub.MaterialCode = item1.MaterialCode;
					newMasterPreSalePromotionItems.Add(modelSub);
					var masterPreSaleHouseItemSub = await DB.MasterPreSaleHouseModelItems.Where(o => o.MasterPreSalePromotionItemID == item1.ID).ToListAsync();
					foreach (var house1 in masterPreSaleHouseItemSub)
					{
						var newhousesub = new MasterPreSaleHouseModelItem
						{
							MasterPreSalePromotionItemID = modelSub.ID,
							ModelID = house1.ModelID
						};
						newMasterPreSaleHouseModelItems.Add(newhousesub);
					}
				}

				//Add Total MainPromotionItems
				model.TotalPrice = (int)Math.Ceiling(totalPriceInCludeVat);
				newMasterPreSalePromotionItems.Add(model);
			}

			await DB.MasterPreSalePromotions.AddAsync(newMasterPreSalePromotion);

			if (newMasterPreSalePromotionItems.Count() > 0)
			{
				await DB.MasterPreSalePromotionItems.AddRangeAsync(newMasterPreSalePromotionItems);
			}
			if (newMasterPreSaleHouseModelItems.Count() > 0)
			{
				await DB.MasterPreSaleHouseModelItems.AddRangeAsync(newMasterPreSaleHouseModelItems);
			}
			await DB.SaveChangesAsync();

			return await this.GetMasterPreSalePromotionDetailAsync(newMasterPreSalePromotion.ID);
		}

		public async Task<CloneMasterPromotionConfirmDTO> GetCloneMasterPreSalePromotionConfirmAsync(Guid id,CancellationToken cancellationToken = default)
		{

			CloneMasterPromotionConfirmDTO result = new CloneMasterPromotionConfirmDTO();

			var statusActiveId = await DB.MasterCenters.AsNoTracking()
										.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionItemStatus
													&& o.Key == "1")
										.Select(o => o.ID)
										.FirstOrDefaultAsync(cancellationToken);
			//Clone NotExpire and Active
			result.CloneItemCount = await DB.MasterPreSalePromotionItems.AsNoTracking()
											.Where(o => o.MasterPreSalePromotionID == id && o.ExpireDate != null
											&& !string.IsNullOrEmpty(o.ItemNo)
											&& (!string.IsNullOrEmpty(o.MaterialCode) || o.AgreementNo.ToLower().Contains("welcomehome"))
											&& (!string.IsNullOrEmpty(o.GLAccountNo) || o.AgreementNo.ToLower().Contains("welcomehome"))
											&& !string.IsNullOrEmpty(o.MaterialGroupKey)
											&& o.ExpireDate > DateTime.Now
											&& o.PromotionItemStatusMasterCenterID == statusActiveId).CountAsync(cancellationToken);

			//Expire or NotActive
			result.ExpiredItemCount = await DB.MasterPreSalePromotionItems.AsNoTracking()
											.Where(o => o.MasterPreSalePromotionID == id && o.ExpireDate != null
											&& (o.ExpireDate <= DateTime.Now || o.PromotionItemStatusMasterCenterID != statusActiveId)).CountAsync(cancellationToken);

			return result;
		}

		private async Task ValidatePromotionMaterial(Guid masterPreSalePromotionID, PromotionMaterialDTO input)
		{
			ValidateException ex = new ValidateException();
			if (string.IsNullOrEmpty(input.AgreementNo))
			{
				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
				string desc = input.GetType().GetProperty(nameof(PromotionMaterialDTO.AgreementNo)).GetCustomAttribute<DescriptionAttribute>().Description;
				var msg = errMsg.Message.Replace("[field]", desc);
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}
			if (string.IsNullOrEmpty(input.ItemNo))
			{
				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
				string desc = input.GetType().GetProperty(nameof(PromotionMaterialDTO.ItemNo)).GetCustomAttribute<DescriptionAttribute>().Description;
				var msg = errMsg.Message.Replace("[field]", desc);
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}
			if (ex.HasError)
			{
				throw ex;
			}
		}
 
		private async Task<bool> SubMasterPreSalePromotionMaterialHasValue(PromotionMaterialDTO input, Guid mainPromotionItemID, Guid masterPreSalePromotionID)
		{
			var childmasterPreSalePromotionItem = await DB.MasterPreSalePromotionItems.Where(o => o.MasterPreSalePromotionID == masterPreSalePromotionID && o.MainPromotionItemID == mainPromotionItemID
												&& o.PromotionMaterialItemID == input.Id && o.IsDeleted == false).ToListAsync();

			var parentmasterPreSalePromotionItem = await DB.MasterPreSalePromotionItems.Where(o => o.ID == mainPromotionItemID
						&& o.PromotionMaterialItemID == input.Id && o.IsDeleted == false).ToListAsync();

			int count = childmasterPreSalePromotionItem.Count + parentmasterPreSalePromotionItem.Count;

			return count > 0 ? true : false;
		}
 
    }
}
