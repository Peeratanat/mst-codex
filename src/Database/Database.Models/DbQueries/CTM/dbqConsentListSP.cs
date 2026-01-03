using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.CTM
{
    public class dbqConsentListSP : BaseDbQueries
    {
        public Guid? ID { get; set; }
        public Guid? ConsentTypeMasterCenterID { get; set; }
        public string ReferentSubTypeMasterCenter { get; set; }
        public Guid? ProjectID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpDateDate { get; set; }
        public Guid? ReferentType { get; set; }
        public string ContactNumber { get; set; }



    }
}
