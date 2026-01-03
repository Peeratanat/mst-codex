using Base.DbQueries;
using Database.Models.USR;
using System;

namespace Database.Models.DbQueries.MST
{
    public class dbqUnitCombine : BaseDbQueries
    {
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string ProjectNameEN { get; set; }
        public Guid? UnitID { get; set; } 
        public string UnitNo { get; set; }
        public Guid? UnitCombineID { get; set; }
        public string UnitCombineNo { get; set; }
        public Guid? CombineDocTypeID { get; set; }
        public string CombineDocTypeName { get; set; }
        public string CombineDocTypeKey { get; set; }
        public Guid? CombineStatusID { get; set; }
        public string CombineStatusName { get; set; }
        public string CombineStatusKey { get; set; }
        public Guid? CombineUnitID { get; set; }
        public DateTime? ApprovedDate { get; set;}
        public string ApprovedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsEdit { get; set; }
        public bool? IsCanEdit { get; set; }
        public bool? IsCanDelete { get; set; }
    }
}
