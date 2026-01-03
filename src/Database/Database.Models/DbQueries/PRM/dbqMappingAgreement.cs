using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.PRM
{
    public class dbqMappingAgreement : BaseDbQueries
    {
        public Guid? MaID { get; set; }
        public string OldAgreement { get; set; }
        public string OldItem { get; set; }
        public string OldMaterialCode { get; set; }
        public string OldName { get; set; }
        public string OldType { get; set; }
        public string NewAgreement { get; set; }
        public string NewItem { get; set; }
        public string NewMaterialCode { get; set; }
        public string NewName { get; set; }
        public string NewType { get; set; }
        public string CreateBy { get; set; }
        public DateTime? Created { get; set; }
        public string Remark { get; set; }
    }
}


