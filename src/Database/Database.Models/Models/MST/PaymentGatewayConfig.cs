using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ข้อมูล PaymentGatewayConfig")]
    [Table("PaymentGatewayConfig", Schema = Schema.MASTER)]
    public class PaymentGatewayConfig : BaseEntityWithoutMigrate
    {
        public Guid? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public Company Company { get; set; }

        public string SAPCompanyID { get; set; }

        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        public string ProjectNo { get; set; }

        public string APIUrl { get; set; }

        public string APIKey { get; set; }

        public string MerchantID { get; set; }

        public string TerminalID { get; set; }

        public string MerchantName { get; set; }

        public string MerchantLocation { get; set; }

        public int ExpiredTimeMinute { get; set; }
    }
}
