using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class BaseEntityWithoutKey : ISoftDeleteEntity
    {
        [Column(Order = 100)]
        public virtual DateTime? Created { get; set; }
        [Column(Order = 101)]
        public virtual DateTime? Updated { get; set; }
        public Guid? CreatedByUserID { get; set; }
        [ForeignKey("CreatedByUserID")]
        public USR.User CreateBy { get; set; }
        [Column(Order = 103)]
        public Guid? UpdatedByUserID { get; set; }
        [ForeignKey("UpdatedByUserID")]
        public USR.User UpdatedBy { get; set; }
        [Column(Order = 104)]
        public virtual bool IsDeleted { get; set; }
    }
}
