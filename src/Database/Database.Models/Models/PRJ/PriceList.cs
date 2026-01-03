using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ราคาโครงการ")]
    [Table("PriceList", Schema = Schema.PROJECT)]
    public class PriceList : BaseEntity
    {
        [Description("รหัสแปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }
        [Description("วันที่ Active")]
        public DateTime? ActiveDate { get; set; }

        public List<PriceListItem> PriceListItems { get; set; }

    }
}
