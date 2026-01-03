using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลโครงการ")]
    [Table("Project", Schema = Schema.PROJECT)]
    public class Project : BaseEntity
    {
        [Description("รหัสโครงการ")]
        [MaxLength(50)]
        public string ProjectNo { get; set; }
        private string _sapCode;
        [Description("ข้อมูลโครงการ SAP")]
        [MaxLength(100)]
        public string SapCode {
            get { return _sapCode; }
            set {
                _sapCode = value;
                this.Plant = value?.Replace("/", string.Empty);
            }
        }
        [Description("รหัส SapCode ที่ตัด Slash (/) ออก ใช้กับ Master Promotion")]
        [MaxLength(100)]
        public string Plant { get; set; }
        [Description("เบอร์โทรศัพท์ติดต่อ")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        [Description("ชื่อโครงการ (ภาษาไทย)")]
        [MaxLength(100)]
        public string ProjectNameTH { get; set; }
        [Description("ชื่อโครงการ (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string ProjectNameEN { get; set; }
        [Description("ชื่อย่อโครงการ")]
        [MaxLength(100)]
        public string ProjectShortName { get; set; }

        [Description("ประเภทของโครงการ (แนวสูง แนวราบ)")]
        public Guid? ProductTypeMasterCenterID { get; set; }
        [ForeignKey("ProductTypeMasterCenterID")]
        public MST.MasterCenter ProductType { get; set; }

        [Description("ประเภทโครงการ")]
        public Guid? ProjectTypeMasterCenterID { get; set; }
        [ForeignKey("ProjectTypeMasterCenterID")]
        public MST.MasterCenter ProjectType { get; set; }
        [Description("มูลค่าโครงการ (บาท)")]
        [Column(TypeName = "Money")]
        public decimal? ProjectPrice { get; set; }

        [Description("BG")]
        public Guid? BGID { get; set; }
        [ForeignKey("BGID")]
        public MST.BG BG { get; set; }

        [Description("Sub BG")]
        public Guid? SubBGID { get; set; }
        [ForeignKey("SubBGID")]
        public MST.SubBG SubBG { get; set; }

        [Description("กลุ่มโครงการ")]
        [MaxLength(1000)]
        public string Group { get; set; }

        [Description("แบรนด์")]
        public Guid? BrandID { get; set; }
        [ForeignKey("BrandID")]
        public MST.Brand Brand { get; set; }

        [Description("บริษัท")]
        public Guid? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("รหัส Cost Center")]
        [MaxLength(1000)]
        public string CostCenterCode { get; set; }
        [Description("รหัส Profit Center")]
        [MaxLength(1000)]
        public string ProfitCenterCode { get; set; }
        [Description("วันที่เปิดโครงการ")]
        public DateTime? ProjectStartDate { get; set; }
        [Description("วันที่ปิดโครงการ")]
        public DateTime? ProjectEndDate { get; set; }
        [Description("วันที่สิ้นสุดการโอนลอย")]
        public DateTime? FloatingEndDate { get; set; }

        [Description("รหัสธนาคารที่จดจำนอง")]
        public Guid? MortgageBankID { get; set; }
        [ForeignKey("MortgageBankID")]
        public MST.Bank MortgageBank { get; set; }

        [Description("รวมเงินจดจำนอง")]
        [Column(TypeName = "Money")]
        public decimal? MortgageAmount { get; set; }
        [Description("จำนวนแปลงทั้งหมด (แปลง)")]
        public double? TotalUnit { get; set; }
        [Description("ข้อมูลพื้นที่จัดสรร (ไร่)")]
        [MaxLength(1000)]
        public string Rai { get; set; }
        [Description("ข้อมูลพื้นที่จัดสรร (งาน)")]
        [MaxLength(1000)]
        public string Ngan { get; set; }
        [Description("ข้อมูลพื้นที่จัดสรร (ตารางวา)")]
        [MaxLength(1000)]
        public string SqaureWa { get; set; }
        [Description("ข้อมูลอื่นๆ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("สถานะการขายของโครงการ")]
        public Guid? ProjectStatusMasterCenterID { get; set; }
        [ForeignKey("ProjectStatusMasterCenterID")]
        public MST.MasterCenter ProjectStatus { get; set; }

        [Description("สถานะ Active")]
        public bool IsActive { get; set; }

        [Description("WeChatID")]
        [MaxLength(1000)]
        public string WeChatID { get; set; }
        [Description("WhatsAppID")]
        [MaxLength(100)]
        public string WhatsAppID { get; set; }
        [Description("LineID")]
        [MaxLength(100)]
        public string LineID { get; set; }
        [Description("Logo")]
        [MaxLength(1000)]
        public string Logo { get; set; }

        [Description("เหตุผลที่ลบโครงการ")]
        [MaxLength(5000)]
        public string DeleteReason { get; set; }

        //Data Validate Status
        [Description("สถานะข้อมูลทั่วไป")]
        public Guid? GeneralDataStatusMasterCenterID { get; set; }
        [ForeignKey("GeneralDataStatusMasterCenterID")]
        public MST.MasterCenter GeneralDataStatus { get; set; }
        [Description("สถานะข้อมูลสัญญา")]
        public Guid? AgreementDataStatusMasterCenterID { get; set; }
        [ForeignKey("AgreementDataStatusMasterCenterID")]
        public MST.MasterCenter AgreementDataStatus { get; set; }
        [Description("สถานะข้อมูลแบบบ้าน")]
        public Guid? ModelDataStatusMasterCenterID { get; set; }
        [ForeignKey("ModelDataStatusMasterCenterID")]
        public MST.MasterCenter ModelDataStatus { get; set; }
        [Description("สถานะข้อมูลตึก")]
        public Guid? TowerDataStatusMasterCenterID { get; set; }
        [ForeignKey("TowerDataStatusMasterCenterID")]
        public MST.MasterCenter TowerDataStatus { get; set; }
        [Description("สถานะข้อมูลแปลง")]
        public Guid? UnitDataStatusMasterCenterID { get; set; }
        [ForeignKey("UnitDataStatusMasterCenterID")]
        public MST.MasterCenter UnitDataStatus { get; set; }
        [Description("สถานะข้อมูลโฉนด")]
        public Guid? TitleDeedDataStatusMasterCenterID { get; set; }
        [ForeignKey("TitleDeedDataStatusMasterCenterID")]
        public MST.MasterCenter TitleDeedDataStatus { get; set; }
        [Description("สถานะข้อมูลรูป")]
        public Guid? PictureDataStatusMasterCenterID { get; set; }
        [ForeignKey("PictureDataStatusMasterCenterID")]
        public MST.MasterCenter PictureDataStatus { get; set; }
        [Description("สถานะข้อมูล Min Price")]
        public Guid? MinPriceDataStatusMasterCenterID { get; set; }
        [ForeignKey("MinPriceDataStatusMasterCenterID")]
        public MST.MasterCenter MinPriceDataStatus { get; set; }
        [Description("สถานะข้อมูล Price List")]
        public Guid? PriceListDataStatusMasterCenterID { get; set; }
        [ForeignKey("PriceListDataStatusMasterCenterID")]
        public MST.MasterCenter PriceListDataStatus { get; set; }
        [Description("สถานะข้อมูลค่าธรรมเนียมโอน")]
        public Guid? TransferFeeDataStatusMasterCenterID { get; set; }
        [ForeignKey("TransferFeeDataStatusMasterCenterID")]
        public MST.MasterCenter TransferFeeDataStatus { get; set; }
        [Description("สถานะข้อมูล Budget Promotion")]
        public Guid? BudgetProDataStatusMasterCenterID { get; set; }
        [ForeignKey("BudgetProDataStatusMasterCenterID")]
        public MST.MasterCenter BudgetProDataStatus { get; set; }
        [Description("สถานะข้อมูล Waive QC & Waive รับบ้าน")]
        public Guid? WaiveDataStatusMasterCenterID { get; set; }
        [ForeignKey("WaiveDataStatusMasterCenterID")]
        public MST.MasterCenter WaiveDataStatus { get; set; }

        [Description("สัดส่วนต่างชาติ (เมื่อมีเปลี่ยนแปลงผู้จองหรือผู้ทำสัญญา การขายต่างชาติ ให้อัพเดตค่าโดยคิดจากพื้นที่ขายรวมทั้งโครงการ)")]
        public double ForeignerRatio { get; set; }

        //สถานะ Auto PR
        [Description("Auto Gen PR FGF Auto ถ้า false เลข PR ของ FGF จะเป็น 'Manual PR'")]
        public bool IsPRAutoFGF { get; set; }
        [Description("Auto Gen PR Promotion Item ถ้า false เลข PR ของ Item ที่ PromotionMaterialGroup.DocType=P01 จะเป็น 'Manual PR'")]
        public bool IsPRAutoCost { get; set; }
        [Description("Auto Gen PR Promotion Item ถ้า false เลข PR ของ Item ที่ PromotionMaterialGroup.DocType=P04 จะเป็น 'Manual PR'")]
        public bool IsPRAutoExpense { get; set; }
        [Description("Auto Gen PR Promotion Item ถ้า false เลข PR ของ Item ที่ PromotionMaterialGroup.DocType!=P01,P04 จะเป็น 'Manual PR'")]
        public bool IsPRAutoStand { get; set; }

        public List<USR.UserAuthorizeProject> UserAuthorizeProjects { get; set; }

        [Description("% ฟรีดาวน์")]
        public double? PercentFreeDown { get; set; }
        [Description("จำนวนเงินฟรีดาวน์สูงสุด")]
        public decimal? MaxFreeDownAmount { get; set; }

        [Description("flag ให้ขายต่างชาติได้ เพื่อออกเอกสารจองหรือสัญญา")]
        public bool? IsForeignProject { get; set; }

        [Description("flag เป็นโครงการค่าส่วนกลางเหมาจ่าย")]
        public bool? IsPublicFundPayProject { get; set; }

        //Modified by Suchat S. 2020-10-20 for JobHelpDesk : REQ2020010080
        [Description("วันที่โอนห้องหลังแรก")]
        public DateTime? FirstUnitTransferDate { get; set; }
        [Description("วันที่โอนห้องหลังสุดท้าย")]
        public DateTime? LasttUnitTransferDate { get; set; }

        //Modified by Suchat S. 2020-10-27 for JobHelpDesk : REQ2020010233
        [Description("flag เป็นโครงการเป็นประเภท Up-Country (ต่างจังหวัด) หรือไม่")]
        public bool? IsUpCountry { get; set; }

        [Description("บระบุวันไถ่ถอนโฉนดของโครงการนั้น กรณีที่ระบุวันที่ แสดงว่าโครงการนั้นได้มีการไถ่ถอนโฉนดเรียบร้อยแล้ว")]
        public DateTime? RedeemLoanDate { get; set; }

        [Description("ปลดล็อควันจอง เริ่มต้น")]
        public DateTime? StartUnlockBookingDate { get; set; }

        [Description("ปลดล็อควันจอง สิ้นสุด")]
        public DateTime? EndUnlockBookingDate { get; set; }


        [Description("Flag แสดงว่าเป็น NewProject")]
        public bool IsNewProject { get; set; }

        [Description("วันที่มีการแก้ไขค่า IsNewProject")]
        public DateTime? ChangeNewProjectDate { get; set; }

        [Description("User ที่ทำการแก้ไขค่า IsNewProject(UUID)")]
        public Guid? ChangeNewProjectByUserID { get; set; }
        [ForeignKey("ChangeNewProjectByUserID")]
        public USR.User ChangeNewProjectByUser { get; set; }

        [Description("ZoneID")]
        public Guid? ZoneID { get; set; }
        [ForeignKey("ZoneID")]
        public MST.Zone Zone { get; set; }


        public Guid? TitledeedConfigID { get; set; }
        [ForeignKey("TitledeedConfigID")]
        public PRJ.TitledeedConfig TitledeedConfig { get; set; }

        [Description("Folder ไฟล์แนบ")]
        [MaxLength(1000)]
        public string MasterPlanFilePath { get; set; }

        [Description("ชื่อไฟล์แนบ")]
        [MaxLength(1000)]
        public string MasterPlanFileName { get; set; }

        public DateTime? ForeignProjectStart { get; set; }
        public DateTime? ForeignProjectEnd { get; set; }
        public Guid? ForeignProjectByUserID { get; set; }
    }
}
