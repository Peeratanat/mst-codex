using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.PRJ
{
    public class sqlUnitDropdownDefectList
    {
        public static string QueryString = @"        
            SELECT a.UnitID AS ID,a.UnitNo
            FROM dbo.vw_UnitAllStatus a WITH(NOLOCK)
            WHERE 1=1
            AND a.UnitStatus IN ('BK','AG')  AND ISNULL(a.IsAccept,0) = 0 AND a.AcceptDate IS null 
        ";

        public static string QueryStringOrder = @" ORDER BY a.UnitNo";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid? ProjectID, string KeySearch)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (ProjectID != null)
            {
                ParamList.Add(new SqlParameter("@ProjectID", ProjectID));
                QueryString += " AND a.ProjectID = @ProjectID";
            }

            if (!string.IsNullOrEmpty(KeySearch))
            {
                ParamList.Add(new SqlParameter("@KeySearch", KeySearch));
                QueryString += " AND a.UnitNo LIKE '%' + @KeySearch + '%' ";
            }
            QueryString += QueryStringOrder;
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
            public string UnitNo { get; set; }
        }
    }
}
