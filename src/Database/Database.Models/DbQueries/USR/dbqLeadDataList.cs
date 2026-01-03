using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.USR
{
    public class dbqLeadDataList
    {
        public Guid ID { get; set; }
        public Guid? ProjectID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumberm { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? OwnerID { get; set; }
        public string RefID { get; set; }
        public string NumberOfContact { get; set; }
        public DateTime? Created { get; set; }

    }
}
