using System;

namespace Database.Models.DbQueries.IDT
{
    public class dbqGetUserDashboardMenu
    {
        public Guid? UserID { get; set; }
     
        public Guid? MenuID { get; set; }

        public string DashboardCode { get; set; }

        public string DashboardNameTH { get; set; }

        public string DashboardURL { get; set; }
    }

}
