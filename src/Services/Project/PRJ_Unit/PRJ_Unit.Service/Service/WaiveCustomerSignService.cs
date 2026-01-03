using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.LOG;
using Database.Models.PRJ;
using ErrorHandling;
using ExcelExtensions;
using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Outputs;
using PRJ_Unit.Services.Excels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage;
namespace PRJ_Unit.Services
{
	public class WaiveCustomerSignService : IWaiveCustomerSignService
	{
		private readonly DatabaseContext DB;
		private readonly IConfiguration Configuration;
		public LogModel logModel { get; set; }
		private FileHelper FileHelper;

		public WaiveCustomerSignService(DatabaseContext db)
		{
			this.DB = db;
			logModel = new LogModel("WaiveCustomerSignService", null);

			var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
			var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
			var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
			var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
			var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
			var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
			var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

			this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
		}
		public WaiveCustomerSignService(IConfiguration configuration, DatabaseContext db)
		{
			logModel = new LogModel("WaiveCustomerSignService", null);
			this.DB = db;
			this.Configuration = configuration;

			var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
			var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
			var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
			var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
			var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
			var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
			var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

			this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
		}

		public async Task<WaiveCustomerSignPaging> GetWaiveCustomerSignListAsync(Guid projectID, WaiveCustomerSignFilter filter, PageParam pageParam, WaiveCustomerSignSortByParam sortByParam, CancellationToken cancellationToken = default)
		{
			IQueryable<WaiveCustomerSignQueryResult> query = from u in DB.Units.AsNoTracking().Where(o => o.ProjectID == projectID)

															 join wqc in DB.WaiveQCs.AsNoTracking().Where(o => o.ProjectID == projectID)
																 on u.ID equals wqc.UnitID into wqcData
															 from wqcModel in wqcData.DefaultIfEmpty()

															 join mas in DB.MasterCenters.AsNoTracking()
																 on u.UnitStatusMasterCenterID equals mas.ID into masData
															 from masModel in masData.DefaultIfEmpty()

															 select new WaiveCustomerSignQueryResult
															 {
																 Unit = u,
																 MasterCenter = masModel,
																 UpdatedBy = wqcModel.UpdatedBy,
																 WaiveQC = wqcModel
															 };

			// Query 2
			IQueryable<WaiveCustomerSignQueryResult> queryChkTrf = from u in DB.Units.AsNoTracking().Where(o => o.ProjectID == projectID)

																   join arg in DB.Agreements.AsNoTracking().Where(o => !o.IsDeleted && !o.IsCancel)
																	   on u.ID equals arg.UnitID

																   join trf in DB.Transfers.AsNoTracking().Where(o => !o.IsDeleted)
																	   on arg.ID equals trf.AgreementID

																   join wqc in DB.WaiveQCs.AsNoTracking().Where(o => o.ProjectID == projectID)
																	   on u.ID equals wqc.UnitID into wqcData
																   from wqcModel in wqcData.DefaultIfEmpty()

																   join mas in DB.MasterCenters.AsNoTracking()
																	   on u.UnitStatusMasterCenterID equals mas.ID into masData
																   from masModel in masData.DefaultIfEmpty()

																   select new WaiveCustomerSignQueryResult
																   {
																	   Unit = u,
																	   MasterCenter = masData.FirstOrDefault(),
																	   UpdatedBy = u.UpdatedBy,
																	   WaiveQC = wqcModel
																   };

			// Filter and sorting
			if (filter.UnitID != null)
			{
				query = query.Where(o => o.Unit.ID == filter.UnitID);
			}
			if (filter.ActualTransferDateFrom != null)
			{
				query = query.Where(o => o.WaiveQC.ActualTransferDate >= filter.ActualTransferDateFrom);
			}
			if (filter.ActualTransferDateTo != null)
			{
				query = query.Where(o => o.WaiveQC.ActualTransferDate <= filter.ActualTransferDateTo);
			}
			if (filter.ActualTransferDateFrom != null && filter.ActualTransferDateTo != null)
			{
				query = query.Where(o => o.WaiveQC.ActualTransferDate >= filter.ActualTransferDateFrom
										&& o.WaiveQC.ActualTransferDate <= filter.ActualTransferDateTo);
			}

			if (filter.WaiveSignDateFrom != null)
			{
				query = query.Where(o => o.WaiveQC.WaiveSignDate >= filter.WaiveSignDateFrom);
			}
			if (filter.WaiveSignDateTo != null)
			{
				query = query.Where(o => o.WaiveQC.WaiveSignDate <= filter.WaiveSignDateTo);
			}
			if (filter.WaiveSignDateFrom != null && filter.WaiveSignDateTo != null)
			{
				query = query.Where(o => o.WaiveQC.WaiveSignDate >= filter.WaiveSignDateFrom
										&& o.WaiveQC.WaiveSignDate <= filter.WaiveSignDateTo);
			}

			if (!string.IsNullOrEmpty(filter.UnitStatusKey))
			{
				var unitStatusMasterCenterID = await DB.MasterCenters
					.Where(x => x.Key == filter.UnitStatusKey && x.MasterCenterGroupKey == "UnitStatus")
					.Select(x => x.ID).FirstAsync();
				query = query.Where(o => o.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
			}

			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(x => x.WaiveQC.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(x => x.WaiveQC.Updated <= filter.UpdatedTo);
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				query = query.Where(x => x.WaiveQC.Updated >= filter.UpdatedFrom && x.WaiveQC.Updated <= filter.UpdatedTo);
			}

			// Sorting
			WaiveCustomerSignDTO.SortBy(sortByParam, ref query);

			// Paging
			var pageOutput = PagingHelper.Paging(pageParam, ref query);

			// Execute queries
			var queryResults = await query.ToListAsync(cancellationToken);
			var queryChkTrfResults = await queryChkTrf.Select(o => o.Unit.UnitNo).ToListAsync(cancellationToken);
			var results = queryResults.Select(o => WaiveCustomerSignDTO.CreateFromQueryResult(o, queryChkTrfResults)).ToList();
			//var results = isData ? queryResults.Select(o => WaiveCustomerSignDTO.CreateFromQueryResult(o)).ToList()
			//                : dataT.Select(o => WaiveCustomerSignDTO.CreateFromQueryTempResult(o)).ToList();

			return new WaiveCustomerSignPaging()
			{
				PageOutput = pageOutput,
				WaiveCustomerSigns = results
			};
		}

		public async Task<WaiveCustomerSignDTO> GetWaiveCustomerSignAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default)
		{
			var model = await DB.WaiveQCs.AsNoTracking()
										 .Include(o => o.Unit)
										 .Include(o => o.Unit.UnitStatus)
										 .Include(o => o.UpdatedBy)
										 .FirstOrDefaultAsync(o => o.ProjectID == projectID && o.ID == id, cancellationToken);
			var result = WaiveCustomerSignDTO.CreateFromModel(model);
			return result;
		}

		public async Task<WaiveCustomerSignDTO> CreateWaiveCustomerSignAsync(Guid projectID, WaiveCustomerSignDTO input)
		{
			WaiveQC model = new WaiveQC();
			input.ToModel(ref model, false);
			model.ProjectID = projectID;
			await DB.WaiveQCs.AddAsync(model);
			await DB.SaveChangesAsync();

			var result = await this.GetWaiveCustomerSignAsync(projectID, model.ID);
			return result;
		}

		public async Task<WaiveCustomerSignDTO> UpdateWaiveCustomerSignAsync(Guid projectID, Guid? id, WaiveCustomerSignDTO input)
		{
			var model = await DB.WaiveQCs.Where(o => o.ProjectID == projectID && o.ID == id).FirstOrDefaultAsync();
			if (model != null)  //Update
			{
				//input.ToModel(ref model, true);
				//model.ProjectID = projectID;
				//DB.Entry(model).State = EntityState.Modified;
				var checkUnitStatus = await DB.WaiveQCs.Where(o => o.ProjectID == projectID && o.ID == id)
										.Include(o => o.Unit)
										.FirstOrDefaultAsync();

				ValidateException ex = new ValidateException();
				var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == "4" && x.IsActive == true
												&& x.MasterCenterGroupKey == "UnitStatus").Select(x => x.ID).FirstOrDefaultAsync();
				if (checkUnitStatus.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID)
				{
					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0143").FirstOrDefaultAsync();
					string desc = checkUnitStatus.Unit.UnitNo;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
				if (ex.HasError)
				{
					throw ex;
				}

				input.ToModel(ref model, true);
				model.ProjectID = projectID;
				DB.Entry(model).State = EntityState.Modified;
			}
			else
			{
				WaiveQC modelAdd = new WaiveQC();
				input.ToModel(ref modelAdd, false);
				modelAdd.ProjectID = projectID;
				await DB.WaiveQCs.AddAsync(modelAdd);
				id = modelAdd.ID;
			}
			await DB.SaveChangesAsync();
			if (id is null) return null;
			var result = await this.GetWaiveCustomerSignAsync(projectID, (Guid)id);
			return result;
		}

		public async Task<WaiveQC> DeleteWaiveCustomerSignAsync(Guid projectID, Guid id)
		{
			var model = await DB.WaiveQCs.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();
			model.IsDeleted = true;
			await DB.SaveChangesAsync();
			return model;
		}

		public async Task<WaiveCustomerSignExcelDTO> ImportWaiveCustomerSignTempAsync(Guid projectID, FileDTO input)
		{
			// Require
			var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();

			// FormatDate
			var err0071 = await DB.ErrorMessages.Where(o => o.Key == "ERR0071").FirstAsync();

			// Not Found
			var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();

			var units = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();

			var result = new WaiveCustomerSignExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>() };

			var dt = await this.ConvertExcelToDataTable(input);

			/// Valudate Header
			if (dt.Columns.Count != 5)
			{
				throw new Exception("Invalid File Format");
			}
			var row = 2;
			var error = 0;

			var checkNullUnitNos = new List<string>();
			var checkNullWbsNos = new List<string>();
			var checkFormateWaiveSignDates = new List<string>();
			var checkUnitNotFounds = new List<string>();
			//Read Excel Model
			var waiveCustomerSignExcelModels = new List<WaiveCustomerSignExcelModel>();
			foreach (DataRow r in dt.Rows)
			{
				var isError = false;
				var excelModel = WaiveCustomerSignExcelModel.CreateFromDataRow(r);
				waiveCustomerSignExcelModels.Add(excelModel);

				#region Validate
				var unit = units.Find(o => o.UnitNo == excelModel.UnitNo && o.SAPWBSNo == excelModel.WBSNo);
				if (unit == null)
				{
					checkUnitNotFounds.Add((row).ToString());
					isError = true;
				}
				if (string.IsNullOrEmpty(excelModel.WBSNo))
				{
					checkNullWbsNos.Add((row).ToString());
					isError = true;
				}
				if (string.IsNullOrEmpty(excelModel.UnitNo))
				{
					checkNullUnitNos.Add((row).ToString());
					isError = true;
				}
				if (!string.IsNullOrEmpty(r[WaiveCustomerSignExcelModel._waiveSignDateIndex].ToString()))
				{
					//if (!r[WaiveCustomerSignExcelModel._waiveSignDateIndex].ToString().isFormatDate())
					//{
					//	checkFormateWaiveSignDates.Add((row).ToString());
					//	isError = true;
					//}
				}
				#endregion

				if (isError)
				{
					error++;
				}
				row++;
			}
			#region ValidateProject

			var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
			ValidateException ex = new ValidateException();
			if (waiveCustomerSignExcelModels.Any(o => o.ProjectNo != project.ProjectNo))
			{
				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
				var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ");
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}
			if (ex.HasError)
			{
				throw ex;
			}
			#endregion

			#region AddResultErrorMassage


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

			if (checkNullWbsNos.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0061.Message.Replace("[column]", "WBS Code");
					msg = msg.Replace("[row]", String.Join(",", checkNullWbsNos));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0061.Message.Replace("[column]", "WBS Code");
					msg = msg.Replace("[row]", String.Join(",", checkNullWbsNos));
					result.ErrorMessages.Add(msg);
				}
			}

			if (checkFormateWaiveSignDates.Any())
			{
				if (result.ErrorMessages != null)
				{
					var msg = err0071.Message.Replace("[column]", "วันที่ Waive Sign");
					msg = msg.Replace("[row]", String.Join(",", checkFormateWaiveSignDates));
					result.ErrorMessages.Add(msg);
				}
				else
				{
					result.ErrorMessages = new List<string>();
					var msg = err0071.Message.Replace("[column]", "วันที่ Waive Sign");
					msg = msg.Replace("[row]", String.Join(",", checkFormateWaiveSignDates));
					result.ErrorMessages.Add(msg);
				}
			}

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

			#region RowErrors
			var rowErrors = new List<string>();
			rowErrors.AddRange(checkNullUnitNos);
			rowErrors.AddRange(checkNullWbsNos);
			rowErrors.AddRange(checkFormateWaiveSignDates);
			rowErrors.AddRange(checkUnitNotFounds);
			#endregion


			var waiveQCs = await DB.WaiveQCs.Where(o => o.ProjectID == projectID).ToListAsync();

			List<WaiveQC> waiveQCsCreate = new List<WaiveQC>();
			List<WaiveQC> waiveQCsUpdate = new List<WaiveQC>();

			var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
			row = 2;
			//Update Data
			foreach (var item in waiveCustomerSignExcelModels)
			{
				if (!rowIntErrors.Contains(row))
				{
					var unit = units.Find(o => o.UnitNo == item.UnitNo && o.SAPWBSNo == item.WBSNo);
					if (unit != null)
					{
						var existedwaive = waiveQCs.Find(o => o.UnitID == unit.ID);
						if (existedwaive == null)
						{
							WaiveQC waive = new WaiveQC();
							item.ToModel(ref waive);
							waive.ProjectID = projectID;
							waive.UnitID = unit.ID;
							result.Success++;
							waiveQCsCreate.Add(waive);
						}
						else
						{
							item.ToModel(ref existedwaive);
							existedwaive.ProjectID = projectID;
							existedwaive.UnitID = unit.ID;
							result.Success++;
							waiveQCsUpdate.Add(existedwaive);
						}
					}
				}
				row++;
			}
			await DB.WaiveQCs.AddRangeAsync(waiveQCsCreate);
			DB.UpdateRange(waiveQCsUpdate);
			await DB.SaveChangesAsync();
			result.Error = error;
			return result;
		}

		public async Task<WaiveCustomerSignExcelDTO> ImportWaiveCustomerSignAsync(Guid projectID, FileDTO input, Guid? UserID = null)
		{
			var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
			var result = new WaiveCustomerSignExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>(), Messages = new List<string>() };

			if (input.IsTemp)
			{
				string Name = "WaiveCustomerSign.xlsx";
				string titledeedName = $"import-project/{projectNo}/wavecustomersign/{Name}";
				await FileHelper.MoveTempFileAsync(input.Name, titledeedName);
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
			imp.Import_Type = "wavecustomersign";
			imp.Import_Status = "I";

			await DB.ImptMstProjTrans.AddAsync(imp);
			await DB.SaveChangesAsync();

			return result;

		}


		public async Task<FileDTO> ExportExcelWaiveCustomerSignAsync(Guid projectID, WaiveCustomerSignFilter filter, WaiveCustomerSignSortByParam sortByParam)
		{
			ExportExcel result = new ExportExcel();
			//IQueryable<WaiveCustomerSignQueryResult> query = DB.WaiveQCs.Where(o => o.ProjectID == projectID)
			//                                                .Include(o => o.Unit.UnitStatus)
			//                                                .Select(o => new WaiveCustomerSignQueryResult
			//                                                {
			//                                                    Unit = o.Unit,
			//                                                    WaiveQC = o
			//                                                });

			IQueryable<WaiveCustomerSignQueryResult> query = (from u in DB.Units.Where(o => o.ProjectID == projectID)

															  join wqc in DB.WaiveQCs.Where(o => o.ProjectID == projectID) on u.ID equals wqc.UnitID into wqcData
															  from wqcModel in wqcData.DefaultIfEmpty()

															  join mas in DB.MasterCenters on u.UnitStatusMasterCenterID equals mas.ID into masData
															  from masModel in masData.DefaultIfEmpty()

															  select new WaiveCustomerSignQueryResult
															  {
																  Unit = u,
																  WaiveQC = wqcModel ?? new WaiveQC(),
																  MasterCenter = masModel ?? new Database.Models.MST.MasterCenter()
															  });

			IQueryable<WaiveCustomerSignQueryResult> queryChkTrf = (from u in DB.Units.Where(o => o.ProjectID == projectID)

																	join arg in DB.Agreements.Where(o => o.IsDeleted == false && o.IsCancel == false)
																	on u.ID equals arg.UnitID

																	join trf in DB.Transfers.Where(o => o.IsDeleted == false)
																	on arg.ID equals trf.AgreementID

																	join wqc in DB.WaiveQCs.Where(o => o.ProjectID == projectID) on u.ID equals wqc.UnitID into wqcData
																	from wqcModel in wqcData.DefaultIfEmpty()

																	join mas in DB.MasterCenters on u.UnitStatusMasterCenterID equals mas.ID into masData
																	from masModel in masData.DefaultIfEmpty()

																	select new WaiveCustomerSignQueryResult
																	{
																		Unit = u,
																		WaiveQC = wqcModel ?? new WaiveQC()
																	});



			#region Filter

			#region ActualTransferDate
			if (filter.ActualTransferDateFrom != null)
			{
				query = query.Where(o => o.WaiveQC.ActualTransferDate >= filter.ActualTransferDateFrom);
			}
			if (filter.ActualTransferDateTo != null)
			{
				query = query.Where(o => o.WaiveQC.ActualTransferDate <= filter.ActualTransferDateTo);
			}
			if (filter.ActualTransferDateFrom != null && filter.ActualTransferDateTo != null)
			{
				query = query.Where(o => o.WaiveQC.ActualTransferDate >= filter.ActualTransferDateFrom
									&& o.WaiveQC.ActualTransferDate <= filter.ActualTransferDateTo);
			}
			#endregion

			#region WaiveSigneDate
			if (filter.WaiveSignDateFrom != null)
			{
				query = query.Where(o => o.WaiveQC.WaiveSignDate >= filter.WaiveSignDateFrom);
			}
			if (filter.WaiveSignDateTo != null)
			{
				query = query.Where(o => o.WaiveQC.WaiveSignDate <= filter.WaiveSignDateTo);
			}
			if (filter.WaiveSignDateFrom != null && filter.WaiveSignDateTo != null)
			{
				query = query.Where(o => o.WaiveQC.WaiveSignDate >= filter.WaiveSignDateFrom
									&& o.WaiveQC.WaiveSignDate <= filter.WaiveSignDateTo);
			}
			#endregion

			if (!string.IsNullOrEmpty(filter.UnitStatusKey))
			{
				var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
																	   && x.MasterCenterGroupKey == "UnitStatus")
																	  .Select(x => x.ID).FirstAsync();
				query = query.Where(o => o.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
			}
			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				query = query.Where(x => x.WaiveQC.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				query = query.Where(x => x.WaiveQC.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				query = query.Where(x => x.WaiveQC.Updated <= filter.UpdatedTo);
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				query = query.Where(x => x.WaiveQC.Updated >= filter.UpdatedFrom && x.WaiveQC.Updated <= filter.UpdatedTo);
			}
			#endregion

			WaiveCustomerSignDTO.SortBy(sortByParam, ref query);

			//Not Data
			//var allUnits = await DB.Units.Where(o => o.ProjectID == projectID).Include(o => o.UnitStatus).Include(o => o.Model).ToListAsync();
			//var tempData = (from unit in allUnits
			//                select new WaiveCustomerSignQueryResult
			//                {
			//                    Unit = unit,
			//                    Model = unit.Model
			//                });

			//var dataT = tempData.Select(o => new WaiveCustomerSignQueryResult
			//{
			//    Unit = o.Unit,
			//    Model = o.Model
			//}).OrderBy(o => o.Unit?.SAPWBSNo).ToList();
			//var queryT = await query.ToListAsync();
			//bool isData = queryT.Count > 1 ? true : false;

			var queryChkTrfResults = await queryChkTrf.Select(o => o.Unit.UnitNo).ToListAsync();
			var data = await query.Where(o => !queryChkTrfResults.Contains(o.Unit.UnitNo)).OrderBy(x => x.Unit.SAPWBSNo).ToListAsync();
			//var data = isData ? query.OrderBy(c => c.Unit.UnitNo).ToList() : dataT.OrderBy(c => c.Unit.UnitNo).ToList();


			string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_WaiveCustomerSign.xlsx");
			byte[] tmp = await File.ReadAllBytesAsync(path);

			using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
			using (ExcelPackage package = new ExcelPackage(stream))
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

				int _projectNoIndex = WaiveCustomerSignExcelModel._projectNoIndex + 1;
				int _wbsNoIndex = WaiveCustomerSignExcelModel._wbsNoIndex + 1;
				int _unitNoIndex = WaiveCustomerSignExcelModel._unitNoIndex + 1;
				int _waiveSigneDateIndex = WaiveCustomerSignExcelModel._waiveSignDateIndex + 1;
				int _unitStatusIndex = WaiveCustomerSignExcelModel._unitStatusIndex + 1;

				var project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
				for (int c = 2; c < data.Count + 2; c++)
				{
					worksheet.Cells[c, _projectNoIndex].Value = project.ProjectNo;
					worksheet.Cells[c, _wbsNoIndex].Value = data[c - 2].Unit?.SAPWBSNo;
					worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Unit?.UnitNo;
					//if (isData)
					//{
					worksheet.Cells[c, _waiveSigneDateIndex].Style.Numberformat.Format = "dd/mm/yyyy";
					worksheet.Cells[c, _waiveSigneDateIndex].Value = data[c - 2].WaiveQC.WaiveSignDate;
					worksheet.Cells[c, _unitStatusIndex].Value = data[c - 2].MasterCenter?.Name;
					//}
				}
				//worksheet.Cells.AutoFitColumns();

				result.FileContent = package.GetAsByteArray();
				result.FileName = project.ProjectNo + "_WaiveCustomerSign.xlsx";
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
	}
}
