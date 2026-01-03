using Database.Models.MST;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("หนังสือสัญญาค้ำประกัน")]
    [Table("LetterGuarantee", Schema = Schema.PROJECT)]
    public class LetterGuarantee : BaseEntityWithoutMigrate
    {

        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string MeterNumber { get; set; }
        public bool? IsJuristicSetup { get; set; }
        public DateTime? JuristicSetupDate { get; set; }
        public string JuristicSetupBy { get; set; }
        public string JuristicSetupRemarks { get; set; }

        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public Bank Banks { get; set; }

        public string Bank { get; set; }

        public Guid? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public Company Company { get; set; }

        public string CompanyCode { get; set; }
        public string CostCenter { get; set; }

        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        public double? ProjectArea { get; set; }
        public string LetterOfGuaranteeNo { get; set; }

        public Guid? LGGuarantorMasterCenterID { get; set; }
        [ForeignKey("LGGuarantorMasterCenterID")]
        public MasterCenter LGGuarantor { get; set; }

        public Guid? LGTypeMasterCenterID { get; set; }
        [ForeignKey("LGTypeMasterCenterID")]
        public MasterCenter LGType { get; set; }

        public decimal? IssueAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public decimal? RemainAmount { get; set; }
        public Guid? LGGuaranteeConditionsMasterCenterID { get; set; }
        [ForeignKey("LGGuaranteeConditionsMasterCenterID")]
        public MasterCenter LGGuaranteeConditions { get; set; }

        public string Remark { get; set; }
        public bool? IsCanceled { get; set; }
        public DateTime? CancelDate { get; set; }

        public Guid? CancelByUserID { get; set; }
        //[ForeignKey("CancelByUserID")]
        //public User CancelBy { get; set; }

        public string CancelRemark { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? ConditionCalFee { get; set; }
        public double? FeeRate { get; set; }
        public decimal? FeeRateAmountByPeriod { get; set; }

        public DateTime? ExpiredPeriodDate { get; set; }

        public Guid? LGSubTypeMasterCenterID { get; set; }
        [ForeignKey("LGSubTypeMasterCenterID")]
        public MasterCenter LGSubType { get; set; }
    }

    
}
