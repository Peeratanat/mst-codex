using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic; 

namespace Database.Models.DbQueries.CMS
{
    public class sqlExportExcelRateTransfer
    {
        public static string QueryString =
            @"SELECT * 
                ,CONVERT(INT, ROW_NUMBER() OVER (PARTITION BY BGNo,ProjectNo,ProjectNameTH ORDER By Rate) )
                as  [Sequence]
                FROM (
                SELECT   BG.BGNo, P.ProjectNo, P.ProjectNameTH, R.Rate, RSS.StartRange, RSS.EndRange            
                FROM PRJ.Project P             
                LEFT OUTER JOIN MST.BG BG ON BG.ID = P.BGID            
                LEFT OUTER JOIN CMS.RateTransfer R ON R.BGNo = BG.BGNo AND R.IsDeleted=0            
                LEFT OUTER JOIN ( SELECT r1.*  FROM CMS.RateSettingTransfer r1 
	                INNER JOIN ( SELECT ProjectID,MAX(ActiveDate) AS ActiveDate 
	                FROM CMS.RateSettingTransfer 
	                WHERE IsActive = 1 AND IsDeleted=0 GROUP BY ProjectID ) r2 ON r2.ProjectID = r1.ProjectID AND r2.ActiveDate = r1.ActiveDate 
		                WHERE r1.IsActive = 1 AND r1.IsDeleted=0 ) RSS ON RSS.ProjectID = P.ID AND R.Rate = RSS.Amount            
		                WHERE P.IsDeleted=0 AND P.IsActive = 1 AND BG.BGNo IN (3,4)  
		                {WHERE}
                union 
                SELECT   BG.BGNo, P.ProjectNo, P.ProjectNameTH, RSS.amount, RSS.StartRange, RSS.EndRange            
                FROM PRJ.Project P             
                LEFT OUTER JOIN MST.BG BG ON BG.ID = P.BGID                      
                INNER JOIN ( SELECT r1.*  FROM CMS.RateSettingTransfer r1 
	                INNER JOIN ( SELECT ProjectID,MAX(ActiveDate) AS ActiveDate  
	                FROM CMS.RateSettingTransfer 
	                WHERE IsActive = 1 AND IsDeleted=0 GROUP BY ProjectID ) r2 ON r2.ProjectID = r1.ProjectID AND r2.ActiveDate = r1.ActiveDate 
		                WHERE r1.IsActive = 1 AND r1.IsDeleted=0 ) RSS ON RSS.ProjectID = P.ID  
		                WHERE P.IsDeleted=0 AND P.IsActive = 1 AND BG.BGNo IN (3,4)  
		                {WHERE}
		                ) AS A
            ";

        public static string QueryStringOrder = @" ORDER BY a.BGNo,a.ProjectNo,a.Rate";

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
            string WhereTmp = "";
            if (BGID != null)
            {
                ParamList.Add(new SqlParameter("@BGID", BGID));
                WhereTmp += " AND BG.ID = @BGID";
            }

            if (!string.IsNullOrEmpty(ListProjectId))
            {
                ParamList.Add(new SqlParameter("@ListProjectId", ListProjectId));
                WhereTmp += " AND P.ID IN (SELECT Val FROM dbo.fn_SplitString(@ListProjectId,','))";
            }
            QueryString = QueryString.Replace("{WHERE}", WhereTmp);
            return ParamList;
        }

        public static string QueryOrder(ref string QueryString)
        {
            QueryString += QueryStringOrder;

            return QueryString;
        }
    }
}
