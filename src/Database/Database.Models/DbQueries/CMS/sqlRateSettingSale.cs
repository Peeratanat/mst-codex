using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.CMS
{
    public class sqlRateSettingSale
    {
        public static string QueryString = @"        
        SELECT SS.[ID],
            SS.[ActiveDate],
            SS.[GroupID],
            SS.[ProjectID],
            SS.[Amount],
            SS.[StartRange],
            SS.[EndRange],
            SS.[IsActive],
            BG.ID AS BGID,
            BG.BGNO,
            P.ProjectNo,
            P.ProjectNameTH,
            R.Rate,
            R.[Sequence]
        FROM [CMS].[RateSettingSale] SS
	        LEFT OUTER JOIN PRJ.Project P ON P.ID = SS.ProjectID
	        LEFT OUTER JOIN MST.BG BG ON BG.ID = P.BGID
	        LEFT OUTER JOIN CMS.RateSale R ON R.BGNo = BG.BGNo AND SS.Amount=R.Rate AND r.IsDeleted = 0
        WHERE SS.IsDeleted = 0";

        public static string QueryStringOrder = @" ORDER BY BG.BGNo,P.ProjectNo, SS.[Amount] ";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid? ProjectID, DateTime? ActiveDate, Guid? GroupID)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (ProjectID != null)
            {
                ParamList.Add(new SqlParameter("@ProjectID", ProjectID));
                QueryString += " AND SS.ProjectID = @ProjectID";
            }

            if (ActiveDate != null)
            {
                ParamList.Add(new SqlParameter("@ActiveDate", ActiveDate));
                QueryString += " AND (MONTH(SS.ActiveDate) = MONTH(@ActiveDate) AND YEAR(SS.ActiveDate) = YEAR(@ActiveDate))";
            }

            if (GroupID != null)
            {
                ParamList.Add(new SqlParameter("@GroupID", GroupID));
                QueryString += " AND SS.GroupID = @GroupID";
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
            public Guid? GroupID { get; set; }
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
