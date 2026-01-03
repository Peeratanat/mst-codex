using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using ExcelExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using MST_Titledeeds.Params.Filters;
using MST_Titledeeds.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorHandling;
using System.Reflection;
using System.ComponentModel;
using NPOI.SS.Formula.Functions;
using Database.Models.LOG;
using static Database.Models.DbQueries.DBQueryParam;
using System.Data.SqlClient;
using Database.Models.DbQueries;
using Database.Models.DbQueries.PRJ;
using Database.Models.DbQueries.FIN;
using Database.Models.MST;
using Database.Models.DbQueries.ACC;
using Common.Helper.Logging;
using MST_Titledeeds.Services.Excels;
using MST_Titledeeds.Params.Inputs;
using Report.Integration;
using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

namespace MST_Titledeeds.Services
{
	public class TitleDeedService : ITitleDeedService
	{
		private readonly DatabaseContext DB;
		private readonly IConfiguration Configuration;
		private FileHelper FileHelper;
		public LogModel logModel { get; set; }

		public TitleDeedService(IConfiguration configuration, DatabaseContext db)
		{
			logModel = new LogModel("TitleDeedService", null);
			DB = db;

			var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
			var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
			var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
			var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
			var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
			var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
			var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

			FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
		}


		public async Task<TitleDeedPaging> GetTitleDeedListAsync(Guid? projectID, TitleDeedFilter filter, PageParam pageParam, TitleDeedListSortByParam sortByParam, CancellationToken cancellationToken = default)
		{
			IQueryable<TitleDeedQueryResult> query = from u in DB.Units.AsNoTracking().Where(o => o.ProjectID == projectID)
													 join ttd in DB.TitledeedDetails.AsNoTracking().Where(o => o.ProjectID == projectID)
													  on u.ID equals ttd.UnitID into ttdData
													 from ttdModel in ttdData.DefaultIfEmpty()
													 join assetT in DB.MasterCenters.AsNoTracking()
													   on u.AssetTypeMasterCenterID equals assetT.ID

													 where u.ProjectID == projectID

													 select new TitleDeedQueryResult
													 {
														 Unit = u,
														 Titledeed = ttdModel,
														 Project = ttdModel.Project,
														 Model = ttdModel.Unit.Model,
														 LandOffice = ttdModel.Unit.LandOffice,
														 LandStatus = ttdModel.LandStatus,
														 PreferStatus = ttdModel.PreferStatus,
														 UpdatedBy = ttdModel.UpdatedBy,
														 AssetType = assetT,
													 };


			IQueryable<TitleDeedQueryResult> queryChkTrf = (from u in DB.Units.Where(o => o.ProjectID == projectID)

															join arg in DB.Agreements.Where(o => o.IsDeleted == false && o.IsCancel == false)
															on u.ID equals arg.UnitID

															join trf in DB.Transfers.Where(o => o.IsDeleted == false)
															on arg.ID equals trf.AgreementID

															join ttd in DB.TitledeedDetails.Where(o => o.ProjectID == projectID)
															on u.ID equals ttd.UnitID into ttdData
															from ttdModel in ttdData.DefaultIfEmpty()

															select new TitleDeedQueryResult
															{
																Unit = u,
																Titledeed = ttdModel,
																Project = ttdModel.Project,
																Model = ttdModel.Unit.Model,
																LandOffice = ttdModel.Unit.LandOffice,
																LandStatus = ttdModel.LandStatus,
																PreferStatus = ttdModel.PreferStatus,
																UpdatedBy = ttdModel.UpdatedBy,
																AssetType = DB.MasterCenters.Where(o => o.ID == u.AssetTypeMasterCenterID).FirstOrDefault()
															});

			query = query.Where(o => o.AssetType.Key != "6" && o.AssetType.Key != "7");
			queryChkTrf = queryChkTrf.Where(o => o.AssetType.Key != "6" && o.AssetType.Key != "7");

			#region Filter
			//if (isData)
			//{
			if (!string.IsNullOrEmpty(filter.UnitNo))
			{
				query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
			}
			if (!string.IsNullOrEmpty(filter.TitledeedNo))
			{
				query = query.Where(x => x.Titledeed.TitledeedNo != null && x.Titledeed.TitledeedNo.Contains(filter.TitledeedNo));
				//query = query.Where(x => x.Titledeed.TitledeedNo.Contains(filter.TitledeedNo));
			}
			if (!string.IsNullOrEmpty(filter.HouseNo))
			{
				query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo));
			}
			if (filter.LandOfficeID != null && filter.LandOfficeID != Guid.Empty)
			{
				query = query.Where(x => x.LandOffice.ID == filter.LandOfficeID);
			}
			if (!string.IsNullOrEmpty(filter.HouseName))
			{
				Guid.TryParse(filter.HouseName, out Guid valueGuid);
				if (valueGuid != Guid.Empty)
				{
					query = query.Where(x => x.Model.ID == valueGuid);
				}
			}

			if (filter.TitledeedAreaFrom != null)
			{
				query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom);
			}
			if (filter.TitledeedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo);
			}
			if (filter.TitledeedAreaFrom != null && filter.TitledeedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom
										&& x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo);
			}

			if (filter.UsedAreaFrom != null)
			{
				query = query.Where(x => x.Titledeed.Unit.UsedArea >= filter.UsedAreaFrom);
			}
			if (filter.UsedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.Unit.UsedArea <= filter.UsedAreaTo);
			}
			if (filter.UsedAreaFrom != null && filter.UsedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.Unit.UsedArea >= filter.UsedAreaFrom
										&& x.Titledeed.Unit.UsedArea <= filter.UsedAreaTo);
			}

			if (!string.IsNullOrEmpty(filter.LandNo))
			{
				query = query.Where(x => x.Titledeed.LandNo.Contains(filter.LandNo));
			}
			if (!string.IsNullOrEmpty(filter.LandSurveyArea))
			{
				query = query.Where(x => x.Titledeed.LandSurveyArea.Contains(filter.LandSurveyArea));
			}
			if (!string.IsNullOrEmpty(filter.LandPortionNo))
			{
				query = query.Where(x => x.Titledeed.LandPortionNo.Contains(filter.LandPortionNo));
			}
			if (!string.IsNullOrEmpty(filter.LandStatusKey))
			{
				var landStatusMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(x => x.Key == filter.LandStatusKey
																	   && x.MasterCenterGroupKey == "LandStatus"))?.ID;
					query = query.Where(x => x.LandStatus.ID == landStatusMasterCenterID);
			}
			if (!string.IsNullOrEmpty(filter.UnitStatusKey))
			{
				var unitStatusMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(x => x.Key == filter.UnitStatusKey
																	   && x.MasterCenterGroupKey == "UnitStatus"))?.ID;
					query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
			}
			if (!string.IsNullOrEmpty(filter.PreferStatusKey))
			{
				var preferStatusMasterCenterID = (await DB.MasterCenters.FirstOrDefaultAsync(x => x.Key == filter.PreferStatusKey
																	   && x.MasterCenterGroupKey == "PreferStatus"))?.ID;
					query = query.Where(x => x.PreferStatus.ID == preferStatusMasterCenterID);
			}

			if (filter.LandStatusDateFrom != null)
			{
				query = query.Where(x => x.Titledeed.LandStatusDate >= filter.LandStatusDateFrom);
			}
			if (filter.LandStatusDateTo != null)
			{
				query = query.Where(x => x.Titledeed.LandStatusDate <= filter.LandStatusDateTo);
			}
			if (filter.LandStatusDateFrom != null && filter.LandStatusDateTo != null)
			{
				query = query.Where(x => x.Titledeed.LandStatusDate >= filter.LandStatusDateFrom
										&& x.Titledeed.LandStatusDate <= filter.LandStatusDateTo);
			}


			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(x => x.Titledeed.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(x => x.Titledeed.Updated <= filter.UpdatedTo);
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				query = query.Where(x => x.Titledeed.Updated >= filter.UpdatedFrom && x.Titledeed.Updated <= filter.UpdatedTo);
			}
			if (filter.BuildingPermitAreaFrom != null && filter.BuildingPermitAreaTo != null)
			{
				query = query.Where(x => x.Unit.BuildingPermitArea >= filter.BuildingPermitAreaFrom && x.Unit.BuildingPermitArea <= filter.BuildingPermitAreaTo);
			}

			//}
			#endregion

			var ProJect = await DB.Projects.Where(o => o.ID == projectID
											&& (
												(o.MortgageBankID == null && o.RedeemLoanDate == null)
												|| (o.MortgageBankID == null && o.RedeemLoanDate != null && o.RedeemLoanDate <= DateTime.Now)
												|| (o.MortgageBankID != null && o.RedeemLoanDate != null && o.RedeemLoanDate <= DateTime.Now)
											)).AnyAsync();


			TitleDeedListDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<TitleDeedQueryResult>(pageParam, ref query);

			var queryResults = await query.OrderBy(c => c.Unit.UnitNo).ToListAsync(cancellationToken);
			var queryChkTrfResults = await queryChkTrf.Select(o => o.Unit.UnitNo).ToListAsync(cancellationToken);



			var results = queryResults.Select(o => TitleDeedListDTO.CreateFromQueryTitleUnitResult(o, queryChkTrfResults, ProJect)).ToList();

			return new TitleDeedPaging()
			{
				PageOutput = pageOutput,
				TitleDeeds = results
			};
		}
		public async Task<TitleDeedPaging> GetTitleDeedStatusListAsync(Guid? projectID, TitleDeedFilter filter, PageParam pageParam, TitleDeedListSortByParam sortByParam, CancellationToken cancellationToken = default)
		{

			IQueryable<TitleDeedQueryResult> query = DB.TitledeedDetails.Where(o => o.ProjectID != null)
													.Include(o => o.Unit.UnitStatus)
													.Select(o => new TitleDeedQueryResult
													{
														Titledeed = o,
														Project = o.Project,
														Unit = o.Unit,
														Model = o.Unit.Model,
														LandOffice = o.Unit.LandOffice,
														LandStatus = o.LandStatus,
														PreferStatus = o.PreferStatus,
														UpdatedBy = o.UpdatedBy
													});


			#region Filter
			if (projectID != null)
			{
				query = query.Where(o => o.Project.ID == projectID);
			}
			if (!string.IsNullOrEmpty(filter.UnitNo))
			{
				query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
			}
			if (!string.IsNullOrEmpty(filter.TitledeedNo))
			{
				query = query.Where(x => x.Titledeed.TitledeedNo.Contains(filter.TitledeedNo));
			}
			if (!string.IsNullOrEmpty(filter.HouseNo))
			{
				query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo));
			}
			if (filter.LandOfficeID != null && filter.LandOfficeID != Guid.Empty)
			{
				query = query.Where(x => x.LandOffice.ID == filter.LandOfficeID);
			}
			if (!string.IsNullOrEmpty(filter.HouseName))
			{
				query = query.Where(x => x.Model.ID == Guid.Parse(filter.HouseName));
			}

			if (filter.TitledeedAreaFrom != null)
			{
				query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom);
			}
			if (filter.TitledeedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo);
			}
			if (filter.TitledeedAreaFrom != null && filter.TitledeedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom
										&& x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo);
			}

			if (filter.UsedAreaFrom != null)
			{
				query = query.Where(x => x.Titledeed.Unit.UsedArea >= filter.UsedAreaFrom);
			}
			if (filter.UsedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.Unit.UsedArea <= filter.UsedAreaTo);
			}
			if (filter.UsedAreaFrom != null && filter.UsedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.Unit.UsedArea >= filter.UsedAreaFrom
										&& x.Titledeed.Unit.UsedArea <= filter.UsedAreaTo);
			}

			if (!string.IsNullOrEmpty(filter.LandNo))
			{
				query = query.Where(x => x.Titledeed.LandNo.Contains(filter.LandNo));
			}
			if (!string.IsNullOrEmpty(filter.LandSurveyArea))
			{
				query = query.Where(x => x.Titledeed.LandSurveyArea.Contains(filter.LandSurveyArea));
			}
			if (!string.IsNullOrEmpty(filter.LandPortionNo))
			{
				query = query.Where(x => x.Titledeed.LandPortionNo.Contains(filter.LandPortionNo));
			}
			if (!string.IsNullOrEmpty(filter.LandStatusKey))
			{
				var landStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.LandStatusKey
																	   && x.MasterCenterGroupKey == "LandStatus")
																	  .Select(x => x.ID).FirstAsync();
				query = query.Where(x => x.LandStatus.ID == landStatusMasterCenterID);
			}
			if (!string.IsNullOrEmpty(filter.UnitStatusKey))
			{
				var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
																	   && x.MasterCenterGroupKey == "UnitStatus")
																	  .Select(x => x.ID).FirstAsync();
				query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
			}
			if (!string.IsNullOrEmpty(filter.PreferStatusKey))
			{
				var preferStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.PreferStatusKey
																	   && x.MasterCenterGroupKey == "PreferStatus")
																	  .Select(x => x.ID).FirstAsync();
				query = query.Where(x => x.PreferStatus.ID == preferStatusMasterCenterID);
			}

			if (filter.LandStatusDateFrom != null)
			{
				query = query.Where(x => x.Titledeed.LandStatusDate >= filter.LandStatusDateFrom);
			}
			if (filter.LandStatusDateTo != null)
			{
				query = query.Where(x => x.Titledeed.LandStatusDate <= filter.LandStatusDateTo);
			}
			if (filter.LandStatusDateFrom != null && filter.LandStatusDateTo != null)
			{
				query = query.Where(x => x.Titledeed.LandStatusDate >= filter.LandStatusDateFrom
										&& x.Titledeed.LandStatusDate <= filter.LandStatusDateTo);
			}


			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(x => x.Titledeed.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(x => x.Titledeed.Updated <= filter.UpdatedTo);
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				query = query.Where(x => x.Titledeed.Updated >= filter.UpdatedFrom && x.Titledeed.Updated <= filter.UpdatedTo);
			}

			#endregion

			TitleDeedListDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<TitleDeedQueryResult>(pageParam, ref query);

			var queryResults = await query.ToListAsync(cancellationToken);

			var results = queryResults.Select(o => TitleDeedListDTO.CreateFromQueryResult(o)).ToList();

			return new TitleDeedPaging()
			{
				PageOutput = pageOutput,
				TitleDeeds = results
			};
		}

		public async Task<TitleDeedPaging> GetTitleDeedStatusSelectAllListAsync(Guid? projectID, TitleDeedFilter filter, PageParam pageParam, TitleDeedListSortByParam sortByParam, CancellationToken cancellationToken = default)
		{

			IQueryable<TitleDeedQueryResult> query = DB.TitledeedDetails.Where(o => o.ProjectID != null)
													.Include(o => o.Unit.UnitStatus)
													.Select(o => new TitleDeedQueryResult
													{
														Titledeed = o,
														Project = o.Project,
														Unit = o.Unit,
														Model = o.Unit.Model,
														LandOffice = o.Unit.LandOffice,
														LandStatus = o.LandStatus,
														PreferStatus = o.PreferStatus,
														UpdatedBy = o.UpdatedBy
													});


			#region Filter
			if (projectID != null)
			{
				query = query.Where(o => o.Project.ID == projectID);
			}
			if (!string.IsNullOrEmpty(filter.UnitNo))
			{
				query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
			}
			if (!string.IsNullOrEmpty(filter.TitledeedNo))
			{
				query = query.Where(x => x.Titledeed.TitledeedNo.Contains(filter.TitledeedNo));
			}
			if (!string.IsNullOrEmpty(filter.HouseNo))
			{
				query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo));
			}
			if (filter.LandOfficeID != null && filter.LandOfficeID != Guid.Empty)
			{
				query = query.Where(x => x.LandOffice.ID == filter.LandOfficeID);
			}
			if (!string.IsNullOrEmpty(filter.HouseName))
			{
				Guid.TryParse(filter.HouseName, out Guid valueGuid);
				if (valueGuid != Guid.Empty)
				{
					query = query.Where(x => x.Model.ID == valueGuid);
				}
			}

			if (filter.TitledeedAreaFrom != null)
			{
				query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom);
			}
			if (filter.TitledeedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo);
			}
			if (filter.TitledeedAreaFrom != null && filter.TitledeedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom
										&& x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo);
			}

			if (filter.UsedAreaFrom != null)
			{
				query = query.Where(x => x.Titledeed.Unit.UsedArea >= filter.UsedAreaFrom);
			}
			if (filter.UsedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.Unit.UsedArea <= filter.UsedAreaTo);
			}
			if (filter.UsedAreaFrom != null && filter.UsedAreaTo != null)
			{
				query = query.Where(x => x.Titledeed.Unit.UsedArea >= filter.UsedAreaFrom
										&& x.Titledeed.Unit.UsedArea <= filter.UsedAreaTo);
			}

			if (!string.IsNullOrEmpty(filter.LandNo))
			{
				query = query.Where(x => x.Titledeed.LandNo.Contains(filter.LandNo));
			}
			if (!string.IsNullOrEmpty(filter.LandSurveyArea))
			{
				query = query.Where(x => x.Titledeed.LandSurveyArea.Contains(filter.LandSurveyArea));
			}
			if (!string.IsNullOrEmpty(filter.LandPortionNo))
			{
				query = query.Where(x => x.Titledeed.LandPortionNo.Contains(filter.LandPortionNo));
			}
			if (!string.IsNullOrEmpty(filter.LandStatusKey))
			{
				var landStatusMasterCenterID = await DB.MasterCenters.FirstOrDefaultAsync(x => x.Key == filter.LandStatusKey
																	   && x.MasterCenterGroupKey == "LandStatus");
				if (landStatusMasterCenterID is not null)
					query = query.Where(x => x.LandStatus.ID == landStatusMasterCenterID.ID);
			}
			if (!string.IsNullOrEmpty(filter.UnitStatusKey))
			{
				var unitStatusMasterCenterID = await DB.MasterCenters.FirstOrDefaultAsync(x => x.Key == filter.UnitStatusKey
																	   && x.MasterCenterGroupKey == "UnitStatus");
				if (unitStatusMasterCenterID is not null)
					query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID.ID);
			}
			if (!string.IsNullOrEmpty(filter.PreferStatusKey))
			{
				var preferStatusMasterCenterID = await DB.MasterCenters.FirstOrDefaultAsync(x => x.Key == filter.PreferStatusKey
																	   && x.MasterCenterGroupKey == "PreferStatus");
				if (preferStatusMasterCenterID is not null)
					query = query.Where(x => x.PreferStatus.ID == preferStatusMasterCenterID.ID);
			}

			if (filter.LandStatusDateFrom != null)
			{
				query = query.Where(x => x.Titledeed.LandStatusDate >= filter.LandStatusDateFrom);
			}
			if (filter.LandStatusDateTo != null)
			{
				query = query.Where(x => x.Titledeed.LandStatusDate <= filter.LandStatusDateTo);
			}
			if (filter.LandStatusDateFrom != null && filter.LandStatusDateTo != null)
			{
				query = query.Where(x => x.Titledeed.LandStatusDate >= filter.LandStatusDateFrom
										&& x.Titledeed.LandStatusDate <= filter.LandStatusDateTo);
			}


			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(x => x.Titledeed.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(x => x.Titledeed.Updated <= filter.UpdatedTo);
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				query = query.Where(x => x.Titledeed.Updated >= filter.UpdatedFrom && x.Titledeed.Updated <= filter.UpdatedTo);
			}

			#endregion

			TitleDeedListDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging<TitleDeedQueryResult>(pageParam, ref query);

			var queryResults = await query.ToListAsync(cancellationToken);

			var results = queryResults.Select(o => TitleDeedListDTO.CreateFromQueryResult(o)).ToList();

			return new TitleDeedPaging()
			{
				PageOutput = pageOutput,
				TitleDeeds = results
			};
		}

		public async Task<TitleDeedDTO> GetTitleDeedAsync(Guid id, CancellationToken cancellationToken = default)
		{
			var model = await DB.TitledeedDetails.Where(o => o.ID == id)
												 .Include(o => o.Project)
												 .Include(o => o.Unit)
												 .ThenInclude(o => o.LandOffice)
												 .Include(o => o.Unit)
												 .ThenInclude(o => o.HouseProvince)
												 .Include(o => o.Unit)
												 .ThenInclude(o => o.HouseDistrict)
												 .Include(o => o.Unit)
												 .ThenInclude(o => o.HouseSubDistrict)
												 .Include(o => o.Address)
												 .Include(o => o.Address.Province)
												 .Include(o => o.Address.District)
												 .Include(o => o.Address.District.SubDistricts)
												 .Include(o => o.LandStatus)
												 .Include(o => o.PreferStatus)
												 .Include(o => o.UpdatedBy)
												 .FirstOrDefaultAsync(cancellationToken);

			var result = TitleDeedDTO.CreateFromModel(model);
			return result;
		}

		public async Task<TitleDeedDTO> UpdateTitleDeedStatusAsync(Guid id, TitleDeedDTO input)
		{
			var model = await DB.TitledeedDetails.FindAsync(id);
			model.LandStatusMasterCenterID = input.LandStatus?.Id;
			model.PreferStatusMasterCenterID = input.PreferStatus?.Id;
			model.LandStatusDate = input.LandStatusDate;
			model.LandStatusNote = input.LandStatusNote;
			var newModel = model.CloneToHistoryItem();

			DB.Entry(model).State = EntityState.Modified;
			await DB.AddAsync(newModel);
			await DB.SaveChangesAsync();
			var result = await GetTitleDeedAsync(model.ID);
			return result;
		}

		public async Task<TitleDeedDTO> UpdateTitleDeedListStatusAsync(Guid id, List<TitleDeedDTO> inputs)
		{
			foreach (TitleDeedDTO input in inputs)
			{
				var model = await DB.TitledeedDetails.Where(o => o.ID == input.Id).FirstAsync();
				model.LandStatusMasterCenterID = input.LandStatus?.Id;
				model.PreferStatusMasterCenterID = input.PreferStatus?.Id;
				model.LandStatusDate = input.LandStatusDate;
				model.LandStatusNote = input.LandStatusNote;
				var newModel = model.CloneToHistoryItem();

				DB.Entry(model).State = EntityState.Modified;
				await DB.AddAsync(newModel);
				await DB.SaveChangesAsync();
			}

			var result = new TitleDeedDTO();
			return result;
		}

		public async Task<List<TitleDeedDTO>> GetTitleDeedHistoryItemsAsync(Guid id, CancellationToken cancellationToken = default)
		{
			var model = await DB.TitledeedDetails.Where(o => o.ID == id).FirstAsync(cancellationToken);
			var historyModels = await DB.TitledeedDetailHistories
				.Where(o => o.TitledeedDetailID == model.ID)
				.Include(o => o.Unit)
				.Include(o => o.Project)
				.Include(o => o.LandStatusMasterCenter)
				.Include(o => o.PreferStatus)
				.Include(o => o.CreatedBy)
				.OrderBy(o => o.Created)
				.ToListAsync(cancellationToken);

			var results = historyModels.Select(o => TitleDeedDTO.CreateFromHistoryModel(o)).ToList();

			return results;
		}
		public async Task<ReportResult> ExportDebtFreePrintFormUrlAsync(TitleDeedReportDTO input, Guid? userID)
		{
			ValidateException ex = new ValidateException();
			if (input == null || input.ProjectNo == null || input.DateStart == null || input.UnitNo == null)
			{
				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0177").FirstAsync();
				string desc = typeof(TitleDeedReportDTO).GetProperty(nameof(TitleDeedReportDTO.DateStart)).GetCustomAttribute<DescriptionAttribute>().Description;
				var msg = errMsg.Message.Replace("[field]", desc);
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}
			if (ex.HasError)
			{
				throw ex;
			}

			string printform = "PF_LC_004";
			string unitNos = string.Empty;
			int i = 1;
			foreach (var item in input.UnitNo)
			{
				if (i == 1)
				{
					unitNos = item.ToString();
				}
				else
				{
					unitNos = String.Concat(unitNos, ",", item.ToString());
				}
				i++;
			}

			string projectIDs = string.Empty;
			int j = 1;
			foreach (var items in input.ProjectID)
			{
				if (j == 1)
				{
					projectIDs = items.ToString();
				}
				else
				{
					projectIDs = String.Concat(projectIDs, ",", items.ToString());
				}
				j++;
			}

			ReportFactory reportFactory = null;
			reportFactory = new ReportFactory(Configuration, ReportFolder.LC, printform, ShowAs.PDF);
			reportFactory.AddParameter("@DateStart", input.DateStart);
			reportFactory.AddParameter("@ProjectID", projectIDs);
			reportFactory.AddParameter("@UnitID", unitNos);



			return reportFactory.CreateUrl();
		}

		public async Task<TitleDeedHistoryExcelDTO> ImportTitleDeedHistoryAsync(Guid projectID, FileDTO input, Guid? UserID = null)
		{
			var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
			var result = new TitleDeedHistoryExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>(), Messages = new List<string>() };

			if (input.IsTemp)
			{

				string Name = "TitledeedStatus.xlsx";
				string generalunitsName = $"import-project/{projectNo}/titledeedstatus/{Name}";
				await FileHelper.MoveTempFileAsync(input.Name, generalunitsName);
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
			imp.Import_Type = "titledeedstatus";
			imp.Import_Status = "I";

			await DB.ImptMstProjTrans.AddAsync(imp);
			await DB.SaveChangesAsync();

			return result;
		}

		public async Task<FileDTO> ExportTitleDeedStatusAsync(Guid projectID, TitleDeedDTO filter, CancellationToken cancellationToken = default)
		{
			{
				using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
				DynamicParameters ParamList = new();
				ParamList.Add("ProjectID", projectID);
				ParamList.Add("LandStatusID", filter.LandStatus?.Id);
				ParamList.Add("LandStatusDateFrom", filter.LandStatusDateFrom);
				ParamList.Add("LandStatusDateTo", filter.LandStatusDateTo);
				ParamList.Add("UnitID", filter.UnitIDs);

				CommandDefinition commandDefinition = new(
							 commandText: DBStoredNames.sp_TitleDeedStatusList,
							 parameters: ParamList,
									 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
							 commandType: CommandType.StoredProcedure,
							 cancellationToken: cancellationToken
						 );
				var queryResult = await cmd.Connection.QueryAsync<dbqTitleDeedStatusList>(commandDefinition) ?? new List<dbqTitleDeedStatusList>();
				var data = queryResult.Select(x => TitleDeedStatusDTO.CreateFromModel(x)).ToList();

				ExportExcel result = new ExportExcel();
				string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_TitleDeedStatus.xlsx");
				byte[] tmp = await File.ReadAllBytesAsync(path);

				using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))

				using (ExcelPackage package = new ExcelPackage(stream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

					int _projectnoIndex = TitledeedStatusExcelModel._projectnoIndex + 1;
					int _projectnameIndex = TitledeedStatusExcelModel._projectnameIndex + 1;
					int _unitnoIndex = TitledeedStatusExcelModel._unitnoIndex + 1;
					int _titledeednoIndex = TitledeedStatusExcelModel._titledeednoIndex + 1;
					int _titledeedareaIndex = TitledeedStatusExcelModel._titledeedareaIndex + 1;
					int _allownernameIndex = TitledeedStatusExcelModel._allownernameIndex + 1;
					int _totalpriceIndex = TitledeedStatusExcelModel._totalpriceIndex + 1;
					int _repayloanIndex = TitledeedStatusExcelModel._repayloanIndex + 1;

					var Project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
					int rowCount = 0;
					for (int c = 2; c < data.Count + 2; c++)
					{
						worksheet.Cells[c, _projectnoIndex].Value = data[c - 2].ProjectNo;
						worksheet.Cells[c, _projectnameIndex].Value = data[c - 2].ProjectNameTH;
						worksheet.Cells[c, _unitnoIndex].Value = data[c - 2].UnitNo;
						worksheet.Cells[c, _titledeednoIndex].Value = data[c - 2].TitledeedNo;
						worksheet.Cells[c, _titledeedareaIndex].Value = data[c - 2].TitledeedArea;

						worksheet.Cells[c, _allownernameIndex].Value = data[c - 2].AllOwnerName;
						worksheet.Cells[c, _totalpriceIndex].Value = data[c - 2].TotalPrice;
						worksheet.Cells[c, _repayloanIndex].Value = data[c - 2].RepayLoan;
						rowCount += 1;
					}

					result.FileContent = package.GetAsByteArray();
					result.FileName = Project.ProjectNo + "_TitleDeedStatus.xlsx";
					result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				}
				Stream fileStream = new MemoryStream(result.FileContent);
				string fileName = result.FileName;
				string contentType = result.FileType;
				string filePath = $"project/{projectID}/export-excels/";
				var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
				var uploadResult = await FileHelper.UploadFileFromStreamWithOutGuid(fileStream, minioTempBucketName, filePath, fileName, contentType);
				return new FileDTO()
				{
					Name = result.FileName,
					Url = uploadResult.Url
				};
			}

		}

    }
}
