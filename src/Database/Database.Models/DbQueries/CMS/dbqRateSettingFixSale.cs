using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.CMS
{
    public class dbqRateSettingFixSale : BaseDbQueries
    {
        public Guid? ID { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? CreatedByUserID { get; set; }
        public DateTime? Created { get; set; }
        public bool? IsActive { get; set; }
        public decimal? Amount { get; set; }
    }
}
