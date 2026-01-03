using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.CMS
{
    [Description("LCReconUser")]
    [Table("LCReconUser", Schema = Schema.COMMISSION)]
    public class LCReconUserByProject : BaseEntityWithoutMigrate
    {
        public Guid? SaleUserID { get; set; }
        [ForeignKey("SaleUserID")]
        public USR.User SaleUser { get; set; }

        [Description("วันที่เริ่ม")]
        public DateTime? StartActiveDate { get; set; }

        [Description("วันที่สิ้นสุด")]
        public DateTime? EndActiveDate { get; set; }

        [Description("Remark")]
        [MaxLength(2000)]
        public string Remark { get; set; }
    }
}
