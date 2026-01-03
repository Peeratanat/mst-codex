using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("OnlinePaymentHistory")]
    [Table("OnlinePaymentHistory", Schema = Schema.FINANCE)]
    public class OnlinePaymentHistory : BaseEntityWithoutMigrate
    {
        [Description("Booking No")]
        public string BookingNo { get; set; }        
        [Description("Project No")]
        public string ProjectNo { get; set; }        
        [Description("Unit No")]
        public string UnitNo { get; set; }        
        [Description("Contact No")]
        public string ContactNo { get; set; }
        [Description("Amount")]
        [MaxLength(100)]
        public decimal? Amount { get; set; }
        [Description("Payment Type")]
        public string PaymentType { get; set; }
        [Description("Emp Code")]
        public string EmpCode { get; set; }        
        [Description("Payment Channel")]
        public string PaymentChannel { get; set; }        
        [Description("Payment_Service Name")]
        public string Payment_Service_Name { get; set; }        
        [Description("Link_Ref")]
        public string Link_Ref { get; set; }        
        [Description("Link_Url")]
        public string Link_Url { get; set; }
        [Description("Currency")]
        public string Currency { get; set; }
        [Description("Active_Time")]
        public string Active_Time { get; set; }
        [Description("Expire_Time")]
        public string Expire_Time { get; set; }
        [Description("Payment_Description")]
        public string Payment_Description { get; set; }
        [Description("Payment_Status")]
        public string Payment_Status { get; set; }
        [Description("Payment_Create_date")]
        public string Payment_Create_date { get; set; }
        [Description("Reference_Number")]
        public string Reference_Number { get; set; }
        [Description("Merchant_Id")]
        public string Merchant_Id { get; set; }
        [Description("Merchant_Name")]
        public string Merchant_Name { get; set; }
        [Description("Ref_1")]
        public string Ref_1 { get; set; }
        [Description("Ref_2")]
        public string Ref_2 { get; set; }
        [Description("Ref_3")]
        public string Ref_3 { get; set; }
        [Description("Info_1")]
        public string Info_1 { get; set; }
        [Description("Info_2")]
        public string Info_2 { get; set; }
        [Description("Info_3")]
        public string Info_3 { get; set; }
        [Description("Remark")]
        public string Remark { get; set; }
        [Description("SourceID")]
        public string SourceID { get; set; }
        [Description("SourceObject")]
        public string SourceObject { get; set; }
        [Description("SourceBrand")]
        public string SourceBrand { get; set; }
        [Description("SourceCardMasking")]
        public string SourceCardMasking { get; set; }
        [Description("SourceIssuerBank")]
        public string SourceIssuerBank { get; set; }
        [Description("BookingID")]
        public Guid? BookingID { get; set; }
    }
}