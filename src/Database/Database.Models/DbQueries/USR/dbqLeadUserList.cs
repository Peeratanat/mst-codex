using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.USR
{
    public class dbqLeadUserList
    {
        public Guid ID { get; set; }
        public string EmployeeNo { get;set;}
        public string FirstName { get;set;}
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string LineId { get; set; }

        public string DisplayName { get; set; }
        public string Title { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string DisplayNameEng { get; set; }
        public string FirstNameEng { get; set; }

        public string LastNameEng { get; set; }
        public string MiddleNameEng { get; set; }
        public string TitleEng { get; set; }

    }
}
