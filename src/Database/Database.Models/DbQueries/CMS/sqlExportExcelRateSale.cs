using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.CMS
{
    public class sqlExportExcelRateSale
    {
        public static string QueryString =
            @"SELECT DISTINCT BG.BGNo,
	            P.ProjectNo,
	            P.ProjectNameTH,
	            R.Sequence,
	            R.Rate,
	            RSS.StartRange,
	            RSS.EndRange
            FROM PRJ.Project P 
            LEFT OUTER JOIN MST.BG BG ON BG.ID = P.BGID
            LEFT OUTER JOIN CMS.RateSale R ON R.BGNo = BG.BGNo AND R.IsDeleted=0
            LEFT OUTER JOIN 
            (
	            SELECT r1.* 
	            FROM CMS.RateSettingSale r1
		            INNER JOIN (
			            SELECT ProjectID,MAX(ActiveDate) AS ActiveDate
			            FROM CMS.RateSettingSale
			            WHERE IsActive = 1
				            AND IsDeleted=0
			            GROUP BY ProjectID
		            ) r2 ON r2.ProjectID = r1.ProjectID AND r2.ActiveDate = r1.ActiveDate
	            WHERE r1.IsActive = 1
		            AND r1.IsDeleted=0
            ) RSS ON RSS.ProjectID = P.ID AND R.Rate = RSS.Amount
            WHERE P.IsDeleted=0
	            AND P.IsActive = 1
            ";

        public static string QueryStringOrder = @" ORDER BY BG.BGNo,P.ProjectNo,R.Sequence";

        public class QueryResult
        {
            public string BGNo { get; set; }
            public string ProjectNo { get; set; }
            public string ProjectNameTH { get; set; }
            public int? Sequence { get; set; }
            public double? Rate { get; set; }
            public decimal? StartRange { get; set; }
            public decimal? EndRange { get; set; }
        }

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid? BGID, string ListProjectId)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (BGID != null)
            {
                ParamList.Add(new SqlParameter("@BGID", BGID));
                QueryString += " AND BG.ID = @BGID";
            }

            if(!string.IsNullOrEmpty(ListProjectId))
            {
                ParamList.Add(new SqlParameter("@ListProjectId", ListProjectId));
                QueryString += " AND P.ID IN (SELECT Val FROM dbo.fn_SplitString(@ListProjectId,','))";
            }

            return ParamList;
        }

        public static string QueryOrder(ref string QueryString)
        {
            QueryString += QueryStringOrder;

            return QueryString;
        }
    }
}
