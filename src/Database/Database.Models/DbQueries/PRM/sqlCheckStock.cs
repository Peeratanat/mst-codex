using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.PRM
{
    public class sqlCheckStock
    {
        public static string QueryString = @"        
              SELECT wbsc.WBSDeliveryID  FROM
		Stock_Promotion_REVO.dbo.WBSClearDelivery wbsc
        WHERE  ISNULL(wbsc.IsCancel,0) =0 AND wbsc.IsAdminApprove =1";

        public static string QueryStringOrder = @"";

        public static List<SqlParameter> QueryFilter(ref string QueryString, string DelvPromotionId)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(DelvPromotionId))
            {
                ParamList.Add(new SqlParameter("@DelvPromotionId", DelvPromotionId));
                QueryString += " AND wbsc.DeliveryNo = @DelvPromotionId";
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
            public long? WBSDeliveryID { get; set; }
      
        }
    }
}
