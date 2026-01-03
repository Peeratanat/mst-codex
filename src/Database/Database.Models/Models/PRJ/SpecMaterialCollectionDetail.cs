using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("spec วัสดุ")]
    [Table("SpecMaterialCollectionDetail", Schema = Schema.PROJECT)]
    public class SpecMaterialCollectionDetail : BaseEntityWithoutMigrate
    {
        [Description("SpecMaterialGroupMasterCenterID")]
        public Guid? SpecMaterialGroupMasterCenterID { get; set; }
        [ForeignKey("SpecMaterialGroupMasterCenterID")]
        public MasterCenter SpecMaterialGroup { get; set; }

        [Description("SpecMaterialItemID")]
        public Guid? SpecMaterialItemID { get; set; }
        [ForeignKey("SpecMaterialItemID")]
        public SpecMaterialItem SpecMaterialItem { get; set; }


        [Description("สถานะ Active")]
        public bool? IsActive { get; set; }

        [Description("SpecMaterialCollectionID")]
        public Guid? SpecMaterialCollectionID { get; set; }
        [ForeignKey("SpecMaterialCollectionID")]
        public SpecMaterialCollection SpecMaterialCollection { get; set; }

    }
}
