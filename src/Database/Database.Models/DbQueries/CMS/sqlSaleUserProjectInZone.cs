using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace Database.Models.DbQueries.CMS
{
    public class sqlSaleUserProjectInZone
    {
        public static string QueryString = @"        
            SELECT DISTINCT U.ID
               , U.EmployeeNo
               , U.FirstName
               , U.LastName
               , U.DisplayName
               , U.PhoneNo
             FROM USR.[User] U
             LEFT OUTER JOIN USR.UserAuthorizeProject UP ON UP.UserID = U.ID
             LEFT OUTER JOIN USR.UserRole UR ON UR.UserID = U.ID
             LEFT OUTER JOIN USR.[Role] R ON R.ID = UR.RoleID
             WHERE 1 = 1
                AND R.Code = 'LC'
                AND U.EmployeeNo NOT LIKE 'TEST%'
			    #IsDeleted 
			    #ZoneProjectIDs
			    #SaleUserFullName
             UNION
             SELECT DISTINCT U.ID
               , U.EmployeeNo
               , U.FirstName
               , U.LastName
               , U.DisplayName
               , U.PhoneNo
             FROM USR.[User] U
             LEFT OUTER JOIN SAL.Booking B ON B.ProjectSaleUserID = U.ID
             WHERE B.IsDeleted = 0 AND U.IsDeleted = 0
			    AND B.ProjectID = @ProjectID
			    #SaleUserFullName";

        public static string QueryStringOrder = @" ORDER BY U.EmployeeNo ";

        public static DynamicParameters QueryFilter(ref string QueryString, Guid ProjectId, List<Guid> ZoneProjectIDs, bool ignoreQueryFilters, string saleUserFullName)
        {
            DynamicParameters ParamList = new DynamicParameters();

            ParamList.Add("@ProjectID", ProjectId);

            if (!ignoreQueryFilters)
                QueryString = QueryString.Replace("#IsDeleted", " AND U.IsDeleted = 0");
            else
                QueryString = QueryString.Replace("#IsDeleted", "");

            if (ZoneProjectIDs.Any())
            {
                for (var i = 1; i <= ZoneProjectIDs.Count; i++)
                {
                    ParamList.Add($"@ZProjectID{i}", ZoneProjectIDs[i - 1]);
                }
                QueryString = QueryString.Replace("#ZoneProjectIDs",
                      string.Format(" AND UP.ProjectID IN ({0})",
                      string.Join(",", ZoneProjectIDs.Select((_, index) => $"@ZProjectID{index + 1}"))));
            }
            else
            {
                QueryString = QueryString.Replace("#ZoneProjectIDs", string.Format(" AND UP.ProjectID = @ProjectID"));
            }

            if (!string.IsNullOrEmpty(saleUserFullName))
            {
                QueryString = QueryString.Replace("#SaleUserFullName", " AND U.DisplayName LIKE '%' + @SaleUserFullName + '%'");
                ParamList.Add("@SaleUserFullName", string.IsNullOrEmpty(saleUserFullName) ? "" : saleUserFullName);
            }
            else
            {
                QueryString = QueryString.Replace("#SaleUserFullName", "");
            }

            return ParamList;
        }

        public static string QueryOrder(ref string QueryString)
        {
            QueryString += QueryStringOrder;

            return QueryString;
        }

        public class QueryResult
        {
            public Guid? ID { get; set; }
            public string EmployeeNo { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string DisplayName { get; set; }
            public string PhoneNo { get; set; }
        }
    }
}
