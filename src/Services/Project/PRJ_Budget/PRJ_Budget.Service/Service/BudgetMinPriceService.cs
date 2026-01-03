using System.Data;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using ExcelExtensions;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PagingExtensions;
using PRJ_Budget.Params.Filters;
using PRJ_Budget.Services.Excels;
using PRJ_Budget.Params.Outputs;
using ErrorHandling;
using System.ComponentModel;
using System.Reflection;
using Common.Helper.Logging;
using FileStorage;

namespace PRJ_Budget.Services
{
    public class BudgetMinPriceService : IBudgetMinPriceService
    {
        private readonly DatabaseContext DB;
        private FileHelper FileHelper;
        public LogModel logModel { get; set; }

        public BudgetMinPriceService(DatabaseContext db)
        {
            DB = db;
            logModel = new LogModel("BudgetMinPriceService", null);

            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }
        public async Task<BudgetMinPricePaging> GetBudgetMinPriceListAsync(BudgetMinPriceFilter filter, PageParam pageParam, BudgetMinPriceSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            ValidateException ex = new ValidateException();
            BudgetMinPriceDTO MsgDTO = new BudgetMinPriceDTO();
            if (filter.Quarter != null && filter.Quarter > 4)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstAsync();
                string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[message]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (filter.Year != null && filter.Year > 2300)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                //string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                //var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            var result = new BudgetMinPricePaging();
            result = await GetBudgetMinPriceUnitListAsync(filter, pageParam, sortByParam, cancellationToken);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137948/preview
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private async Task<BudgetMinPriceDTO> GetBudgetMinPriceAsync(BudgetMinPriceFilter filter, CancellationToken cancellationToken = default)
        {
            var temp = await DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID && o.Quarter == filter.Quarter && o.Year == filter.Year)
                                                .Include(o => o.BudgetMinPriceType)
                                                .Include(o => o.UpdatedBy)
                                                .Select(o => new BudgetMinPriceQueryResult
                                                {
                                                    BudgetMinPrice = o,
                                                    Project = o.Project,
                                                })
                                                .ToListAsync(cancellationToken);
            if (temp.Count > 0)
            {
                var model = temp.GroupBy(o => new { o.Project, o.BudgetMinPrice.Year, o.BudgetMinPrice.Quarter }).Select(o => new BudgetMinPriceQueryResult
                {
                    Project = o.Key.Project,
                    BudgetMinPrice = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                    BudgetMinPriceQuarterly = o.Where(p => p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.Quarterly).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                    BudgetMinPriceTransfer = o.Where(p => p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.TransferPromotion).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                }).FirstOrDefault();

                var result = BudgetMinPriceDTO.CreateFromQueryResult(model);

                return result;
            }
            else
            {
                var project = await DB.Projects.FirstOrDefaultAsync(o => o.ID == filter.ProjectID.Value, cancellationToken);
                var result = new BudgetMinPriceDTO
                {
                    ID = Guid.NewGuid(),
                    Project = ProjectDropdownDTO.CreateFromModel(project),
                    Quarter = filter.Quarter ?? 0,
                    Year = filter.Year ?? 0,
                    TransferTotalAmount = 0,
                    TransferTotalUnit = 0,
                    QuarterlyTotalAmount = 0
                };

                return result;
            }
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137948/preview
        /// *ดึงข้อมูลโดย query จาก unit ทั้งหมดของโครงการนั้นก่อน (ข้อมูลโครงการอยู่ใน BudgetMinPriceFilter)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private async Task<BudgetMinPricePaging> GetBudgetMinPriceUnitListAsync(BudgetMinPriceFilter filter, PageParam pageParam, BudgetMinPriceSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var BudgetMinPrice = await GetBudgetMinPriceAsync(filter, cancellationToken);
            var budgetMinPriceQuarterlyID = BudgetMinPrice.ID;
            List<string> AssetTypeKeyList = [AssetTypeKeys.Unit, AssetTypeKeys.SampleModelHome];
            // 
            IQueryable<BudgetMinPriceUnitQueryResult> query = from unit in DB.Units.Include(o => o.UnitStatus).Include(x => x.AssetType)
                                                              .Where(o => o.ProjectID == filter.ProjectID && AssetTypeKeyList.Contains(o.AssetType.Key))

                                                              join BudgetMinPriceUnit in DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == budgetMinPriceQuarterlyID)
                                                              on unit.ID equals BudgetMinPriceUnit.UnitID into BudgetMinPriceUnitGroup
                                                              from BudgetMinPriceUnitModel in BudgetMinPriceUnitGroup.DefaultIfEmpty()

                                                              join UpdatedBy in DB.Users
                                                              on BudgetMinPriceUnitModel.UpdatedByUserID equals UpdatedBy.ID into UpdatedByGroup
                                                              from UpdatedByModel in UpdatedByGroup.DefaultIfEmpty()

                                                              select new BudgetMinPriceUnitQueryResult
                                                              {
                                                                  Unit = unit,
                                                                  BudgetMinPriceUnit = BudgetMinPriceUnitModel  ,
                                                                  UpdatedBy = UpdatedByModel  
                                                              };


            #region filter
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => (x.Unit.UnitNo ?? "").ToLower().Contains(filter.UnitNo.ToLower()));
            }
            if (filter.AmonutFrom != null)
            {
                query = query.Where(x => x.BudgetMinPriceUnit.Amount >= filter.AmonutFrom);
            }
            if (filter.AmonutTo != null)
            {
                query = query.Where(x => x.BudgetMinPriceUnit.Amount <= filter.AmonutTo);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedByFilter))
            {
                query = query.Where(x => (x.UpdatedBy.DisplayName ?? "").ToLower().Contains(filter.UpdatedByFilter.ToLower()));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.BudgetMinPriceUnit.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.BudgetMinPriceUnit.Updated <= filter.UpdatedTo);
            }
            if (filter.UnitStatus != null)
            {
                //query = query.Where(x => x.Unit.UnitStatus.ID == filter.UnitStatus);
                var UnitStatusMaster = await DB.MasterCenters.Where(x => x.MasterCenterGroupKey.Equals(MasterCenterGroupKeys.UnitStatus)).ToListAsync();
                List<Guid> UnitStatusId = new List<Guid>();
                if (filter.UnitStatus == 1)
                {
                    UnitStatusId = UnitStatusMaster.Where(x => x.Key.Equals(UnitStatusKeys.WaitingForConfirmBooking) || x.Key.Equals(UnitStatusKeys.Available)).Select(x => x.ID).ToList();

                }
                else if (filter.UnitStatus == 2)
                {
                    UnitStatusId = UnitStatusMaster.Where(x => x.Key.Equals(UnitStatusKeys.WaitingForAgreement)).Select(x => x.ID).ToList();
                }
                else if (filter.UnitStatus == 3)
                {
                    UnitStatusId = UnitStatusMaster.Where(x => x.Key.Equals(UnitStatusKeys.WaitingForTransfer)).Select(x => x.ID).ToList();
                }
                else if (filter.UnitStatus == 4)
                {
                    UnitStatusId = UnitStatusMaster.Where(x => x.Key.Equals(UnitStatusKeys.Transfer)).Select(x => x.ID).ToList();
                }

                query = query.Where(x => UnitStatusId.Contains(x.Unit.UnitStatus.ID));
            }
            #endregion
            BudgetMinPriceUnitDTO.SortBy(sortByParam, ref query);
            var pageOuput = PagingHelper.Paging<BudgetMinPriceUnitQueryResult>(pageParam, ref query);


            var queryResults = await query.ToListAsync(cancellationToken);
            var results = queryResults.Select(o => BudgetMinPriceUnitDTO.CreateFromQueryResult(o)).ToList();

            var isCheckQuater = !BudgetMinPriceTransferExcelModel.CheckOldQuater(filter.Year, filter.Quarter);
            results.ForEach(x => x.isCanEdit = isCheckQuater);
            var resultBudgetMinPricec = new BudgetMinPricePaging
            {
                BudgetMinPriceListDTO = new BudgetMinPriceListDTO
                {
                    BudgetMinPriceUnitDTO = results,
                    BudgetMinPriceDTO = new BudgetMinPriceDTO()
                },
                PageOutput = pageOuput
            };
            resultBudgetMinPricec.BudgetMinPriceListDTO.BudgetMinPriceDTO = BudgetMinPrice;

            return resultBudgetMinPricec;
        }


        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137948/preview
        /// สร้างหรือแก้ไข (โดยเช็คจาก project id, quarter, year)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BudgetMinPriceDTO> SaveBudgetMinPriceAsync(BudgetMinPriceFilter filter, BudgetMinPriceDTO input)
        {
            var project = await DB.Projects.Where(o => o.ID == filter.ProjectID.Value).FirstAsync();
            var masterCenterQuarterlyID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly)).ID;
            var masterCenterTransferID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.TransferPromotion)).ID;
            var temp = await DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID && o.Quarter == filter.Quarter && o.Year == filter.Year)
                                                .Include(o => o.BudgetMinPriceType)
                                                .Select(o => new BudgetMinPriceQueryResult
                                                {
                                                    BudgetMinPrice = o,
                                                    Project = o.Project,
                                                })
                                                .ToListAsync();
            var model = temp.GroupBy(o => o.Project).Select(o => new BudgetMinPriceQueryResult
            {
                Project = o.Key,
                BudgetMinPrice = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceQuarterly = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.Quarterly).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceTransfer = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.TransferPromotion).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
            }).FirstOrDefault();

            if (model.BudgetMinPriceQuarterly.TotalAmount != input.QuarterlyTotalAmount)
            {
                var quarterly = new BudgetMinPrice();
                input.ToModelQuarterly(ref quarterly);
                quarterly.BudgetMinPriceTypeMasterCenterID = masterCenterQuarterlyID;
                quarterly.UsedAmount = await GetOldUsedAmount(model.BudgetMinPriceQuarterly.ID);
                await DB.BudgetMinPrices.AddAsync(quarterly);

                var allunitinquarterly = await DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == model.BudgetMinPriceQuarterly.ID).ToListAsync();
                var newUnitQuarterly = new List<BudgetMinPriceUnit>();

                foreach (var item in allunitinquarterly)
                {
                    var newUnitModel = new BudgetMinPriceUnit();
                    newUnitModel.BudgetMinPriceID = quarterly.ID;
                    newUnitModel.UnitID = item.UnitID;
                    newUnitModel.Amount = item.Amount;
                    newUnitQuarterly.Add(newUnitModel);
                }

                await DB.BudgetMinPriceUnits.AddRangeAsync(newUnitQuarterly);
            }
            if (model.BudgetMinPriceTransfer?.TotalAmount != input.TransferTotalAmount || model.BudgetMinPriceTransfer?.UnitAmount != input.TransferTotalUnit)
            {
                var transfer = new BudgetMinPrice();
                input.ToModelTransfer(ref transfer);
                transfer.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;
                await DB.BudgetMinPrices.AddAsync(transfer);
            }
            await DB.SaveChangesAsync();
            return new BudgetMinPriceDTO();
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137948/preview
        /// ถ้าไม่เคยมี budget min price unit ให้สร้างใหม่
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public async Task SaveBudgetMinPriceUnitListAsync(BudgetMinPriceListDTO inputs, Guid userID, bool SaveTranfer)
        {

            ValidateException ex = new ValidateException();
            BudgetMinPriceDTO MsgDTO = new BudgetMinPriceDTO();
            if (inputs.BudgetMinPriceDTO.Quarter != null && inputs.BudgetMinPriceDTO.Quarter > 4)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstAsync();
                string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[message]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (inputs.BudgetMinPriceDTO.Year != null && inputs.BudgetMinPriceDTO.Year > 2300)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            var budgetMinPriceQuarterly = await DB.BudgetMinPrices.Include(o => o.BudgetMinPriceType).Where(x => x.ID == inputs.BudgetMinPriceDTO.ID).FirstOrDefaultAsync() ?? null;
            var temp = await DB.BudgetMinPrices.Where(o => o.ProjectID == inputs.BudgetMinPriceDTO.Project.Id && o.Quarter == inputs.BudgetMinPriceDTO.Quarter && o.Year == inputs.BudgetMinPriceDTO.Year)
                                               .Include(o => o.BudgetMinPriceType)
                                               .Include(o => o.UpdatedBy)
                                               .Select(o => new BudgetMinPriceQueryResult
                                               {
                                                   BudgetMinPrice = o,
                                                   Project = o.Project,
                                               })
                                               .ToListAsync();


            var masterCenterQuarterlyID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstOrDefaultAsync();
            var masterCenterTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.TransferPromotion).Select(o => o.ID).FirstOrDefaultAsync();

            if (temp.Count > 0)
            {
                var BudgetMinPriceQuarterly = temp.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.Quarterly).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).FirstOrDefault();
                var BudgetMinPriceTransfer = temp.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.TransferPromotion).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).FirstOrDefault();

                var quarterly = new BudgetMinPrice();
                inputs.BudgetMinPriceDTO.ToModelQuarterly(ref quarterly);
                quarterly.BudgetMinPriceTypeMasterCenterID = masterCenterQuarterlyID;

                var transfer = new BudgetMinPrice();
                inputs.BudgetMinPriceDTO.ToModelTransfer(ref transfer);
                transfer.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;

                if (BudgetMinPriceTransfer != null || transfer.TotalAmount == BudgetMinPriceTransfer?.BudgetMinPrice?.TotalAmount || transfer.UnitAmount == BudgetMinPriceTransfer?.BudgetMinPrice?.UnitAmount)
                {
                    transfer.Updated = BudgetMinPriceTransfer?.BudgetMinPrice?.Updated;
                    transfer.UpdatedByUserID = BudgetMinPriceTransfer?.BudgetMinPrice?.UpdatedByUserID;
                }
                if (BudgetMinPriceQuarterly != null || quarterly.TotalAmount == BudgetMinPriceQuarterly?.BudgetMinPrice?.TotalAmount)
                {
                    quarterly.Updated = BudgetMinPriceQuarterly?.BudgetMinPrice?.Updated;
                    quarterly.UpdatedByUserID = BudgetMinPriceQuarterly?.BudgetMinPrice?.UpdatedByUserID;

                }
                if (SaveTranfer)
                {
                    await DB.BudgetMinPrices.AddAsync(transfer);
                }
                await DB.BudgetMinPrices.AddAsync(quarterly);
                await DB.SaveChangesAsync();

                Guid newID = quarterly.ID;
                IQueryable<BudgetMinPriceUnitQueryResult> query = null;
                List<string> AssetTypeKeyList = new List<string>();
                AssetTypeKeyList.Add(AssetTypeKeys.Unit);
                AssetTypeKeyList.Add(AssetTypeKeys.SampleModelHome);
                if ((BudgetMinPriceQuarterly?.BudgetMinPrice?.ID ?? null) != null)
                {

                    query = from unit in DB.Units.Include(o => o.UnitStatus).Include(x => x.AssetType)
                            .Where(o => o.ProjectID == inputs.BudgetMinPriceDTO.Project.Id && AssetTypeKeyList.Contains(o.AssetType.Key))

                            join BudgetMinPriceUnit in DB.BudgetMinPriceUnits.Include(o => o.UpdatedBy).Where(o => o.BudgetMinPriceID == BudgetMinPriceQuarterly.BudgetMinPrice.ID)
                            on unit.ID equals BudgetMinPriceUnit.UnitID into BudgetMinPriceUnitGroup
                            from BudgetMinPriceUnitModel in BudgetMinPriceUnitGroup.DefaultIfEmpty()


                            join UpdatedBy in DB.Users
                            on BudgetMinPriceUnitModel.UpdatedByUserID equals UpdatedBy.ID into UpdatedByGroup
                            from UpdatedByModel in UpdatedByGroup.DefaultIfEmpty()

                            select new BudgetMinPriceUnitQueryResult
                            {
                                Unit = unit,
                                BudgetMinPriceUnit = BudgetMinPriceUnitModel ?? new BudgetMinPriceUnit(),
                                UpdatedBy = UpdatedByModel ?? new Database.Models.USR.User()
                            };
                }
                else
                {
                    query = from unit in DB.Units.Include(o => o.UnitStatus).Include(x => x.AssetType)
                            .Where(o => o.ProjectID == inputs.BudgetMinPriceDTO.Project.Id && AssetTypeKeyList.Contains(o.AssetType.Key))
                            select new BudgetMinPriceUnitQueryResult
                            {
                                Unit = unit,
                                BudgetMinPriceUnit = new BudgetMinPriceUnit(),
                                UpdatedBy = new Database.Models.USR.User()
                            };
                }


                var queryToList = await query.ToListAsync();
                var createBudgetMinPriceUnits = new List<BudgetMinPriceUnit>();

                foreach (var item in queryToList)
                {
                    var model = inputs.BudgetMinPriceUnitDTO.Where(o => o.Unit.Id == item.Unit.ID).FirstOrDefault();
                    if (model == null)
                    {
                        var newModel = new BudgetMinPriceUnit();
                        var BudgetMinPriceUnit = new BudgetMinPriceUnitDTO()
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(item.Unit),
                            UnitAmount = item.BudgetMinPriceUnit?.Amount ?? 0.000m,
                        };
                        BudgetMinPriceUnit.ToModel(ref newModel);
                        newModel.BudgetMinPriceID = newID;
                        if (item.BudgetMinPriceUnit?.UpdatedByUserID != null)
                        {
                            newModel.Created = item.BudgetMinPriceUnit.Created;
                            newModel.CreatedByUserID = item.BudgetMinPriceUnit.CreatedByUserID;
                            newModel.UpdatedByUserID = item.BudgetMinPriceUnit.UpdatedByUserID;
                            newModel.Updated = item.BudgetMinPriceUnit.Updated;
                        }

                        createBudgetMinPriceUnits.Add(newModel);
                    }
                    else
                    {
                        var newModel = new BudgetMinPriceUnit();
                        model.ToModel(ref newModel);
                        newModel.BudgetMinPriceID = newID;
                        newModel.Created = DateTime.Now;
                        newModel.CreatedByUserID = userID;
                        newModel.UpdatedByUserID = userID;
                        newModel.Updated = DateTime.Now;
                        createBudgetMinPriceUnits.Add(newModel);
                    }
                }
                await DB.BudgetMinPriceUnits.AddRangeAsync(createBudgetMinPriceUnits);
                await DB.SaveChangesNoUpdateUserAsync();
            }
            else
            {

                var quarterly = new BudgetMinPrice();
                inputs.BudgetMinPriceDTO.ToModelQuarterly(ref quarterly);
                quarterly.BudgetMinPriceTypeMasterCenterID = masterCenterQuarterlyID;

                //var transfer = new BudgetMinPrice();
                //inputs.BudgetMinPriceDTO.ToModelTransfer(ref transfer);
                //transfer.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;


                await DB.BudgetMinPrices.AddAsync(quarterly);
                //await DB.BudgetMinPrices.AddAsync(transfer);
                await DB.SaveChangesAsync();
                Guid newID = quarterly.ID;
                List<string> AssetTypeKeyList = new List<string>();
                AssetTypeKeyList.Add(AssetTypeKeys.Unit);
                AssetTypeKeyList.Add(AssetTypeKeys.SampleModelHome);

                IQueryable<BudgetMinPriceUnitQueryResult> query = from unit in DB.Units.Include(o => o.UnitStatus).Include(x => x.AssetType)
                                                                  .Where(o => o.ProjectID == inputs.BudgetMinPriceDTO.Project.Id
                                                                  && AssetTypeKeyList.Contains(o.AssetType.Key))
                                                                  select new BudgetMinPriceUnitQueryResult
                                                                  {
                                                                      Unit = unit,
                                                                      BudgetMinPriceUnit = new BudgetMinPriceUnit()
                                                                  };
                var queryToList = await query.ToListAsync();
                var createBudgetMinPriceUnits = new List<BudgetMinPriceUnit>();

                foreach (var item in queryToList)
                {
                    var model = inputs.BudgetMinPriceUnitDTO.Where(o => o.Unit.Id == item.Unit.ID).FirstOrDefault();
                    if (model == null)
                    {
                        var newModel = new BudgetMinPriceUnit();
                        //var BudgetMinPriceUnit = BudgetMinPriceUnitDTO.CreateFromModel(item.BudgetMinPriceUnit);
                        var BudgetMinPriceUnit = new BudgetMinPriceUnitDTO()
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(item.Unit),
                            UnitAmount = item.BudgetMinPriceUnit?.Amount ?? 0.000m,
                        };
                        BudgetMinPriceUnit.ToModel(ref newModel);
                        newModel.BudgetMinPriceID = newID;
                        createBudgetMinPriceUnits.Add(newModel);
                    }
                    else
                    {
                        var newModel = new BudgetMinPriceUnit();
                        model.ToModel(ref newModel);
                        newModel.BudgetMinPriceID = newID;
                        createBudgetMinPriceUnits.Add(newModel);
                    }
                }
                await DB.BudgetMinPriceUnits.AddRangeAsync(createBudgetMinPriceUnits);
                await DB.SaveChangesAsync();
            }
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137948/preview
        /// ถ้าไม่เคยมี budget min price unit ให้สร้างใหม่
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BudgetMinPriceUnitDTO> SaveBudgetMinPriceUnitAsync(BudgetMinPriceFilter filter, BudgetMinPriceUnitDTO input)
        {
            var budgetMinPriceQuarterlyID = await DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID
                                                           && o.Quarter == filter.Quarter
                                                           && o.Year == filter.Year
                                                           && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly
                                                           && o.ActiveDate <= DateTime.Now
                                          ).OrderByDescending(o => o.ActiveDate).Select(o => o.ID).FirstAsync();
            var budgetMinPriceUnits = await DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == budgetMinPriceQuarterlyID).ToListAsync();
            var model = budgetMinPriceUnits.Where(o => o.UnitID == input.Unit.Id).FirstOrDefault();
            var result = new BudgetMinPriceUnitDTO();
            if (model == null)
            {
                var newModel = new BudgetMinPriceUnit();
                input.ToModel(ref newModel);
                newModel.BudgetMinPriceID = budgetMinPriceQuarterlyID;
                await DB.BudgetMinPriceUnits.AddAsync(newModel);
                result = BudgetMinPriceUnitDTO.CreateFromModel(newModel);
            }
            else
            {
                input.ToModel(ref model);
                model.BudgetMinPriceID = budgetMinPriceQuarterlyID;
                DB.BudgetMinPriceUnits.Update(model);
                result = BudgetMinPriceUnitDTO.CreateFromModel(model);
            }
            await DB.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137949/preview
        /// อัพไฟล์ แล้ว return dto (ยังไม่ต้อง save ลง db) ไม่ต้องทำ paging
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BudgetMinPriceQuarterlyDTO> ImportQuarterlyBudgetAsync(FileDTO input, bool notChkAmountZero)
        {
            var dt = await ConvertExcelToDataTableQuarterly(input);
            /// Valudate Header
            //if (dt.Columns.Count != 3)
            //{
            //    //throw new Exception("Invalid File Format");
            //    ValidateException ex = new ValidateException();
            //    var errMsgFile = await DB.ErrorMessages.Where(o => o.Key == "ERR0147").FirstAsync();
            //    ex.AddError(errMsgFile.Key, errMsgFile.Message, (int)errMsgFile.Type);
            //    throw ex;
            //}
            //Read Excel Model
            var budgetMinPriceQuarterlyExcelModels = new List<BudgetMinPriceQuarterlyExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var excelModel = BudgetMinPriceQuarterlyExcelModel.CreateFromDataRow(r, DB);
                budgetMinPriceQuarterlyExcelModels.Add(excelModel);
            }
            var budgetMinPriceQuarterly = new BudgetMinPriceQuarterlyDTO { BudgetMinPrice = new BudgetMinPriceDTO(), Units = new List<BudgetMinPriceUnitDTO>() };
            var header = await GetHeaderQuarterly(input);
            var project = await DB.Projects.Where(o => o.ProjectNo == header.ProjectNo).FirstOrDefaultAsync();

            var budgetMinQuarterly = DB.BudgetMinPrices.Where(o => o.ProjectID == project.ID
                                               && o.Quarter == header.Quarter
                                               && o.Year == header.Year
                                               && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly
                               ).OrderByDescending(o => o.ActiveDate).FirstOrDefault();

            budgetMinPriceQuarterly.BudgetMinPrice.Project = ProjectDropdownDTO.CreateFromModel(project);
            budgetMinPriceQuarterly.BudgetMinPrice.QuarterlyTotalAmount = header.QuarterlyTotalAmount;
            budgetMinPriceQuarterly.BudgetMinPrice.Quarter = header.Quarter;
            budgetMinPriceQuarterly.BudgetMinPrice.Year = header.Year;

            List<string> AssetTypeKeyList = new List<string>();
            AssetTypeKeyList.Add(AssetTypeKeys.Unit);
            AssetTypeKeyList.Add(AssetTypeKeys.SampleModelHome);
            var allUnit = await DB.Units.Include(o => o.UnitStatus).Include(x => x.AssetType)
                .Where(o => o.ProjectID == project.ID && AssetTypeKeyList.Contains(o.AssetType.Key)).ToListAsync();
            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
            var errMsgTransfer = await DB.ErrorMessages.Where(o => o.Key == "ERR0133").FirstAsync();


            if (budgetMinQuarterly != null)
            {
                var budgetMinPriceUnits = await DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == budgetMinQuarterly.ID).ToListAsync();



                var query = (from unit in allUnit
                             join budgetminpriceunit in budgetMinPriceUnits.DefaultIfEmpty()
                             on unit.ID equals budgetminpriceunit.UnitID into ps
                             select new BudgetMinPriceUnitQueryResult
                             {
                                 Unit = unit,
                                 BudgetMinPriceUnit = ps.FirstOrDefault()
                             }
                            );
                var queryResults = query.OrderBy(o => o.Unit.UnitNo).ToList();
                int CountItem = 0;
                foreach (var item in budgetMinPriceQuarterlyExcelModels)
                {
                    var unit = allUnit.Where(o => o.UnitNo == item.UnitNo).FirstOrDefault();
                    var oldBudgetMinPriceUnit = queryResults.Where(o => o.Unit.UnitNo == item.UnitNo).FirstOrDefault();
                    BudgetMinPriceUnitDTO budgetMinPriceUnit = new BudgetMinPriceUnitDTO();
                    //budgetMinPriceUnit.Unit = new UnitDropdownDTO { UnitNo = unit.UnitNo};
                    budgetMinPriceUnit.Unit = UnitDropdownDTO.CreateFromModel(unit);
                    budgetMinPriceUnit.Unit.UnitStatus.Name = getUnitStatus(budgetMinPriceUnit.Unit.UnitStatus.Key);
                    budgetMinPriceUnit.UnitAmount = Convert.ToDecimal(item.BudgetAmount) == 0 ? 0 : Convert.ToDecimal(item.BudgetAmount);
                    budgetMinPriceUnit.OldUnitAmount = Convert.ToDecimal(oldBudgetMinPriceUnit?.BudgetMinPriceUnit?.Amount) == 0 ? 0 : Convert.ToDecimal(oldBudgetMinPriceUnit?.BudgetMinPriceUnit?.Amount);
                    budgetMinPriceUnit.UpdatedByUserID = oldBudgetMinPriceUnit?.BudgetMinPriceUnit?.UpdatedByUserID;
                    budgetMinPriceUnit.isCorrected = true;
                    if (!item.isCorrected)
                    {
                        budgetMinPriceUnit.isCorrected = false;
                        budgetMinPriceUnit.Remark = item.Remark;
                    }

                    if (unit == null)
                    {
                        budgetMinPriceUnit.isCorrected = false;
                        var msg = errMsg.Message.Replace("[field]", item.UnitNo);
                        budgetMinPriceUnit.Remark = msg;
                    }
                    else
                    {
                        var unitList = budgetMinPriceQuarterlyExcelModels.Where(x => x.UnitNo == item.UnitNo).Count();
                        if (unitList > 1)
                        {
                            budgetMinPriceUnit.isCorrected = false;
                            budgetMinPriceUnit.Remark = item.UnitNo + " มากกว่า 1 rows";
                        }
                        if (budgetMinPriceUnit.Unit.UnitStatus.Key == UnitStatusKeys.Transfer)
                        {
                            budgetMinPriceUnit.isCorrected = false;
                            var msg = errMsgTransfer.Message.Replace("[field]", budgetMinPriceUnit.Unit.UnitStatus.Name);
                            budgetMinPriceUnit.Remark = msg;
                        }
                        if (!notChkAmountZero)
                        {
                            if (budgetMinPriceUnit.UnitAmount == 0)
                                CountItem++;
                        }
                    }
                    budgetMinPriceQuarterly.Units.Add(budgetMinPriceUnit);

                }
                if (CountItem > 0)
                {
                    ValidateException exerrMsgChkAmountZero = new ValidateException();
                    var errMsgexerrMsgChkAmountZero = DB.ErrorMessages.Where(o => o.Key == "ERR0134").FirstOrDefault();
                    var MsgChkAmountZero = errMsgexerrMsgChkAmountZero.Message.Replace("[field]", CountItem.ToString());
                    budgetMinPriceQuarterly.ChkError = new ChkErrorDTO();
                    budgetMinPriceQuarterly.ChkError.MsgPopUP = true;
                    budgetMinPriceQuarterly.ChkError.Msg = MsgChkAmountZero;
                    //throw exerrMsgChkAmountZero;
                }
                else
                {
                    budgetMinPriceQuarterly.ChkError = new ChkErrorDTO();
                    budgetMinPriceQuarterly.ChkError.MsgPopUP = false;
                }

                budgetMinPriceQuarterly.BudgetMinPrice.TotalSuccess = budgetMinPriceQuarterly.Units.Where(x => x.isCorrected == true).Count();
                budgetMinPriceQuarterly.BudgetMinPrice.TotalError = budgetMinPriceQuarterly.Units.Where(x => x.isCorrected == false).Count();
            }
            else
            {

                int CountItem = 0;
                foreach (var item in budgetMinPriceQuarterlyExcelModels)
                {
                    var unit = allUnit.Where(o => o.UnitNo == item.UnitNo).FirstOrDefault();
                    BudgetMinPriceUnitDTO budgetMinPriceUnit = new BudgetMinPriceUnitDTO();
                    //budgetMinPriceUnit.Unit = new UnitDropdownDTO { UnitNo = item.UnitNo };
                    budgetMinPriceUnit.Unit = UnitDropdownDTO.CreateFromModel(unit);
                    budgetMinPriceUnit.UnitAmount = Convert.ToDecimal(item.BudgetAmount) == 0 ? 0 : Convert.ToDecimal(item.BudgetAmount);
                    budgetMinPriceUnit.OldUnitAmount = 0;

                    budgetMinPriceUnit.isCorrected = true;

                    if (!item.isCorrected)
                    {
                        budgetMinPriceUnit.isCorrected = false;
                        budgetMinPriceUnit.Remark = item.Remark;
                    }
                    if (unit == null)
                    {
                        budgetMinPriceUnit.isCorrected = false;
                        var msg = errMsg.Message.Replace("[field]", item.UnitNo);
                        budgetMinPriceUnit.Remark = msg;

                    }
                    else
                    {
                        if (budgetMinPriceUnit.Unit.UnitStatus.Key == UnitStatusKeys.Transfer)
                        {
                            budgetMinPriceUnit.isCorrected = false;
                            var msg = errMsgTransfer.Message.Replace("[field]", budgetMinPriceUnit.Unit.UnitStatus.Name);
                            budgetMinPriceUnit.Remark = msg;

                        }
                        if (!notChkAmountZero)
                        {
                            if (budgetMinPriceUnit.UnitAmount == 0)
                                CountItem++;
                        }
                    }

                    budgetMinPriceQuarterly.Units.Add(budgetMinPriceUnit);
                }
                budgetMinPriceQuarterly.BudgetMinPrice.TotalSuccess = budgetMinPriceQuarterly.Units.Where(x => x.isCorrected == true).Count();
                budgetMinPriceQuarterly.BudgetMinPrice.TotalError = budgetMinPriceQuarterly.Units.Where(x => x.isCorrected == false).Count();

                if (CountItem > 0)
                {
                    ValidateException exerrMsgChkAmountZero = new ValidateException();
                    var errMsgexerrMsgChkAmountZero = DB.ErrorMessages.Where(o => o.Key == "ERR0134").FirstOrDefault();
                    var MsgChkAmountZero = errMsgexerrMsgChkAmountZero.Message.Replace("[field]", CountItem.ToString());
                    budgetMinPriceQuarterly.ChkError = new ChkErrorDTO();
                    budgetMinPriceQuarterly.ChkError.MsgPopUP = true;
                    budgetMinPriceQuarterly.ChkError.Msg = MsgChkAmountZero;
                    //throw exerrMsgChkAmountZero;
                }
                else
                {
                    budgetMinPriceQuarterly.ChkError = new ChkErrorDTO();
                    budgetMinPriceQuarterly.ChkError.MsgPopUP = false;
                }
            }
            return budgetMinPriceQuarterly;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137949/preview
        /// ส่ง List dto เพื่อ save เข้า db
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ConfirmImportQuarterlyBudgetAsync(BudgetMinPriceQuarterlyDTO input, Guid userID)
        {
            if (input.BudgetMinPrice != null || input.Units != null)
            {
                //var chkCorrected = input.Units.Where(x => x.isCorrected == false).ToList();
                //if (chkCorrected.Count == 0)
                //{
                input.Units = input.Units.Where(x => x.isCorrected == true).ToList();
                var newInput = new BudgetMinPriceListDTO
                {
                    BudgetMinPriceDTO = input.BudgetMinPrice,
                    BudgetMinPriceUnitDTO = input.Units
                };
                await SaveBudgetMinPriceUnitListAsync(newInput, userID, false);
                //} 
            }
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137950/preview
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<FileDTO> ExportQuarterlyBudgetAsync(BudgetMinPriceFilter filter, CancellationToken cancellationToken = default)
        {
            ExportExcel result = new ExportExcel();
            var budgetMinPriceQuarterly = await DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID
                                                            && o.Quarter == filter.Quarter
                                                            && o.Year == filter.Year
                                                            && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly
                                           //&& o.ActiveDate <= DateTime.Now
                                           ).OrderByDescending(o => o.ActiveDate).FirstOrDefaultAsync(cancellationToken);

            List<BudgetMinPriceUnit> budgetMinPriceUnits = new List<BudgetMinPriceUnit>();
            if (budgetMinPriceQuarterly != null)
            {
                budgetMinPriceUnits = await DB.BudgetMinPriceUnits.Include(o => o.UpdatedBy).Where(o => o.BudgetMinPriceID == budgetMinPriceQuarterly.ID).ToListAsync(cancellationToken) ?? new List<BudgetMinPriceUnit>();
            }
            List<string> AssetTypeKeyList = [AssetTypeKeys.Unit, AssetTypeKeys.SampleModelHome];
            var allUnit = await DB.Units.Include(o => o.UnitStatus)
                .Where(o => o.ProjectID == filter.ProjectID
                && AssetTypeKeyList.Contains(o.AssetType.Key)
                ).ToListAsync(cancellationToken);

            if (!(filter.UnitTransfer ?? false))
                allUnit = allUnit.Where(x => x.UnitStatus.Key != UnitStatusKeys.Transfer).ToList();

            var query = from unit in allUnit
                        join budgetminpriceunit in budgetMinPriceUnits
                        on unit.ID equals budgetminpriceunit.UnitID into ps
                        select new BudgetMinPriceUnitQueryResult
                        {
                            Unit = unit,
                            BudgetMinPriceUnit = ps.FirstOrDefault()
                        }
                        ;

            var project = await DB.Projects.Where(o => o.ID == filter.ProjectID).FirstAsync(cancellationToken);
            var results = query.OrderBy(o => o.Unit.UnitNo).ToList();
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "QuarterlySaleBudget.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _unitNoIndex = BudgetMinPriceQuarterlyExcelModel._unitNoIndex + 1;
                int _budgetAmountIndex = BudgetMinPriceQuarterlyExcelModel._budgetAmountIndex + 1;
                int _unitStatusIndex = BudgetMinPriceQuarterlyExcelModel._unitStatusIndex + 1;

                worksheet.Cells[1, 2].Value = project?.ProjectNo;
                worksheet.Cells[1, 3].Value = project?.ProjectNameTH;
                worksheet.Cells[2, 2].Value = budgetMinPriceQuarterly?.TotalAmount;
                worksheet.Cells[3, 2].Value = filter.Year.ToString();
                worksheet.Cells[4, 2].Value = filter.Quarter.ToString();

                for (int c = 7; c < results.Count + 7; c++)
                {
                    worksheet.Cells[c, _unitNoIndex].Value = results[c - 7].Unit?.UnitNo;
                    worksheet.Cells[c, _budgetAmountIndex].Value = results[c - 7].BudgetMinPriceUnit?.Amount ?? 0;
                    worksheet.Cells[c, _unitStatusIndex].Value = getUnitStatus(results[c - 7].Unit?.UnitStatus?.Key);
                }

                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = "QuarterlySaleBudget_" + project.ProjectNo + "_" + filter.Year + "_" + filter.Quarter + ".xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{result.FileName}_{Guid.NewGuid()}";
            string contentType = result.FileType;
            string filePath = $"budget-minprices/";

            var uploadResult = await FileHelper.UploadFileFromStreamWithOutGuid(fileStream, Environment.GetEnvironmentVariable("minio_DefaultBucket"), filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137951/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BudgetMinPriceTransferDTO> ImportTransferBudgetAsync(FileDTO input, bool notChkAmountZero)
        {
            var dt = await ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 8)
            {
                //throw new Exception("Invalid File Format");
                ValidateException ex = new ValidateException();
                var errMsgFile = await DB.ErrorMessages.Where(o => o.Key == "ERR0147").FirstAsync();
                ex.AddError(errMsgFile.Key, errMsgFile.Message, (int)errMsgFile.Type);
                throw ex;
            }
            //Read Excel Model
            var budgetMinPriceTransferExcelModels = new List<BudgetMinPriceTransferExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var excelModel = BudgetMinPriceTransferExcelModel.CreateFromDataRow(r, DB);
                budgetMinPriceTransferExcelModels.Add(excelModel);
            }


            var queryList = budgetMinPriceTransferExcelModels.GroupBy(t => new
            {
                t.ProjectNo,
                t.Quarter,
                t.Year
            }).Select(t => new
            {
                ProjectNo = t.Key.ProjectNo,
                count = budgetMinPriceTransferExcelModels.Where(x => x.ProjectNo == t.Key.ProjectNo && x.Quarter == t.Key.Quarter && x.Year == t.Key.Year).Count()
            }).ToList();

            var chkCount = queryList.Where(x => x.count > 1).Select(x => x.ProjectNo).ToList();
            if (chkCount.Count > 0)
            {
                ValidateException exProjectNo = new ValidateException();
                var errMsgProjectNo = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                var msg = errMsgProjectNo.Message.Replace("[field]", "ProjectNo : " + string.Join(",", chkCount.ToList()));
                exProjectNo.AddError(errMsgProjectNo.Key, msg, (int)errMsgProjectNo.Type);
                throw exProjectNo;
            }

            var listProjectNo = budgetMinPriceTransferExcelModels.Select(s => s.ProjectNo);
            var budgetMinPriceTransfers = new BudgetMinPriceTransferDTO { BudgetMinPrices = new List<BudgetMinPriceDTO>(), TotalAmount = 0 };
            var allProject = await DB.Projects.Where(x => listProjectNo.Contains(x.ProjectNo)).ToListAsync();
            var temp = await DB.BudgetMinPrices.Where(x => listProjectNo.Contains(x.Project.ProjectNo))
                        .Include(o => o.BudgetMinPriceType)
                        .Include(o => o.Project)
                        .Select(o => new BudgetMinPriceQueryResult
                        {
                            BudgetMinPrice = o,
                            Project = o.Project,
                        })
                        .ToListAsync();
            var queryResults = temp.GroupBy(o => new { o.Project, o.BudgetMinPrice.Year, o.BudgetMinPrice.Quarter }).Select(o => new BudgetMinPriceQueryResult
            {
                Project = o.Key.Project,
                BudgetMinPrice = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceQuarterly = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.Quarterly).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceTransfer = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.TransferPromotion).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
            }).OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.BudgetMinPrice.Year).ThenBy(o => o.BudgetMinPrice.Quarter).ToList();
            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
            int CountItemAmount = 0;
            int CountItemUnit = 0;

            foreach (var item in budgetMinPriceTransferExcelModels)
            {
                BudgetMinPriceDTO budgetMinPrice = new BudgetMinPriceDTO();
                var project = allProject.Find(o => o.ProjectNo == item.ProjectNo);
                budgetMinPrice.isCorrected = true;
                if (!item.isCorrected)
                {
                    budgetMinPrice.isCorrected = false;
                    budgetMinPrice.Remark = item.Remark;
                }
                if (project != null)
                {
                    var oldbudgetminprice = queryResults.Find(o => o.BudgetMinPrice.Year == item.Year && o.BudgetMinPrice.Quarter == item.Quarter && o.BudgetMinPrice.ProjectID == project.ID);
                    budgetMinPrice.OldTransferTotalAmount = oldbudgetminprice?.BudgetMinPriceTransfer?.TotalAmount ?? 0;
                    budgetMinPrice.OldTransferTotalUnit = oldbudgetminprice?.BudgetMinPriceTransfer?.UnitAmount ?? 0;
                }
                else
                {
                    budgetMinPrice.OldTransferTotalAmount = 0;
                    budgetMinPrice.OldTransferTotalUnit = 0;
                }
                budgetMinPrice.Project = ProjectDropdownDTO.CreateFromModel(project);
                budgetMinPrice.Year = item.Year;
                budgetMinPrice.Quarter = item.Quarter;
                budgetMinPrice.TransferTotalAmount = Convert.ToDecimal(item.TransferTotalAmount) == 0 ? 0 : Convert.ToDecimal(item.TransferTotalAmount);
                budgetMinPrice.TransferTotalUnit = Convert.ToDecimal(item.TransferTotalUnit) == 0 ? 0 : Convert.ToDecimal(item.TransferTotalUnit);

                if (budgetMinPrice.isCorrected == true && (project == null
                   || budgetMinPrice.Year == 0
                   || budgetMinPrice.Quarter == 0
                   || budgetMinPrice.TransferTotalUnit < budgetMinPrice.TransferTotalUnit)
                   )
                {
                    budgetMinPrice.isCorrected = false;
                    var msg = errMsg.Message.Replace("[field]", item.ProjectNo + "-" + item.ProjectName);
                    budgetMinPrice.Remark = msg;
                }
                else
                {

                    if (!notChkAmountZero)
                    {
                        if (budgetMinPrice.TransferTotalAmount == 0)
                            CountItemAmount++;

                        if (budgetMinPrice.TransferTotalUnit == 0)
                            CountItemUnit++;
                    }
                }

                budgetMinPriceTransfers.BudgetMinPrices.Add(budgetMinPrice);
            }
            budgetMinPriceTransfers.TotalAmount = budgetMinPriceTransfers.BudgetMinPrices.Sum(o => o.TransferTotalAmount);
            budgetMinPriceTransfers.TotalSuccess = budgetMinPriceTransfers.BudgetMinPrices.Where(x => x.isCorrected == true).Count();
            budgetMinPriceTransfers.TotalError = budgetMinPriceTransfers.BudgetMinPrices.Where(x => x.isCorrected == false).Count();
            string exerrMsgChkAmountZero = null;
            if (CountItemAmount > 0)
            {

                var errMsgexerrMsgChkAmountZero = DB.ErrorMessages.Where(o => o.Key == "ERR0135").FirstOrDefault();
                exerrMsgChkAmountZero = errMsgexerrMsgChkAmountZero.Message.Replace("[field]", CountItemAmount.ToString());
            }
            if (CountItemUnit > 0)
            {

                var errMsgexerrMsgChkAmountZero = DB.ErrorMessages.Where(o => o.Key == "ERR0136").FirstOrDefault();
                if (string.IsNullOrEmpty(exerrMsgChkAmountZero))
                {
                    exerrMsgChkAmountZero = errMsgexerrMsgChkAmountZero.Message.Replace("[field]", CountItemUnit.ToString());
                }
                else
                {
                    exerrMsgChkAmountZero = System.Environment.NewLine + errMsgexerrMsgChkAmountZero.Message.Replace("[field]", CountItemUnit.ToString());
                }


            }
            if (!string.IsNullOrEmpty(exerrMsgChkAmountZero))
            {
                budgetMinPriceTransfers.ChkError = new ChkErrorDTO();
                budgetMinPriceTransfers.ChkError.MsgPopUP = true;
                budgetMinPriceTransfers.ChkError.Msg = exerrMsgChkAmountZero;
            }
            else
            {
                budgetMinPriceTransfers.ChkError = new ChkErrorDTO();
                budgetMinPriceTransfers.ChkError.MsgPopUP = false;
            }

            return budgetMinPriceTransfers;
        }


        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137951/preview
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public async Task ConfirmImportTransferBudgetAsync(BudgetMinPriceTransferDTO inputs)
        {
            var masterCenterTransferID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.TransferPromotion)).ID;
            var allproject = await DB.Projects.AsNoTracking().ToListAsync();
            inputs.BudgetMinPrices = inputs.BudgetMinPrices.Where(x => x.isCorrected == true).ToList();

            //if (chkCorrected.Count == 0)
            //{
            var budgetMinPrices = new List<BudgetMinPrice>();
            foreach (var item in inputs.BudgetMinPrices)
            {
                var project = allproject.Find(o => o.ProjectNo == item.Project.ProjectNo);
                if (project != null)
                {
                    var model = new BudgetMinPrice();
                    model.ProjectID = project.ID;
                    model.TotalAmount = item.TransferTotalAmount;
                    model.UnitAmount = item.TransferTotalUnit;
                    model.Quarter = item.Quarter;
                    model.Year = item.Year;
                    model.ActiveDate = DateTime.Now;
                    model.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;
                    budgetMinPrices.Add(model);
                }
            }
            await DB.BudgetMinPrices.AddRangeAsync(budgetMinPrices);
            await DB.SaveChangesAsync();
            //}
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137952/preview
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<FileDTO> ExportTransferBudgetAsync(BudgetMinPriceFilter filter, CancellationToken cancellationToken = default)
        {
            ExportExcel result = new ExportExcel();
            var allProjects = await DB.Projects.Include(x => x.BG).Include(x => x.SubBG).Where(x => x.IsActive == true).ToListAsync(cancellationToken);
            if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
            {
                allProjects = allProjects.Where(o => o.ID == filter.ProjectID).ToList();
            }
            if (filter.BG != null)
            {
                allProjects = allProjects.Where(o => o.BGID == filter.BG).ToList();
            }
            if (filter.SUBBG != null)
            {
                allProjects = allProjects.Where(o => o.SubBGID == filter.SUBBG).ToList();
            }
            var idTransferPromotion = (await DB.MasterCenters.FirstOrDefaultAsync(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType
             && x.Key == BudgetMinPriceTypeKeys.TransferPromotion, cancellationToken))?.ID;

            // Step 1: Retrieve Project IDs
            var projectIds = allProjects.Select(z => z.ID).ToList();

            // Step 2: Filter BudgetMinPrices using the list of Project IDs
            var budgetMinPriceQuarterly = await DB.BudgetMinPrices
                .Where(o => projectIds.Contains(o.ProjectID)
                            && o.Quarter == filter.Quarter
                            && o.Year == filter.Year
                            && o.BudgetMinPriceTypeMasterCenterID == idTransferPromotion)
                .Include(x => x.BudgetMinPriceType)
                .OrderByDescending(p => p.ActiveDate)
                .ToListAsync(cancellationToken) ?? new List<BudgetMinPrice>();


            var temp = (from Project in allProjects
                        join TransferBudget in budgetMinPriceQuarterly
                        on Project.ID equals TransferBudget.ProjectID into ps
                        from TransferBudgetModel in ps.DefaultIfEmpty()
                        select new BudgetMinPriceQueryResult
                        {
                            Project = Project,
                            BudgetMinPrice = TransferBudgetModel ?? new BudgetMinPrice()
                        }
                        );
            #region Filter
            //if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
            //{
            //    temp = temp.Where(o => o.Project.ID == filter.ProjectID).ToList();
            //}
            //if (filter.Year != null)
            //{
            //    temp = temp.Where(o => o.BudgetMinPrice.Year == filter.Year).ToList();
            //}
            //if (filter.Quarter != null)
            //{
            //    temp = temp.Where(o => o.BudgetMinPrice.Quarter == filter.Quarter).ToList();
            //}
            #endregion

            var query = temp.GroupBy(o => new { o.Project }).Select(o => new BudgetMinPriceQueryResult
            {
                Project = o.Key.Project,
                BudgetMinPrice = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceQuarterly = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.Quarterly).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceTransfer = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.TransferPromotion).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
            }).OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.BudgetMinPrice.Year).ThenBy(o => o.BudgetMinPrice.Quarter).ToList();

            //var results = query.Select(o => BudgetMinPriceDTO.CreateFromQueryResult(o, filter.Year, filter.Quarter)).ToList();
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "TransferSaleBudget.xlsx");
            byte[] tmp = await File.ReadAllBytesAsync(path);

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectBGIndex = BudgetMinPriceTransferExcelModel._projectBGIndex + 1;
                int _projectSUBBGIndex = BudgetMinPriceTransferExcelModel._projectSUBBGIndex + 1;
                int _projectNoIndex = BudgetMinPriceTransferExcelModel._projectNoIndex + 1;
                int _projectNameIndex = BudgetMinPriceTransferExcelModel._projectNameIndex + 1;
                int _yearIndex = BudgetMinPriceTransferExcelModel._yearIndex + 1;
                int _quarterIndex = BudgetMinPriceTransferExcelModel._quarterIndex + 1;
                int _transferTotalAmountIndex = BudgetMinPriceTransferExcelModel._transferTotalAmountIndex + 1;
                int _transferTotalUnitIndex = BudgetMinPriceTransferExcelModel._transferTotalUnitIndex + 1;

                for (int c = 2; c < query.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectBGIndex].Value = query[c - 2].Project?.BG?.Name;
                    worksheet.Cells[c, _projectSUBBGIndex].Value = query[c - 2].Project?.SubBG?.Name;
                    worksheet.Cells[c, _projectNoIndex].Value = query[c - 2].Project?.ProjectNo;
                    worksheet.Cells[c, _projectNameIndex].Value = query[c - 2].Project?.ProjectNameTH;
                    worksheet.Cells[c, _yearIndex].Value = filter.Year ?? 0;
                    worksheet.Cells[c, _quarterIndex].Value = filter.Quarter ?? 0;
                    worksheet.Cells[c, _transferTotalAmountIndex].Value = query[c - 2].BudgetMinPriceTransfer?.TotalAmount ?? 0;
                    worksheet.Cells[c, _transferTotalUnitIndex].Value = query[c - 2].BudgetMinPriceTransfer?.UnitAmount ?? 0;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                if (filter.ProjectID == null)
                {
                    result.FileName = "TransferSaleBudget_" + filter.Year + "_" + filter.Quarter + ".xlsx";
                }
                else
                {
                    var project = await DB.Projects.Where(o => o.ID == filter.ProjectID).FirstAsync();
                    result.FileName = "TransferSaleBudget_" + project.ProjectNo + "_" + filter.Year + "_" + filter.Quarter + ".xlsx";
                }
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = result.FileName; //$"{Guid.NewGuid()}_{result.FileName}"; 
            string contentType = result.FileType;
            string filePath = $"budget-minprices/";

            var uploadResult = await FileHelper.UploadFileFromStreamWithOutGuid(fileStream, Environment.GetEnvironmentVariable("minio_DefaultBucket"), filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = result.FileName,
                Url = uploadResult.Url
            };
        }

        private async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
        {
            try
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
                        int xxx = ws.Dimension.End.Row;

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
            catch
            {
                //throw new Exception("Invalid File Format");
                ValidateException ex = new ValidateException();
                var errMsgFile = await DB.ErrorMessages.Where(o => o.Key == "ERR0147").FirstAsync();
                ex.AddError(errMsgFile.Key, errMsgFile.Message, (int)errMsgFile.Type);
                throw ex;
            }
        }



        private async Task<DataTable> ConvertExcelToDataTableQuarterly(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                try
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
                        foreach (var firstRowCell in ws.Cells[6, 1, 6, ws.Dimension.End.Column])
                        {
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                        }
                        var startRow = hasHeader ? 7 : 6;


                        ValidateException ex = new ValidateException();
                        BudgetMinPriceDTO MsgDTO = new BudgetMinPriceDTO();
                        int Quarter;
                        if (!int.TryParse(ws.Cells[4, 2].Text, out Quarter))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstAsync();
                            string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[message]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                        else if (Quarter < 1 || Quarter > 4)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstAsync();
                            string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[message]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }


                        int Year;
                        if (!int.TryParse(ws.Cells[3, 2].Text, out Year))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                            ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                        }
                        else if (Year < 2000 || Year > 2300)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                            ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                        }

                        if (Quarter != 0 && Year != 0)
                        {
                            if (BudgetMinPriceTransferExcelModel.CheckOldQuater(Year, Quarter))
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0168").FirstAsync();
                                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                            }
                        }

                        if (ex.HasError)
                        {
                            throw ex;
                        }

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
                catch (ValidateException ex)
                {
                    throw ex;
                }
                catch (Exception exp)
                {
                    stream.Close();
                    ValidateException ex = new ValidateException();
                    var errMsgFile = await DB.ErrorMessages.Where(o => o.Key == "ERR0147").FirstAsync();
                    ex.AddError(errMsgFile.Key, errMsgFile.Message, (int)errMsgFile.Type);
                    throw ex;
                }
            }
        }

        private async Task<HeaderQuarterlyExcelModel> GetHeaderQuarterly(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;
            BudgetMinPriceDTO MsgDTO = new BudgetMinPriceDTO();
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                try
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
                        HeaderQuarterlyExcelModel headerModel = new HeaderQuarterlyExcelModel();
                        headerModel.ProjectNo = ws.Cells[1, 2].Text;
                        ValidateException ex = new ValidateException();

                        var project = await DB.Projects.Where(x => x.ProjectNo.Equals(headerModel.ProjectNo)).FirstOrDefaultAsync();

                        if (project == null)
                        {
                            var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstOrDefault();
                            var msg = errMsg.Message.Replace("[field]", "ProjectNo");
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            throw ex;
                        }

                        decimal d = 0;
                        var result = decimal.TryParse(ws.Cells[2, 2].Text, out d);

                        headerModel.QuarterlyTotalAmount = d;
                        if (headerModel.QuarterlyTotalAmount < 0)
                        {
                            var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault();
                            ex.AddError(errMsg.Key, errMsg.Message + "- QuarterlyTotalAmount ", (int)errMsg.Type);
                            throw ex;
                        }
                        int quarter;
                        if (int.TryParse(ws.Cells[4, 2].Text, out quarter))
                        {
                            if (quarter < 1 || quarter > 4)
                            {
                                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstOrDefault();
                                string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[message]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            else
                            {
                                headerModel.Quarter = quarter;
                            }

                        }
                        else
                        {
                            var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstOrDefault();
                            ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                        }
                        int year;
                        if (int.TryParse(ws.Cells[3, 2].Text, out year))
                        {
                            if (year < 2000 || year > 2300)
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                            }
                            headerModel.Year = year;
                        }
                        else
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                            ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                        }
                        if (ex.HasError)
                        {
                            throw ex;
                        }
                        return headerModel;
                    }
                }
                catch (ValidateException ex)
                {
                    throw ex;
                }
                catch (Exception exp)
                {
                    stream.Close();
                    ValidateException ex = new ValidateException();
                    var errMsgFile = await DB.ErrorMessages.Where(o => o.Key == "ERR0147").FirstAsync();
                    ex.AddError(errMsgFile.Key, errMsgFile.Message, (int)errMsgFile.Type);
                    throw ex;
                }
            }
        }


        private async Task<decimal> GetOldUsedAmount(Guid budgetMinPriceID)
        {
            var usedAmount = await DB.BudgetMinPrices.Where(o => o.ID == budgetMinPriceID).Select(o => o.UsedAmount).FirstAsync();

            return usedAmount;
        }

        private string getUnitStatus(string Key)
        {
            string result = "";

            if (!string.IsNullOrEmpty(Key))
            {
                if (Key.Equals(UnitStatusKeys.Available))
                {
                    result = "ว่าง";
                }
                else if (Key.Equals(UnitStatusKeys.WaitingForConfirmBooking) || Key.Equals(UnitStatusKeys.WaitingForAgreement))
                {
                    result = "จอง";
                }
                else if (Key.Equals(UnitStatusKeys.WaitingForTransfer))
                {
                    result = "สัญญา";
                }
                else if (Key.Equals(UnitStatusKeys.Transfer))
                {
                    result = "โอนกรรมสิทธิ์";
                }
            }

            return result;
        }
    }
}
