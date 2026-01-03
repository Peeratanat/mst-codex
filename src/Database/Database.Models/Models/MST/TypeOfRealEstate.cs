using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("ประเภทบ้าน")]
    [Table("TypeOfRealEstate", Schema = Schema.MASTER)]
    public class TypeOfRealEstate : BaseEntity
    {
        [Description("รหัสประเภทบ้าน")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Description("ชื่อประเภทบ้าน")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Description("ลักษณะ")]
        public Guid? RealEstateCategoryMasterCenterID { get; set; }
        [ForeignKey("RealEstateCategoryMasterCenterID")]
        public MasterCenter RealEstateCategory { get; set; }
        [Description("Standard Cost")]
        [Column(TypeName = "Money")]
        public decimal StandardCost { get; set; }
        [Description("Standard Price")]
        [Column(TypeName = "Money")]
        public decimal StandardPrice { get; set; }
    }
}