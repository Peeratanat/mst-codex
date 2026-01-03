using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.Finance
{
    public static class sqlUnknownPaymentRemain
    {
        public static string QueryString = @"SELECT 'UnknownPaymentID' = un.ID
             , 'UnknownPaymentAmount' = un.Amount
             , 'ConvertAmount' = ISNULL(SUM(p.TotalAmount), 0)
             , 'RemainAmount' = un.Amount - ISNULL(SUM(p.TotalAmount), 0)
            FROM FIN.UnknownPayment un 
            LEFT JOIN FIN.PaymentMethod pm ON pm.UnknownPaymentID = un.ID AND pm.IsDeleted = 0
            LEFT JOIN FIN.Payment p ON p.ID = pm.PaymentID AND p.IsDeleted = 0 AND p.IsCancel = 0
            WHERE un.IsDeleted = 0";
           
        //        AND un.BookingID = '300FA30D-3CE1-4659-A35A-BA44FFC89813'

        public static string QueryStringGrouping = @" GROUP BY un.ID, un.Amount";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid BookingID)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (BookingID != Guid.Empty)
            {
                ParamList.Add(new SqlParameter("@prmBookingID", BookingID));
                QueryString += " AND un.BookingID = @prmBookingID";
            }

            return ParamList;
        }

        public static string QueryGrouping(string QueryString)
        {
            QueryString += QueryStringGrouping;

            return QueryString;
        }

        public class QueryResult
        {
            public Guid? UnknownPaymentID { get; set; }
            public decimal? UnknownPaymentAmount { get; set; }
            public decimal? ConvertAmount { get; set; }
            public decimal? RemainAmount { get; set; }
        }
    }
}


