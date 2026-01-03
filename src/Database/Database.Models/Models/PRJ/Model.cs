using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลแบบบ้าน")]
    [Table("Model", Schema = Schema.PROJECT)]
    public class Model : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
        [Description("รหัสแบบบ้าน")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Description("ชื่อแบบบ้าน (ภาษาไทย)")]
        [MaxLength(100)]
        public string NameTH { get; set; }
        [Description("ชื่อแบบบ้าน (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string NameEN { get; set; }
        [Description("ชื่อย่อ")]
        public Guid? ModelShortNameMasterCenterID { get; set; }
        [ForeignKey("ModelShortNameMasterCenterID")]
        public MST.MasterCenter ModelShortName { get; set; }

        [Description("ลักษณะบ้าน")]
        public Guid? ModelUnitTypeMasterCenterID { get; set; }
        [ForeignKey("ModelUnitTypeMasterCenterID")]
        public MST.MasterCenter ModelUnitType { get; set; }

        [Description("ประเภทบ้าน")]
        public Guid? TypeOfRealEstateID { get; set; }
        [ForeignKey("TypeOfRealEstateID")]
        public MST.TypeOfRealEstate TypeOfRealEstate { get; set; }

        [Description("ประเภทโครงการ")]
        public Guid? ModelTypeMasterCenterID { get; set; }
        [ForeignKey("ModelTypeMasterCenterID")]
        public MST.MasterCenter ModelType { get; set; }

        [Description("อัตราชำระคืน")]
        public double? PreferUnit { get; set; }
        [Description("อัตราชำระคืนต่อพื้นที่")]
        public double? PreferUnitMinimum { get; set; }
        [Description("จำนวนชำระคืนต่อหน่วย")]
        public double? PreferHouse { get; set; }

        [Description("หน้ากว้าง")]
        public double? FrontWidth { get; set; }

        [Description("ค่าส่วนกลางเหมาจ่าย (บาท)")]
        [Column(TypeName = "Money")]
        public decimal? PublicFundAmount { get; set; }


        public Guid? SpecMaterialCollectionID { get; set; }
        [ForeignKey("SpecMaterialCollectionID")]
        public PRJ.SpecMaterialCollection SpecMaterialCollection { get; set; }



        [Description("ห้องนอน")]
        public int? Bedroom { get; set; }
        [Description("ห้องน้ำ")]
        public int? Bathroom { get; set; }
        [Description("ที่จอดรถ")]
        public int? ParkingSpace { get; set; }
    }
}
