using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class BaseEntityWithoutMigrate : ISoftDeleteEntity
    {
        public BaseEntityWithoutMigrate()
        {
            if (ID == Guid.Empty)
            {
                ID = Guid.NewGuid();
            }
        }

        [Key]
        public Guid ID { get; set; }

        [Column(Order = 100)]
        public DateTime? Created { get; set; }

        [Column(Order = 101)]
        public DateTime? Updated { get; set; }

        [Column(Order = 102)]
        public Guid? CreatedByUserID { get; set; }
        [ForeignKey("CreatedByUserID")]
        public USR.User CreatedBy { get; set; }

        [Column(Order = 103)]
        public Guid? UpdatedByUserID { get; set; }
        [ForeignKey("UpdatedByUserID")]
        public USR.User UpdatedBy { get; set; }

        [Column(Order = 104)]
        public bool IsDeleted { get; set; }
    }
}
