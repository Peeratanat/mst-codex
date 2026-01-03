using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class BaseEntityWithoutUpdater : ISoftDeleteEntity
    {
        public BaseEntityWithoutUpdater()
        {
            if (ID == Guid.Empty)
                ID = Guid.NewGuid();
        }

        [Key]
        [Column(Order = 1)]
        public Guid ID { get; set; }

        [Column(Order = 100)]
        public DateTime? Created { get; set; }

        [Column(Order = 102)]
        public Guid? CreatedByUserID { get; set; }
        [ForeignKey("CreatedByUserID")]
        public USR.User CreatedBy { get; set; }

        [Column(Order = 104)]
        public bool IsDeleted { get; set; } = false;
    }
}