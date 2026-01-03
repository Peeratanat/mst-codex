using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Dapper.Contrib.Extensions;
using Database.Models.DbQueries.PRJ;
using PRJ_ProjectInfo.Params.Outputs;
using static Database.Models.PRJ.ProjectInformationModel;
using Database.Models.PRJ;
using Database.Models;
using Common.Helper.Logging;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using Database.Models.MST;
using EFCore.BulkExtensions;

namespace PRJ_ProjectInfo.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public const string TAG = "ProjectInfoRepository";
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }


        public ProjectRepository(IHostingEnvironment environment, IConfiguration config, DatabaseContext dB)
        {
            logModel = new LogModel("ProjectRepository", null);
            _config = config;
            _hostingEnvironment = environment;
            DB = dB;
        }

        public async Task<List<ProjectInfo>> GetProjectInfoAsync(Guid? ID, Guid? ProjectID, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            var queryBuilder = new StringBuilder("SELECT * FROM PRJ.ProjectInfo WITH(NOLOCK) WHERE IsDeleted = 0");

            if (ID.HasValue)
            {
                queryBuilder.Append(" AND ID = @ID");
                ParamList.Add("@ID", ID);
            }

            if (ProjectID.HasValue)
            {
                queryBuilder.Append(" AND ProjectID = @ProjectID");
                ParamList.Add("@ProjectID", ProjectID);
            }

            CommandDefinition commandDefinition = new(
                                 commandText: queryBuilder.ToString(),
                                 parameters: ParamList,
                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                 commandType: CommandType.Text,
                                 cancellationToken: cancellationToken
                             );
            var result = (await cmd.Connection.QueryAsync<ProjectInfo>(commandDefinition))?.ToList() ?? [];
            return result;
        }

        public async Task<sql_ProjectInformation.PageResult> GetProjectInformationListAsync(ProjectInformationPaging.Filter filter, ProjectInformationPaging.SortByParam sortByParam, PageParam page, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            var queryBuilder = new StringBuilder(sql_ProjectInformation.Query);

            // Filter
            if (filter.ID.HasValue)
            {
                queryBuilder.Append($" AND prj.ID = @ID");
                ParamList.Add("@ID", filter.ID);
            }

            if (!string.IsNullOrEmpty(filter.Zone))
            {
                queryBuilder.Append($" AND prj.ProjectLocation LIKE '%' + @Zone + '%'");
                ParamList.Add("@Zone", filter.Zone);
            }

            if (!string.IsNullOrEmpty(filter.BrandName))
            {
                queryBuilder.Append($" AND prj.BrandName = @BrandName");
                ParamList.Add("@BrandName", filter.BrandName);
            }

            if (!string.IsNullOrEmpty(filter.ProjectTypeID))
            {
                if (filter.ProjectTypeID == "3")
                    queryBuilder.Append($" AND(prj.BGNo IN ('3', '4') )");
                else
                {
                    queryBuilder.Append($" AND prj.BGNo IN (@ProjectTypeID)");
                    ParamList.Add("@ProjectTypeID", filter.ProjectTypeID);
                }
            }

            if (!string.IsNullOrEmpty(filter.UnitType))
            {
                queryBuilder.Append($" AND prj.UnitTypeDescription LIKE '%' + @UnitType + '%'");
                ParamList.Add("@UnitType", filter.UnitType);
            }

            if ((filter.SellingPriceMin ?? 0) > 0)
            {
                queryBuilder.Append($" AND(ISNULL(prj.SellingPriceMin, 0) >= @SellingPriceMin AND prj.SellingPriceMin IS NOT NULL)");
                ParamList.Add("@SellingPriceMin", filter.SellingPriceMin);
            }

            if ((filter.SellingPriceMax ?? 0) > 0)
            {
                queryBuilder.Append($" AND(ISNULL(prj.SellingPriceMax, 0) <= @SellingPriceMax AND prj.SellingPriceMax IS NOT NULL)");
                ParamList.Add("@SellingPriceMax", filter.SellingPriceMax);
            }

            if (!string.IsNullOrEmpty(filter.ProjectStatusKey))
            {
                if (filter.ProjectStatusKey == "0")
                    queryBuilder.Append($" AND prj.ProjectStatusKey = @ProjectStatusID AND LasttUnitTransferDate IS NULL");

                if (filter.ProjectStatusKey == "1")
                    queryBuilder.Append($" AND prj.ProjectStatusKey = @ProjectStatusID AND IsActive = 1");

                if (filter.ProjectStatusKey == "2")
                    queryBuilder.Append($" AND prj.ProjectStatusKey = @ProjectStatusID AND LasttUnitTransferDate IS NOT NULL");

                ParamList.Add("@ProjectStatusID", filter.ProjectStatusKey);
            }

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                queryBuilder.Append(@" AND (
		                            prj.ProjectNo LIKE '%' + @SearchText + '%'
		                            OR prj.ProjectNameTH LIKE '%' + @SearchText + '%'
		                            OR prj.ProjectType LIKE '%' + @SearchText + '%'
		                            OR prj.ProjectStatus LIKE '%' + @SearchText + '%'
		                            OR prj.ProjectLocation LIKE '%' + @SearchText + '%'
		                            OR prj.UnitTypeDescription LIKE '%' + @SearchText + '%'
	                            )");
                ParamList.Add("@SearchText", filter.SearchText);
            }

            // Sort By 
            if (sortByParam?.SortBy != null)
                queryBuilder.Append($" ORDER BY {sortByParam.SortBy.Value} {sortByParam.Ascending.Value}");
            else
                queryBuilder.Append(" ORDER BY ProjectNameTH DESC ");

            CommandDefinition commandDefinition = new(
                                           commandText: queryBuilder.ToString(),
                                           parameters: ParamList,
                                           transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                           commandType: CommandType.Text,
                                           cancellationToken: cancellationToken
                                       );

            var TotalCount = (await cmd.Connection.QueryAsync<sql_ProjectInformation.Result>(commandDefinition)).Count();
            page.Page ??= 1;
            page.PageSize ??= 10;
            ParamList.Add("@Page", page.Page - 1); // ลบ -1 เสมอ
            ParamList.Add("@PageSize", page.PageSize);
            queryBuilder.Append(" OFFSET (@Page * @PageSize) ROWS FETCH FIRST @PageSize ROWS ONLY");

            commandDefinition = new(
                              commandText: queryBuilder.ToString(),
                              parameters: ParamList,
                              transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                              commandType: CommandType.Text,
                              cancellationToken: cancellationToken
                          );

            var Results = (await cmd.Connection.QueryAsync<sql_ProjectInformation.Result>(commandDefinition)).ToList();

            var PageCount = (int?)Math.Ceiling(TotalCount / (decimal)page.PageSize);

            return new sql_ProjectInformation.PageResult
            {
                DataResult = Results ?? new(),
                Page = page.Page ?? 0,
                PageCount = PageCount ?? 0,
                PageSize = page.PageSize ?? 0,
                RecordCount = TotalCount
            };

        }

        public async Task<sql_ProjectInformation.Result> GetProjectInfoDetailAsync(Guid ProjectID, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();

            var queryBuilder = new StringBuilder(sql_ProjectInformation.Query);
            queryBuilder.Append(" AND(prj.ID = @ID)");

            CommandDefinition commandDefinition = new(
                                 commandText: queryBuilder.ToString(),
                                 parameters: new { @ID = ProjectID },
                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                 commandType: CommandType.Text,
                                 cancellationToken: cancellationToken
                             );

            var Results = await cmd.Connection.QueryFirstOrDefaultAsync<sql_ProjectInformation.Result>(commandDefinition) ?? new();
            return Results;

        }

        public async Task<List<sql_ProjectInforLocationDetail.Result>> GetProjectInfoLocationDetailAsync(Guid ProjectID, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            var queryBuilder = new StringBuilder(sql_ProjectInforLocationDetail.Query);
            queryBuilder.Append(" AND pil.ProjectID = @ProjectID");
            ParamList.Add("@ProjectID", ProjectID);


            CommandDefinition commandDefinition = new(
                                 commandText: queryBuilder.ToString(),
                                 parameters: ParamList,
                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                 commandType: CommandType.Text,
                                 cancellationToken: cancellationToken
                             );
            var result = (await cmd.Connection.QueryAsync<sql_ProjectInforLocationDetail.Result>(commandDefinition)).ToList() ?? [];
            return result;

        }

        public async Task<List<PRJ_ProjectInfoPromotion>> GetProjectInfoPromotionAsync(Guid ProjectID, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();



            var queryBuilder = new StringBuilder("SELECT * FROM PRJ.ProjectInfoPromotion WITH(NOLOCK) WHERE IsDeleted = 0");
            queryBuilder.Append(" AND ProjectID = @ProjectID");

            ParamList.Add("@ProjectID", ProjectID);

            CommandDefinition commandDefinition = new(
                                 commandText: queryBuilder.ToString(),
                                 parameters: ParamList,
                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                 commandType: CommandType.Text,
                                 cancellationToken: cancellationToken
                             );

            var result = (await cmd.Connection.QueryAsync<PRJ_ProjectInfoPromotion>(commandDefinition)).ToList() ?? [];
            return result;

        }

        public async Task<List<PRJ_ProjectInfoCampaign>> GetProjectInfoCampaignAsync(string BUType, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            var queryBuilder = new StringBuilder("SELECT * FROM PRJ.ProjectInfoCampaign WITH(NOLOCK) WHERE IsDeleted = 0 AND IsActive = 1");
            queryBuilder.Append(" AND BUType LIKE '%' + @BUType + '%'");

            ParamList.Add("@BUType", BUType);

            CommandDefinition commandDefinition = new(
                                 commandText: queryBuilder.ToString(),
                                 parameters: ParamList,
                                 transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                 commandType: CommandType.Text,
                                 cancellationToken: cancellationToken
                             );

            var result = (await cmd.Connection.QueryAsync<PRJ_ProjectInfoCampaign>(commandDefinition)).ToList() ?? [];
            return result;
        }

        public async Task<List<Brand>> GetActiveMSTBrandAsync(Guid? ID, string TextSearch, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();


            var queryBuilder = new StringBuilder(@"SELECT DISTINCT * 
                        FROM MST.Brand b
                        WHERE b.IsDeleted = 0
	                        AND EXISTS (SELECT * FROM PRJ.Project p WHERE p.IsDeleted = 0 AND p.IsActive = 1 AND p.BrandID = b.ID AND p.BrandID IS NOT NULL)");

            if (ID.HasValue)
            {
                queryBuilder.Append(" AND ID = @ID");
                ParamList.Add("@ID", ID);
            }

            if (!string.IsNullOrEmpty(TextSearch))
            {
                queryBuilder.Append(" AND Name LIKE '%' + @TextSearch + '%'");
                ParamList.Add("@TextSearch", TextSearch);
            }
            CommandDefinition commandDefinition = new(
                                   commandText: queryBuilder.ToString(),
                                   parameters: ParamList,
                                   transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                   commandType: CommandType.Text,
                                   cancellationToken: cancellationToken
                               );
            var result = (await cmd.Connection.QueryAsync<Brand>(commandDefinition)).ToList() ?? [];
            return result;

        }

        public async Task<List<MasterCenter>> GetMasterCenterAsync(Guid? ID, string Name, string MasterCenterGroupKey, bool? IsDeleted, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            var sQuery = new StringBuilder(@"SELECT * FROM MST.MasterCenter WHERE 1=1 AND IsDeleted = 0 AND IsActive = 1");

            if (!string.IsNullOrEmpty(MasterCenterGroupKey))
            {
                sQuery.Append(" AND MasterCenterGroupKey = @MasterCenterGroupKey");
                ParamList.Add("@MasterCenterGroupKey", MasterCenterGroupKey);
            }

            if (!string.IsNullOrEmpty(Name))
            {
                sQuery.Append(" AND Name LIKE '%' + @Name + '%'");
                ParamList.Add("@Name", Name);
            }

            if (ID.HasValue)
            {
                sQuery.Append(" AND ID = @ID");
                ParamList.Add("@ID", ID);
            }

            if (IsDeleted.HasValue)
            {
                sQuery.Append(" AND IsDeleted = @IsDeleted");
                ParamList.Add("@IsDeleted", IsDeleted);
            }
            CommandDefinition commandDefinition = new(
                                   commandText: sQuery?.ToString(),
                                   parameters: ParamList,
                                   transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                   commandType: CommandType.Text,
                                   cancellationToken: cancellationToken
                               );
            var result = (await cmd.Connection.QueryAsync<MasterCenter>(commandDefinition)).ToList() ?? [];
            return result;

        }

        public async Task<List<Project>> GetProjectAsync(CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();

            string sQuery = "SELECT * FROM PRJ.Project WITH(NOLOCK) WHERE IsDeleted = 0 AND IsActive = 1";

            CommandDefinition commandDefinition = new(
                                   commandText: sQuery,
                                   transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                   commandType: CommandType.Text,
                                   cancellationToken: cancellationToken
                               );

            var result = (await cmd.Connection.QueryAsync<Project>(commandDefinition)).ToList() ?? [];
            return result;

        }

        public async Task<List<ProjectInfoLocation>> GetProjectZoneAsync(Guid? ID, string TextSearch, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            var sQuery = new StringBuilder(@"SELECT * FROM PRJ.ProjectInfoLocation WHERE LocationType = 'normal' AND IsDeleted = 0");
            if (ID.HasValue)
            {
                sQuery.Append(" AND ID = @ID");
                ParamList.Add("@ID", ID);
            }

            if (!string.IsNullOrEmpty(TextSearch))
            {
                sQuery.Append(" AND [Description] LIKE '%' + @TextSearch + '%'");
                ParamList.Add("@TextSearch", TextSearch);
            }

            CommandDefinition commandDefinition = new(
                parameters: ParamList,
                commandText: sQuery?.ToString(),
                transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            );
            var result = (await cmd.Connection.QueryAsync<ProjectInfoLocation>(commandDefinition)).ToList() ?? [];
            return result;

        }

        public async Task<List<USR_vwPMUser>> GetvwPMUserAsync(Guid? ProjectID, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            var sQuery = new StringBuilder(@"SELECT * FROM  USR.vw_PMUser WHERE 1=1");

            if (ProjectID.HasValue)
            {
                sQuery.Append(" AND ProjectID = @ProjectID");
                ParamList.Add("@ProjectID", ProjectID);
            }
            CommandDefinition commandDefinition = new(
                parameters: ParamList,
                commandText: sQuery?.ToString(),
                transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            );
            var result = (await cmd.Connection.QueryAsync<USR_vwPMUser>(commandDefinition)).ToList() ?? [];
            return result;

        }

        public async Task<List<USR_vwLCMUserDetail>> GetvwLCMUserDetailAsync(Guid? ProjectID, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            var sQuery = new StringBuilder(@"SELECT * FROM  USR.vw_LCMUserDetail WHERE 1=1");
            if (ProjectID.HasValue)
            {
                sQuery.Append(" AND ProjectID = @ProjectID");
                ParamList.Add("@ProjectID", ProjectID);
            }
            CommandDefinition commandDefinition = new(
                parameters: ParamList,
                commandText: sQuery?.ToString(),
                transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            );
            var result = (await cmd.Connection.QueryAsync<USR_vwLCMUserDetail>(commandDefinition)).ToList() ?? [];
            return result;

        }

        public async Task<List<USR_vwLCUserDetail>> GetvwLCUserDetailAsync(Guid? ProjectID, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            var sQuery = new StringBuilder(@"SELECT * FROM USR.vw_LCUserDetail WHERE 1=1");

            if (ProjectID.HasValue)
            {
                sQuery.Append(" AND ProjectID = @ProjectID");
                ParamList.Add("@ProjectID", ProjectID);
            }
            CommandDefinition commandDefinition = new(
                commandText: sQuery?.ToString(),
                parameters: ParamList,
                transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            );
            var result = (await cmd.Connection.QueryAsync<USR_vwLCUserDetail>(commandDefinition)).ToList() ?? [];
            return result;

        }

        public async Task<bool> UpdateProjecInfoAsync(List<ProjectInfo> Input)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            try
            {
                await DB.BulkUpdateAsync(Input);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{TAG}.UpdateProjecInfoAsync() :: Error ", ex);
            }
        }

        public async Task<sql_ProjectInfoDetail.Result> GetProjectInfoDetailDataAsync(Guid ProjectID, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();


            var sQuery = new StringBuilder(sql_ProjectInfoDetail.Query);
            sQuery.Append($" AND(prj.ProjectID = @ProjectID)");
            CommandDefinition commandDefinition = new(
                commandText: sQuery?.ToString(),
                parameters: new { ProjectID = ProjectID },
                transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            );
            var Results = (await cmd.Connection.QueryFirstOrDefaultAsync<sql_ProjectInfoDetail.Result>(commandDefinition)) ?? new();
            return Results;

        }

        public async Task<List<ProjectInfoDetail>> GetProjectInfoDestAsync(Guid? ID, Guid? ProjectID, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();


            var sQuery = new StringBuilder("SELECT * FROM PRJ.ProjectInfoDetail WITH(NOLOCK) WHERE IsDeleted = 0");

            if (ID.HasValue)
            {
                sQuery.Append(" AND ID = @ID");
                ParamList.Add("@ID", ID);
            }

            if (ProjectID.HasValue)
            {
                sQuery.Append(" AND ProjectID = @ProjectID");
                ParamList.Add("@ProjectID", ProjectID);
            }
            CommandDefinition commandDefinition = new(
                commandText: sQuery?.ToString(),
                parameters: ParamList,
                transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            );
            var result = (await cmd.Connection.QueryAsync<ProjectInfoDetail>(commandDefinition)).ToList() ?? [];
            return result;

        }
        public async Task<bool> UpdateProjecInfoDetailAsync(List<ProjectInfoDetail> Input)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            try
            {
                await DB.BulkUpdateAsync(Input);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{TAG}.UpdateProjecInfoDetailAsync() :: Error ", ex);
            }
        }

        public async Task<bool> InsertProjecInfoDetailAsync(List<ProjectInfoDetail> Input)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();

            try
            {
                await DB.BulkInsertAsync(Input);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{TAG}.InsertProjecInfoDetailAsync() :: Error ", ex);
            }
        }

        public async Task<List<sql_ProjectInfoBrand.Result>> GetProjectInfoBrandDDLAsync(CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();

            string sQuery = sql_ProjectInfoBrand.Query;
            CommandDefinition commandDefinition = new(
                commandText: sQuery,
                transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            );
            return (await cmd.Connection.QueryAsync<sql_ProjectInfoBrand.Result>(commandDefinition)).ToList() ?? [];
        }
    }
}
