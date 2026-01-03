using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.EXT
{
    public static class sqlGetPricelist
    {
        public static string QueryString = @"
        SELECT pj.ID,pj.ProjectNo ,pj.ProjectNameTH AS ProjectName,COUNT(pwf.ID) AS SumByProject
FROM USR.[User] us
    LEFT JOIN USR.UserRole ur
        ON ur.UserID = us.ID AND ur.IsDeleted=0
    LEFT JOIN USR.Role r
        ON r.ID = ur.RoleID AND r.IsDeleted=0
    LEFT JOIN USR.UserAuthorizeProject usp
        ON usp.UserID = us.ID AND usp.IsDeleted=0
    LEFT JOIN PRJ.Project pj
        ON pj.ID = usp.ProjectID AND pj.IsDeleted=0
	LEFT JOIN SAL.PriceListWorkflow  pwf ON pwf.ProjectID = usp.ProjectID
	WHERE pwf.IsApproved is null AND pwf.ID IS NOT NULL AND pwf.IsDeleted=0 
	AND (r.code='LCM' or r.code='HOCS') #UserId  GROUP BY pj.ID,
                                                              pj.ProjectNo,
                                                              pj.ProjectNameTH";


        public static string QueryStringOrder = @" ORDER BY p.ReceiveDate, p.Created, mstPayType.[Order], mpi.[Order], pi.Period";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid UserID)
        {
            var ParamList = new List<SqlParameter>() ?? new List<SqlParameter>();

        
                ParamList.Add(new SqlParameter($"@UserID", UserID));

            QueryString = QueryString.Replace("#UserId", string.Format(" AND us.id = @UserID"));

            return ParamList;
        }

        public static string QueryOrder(string QueryString)
        {
            QueryString += QueryStringOrder;

            return QueryString;
        }

        public class QueryResult
        {
            public Guid? ID { get; set; }
            /// <summary>
            /// เลขโปรเจค
            /// </summary>
            public string ProjectNo { get; set; }
            /// <summary>
            /// ชื่อโปรเจค
            /// </summary>
            public string ProjectName { get; set; }
            /// <summary>
            /// ยอดรวม
            /// </summary>
            public int SumByProject { get; set; }

        }
    }
}


