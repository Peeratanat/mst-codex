using Database.Models.CTM;
using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("BookingControl")]
    [Table("BookingControl", Schema = Schema.SALE)]
    public class BookingControl : BaseEntity
    {
        [Description("รหัสใบจอง")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public Booking Booking { get; set; }

        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("รหัสแปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }

        [Description("EffectiveDate")]
        public DateTime? EffectiveDate { get; set; }

        [Description("ExpiredDate")]
        public DateTime? ExpiredDate { get; set; }

        [Description("Remark")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("BookingLock")]
        public Guid? BookingLockMasterCenterID { get; set; }
    }
}
