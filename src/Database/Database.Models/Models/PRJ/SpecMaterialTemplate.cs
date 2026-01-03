using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Database.Models.PRJ
{
    [Description("SpecMaterialTemplate")]
    [Table("SpecMaterialTemplate", Schema = Schema.PROJECT)]
    public class SpecMaterialTemplate : BaseEntityWithoutMigrate
    {

        public string BG { get; set; }
        public string SpecMaterialGroupTH { get; set; }
        public string SpecMaterialGroupEN { get; set; }
        public string SpecMaterialTypeTH { get; set; }
        public string SpecMaterialTypeEN { get; set; }
        public string SpecMaterialDetailTH { get; set; }
        public string SpecMaterialDetailEN { get; set; }
        public bool? IsActive { get; set; }

        
    }
}
