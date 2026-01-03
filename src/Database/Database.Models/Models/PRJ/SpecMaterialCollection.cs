using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("spec วัสดุ")]
    [Table("SpecMaterialCollection", Schema = Schema.PROJECT)]
    public class SpecMaterialCollection : BaseEntityWithoutMigrate
    {
        [Description("ProjectID")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }




        [Description("Name")]
        public string Name { get; set; }


        [Description("สถานะ Active")]
        public bool? IsActive { get; set; }


    }
}
