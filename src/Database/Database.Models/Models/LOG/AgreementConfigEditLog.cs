using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Description("เก็บ log การ แก้ไข AgreementConfig")]
    [Table("AgreementConfigEditLog", Schema = Schema.LOG)]
    public class AgreementConfigEditLog : BaseEntityWithoutMigrate
    {
        public Guid ProjectID { get; set; }
        public DateTime? LicenseProductExpireDate { get; set; }
        public DateTime? LicenseProductIssueDate { get; set; }
        public string LicenseProductNo { get; set; }
        public string LicenseProductRemark { get; set; }
        public DateTime? PreLicenseLandExpireDate { get; set; }
        public string PreLicenseLandNo { get; set; }
        public DateTime? ExpectedEnvironmentalApprovalDate { get; set; }
        public DateTime? CondoConstructionPermitSubmitDate { get; set; }
        public DateTime? ExpectedPermitReceiveDate { get; set; }
        public string CondoConstructionPermitNo { get; set; }
        public DateTime? CondoConstructionPermitDate { get; set; }
        public DateTime? CondoConstructionPermitExpireDate { get; set; }
        public string CondoConstructionPermitRemark { get; set; }
    }
}
