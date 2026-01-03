using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRJ
{
    [Table("ProjectInfoDetail", Schema = Schema.PROJECT)]
    public partial class ProjectInfoDetail
    {
        public Guid ID { get; set; }
        public Guid ProjectID { get; set; }
        public string AdminDescription { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? CreatedByUserID { get; set; }
        public Guid? UpdatedByUserID { get; set; }
    }


}
