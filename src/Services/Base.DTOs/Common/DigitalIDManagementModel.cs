using System;

namespace Base.DTOs.Common
{
     
    public partial class DigitalIDManagementSearch
    {
        public string? BG { get; set; }
        public string? ProjectCode { get; set; }
        public string? LcCode { get; set; }

        public Guid? BGID { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? LCUserID { get; set; }
        public Guid? UserID { get; set; }

        public int Take { get; set; }
        public int Skip { get; set; }
        public string? SortOrder { get; set; }

        public bool? IsReview { get; set; } = false;
    } 
 
}
