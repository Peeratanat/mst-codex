using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.CMS
{
    public class sqlRateSettingFixTransferModelProject
    {
        public static string QueryString = @"        
        SELECT  M.ID AS ID,
	        M.ID AS ModelID,
	        M.ProjectID,
	        M.Code,
	        M.NameTH,
	        S.Amount,
	        S.IsActive,
	        S.ActiveDate,
	        S.ExpireDate,
	        S.CreatedByUserID,
	        S.Created
        FROM PRJ.Model M
	        LEFT OUTER JOIN PRJ.Project P ON P.ID = M.ProjectID
	        LEFT OUTER JOIN  (
		        SELECT S1.*
		        FROM CMS.RateSettingFixTransferModel S1 INNER JOIN
		        (
			        SELECT ProjectID,ModelID,MAX(ActiveDate) AS ActiveDate
			        FROM CMS.RateSettingFixTransferModel 
			        WHERE IsActive =1 
				        AND IsDeleted = 0
			        GROUP BY ProjectID,ModelID
		        )S2 ON S2.ProjectID = S1.ProjectID AND S2.ModelID = S1.ModelID AND S2.ActiveDate = S1.ActiveDate
		        WHERE S1.IsActive =1 
			        AND S1.IsDeleted = 0
	        ) S ON S.ModelID = M.ID AND S.ProjectID = M.ProjectID
        WHERE 1=1
	        AND M.IsDeleted = 0";

        public static string QueryStringOrder = @" ORDER BY M.Code";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid? ProjectID, DateTime? ActiveDate,string ModelCode)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (ProjectID != null)
            {
                ParamList.Add(new SqlParameter("@ProjectID", ProjectID));
                QueryString += " AND M.ProjectID = @ProjectID";
            }

            if (ActiveDate != null)
            {
                ParamList.Add(new SqlParameter("@ActiveDate", ActiveDate));
                QueryString += " AND (MONTH(S.ActiveDate) = MONTH(@ActiveDate) AND YEAR(S.ActiveDate) = YEAR(@ActiveDate))";
            }

            if (!string.IsNullOrEmpty( ModelCode ) )
            {
                ParamList.Add(new SqlParameter("@ActiveDate", ActiveDate));
                QueryString += " AND M.Code = ModelCode";
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
            public Guid? ModelID { get; set; }
            public Guid? ProjectID { get; set; }
            public string Code { get; set; }
            public string NameTH { get; set; }
            public decimal? Amount { get; set; }
            public bool? IsActive { get; set; }
            public DateTime? ActiveDate { get; set; }
            public DateTime? ExpireDate { get; set; }
            public Guid? CreatedByUserID { get; set; }
            public DateTime? Created { get; set; }
        }
    }
}
