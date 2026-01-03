using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลต้นแบบของราคาโครงการแบ่งตามประเภท")]
    [Table("PriceListItemTemplate", Schema = Schema.PROJECT)]
    public class PriceListItemTemplate : BaseEntity
    {
        [Description("ประเภทของโครงการ (แนวราบ แนวสูง)")]
        public Guid? ProductTypeMasterCenterID { get; set; }
        [ForeignKey("ProductTypeMasterCenterID")]
        public MST.MasterCenter ProductType { get; set; }
        [Description("รหัสของราคาโครงการ")]
        public Guid PriceListID { get; set; }
        [ForeignKey("PriceListID")]
        public PriceList PriceList { get; set; }
        [Description("ลำดับ")]
        public int Order { get; set; }
        [Description("ชนิดของราคา")]
        public Guid? MasterPriceItemID { get; set; }
        [ForeignKey("MasterPriceItemID")]
        public MST.MasterPriceItem MasterPriceItem { get; set; }

    }
}
