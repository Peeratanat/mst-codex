using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Database.Models.DbQueries.IDT
{
    public static class sqlMenuAction
    {
        public static string QueryString = @"
           	SELECT 'MenuActionID' = menuAc.ID
		        , 'MenuActionCode' = menuAc.MenuActionCode
		        , 'MenuActionName' = menuAc.MenuActionName
		        , 'MenuID' = menu.ID
		        , 'MenuCode' = menu.MenuCode
		        , 'MenuNameTH' = menu.MenuNameTH
		        , 'MenuNameEN' = menu.MenuNameTH
		        , 'ModuleID' = module.ID
		        , 'ModuleCode' = module.Code
		        , 'ModuleNameTH' = module.NameTH
		        , 'ModuleNameEN' = module.NameEng
	        FROM USR.MenuAction menuAc WITH (NOLOCK)
	        LEFT JOIN USR.Menu menu WITH (NOLOCK) ON menu.ID = menuAc.MenuID
	        LEFT JOIN USR.Module module WITH (NOLOCK) ON module.ID = menu.ModuleID
	        WHERE menuAc.IsDeleted = 0
		        AND menu.IsDeleted = 0
		        AND module.IsDeleted = 0";

        public static DynamicParameters QueryFilter(ref string QueryString, List<Guid> ModuleIDs, List<Guid> MenuIDs, List<Guid> MenuActionIDs)
        {
            DynamicParameters ParamList = new DynamicParameters();


            if ((ModuleIDs ?? new List<Guid>()).Any())
            {
                for (var i = 1; i <= ModuleIDs.Count; i++)
                {
                    ParamList.Add($"ModuleID{i}", ModuleIDs[i - 1]);
                }
                var inClause = string.Join(",", ModuleIDs.Select((_, index) => $"@ModuleID{index + 1}"));
                QueryString += $" AND module.ID IN ({inClause})";
            }

            if ((MenuIDs ?? new List<Guid>()).Any())
            {
                for (var i = 1; i <= MenuIDs.Count; i++)
                {
                    ParamList.Add($"MenuID{i}", MenuIDs[i - 1]);
                }
                var inClause = string.Join(",", MenuIDs.Select((_, index) => $"@MenuID{index + 1}"));
                QueryString += $" AND menu.ID IN ({inClause})";
            }
            if ((MenuActionIDs ?? new List<Guid>()).Any())
            {
                for (var i = 1; i <= MenuActionIDs.Count; i++)
                {
                    ParamList.Add($"MenuActionID{i}", MenuActionIDs[i - 1]);
                }
                var inClause = string.Join(",", MenuActionIDs.Select((_, index) => $"@MenuActionID{index + 1}"));
                QueryString += $" AND menuAc.ID IN ({inClause})";
            }
            return ParamList;
        }

        public static string QueryOrder(ref string QueryString, PermissionByRoleSortByParam sortByParam, int Page = 0, int PageSize = 0)
        {
            var sortby = "module.Code";
            bool sort = sortByParam.Ascending;
            if (sortByParam?.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case PermissionByRoleSortBy.Module:
                        sortby = string.Format("module.Code {0}, menu.MenuCode, menuAc.MenuActionCode", sort ? "ASC" : "DESC");
                        break;
                    case PermissionByRoleSortBy.Menu:
                        sortby = string.Format("menu.MenuCode {0}, menuAc.MenuActionCode, module.Code", sort ? "ASC" : "DESC");
                        break;
                    case PermissionByRoleSortBy.MenuAction:
                        sortby = string.Format("menuAc.MenuActionCode {0}, module.Code, menu.MenuCode", sort ? "ASC" : "DESC");
                        break;
                    default:
                        sortby = string.Format("module.Code {0}, menu.MenuCode, menuAc.MenuActionCode", sort ? "ASC" : "DESC");
                        break;
                }
            }
            else
            {
                sortby = string.Format("module.Code {0}, menu.MenuCode, menuAc.MenuActionCode", sort ? "ASC" : "DESC");
            }

            QueryString += " ORDER BY " + sortby;

            if (Page > 0 && PageSize > 0)
                QueryString += $" OFFSET({Page.ToString()} * {@PageSize.ToString()}) ROWS FETCH FIRST {PageSize.ToString()} ROWS ONLY";

            return QueryString;
        }

        public class QueryResult
        {
            public Guid? MenuActionID { get; set; }
            public string MenuActionCode { get; set; }
            public string MenuActionName { get; set; }

            public Guid? MenuID { get; set; }
            public string MenuCode { get; set; }
            public string MenuNameTH { get; set; }
            public string MenuNameEN { get; set; }

            public Guid? ModuleID { get; set; }
            public string ModuleCode { get; set; }
            public string ModuleNameTH { get; set; }
            public string ModuleNameEN { get; set; }
        }

        public class PermissionByRoleSortByParam
        {
            public PermissionByRoleSortBy? SortBy { get; set; }
            public bool Ascending { get; set; } = true;
        }

        public enum PermissionByRoleSortBy
        {
            Menu,
            Module,
            MenuAction
        }

    }
}


