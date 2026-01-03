using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRJ
{
    [Table("ProjectInfoLocation", Schema = Schema.PROJECT)]
    public class ProjectInfoLocation
    {
        public Guid ID { get; set; }
        public int? RefID { get; set; }
        public int? RefID2 { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string LanguageType { get; set; }
        public string Description { get; set; }
        public string LocationType { get; set; }
        public string LocationTitle { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? CreatedByUserID { get; set; }
        public Guid? UpdatedByUserID { get; set; }
    }

}
