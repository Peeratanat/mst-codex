using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("เช็คเพื่อนส้ราง SubLeadType ใหม่")]
    [Table("MappingSubLeadType", Schema = Schema.CUSTOMER)]
    public class MappingSubLeadType : BaseEntity
    {
        [Description("รหัสของ LeadType")]
        public Guid LeadTypeMasterCenterID { get; set; }
        [ForeignKey("LeadTypeMasterCenterID")]
        public MST.MasterCenter LeadType { get; set; }

        [Description("ประเภทย่อยของ Lead")]
        [MaxLength(100)]
        public string SubLeadType { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }
    }
}
