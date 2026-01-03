using System;

namespace Database.Models.DbQueries.Finance
{
    public class sqlBookingForPayment
    {
        public static string QueryString = @"select 'BookingID' = b.ID,'CompanyID' = p.CompanyID from sal.Booking b
                                            left join prj.Project p on b.ProjectID = p.ID AND p.IsDeleted = 0
                                            where 1=1 
                                            and b.IsDeleted = 0 "; 

        public class QueryResult
        {
            public Guid? BookingID { get; set; }
            public Guid? CompanyID { get; set; } 

        }
    }
}


