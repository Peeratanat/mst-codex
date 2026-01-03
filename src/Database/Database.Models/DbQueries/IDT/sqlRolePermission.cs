using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Database.Models.DbQueries.IDT
{
    public static class sqlRolePermission
    {
        public static string QueryString = @"
            SELECT 'RoleID' = rol.ID
			    , 'RoleCode' = rol.Code
			    , 'RoleName' = rol.Name
			    , 'MenuActionID' = menuAct.ID
			    , 'MenuActionCode' = menuAct.MenuActionCode
			    , 'MenuActionName' = menuAct.MenuActionName
		    FROM USR.Role rol
		    LEFT JOIN USR.MenuPermission menuPer ON menuPer.RoleID = rol.ID AND menuPer.IsDeleted = 0
		    LEFT JOIN USR.MenuAction menuAct ON menuAct.ID = menuPer.MenuActionID AND menuAct.IsDeleted = 0
		    WHERE rol.IsDeleted = 0";

        public static DynamicParameters QueryFilter(ref string QueryString, List<Guid> RoleIDs, List<Guid> MenuActionIDs)
        {
            DynamicParameters ParamList = new DynamicParameters();
            if ((RoleIDs ?? new List<Guid>()).Any())
            {
                for (var i = 1; i <= RoleIDs.Count; i++)
                {
                    ParamList.Add($"RoleID{i}", RoleIDs[i - 1]);
                }
                var inClause = string.Join(",", RoleIDs.Select((_, index) => $"@RoleID{index + 1}"));
                QueryString += $" AND rol.ID IN ({inClause})";
            }

            return ParamList;
        }

        public static string QueryOrder(ref string QueryString)
        {
            QueryString += " ORDER BY rol.Code, rol.Name";

            return QueryString;
        }

        public class QueryResult
        {
            public Guid? RoleID { get; set; }
            public string RoleCode { get; set; }
            public string RoleName { get; set; }

            public Guid? MenuActionID { get; set; }
            public string MenuActionCode { get; set; }
            public string MenuActionName { get; set; }
        }

    }
}


