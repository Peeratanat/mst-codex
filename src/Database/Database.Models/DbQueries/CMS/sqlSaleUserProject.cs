using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.CMS
{
    public class sqlSaleUserProject
    {
        public static string QueryString = @"        
        SELECT DISTINCT U.ID,
            U.EmployeeNo,
            U.FirstName,
            U.LastName,
            U.DisplayName,
            U.PhoneNo 
        FROM USR.[User] U
	        LEFT OUTER JOIN USR.UserAuthorizeProject UP ON UP.UserID = U.ID
	        LEFT OUTER JOIN USR.UserRole UR ON	UR.UserID = U.ID
	        LEFT OUTER JOIN USR.[Role] R ON R.ID = UR.RoleID
        WHERE 1=1
	        AND R.Code = 'LC'
	        AND U.EmployeeNo NOT LIKE 'TEST%'
	        AND UP.ProjectID = @ProjectID
	        AND ((@IgnoreDelete = 0 AND U.IsDeleted = 0) OR (@IgnoreDelete = 1))
	        AND (ISNULL(@SaleUserFullName,'') = '' OR U.DisplayName LIKE '%' + @SaleUserFullName + '%')

        UNION

        SELECT DISTINCT U.ID,
            U.EmployeeNo,
            U.FirstName,
            U.LastName,
            U.DisplayName,
            U.PhoneNo  
        FROM USR.[User] U
	        LEFT OUTER JOIN SAL.Booking B ON B.ProjectSaleUserID = U.ID
        WHERE B.IsDeleted = 0
	        AND B.ProjectID = @ProjectID
            AND U.IsDeleted = 0
            AND (ISNULL(@SaleUserFullName,'') = '' OR U.DisplayName LIKE '%' + @SaleUserFullName + '%')";

        public static string QueryStringOrder = @" ORDER BY U.EmployeeNo ";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid ProjectId, bool ignoreQueryFilters, string saleUserFullName)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@ProjectID", ProjectId));
            ParamList.Add(new SqlParameter("@IgnoreDelete", ignoreQueryFilters ? 1 : 0));
            ParamList.Add(new SqlParameter("@SaleUserFullName", string.IsNullOrEmpty(saleUserFullName) ? "": saleUserFullName));

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
