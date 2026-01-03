using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.PRJ
{
    public class sqlUnitDropdownSellPriceList
    {
        public static string QueryString = @"        
        SELECT U.ID
            ,U.UnitNo
            ,U.UnitStatusMasterCenterID 
            ,'SellPrice' = (SELECT TOP 1 I.Amount
					            FROM PRJ.PriceList P WITH(NOLOCK)
						            LEFT OUTER JOIN PRJ.PriceListItem I WITH(NOLOCK) ON I.PriceListID = P.ID
						            LEFT OUTER JOIN MST.MasterPriceItem M WITH(NOLOCK) ON M.ID = I.MasterPriceItemID
					            WHERE P.ActiveDate <= GETDATE()
						            AND P.UnitID = U.ID
						            AND M.[Key] = 'SellingPrice'
                                    AND P.IsDeleted = 0
					            ORDER BY ActiveDate DESC)
        FROM PRJ.Unit U WITH(NOLOCK)
	        LEFT OUTER JOIN MST.MasterCenter M WITH(NOLOCK) ON M.ID = U.UnitStatusMasterCenterID
        WHERE 1=1
            AND U.IsDeleted = 0
	        AND M.[Key] = '0'
	        AND NOT EXISTS(SELECT * 
				        FROM PRM.PreSalePromotionRequestUnit RU WITH(NOLOCK)
				        LEFT OUTER JOIN  PRM.PreSalePromotionRequest PR WITH(NOLOCK) ON PR.ID = RU.PreSalePromotionRequestID
						LEFT OUTER JOIN PRM.PRRequestJob prj WITH(NOLOCK) ON prj.PreSalePromotionRequestUnitID =RU.ID
						LEFT OUTER JOIN  PRM.PRCancelJob pcj WITH (NOLOCK) ON pcj.PreSalePromotionRequestUnitID = ru.ID AND pcj.Created >prj.Created
				        WHERE RU.IsDeleted = 0 
					        AND PR.IsDeleted = 0 
					        AND RU.UnitID = U.ID
							AND pcj.ID IS NULL)
        ";

        public static string QueryStringOrder = @" ORDER BY U.UnitNo";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid? ProjectID, string KeySearch, List<string> excludeUnits)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (ProjectID != null)
            {
                ParamList.Add(new SqlParameter("@ProjectID", ProjectID));
                QueryString += " AND U.ProjectID = @ProjectID";
            }

            if (!string.IsNullOrEmpty(KeySearch))
            {
                ParamList.Add(new SqlParameter("@KeySearch", KeySearch));
                QueryString += " AND U.UnitNo LIKE '%' + @KeySearch + '%' ";
            }

            if (excludeUnits != null && excludeUnits.Count > 0)
            {
                ParamList.Add(new SqlParameter("@ExcludeUnits", string.Join(",", excludeUnits)));
                QueryString += " AND U.UnitNo NOT IN (SELECT val FROM[dbo].[fn_SplitString](@ExcludeUnits, ',')) ";
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
            public string UnitNo { get; set; }
            public Guid? UnitStatusMasterCenterID { get; set; }
            public decimal? SellPrice { get; set; }
        }
    }
}
