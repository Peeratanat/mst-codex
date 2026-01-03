using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.CMS
{
    public class sqlRateSettingTransfer
    {
        public static string QueryString = @"
        SELECT ST.[ID],
            ST.[ActiveDate],
            ST.[GroupID],
            ST.[ProjectID],
            ST.[Amount],
            ST.[StartRange],
            ST.[EndRange],
            ST.[IsActive],
            BG.ID AS BGID,
            BG.BGNO,
            P.ProjectNo,
            P.ProjectNameTH,
            R.Rate,
            R.[Sequence]
        FROM  [CMS].[RateSettingTransfer] ST
	        LEFT OUTER JOIN PRJ.Project P ON P.ID = ST.ProjectID
	        LEFT OUTER JOIN MST.BG BG ON BG.ID = P.BGID
	        LEFT JOIN [CMS].[RateTransfer] AS R ON R.BGNo = BG.BGNo AND ST.Amount=R.Rate
        WHERE ST.IsDeleted = 0";

        public static string QueryStringOrder = @" ORDER BY BG.BGNo,P.ProjectNo,ST.[Amount] ";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid? ProjectID, DateTime? ActiveDate, Guid? GroupID)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (ProjectID != null)
            {
                ParamList.Add(new SqlParameter("@ProjectID", ProjectID));
                QueryString += " AND ST.ProjectID = @ProjectID";
            }

            if (ActiveDate != null)
            {
                ParamList.Add(new SqlParameter("@ActiveDate", ActiveDate));
                QueryString += " AND (MONTH(ST.ActiveDate) = MONTH(@ActiveDate) AND YEAR(ST.ActiveDate) = YEAR(@ActiveDate))";
            }

            if (GroupID != null)
            {
                ParamList.Add(new SqlParameter("@GroupID", GroupID));
                QueryString += " AND ST.GroupID = @GroupID";
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
            public DateTime? ActiveDate { get; set; }
            public double? Amount { get; set; }
            public decimal? StartRange { get; set; }
            public decimal? EndRange { get; set; }
            public Guid? ProjectID { get; set; }
            public bool? IsActive { get; set; }
            public double? Rate { get; set; }
            public Guid? BGID { get; set; }
            public string BGNo { get; set; }
            public string ProjectNo { get; set; }
            public string ProjectNameTH { get; set; }
            public int? Sequence { get; set; }

        }
    }
}
