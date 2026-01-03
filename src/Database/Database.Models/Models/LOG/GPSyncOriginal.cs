using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Description("GPSyncOriginal")]
    [Table("GPSyncOriginal", Schema = Schema.LOG)]
    public class GPSyncOriginal
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime? LogSyncDate { get; set; }
        public int? Y { get; set; }
        public int? Q { get; set; }
        public int? M { get; set; }
    }
}
