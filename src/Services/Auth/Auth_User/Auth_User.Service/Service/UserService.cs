using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.DbQueries;
using Auth_User.Params.Filters;
using Auth_User.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Database.Models.DbQueries.USR;
using static Database.Models.DbQueries.DBQueryParam;
using Microsoft.Data.SqlClient;
using System.Data;
using Common.Helper.Logging;
using Auth;
using Database.Models.USR;
using NPOI.SS.Formula.Functions;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore.Storage;
using Database.Models.PRJ;
using Database.Models.DbQueries.CMS;
using Database.Models.DbQueries.SAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Auth_User.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext DB;
        private readonly DbQueryContext DBQuery;
        public LogModel logModel { get; set; }
        int Timeout = 300;

        public UserService(DatabaseContext db, DbQueryContext dbQuery)
        {
            DB = db;
            DB.Database.SetCommandTimeout(Timeout);
            DBQuery = dbQuery;
            logModel = new LogModel("UserService", null);
        }

        public async Task<UserPaging> GetUserListAsync(UserFilter filter, PageParam pageParam, UserListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<UserQueryResult> query;
            var projectIDs = filter.AuthorizeProjectIDs?
                .Split(',')
                .Select(o =>
                {
                    Guid.TryParse(o, out Guid guidValue);
                    return guidValue;
                })
                .Where(g => g != Guid.Empty)
                .ToList();

            if (filter.IgnoreQueryFilters)
            {
                query = DB.Users.AsNoTracking().Where(o => (o.EmployeeNo.StartsWith("AP") || o.EmployeeNo.StartsWith("BC")) && (!o.EmployeeNo.StartsWith("APE") && !o.EmployeeNo.StartsWith("AP_") && !o.EmployeeNo.StartsWith("APB") && !o.EmployeeNo.StartsWith("apf"))).IgnoreQueryFilters()
             .Select(o => new UserQueryResult
             {
                 User = o
             });
            }
            else if (filter.IgnoreTemp)
            {
                query = DB.Users.AsNoTracking().Where(o => !o.EmployeeNo.Contains("TE") && (o.EmployeeNo.StartsWith("AP") || o.EmployeeNo.StartsWith("BC")) && (!o.EmployeeNo.StartsWith("APE") && !o.EmployeeNo.StartsWith("AP_") && !o.EmployeeNo.StartsWith("APB") && !o.EmployeeNo.StartsWith("apf")))
              .Select(o => new UserQueryResult
              {
                  User = o
              });
            }
            else if (filter.IgnoreLead)
            {
                using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
                DynamicParameters ParamList = new DynamicParameters();
                ParamList.Add("ProjectID", filter.AuthorizeProjectIDs);

                CommandDefinition commandDefinition = new(
                                             commandText: DBStoredNames.sp_GetLCUserLeadToAssign,
                                             parameters: ParamList,
                                             transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                             cancellationToken: cancellationToken,
                                             commandType: CommandType.StoredProcedure);
                var data = (await cmd.Connection.QueryAsync<UserListDTO>(commandDefinition)).ToList() ?? [];
                data = data.Where(w => w.EmployeeNo.StartsWith("AP") || w.EmployeeNo.StartsWith("BC")).ToList();

                return new UserPaging()
                {
                    PageOutput = new(),
                    Users = data
                };
            }
            else
            {
                query = DB.Users.Where(o => (o.EmployeeNo.StartsWith("AP") || o.EmployeeNo.StartsWith("BC")) && (!o.EmployeeNo.StartsWith("APE") && !o.EmployeeNo.StartsWith("AP_") && !o.EmployeeNo.StartsWith("APB") && !o.EmployeeNo.StartsWith("apf"))).AsNoTracking()
                 .Select(o => new UserQueryResult
                 {
                     User = o
                 });
            }
            #region Filter 
            if (filter.IgnoreUser)
                query = query.Where(q => !q.User.FirstName.Contains("LC") && !q.User.IsDeleted && !q.User.EmployeeNo.Equals("APXXXXXX"));
            else
                query = query.Where(q => !q.User.FirstName.Contains("LC") && !q.User.IsDeleted);

            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(q => q.User.FirstName.Contains(filter.FirstName));

            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(q => q.User.LastName.Contains(filter.LastName));

            if (!string.IsNullOrEmpty(filter.EmployeeNo))
                query = query.Where(q => q.User.EmployeeNo.Contains(filter.EmployeeNo));
            if (!string.IsNullOrEmpty(filter.DisplayName))
                query = query.Where(q => q.User.DisplayName.Contains(filter.DisplayName));
            if (!string.IsNullOrEmpty(filter.RoleCodes))
            {
                var roleCodes = filter.RoleCodes.Split(',');
                query = query.Where(o => o.User.UserRoles.Where(m => roleCodes.Contains(m.Role.Code)).Any());
            }
            if (!string.IsNullOrEmpty(filter.PositionCodes))
            {
                var positionCodes = filter.PositionCodes.Split(',');
                query = query.Where(q => positionCodes.Contains(q.User.PositionCode));
            }
            if (!string.IsNullOrEmpty(filter.AuthorizeProjectIDs))
            {
                query = query.Where(o => o.User.UserAuthorizeProjects.Where(m => projectIDs.Contains(m.ProjectID.Value)).Any());
            }
            #endregion

            UserListDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<UserQueryResult>(pageParam, ref query);
            var queryResults = await query.ToListAsync(cancellationToken);
            var result = queryResults.Select(o => UserListDTO.CreateFromQueryResult(o))?.ToList();
            return new UserPaging()
            {
                PageOutput = pageOutput,
                Users = result
            };
        }

        public async Task<UserPaging> GetUserForProjectListAsync(UserFilter filter, PageParam pageParam, UserListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            Guid.TryParse(filter.AuthorizeProjectIDs, out Guid projectID);
            if (projectID == Guid.Empty) return null;

            var usersLCforProjects = (from l in DB.Leads.AsNoTracking().Where(o => o.ProjectID == projectID && o.OwnerID != null)
                                      join own in DB.Users.AsNoTracking() on l.OwnerID equals own.ID
                                      select l.OwnerID).Distinct();

            var query = await DB.Users.AsNoTracking().Where(o => usersLCforProjects.Contains(o.ID)).ToListAsync(cancellationToken);

            query = query.Where(q => !q.FirstName.Contains("LC") && !q.IsDeleted).ToList();

            #region Filter
            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(q => q.FirstName.Contains(filter.FirstName)).ToList();

            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(q => q.LastName.Contains(filter.LastName)).ToList();

            if (!string.IsNullOrEmpty(filter.EmployeeNo))
                query = query.Where(q => q.EmployeeNo.Contains(filter.EmployeeNo)).ToList();

            if (!string.IsNullOrEmpty(filter.DisplayName))
                query = query.Where(q => q.DisplayName.Contains(filter.DisplayName)).ToList();
            #endregion

            var result = query.Select(o => UserListDTO.CreateFromModel(o))?.ToList();

            return new UserPaging()
            {
                Users = result
            };
        }

        public async Task<List<UserListDTO>> GetUserDropdownListAsync(string name, CancellationToken cancellationToken = default)
        {
            var query = DB.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.FirstName.Contains(name) || o.LastName.Contains(name));
            }

            var results = await query.Take(100).Select(o => UserListDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
            return results;
        }
        public async Task<bool> SyncUserRoleByEmpCodeAsync(string Empcode, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("EmpCode", Empcode);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.sp_Sync_UserRoleProjectByEmpCode,
                                         cancellationToken: cancellationToken,
                                         parameters: ParamList,
                                         commandTimeout: Timeout,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            await cmd.Connection.ExecuteAsync(commandDefinition);
            return true;
        }
        public async Task<List<UserListDTO>> GetUserLCDropdownListAsync(string name, CancellationToken cancellationToken = default)
        {
            var lcRoleId = await DB.Roles.AsNoTracking().Where(o => o.Code == "LC").Select(o => o.ID).FirstAsync();
            var query = DB.UserRoles.AsNoTracking().Include(o => o.User)
                .Where(o => o.RoleID == lcRoleId).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.User.FirstName.Contains(name) || o.User.LastName.Contains(name));
            }

            var results = await query.Take(100).Select(o => UserListDTO.CreateFromModel(o.User)).ToListAsync(cancellationToken);
            return results;
        }

        public async Task SyncUserFromAuthAsync(CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            CommandDefinition commandDefinition = new(
                                         commandText: "dbo.sp_Sync_UserRoleProject",
                                         cancellationToken: cancellationToken,
                                         //  commandTimeout: Timeout,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            await cmd.Connection.ExecuteReaderAsync(commandDefinition);
        }


        public async Task<List<UserListDTO>> GetKCashCardUserByProjectAsync(Guid? projectID, string textFilter, CancellationToken cancellationToken = default)
        {
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("ProjectID", projectID);
            ParamList.Add("TextFilter", textFilter);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.sp_UserKCashCard,
                                         cancellationToken: cancellationToken,
                                         parameters: ParamList,
                                         commandTimeout: Timeout,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            var queryResult = await cmd.Connection.QueryAsync<dbqKCashCardUserList>(commandDefinition);
            return queryResult.Select(o => UserListDTO.CreateFromModel(o))?.ToList();
        }

        public async Task<UserListDTO> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
        {

            var query = await DB.Users.AsNoTracking().FirstOrDefaultAsync(o => o.ID == id, cancellationToken);

            var result = UserListDTO.CreateFromModel(query);

            return result;
        }
        public async Task<JsonWebToken> GetUserAppPermissionAsync(Guid userid, CancellationToken cancellationToken = default)
        {
            var result = new JsonWebToken();
            var user = await DB.Users.AsNoTracking().FirstOrDefaultAsync(o => o.ID == userid, cancellationToken) ?? new User();
            var userRoles = await DB.UserRoles.AsNoTracking().Where(o => o.UserID == userid).Select(o => o.RoleID).ToListAsync(cancellationToken);
            var ListMenuAPP = new List<string>() { "CRM-Sale", "CRM-BackOffice", "CRM-Master" };
            var APPPermission = await DB.MenuPermissions
                                     .Include(o => o.MenuAction)
                                     .ThenInclude(o => o.Menu)
                                     .Where(o => userRoles.Contains(o.RoleID) && ListMenuAPP.Contains(o.MenuAction.Menu.MenuCode) && o.IsDeleted == false)
                                     .Select(o => o.MenuAction.Menu.MenuCode).Distinct().ToListAsync(cancellationToken);
            var APPPermissionName = new List<string>();
            foreach (var item in APPPermission)
            {
                if (item.Equals("CRM-Sale"))
                {
                    APPPermissionName.Add("Sale");
                }
                else if (item.Equals("CRM-BackOffice"))
                {
                    APPPermissionName.Add("BackOffice");
                }
                else if (item.Equals("CRM-Master"))
                {
                    APPPermissionName.Add("MasterData");
                }
            }
            if (user?.DefaultRoleID != null)
            {
                result.DefaultAPP = (await DB.Roles.FirstOrDefaultAsync(x => x.ID == user.DefaultRoleID, cancellationToken))?.DefaultApp;
                // กรณี Defailt Role ไม่มี Default App ให้เอา App ที่มีสิทธิ์เข้าถึงมากที่สุด
                if (string.IsNullOrEmpty(result.DefaultAPP))
                {
                    if (APPPermissionName.Any())
                    {
                        result.DefaultAPP = APPPermissionName.FirstOrDefault();
                        var role = await DB.Roles.FirstOrDefaultAsync(x => x.ID == user.DefaultRoleID, cancellationToken);
                        if (role != null)
                        {
                            role.DefaultApp = result.DefaultAPP;
                            await DB.SaveChangesAsync();
                        }
                    }
                }
            }
            else
            {
                if (APPPermissionName.Any())
                {
                    result.DefaultAPP = APPPermissionName.FirstOrDefault();
                }
            }
            result.APPPermission = APPPermissionName;
            // insert RefreshToken
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString("N"),
                UserID = userid,
                ExpireDate = DateTime.Now.AddDays(14)
            };
            DB.RefreshTokens.Add(refreshToken);
            await DB.SaveChangesAsync();
            result.user_id = userid;
            return result;
        }

        public async Task<List<UserListDTO>> GetSaleUserProjectInZoneAsync(Guid ProjectId, bool ignoreQueryFilters, string SaleUserFullName, CancellationToken cancellationToken = default)
        {
            var ZoneProjectIDs = new List<Guid>();

            var ProjectData = await DB.Projects
                    .Include(o => o.BG)
                    .Include(o => o.Zone)
                    .Where(o => o.ID == ProjectId).FirstOrDefaultAsync() ?? new Project();

            if (ProjectData.BG?.BGNo == "2" && ProjectData.ZoneID.HasValue)
                ZoneProjectIDs = await DB.Projects.Where(o => o.ZoneID == ProjectData.ZoneID).Select(o => o.ID).ToListAsync();

            string sqlQuery = sqlSaleUserProjectInZone.QueryString;
            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = sqlSaleUserProjectInZone.QueryFilter(ref sqlQuery, ProjectId, ZoneProjectIDs, ignoreQueryFilters, SaleUserFullName);

            sqlQuery = sqlSaleUserProjectInZone.QueryOrder(ref sqlQuery);
            CommandDefinition commandDefinition = new(
                                         commandText: sqlQuery,
                                         cancellationToken: cancellationToken,
                                         parameters: ParamList,
                                         commandTimeout: Timeout,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.Text);


            var RateSettingSalesQuery = (await cmd.Connection.QueryAsync<sqlSaleUserProjectInZone.QueryResult>(commandDefinition))?.ToList() ?? [];

            var results = RateSettingSalesQuery.Select(o => UserListDTO.CreateFromSQLQueryResult(o)).ToList() ?? new List<UserListDTO>();

            return results.OrderBy(o => o.DisplayName).ToList();
        }

        public async Task<List<UserListDTO>> GetPCardUserByProjectAsync(Guid? transferId)
        {
            var param = string.Format(@"@TransferID = '{0}'", transferId);

            string strQuery = string.Format("EXEC {0} {1}", DBStoredNames.sp_GetListUserKCashCard, param);

            var query = await DBQuery.dbqPCardUserLists.FromSqlRaw(strQuery).ToListAsync();

            var queryResult = query ?? new List<dbqPCardUserList>();

            var result = queryResult.Select(o => UserListDTO.CreateFromModel(o)).ToList();

            return result;

        }
    }
}
