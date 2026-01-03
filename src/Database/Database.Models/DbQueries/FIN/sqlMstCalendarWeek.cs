using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.Finance
{
    public static class sqlMstCalendarWeek
    {
        public static string QueryString = @"Select 'W'= W  , 'Y' = Y , 'Q' = Q , 'M' = M
                                    from BI.Mst_Calendar_Week where 1=1 ";

        public static string QueryWhereDate = @" AND CONVERT(DATE,StartDate) <= CONVERT(DATE,GETDATE()) AND CONVERT(DATE,EndDate) >= CONVERT(DATE,GETDATE()) ";
        public class QueryResult
        {
            public int? W { get; set; } 
            public int? Y { get; set; } 
            public int? Q { get; set; } 
            public int? M { get; set; } 
        }


    }
}


