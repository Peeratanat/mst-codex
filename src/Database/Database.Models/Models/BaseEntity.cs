using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Database.Models
{
    public class BaseEntity : ISoftDeleteEntity
    {
        public BaseEntity()
        {
            if (this.ID == Guid.Empty)
            {
                this.ID = Guid.NewGuid();
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
        [JsonIgnore]
        public USR.User UpdatedBy { get; set; }
        [Column(Order = 104)]
        public bool IsDeleted { get; set; }

        [Column(Order = 105)]
        [MaxLength(100)]
        public string RefMigrateID1 { get; set; }
        [Column(Order = 106)]
        [MaxLength(100)]
        public string RefMigrateID2 { get; set; }
        [Column(Order = 107)]
        [MaxLength(100)]
        public string RefMigrateID3 { get; set; }

        [Column(Order = 108)]
        public DateTime? LastMigrateDate { get; set; }
    }
}
