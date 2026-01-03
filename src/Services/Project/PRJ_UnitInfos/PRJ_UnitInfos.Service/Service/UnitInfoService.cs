using Base.DTOs.SAL;
using Database.Models;
using PagingExtensions;
using Microsoft.EntityFrameworkCore;
using Database.Models.SAL;
using FileStorage;
using static Base.DTOs.SAL.UnitInfoListDTO;
using Database.Models.DbQueries;
using Base.DTOs;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using PRJ_UnitInfos.Params.Outputs;
using PRJ_UnitInfos.Params.Filters;
using System.Data.Common;
using Dapper;
using Database.Models.DbQueries.SAL;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Common.Helper.Logging;

namespace PRJ_UnitInfos.Services
{
    public class UnitInfoService : IUnitInfoService
    {
        private readonly DatabaseContext DB;
        private FileHelper FileHelper;
        public LogModel logModel { get; set; }
        public UnitInfoService(DatabaseContext db)
        {
            logModel = new LogModel("UnitInfoService", null);
            DB = db;
            var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
            var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
            var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
            var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
            var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var minioBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
            var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");
            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, minioWithSSL == "true");
        }

        public async Task<UnitInfoListPaging> GetUnitInfoListAsync(UnitInfoListFilter filter, PageParam pageParam, UnitInfoListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            #region Filter
            if (filter.ProjectID != null)
                ParamList.Add("ProjectID", filter.ProjectID);

            if (!string.IsNullOrEmpty(filter.UnitNo))
                ParamList.Add("UnitNo", filter.UnitNo);

            if (!string.IsNullOrEmpty(filter.HouseNo))
                ParamList.Add("HouseNo", filter.HouseNo);

            if (!string.IsNullOrEmpty(filter.FullName))
                ParamList.Add("FullName", filter.FullName);

            if (!string.IsNullOrEmpty(filter.BookingNo))
                ParamList.Add("BookingNo", filter.BookingNo);

            if (!string.IsNullOrEmpty(filter.AgreementNo))
                ParamList.Add("AgreementNo", filter.AgreementNo);

            if (!string.IsNullOrEmpty(filter.TransferPromotionNo))
                ParamList.Add("TransferPromotionNo", filter.TransferPromotionNo);

            if (!string.IsNullOrEmpty(filter.TransferNo))
                ParamList.Add("TransferNo", filter.TransferNo);

            if (!string.IsNullOrEmpty(filter.UnitStatusKeys))
                ParamList.Add("UnitStatusKeys", filter.UnitStatusKeys);

            if (filter.BankID != null)
                ParamList.Add("BankID", filter.BankID);

            if (filter.ContactID != null)
                ParamList.Add("ContactID", filter.ContactID);

            if (filter.TowerID != null)
                ParamList.Add("TowerID", filter.TowerID);

            #endregion Filter

            var sortby = string.Empty;
            bool sort = true;
            sortby = nameof(UnitInfoListSortBy.UnitNo);
            if (sortByParam.SortBy != null)
            {
                sort = sortByParam.Ascending;
                switch (sortByParam.SortBy.Value)
                {
                    case UnitInfoListSortBy.FullName:
                        sortby = nameof(UnitInfoListSortBy.FullName);
                        break;
                    case UnitInfoListSortBy.HouseNo:
                        sortby = nameof(UnitInfoListSortBy.HouseNo);
                        break;
                    case UnitInfoListSortBy.BookingNo:
                        sortby = nameof(UnitInfoListSortBy.BookingNo);
                        break;
                    case UnitInfoListSortBy.UnitStatus:
                        sortby = nameof(UnitInfoListSortBy.UnitStatus);
                        break;
                    case UnitInfoListSortBy.ProjectNo:
                        sortby = nameof(UnitInfoListSortBy.ProjectNo);
                        break;
                    case UnitInfoListSortBy.UnitNo:
                        sortby = nameof(UnitInfoListSortBy.UnitNo);
                        break;
                    default:
                        sortby = nameof(UnitInfoListSortBy.UnitNo);
                        break;
                }
            }
            ParamList.Add("Sys_SortBy", sortby);
            ParamList.Add("Sys_SortType", sort ? "asc" : "desc");
            ParamList.Add("Page", pageParam?.Page ?? 1);
            ParamList.Add("PageSize", pageParam?.PageSize ?? 10);



            //unknown 285 

            var dbTransaction = DB?.Database?.CurrentTransaction?.GetDbTransaction();

            CommandDefinition commandDefinition = new(
                                     commandText: DBStoredNames.sp_UNIT_SEARCH_UnitInfoList,
                                     cancellationToken: cancellationToken,
                                     parameters: ParamList,
                                     transaction: dbTransaction,
                                     commandType: CommandType.StoredProcedure);
            var query = await cmd.Connection.QueryAsync<dbqUnitInfoList>(commandDefinition);



            var querylist = query.ToList();
            var results = new List<UnitInfoListDTO>();
            PageOutput pageout = new PageOutput();
            if (querylist.Count > 0)
            {
                //results = querylist.Select(o =>   CreateFromQuery(o, DB)).ToList();
                foreach (var x in querylist)
                {
                    UnitInfoListDTO res = CreateFromQuery(x, DB);
                    results.Add(res);
                }
                pageout = querylist.FirstOrDefault() != null ? querylist.FirstOrDefault().CreateBaseDTOFromQuery() : new PageOutput();
            }

            return new UnitInfoListPaging()
            {
                PageOutput = pageout,
                Units = results
            };
        }

         public async Task<UnitInfoListPagingByContact> GetUnitInfoListByContactAsync(UnitInfoListFilterByContact filter, PageParam pageParam, UnitInfoListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            #region Filter 

            if (filter.ContactID != null)
                ParamList.Add("ContactID", filter.ContactID);

            #endregion Filter

            var sortby = string.Empty;
            bool sort = true;
            sortby = nameof(UnitInfoListSortBy.UnitNo);
            if (sortByParam.SortBy != null)
            {
                sort = sortByParam.Ascending;
                switch (sortByParam.SortBy.Value)
                {

                    case UnitInfoListSortBy.UnitNo:
                        sortby = nameof(UnitInfoListSortBy.UnitNo);
                        break;
                    case UnitInfoListSortBy.ProjectNo:
                        sortby = nameof(UnitInfoListSortBy.ProjectNo);
                        break;
                    case UnitInfoListSortBy.BookingNo:
                        sortby = nameof(UnitInfoListSortBy.BookingNo);
                        break;
                    case UnitInfoListSortBy.TransferNo:
                        sortby = nameof(UnitInfoListSortBy.TransferNo);
                        break;
                    case UnitInfoListSortBy.AgreementNo:
                        sortby = nameof(UnitInfoListSortBy.AgreementNo);
                        break;
                    case UnitInfoListSortBy.UnitStatus:
                        sortby = nameof(UnitInfoListSortBy.UnitStatus);
                        break;
                    default:
                        sortby = nameof(UnitInfoListSortBy.UnitNo);
                        break;
                }
            }
            ParamList.Add("Sys_SortBy", sortby);
            ParamList.Add("Sys_SortType", sort ? "asc" : "desc");
            ParamList.Add("Page", pageParam?.Page ?? 1);
            ParamList.Add("PageSize", pageParam?.PageSize ?? 10);



            //unknown 285 
            CommandDefinition commandDefinition = new(
                                     commandText: DBStoredNames.sp_UNIT_SEARCH_UnitInfoList_By_Contact,
                                     cancellationToken: cancellationToken,
                                     parameters: ParamList,
                                     commandType: CommandType.StoredProcedure);
            var query = await cmd.Connection.QueryAsync<dbqUnitInfoListByContact>(commandDefinition);



            var querylist = query.ToList();
            return new UnitInfoListPagingByContact()
            {
                PageOutput = querylist.FirstOrDefault() != null ? querylist.FirstOrDefault().CreateBaseDTOFromQuery() : new PageOutput(),
                Units = querylist
            };
        }

        public async Task<UnitInfoDTO> GetUnitInfoAsync(Guid unitID, CancellationToken cancellationToken = default)
        {
            var unitQuery = DB.Units.AsNoTracking()
                .Include(o => o.UnitStatus)
                .Include(o => o.Model)
                    .ThenInclude(o => o.TypeOfRealEstate)
                .Include(o => o.TitledeedDetails)
                .Include(o => o.Floor)
                .Include(o => o.Tower)
                .Include(o => o.Project)
                    .ThenInclude(o => o.ProductType)
                .Include(o => o.AssetType)
                .Where(o => !o.IsDeleted && o.ID == unitID);

            var bookingQuery = DB.Bookings.AsNoTracking()
                .Include(b => b.BookingStatus)
                .Where(b => !b.IsDeleted && !b.IsCancelled);

            var query = from unit in unitQuery
                        join booking in bookingQuery on unit.ID equals booking.UnitID into bookings
                        from booking in bookings.DefaultIfEmpty()
                        select new UnitInfoQueryResult
                        {
                            Project = unit.Project,
                            Unit = unit,
                            Booking = booking ?? new Booking()
                        };

            var data = await query.FirstOrDefaultAsync(cancellationToken);

            var result = await UnitInfoDTO.CreateFromQueryResultAsync(data, DB, FileHelper);
            return result;
        }

        public async Task<UnitInfoDTO> GetUnitInfoForPaymentAsync(Guid unitID, CancellationToken cancellationToken = default)
        {
            var query = await (from o in DB.Units.AsNoTracking().Where(o => o.ID == unitID)
                               .Include(o => o.UnitStatus)
                                      .Include(o => o.TitledeedDetails)
                                      .Include(o => o.Project)

                               join bk in DB.Bookings.Where(o => o.IsCancelled == false).Include(o => o.BookingStatus).Include(o => o.CreatedBy).Where(o => o.IsCancelled == false) on o.ID equals bk.UnitID into bkData
                               from bkModel in bkData.DefaultIfEmpty()

                               join bkSaleUser in DB.Users on bkModel.SaleUserID equals bkSaleUser.ID into bkSaleUserData
                               from bkSaleUserModel in bkSaleUserData.DefaultIfEmpty()

                               join ag in DB.Agreements.Include(o => o.CreatedBy).Where(o => o.IsCancel == false) on o.ID equals ag.UnitID into agData
                               from agModel in agData.DefaultIfEmpty()

                               join tf in DB.Transfers on agModel.ID equals tf.AgreementID into tfData
                               from tfModel in tfData.DefaultIfEmpty()

                               join tfLC in DB.Users on tfModel.TransferSaleUserID equals tfLC.ID into tfLCData
                               from tfLCModel in tfLCData.DefaultIfEmpty()

                               join tfStatus in DB.MasterCenters on tfModel.TransferStatusMasterCenterID equals tfStatus.ID into tfStatusData
                               from tfStatusModel in tfStatusData.DefaultIfEmpty()

                               select new UnitInfoQueryResultForPayment
                               {
                                   Project = o.Project,
                                   Unit = o,
                                   Booking = bkModel ?? new Booking(),
                                   Agreement = agModel ?? new Agreement(),
                                   Transfer = tfModel ?? new Transfer(),
                                   TransferSaleUser = tfLCModel ?? new User(),
                                   SaleUser = bkSaleUserModel ?? new User(),
                                   TransferStatus = tfStatusModel ?? new MasterCenter(),
                               }).FirstOrDefaultAsync(cancellationToken);

            var result = await UnitInfoDTO.CreateFromQueryResultForPaymentAsync(query, DB);
            return result;
        }

        public async Task<UnitInfoSalePromotionDTO> GetUnitInfoSalePromotionAsync(Guid unitID, CancellationToken cancellationToken = default)
        {
            var booking = await DB.Bookings.AsNoTracking()
                .Where(o => o.UnitID == unitID && o.IsCancelled == false).FirstOrDefaultAsync(cancellationToken);
            if (booking != null)
            {
                var model = await DB.SalePromotions
                    .Include(o => o.MasterPromotion)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.BookingID == booking.ID && o.IsActive == true).FirstOrDefaultAsync(cancellationToken);

                var result = await UnitInfoSalePromotionDTO.CreateFromModelAsync(model, DB);
                return result;
            }
            else
            {
                var result = await UnitInfoSalePromotionDTO.CreateFromUnitAsync(unitID, DB);
                return result;
            }
        }

        public async Task<List<UnitInfoSalePromotionExpenseDTO>> GetUnitInfoPromotionExpensesAsync(Guid unitID, CancellationToken cancellationToken = default)
        {
            var booking = await DB.Bookings.AsNoTracking().Where(o => o.UnitID == unitID && o.IsCancelled == false).FirstOrDefaultAsync(cancellationToken);
            if (booking != null)
            {
                var promotionTransfer = await DB.TransferPromotions.AsNoTracking().Where(o => o.BookingID == booking.ID && o.IsActive == true && o.IsApprove == true).FirstOrDefaultAsync(cancellationToken);
                var unitPrice = await DB.UnitPrices.AsNoTracking().Where(o => o.BookingID == booking.ID).Select(o => o.ID).FirstOrDefaultAsync(cancellationToken);

                if (promotionTransfer != null)
                {
                    var models = await DB.TransferPromotionExpenses
                        .Include(o => o.MasterPriceItem)
                        .Include(o => o.ExpenseReponsibleBy)
                        .Include(o => o.TransferPromotion)
                        .Where(o => o.TransferPromotionID == promotionTransfer.ID)
                        .ToListAsync();

                    var results = models.Select(async o => await UnitInfoSalePromotionExpenseDTO.CreateFromModelAsync(o, DB))
                                        .Select(o => o.Result)
                                        .OrderBy(o => o.Order)
                                        .ToList();
                    return results;
                }
                else
                {
                    var promotion = await DB.SalePromotions.AsNoTracking().Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync(cancellationToken);
                    var models = await DB.SalePromotionExpenses
                        .Include(o => o.MasterPriceItem)
                        .Include(o => o.ExpenseReponsibleBy)
                        .Include(o => o.SalePromotion)
                        .Where(o => o.SalePromotionID == promotion.ID)
                        .ToListAsync();

                    var results = models.Select(async o => await UnitInfoSalePromotionExpenseDTO.CreateFromModelAsync(o, DB))
                                        .Select(o => o.Result)
                                        .OrderBy(o => o.Order)
                                        .ToList();
                    return results;
                }
            }
            else
            {
                var result = await UnitInfoSalePromotionExpenseDTO.CreateDraftFromUnitAsync(unitID, DB);

                return result;
            }

        }

        public async Task<UnitInfoPreSalePromotionDTO> GetUnitInfoPreSalePromotionAsync(Guid unitID, CancellationToken cancellationToken = default)
        {
            var booking = await DB.Bookings.AsNoTracking().Where(o => o.UnitID == unitID && o.IsCancelled == false).FirstOrDefaultAsync(cancellationToken);
            if (booking != null)
            {
                var model = await DB.PreSalePromotions.AsNoTracking()
                .Include(o => o.MasterPromotion)
                .Include(o => o.UpdatedBy)
                .Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync(cancellationToken);

                var result = await UnitInfoPreSalePromotionDTO.CreateFromModelAsync(model, DB);

                return result;
            }
            else
            {
                var result = await UnitInfoPreSalePromotionDTO.CreateFromUnitAsync(unitID, DB);

                return result;
            }
        }

        public async Task<UnitInfoPriceListDTO> GetPriceListAsync(Guid unitID, CancellationToken cancellationToken = default)
        {
            var booking = await DB.Bookings.AsNoTracking().Where(o => o.UnitID == unitID && o.IsCancelled == false).FirstOrDefaultAsync(cancellationToken);
            if (booking != null)
            {
                var unitPrice = await DB.UnitPrices
                                    .Include(o => o.Booking)
                                    .ThenInclude(o => o.ReferContact)
                                    .Where(o => o.BookingID == booking.ID && o.IsActive == true)
                                    .FirstOrDefaultAsync(cancellationToken);

                var result = await UnitInfoPriceListDTO.CreateFromModelAsync(unitPrice, DB);
                return result;
            }
            else
            {
                var result = await UnitInfoPriceListDTO.CreateDraftFromUnitAsync(unitID, DB);
                return result;
            }
        }

        public async Task<UnitInfoStatusCountDTO> GetUnitInfoCountAsync(Guid? projectID, CancellationToken cancellationToken = default)
        {

            var result = new UnitInfoStatusCountDTO();

            if (projectID != null)
            {
                var query = DB.Units.AsNoTracking()
                .Include(o => o.Project)
                .Include(o => o.UnitStatus)
                .Where(o => o.Project.IsActive == true);

                query = query.Where(o => o.ProjectID == projectID);

                result = new UnitInfoStatusCountDTO
                {
                    All = await query.CountAsync(o => o.AssetType.Key == AssetTypeKeys.Unit, cancellationToken),
                    Open = await query.CountAsync(o => o.AssetType.Key == AssetTypeKeys.Unit, cancellationToken),
                    Available = await query.CountAsync(o => o.AssetType.Key == AssetTypeKeys.Unit && o.UnitStatus.Key == UnitStatusKeys.Available, cancellationToken),
                    WaitingForConfirmBooking = await query.CountAsync(o => o.UnitStatus.Key == UnitStatusKeys.WaitingForConfirmBooking, cancellationToken),
                    WaitingForAgreement = await query.CountAsync(o => o.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement, cancellationToken),
                    WaitingForTransfer = await query.CountAsync(o => o.UnitStatus.Key == UnitStatusKeys.WaitingForTransfer, cancellationToken),
                    Transfer = await query.CountAsync(o => o.UnitStatus.Key == UnitStatusKeys.Transfer, cancellationToken),
                    PreTransfer = await query.CountAsync(o => o.UnitStatus.Key == UnitStatusKeys.PreTransfer, cancellationToken)
                };
            }

            return result;
        }


        public async Task<string> CheckPrebookAsync(Guid? unitID)
        {
            //var Prebook = await DB.Quotations.Where(o => o.IsPrebook == true && o.UnitID == unitID).FirstOrDefaultAsync();
            var Prebook = await DB.PaymentPrebooks
                                .Include(o => o.Quotation)
                                .Where(o => o.Quotation.IsPrebook == true && o.Quotation.UnitID == unitID && o.IsCancel == false && o.IsChangeUnit == false && o.IsCancelRealBook == false && o.IsCancelAgreement == false)
                                .AnyAsync();

            if (Prebook)
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR8080").FirstAsync();
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                throw ex;
            }
            else
            {
                return "";
            }


        }

    }
}
