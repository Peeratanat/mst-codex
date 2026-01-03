using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models;
using Database.Models.DbQueries.PRM;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRM;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NPOI.SS.Formula.Functions;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Params.Outputs;
using Common.Helper.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;

namespace MST_Promotion.Services
{
	public class MasterSalePromotionService : IMasterSalePromotionService
	{
		private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }
 
		public MasterSalePromotionService(DatabaseContext db)
		{
            logModel = new LogModel("MasterSalePromotionService", null);
			this.DB = db;
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358677/preview
		/// </summary>
		/// <returns>The master booking promotion async.</returns>
		/// <param name="input">Input.</param>
		public async Task<MasterSalePromotionDTO> CreateMasterSalePromotionAsync(MasterSalePromotionDTO input)
		{
			await input.ValidateAsync(DB);
			var masterCenterPromotionStatusActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
																	  .Select(o => o.ID)
																	  .FirstAsync();
			var masterCenterPromotionStatusInActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
																	  .Select(o => o.ID)
																	  .FirstAsync();
			MasterSalePromotion model = new MasterSalePromotion();
			input.ToModel(ref model);

			string year = Convert.ToString(DateTime.Today.Year);
			var key = "PS" + input.Project?.ProjectNo + year[2] + year[3];
			var type = "PRM.MasterSalePromotion";
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
				var allPromotionStatusActive = await DB.MasterSalePromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != model.ID && o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
																			 .ToListAsync();

				allPromotionStatusActive.ForEach(o => o.PromotionStatusMasterCenterID = masterCenterPromotionStatusInActiveID);
				DB.MasterSalePromotions.UpdateRange(allPromotionStatusActive);
				await DB.SaveChangesAsync();
			}

			await DB.MasterSalePromotions.AddAsync(model);
			await DB.SaveChangesAsync();
			var result = await this.GetMasterSalePromotionDetailAsync(model.ID);
			return result;
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358679/preview
		/// </summary>
		/// <returns>The master booking promotion detail async.</returns>
		/// <param name="id">MasterSalePromotion.ID</param>
		public async Task<MasterSalePromotionDTO> GetMasterSalePromotionDetailAsync(Guid id,CancellationToken cancellationToken = default)
		{
			var model = await DB.MasterSalePromotions.Where(o => o.ID == id)
														.Include(o => o.Project)
														.ThenInclude(o => o.Company)
														.Include(o => o.PromotionStatus)
														.Include(o => o.UpdatedBy)
														.FirstAsync(cancellationToken);
			var result = MasterSalePromotionDTO.CreateFromModel(model);
			return result;
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358677/preview
		/// </summary>
		/// <returns>The master booking promotion list async.</returns>
		/// <param name="filter">Filter.</param>
		public async Task<MasterSalePromotionPaging> GetMasterSalePromotionListAsync(MasterSalePromotionListFilter filter, PageParam pageParam, MasterSalePromotionSortByParam sortByParam,CancellationToken cancellationToken = default)
		{
			IQueryable<MasterSalePromotionQueryResult> query = DB.MasterSalePromotions.AsNoTracking().Select(o =>
																 new MasterSalePromotionQueryResult
																 {
																	 Project = o.Project,
																	 MasterSalePromotion = o,
																	 PromotionStatus = o.PromotionStatus,
																	 UpdatedBy = o.UpdatedBy
																 });

			#region Filter
			if (!string.IsNullOrEmpty(filter.PromotionNo))
			{
				query = query.Where(o => o.MasterSalePromotion.PromotionNo.Contains(filter.PromotionNo));
			}
			if (!string.IsNullOrEmpty(filter.Name))
			{
				query = query.Where(o => o.MasterSalePromotion.Name.Contains(filter.Name));
			}
			if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
			{
				query = query.Where(o => o.Project.ID == filter.ProjectID);
			}
			if (!string.IsNullOrEmpty(filter.PromotionStatusKey))
			{
				var promotionStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.PromotionStatusKey
																	  && x.MasterCenterGroupKey == "PromotionStatus")
																	 .Select(x => x.ID).FirstAsync(cancellationToken);
				query = query.Where(x => x.MasterSalePromotion.PromotionStatusMasterCenterID == promotionStatusMasterCenterID);
			}
			if (filter.StartDateFrom != null)
			{
				query = query.Where(o => o.MasterSalePromotion.StartDate >= filter.StartDateFrom);
			}
			if (filter.StartDateTo != null)
			{
				query = query.Where(o => o.MasterSalePromotion.StartDate <= filter.StartDateTo);
			}
			if (filter.StartDateFrom != null && filter.StartDateTo != null)
			{
				query = query.Where(o => o.MasterSalePromotion.StartDate >= filter.StartDateFrom
									&& o.MasterSalePromotion.StartDate <= filter.StartDateTo);
			}
			if (filter.IsUsed != null)
			{
				query = query.Where(o => o.MasterSalePromotion.IsUsed == filter.IsUsed);
			}
			if (filter.EndDateFrom != null)
			{
				query = query.Where(o => o.MasterSalePromotion.EndDate >= filter.EndDateFrom);
			}
			if (filter.EndDateTo != null)
			{
				query = query.Where(o => o.MasterSalePromotion.EndDate <= filter.EndDateTo);
			}
			if (filter.EndDateFrom != null && filter.EndDateTo != null)
			{
				query = query.Where(o => o.MasterSalePromotion.EndDate >= filter.EndDateFrom
									&& o.MasterSalePromotion.EndDate <= filter.EndDateTo);
			}
			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(x => x.MasterSalePromotion.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(x => x.MasterSalePromotion.Updated <= filter.UpdatedTo);
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				query = query.Where(x => x.MasterSalePromotion.Updated >= filter.UpdatedFrom && x.MasterSalePromotion.Updated <= filter.UpdatedTo);
			}


			#endregion

			MasterSalePromotionDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<MasterSalePromotionQueryResult>(pageParam, ref query);

			var queryResults = await query.ToListAsync(cancellationToken);

			var results = queryResults.Select(o => MasterSalePromotionDTO.CreateFromQueryResult(o)).ToList();

			return new MasterSalePromotionPaging()
			{
				PageOutput = pageOutput,
				MasterSalePromotionDTOs = results
			};
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358679/preview
		/// </summary>
		/// <returns>The master booking promotion async.</returns>
		/// <param name="input">Input.</param>
		public async Task<MasterSalePromotionDTO> UpdateMasterSalePromotionAsync(Guid id, MasterSalePromotionDTO input)
		{
			await input.ValidateAsync(DB, true);
			var model = await DB.MasterSalePromotions.Where(o => o.ID == id).FirstAsync();
			var masterCenterPromotionStatusActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
																	  .Select(o => o.ID)
																	  .FirstAsync();
			var masterCenterPromotionStatusInActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
																	  .Select(o => o.ID)
																	  .FirstAsync();

			var countPromotionStatusActive = await DB.MasterSalePromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != id && o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
																			 .CountAsync();
			ValidateException ex = new ValidateException();
			if (input.StartDate != null && input.EndDate != null)
			{
				int totalDay = input.EndDate.Value.Subtract(input.StartDate.Value).Days;
				if (totalDay > 180)
				{
					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0038").FirstAsync();
					string desc = "วันที่สิ้นสุดการใช้งาน";
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}
			if (input.PromotionStatus?.Id == masterCenterPromotionStatusActiveID)
			{
				if (countPromotionStatusActive < 1)
				{
					input.ToModel(ref model);
				}
				else
				{
					var allPromotionStatusActive = await DB.MasterSalePromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != id && o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
																			 .ToListAsync();

					allPromotionStatusActive.ForEach(o => o.PromotionStatusMasterCenterID = masterCenterPromotionStatusInActiveID);
					DB.MasterSalePromotions.UpdateRange(allPromotionStatusActive);
					await DB.SaveChangesAsync();
					input.ToModel(ref model);
				}
			}
			else
			{
				input.ToModel(ref model);
			}

			if (ex.HasError)
			{
				throw ex;
			}

			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();
			var result = await this.GetMasterSalePromotionDetailAsync(model.ID);
			return result;
		}

		public async Task<MasterSalePromotionDTO> CheckActiveMasterSalePromotionAsync(Guid id, MasterSalePromotionDTO input)
		{
			var promotionStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1").Select(o => o.ID).FirstOrDefaultAsync();
			var project = await DB.Projects.Where(o => o.ProjectNo == input.Project.ProjectNo).FirstOrDefaultAsync();
			bool isUpdate = id.ToString() != "00000000-0000-0000-0000-000000000000" ? true : false;
			var model = isUpdate ? await DB.MasterSalePromotions.Where(o => o.ID != input.Id && o.ProjectID == project.ID && o.IsDeleted == false && o.PromotionStatusMasterCenterID == promotionStatusActive).CountAsync() > 0 :
									 await DB.MasterSalePromotions.Where(o => o.ProjectID == project.ID && o.IsDeleted == false && o.PromotionStatusMasterCenterID == promotionStatusActive).CountAsync() > 0;
			var masterSalePromotionsUsed = isUpdate ? await DB.MasterSalePromotions.Where(o => o.ID != input.Id && o.ProjectID == project.ID && o.IsDeleted == false && o.PromotionStatusMasterCenterID == promotionStatusActive).FirstOrDefaultAsync() :
								   await DB.MasterSalePromotions.Where(o => o.ProjectID == project.ID && o.IsDeleted == false && o.PromotionStatusMasterCenterID == promotionStatusActive).FirstOrDefaultAsync();

			ValidateException ex = new ValidateException();
			var result = new MasterSalePromotionDTO();
			if (model)
			{
				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0184").FirstAsync();
				string desc = masterSalePromotionsUsed.PromotionNo + ":" + masterSalePromotionsUsed.Name;
				var msg = errMsg.Message.Replace("[field]", desc);
				result.Message = msg;
				result.IsPopUp = true;
			}
			else
			{
				result.IsPopUp = false;
			}

			return result;
		}

		/// <summary>
		/// ลบ Master โปรขาย
		/// </summary>
		/// <returns>The master booking promotion async.</returns>
		/// <param name="id">Identifier.</param>
		public async Task DeleteMasterSalePromotionAsync(Guid id)
		{
			var model = await DB.MasterSalePromotions.FindAsync(id);
			model.IsDeleted = true;
			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master booking promotion item list async.</returns>
		/// <param name="pageParam">Page parameter.</param>
		/// <param name="sortByParam">Sort by parameter.</param>
		public async Task<MasterSalePromotionItemPaging> GetMasterSalePromotionItemListAsync(Guid masterSalePromotionID, PageParam pageParam, MasterSalePromotionItemSortByParam sortByParam,CancellationToken cancellationToken = default)
		{
			IQueryable<MasterSalePromotionItemQueryResult> query = DB.MasterSalePromotionItems.AsNoTracking()
																	 .Include(o => o.PromotionMaterialItem)
																	 .Where(o => o.MasterSalePromotionID == masterSalePromotionID)
																	 .Select(o =>
																	 new MasterSalePromotionItemQueryResult
																	 {
																		 PromotionMaterialItem = o.PromotionMaterialItem,
																		 MasterSalePromotionItem = o,
																		 PromotionItemStatus = o.PromotionItemStatus,
																		 WhenPromotionReceive = o.WhenPromotionReceive,
																		 UpdatedBy = o.UpdatedBy,
																		 Created = o.Created

																	 });
			var statusDeleteFromSap = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MaterialItemStatus && o.Key == "102")
												.Select(o => o.ID).FirstOrDefaultAsync(cancellationToken);

			MasterSalePromotionItemDTO.SortBy(sortByParam, ref query);

			//var queryResults = await query.OrderByDescending(o => o.MasterSalePromotionItem.IsUsed == false).ToListAsync();
			var queryResults = await query.ToListAsync(cancellationToken);
			//var results = queryResults.Select(o => MasterSalePromotionItemDTO.CreateFromQueryResult(o)).OrderBy(o => o.MainPromotionItemID).ToList();
			//var results = queryResults.Select(o => MasterSalePromotionItemDTO.CreateFromQueryResult(o, statusDeleteFromSap)).OrderBy(o => o.MainPromotionItemID).ToList();
			var results = queryResults.Select(o => MasterSalePromotionItemDTO.CreateFromQueryResult(o, statusDeleteFromSap)).ToList();
			List<MasterSalePromotionItemDTO> subItems = new List<MasterSalePromotionItemDTO>();
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
					MasterSalePromotionItemDTO.SortByDTO(sortByParam, ref subs);
					results.InsertRange(i + 1, subs);
					i++;
					i += subs.Count();
				}
				else
				{
					i++;
				}
			}

			var pageOutput = PagingHelper.PagingList<MasterSalePromotionItemDTO>(pageParam, ref results);

			return new MasterSalePromotionItemPaging()
			{
				PageOutput = pageOutput,
				MasterSalePromotionItemDTOs = results
			};
		}

		/// <summary>
		/// เรียกใช้ตอนกดปุ่ม "บันทึก"
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master booking promotion item list async.</returns>
		/// <param name="inputs">Inputs.</param>
		public async Task<List<MasterSalePromotionItemDTO>> UpdateMasterSalePromotionItemListAsync(Guid masterSalePromotionID, List<MasterSalePromotionItemDTO> inputs)
		{
			foreach (var item in inputs)
			{
				await item.ValidateAsync(DB);
			}
			var listMasterSalePromotionItemUpdate = new List<MasterSalePromotionItem>();
			var allMasterSalePromotionItem = await DB.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID).ToListAsync();
			foreach (var item in inputs)
			{
				var existingItem = allMasterSalePromotionItem.Where(o => o.ID == item.Id).FirstOrDefault();
				if (existingItem != null)
				{
					item.ToModel(ref existingItem);
					//if (existingItem.MainPromotionItemID == null)
					//{
					existingItem.TotalPrice = (int)Math.Ceiling(existingItem.Quantity * existingItem.PricePerUnit);
					//}
					listMasterSalePromotionItemUpdate.Add(existingItem);
				}
			}

			DB.UpdateRange(listMasterSalePromotionItemUpdate);
			await DB.SaveChangesAsync();

			var queryResults = await DB.MasterSalePromotionItems
										.Where(o => o.MasterSalePromotionID == masterSalePromotionID)
										.Include(o => o.PromotionMaterialItem)
										.Include(o => o.WhenPromotionReceive)
										.Include(o => o.PromotionItemStatus)
										.Include(o => o.UpdatedBy)
										.ToListAsync();

			var results = queryResults.Select(o => MasterSalePromotionItemDTO.CreateFromModel(o)).ToList();

			return results;
		}

		/// <summary>
		/// แก้ไขรายการโปร ทีละอัน
		/// </summary>
		/// <returns>The master booking promotion item async.</returns>
		/// <param name="masterSalePromotionID">Master booking promotion identifier.</param>
		/// <param name="input">Inputs.</param>
		public async Task<MasterSalePromotionItemDTO> UpdateMasterSalePromotionItemAsync(Guid masterSalePromotionID, Guid masterSalePromotionItemID, MasterSalePromotionItemDTO input)
		{
			await input.ValidateAsync(DB);
			decimal sumPriceChildOld = DB.MasterSalePromotionItems.Where(o => o.MainPromotionItemID == input.MainPromotionItemID).Sum(o => o.TotalPrice);
			var model = await DB.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID && o.ID == masterSalePromotionItemID).FirstOrDefaultAsync();
			input.ToModel(ref model);
			if (model.MainPromotionItemID == null) //Parent
			{
				model.PricePerUnit = (int)Math.Ceiling(model.Quantity * model.PricePerUnit);
				//model.TotalPrice = (int)Math.Ceiling(model.PricePerUnit);
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
				decimal sumPriceChildNew = DB.MasterSalePromotionItems.Where(o => o.MainPromotionItemID == input.MainPromotionItemID).Sum(o => o.TotalPrice);
				var masterMain = await DB.MasterSalePromotionItems.Where(o => o.ID == input.MainPromotionItemID).FirstOrDefaultAsync();
				masterMain.PricePerUnit = (int)Math.Ceiling((masterMain.PricePerUnit - sumPriceChildOld) + sumPriceChildNew);
				masterMain.TotalPrice = (int)Math.Ceiling((masterMain.TotalPrice - sumPriceChildOld) + sumPriceChildNew);

				DB.UpdateRange(masterMain);
				await DB.SaveChangesAsync();

			}
			else
			{
				//Update Child Status in
				if (model.ID != null)
				{
					var childList = await DB.MasterSalePromotionItems.Where(o => o.MainPromotionItemID == model.ID).ToListAsync();
					var childMasterSalePromotionItem = new List<MasterSalePromotionItem>();
					foreach (MasterSalePromotionItem mpsp in childList)
					{
						mpsp.WhenPromotionReceiveMasterCenterID = input.WhenPromotionReceive?.Id;
						childMasterSalePromotionItem.Add(mpsp);
					}
					DB.UpdateRange(childMasterSalePromotionItem);
					await DB.SaveChangesAsync();
				}
			}

			if (input.PromotionItemStatus?.Key == "0") //InActive
			{
				var promotionItemStatusID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "0").Select(o => o.ID).FirstOrDefaultAsync();
				//Update Inactive all
				if (model.MainPromotionItemID != null) //input child >> update main and child
				{
					var main = await DB.MasterSalePromotionItems.Where(o => o.ID == model.MainPromotionItemID).FirstOrDefaultAsync();
					main.PromotionItemStatusMasterCenterID = promotionItemStatusID;
					DB.Entry(main).State = EntityState.Modified;

					var childList = await DB.MasterSalePromotionItems.Where(o => o.MainPromotionItemID == model.MainPromotionItemID).ToListAsync();
					var childMasterSalePromotionItem = new List<MasterSalePromotionItem>();
					foreach (MasterSalePromotionItem mpsp in childList)
					{
						mpsp.PromotionItemStatusMasterCenterID = promotionItemStatusID;
						childMasterSalePromotionItem.Add(mpsp);
					}
					DB.UpdateRange(childMasterSalePromotionItem);
					await DB.SaveChangesAsync();
				}
				else //input main >> update child
				{
					if (model.ID != null)
					{
						var childList = await DB.MasterSalePromotionItems.Where(o => o.MainPromotionItemID == model.ID).ToListAsync();
						var childMasterSalePromotionItem = new List<MasterSalePromotionItem>();
						foreach (MasterSalePromotionItem mpsp in childList)
						{
							mpsp.PromotionItemStatusMasterCenterID = promotionItemStatusID;
							childMasterSalePromotionItem.Add(mpsp);
						}
						DB.UpdateRange(childMasterSalePromotionItem);
						await DB.SaveChangesAsync();
					}
				}
			}

			var dataResult = await DB.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID
																		&& o.ID == masterSalePromotionItemID)
																  .Include(o => o.PromotionMaterialItem)
																  .Include(o => o.WhenPromotionReceive)
																  .Include(o => o.PromotionItemStatus)
																  .Include(o => o.UpdatedBy)
																  .FirstAsync();

			var result = MasterSalePromotionItemDTO.CreateFromModel(dataResult);
			return result;
		}

		/// <summary>
		/// ลบทีละอัน ตรงปุ่ม ถังขยะ
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master booking promotion item async.</returns>
		/// <param name="id">Identifier.</param>
		public async Task DeleteMasterSalePromotionItemAsync(Guid id)
		{
			var model = await DB.MasterSalePromotionItems.FindAsync(id);
			model.IsDeleted = true;
			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();

			//Update Parent
			if (model.MainPromotionItemID != null)
			{
				var modelMain = await DB.MasterSalePromotionItems.Where(x => x.ID == model.MainPromotionItemID).FirstAsync();
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
		/// <returns>The master booking promotion item from material async.</returns>
		/// <param name="inputs">ส่งเฉพาะ Item ที่เลือก</param>
		public async Task<List<MasterSalePromotionItemDTO>> CreateMasterSalePromotionItemFromMaterialAsync(Guid masterSalePromotionID, List<PromotionMaterialDTO> inputs)
		{
			var listMasterSalePromotionItemCreate = new List<MasterSalePromotionItem>();
			var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
			var whenPromotionReceiveAfterContract = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
			foreach (var item in inputs)
			{
				await ValidatePromotionMaterial(masterSalePromotionID, item);

				//if (await MasterSalePromotionMaterialHasValue(item, masterSalePromotionID))
				//{
				//	continue;
				//}

				MasterSalePromotionItem model = new MasterSalePromotionItem();
				item.ToMasterSalePromotionItemModel(ref model);
				model.MasterSalePromotionID = masterSalePromotionID;
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
					model.RemarkTH = promotionMaterialItem.RemarkTH;
					model.RemarkEN = promotionMaterialItem.RemarkEN;
					model.UnitEN = promotionMaterialItem.UnitEN;
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

				listMasterSalePromotionItemCreate.Add(model);
			}
			await DB.AddRangeAsync(listMasterSalePromotionItemCreate);
			await DB.SaveChangesAsync();
			var createdIDs = listMasterSalePromotionItemCreate.Select(o => o.ID).ToList();
			var dataResult = await DB.MasterSalePromotionItems.Where(o => createdIDs.Contains(o.ID))
																  .Include(o => o.PromotionMaterialItem)
																  .Include(o => o.WhenPromotionReceive)
																  .Include(o => o.PromotionItemStatus)
																  .Include(o => o.UpdatedBy)
																  .ToListAsync();

			//HouseModel
			var masterSaleHouseModelItemsCreate = new List<MasterSaleHouseModelItem>();
			var projectID = await DB.MasterSalePromotions.Where(o => o.ID == masterSalePromotionID).Select(o => o.ProjectID).FirstOrDefaultAsync();
			var models = await DB.Models.Where(o => o.ProjectID == projectID).ToListAsync();
			foreach (var item in dataResult)
			{
				foreach (var itemModel in models)
				{
					MasterSaleHouseModelItem modelH = new MasterSaleHouseModelItem();
					modelH.MasterSalePromotionItemID = item.ID;
					modelH.ModelID = itemModel.ID;
					masterSaleHouseModelItemsCreate.Add(modelH);
				}
			}

			await DB.AddRangeAsync(masterSaleHouseModelItemsCreate);
			await DB.SaveChangesAsync();

			var result = dataResult.Select(o => MasterSalePromotionItemDTO.CreateFromModel(o)).ToList();
			return result;
		}

		/// <summary>
		/// เพิ่มรายการย่อยจากการเลือก Material 
		/// รายการหลักกับรายการย่อยใช้ DTO เดียวกัน เหมือนกับ Model
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358682/preview
		/// </summary>
		/// <returns>The sub master booking promotion item from material async.</returns>
		/// <param name="inputs">ส่งมาเฉพาะรายการที่เลือกมาเท่านั้น</param>
		public async Task<List<MasterSalePromotionItemDTO>> CreateSubMasterSalePromotionItemFromMaterialAsync(Guid masterSalePromotionID, Guid mainMasterSalePromotionItemID, List<PromotionMaterialDTO> inputs)
		{
			var listMasterSalePromotionItemCreate = new List<MasterSalePromotionItem>();
			var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
			var whenPromotionReceiveAfterContract = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
			decimal sumPriceChildOld = DB.MasterSalePromotionItems.Where(o => o.MainPromotionItemID == mainMasterSalePromotionItemID).Sum(o => o.TotalPrice);

			foreach (var item in inputs)
			{
				await ValidatePromotionMaterial(masterSalePromotionID, item);

				if (await SubMasterSalePromotionMaterialHasValue(item, mainMasterSalePromotionItemID, masterSalePromotionID))
				{
					continue;
				}

				MasterSalePromotionItem model = new MasterSalePromotionItem();
				item.ToMasterSalePromotionItemModel(ref model);
				model.MasterSalePromotionID = masterSalePromotionID;
				model.MainPromotionItemID = mainMasterSalePromotionItemID;
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
					model.RemarkTH = promotionMaterialItem.RemarkTH;
					model.RemarkEN = promotionMaterialItem.RemarkEN;
					model.UnitEN = promotionMaterialItem.UnitEN;
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
				listMasterSalePromotionItemCreate.Add(model);
			}
			await DB.AddRangeAsync(listMasterSalePromotionItemCreate);
			await DB.SaveChangesAsync();

			//Calc Main
			decimal sumPriceChildNew = DB.MasterSalePromotionItems.Where(o => o.MainPromotionItemID == mainMasterSalePromotionItemID).Sum(o => o.TotalPrice);
			var masterMain = await DB.MasterSalePromotionItems.Where(o => o.ID == mainMasterSalePromotionItemID).FirstOrDefaultAsync();
			//masterMain.PricePerUnit = (int)Math.Ceiling((masterMain.PricePerUnit - sumPriceChildOld) + sumPriceChildNew);
			masterMain.TotalPrice = (int)Math.Ceiling((masterMain.TotalPrice - sumPriceChildOld) + sumPriceChildNew);

			DB.UpdateRange(masterMain);
			await DB.SaveChangesAsync();


			var createdIDs = listMasterSalePromotionItemCreate.Select(o => o.ID).ToList();
			var dataResult = await DB.MasterSalePromotionItems.Where(o => createdIDs.Contains(o.ID))
																  .Include(o => o.PromotionMaterialItem)
																  .Include(o => o.WhenPromotionReceive)
																  .Include(o => o.PromotionItemStatus)
																  .Include(o => o.UpdatedBy)
																  .ToListAsync();

			var result = dataResult.Select(o => MasterSalePromotionItemDTO.CreateFromModel(o)).ToList();
			return result;
		}

		/// <summary>
		/// ดึงแบบบ้านจาก Item โปรขาย
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
		/// </summary>
		/// <returns>The master booking promotion item model list async.</returns>
		/// <param name="masterSalePromotionItemID">Master booking promotion item identifier.</param>
		public async Task<List<ModelListDTO>> GetMasterSalePromotionItemModelListAsync(Guid masterSalePromotionItemID,CancellationToken cancellationToken = default)
		{
			var listModel = await DB.MasterSaleHouseModelItems.AsNoTracking().Where(o => o.MasterSalePromotionItemID == masterSalePromotionItemID).ToListAsync(cancellationToken);
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
		/// เพิ่มแบบบ้านเข้าไปใน Item โปรขาย
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
		/// </summary>
		/// <returns>The master booking promotion item model list async.</returns>
		/// <param name="masterSalePromotionItemID">Master booking promotion item identifier.</param>
		/// <param name="inputs">Inputs.</param>
		public async Task<List<ModelListDTO>> AddMasterSalePromotionItemModelListAsync(Guid masterSalePromotionItemID, List<ModelListDTO> inputs)
		{
			var masterSaleHouseModelItemsCreate = new List<MasterSaleHouseModelItem>();
			var masterSaleHouseModelItemsUpdate = new List<MasterSaleHouseModelItem>();
			var masterSaleHouseModelItemsDelete = new List<MasterSaleHouseModelItem>();
			var masterSaleHouseModelItems = await DB.MasterSaleHouseModelItems.Where(o => o.MasterSalePromotionItemID == masterSalePromotionItemID).ToListAsync();

			foreach (var item in inputs)
			{
				var existingItem = masterSaleHouseModelItems.Where(o => o.ModelID == item.Id).FirstOrDefault();
				if (existingItem == null)
				{
					MasterSaleHouseModelItem model = new MasterSaleHouseModelItem();
					model.MasterSalePromotionItemID = masterSalePromotionItemID;
					model.ModelID = item.Id;
					masterSaleHouseModelItemsCreate.Add(model);
				}
				else
				{
					masterSaleHouseModelItemsUpdate.Add(existingItem);
				}
			}
			foreach (var item in masterSaleHouseModelItems)
			{
				var existingInput = inputs.Where(o => o.Id == item.ModelID).FirstOrDefault();
				if (existingInput == null)
				{
					item.IsDeleted = true;
					masterSaleHouseModelItemsDelete.Add(item);
				}
			}
			DB.UpdateRange(masterSaleHouseModelItemsUpdate);
			DB.UpdateRange(masterSaleHouseModelItemsDelete);
			await DB.AddRangeAsync(masterSaleHouseModelItemsCreate);
			await DB.SaveChangesAsync();

			var results = await this.GetMasterSalePromotionItemModelListAsync(masterSalePromotionItemID);
			return results;
		}

		/// <summary>
		/// ดึงรายการที่ไม่ต้องจัดซื้อ
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master booking promotion free item list async.</returns>
		/// <param name="pageParam">Page parameter.</param>
		/// <param name="sortByParam">Sort by parameter.</param>
		public async Task<MasterSalePromotionFreeItemPaging> GetMasterSalePromotionFreeItemListAsync(Guid masterSalePromotionID, PageParam pageParam, MasterSalePromotionFreeItemSortByParam sortByParam,CancellationToken cancellationToken = default)
		{
			IQueryable<MasterSalePromotionFreeItemQueryResult> query = DB.MasterSalePromotionFreeItems.AsNoTracking()
																	 .Where(o => o.MasterSalePromotionID == masterSalePromotionID)
																	 .Select(o =>
																	 new MasterSalePromotionFreeItemQueryResult
																	 {
																		 MasterSalePromotionFreeItem = o,
																		 WhenPromotionReceive = o.WhenPromotionReceive,
																		 UpdatedBy = o.UpdatedBy
																	 });

			MasterSalePromotionFreeItemDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<MasterSalePromotionFreeItemQueryResult>(pageParam, ref query);

			var queryResults = await query.ToListAsync(cancellationToken);

			var results = queryResults.Select(o => MasterSalePromotionFreeItemDTO.CreateFromQueryResult(o)).ToList();

			return new MasterSalePromotionFreeItemPaging()
			{
				PageOutput = pageOutput,
				MasterSalePromotionFreeItemDTOs = results
			};
		}

		/// <summary>
		/// สร้างรายการที่ไม่ต้องจัดซื้อ
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master booking promotion free item async.</returns>
		/// <param name="input">Input.</param>
		public async Task<MasterSalePromotionFreeItemDTO> CreateMasterSalePromotionFreeItemAsync(Guid masterSalePromotionID, MasterSalePromotionFreeItemDTO input)
		{
			await input.ValidateAsync(DB);
			MasterSalePromotionFreeItem model = new MasterSalePromotionFreeItem();
			input.ToModel(ref model);
			model.MasterSalePromotionID = masterSalePromotionID;
			await DB.MasterSalePromotionFreeItems.AddAsync(model);
			await DB.SaveChangesAsync();

			model = await DB.MasterSalePromotionFreeItems
						.Include(o => o.WhenPromotionReceive)
						.Include(o => o.UpdatedBy)
						.FirstAsync(o => o.ID == model.ID);

			//HouseModel
			var masterSaleHouseModelFreeItemsCreate = new List<MasterSaleHouseModelFreeItem>();
			var projectID = await DB.MasterSalePromotions.Where(o => o.ID == masterSalePromotionID).Select(o => o.ProjectID).FirstOrDefaultAsync();
			var models = await DB.Models.Where(o => o.ProjectID == projectID).ToListAsync();

			foreach (var itemModel in models)
			{
				MasterSaleHouseModelFreeItem modelH = new MasterSaleHouseModelFreeItem();
				modelH.MasterSalePromotionFreeItemID = model.ID;
				modelH.ModelID = itemModel.ID;
				masterSaleHouseModelFreeItemsCreate.Add(modelH);
			}

			await DB.AddRangeAsync(masterSaleHouseModelFreeItemsCreate);
			await DB.SaveChangesAsync();


			var result = MasterSalePromotionFreeItemDTO.CreateFromModel(model);
			return result;
		}

		/// <summary>
		/// แก้ไขรายการที่ไม่ต้องจัดซื้อ (แบบที่ละหลายรายการ)
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master booking promotion free item list async.</returns>
		/// <param name="inputs">Inputs.</param>
		public async Task<List<MasterSalePromotionFreeItemDTO>> UpdateMasterSalePromotionFreeItemListAsync(Guid masterSalePromotionID, List<MasterSalePromotionFreeItemDTO> inputs)
		{
			var listMasterSalePromotionFreeItemUpdate = new List<MasterSalePromotionFreeItem>();
			var allMasterSalePromotionFreeItem = await DB.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID).ToListAsync();
			foreach (var item in inputs)
			{
				await item.ValidateAsync(DB);
			}

			foreach (var item in inputs)
			{
				var existingItem = allMasterSalePromotionFreeItem.Where(o => o.ID == item.Id).FirstOrDefault();
				if (existingItem != null)
				{
					item.ToModel(ref existingItem);
					listMasterSalePromotionFreeItemUpdate.Add(existingItem);
				}
			}

			DB.UpdateRange(listMasterSalePromotionFreeItemUpdate);
			await DB.SaveChangesAsync();

			var updatedIDs = listMasterSalePromotionFreeItemUpdate.Select(o => o.ID).ToList();
			var dataResult = await DB.MasterSalePromotionFreeItems
				.Include(o => o.WhenPromotionReceive)
				.Include(o => o.UpdatedBy)
				.Where(o => updatedIDs.Contains(o.ID)).ToListAsync();

			var listresult = dataResult.Select(o => MasterSalePromotionFreeItemDTO.CreateFromModel(o)).ToList();
			return listresult;
		}

		/// <summary>
		/// แก้ไขรายการที่ไม่ต้องจัดซื้อ (แบบที่ละรายการ)
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master booking promotion free item async.</returns>
		/// <param name="masterSalePromotionID">Master booking promotion identifier.</param>
		/// <param name="input">Input.</param>
		public async Task<MasterSalePromotionFreeItemDTO> UpdateMasterSalePromotionFreeItemAsync(Guid masterSalePromotionID, Guid masterSalePromotionFreeItemID, MasterSalePromotionFreeItemDTO input)
		{
			await input.ValidateAsync(DB);
			var model = await DB.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID && o.ID == masterSalePromotionFreeItemID).FirstAsync();
			input.ToModel(ref model);
			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();

			var dataResult = await DB.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID
																		&& o.ID == masterSalePromotionFreeItemID)
																	.Include(o => o.WhenPromotionReceive)
																	.Include(o => o.UpdatedBy)
																  .FirstAsync();
			var result = MasterSalePromotionFreeItemDTO.CreateFromModel(dataResult);
			return result;
		}

		/// <summary>
		/// ลบรายการที่ไม่ต้องจัดซื้อ
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
		/// </summary>
		/// <returns>The master booking promotion free item async.</returns>
		/// <param name="id">Identifier.</param>
		public async Task DeleteMasterSalePromotionFreeItemAsync(Guid id)
		{
			var model = await DB.MasterSalePromotionFreeItems.FindAsync(id);
			model.IsDeleted = true;
			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();
		}

		/// <summary>
		/// ดึงแบบบ้านของ Item ที่ไม่ต้องจัดซื้อ
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
		/// </summary>
		/// <returns>The master booking promotion free item model list async.</returns>
		/// <param name="masterSalePromotionItemID">Master booking promotion item identifier.</param>
		public async Task<List<ModelListDTO>> GetMasterSalePromotionFreeItemModelListAsync(Guid masterSalePromotionFreeItemID,CancellationToken cancellationToken = default)
		{

			var listModel = await DB.MasterSaleHouseModelFreeItems.AsNoTracking().Where(o => o.MasterSalePromotionFreeItemID == masterSalePromotionFreeItemID).ToListAsync(cancellationToken);
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
		/// เพิ่มแบบบ้านไปใน Item ที่ไม่ต้องจัดซื้อ
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
		/// </summary>
		/// <returns>The master booking promotion free item model list async.</returns>
		/// <param name="masterSalePromotionFreeItemID">Master booking promotion free item identifier.</param>
		/// <param name="inputs">Inputs.</param>
		public async Task<List<ModelListDTO>> AddMasterSalePromotionFreeItemModelListAsync(Guid masterSalePromotionFreeItemID, List<ModelListDTO> inputs)
		{
			var masterSaleHouseModelFreeItemsCreate = new List<MasterSaleHouseModelFreeItem>();
			var masterSaleHouseModelFreeItemsUpdate = new List<MasterSaleHouseModelFreeItem>();
			var masterSaleHouseModelFreeItemsDelete = new List<MasterSaleHouseModelFreeItem>();
			var masterSaleHouseModelFreeItems = await DB.MasterSaleHouseModelFreeItems.Where(o => o.MasterSalePromotionFreeItemID == masterSalePromotionFreeItemID).ToListAsync();

			foreach (var item in inputs)
			{
				var existingItem = masterSaleHouseModelFreeItems.Where(o => o.ModelID == item.Id).FirstOrDefault();
				if (existingItem == null)
				{
					MasterSaleHouseModelFreeItem model = new MasterSaleHouseModelFreeItem();
					model.MasterSalePromotionFreeItemID = masterSalePromotionFreeItemID;
					model.ModelID = item.Id;
					masterSaleHouseModelFreeItemsCreate.Add(model);
				}
				else
				{
					masterSaleHouseModelFreeItemsUpdate.Add(existingItem);
				}
			}
			foreach (var item in masterSaleHouseModelFreeItems)
			{
				var existingInput = inputs.Where(o => o.Id == item.ModelID).FirstOrDefault();
				if (existingInput == null)
				{
					item.IsDeleted = true;
					masterSaleHouseModelFreeItemsDelete.Add(item);
				}
			}
			DB.UpdateRange(masterSaleHouseModelFreeItemsUpdate);
			DB.UpdateRange(masterSaleHouseModelFreeItemsDelete);
			await DB.AddRangeAsync(masterSaleHouseModelFreeItemsCreate);
			await DB.SaveChangesAsync();

			var results = await this.GetMasterSalePromotionFreeItemModelListAsync(masterSalePromotionFreeItemID);
			return results;
		}

		/// <summary>
		/// ดึงรายการค่าธรรมเนียมรูดบัตร
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
		/// </summary>
		/// <returns>The master booking credit card item async.</returns>
		/// <param name="pageParam">Page parameter.</param>
		/// <param name="sortByParam">Sort by parameter.</param>
		public async Task<MasterSalePromotionCreditCardItemPaging> GetMasterSalePromotionCreditCardItemAsync(Guid masterSalePromotionID, PageParam pageParam, MasterSalePromotionCreditCardItemSortByParam sortByParam,CancellationToken cancellationToken = default)
		{
			IQueryable<MasterSalePromotionCreditCardItemQueryResult> query = DB.MasterSalePromotionCreditCardItems.AsNoTracking()
																	 .Where(o => o.MasterSalePromotionID == masterSalePromotionID)
																	 .OrderBy(o => o.BankName)
																	 .Select(o =>
																	 new MasterSalePromotionCreditCardItemQueryResult
																	 {
																		 MasterSalePromotionCreditCardItem = o,
																		 Bank = o.Bank,
																		 EDCFee = o.EDCFee,
																		 PromotionItemStatus = o.PromotionItemStatus,
																		 UpdatedBy = o.UpdatedBy
																	 });

			MasterSalePromotionCreditCardItemDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<MasterSalePromotionCreditCardItemQueryResult>(pageParam, ref query);

			var queryResults = await query.ToListAsync(cancellationToken);

			var results = queryResults.Select(o => MasterSalePromotionCreditCardItemDTO.CreateFromQueryResult(o)).ToList();

			return new MasterSalePromotionCreditCardItemPaging()
			{
				PageOutput = pageOutput,
				MasterSalePromotionCreditCardItemDTOs = results
			};
		}

		/// <summary>
		/// แก้ไขรายการค่าธรรมเนียมรูดบัตร (ทีละหลายรายการ)
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
		/// </summary>
		/// <returns>The master booking credit card item list async.</returns>
		/// <param name="inputs">Inputs.</param>
		public async Task<List<MasterSalePromotionCreditCardItemDTO>> UpdateMasterSalePromotionCreditCardItemListAsync(Guid masterSalePromotionID, List<MasterSalePromotionCreditCardItemDTO> inputs)
		{
			foreach (var item in inputs)
			{
				await item.ValidateAsync(DB);
			}
			var masterSalePromotionCreditCardItems = new List<MasterSalePromotionCreditCardItem>();
			var existingMasterSalePromotionCreditCardItems = await DB.MasterSalePromotionCreditCardItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID).ToListAsync();
			foreach (var item in inputs)
			{
				var existingItem = existingMasterSalePromotionCreditCardItems.Where(o => o.ID == item.Id).FirstOrDefault();
				if (existingItem != null)
				{
					item.ToModel(ref existingItem);
					masterSalePromotionCreditCardItems.Add(existingItem);
				}
			}

			DB.UpdateRange(masterSalePromotionCreditCardItems);
			await DB.SaveChangesAsync();

			var updatedIDs = masterSalePromotionCreditCardItems.Select(o => o.ID).ToList();
			var dataResult = await DB.MasterSalePromotionCreditCardItems
				.Include(o => o.Bank)
				.Include(o => o.EDCFee)
				.Include(o => o.PromotionItemStatus)
				.Include(o => o.UpdatedBy)
				.Where(o => updatedIDs.Contains(o.ID)).ToListAsync();

			var listresult = dataResult.Select(o => MasterSalePromotionCreditCardItemDTO.CreateFromModel(o)).ToList();
			return listresult;
		}

		/// <summary>
		/// แก้ไขรายการค่าธรรมเนียมรูดบัตร (ทีละรายการ)
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
		/// </summary>
		/// <returns>The master booking credit card item async.</returns>
		/// <param name="masterSalePromotionID">Master booking promotion identifier.</param>
		/// <param name="masterSalePromotionCreditCardItemID">Master booking promotion credit card item identifier.</param>
		/// <param name="input">Input.</param>
		public async Task<MasterSalePromotionCreditCardItemDTO> UpdateMasterSalePromotionCreditCardItemAsync(Guid masterSalePromotionID, Guid masterSalePromotionCreditCardItemID, MasterSalePromotionCreditCardItemDTO input)
		{
			await input.ValidateAsync(DB);
			var model = await DB.MasterSalePromotionCreditCardItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID && o.ID == masterSalePromotionCreditCardItemID).FirstAsync();
			input.ToModel(ref model);
			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();

			var dataResult = await DB.MasterSalePromotionCreditCardItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID
																		&& o.ID == masterSalePromotionCreditCardItemID)
																	.Include(o => o.Bank)
																	.Include(o => o.EDCFee)
																	.Include(o => o.PromotionItemStatus)
																	.Include(o => o.UpdatedBy)
																  .FirstAsync();
			var result = MasterSalePromotionCreditCardItemDTO.CreateFromModel(dataResult);
			return result;
		}

		/// <summary>
		/// ลบรายการค่าธรรมเนียมรูดบัตร
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
		/// </summary>
		/// <returns>The master booking credit card item async.</returns>
		/// <param name="id">Identifier.</param>
		public async Task DeleteMasterSalePromotionCreditCardItemAsync(Guid id)
		{
			var model = await DB.MasterSalePromotionCreditCardItems.FindAsync(id);
			model.IsDeleted = true;
			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358686/preview
		/// EDCFee จะดึงมาจาก GET EDCFees API โดยมีเงื่อนไขดังนี้
		/// บัตรที่รูด เป็นธนาคารเดียวกัน, รูปแบบการรูดเฉพาะการผ่อน
		/// </summary>
		/// <returns>The master booking credit card items.</returns>
		/// <param name="masterSalePromotionID">Master booking promotion identifier.</param>
		/// <param name="inputs">Inputs.</param>
		public async Task<List<MasterSalePromotionCreditCardItemDTO>> CreateMasterSalePromotionCreditCardItemsAsync(Guid masterSalePromotionID, List<EDCFeeDTO> inputs)
		{
			var masterSalePromotionCreditCardItemsCreate = new List<MasterSalePromotionCreditCardItem>();
			var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
			foreach (var item in inputs)
			{
				MasterSalePromotionCreditCardItem model = new MasterSalePromotionCreditCardItem();
				item.ToMasterSalePromotionCreditCardItemModel(ref model);
				model.MasterSalePromotionID = masterSalePromotionID;
				model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
				masterSalePromotionCreditCardItemsCreate.Add(model);
			}
			await DB.AddRangeAsync(masterSalePromotionCreditCardItemsCreate);
			await DB.SaveChangesAsync();

			var createdIDs = masterSalePromotionCreditCardItemsCreate.Select(o => o.ID).ToList();
			var dataResult = await DB.MasterSalePromotionCreditCardItems
				.Include(o => o.Bank)
				.Include(o => o.EDCFee)
				.Include(o => o.PromotionItemStatus)
				.Include(o => o.UpdatedBy)
				.Where(o => createdIDs.Contains(o.ID)).ToListAsync();

			var results = dataResult.Select(o => MasterSalePromotionCreditCardItemDTO.CreateFromModel(o)).ToList();
			return results;
		}

		/// <summary>
		/// Clone Promotion ให้ Copy ทุกอย่างใน MasterPromotion สร้างเป็น Promotion ใหม่
		/// </summary>
		/// <returns>The master booking promotion async.</returns>
		/// <param name="id">Identifier.</param>
		public async Task<MasterSalePromotionDTO> CloneMasterSalePromotionAsync(Guid id)
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

			var masterSalePromotion = await DB.MasterSalePromotions.Where(o => o.ID == id).Include(o => o.Project).FirstOrDefaultAsync();
			var masterSalePromotionItems = await DB.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == id
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
			if (masterSalePromotionItems.Count > 0)
			{
				var promotionMaterialItemsID = masterSalePromotionItems.Select(o => o.PromotionMaterialItemID).ToList();
				promotionMaterialItems = await DB.PromotionMaterialItems.Where(o => promotionMaterialItemsID.Contains(o.ID)).ToListAsync();
			}

			var masterSalePromotionFreeItems = await DB.MasterSalePromotionFreeItems.Where(o => o.MasterSalePromotionID == id).ToListAsync();
			var masterSalePromotionCreditCardItems = await DB.MasterSalePromotionCreditCardItems.Where(o => o.MasterSalePromotionID == id
																									   && o.PromotionItemStatusMasterCenterID == statusActiveId).ToListAsync();


			var promotionStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == "0"
																	&& x.MasterCenterGroupKey == "PromotionStatus")
																   .Select(x => x.ID).FirstAsync();

			var productType = await DB.MasterCenters.Where(o => o.ID == masterSalePromotion.Project.ProductTypeMasterCenterID).FirstOrDefaultAsync();

			var newMasterSalePromotion = new MasterSalePromotion
			{
				Name = masterSalePromotion.Name,
				ProjectID = masterSalePromotion.ProjectID,
				//StartDate = masterSalePromotion.StartDate,
				//EndDate = masterSalePromotion.EndDate,
				StartDate = DateTime.Now,
				CashDiscount = masterSalePromotion.CashDiscount,
				FGFDiscount = masterSalePromotion.FGFDiscount,
				TransferDiscount = masterSalePromotion.TransferDiscount,
				PromotionStatusMasterCenterID = promotionStatusMasterCenterID,
				IsUsed = false,
				RefMigrateID1 = "ClonedFromProm.No : " + masterSalePromotion.PromotionNo?.ToString()
			};

			string year = Convert.ToString(DateTime.Today.Year);
			var key = "PS" + masterSalePromotion.Project?.ProjectNo + year[2] + year[3];
			var type = "PRM.MasterSalePromotion";
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

				newMasterSalePromotion.PromotionNo = key + runningNumberCounter.Count.ToString("000");
				runningNumberCounter.Count++;
				DB.Entry(runningNumberCounter).State = EntityState.Modified;
				await DB.SaveChangesAsync();
			}
			else
			{
				newMasterSalePromotion.PromotionNo = key + runningno.Count.ToString("000");
				runningno.Count++;
				DB.Entry(runningno).State = EntityState.Modified;
				await DB.SaveChangesAsync();
			}

			var newMasterSalePromotionItems = new List<MasterSalePromotionItem>();
			var newMasterSaleHouseModelItems = new List<MasterSaleHouseModelItem>();
			var newMasterSalePromotionFreeItems = new List<MasterSalePromotionFreeItem>();
			var newMasterSaleHouseModelFreeItems = new List<MasterSaleHouseModelFreeItem>();
			var newMasterSalePromotionCreditCardItem = new List<MasterSalePromotionCreditCardItem>();
			var listPromotionMaterialitem = new List<Guid?>();
			foreach (var item in masterSalePromotionItems.Where(o => o.MainPromotionItemID == null))
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
				//             || string.IsNullOrEmpty(item.SAPSaleTaxCode) ? 0 : Convert.ToDecimal(1.07);
				//4
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
				var checkSubPromotionitem = await DB.MasterSalePromotionItems.Where(o => o.MainPromotionItemID == item.ID).ToListAsync();
				if (checkSubPromotionitem.Count == 0)
				{
					if (listPromotionMaterialitem.Contains(item.PromotionMaterialItemID))
					{
						continue;
					}
					listPromotionMaterialitem.Add(item.PromotionMaterialItemID);
				}

				using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
				string sqlQuery = sqlDupSalePromotion.QueryString;
				DynamicParameters ParamList = sqlDupSalePromotion.QueryFilter(ref sqlQuery, id);
				CommandDefinition commandDefinition = new(
													  commandText: sqlQuery,
													  parameters: ParamList,
													  transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
													  commandType: CommandType.Text);
				var getDupSalePromo = (await cmd.Connection.QueryAsync<sqlDupSalePromotion.QueryResult>(commandDefinition))?.ToList() ?? new();
				var listGetDupSalePromo = getDupSalePromo.Select(o => o.MasterSalePromoItemID).ToList().FirstOrDefault();

                //if (listGetDupSalePromo == item.ID)
                //{
                //    continue;
                //}


                var model = new MasterSalePromotionItem
				{
					MasterSalePromotionID = newMasterSalePromotion.ID,
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
					MainPromotionItemID = null,
					PromotionMaterialItemID = item.PromotionMaterialItemID,
					IsUsed = false,
				};
				 #region evoucher aurora
                if (!string.IsNullOrEmpty(item.SAPVendor) && item.SAPVendor.Equals("0000107009") && item.MaterialGroupKey.Equals("EST100"))
                {
                    // find fee mat in PromotionOperatingFee
                    var matfee = await DB.PromotionOperatingFees.Where(x => x.IsActived == true).Select(x => x.PromotionMaterialCode).ToListAsync();
                    // fine Mat Fee in PromotionMaterialItems
                    var matFeeItem = await DB.PromotionMaterialItems.Where(x =>x.AgreementNo.Equals(item.AgreementNo) && matfee.Contains(x.MaterialCode) && x.SAPDeleteIndicator != "L").FirstOrDefaultAsync();
                    decimal fee = 0;
					if(matFeeItem != null)
					{
						fee = matFeeItem.BasePrice  ; 
                    }
                    totalPriceInCludeVat = (int)Math.Ceiling(pricePerUnitIncludeVat7 + fee );
                    model.PricePerUnit = (int)Math.Ceiling( totalPriceInCludeVat);
                }
                #endregion
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
				var masterBookingHouseItem = await DB.MasterSaleHouseModelItems.Where(o => o.MasterSalePromotionItemID == item.ID).ToListAsync();
				foreach (var house in masterBookingHouseItem)
				{
					var newhouse = new MasterSaleHouseModelItem
					{
						MasterSalePromotionItemID = model.ID,
						ModelID = house.ModelID
					};
					newMasterSaleHouseModelItems.Add(newhouse);
				}
				//newMasterSalePromotionItems.Add(model);

				//Add SubPromotionItems
				var listSub = masterSalePromotionItems.Where(o => o.MainPromotionItemID == item.ID).ToList();
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

					var modelSub = new MasterSalePromotionItem
					{
						MasterSalePromotionID = newMasterSalePromotion.ID,
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
					newMasterSalePromotionItems.Add(modelSub);
					var masterBookingHouseItemSub = await DB.MasterSaleHouseModelItems.Where(o => o.MasterSalePromotionItemID == item1.ID).ToListAsync();
					foreach (var house1 in masterBookingHouseItemSub)
					{
						var newhousesub = new MasterSaleHouseModelItem
						{
							MasterSalePromotionItemID = modelSub.ID,
							ModelID = house1.ModelID
						};
						newMasterSaleHouseModelItems.Add(newhousesub);
					}
				}

				//Add Total MainPromotionItems
				model.TotalPrice = (int)Math.Ceiling(totalPriceInCludeVat);
				newMasterSalePromotionItems.Add(model);
			}
			foreach (var item in masterSalePromotionFreeItems)
			{
				var model = new MasterSalePromotionFreeItem
				{
					MasterSalePromotionID = newMasterSalePromotion.ID,

					NameTH = item.NameTH,
					NameEN = item.NameEN,
					Quantity = item.Quantity,
					UnitTH = item.UnitTH,
					UnitEN = item.UnitEN,
					ReceiveDays = item.ReceiveDays,
					WhenPromotionReceiveMasterCenterID = item.WhenPromotionReceiveMasterCenterID,
					IsShowInContract = item.IsShowInContract,
				};
				newMasterSalePromotionFreeItems.Add(model);
				var masterBookingHouseFreeItem = await DB.MasterSaleHouseModelFreeItems.Where(o => o.MasterSalePromotionFreeItemID == item.ID).ToListAsync();
				foreach (var item1 in masterBookingHouseFreeItem)
				{
					var house = new MasterSaleHouseModelFreeItem
					{
						MasterSalePromotionFreeItemID = model.ID,
						ModelID = item1.ModelID
					};
					newMasterSaleHouseModelFreeItems.Add(house);
				}
			}
			//foreach (var item in masterSalePromotionCreditCardItems)
			//{
			//	var model = new MasterSalePromotionCreditCardItem()
			//	{
			//		MasterSalePromotionID = newMasterSalePromotion.ID,
			//		BankID = item.BankID,
			//		NameTH = item.NameTH,
			//		NameEN = item.NameEN,
			//		Fee = item.Fee,
			//		UnitTH = item.UnitTH,
			//		UnitEN = item.UnitEN,
			//		PromotionItemStatusMasterCenterID = item.PromotionItemStatusMasterCenterID,
			//		Quantity = item.Quantity,
			//		EDCFeeID = item.EDCFeeID
			//	};

			//	newMasterSalePromotionCreditCardItem.Add(model);
			//}
			var masterSalePromotionCreditCardItemsCreate = new List<MasterSalePromotionCreditCardItem>();
			foreach (var item in masterSalePromotionCreditCardItems)
			{
				MasterSalePromotionCreditCardItem model = new MasterSalePromotionCreditCardItem();
				model.MasterSalePromotionID = newMasterSalePromotion.ID;
				model.Quantity = 1;
				model.UnitTH = item.UnitTH;
				model.UnitEN = item.UnitEN;
				model.Fee = item.Fee;
				model.NameTH = item.NameTH;
				model.NameEN = item.NameEN;
				model.BankName = item.BankName;
				model.PromotionItemStatusMasterCenterID = item.PromotionItemStatusMasterCenterID;
				model.Order = 0;
				model.TotalPrice = item.TotalPrice;
				masterSalePromotionCreditCardItemsCreate.Add(model);
			}
			await DB.AddRangeAsync(masterSalePromotionCreditCardItemsCreate);


			await DB.MasterSalePromotions.AddAsync(newMasterSalePromotion);

			if (newMasterSalePromotionItems.Count() > 0)
			{
				await DB.MasterSalePromotionItems.AddRangeAsync(newMasterSalePromotionItems);
			}
			if (newMasterSaleHouseModelItems.Count() > 0)
			{
				await DB.MasterSaleHouseModelItems.AddRangeAsync(newMasterSaleHouseModelItems);
			}
			if (newMasterSalePromotionFreeItems.Count() > 0)
			{
				await DB.MasterSalePromotionFreeItems.AddRangeAsync(newMasterSalePromotionFreeItems);
			}
			if (newMasterSaleHouseModelFreeItems.Count() > 0)
			{
				await DB.MasterSaleHouseModelFreeItems.AddRangeAsync(newMasterSaleHouseModelFreeItems);
			}
			if (newMasterSalePromotionCreditCardItem.Count() > 0)
			{
				await DB.MasterSalePromotionCreditCardItems.AddRangeAsync(newMasterSalePromotionCreditCardItem);
			}
			await DB.SaveChangesAsync();

			return await this.GetMasterSalePromotionDetailAsync(newMasterSalePromotion.ID);
		}

		public async Task<CloneMasterPromotionConfirmDTO> GetCloneMasterSalePromotionConfirmAsync(Guid id,CancellationToken cancellationToken = default)
		{
			CloneMasterPromotionConfirmDTO result = new CloneMasterPromotionConfirmDTO();

			var statusActiveId = await DB.MasterCenters
										.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionItemStatus
													&& o.Key == "1")
										.Select(o => o.ID)
										.FirstOrDefaultAsync(cancellationToken);
			//Clone NotExpire and Active
			result.CloneItemCount = await DB.MasterSalePromotionItems
											.Where(o => o.MasterSalePromotionID == id && o.ExpireDate != null
											&& !string.IsNullOrEmpty(o.ItemNo)
											&& (!string.IsNullOrEmpty(o.MaterialCode) || o.AgreementNo.ToLower().Contains("welcomehome"))
											&& (!string.IsNullOrEmpty(o.GLAccountNo) || o.AgreementNo.ToLower().Contains("welcomehome"))
											&& !string.IsNullOrEmpty(o.MaterialGroupKey)
											&& o.ExpireDate > DateTime.Now
											&& o.PromotionItemStatusMasterCenterID == statusActiveId).CountAsync(cancellationToken);

			//Expire or NotActive
			result.ExpiredItemCount = await DB.MasterSalePromotionItems
											.Where(o => o.MasterSalePromotionID == id && o.ExpireDate != null
											&& (o.ExpireDate <= DateTime.Now || o.PromotionItemStatusMasterCenterID != statusActiveId)).CountAsync(cancellationToken);

			return result;
		}

		private async Task ValidatePromotionMaterial(Guid masterSalePromotionID, PromotionMaterialDTO input)
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

		private async Task<bool> MasterSalePromotionMaterialHasValue(PromotionMaterialDTO input, Guid masterSalePromotionID)
		{
			var masterSalePromotionItem = await DB.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID && o.MainPromotionItemID == null
												&& o.PromotionMaterialItemID == input.Id && o.IsDeleted == false).ToListAsync();

			return masterSalePromotionItem.Count > 0 ? true : false;
		}
		private async Task<bool> SubMasterSalePromotionMaterialHasValue(PromotionMaterialDTO input, Guid mainPromotionItemID, Guid masterSalePromotionID)
		{
			var childmasterSalePromotionItem = await DB.MasterSalePromotionItems.Where(o => o.MasterSalePromotionID == masterSalePromotionID && o.MainPromotionItemID == mainPromotionItemID
												&& o.PromotionMaterialItemID == input.Id && o.IsDeleted == false).ToListAsync();

			var parentmasterSalePromotionItem = await DB.MasterSalePromotionItems.Where(o => o.ID == mainPromotionItemID
												&& o.PromotionMaterialItemID == input.Id && o.IsDeleted == false).ToListAsync();

			int count = childmasterSalePromotionItem.Count + parentmasterSalePromotionItem.Count;

			return count > 0 ? true : false;
		}

		/// <summary>
		/// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358686/preview
		/// EDCFee จะดึงมาจาก GET PromotionMaterial API 
		/// </summary>
		/// <returns>The master booking credit card items.</returns>
		/// <param name="masterSalePromotionID">Master booking promotion identifier.</param>
		/// <param name="inputs">Inputs.</param>
		public async Task<List<MasterSalePromotionCreditCardItemDTO>> CreditCardItemsFromPromotionMaterialListAsync(Guid masterSalePromotionID, List<PromotionMaterialDTO> inputs)
		{
			var masterSalePromotionCreditCardItemsCreate = new List<MasterSalePromotionCreditCardItem>();
			var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstOrDefaultAsync();
			int i = 1;
			foreach (var item in inputs)
			{
				MasterSalePromotionCreditCardItem model = new MasterSalePromotionCreditCardItem();
				item.ToMasterSalePromotionCreditCardItemModel(ref model);
				model.MasterSalePromotionID = masterSalePromotionID;
				model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
				model.Order = 0;
				masterSalePromotionCreditCardItemsCreate.Add(model);
				i++;
			}
			await DB.AddRangeAsync(masterSalePromotionCreditCardItemsCreate);
			await DB.SaveChangesAsync();

			var createdIDs = masterSalePromotionCreditCardItemsCreate.Select(o => o.ID).ToList();
			var dataResult = await DB.MasterSalePromotionCreditCardItems
				.Include(o => o.PromotionItemStatus)
				.Include(o => o.UpdatedBy)
				.Where(o => createdIDs.Contains(o.ID)).ToListAsync();

			var results = dataResult.Select(o => MasterSalePromotionCreditCardItemDTO.CreateFromModel(o)).ToList();
			return results;
		}
 
    }
}
