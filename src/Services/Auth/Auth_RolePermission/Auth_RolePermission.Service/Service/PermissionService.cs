using Auth_RolePermission.Params.Filters;
using Auth_RolePermission.Params.Outputs;
using Base.DTOs.IDT;
using Common;
using Common.Helper.Logging;
using Dapper;
using Database.Models;
using Database.Models.DbQueries;
using Database.Models.DbQueries.IDT;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Database.Models.DbQueries.DBQueryParam;

namespace Auth_RolePermission.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }
        int Timeout = 300;
        private readonly IDistributedCache _cache;
        public PermissionService(DatabaseContext db = null, IDistributedCache cache = null)
        {
            logModel = new LogModel("PermissionService", null);
            DB = db;
            DB.Database.SetCommandTimeout(Timeout);
            _cache = cache;
            // if (DBQuery != null)
            // {
            //     DBQuery.Database.SetCommandTimeout(Timeout);
            // }
        }

        public async Task<Guid?> ChangeUserRoleAsync(Guid UserID, Guid RoleID, CancellationToken cancellationToken = default)
        {
            if (UserID == Guid.Empty && UserID == Guid.Empty)
                return null;

            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("UserID", UserID);
            ParamList.Add("RoleID", RoleID);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.spChangeUserRole,
                                         parameters: ParamList,
                                         commandTimeout: Timeout,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            var queryResult = await cmd.Connection.QueryFirstOrDefaultAsync<dbqChangeUserRoleResult>(commandDefinition) ?? new();
            var result = queryResult.ResultID;

            if (!result.HasValue)
            {
                ValidateException ex = new ValidateException();
                ex.AddError("ERR9999", "ไม่พบ Role ใน User ที่เลือก", 1);
                throw ex;
            }
            return result;
        }

        public async Task<List<UserRoleDTO>> GetUserRolesAsync(Guid UserID, string EmployeeNo, CancellationToken cancellationToken = default)
        {
            if (UserID == Guid.Empty && string.IsNullOrEmpty(EmployeeNo))
                return new List<UserRoleDTO>();

            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("UserID", UserID);
            ParamList.Add("EmployeeNo", EmployeeNo);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.spGetUserRole,
                                         cancellationToken: cancellationToken,
                                         parameters: ParamList,
                                         commandTimeout: Timeout,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            var queryResult = await cmd.Connection.QueryAsync<dbqGetUserRole>(commandDefinition);
            var result = queryResult.Select(o => UserRoleDTO.CreateFromDBQuery(o))?.ToList() ?? [];

            return result;
        }

        public async Task<List<UserMenuDTO>> GetUserMenuAsync(Guid UserID, Guid? RoleID, CancellationToken cancellationToken = default)
        {

            if (UserID == Guid.Empty)
                return new List<UserMenuDTO>();

            // 1) หา roleVersion ก่อน (ถ้า null ให้ใช้ "0")
            var roleVersion = await GetRoleVersionAsync(RoleID, cancellationToken);
            var cacheKey = GetUserMenuCacheKey(UserID, RoleID, roleVersion);

            // 2) ลองอ่านจาก cache
            var cachedJson = await _cache.GetStringAsync(cacheKey, cancellationToken);
            if (!string.IsNullOrEmpty(cachedJson))
            {
                Console.WriteLine($"Cache hit: {cacheKey}");
                var cachedResult = JsonConvert.DeserializeObject<List<UserMenuDTO>>(cachedJson);
                if (cachedResult != null)
                    return cachedResult;
            }

            Console.WriteLine($"Cache miss: {cacheKey}");

            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("UserID", UserID);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.spGetUserMenu,
                                         cancellationToken: cancellationToken,
                                         parameters: ParamList,
                                         commandTimeout: 30,
                                         transaction: null, // Read-only: no transaction
                                         commandType: CommandType.StoredProcedure,
                                         flags: CommandFlags.None); // No cache, read-only
            var queryResult = await cmd.Connection.QueryAsync<dbqGetUserMenu>(commandDefinition);
            var result = queryResult.Select(o => UserMenuDTO.CreateFromDBQuery(o))?.ToList() ?? new List<UserMenuDTO>();

            // 3) เซ็ต cache กลับไป (30 นาที)
            var json = JsonConvert.SerializeObject(result);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };
            await _cache.SetStringAsync(cacheKey, json, options, cancellationToken);

            return result;

        }

        public async Task<List<UserMenuActionsDTO>> GetUserMenuActionsAsync(Guid UserID, Guid? RoleID, CancellationToken cancellationToken = default)
        {
            if (UserID == Guid.Empty)
                return new List<UserMenuActionsDTO>();

            string cacheKey = string.Empty;
            // 1) หา roleVersion ก่อน (ถ้า null ให้ใช้ "0")
            if (RoleID != null) { 
                var roleVersion = await GetRoleVersionAsync(RoleID, cancellationToken);
                 cacheKey = GetUserMenuActionsCacheKey(UserID, RoleID, roleVersion);

                // 2) ลองอ่านจาก cache
                var cachedJson = await _cache.GetStringAsync(cacheKey, cancellationToken);
                if (!string.IsNullOrEmpty(cachedJson))
                {
                    Console.WriteLine($"Cache hit: {cacheKey}");
                    var cachedResult = JsonConvert.DeserializeObject<List<UserMenuActionsDTO>>(cachedJson);
                    if (cachedResult != null)
                        return cachedResult;
                }
                Console.WriteLine($"Cache miss: {cacheKey}");
            }

            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("UserID", UserID);
            ParamList.Add("RoleID", RoleID);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.spGetUserMenuAction,
                                         cancellationToken: cancellationToken,
                                         parameters: ParamList,
                                         commandTimeout: 30,
                                         transaction: null, // Read-only: no transaction
                                         commandType: CommandType.StoredProcedure,
                                         flags: CommandFlags.None); // No cache, read-only
            var query = await cmd.Connection.QueryAsync<dbqGetUserMenuAction>(commandDefinition);
            var queryResult = query
                .GroupBy(o => new { o.UserID, o.RoleID, o.UserRoleID, o.ModuleID, o.MenuID, o.MenuCode })
                .Select(p => new UserMenuActionQueryResult
                {
                    UserID = p.Key.UserID,
                    RoleID = p.Key.RoleID,
                    UserRoleID = p.Key.UserRoleID,
                    ModuleID = p.Key.ModuleID,
                    MenuID = p.Key.MenuID,
                    MenuCode = p.Key.MenuCode,
                    MenuActionPermissionQueryResult =
                        p.Select(o => new MenuActionPermission
                        {
                            MenuPermissionID = o.MenuPermissionID,
                            MenuActionID = o.MenuActionID,
                            MenuActionCode = o.MenuActionCode,
                            MenuActionName = o.MenuActionName,
                            MenuActionOrder = o.MenuActionOrder ?? 0
                        }).ToList()
                }
            )?.ToList() ?? new();


            var result = queryResult.Select(o => UserMenuActionsDTO.CreateFromQueryResult(o))?.ToList() ?? new();

            // 4) เซ็ต cache กลับไป
            var json = JsonConvert.SerializeObject(result);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };
            if (RoleID != null)
            {
                await _cache.SetStringAsync(cacheKey, json, options, cancellationToken);
            }
            return result;
        }
        public async Task<List<UserMenuActionsDTO>> GetUserMenuActionsAsync2(Guid UserID, Guid? RoleID, CancellationToken cancellationToken = default)
        {
            if (UserID == Guid.Empty)
                return new List<UserMenuActionsDTO>();

            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("UserID", UserID);
            ParamList.Add("RoleID", RoleID);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.spGetUserMenuAction,
                                         cancellationToken: cancellationToken,
                                         parameters: ParamList,
                                         commandTimeout: Timeout,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            var query = await cmd.Connection.QueryAsync<dbqGetUserMenuAction>(commandDefinition);
            var queryResult = query
                .GroupBy(o => new { o.UserID, o.RoleID, o.UserRoleID, o.ModuleID, o.MenuID, o.MenuCode })
                .Select(p => new UserMenuActionQueryResult
                {
                    UserID = p.Key.UserID,
                    RoleID = p.Key.RoleID,
                    UserRoleID = p.Key.UserRoleID,
                    ModuleID = p.Key.ModuleID,
                    MenuID = p.Key.MenuID,
                    MenuCode = p.Key.MenuCode,
                    MenuActionPermissionQueryResult =
                        p.Select(o => new MenuActionPermission
                        {
                            MenuPermissionID = o.MenuPermissionID,
                            MenuActionID = o.MenuActionID,
                            MenuActionCode = o.MenuActionCode,
                            MenuActionName = o.MenuActionName,
                            MenuActionOrder = o.MenuActionOrder ?? 0
                        }).OrderBy(o => o.MenuActionCode).ToList()
                }
            )?.ToList() ?? new();


            var result = queryResult.Select(o => UserMenuActionsDTO.CreateFromQueryResult(o))?.ToList() ?? new();

            return result;
        }

        #region Get Permission List

        public async Task<List<RoleDropdownDTO>> GetRoleDropdownListAsync(string DisplayName, CancellationToken cancellationToken = default)
        {
            IQueryable<Role> query = null;

            query = DB.Roles.AsNoTracking()
                    .Include(o => o.CreatedBy)
                    .Include(o => o.UpdatedBy);

            if (!string.IsNullOrEmpty(DisplayName))
                query = query.Where(o => o.Code.ToLower().Contains(DisplayName.ToLower()) || o.Name.ToLower().Contains(DisplayName.ToLower()));

            var queryResults = await query.OrderBy(o => o.Code).ThenBy(o => o.Name).Take(100).ToListAsync(cancellationToken);

            var results = queryResults.Select(o => RoleDropdownDTO.CreateFromModel(o))?.ToList();

            return results;
        }

        public async Task<List<ModuleDropdownDTO>> GetModuleDropdownListAsync(string DisplayName, CancellationToken cancellationToken = default)
        {
            IQueryable<Module> query = DB.Modules.AsNoTracking()
                                        .Include(o => o.CreatedBy)
                                        .Include(o => o.UpdatedBy);

            if (!string.IsNullOrEmpty(DisplayName))
                query = query.Where(o => o.Code.ToLower().Contains(DisplayName.ToLower()) || o.NameTH.ToLower().Contains(DisplayName.ToLower()));

            var queryResults = await query.OrderBy(o => o.Code).ThenBy(o => o.NameTH).Take(100).ToListAsync(cancellationToken);

            var results = queryResults.Select(o => ModuleDropdownDTO.CreateFromModel(o))?.ToList();

            return results;
        }

        public async Task<List<MenuDropdownDTO>> GetMenuDropdownListAsync(List<Guid?> ModuleIDs, string DisplayName, CancellationToken cancellationToken = default)
        {
            IQueryable<Menu> query = null;

            query = DB.Menus.AsNoTracking()
                    .Include(o => o.Module)
                    .Include(o => o.CreatedBy)
                    .Include(o => o.UpdatedBy);

            if (!string.IsNullOrEmpty(DisplayName))
                query = query.Where(o => o.MenuCode.ToLower().Contains(DisplayName.ToLower()) || o.MenuNameTH.ToLower().Contains(DisplayName.ToLower()));

            if ((ModuleIDs ?? []).Any())
                query = query.Where(o => ModuleIDs.Contains(o.ModuleID));

            var queryResults = await query.OrderBy(o => o.MenuCode).ThenBy(o => o.MenuNameTH).Take(100).ToListAsync(cancellationToken);

            var results = queryResults.Select(o => MenuDropdownDTO.CreateFromModel(o))?.ToList();

            return results;
        }

        public async Task<List<MenuActionDropdownDTO>> GetMenuActionDropdownListAsync(List<Guid?> ModuleIDs, List<Guid?> MenuIDs, string DisplayName, CancellationToken cancellationToken = default)
        {
            IQueryable<MenuAction> query = null;

            query = DB.MenuActions.AsNoTracking()
                    .Include(o => o.Menu)
                        .ThenInclude(o => o.Module)
                    .Include(o => o.CreatedBy)
                    .Include(o => o.UpdatedBy);

            if (!string.IsNullOrEmpty(DisplayName))
                query = query.Where(o => o.MenuActionCode.ToLower().Contains(DisplayName.ToLower()) || o.MenuActionName.ToLower().Contains(DisplayName.ToLower()));

            if ((ModuleIDs ?? new List<Guid?>()).Any())
                query = query.Where(o => ModuleIDs.Contains(o.Menu.ModuleID));

            if ((MenuIDs ?? new List<Guid?>()).Any())
                query = query.Where(o => MenuIDs.Contains(o.MenuID));

            var queryResults = await query.OrderBy(o => o.Order).Take(100).ToListAsync(cancellationToken);

            var results = queryResults.Select(o => MenuActionDropdownDTO.CreateFromModel(o))?.ToList();

            return results;
        }

        #endregion

        public async Task<PermissionByRolePaging> GetPermissionByRoleAsync(PermissionByRoleFilter filter, PageParam pageParam, sqlMenuAction.PermissionByRoleSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            var Result = new PermissionByRolePaging();

            filter = filter ?? new PermissionByRoleFilter();
            pageParam = pageParam ?? new PageParam { Page = 1, PageSize = 10 };

            int iPage = pageParam.Page ?? 1;
            int iPageSize = pageParam.PageSize ?? 10;

            #region MenuActionPermissions

            string MenuActionPermission = sqlMenuAction.QueryString;

            DynamicParameters ParamListMenuActionPermissions = sqlMenuAction.QueryFilter(ref MenuActionPermission, filter.ModuleIDs, filter.MenuIDs, filter.MenuActionIDs);
            sqlMenuAction.QueryOrder(ref MenuActionPermission, sortByParam);

            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            CommandDefinition commandDefinition = new(
                                         commandText: MenuActionPermission,
                                         cancellationToken: cancellationToken,
                                         parameters: ParamListMenuActionPermissions,
                                         commandTimeout: Timeout,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.Text);
            var MenuActionPermissionResult = (await cmd.Connection.QueryAsync<sqlMenuAction.QueryResult>(commandDefinition))?.ToList() ?? new();

            var TotalRecord = MenuActionPermissionResult.Count;
            var PageCount = (TotalRecord == 0) ? 0 : (int)Math.Floor(TotalRecord / (decimal)(pageParam.PageSize ?? 10));

            int iSkipRow = (iPage - 1 == 0) ? 0 : (iPage - 1) * iPageSize;

            MenuActionPermissionResult = MenuActionPermissionResult.Skip(iSkipRow).Take(pageParam.PageSize ?? 10).ToList();

            var MenuActionPermissionModel = MenuActionPermissionResult.Select(o => MenuActionDTO.CreateFromModel(o)).ToList();

            List<string> columnNames = new List<string>();

            var MenuActionPermissionTable = MenuActionPermissionModel.CreateDataTableFromModel(ref columnNames);

            #endregion

            if (MenuActionPermissionResult.Any())
            {
                #region RolePermission

                var MenuActionIDs = MenuActionPermissionResult.Select(o => o.MenuActionID ?? Guid.Empty).ToList();

                string RolePermission = sqlRolePermission.QueryString;
                DynamicParameters ParamListRolePermission = sqlRolePermission.QueryFilter(ref RolePermission, filter.RoldIDs, MenuActionIDs);
                sqlRolePermission.QueryOrder(ref RolePermission);


                CommandDefinition commandDefinition2 = new(
                                             commandText: RolePermission,
                                             cancellationToken: cancellationToken,
                                             parameters: ParamListRolePermission,
                                             commandTimeout: Timeout,
                                             transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                             commandType: CommandType.Text);
                var RolePermissionResult = (await cmd.Connection.QueryAsync<sqlRolePermission.QueryResult>(commandDefinition2))?.ToList() ?? new();

                #endregion

                var RoleCodeList = RolePermissionResult.Select(o => o.RoleCode).Distinct().ToList();

                columnNames.AddRange(RoleCodeList);
                if (RoleCodeList?.Count > 0)
                {
                    foreach (var roleColumn in RoleCodeList)
                    {
                        MenuActionPermissionTable?.Columns.Add(new DataColumn(roleColumn?.ToLower(), typeof(bool)));

                        var col = MenuActionPermissionTable.Columns[roleColumn];
                        foreach (DataRow row in MenuActionPermissionTable.Rows)
                            row[roleColumn] = false;
                    }
                }
                var RolePermissionResultList = RolePermissionResult.Where(o => o.MenuActionID.HasValue).ToList();
                if (RolePermissionResultList?.Count > 0)
                {
                    foreach (var rolePer in RolePermissionResultList)
                    {
                        // Update
                        string RoleCode = rolePer.RoleCode;
                        DataRow dr = MenuActionPermissionTable.Select("Id = '" + rolePer.MenuActionID.ToString() + "'").FirstOrDefault();
                        if (dr != null)
                        {
                            dr[RoleCode] = true;
                        }
                    }
                }

                Result = new PermissionByRolePaging
                {
                    PermissionByRole = new PermissionByRoleDTO
                    {
                        PermissionByRoleHeader = columnNames,
                        PermissionByRoleDetail = MenuActionPermissionTable
                    },
                    PageOutput = new PageOutput
                    {
                        Page = pageParam.Page ?? 0,
                        PageSize = pageParam.PageSize ?? 0,
                        PageCount = PageCount,
                        RecordCount = TotalRecord
                    }
                };
            }

            return Result;
        }

        public async Task UpdatePermissionByRoleAsync(UpdatePermissionByRoleDTO input, Guid? userID)
        {
            if (!string.IsNullOrEmpty(input.RoleCode) && input.MenuActionID.HasValue && input.IsActive.HasValue)
            {

                using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
                DynamicParameters ParamList = new DynamicParameters();
                ParamList.Add("RoleCode", input.RoleCode);
                ParamList.Add("MenuActionID", input.MenuActionID);
                ParamList.Add("Allow", input.IsActive);
                ParamList.Add("UserID", userID);

                CommandDefinition commandDefinition = new(
                                             commandText: "USR.sp_SetMenuPermission",
                                             parameters: ParamList,
                                             commandTimeout: Timeout,
                                             transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                             commandType: CommandType.StoredProcedure);
                await cmd.Connection.ExecuteAsync(commandDefinition);

                // 2) invalidate cache ทั้ง role
                // get RoldID
                var roleId = await DB.Roles.Where(o => o.Code == input.RoleCode).AsTracking().Select(o => o.ID).FirstOrDefaultAsync();
                await InvalidateRoleMenuCacheAsync(roleId);
            }
        }


        #region Dash Board
        public async Task<List<UserDashboardMenuDTO>> GetUserDashboardAsync(Guid UserID, string MasterApp, CancellationToken cancellationToken = default)
        {
            if (UserID == Guid.Empty && string.IsNullOrEmpty(MasterApp))
                return new List<UserDashboardMenuDTO>();

            using DbCommand cmd = DB.Database.GetDbConnection().CreateCommand();
            DynamicParameters ParamList = new DynamicParameters();
            ParamList.Add("UserID", UserID);
            ParamList.Add("MasterApp", MasterApp);

            CommandDefinition commandDefinition = new(
                                         commandText: DBStoredNames.spGetUserDashboardMenu,
                                         parameters: ParamList,
                                         cancellationToken: cancellationToken,
                                         commandTimeout: Timeout,
                                         transaction: DB?.Database?.CurrentTransaction?.GetDbTransaction(),
                                         commandType: CommandType.StoredProcedure);
            var queryResult = (await cmd.Connection.QueryAsync<dbqGetUserDashboardMenu>(commandDefinition))?.ToList() ?? new();
            var result = queryResult.Select(o => UserDashboardMenuDTO.CreateFromDBQuery(o))?.ToList() ?? [];

            return result;
        }
        #endregion

        private async Task<string> GetRoleVersionAsync(Guid? roleId, CancellationToken token)
        {
            // role อาจเป็น null → ใช้ "global" แทน
            var roleKey = roleId.HasValue ? roleId.Value.ToString() : "global";
            var versionKey = $"role_menu_version:{roleKey}";

            var version = await _cache.GetStringAsync(versionKey, token);
            return string.IsNullOrEmpty(version) ? "0" : version;
        }

        private static string GetUserMenuCacheKey(Guid userId, Guid? roleId, string roleVersion)
        {
            var rolePart = roleId?.ToString() ?? "global";
            return $"user_menu:{userId}:{rolePart}:v{roleVersion}";
        }

        private static string GetUserMenuActionsCacheKey(Guid userId, Guid? roleId, string roleVersion)
        {
            var rolePart = roleId?.ToString() ?? "global";
            return $"user_menu_actions:{userId}:{rolePart}:v{roleVersion}";
        }
        public async Task InvalidateRoleMenuCacheAsync(Guid? roleId, CancellationToken token = default)
        {
            var roleKey = roleId.HasValue ? roleId.Value.ToString() : "global";
            var versionKey = $"role_menu_version:{roleKey}";

            // ใช้อะไรก็ได้ที่ “เปลี่ยนไปแน่นอน” เช่น Guid ใหม่
            var newVersion = Guid.NewGuid().ToString("N");

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7) // หรือยาวกว่านี้ก็ได้
            };

            await _cache.SetStringAsync(versionKey, newVersion, options, token);
        }
    }
}
