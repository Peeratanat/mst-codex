using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูล Opportunity")]
    [Table("Opportunity", Schema = Schema.CUSTOMER)]
    public class Opportunity : BaseEntity
    {
        [Description("รหัสลูกค้า")]
        public Guid ContactID { get; set; }
        [ForeignKey("ContactID")]
        public Contact Contact { get; set; }
        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
        [Description("วันที่เยี่ยมชมโครงการ")]
        public DateTime? ArriveDate { get; set; }
        [Description("สินค้าที่สนใจ 1")]
        [MaxLength(1000)]
        public string InterestedProduct1 { get; set; }
        [Description("สินค้าที่สนใจ 2")]
        [MaxLength(1000)]
        public string InterestedProduct2 { get; set; }
        [Description("สินค้าที่สนใจ 3")]
        [MaxLength(1000)]
        public string InterestedProduct3 { get; set; }

        [Description("LC Owner")]
        public Guid? OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public USR.User Owner { get; set; }

        [Description("ประเมินโอกาสการขาย")]
        public Guid? EstimateSalesOpportunityMasterCenterID { get; set; }
        [ForeignKey("EstimateSalesOpportunityMasterCenterID")]
        public MST.MasterCenter EstimateSalesOpportunity { get; set; }

        [Description("สถานะการทำแบบสอบถาม")]
        public Guid? StatusQuestionaireMasterCenterID { get; set; }
        [ForeignKey("StatusQuestionaireMasterCenterID")]
        public MST.MasterCenter StatusQuestionaire { get; set; }
        [Description("วันที่ตอบแบบสอบถาม")]
        public DateTime? QuestionaireDate { get; set; }

        [Description("โอกาสขาย (สถานะ)")]
        public Guid? SalesOpportunityMasterCenterID { get; set; }
        [ForeignKey("SalesOpportunityMasterCenterID")]
        public MST.MasterCenter SalesOpportunity { get; set; }

        [Description("จำนวนแปลงที่สนใจ")]
        public int ProductQTY { get; set; }
        [Description("โครงการเปรียบเทียบ")]
        [MaxLength(1000)]
        public string ProjectCompare { get; set; }
        [Description("Remark")]
        [MaxLength(5000)]
        public string Remark { get; set; }
        [Description("เหตุผลที่ซื้อ")]
        [MaxLength(1000)]
        public string BuyReason { get; set; }

        [Description("Last Activity")]
        public Guid? LastOpportunityActivityID { get; set; }
        [ForeignKey("LastOpportunityActivityID")]
        public OpportunityActivity LastOpportunityActivity { get; set; }

        [Description("จำนวน Revisit")]
        public int RevisitActivityCount { get; set; }

        [Description("ประเภทการยินยอม (ทุกโครงการ = AllProject, เฉพาะโครงการ = OnlyProject, ไม่ยินยอม = NotConsent)")]
        public Guid? ConsentTypeMasterCenterID { get; set; }
        [ForeignKey("ConsentTypeMasterCenterID")]
        public MST.MasterCenter ConsentType { get; set; }

        [Description("ส่งข้อมูล Opp ให้ BC หรือไม่")]
        public bool IsSendToBC { get; set; }
    }
}
