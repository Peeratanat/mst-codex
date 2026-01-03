using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqUserAGList
    {
        public Guid Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNo { get; set; }
    }
}
