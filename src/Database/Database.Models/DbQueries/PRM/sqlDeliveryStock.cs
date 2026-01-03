using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.PRM
{
    public class sqlDeliveryStock
    {
        public static string QueryString = @"        
        SELECT ReqNo AS ReqHeaderId FROM Stock_Promotion_REVO.dbo.StockManagement
  LEFT JOIN  Stock_Promotion_REVO.dbo.WBSRequest ON WBSRequest.WBSReqNo = StockManagement.RefReqDocID
  WHERE ISNULL(StockManagement.IsCancel,0) = 0 ";

        public static string QueryStringOrder = @"";

        public static List<SqlParameter> QueryFilter(ref string QueryString, string ReqNo)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(ReqNo))
            {
                ParamList.Add(new SqlParameter("@ReqNo", ReqNo));
                QueryString += " AND ReqNo = @ReqNo";
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
            public string ReqHeaderId { get; set; }
         
        }
    }
}
 