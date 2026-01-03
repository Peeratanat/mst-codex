using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("item spec วัสดุ")]
    [Table("SpecMaterialItem", Schema = Schema.PROJECT)]
    public class SpecMaterialItem : BaseEntityWithoutMigrate
    {

        [Description("SpecMaterialGroupMasterCenterID")]
        public Guid? SpecMaterialGroupMasterCenterID { get; set; }
        [ForeignKey("SpecMaterialGroupMasterCenterID")]
        public MasterCenter SpecMaterialGroup { get; set; }


        [Description("Code")]
        public string Code { get; set; }

        [Description("ชื่อ")]
        public string Name { get; set; }
        [Description("Description")]
        public string ItemDescription { get; set; }

        [Description("Order")]
        public int? Order { get; set; }

        [Description("สถานะ Active")]
        public bool? IsActive { get; set; }


        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        public string NameEN { get; set; }
        public string ItemDescriptionEN { get; set; }
        
    }
}
