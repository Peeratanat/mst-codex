using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลแปลง")]
    [Table("Unit", Schema = Schema.PROJECT)]
    public class Unit : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public Model Model { get; set; }
        [Description("ประเภทของสินทรัพย์")]
        public Guid? AssetTypeMasterCenterID { get; set; }
        [ForeignKey("AssetTypeMasterCenterID")]
        public MST.MasterCenter AssetType { get; set; }
        [Description("เลขที่แปลง")]
        [MaxLength(50)]
        public string UnitNo { get; set; }
        [Description("บ้านเลขที่ (ตามโฉนด)")]
        [MaxLength(100)]
        public string HouseNo { get; set; }
        [Description("ปีที่ได้บ้านเลขที่")]
        public int? HouseNoReceivedYear { get; set; }
        [Description("SAP WBS Object")]
        [MaxLength(100)]
        public string SAPWBSObject { get; set; }
        [Description("SAP WBS Number")]
        [MaxLength(100)]
        public string SAPWBSNo { get; set; }
        [Description("SAP WBS Object สำหรับ Budget Promotion")]
        [MaxLength(100)]
        public string SAPWBSObject_P { get; set; }
        [Description("SAP WBS Number สำหรับ Budget Promotion")]
        [MaxLength(100)]
        public string SAPWBSNo_P { get; set; }
        [Description("SAP WBS Status")]
        [MaxLength(100)]
        public string SAPWBSStatus { get; set; }

        [Description("ทิศ")]
        public Guid? UnitDirectionMasterCenterID { get; set; }
        [ForeignKey("UnitDirectionMasterCenterID")]
        public MST.MasterCenter UnitDirection { get; set; }

        [Description("ประเภทแปลง")]
        public Guid? UnitTypeMasterCenterID { get; set; }
        [ForeignKey("UnitTypeMasterCenterID")]
        public MST.MasterCenter UnitType { get; set; }

        [Description("ราคาบุริมสิทธิ์")]
        [Column(TypeName = "Money")]
        public decimal? UnitLoanAmount { get; set; }

        [Description("สถานะแปลง")]
        public Guid? UnitStatusMasterCenterID { get; set; }
        [ForeignKey("UnitStatusMasterCenterID")]
        public MST.MasterCenter UnitStatus { get; set; }
        [Description("พื้นที่ขาย")]
        public double? SaleArea { get; set; }
        [Description("รหัสอาคาร")]
        public Guid? TowerID { get; set; }
        [ForeignKey("TowerID")]
        public Tower Tower { get; set; }

        [Description("รหัสชั้น")]
        public Guid? FloorID { get; set; }
        [ForeignKey("FloorID")]
        public Floor Floor { get; set; }

        [Description("สำนักงานที่ดิน")]
        public Guid? LandOfficeID { get; set; }
        [ForeignKey("LandOfficeID")]
        public MST.LandOffice LandOffice { get; set; }

        [Description("พื้นที่ใช้สอย")]
        public double? UsedArea { get; set; }
        [Description("พื้นที่จอดรถ")]
        public double? ParkingArea { get; set; }
        [Description("พื้นที่รั้วคอนกรีต")]
        public double? FenceArea { get; set; }
        [Description("พื้นที่รั้วเหล็กดัด")]
        public double? FenceIronArea { get; set; }
        [Description("พื้นที่รั้วระเบียง")]
        public double? BalconyArea { get; set; }
        [Description("พื้นที่วางแอร์")]
        public double? AirArea { get; set; }
        [Description("พื้นที่สระว่ายน้ำ")]
        public double? PoolArea { get; set; }
        
        /* Modified by Suchat S. 2020-07-04 for Add Address ID */
        //ที่อยู่ที่ตั้งโฉนดโอน
        [Description("ที่อยู่ที่ตั้งโฉนดโอน")]
        public Guid? AddressID { get; set; }
        [ForeignKey("AddressID")]
        public Address Address { get; set; }


        //ที่อยู่ตามทะเบียนบ้าน 
        [Description("ที่อยู่ทะเบียนบ้านเหมือนโฉนด")]
        public bool? IsSameAddressAsTitledeed { get; set; }
        [Description("บ้านเลขที่ (ตามทะเบียนบ้าน)")]
        [MaxLength(100)]
        public string CensusHouseNo { get; set; }
        [Description("รหัสไปรษณีย์ทะเบียนบ้าน")]
        [MaxLength(10)]
        public string HousePostalCode { get; set; }
        [Description("จังหวัดทะเบียนบ้าน")]
        public Guid? HouseProvinceID { get; set; }
        [ForeignKey("HouseProvinceID")]
        public MST.Province HouseProvince { get; set; }
        [Description("อำเภอ/เขต ทะเบียนบ้าน")]
        public Guid? HouseDistrictID { get; set; }
        [ForeignKey("HouseDistrictID")]
        public MST.District HouseDistrict { get; set; }
        [Description("ตำบล/แขวง ทะเบียนบ้าน")]
        public Guid? HouseSubDistrictID { get; set; }
        [ForeignKey("HouseSubDistrictID")]
        public MST.SubDistrict HouseSubDistrict { get; set; }
        [Description("หมู่ที่ ทะเบียนบ้าน")]
        [MaxLength(1000)]
        public string HouseMoo { get; set; }
        [Description("ซอย (ภาษาไทย)")]
        [MaxLength(1000)]
        public string HouseSoiTH { get; set; }
        [Description("ซอย (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string HouseSoiEN { get; set; }
        [Description("ถนน (ภาษาไทย)")]
        [MaxLength(1000)]
        public string HouseRoadTH { get; set; }
        [Description("ถนน (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string HouseRoadEN { get; set; }

        [Description("Remark")]
        [MaxLength(5000)]
        public string Remark { get; set; }
        [Description("จำนวนบุริมสิทธิ์")]
        public double? NumberOfPrivilege { get; set; }
        [Description("จำนวนที่จอดรถ Fix")]
        public double? NumberOfParkingFix { get; set; }
        [Description("จำนวนที่จอดรถไม่ Fix")]
        public double? NumberOfParkingUnFix { get; set; }
        [Description("ชื่อรูป FloorPlan")]
        [MaxLength(1000)]
        public string FloorPlanFileName { get; set; }

        [Description("ชื่อรูป RoomPlan")]
        [MaxLength(1000)]
        public string RoomPlanFileName { get; set; }

        //(For Data Migration)
        [Description("Code RR ที่ได้จากการ Post GL(รับรู้รายได้) เมื่อห้องนี้โอนแล้ว")]
        [MaxLength(1000)]
        public string GLRaiseBatchID { get; set; }
        [Description("")]
        [MaxLength(1000)]
        public string UnitLayoutType { get; set; }

        //Unit Meter
        [Description("เลขที่มิเตอร์ไฟฟ้า")]
        [MaxLength(100)]
        public string ElectricMeter { get; set; }
        [Description("วันที่บันทึกเลขที่มิเตอร์ไฟฟ้า")]
        public DateTime? ElectrictMeterSaved { get; set; }
        [Description("เลขที่มิเตอร์น้ำ")]
        [MaxLength(100)]
        public string WaterMeter { get; set; }
        [Description("วันที่บันทึกเลขที่มิเตอร์น้ำ")]
        public DateTime? WaterMeterSaved { get; set; }
        [Description("วันที่เอกสารครบ")]
        public DateTime? CompletedDocumentDate { get; set; }

        //MeterStatus
        [Description("สถานะมิเตอร์ไฟฟ้า")]
        public Guid? ElectricMeterStatusMasterCenterID { get; set; }
        [ForeignKey("ElectricMeterStatusMasterCenterID")]
        public MST.MasterCenter ElectrictMeterStatus { get; set; }

        //MeterStatus
        [Description("สถานะมิเตอร์น้ำ")]
        public Guid? WaterMeterStatusMasterCenterID { get; set; }
        [ForeignKey("WaterMeterStatusMasterCenterID")]
        public MST.MasterCenter WaterMeterStatus { get; set; }

        [Description("สถานะโอนมิเตอร์ไฟฟ้า")]
        public bool? IsTransferElectricMeter { get; set; }
        [Description("สถานะโอนมิเตอร์น้ำ")]
        public bool? IsTransferWaterMeter { get; set; }
        [Description("วันที่โอนมิเตอร์ไฟฟ้า")]
        public DateTime? ElectricMeterTransferDate { get; set; }
        [Description("วันที่บันทึกวันที่โอนมิเตอร์ไฟฟ้า")]
        public DateTime? ElectrictMeterTransferDateSaved { get; set; }
        [Description("วันที่โอนมิเตอร์น้ำ")]
        public DateTime? WaterMeterTransferDate { get; set; }
        [Description("วันที่บันทึกวันที่โอนมิเตอร์น้ำ")]
        public DateTime? WaterMeterTransferDateSaved { get; set; }
        [Description("วันที่บันทึกมิเตอร์")]
        public DateTime? MeterCreateDate { get; set; }
        [Description("วันที่แก้ไขมิเตอร์")]
        public DateTime? MeterUpdateDate{ get; set; }
        [Description("ผู้บันทึกมิเตอร์")]
        public Guid? MeterCreateByUserID { get; set; }
        [ForeignKey("MeterCreateByUserID")]
        public USR.User MeterCreateBy { get; set; }
        [Description("ผู้แก้ไขมิเตอร์")]
        public Guid? MeterUpdateByUserID { get; set; }
        [ForeignKey("MeterUpdateByUserID")]
        public USR.User MeterUpdateBy { get; set; }

        //MeterTopic
        [Description("หัวข้อมิเตอร์ไฟฟ้า")]
        public Guid? ElectricMeterTopicMasterCenterID { get; set; }
        [ForeignKey("ElectricMeterTopicMasterCenterID")]
        public MST.MasterCenter ElectricMeterTopic { get; set; }

        //MeterTopic
        [Description("หัวข้อมิเตอร์น้ำ")]
        public Guid? WaterMeterTopicMasterCenterID { get; set; }
        [ForeignKey("WaterMeterTopicMasterCenterID")]
        public MST.MasterCenter WaterMeterTopic { get; set; }

        [Description("หมายเหตุมิเตอร์ไฟฟ้า")]
        [MaxLength(5000)]
        public string ElectricMeterRemark { get; set; }
        [Description("หมายเหตุมิเตอร์น้ำ")]
        [MaxLength(5000)]
        public string WaterMeterRemark { get; set; }
        [Description("ราคามิเตอร์น้ำ")]
        public Guid? WaterMeterPriceID { get; set; }
        [ForeignKey("WaterMeterPriceID")]
        public PRJ.WaterElectricMeterPrice WaterMeterPrice { get; set; }
        [Description("ราคามิเตอร์ไฟ")]
        public Guid? ElectricMeterPriceID { get; set; }
        [ForeignKey("ElectricMeterPriceID")]
        public PRJ.WaterElectricMeterPrice ElectricMeterPrice { get; set; }

        [Description("วันที่โอนกรรมสิทธิ์")]
        public DateTime? TransferOwnerShipDate { get; set; }

        [Description("ใช้เก็บค่าจาก SAP สำหรับห้องที่โอนลอย")]
        [MaxLength(100)]
        public string GLPreTransferBatchID { get; set; }
        [Description("ให้ขายต่างชาติได้")]
        public bool IsForeignUnit { get; set; }

        [Description("ตำแหน่งห้อง (เฉพาะแนวสูง)")]
        [MaxLength(100)]
        public string Position { get; set; }

        //Budget Promotion
        [Description("SAP Budget Promotion ล่าสุด")]
        [Column(TypeName = "Money")]
        public decimal? SAPBudgetProAmount { get; set; }
        [Description("วันเวลาที่แก้ไข Budget Pro ใน SAP ล่าสุด")]
        public DateTime? SAPBudgetProUpdated { get; set; }

        //สถานะ Auto PR
        [Description("Auto Gen PR FGF Auto ถ้า false เลข PR ของ FGF จะเป็น 'Manual PR'")]
        public bool IsPRAutoFGF { get; set; }
        [Description("Auto Gen PR Promotion Item ถ้า false เลข PR ของ Item ที่ PromotionMaterialGroup.DocType=P01 จะเป็น 'Manual PR'")]
        public bool IsPRAutoCost { get; set; }
        [Description("Auto Gen PR Promotion Item ถ้า false เลข PR ของ Item ที่ PromotionMaterialGroup.DocType=P04 จะเป็น 'Manual PR'")]
        public bool IsPRAutoExpense { get; set; }
        [Description("Auto Gen PR Promotion Item ถ้า false เลข PR ของ Item ที่ PromotionMaterialGroup.DocType!=P01,P04 จะเป็น 'Manual PR'")]
        public bool IsPRAutoStand { get; set; }

        //Foreign Key
        public List<TitledeedDetail> TitledeedDetails { get; set; }

        [Description("สถานะเตรียมโอนกรรมสิทธิ์")]
        public bool? IsPrepareTransfer { get; set; }
        [Description("วันที่เตรียมโอนกรรมสิทธิ์")]
        public DateTime? PrepareTransferDate { get; set; }
        [Description("วันที่นัดโอนกรรมสิทธิ์")]
        public DateTime? ScheduleTransferDate { get; set; }

        [Description("ความสูงของพื้นที่ห้องชุด")]
        public float? RoomHeight { get; set; }

        [Description("พื้นที่ก่อสร้างตามใบอนุญาต")]
        public double? BuildingPermitArea { get; set; }



        [Description("IsEndProduct")]
        public bool? IsEndProduct { get; set; }
        [Description("วันที่ EndProduct")]
        public DateTime? EndProductDate { get; set; }
        [Description("คนที่ EndProduct")]
        public string EndProductBy { get; set; }



        public int? PositionX { get; set; }
        public int? PositionY { get; set; }
        public int? PhaseNo { get; set; }
        public bool? IsSaleOffice { get; set; }

        public string RemarkNotSale { get; set; }
        public bool? IsNotSale { get; set; }

        public Guid? NotSaleByUserID { get; set; }



        public bool? IsElectricMeterDocReceiptDate { get; set; }

        public DateTime? ElectricMeterDocReceiptDate { get; set; }

        public Guid? ElectricMeterDocReceiptByUserID { get; set; }
        //[ForeignKey("ElectricMeterDocReceiptByUserID")]
        //public USR.User ElectricMeterDocReceiptBy { get; set; }

        public bool? IsWaterMeterDocReceiptDate { get; set; }

        public DateTime? WaterMeterDocReceiptDate { get; set; }

        public Guid? WaterMeterDocReceiptByUserID { get; set; }
        //[ForeignKey("WaterMeterDocReceiptByUserID")]
        //public USR.User WaterMeterDocReceiptBy { get; set; }

        public Guid? ElectricMeterTransferDateByUserID { get; set; }
        //[ForeignKey("ElectricMeterTransferDateByUserID")]
        //public USR.User ElectricMeterTransferDateBy { get; set; }

        public Guid? WaterMeterTransferDateByUserID { get; set; }
        //[ForeignKey("WaterMeterTransferDateByUserID")]
        //public USR.User WaterMeterTransferDateBy { get; set; }


        public DateTime? ElectricMeterInstallDate { get; set; }
        public Guid? ElectricMeterInstallByUserID { get; set; }
        public DateTime? WaterMeterInstallDate { get; set; }
        public Guid? WaterMeterInstallByUserID { get; set; }
        public int? ElectricMeterBillingCycle { get; set; }
        public Guid? ElectricMeterBillingCycleByUserID { get; set; }
        public int? WaterMeterBillingCycleDate { get; set; }
        public Guid? WaterMeterBillingCycleByUserID { get; set; }
        public bool? IsElectricMeterTerminate { get; set; }
        public DateTime? ElectricMeterTerminateDate { get; set; }
        public Guid? ElectricMeterTerminateByUserID { get; set; }
        public bool? IsWaterMeterTerminate { get; set; }
        public DateTime? WaterMeterTerminateDate { get; set; }
        public Guid? WaterMeterTerminateByUserID { get; set; }



        // public bool? IsReceived { get; set; }
        // public DateTime? ReceivedDate { get; set; }
        // public string ReceiveCustomerName { get; set; }

    }
}


