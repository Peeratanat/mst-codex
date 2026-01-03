using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ROI
{
    [Description("GPVersion")]
    [Table("GPVersion", Schema = Schema.ROI)]
    public class GPVersion : BaseEntityWithoutMigrate
    {
        public string VersionCode { get; set; }
        public string VersionRemark { get; set; }
        public Guid? RoiID { get; set; }
        public Guid? ProjectID { get; set; }
        public int? Y { get; set; }
        public int? Q { get; set; }
        public int? M { get; set; }
        public string GPVersionType { get; set; }
        public Guid? RefGPVersion { get; set; }
        public DateTime? SyncDate { get; set; }
        public Guid? GPSyncOriginalID { get; set; }
        public Guid? GPOriginalProjectID { get; set; }
        public Guid? GPOriginalUnitID { get; set; }
        public int? Status { get; set; }
        public bool? IsActive { get; set; }
    }
}
