using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("ผลการคำนวนหาราคาประเมินของโอนกรรมสิทธิ์")]
    [Table("TransferFeeResult", Schema = Schema.SALE)]
    public class TransferFeeResult : BaseEntity
    {
        [Description("โอนกรรมสิทธิ์")]
        public Guid AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public Agreement Agreement { get; set; }

        [Description("วันที่โอนกรรมสิทธิ์")]
        public DateTime TransferDate { get; set; }

        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("แปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public PRJ.Model Model { get; set; }

        [Description("รหัสอาคาร")]
        public Guid? TowerID { get; set; }
        [ForeignKey("TowerID")]
        public PRJ.Tower Tower { get; set; }

        [Description("รหัสชั้น")]
        public Guid? FloorID { get; set; }
        [ForeignKey("FloorID")]
        public PRJ.Floor Floor { get; set; }

        [Description("สำนักงานที่ดิน")]
        public Guid? LandOfficeID { get; set; }
        [ForeignKey("LandOfficeID")]
        public MST.LandOffice LandOffice { get; set; }

        [Description("ราคาขาย")]
        [Column(TypeName = "Money")]
        public decimal? SalePrice { get; set; }

        [Description("พื้นที่ขาย")]
        public double? SaleArea { get; set; }

        [Description("พื้นที่ใช้สอย")]
        public double? UsedArea { get; set; }

        [Description("ราคาพื้นที่ใช้สอย")]
        [Column(TypeName = "Money")]
        public decimal? UsedAreaPrice { get; set; }

        [Description("ค่าเสื่อมราคาต่อปี")]
        public double? DepreciationPerYear { get; set; }

        [Description("จำนวนปีที่คิดค่าเสื่อมราคา")]
        public int? DepreciationYear { get; set; }

        [Description("อัตราค่าเสื่อมราคา")]
        public int? DepreRateTotal { get; set; }

        [Description("ภาษีเงินได้นิติบุคคล")]
        [Column(TypeName = "Money")]
        public decimal? CompanyIncomeTax { get; set; }

        [Description("ภาษีเงินได้ธุรกิจเฉพาะ")]
        [Column(TypeName = "Money")]
        public decimal? BusinessTax { get; set; }

        [Description("ภาษีท้องถิ่น")]
        [Column(TypeName = "Money")]
        public decimal? LocalTax { get; set; }

        [Description("อัตรากองทุนคอนโด (บาท)")]
        [Column(TypeName = "Money")]
        public decimal? CondoFundPrice { get; set; }


        [Description("จำนวนเดือนที่เก็บค่าส่วนกลาง")]
        public int? PublicFundMonths { get; set; }

        [Description("จำนวนเดือนที่เก็บค่าส่วนกลาง")]
        [Column(TypeName = "Money")]
        public decimal? PublicFundPrice { get; set; }

        [Description("ค่าติดตั้งมิเตอร์ไฟฟ้า")]
        [Column(TypeName = "Money")]
        public decimal? ElectricMeterPrice { get; set; }

        [Description("ค่าติดตั้งมิเตอร์ประปา")]
        [Column(TypeName = "Money")]
        public decimal? WaterMeterPrice { get; set; }

        [Description("พื้นที่ใช้สอย")]
        public double? MortgageRate { get; set; }

        [Description("อัตราค่าจดจำนอง")]
        public double? TransferFeeRate { get; set; }

        [Description("พื้นที่ระเบียง")]
        public double? BalconyArea { get; set; }

        [Description("ราคาประเมินพื้นที่ระเบียง")]
        [Column(TypeName = "Money")]
        public decimal? BalconyAreaPrice { get; set; }

        [Description("พื้นที่วางแอร์")]
        public double? AirArea { get; set; }

        [Description("ราคาประเมินพื้นที่วางแอร์")]
        [Column(TypeName = "Money")]
        public decimal? AirAreaPrice { get; set; }

        [Description("จำนวนที่จอดรถ Fix")]
        public int? NumberOfParkingFix { get; set; }

        [Description("จำนวนที่จอดรถไม่ Fix")]
        public int? NumberOfParkingUnFix { get; set; }

        [Description("พื้นที่จอดรถ")]
        public double? ParkingArea { get; set; }

        [Description("ราคาประเมินพื้นที่จอดรถ")]
        [Column(TypeName = "Money")]
        public decimal? ParkingAreaPrice { get; set; }

        [Description("รวมพื้นที่จอดรถหรือไม่")]
        public bool? Parkingstatus { get; set; }

        [Description("พื้นที่รั้วคอนกรีต")]
        public double? ConcreteArea { get; set; }
        [Description("ราคาประเมินพื้นที่รั้วคอนกรีต")]
        [Column(TypeName = "Money")]
        public decimal? ConcreteAreaPrice { get; set; }

        [Description("พื้นที่รั้วเหล็กดัด")]
        public double? IronArea { get; set; }
        [Description("ราคาประเมินพื้นที่รั้วเหล็กดัด")]
        [Column(TypeName = "Money")]
        public decimal? IronAreaPrice { get; set; }

        [Description("ราคาประเมินพื้นที่ดินต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal? EstimateLandPricePerUnit { get; set; }
        [Description("ราคาประเมินพื้นที่ดิน")]
        [Column(TypeName = "Money")]
        public decimal? EstimateLandPrice { get; set; }
        [Description("ค่าเสื่อมราคาประเมิน")]
        [Column(TypeName = "Money")]
        public decimal? EstimateDepreciationPrice { get; set; }
        [Description("ราคาประเมิน สปส.")]
        [Column(TypeName = "Money")]
        public decimal? EstimateBuildingPrice { get; set; }
        [Description("ราคาประเมินรวม")]
        [Column(TypeName = "Money")]
        public decimal? EstimateTotalPrice { get; set; }

        [Description("ส่วนต่างระหว่างพื้นที่ใช้สอยกับพื้นที่ก่อสร้าง")]
        public double? DiffUsedAreaBuildingPermitArea { get; set; }

        [Description("พื้นที่ก่อสร้างตามใบอนุญาต")]
        public double? BuildingPermitArea { get; set; }

        [Description("ราคาส่วนต่างระหว่างพื้นที่ใช้สอยกับพื้นที่ก่อสร้าง")]
        [Column(TypeName = "Money")]
        public decimal? BuildingPermitPricePerArea { get; set; }
    }
}
